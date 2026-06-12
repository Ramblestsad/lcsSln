using System.Text.Json;
using Todo.DAL.Data;
using Todo.DAL.Messaging;
using Todo.DAL.Orders;
using Todo.WebApi.Orders.Contracts;

namespace Todo.WebApi.Orders;

public sealed class OrderCommandService(
    ApplicationIdentityDbContext dbContext,
    ILogger<OrderCommandService> logger)
{
    public async Task<CreateOrderResponse> CreateOrderAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.UtcNow;
        var order = new Order
        {
            Id = Guid.NewGuid(),
            Sku = request.Sku.Trim(),
            Quantity = request.Quantity,
            SimulateInventoryFailure = request.SimulateInventoryFailure,
            Status = OrderStatus.Pending,
            CreatedAtUtc = now,
            UpdatedAtUtc = now
        };

        var createdEvent = new OrderCreatedEvent
        {
            MessageId = Guid.NewGuid(),
            OrderId = order.Id,
            Sku = order.Sku,
            Quantity = order.Quantity,
            SimulateInventoryFailure = order.SimulateInventoryFailure,
            OccurredOnUtc = now
        };

        var outboxMessage = new OrderOutboxMessage
        {
            MessageId = createdEvent.MessageId,
            CorrelationId = order.Id,
            EventType = MessagingEventTypes.OrderCreated,
            Payload = JsonSerializer.Serialize(createdEvent),
            OccurredOnUtc = now
        };

        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            dbContext.Orders.Add(order);
            dbContext.OrderOutboxMessages.Add(outboxMessage);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            logger.LogError(ex, "Failed to create order {OrderId}.", order.Id);
            throw;
        }

        return new CreateOrderResponse(order.Id, order.Status.ToString(), order.CreatedAtUtc);
    }
}
