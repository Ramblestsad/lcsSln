using System.Data.Common;
using System.Diagnostics;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Todo.DAL.Data;
using Todo.Order.Worker.Configuration;

namespace Todo.Order.Worker.Services;

public sealed class OrderOutboxDispatcherWorker: BackgroundService
{
    private const int BatchSize = 20;
    private const int OutboxLockSeconds = 300;
    private static readonly ActivitySource ActivitySource = new("Todo.Order.Worker");

    private readonly IServiceScopeFactory scopeFactory;
    private readonly ILogger<OrderOutboxDispatcherWorker> logger;
    private readonly RabbitMqOptions options;

    public OrderOutboxDispatcherWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<OrderOutboxDispatcherWorker> logger,
        IOptions<RabbitMqOptions> options)
    {
        this.scopeFactory = scopeFactory;
        this.logger = logger;
        this.options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = BuildConnectionFactory();
        await using var connection = await factory.CreateConnectionAsync(stoppingToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
        await EnsureTopologyAsync(channel, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await DispatchPendingMessagesAsync(channel, stoppingToken);
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
        }
    }

    private async Task DispatchPendingMessagesAsync(IChannel channel, CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
        var pendingMessages = await ClaimPendingMessagesAsync(dbContext, cancellationToken);

        if (pendingMessages.Count == 0)
        {
            return;
        }

        using var activity = ActivitySource.StartActivity("order.outbox.dispatch", ActivityKind.Producer);
        activity?.SetTag("messaging.system", "rabbitmq");
        activity?.SetTag("messaging.destination.name", options.OrderExchange);
        activity?.SetTag("messaging.batch.message_count", pendingMessages.Count);

        foreach (var message in pendingMessages)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message.Payload);
                var props = new BasicProperties
                {
                    MessageId = message.MessageId.ToString(),
                    CorrelationId = message.CorrelationId.ToString(),
                    Persistent = true,
                    Type = message.EventType
                };

                await channel.BasicPublishAsync(
                    exchange: options.OrderExchange,
                    routingKey: message.EventType,
                    mandatory: false,
                    basicProperties: props,
                    body: body,
                    cancellationToken: cancellationToken);

                await MarkPublishedAsync(dbContext, message.Id, cancellationToken);
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                await MarkFailedAsync(dbContext, message.Id, ex.Message, cancellationToken);
                logger.LogError(ex, "Failed to publish order outbox message {MessageId}.", message.MessageId);
            }
        }
    }

    private static async Task<List<PendingOutboxMessage>> ClaimPendingMessagesAsync(
        ApplicationIdentityDbContext dbContext,
        CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        await using var command = dbContext.Database.GetDbConnection().CreateCommand();
        command.Transaction = transaction.GetDbTransaction();
        command.CommandText =
            // language=PostgreSQL
            """
            WITH picked AS (
                SELECT "Id"
                FROM order_outbox_messages
                WHERE "PublishedOnUtc" IS NULL
                  AND ("LockedUntilUtc" IS NULL OR "LockedUntilUtc" <= now())
                ORDER BY "Id"
                LIMIT @limit
                FOR UPDATE SKIP LOCKED
            )
            UPDATE order_outbox_messages AS outbox
            SET "LockedUntilUtc" = now() + (@lockSeconds * INTERVAL '1 second')
            FROM picked
            WHERE outbox."Id" = picked."Id"
            RETURNING outbox."Id", outbox."MessageId", outbox."CorrelationId", outbox."EventType", outbox."Payload";
            """;
        AddParameter(command, "limit", BatchSize);
        AddParameter(command, "lockSeconds", OutboxLockSeconds);

        var messages = new List<PendingOutboxMessage>();
        await using (var reader = await command.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                messages.Add(new PendingOutboxMessage(
                    reader.GetInt64(0),
                    reader.GetGuid(1),
                    reader.GetGuid(2),
                    reader.GetString(3),
                    reader.GetString(4)));
            }
        }

        await transaction.CommitAsync(cancellationToken);
        return messages;
    }

    private static async Task MarkPublishedAsync(
        ApplicationIdentityDbContext dbContext,
        long id,
        CancellationToken cancellationToken)
    {
        await dbContext.OrderOutboxMessages
            .Where(message => message.Id == id)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(message => message.PublishedOnUtc, DateTimeOffset.UtcNow)
                    .SetProperty(message => message.LockedUntilUtc, (DateTimeOffset?)null)
                    .SetProperty(message => message.LastError, (string?)null),
                cancellationToken);
    }

    private static async Task MarkFailedAsync(
        ApplicationIdentityDbContext dbContext,
        long id,
        string error,
        CancellationToken cancellationToken)
    {
        var lastError = error[..Math.Min(1000, error.Length)];
        await dbContext.OrderOutboxMessages
            .Where(message => message.Id == id)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(message => message.RetryCount, message => message.RetryCount + 1)
                    .SetProperty(message => message.LockedUntilUtc, (DateTimeOffset?)null)
                    .SetProperty(message => message.LastError, lastError),
                cancellationToken);
    }

    private static void AddParameter(DbCommand command, string name, object value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value;
        command.Parameters.Add(parameter);
    }

    private ConnectionFactory BuildConnectionFactory()
    {
        return new ConnectionFactory
        {
            HostName = options.Host,
            Port = options.Port,
            UserName = options.Username,
            Password = options.Password,
            VirtualHost = options.VirtualHost
        };
    }

    private async Task EnsureTopologyAsync(IChannel channel, CancellationToken cancellationToken)
    {
        await channel.ExchangeDeclareAsync(
            exchange: options.OrderExchange,
            type: ExchangeType.Direct,
            durable: true,
            autoDelete: false,
            arguments: null,
            passive: false,
            cancellationToken: cancellationToken);
        await channel.ExchangeDeclareAsync(
            exchange: options.InventoryExchange,
            type: ExchangeType.Direct,
            durable: true,
            autoDelete: false,
            arguments: null,
            passive: false,
            cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            queue: options.InventoryReserveQueue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            passive: false,
            cancellationToken: cancellationToken);
        await channel.QueueBindAsync(
            queue: options.InventoryReserveQueue,
            exchange: options.OrderExchange,
            routingKey: "order.created",
            arguments: null,
            noWait: false,
            cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            queue: options.OrderResultQueue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            passive: false,
            cancellationToken: cancellationToken);
        await channel.QueueBindAsync(
            queue: options.OrderResultQueue,
            exchange: options.InventoryExchange,
            routingKey: "inventory.reserved",
            arguments: null,
            noWait: false,
            cancellationToken: cancellationToken);
        await channel.QueueBindAsync(
            queue: options.OrderResultQueue,
            exchange: options.InventoryExchange,
            routingKey: "inventory.failed",
            arguments: null,
            noWait: false,
            cancellationToken: cancellationToken);

        logger.LogInformation(
            "Order dispatcher topology ready. OrderExchange={OrderExchange}, InventoryExchange={InventoryExchange}.",
            options.OrderExchange,
            options.InventoryExchange);
    }

    private sealed record PendingOutboxMessage(
        long Id,
        Guid MessageId,
        Guid CorrelationId,
        string EventType,
        string Payload);
}
