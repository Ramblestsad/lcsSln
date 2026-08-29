using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todo.Contracts;
using Todo.Orders.Contracts;
using Todo.Orders.Data;
using Todo.Orders.Domain;
using Todo.Orders.Messaging;

namespace Todo.Orders;

public sealed class OrderService(OrderDbContext dbContext, ILogger<OrderService> logger)
{
    public async Task<CreateOrderResponse> CreateAsync(
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

        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            dbContext.Orders.Add(order);
            dbContext.OrderOutboxMessages.Add(new OrderOutboxMessage
            {
                MessageId = createdEvent.MessageId,
                CorrelationId = order.Id,
                EventType = MessagingEventTypes.OrderCreated,
                Payload = JsonSerializer.Serialize(createdEvent),
                OccurredOnUtc = now
            });
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            logger.LogError(exception, "Failed to create order {OrderId}.", order.Id);
            throw;
        }

        return new CreateOrderResponse(order.Id, order.Status.ToString(), order.CreatedAtUtc);
    }

    public async Task<OrderDetailsResponse?> GetAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
            .AsNoTracking()
            .Where(candidate => candidate.Id == orderId)
            .SingleOrDefaultAsync(cancellationToken);
        return order is null
            ? null
            : new OrderDetailsResponse(
                order.Id,
                order.Sku,
                order.Quantity,
                order.Status.ToString(),
                order.FailureReason,
                order.CreatedAtUtc,
                order.UpdatedAtUtc);
    }
}
