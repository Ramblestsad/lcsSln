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
    private IModel? _channel;
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
            VirtualHost = _options.VirtualHost,
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        EnsureConsumeTopology(_channel);
        _channel.BasicQos(0, 1, false);

        _consumer = new AsyncEventingBasicConsumer(_channel);
        _consumer.Received += OnReceivedAsync;
        _consumerTag = _channel.BasicConsume(_options.OrderResultQueue, autoAck: false, _consumer);
        _logger.LogInformation("Inventory result consumer started. Queue={Queue}.", _options.OrderResultQueue);

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (_channel is { IsOpen: true } && !string.IsNullOrWhiteSpace(_consumerTag))
            {
                _channel.BasicCancel(_consumerTag);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to cancel consumer {ConsumerTag}.", _consumerTag);
        }

        if (_consumer is not null)
        {
            _consumer.Received -= OnReceivedAsync;
        }

        _channel?.Dispose();
        _connection?.Dispose();
        _channel = null;
        _connection = null;
        _consumer = null;
        _consumerTag = null;

        return base.StopAsync(cancellationToken);
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
                channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            else
            {
                channel.BasicNack(eventArgs.DeliveryTag, false, true);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to ack/nack message {DeliveryTag}.", eventArgs.DeliveryTag);
        }
    }

    private void EnsureConsumeTopology(IModel channel)
    {
        channel.ExchangeDeclare(_options.InventoryExchange, ExchangeType.Direct, durable: true, autoDelete: false);
        channel.QueueDeclare(_options.OrderResultQueue, durable: true, exclusive: false, autoDelete: false);
        channel.QueueBind(_options.OrderResultQueue, _options.InventoryExchange, routingKey: MessagingEventTypes.InventoryReserved);
        channel.QueueBind(_options.OrderResultQueue, _options.InventoryExchange, routingKey: MessagingEventTypes.InventoryFailed);
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
