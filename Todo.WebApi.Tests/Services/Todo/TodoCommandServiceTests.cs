using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Todo.DAL.Context;
using Todo.DAL.Entity;
using Todo.WebApi.Services.Todo;
using Xunit;

namespace Todo.WebApi.Tests.Services.Todo;

public sealed class TodoCommandServiceTests
{
    [Fact]
    public async Task CreateTodoItemAsync_Should_Persist_To_WriteContext()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var databaseId = Guid.NewGuid().ToString("N");
        await using var writeContext = CreateWriteContext(databaseId);
        var service = new TodoCommandService(writeContext, NullLogger<TodoCommandService>.Instance);

        var created = await service.CreateTodoItemAsync(
            new TodoItem { Id = 21, Name = "created", Completed = false },
            cancellationToken);

        created.Id.Should().Be(21);
        ( await writeContext.TodoItems.SingleAsync(e => e.Id == 21, cancellationToken) ).Name.Should().Be("created");
    }

    [Fact]
    public async Task UpdateTodoItemAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var databaseId = Guid.NewGuid().ToString("N");
        await using var writeContext = CreateWriteContext(databaseId);
        var service = new TodoCommandService(writeContext, NullLogger<TodoCommandService>.Instance);

        var result = await service.UpdateTodoItemAsync(
            999,
            new TodoItem { Id = 999, Name = "missing", Completed = false },
            cancellationToken);

        result.Should().Be(TodoCommandStatus.NotFound);
    }

    [Fact]
    public async Task DeleteTodoItemAsync_Should_Remove_Item_From_WriteContext()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var databaseId = Guid.NewGuid().ToString("N");
        await using var writeContext = CreateWriteContext(databaseId);
        writeContext.TodoItems.Add(new TodoItem { Id = 31, Name = "to-delete", Completed = true });
        await writeContext.SaveChangesAsync(cancellationToken);

        var service = new TodoCommandService(writeContext, NullLogger<TodoCommandService>.Instance);

        var result = await service.DeleteTodoItemAsync(31, cancellationToken);

        result.Should().Be(TodoCommandStatus.Success);
        ( await writeContext.TodoItems.AnyAsync(e => e.Id == 31, cancellationToken) ).Should().BeFalse();
    }

    private static ApplicationIdentityDbContext CreateWriteContext(string databaseId)
    {
        var options = new DbContextOptionsBuilder<ApplicationIdentityDbContext>()
            .UseInMemoryDatabase($"write-{databaseId}")
            .Options;

        return new ApplicationIdentityDbContext(options);
    }
}
