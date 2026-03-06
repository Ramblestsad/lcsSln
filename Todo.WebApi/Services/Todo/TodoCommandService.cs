using Microsoft.EntityFrameworkCore;
using Todo.DAL.Context;
using Todo.DAL.Entity;

namespace Todo.WebApi.Services.Todo;

public enum TodoCommandStatus
{
    Success = 0,
    NotFound = 1
}

public sealed class TodoCommandService(
    ApplicationIdentityDbContext dbContext,
    ILogger<TodoCommandService> logger)
    : ITodoCommandService
{
    public async Task<TodoItem> CreateTodoItemAsync(
        TodoItem todoItem,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Creating todo item via write database. {DatabaseRole} {TodoId}",
            "Write",
            todoItem.Id);

        dbContext.TodoItems.Add(todoItem);
        await dbContext.SaveChangesAsync(cancellationToken);
        return todoItem;
    }

    public async Task<TodoCommandStatus> UpdateTodoItemAsync(
        long id,
        TodoItem todoItem,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Updating todo item via write database. {DatabaseRole} {TodoId}",
            "Write",
            id);

        dbContext.Entry(todoItem).State = EntityState.Modified;

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return TodoCommandStatus.Success;
        }
        catch (DbUpdateConcurrencyException)
        {
            var exists = await dbContext.TodoItems.AnyAsync(
                entity => entity.Id == id,
                cancellationToken);
            if (!exists)
            {
                logger.LogInformation(
                    "Todo item was not found during write update. {DatabaseRole} {TodoId}",
                    "Write",
                    id);
                return TodoCommandStatus.NotFound;
            }

            throw;
        }
    }

    public async Task<TodoCommandStatus> DeleteTodoItemAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Deleting todo item via write database. {DatabaseRole} {TodoId}",
            "Write",
            id);

        var todoItem = await dbContext.TodoItems.FindAsync([id], cancellationToken);
        if (todoItem == null)
        {
            return TodoCommandStatus.NotFound;
        }

        dbContext.TodoItems.Remove(todoItem);
        await dbContext.SaveChangesAsync(cancellationToken);
        return TodoCommandStatus.Success;
    }
}
