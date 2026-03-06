using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Todo.DAL.Context;
using Todo.DAL.Entity;
using Todo.WebApi.Filters;
using Todo.WebApi.Services.Todo;
using Xunit;

namespace Todo.WebApi.Tests.Services.Todo;

public sealed class TodoQueryServiceTests
{
    [Fact]
    public async Task GetTodoItemsAsync_Should_Read_From_ReadContext_Only()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var databaseId = Guid.NewGuid().ToString("N");
        await using var writeContext = CreateWriteContext(databaseId);
        await using var readContext = CreateReadContext(databaseId);

        writeContext.TodoItems.Add(new TodoItem { Id = 1, Name = "write-only", Completed = false });
        await writeContext.SaveChangesAsync(cancellationToken);

        readContext.TodoItems.Add(new TodoItem { Id = 2, Name = "read-only", Completed = true });
        readContext.TodoItems.Add(new TodoItem { Id = 3, Name = "read-second", Completed = false });
        await readContext.SaveChangesAsync(cancellationToken);

        var service = new TodoQueryService(readContext, NullLogger<TodoQueryService>.Instance);

        var result = await service.GetTodoItemsAsync(new PaginationFilter(1, 10), cancellationToken);

        result.TotalRecords.Should().Be(2);
        result.Items.Select(item => item.Name).Should().Equal("read-only", "read-second");
    }

    [Fact]
    public async Task GetTodoItemAsync_Should_Return_Null_When_Item_IsMissing_From_ReadContext()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var databaseId = Guid.NewGuid().ToString("N");
        await using var writeContext = CreateWriteContext(databaseId);
        await using var readContext = CreateReadContext(databaseId);

        writeContext.TodoItems.Add(new TodoItem { Id = 10, Name = "write-only", Completed = false });
        await writeContext.SaveChangesAsync(cancellationToken);

        var service = new TodoQueryService(readContext, NullLogger<TodoQueryService>.Instance);

        var todoItem = await service.GetTodoItemAsync(10, cancellationToken);

        todoItem.Should().BeNull();
    }

    private static ApplicationIdentityDbContext CreateWriteContext(string databaseId)
    {
        var options = new DbContextOptionsBuilder<ApplicationIdentityDbContext>()
            .UseInMemoryDatabase($"write-{databaseId}")
            .Options;

        return new ApplicationIdentityDbContext(options);
    }

    private static ApplicationReadDbContext CreateReadContext(string databaseId)
    {
        var options = new DbContextOptionsBuilder<ApplicationReadDbContext>()
            .UseInMemoryDatabase($"read-{databaseId}")
            .Options;

        return new ApplicationReadDbContext(options);
    }
}
