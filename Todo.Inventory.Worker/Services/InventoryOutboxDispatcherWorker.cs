using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Todo.DAL.Context;
using Todo.Inventory.Worker.Configuration;

namespace Todo.Inventory.Worker.Services;

public sealed class InventoryOutboxDispatcherWorker : BackgroundService
{
    private readonly IServiceScopeFactory scopeFactory;
    private readonly ILogger<InventoryOutboxDispatcherWorker> logger;
    private readonly RabbitMqOptions options;

    public InventoryOutboxDispatcherWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<InventoryOutboxDispatcherWorker> logger,
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

        var pendingMessages = await dbContext.InventoryOutboxMessages
            .Where(x => x.PublishedOnUtc == null)
            .OrderBy(x => x.Id)
            .Take(20)
            .ToListAsync(cancellationToken);

        if (pendingMessages.Count == 0)
        {
            return;
        }

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
                    exchange: options.InventoryExchange,
                    routingKey: message.EventType,
                    mandatory: false,
                    basicProperties: props,
                    body: body,
                    cancellationToken: cancellationToken);

                message.PublishedOnUtc = DateTimeOffset.UtcNow;
                message.LastError = null;
            }
            catch (Exception ex)
            {
                message.RetryCount += 1;
                message.LastError = ex.Message[..Math.Min(1000, ex.Message.Length)];
                logger.LogError(ex, "Failed to publish inventory outbox message {MessageId}.", message.MessageId);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
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
            "Inventory dispatcher topology ready. OrderExchange={OrderExchange}, InventoryExchange={InventoryExchange}.",
            options.OrderExchange,
            options.InventoryExchange);
    }
}
