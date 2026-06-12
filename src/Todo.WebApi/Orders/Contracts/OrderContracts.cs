using System.ComponentModel.DataAnnotations;

namespace Todo.WebApi.Orders.Contracts;

public sealed class CreateOrderRequest
{
    [Required]
    [MaxLength(128)]
    public string Sku { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    public bool SimulateInventoryFailure { get; set; }
}

public sealed record CreateOrderResponse(Guid OrderId, string Status, DateTimeOffset CreatedAtUtc);
