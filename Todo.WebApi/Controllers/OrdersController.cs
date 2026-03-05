using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.DAL.Context;
using Todo.DAL.Dto;
using Todo.DAL.Entity;

namespace Todo.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/orders")]
public sealed class OrdersController: ControllerBase
{
    private readonly ApplicationIdentityDbContext dbContext;
    private readonly ILogger<OrdersController> logger;

    public OrdersController(
        ApplicationIdentityDbContext dbContext,
        ILogger<OrdersController> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

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
            return Problem(
                title: "Failed to create order.",
                detail: "The order could not be persisted.",
                statusCode: StatusCodes.Status500InternalServerError);
        }

        return Ok(new CreateOrderResponse(order.Id, order.Status.ToString(), order.CreatedAtUtc));
    }
}

public sealed class CreateOrderRequest
{
    [Required] [MaxLength(128)] public string Sku { get; set; } = null!;

    [Range(1, int.MaxValue)] public int Quantity { get; set; }

    public bool SimulateInventoryFailure { get; set; }
}

public sealed record CreateOrderResponse(Guid OrderId, string Status, DateTimeOffset CreatedAtUtc);
