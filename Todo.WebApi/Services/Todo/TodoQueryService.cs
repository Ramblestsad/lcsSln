using Microsoft.EntityFrameworkCore;
using Todo.DAL.Context;
using Todo.DAL.Entity;
using Todo.WebApi.Filters;

namespace Todo.WebApi.Services.Todo;

public sealed record TodoQueryResult(
    IReadOnlyList<TodoItem> Items,
    int TotalRecords);

public sealed class TodoQueryService(
    ApplicationReadDbContext dbContext,
    ILogger<TodoQueryService> logger)
    : ITodoQueryService
{
    public async Task<TodoQueryResult> GetTodoItemsAsync(
        PaginationFilter paginationFilter,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Fetching todo page via read database. {DatabaseRole} {PageNumber} {PageSize}",
            "Read",
            paginationFilter.PageNumber,
            paginationFilter.PageSize);

        var totalRecords = await dbContext.TodoItems.CountAsync(cancellationToken);
        var items = await dbContext.TodoItems
            .OrderBy(entity => entity.Id)
            .Skip(( paginationFilter.PageNumber - 1 ) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync(cancellationToken);

        return new TodoQueryResult(items, totalRecords);
    }

    public async Task<TodoItem?> GetTodoItemAsync(long id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Fetching todo item via read database. {DatabaseRole} {TodoId}",
            "Read",
            id);

        return await dbContext.TodoItems
            .SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }
}
