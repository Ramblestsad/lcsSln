using Todo.DAL.Todos;

namespace Todo.WebApi.Todos;

public interface ITodoCommandService
{
    Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken = default);

    Task<TodoCommandStatus> UpdateTodoItemAsync(
        long id,
        TodoItem todoItem,
        CancellationToken cancellationToken = default);

    Task<TodoCommandStatus> DeleteTodoItemAsync(long id, CancellationToken cancellationToken = default);
}
