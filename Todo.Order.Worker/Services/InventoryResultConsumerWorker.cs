using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Todo.DAL.Context;
using Todo.DAL.Dto;
using Todo.DAL.Entity;
using Todo.Order.Worker.Configuration;

namespace Todo.Order.Worker.Services;

public sealed class InventoryResultConsumerWorker : BackgroundService
{
    private const string ConsumerName = "order-result-consumer";

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<InventoryResultConsumerWorker> _logger;
    private readonly RabbitMqOptions _options;
    private IConnection? _connection;
    private IChannel? _channel;
    private AsyncEventingBasicConsumer? _consumer;
    private string? _consumerTag;
    private CancellationToken _stoppingToken;

    public InventoryResultConsumerWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<InventoryResultConsumerWorker> logger,
        IOptions<RabbitMqOptions> options)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _stoppingToken = stoppingToken;

        var factory = new ConnectionFactory
        {
            HostName = _options.Host,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password,
            VirtualHost = _options.VirtualHost
        };

        _connection = await factory.CreateConnectionAsync(stoppingToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);
        await EnsureConsumeTopologyAsync(_channel, stoppingToken);
        await _channel.BasicQosAsync(0, 1, false, stoppingToken);

        _consumer = new AsyncEventingBasicConsumer(_channel);
        _consumer.ReceivedAsync += OnReceivedAsync;
        _consumerTag = await _channel.BasicConsumeAsync(_options.OrderResultQueue, autoAck: false, _consumer, stoppingToken);
        _logger.LogInformation("Inventory result consumer started. Queue={Queue}.", _options.OrderResultQueue);

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (_channel is { IsOpen: true } && !string.IsNullOrWhiteSpace(_consumerTag))
            {
                await _channel.BasicCancelAsync(_consumerTag, noWait: false, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to cancel consumer {ConsumerTag}.", _consumerTag);
        }

        if (_consumer is not null)
        {
            _consumer.ReceivedAsync -= OnReceivedAsync;
        }

        if (_channel is not null)
        {
            await _channel.DisposeAsync();
        }

        if (_connection is not null)
        {
            await _connection.DisposeAsync();
        }

        _channel = null;
        _connection = null;
        _consumer = null;
        _consumerTag = null;

        await base.StopAsync(cancellationToken);
    }

    private async Task OnReceivedAsync(object sender, BasicDeliverEventArgs eventArgs)
    {
        var handled = await HandleMessageAsync(eventArgs, _stoppingToken);
        var channel = _channel;
        if (channel is null || !channel.IsOpen)
        {
            return;
        }

        try
        {
            if (handled)
            {
                await channel.BasicAckAsync(eventArgs.DeliveryTag, false, _stoppingToken);
            }
            else
            {
                await channel.BasicNackAsync(eventArgs.DeliveryTag, false, true, _stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to ack/nack message {DeliveryTag}.", eventArgs.DeliveryTag);
        }
    }

    private async Task EnsureConsumeTopologyAsync(IChannel channel, CancellationToken cancellationToken)
    {
        await channel.ExchangeDeclareAsync(
            exchange: _options.InventoryExchange,
            type: ExchangeType.Direct,
            durable: true,
            autoDelete: false,
            arguments: null,
            passive: false,
            cancellationToken: cancellationToken);
        await channel.QueueDeclareAsync(
            queue: _options.OrderResultQueue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            passive: false,
            cancellationToken: cancellationToken);
        await channel.QueueBindAsync(
            queue: _options.OrderResultQueue,
            exchange: _options.InventoryExchange,
            routingKey: MessagingEventTypes.InventoryReserved,
            arguments: null,
            noWait: false,
            cancellationToken: cancellationToken);
        await channel.QueueBindAsync(
            queue: _options.OrderResultQueue,
            exchange: _options.InventoryExchange,
            routingKey: MessagingEventTypes.InventoryFailed,
            arguments: null,
            noWait: false,
            cancellationToken: cancellationToken);
    }

    private async Task<bool> HandleMessageAsync(BasicDeliverEventArgs eventArgs, CancellationToken cancellationToken)
    {
        try
        {
            var payload = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var message = JsonSerializer.Deserialize<InventoryResultEvent>(payload);
            if (message is null)
            {
                _logger.LogWarning("Ignoring inventory result message with invalid payload.");
                return true;
            }

            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();

            var alreadyProcessed = await dbContext.OrderInboxMessages
                .AsNoTracking()
                .AnyAsync(
                    x => x.MessageId == message.MessageId && x.Consumer == ConsumerName,
                    cancellationToken);
            if (alreadyProcessed)
            {
                return true;
            }

            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            dbContext.OrderInboxMessages.Add(new OrderInboxMessage
            {
                MessageId = message.MessageId,
                Consumer = ConsumerName,
                ProcessedAtUtc = DateTimeOffset.UtcNow
            });

            var order = await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == message.OrderId, cancellationToken);
            if (order is null)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogWarning("Order {OrderId} not found while processing inventory result.", message.OrderId);
                return true;
            }

            order.UpdatedAtUtc = DateTimeOffset.UtcNow;
            if (message.EventType == MessagingEventTypes.InventoryReserved)
            {
                order.Status = OrderStatus.Reserved;
                order.FailureReason = null;
            }
            else
            {
                order.Status = OrderStatus.Cancelled;
                order.FailureReason = message.FailureReason;
            }

            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process inventory result message.");
            return false;
        }
    }
}
