using Todo.DAL.Todos;
using Todo.WebApi.Todos.Contracts;

namespace Todo.WebApi.Todos;

public interface ITodoQueryService
{
    Task<TodoQueryResult> GetTodoItemsAsync(
        PaginationFilter paginationFilter,
        CancellationToken cancellationToken = default);

    Task<TodoItem?> GetTodoItemAsync(long id, CancellationToken cancellationToken = default);
}
