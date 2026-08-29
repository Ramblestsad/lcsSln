using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Todo.Inventory.Worker.Data;
using Xunit;

namespace Todo.Inventory.Tests;

public sealed class InventoryDbContextTests
{
    [Fact]
    public async Task Model_Contains_Only_Inventory_Owned_Data()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync(cancellationToken);
        var options = new DbContextOptionsBuilder<InventoryDbContext>()
            .UseSqlite(connection)
            .Options;
        await using var dbContext = new InventoryDbContext(options);

        dbContext.Model.GetEntityTypes()
            .Select(entity => entity.ClrType)
            .Should()
            .BeEquivalentTo([
                typeof(InventoryStock),
                typeof(InventoryInboxMessage),
                typeof(InventoryOutboxMessage)
            ]);
    }
}
