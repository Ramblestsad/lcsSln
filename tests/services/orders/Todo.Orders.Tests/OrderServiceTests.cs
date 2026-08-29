using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Todo.Orders.Contracts;
using Todo.Orders.Data;
using Xunit;

namespace Todo.Orders.Tests;

public sealed class OrderServiceTests
{
    [Fact]
    public async Task CreateAsync_Persists_Order_And_Outbox_In_One_Database()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync(cancellationToken);
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseSqlite(connection)
            .Options;
        await using var dbContext = new OrderDbContext(options);
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        var service = new OrderService(dbContext, NullLogger<OrderService>.Instance);

        var result = await service.CreateAsync(
            new CreateOrderRequest { Sku = "SKU-1", Quantity = 2 },
            cancellationToken);

        (await dbContext.Orders.SingleAsync(cancellationToken)).Id.Should().Be(result.OrderId);
        (await dbContext.OrderOutboxMessages.SingleAsync(cancellationToken)).CorrelationId.Should().Be(result.OrderId);
    }
}
