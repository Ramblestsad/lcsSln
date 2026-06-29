using System.Text;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Todo.DAL.Data;
using Todo.DAL.Messaging;
using Todo.Inventory.Worker.Configuration;

namespace Todo.Inventory.Worker.Services;

public sealed class InventoryEventConsumerWorker : BackgroundService
{
    private const string ConsumerName = "inventory-order-created-consumer";
    private const int InitialStockQuantity = 100;
    private static readonly ActivitySource ActivitySource = new("Todo.Inventory.Worker");

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
        using var activity = ActivitySource.StartActivity("inventory.order-created.consume", ActivityKind.Consumer);
        activity?.SetTag("messaging.system", "rabbitmq");
        activity?.SetTag("messaging.destination.name", _options.InventoryReserveQueue);
        activity?.SetTag("messaging.rabbitmq.routing_key", eventArgs.RoutingKey);

        try
        {
            var payload = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var message = JsonSerializer.Deserialize<OrderCreatedEvent>(payload);
            if (message is null)
            {
                activity?.SetStatus(ActivityStatusCode.Error, "Invalid payload");
                _logger.LogWarning("Ignoring order-created message with invalid payload.");
                return true;
            }
            if (message.Quantity <= 0 || string.IsNullOrWhiteSpace(message.Sku))
            {
                activity?.SetStatus(ActivityStatusCode.Error, "Invalid order-created message");
                _logger.LogWarning("Ignoring order-created message with invalid order data.");
                return true;
            }

            activity?.SetTag("messaging.message.type", MessagingEventTypes.OrderCreated);

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

            var now = DateTimeOffset.UtcNow;
            var resultEvent = new InventoryResultEvent
            {
                MessageId = Guid.NewGuid(),
                OrderId = message.OrderId,
                OccurredOnUtc = now
            };

            await dbContext.Database.ExecuteSqlInterpolatedAsync(
                $"""
                INSERT INTO inventory_stocks ("Sku", "AvailableQuantity", "UpdatedAtUtc")
                VALUES ({message.Sku}, {InitialStockQuantity}, {now})
                ON CONFLICT ("Sku") DO NOTHING
                """,
                cancellationToken);

            if (message.SimulateInventoryFailure)
            {
                resultEvent.EventType = MessagingEventTypes.InventoryFailed;
                resultEvent.FailureReason = "Simulated inventory failure.";
            }
            else
            {
                var reserved = await dbContext.InventoryStocks
                    .Where(x => x.Sku == message.Sku && x.AvailableQuantity >= message.Quantity)
                    .ExecuteUpdateAsync(
                        setters => setters
                            .SetProperty(x => x.AvailableQuantity, x => x.AvailableQuantity - message.Quantity)
                            .SetProperty(x => x.UpdatedAtUtc, now),
                        cancellationToken);

                if (reserved == 0)
                {
                    resultEvent.EventType = MessagingEventTypes.InventoryFailed;
                    resultEvent.FailureReason = $"Insufficient stock for SKU {message.Sku}.";
                }
                else
                {
                    resultEvent.EventType = MessagingEventTypes.InventoryReserved;
                }
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
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            _logger.LogError(ex, "Failed to process order-created message.");
            return false;
        }
    }
}
