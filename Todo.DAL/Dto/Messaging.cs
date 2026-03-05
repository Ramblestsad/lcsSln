namespace Todo.DAL.Dto;

public static class MessagingEventTypes
{
    public const string OrderCreated = "order.created";
    public const string InventoryReserved = "inventory.reserved";
    public const string InventoryFailed = "inventory.failed";
}

public sealed class OrderCreatedEvent
{
    public Guid MessageId { get; set; }
    public Guid OrderId { get; set; }
    public string Sku { get; set; } = null!;
    public int Quantity { get; set; }
    public bool SimulateInventoryFailure { get; set; }
    public DateTimeOffset OccurredOnUtc { get; set; }
}

public sealed class InventoryResultEvent
{
    public Guid MessageId { get; set; }
    public Guid OrderId { get; set; }
    public string EventType { get; set; } = null!;
    public string? FailureReason { get; set; }
    public DateTimeOffset OccurredOnUtc { get; set; }
}
