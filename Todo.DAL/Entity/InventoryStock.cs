using System.ComponentModel.DataAnnotations;

namespace Todo.DAL.Entity;

public class InventoryStock
{
    [Key]
    [MaxLength(128)]
    public string Sku { get; set; } = null!;

    public int AvailableQuantity { get; set; }

    public DateTimeOffset UpdatedAtUtc { get; set; }
}
