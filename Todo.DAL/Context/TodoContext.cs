using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Todo.DAL.Entity;

namespace Todo.DAL.Context;

public class ApplicationIdentityDbContext: IdentityDbContext<IdentityUser>
{
    protected ApplicationIdentityDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TodoItem> TodoItems { get; set; } = null!;
    public virtual DbSet<Order> Orders { get; set; } = null!;
    public virtual DbSet<InventoryStock> InventoryStocks { get; set; } = null!;
    public virtual DbSet<OrderOutboxMessage> OrderOutboxMessages { get; set; } = null!;
    public virtual DbSet<OrderInboxMessage> OrderInboxMessages { get; set; } = null!;
    public virtual DbSet<InventoryOutboxMessage> InventoryOutboxMessages { get; set; } = null!;
    public virtual DbSet<InventoryInboxMessage> InventoryInboxMessages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<TodoItem>().ToTable("todoitems");
        builder.Entity<Order>().ToTable("orders");
        builder.Entity<InventoryStock>().ToTable("inventory_stocks");
        builder.Entity<OrderOutboxMessage>().ToTable("order_outbox_messages");
        builder.Entity<OrderInboxMessage>().ToTable("order_inbox_messages");
        builder.Entity<InventoryOutboxMessage>().ToTable("inventory_outbox_messages");
        builder.Entity<InventoryInboxMessage>().ToTable("inventory_inbox_messages");

        builder.Entity<Order>()
            .HasIndex(x => x.CreatedAtUtc);

        builder.Entity<OrderOutboxMessage>()
            .HasIndex(x => x.PublishedOnUtc);
        builder.Entity<OrderOutboxMessage>()
            .HasIndex(x => x.MessageId)
            .IsUnique();

        builder.Entity<InventoryOutboxMessage>()
            .HasIndex(x => x.PublishedOnUtc);
        builder.Entity<InventoryOutboxMessage>()
            .HasIndex(x => x.MessageId)
            .IsUnique();

        builder.Entity<OrderInboxMessage>()
            .HasIndex(x => new { x.MessageId, x.Consumer })
            .IsUnique();

        builder.Entity<InventoryInboxMessage>()
            .HasIndex(x => new { x.MessageId, x.Consumer })
            .IsUnique();
    }
}

public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<ApplicationIdentityDbContext>
{
    public ApplicationIdentityDbContext CreateDbContext(string[] args)
    {
        var webApiPath = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), "..", "Todo.WebApi"));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(webApiPath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var builder = new DbContextOptionsBuilder<ApplicationIdentityDbContext>();
        var connStr = configuration.GetConnectionString("postgresWrite");
        if (string.IsNullOrWhiteSpace(connStr))
        {
            throw new InvalidOperationException(
                "Connection string 'ConnectionStrings:postgresWrite' is missing for design-time DbContext.");
        }

        builder.UseNpgsql(connStr);
        return new ApplicationIdentityDbContext(builder.Options);
    }
}
