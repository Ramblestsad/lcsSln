using System.ComponentModel.DataAnnotations;

namespace Todo.DAL.Entity;

public enum OrderStatus
{
    Pending = 0,
    Reserved = 1,
    Cancelled = 2
}

public class Order
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Sku { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    public bool SimulateInventoryFailure { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [MaxLength(512)]
    public string? FailureReason { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset UpdatedAtUtc { get; set; }
}
