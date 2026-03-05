using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Todo.DAL.Context;
using Todo.DAL.Dto;
using Todo.DAL.Entity;
using Todo.Inventory.Worker.Configuration;

namespace Todo.Inventory.Worker.Services;

public sealed class InventoryEventConsumerWorker : BackgroundService
{
    private const string ConsumerName = "inventory-order-created-consumer";

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<InventoryEventConsumerWorker> _logger;
    private readonly RabbitMqOptions _options;
    private IConnection? _connection;
    private IChannel? _channel;
    private AsyncEventingBasicConsumer? _consumer;
    private string? _consumerTag;
    private CancellationToken _stoppingToken;

    public InventoryEventConsumerWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<InventoryEventConsumerWorker> logger,
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

        _consumerTag = await _channel.BasicConsumeAsync(_options.InventoryReserveQueue, autoAck: false, _consumer, stoppingToken);
        _logger.LogInformation("Inventory order-created consumer started. Queue={Queue}.", _options.InventoryReserveQueue);

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
            exchange: _options.OrderExchange,
            type: ExchangeType.Direct,
            durable: true,
            autoDelete: false,
            arguments: null,
            passive: false,
            cancellationToken: cancellationToken);
        await channel.ExchangeDeclareAsync(
            exchange: _options.InventoryExchange,
            type: ExchangeType.Direct,
            durable: true,
            autoDelete: false,
            arguments: null,
            passive: false,
            cancellationToken: cancellationToken);
        await channel.QueueDeclareAsync(
            queue: _options.InventoryReserveQueue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            passive: false,
            cancellationToken: cancellationToken);
        await channel.QueueBindAsync(
            queue: _options.InventoryReserveQueue,
            exchange: _options.OrderExchange,
            routingKey: MessagingEventTypes.OrderCreated,
            arguments: null,
            noWait: false,
            cancellationToken: cancellationToken);
    }

    private async Task<bool> HandleMessageAsync(BasicDeliverEventArgs eventArgs, CancellationToken cancellationToken)
    {
        try
        {
            var payload = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var message = JsonSerializer.Deserialize<OrderCreatedEvent>(payload);
            if (message is null)
            {
                _logger.LogWarning("Ignoring order-created message with invalid payload.");
                return true;
            }

            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();

            var processed = await dbContext.InventoryInboxMessages
                .AsNoTracking()
                .AnyAsync(
                    x => x.MessageId == message.MessageId && x.Consumer == ConsumerName,
                    cancellationToken);
            if (processed)
            {
                return true;
            }

            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            dbContext.InventoryInboxMessages.Add(new InventoryInboxMessage
            {
                MessageId = message.MessageId,
                Consumer = ConsumerName,
                ProcessedAtUtc = DateTimeOffset.UtcNow
            });

            var stock = await dbContext.InventoryStocks.FirstOrDefaultAsync(x => x.Sku == message.Sku, cancellationToken);
            if (stock is null)
            {
                stock = new InventoryStock
                {
                    Sku = message.Sku,
                    AvailableQuantity = 100,
                    UpdatedAtUtc = DateTimeOffset.UtcNow
                };
                dbContext.InventoryStocks.Add(stock);
            }

            var resultEvent = new InventoryResultEvent
            {
                MessageId = Guid.NewGuid(),
                OrderId = message.OrderId,
                OccurredOnUtc = DateTimeOffset.UtcNow
            };

            if (message.SimulateInventoryFailure)
            {
                resultEvent.EventType = MessagingEventTypes.InventoryFailed;
                resultEvent.FailureReason = "Simulated inventory failure.";
            }
            else if (stock.AvailableQuantity < message.Quantity)
            {
                resultEvent.EventType = MessagingEventTypes.InventoryFailed;
                resultEvent.FailureReason = $"Insufficient stock for SKU {message.Sku}.";
            }
            else
            {
                stock.AvailableQuantity -= message.Quantity;
                stock.UpdatedAtUtc = DateTimeOffset.UtcNow;
                resultEvent.EventType = MessagingEventTypes.InventoryReserved;
            }

            dbContext.InventoryOutboxMessages.Add(new InventoryOutboxMessage
            {
                MessageId = resultEvent.MessageId,
                CorrelationId = message.OrderId,
                EventType = resultEvent.EventType,
                Payload = JsonSerializer.Serialize(resultEvent),
                OccurredOnUtc = resultEvent.OccurredOnUtc
            });

            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process order-created message.");
            return false;
        }
    }
}
