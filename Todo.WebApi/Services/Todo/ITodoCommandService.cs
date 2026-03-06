using Todo.DAL.Entity;

namespace Todo.WebApi.Services.Todo;

public interface ITodoCommandService
{
    Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken = default);

    Task<TodoCommandStatus> UpdateTodoItemAsync(
        long id,
        TodoItem todoItem,
        CancellationToken cancellationToken = default);

    Task<TodoCommandStatus> DeleteTodoItemAsync(long id, CancellationToken cancellationToken = default);
}
