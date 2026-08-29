using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Todo.Inventory.Worker.Data;

public sealed class InventoryDbContext(DbContextOptions<InventoryDbContext> options): DbContext(options)
{
    public DbSet<InventoryStock> InventoryStocks => Set<InventoryStock>();
    public DbSet<InventoryOutboxMessage> InventoryOutboxMessages => Set<InventoryOutboxMessage>();
    public DbSet<InventoryInboxMessage> InventoryInboxMessages => Set<InventoryInboxMessage>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<InventoryStock>().ToTable("inventory_stocks");
        builder.Entity<InventoryOutboxMessage>().ToTable("inventory_outbox_messages");
        builder.Entity<InventoryOutboxMessage>().HasIndex(x => x.MessageId).IsUnique();
        builder.Entity<InventoryOutboxMessage>().HasIndex(x => new { x.PublishedOnUtc, x.LockedUntilUtc, x.Id });
        builder.Entity<InventoryInboxMessage>().ToTable("inventory_inbox_messages");
        builder.Entity<InventoryInboxMessage>().HasIndex(x => new { x.MessageId, x.Consumer }).IsUnique();
    }
}

public sealed class InventoryStock
{
    [Key, MaxLength(128)]
    public string Sku { get; set; } = null!;
    public int AvailableQuantity { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}

public sealed class InventoryOutboxMessage
{
    [Key]
    public long Id { get; set; }
    public Guid MessageId { get; set; }
    public Guid CorrelationId { get; set; }
    [Required, MaxLength(128)] public string EventType { get; set; } = null!;
    [Required] public string Payload { get; set; } = null!;
    public DateTimeOffset OccurredOnUtc { get; set; }
    public DateTimeOffset? PublishedOnUtc { get; set; }
    public DateTimeOffset? LockedUntilUtc { get; set; }
    public int RetryCount { get; set; }
    [MaxLength(1024)] public string? LastError { get; set; }
}

public sealed class InventoryInboxMessage
{
    [Key]
    public long Id { get; set; }
    public Guid MessageId { get; set; }
    [Required, MaxLength(128)] public string Consumer { get; set; } = null!;
    public DateTimeOffset ProcessedAtUtc { get; set; }
}

public sealed class InventoryDbContextFactory: IDesignTimeDbContextFactory<InventoryDbContext>
{
    public InventoryDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<InventoryDbContext>()
            .UseNpgsql(
                "Host=localhost;Database=axes;Username=postgres;Password=postgres",
                postgres => postgres.MigrationsHistoryTable("__EFMigrationsHistory_inventory"))
            .Options;
        return new InventoryDbContext(options);
    }
}
