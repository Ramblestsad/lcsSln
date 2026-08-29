using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Todo.Orders.Domain;
using Todo.Orders.Messaging;

namespace Todo.Orders.Data;

public sealed class OrderDbContext(DbContextOptions<OrderDbContext> options): DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderOutboxMessage> OrderOutboxMessages => Set<OrderOutboxMessage>();
    public DbSet<OrderInboxMessage> OrderInboxMessages => Set<OrderInboxMessage>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Order>().ToTable("orders").HasIndex(x => x.CreatedAtUtc);
        builder.Entity<OrderOutboxMessage>().ToTable("order_outbox_messages");
        builder.Entity<OrderOutboxMessage>().HasIndex(x => x.MessageId).IsUnique();
        builder.Entity<OrderOutboxMessage>().HasIndex(x => new { x.PublishedOnUtc, x.LockedUntilUtc, x.Id });
        builder.Entity<OrderInboxMessage>().ToTable("order_inbox_messages");
        builder.Entity<OrderInboxMessage>().HasIndex(x => new { x.MessageId, x.Consumer }).IsUnique();
    }
}

public sealed class OrderDbContextFactory: IDesignTimeDbContextFactory<OrderDbContext>
{
    public OrderDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseNpgsql(
                "Host=localhost;Database=axes;Username=postgres;Password=postgres",
                postgres => postgres.MigrationsHistoryTable("__EFMigrationsHistory_orders"))
            .Options;
        return new OrderDbContext(options);
    }
}
