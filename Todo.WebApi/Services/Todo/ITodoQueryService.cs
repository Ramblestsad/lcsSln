using Todo.DAL.Entity;
using Todo.WebApi.Filters;

namespace Todo.WebApi.Services.Todo;

public interface ITodoQueryService
{
    Task<TodoQueryResult> GetTodoItemsAsync(
        PaginationFilter paginationFilter,
        CancellationToken cancellationToken = default);

    Task<TodoItem?> GetTodoItemAsync(long id, CancellationToken cancellationToken = default);
}
