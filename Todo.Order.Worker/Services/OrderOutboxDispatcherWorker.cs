using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Todo.DAL.Context;
using Todo.Order.Worker.Configuration;

namespace Todo.Order.Worker.Services;

public sealed class OrderOutboxDispatcherWorker : BackgroundService
{
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
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        EnsureTopology(channel);

        while (!stoppingToken.IsCancellationRequested)
        {
            await DispatchPendingMessagesAsync(channel, stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
        }
    }

    private async Task DispatchPendingMessagesAsync(IModel channel, CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();

        var pendingMessages = await dbContext.OrderOutboxMessages
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
                var props = channel.CreateBasicProperties();
                props.MessageId = message.MessageId.ToString();
                props.CorrelationId = message.CorrelationId.ToString();
                props.Persistent = true;
                props.Type = message.EventType;

                channel.BasicPublish(
                    exchange: options.OrderExchange,
                    routingKey: message.EventType,
                    basicProperties: props,
                    body: body);

                message.PublishedOnUtc = DateTimeOffset.UtcNow;
                message.LastError = null;
            }
            catch (Exception ex)
            {
                message.RetryCount += 1;
                message.LastError = ex.Message[..Math.Min(1000, ex.Message.Length)];
                logger.LogError(ex, "Failed to publish order outbox message {MessageId}.", message.MessageId);
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
            VirtualHost = options.VirtualHost,
            DispatchConsumersAsync = true
        };
    }

    private void EnsureTopology(IModel channel)
    {
        channel.ExchangeDeclare(options.OrderExchange, ExchangeType.Direct, durable: true, autoDelete: false);
        channel.ExchangeDeclare(options.InventoryExchange, ExchangeType.Direct, durable: true, autoDelete: false);

        channel.QueueDeclare(options.InventoryReserveQueue, durable: true, exclusive: false, autoDelete: false);
        channel.QueueBind(options.InventoryReserveQueue, options.OrderExchange, routingKey: "order.created");

        channel.QueueDeclare(options.OrderResultQueue, durable: true, exclusive: false, autoDelete: false);
        channel.QueueBind(options.OrderResultQueue, options.InventoryExchange, routingKey: "inventory.reserved");
        channel.QueueBind(options.OrderResultQueue, options.InventoryExchange, routingKey: "inventory.failed");

        logger.LogInformation(
            "Order dispatcher topology ready. OrderExchange={OrderExchange}, InventoryExchange={InventoryExchange}.",
            options.OrderExchange,
            options.InventoryExchange);
    }
}
