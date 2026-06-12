using Microsoft.AspNetCore.Mvc;
using Todo.DAL.Todos;
using Todo.WebApi.Infrastructure.RequestContext;
using Todo.WebApi.Infrastructure.Validation;
using Todo.WebApi.Todos.Contracts;

namespace Todo.WebApi.Todos;

public static class TodoEndpoints
{
    public static WebApplication MapTodoEndpoints(this WebApplication app)
    {
        app.MapGet("/api/TodoItems", GetTodoItemsAsync)
            .RequireAuthorization()
            .WithTags("TodoItems")
            .AddEndpointFilter<TodoEndpointTimingFilter>()
            .WithName("GetTodoItems")
            .Produces<PagedResponse<List<TodoItem>>>()
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapGet("/api/TodoItems/{id:long}", GetTodoItemAsync)
            .RequireAuthorization()
            .WithTags("TodoItems")
            .WithName("GetTodoItem")
            .Produces<TodoItem>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapPut("/api/TodoItems/{id:long}", PutTodoItemAsync)
            .RequireAuthorization()
            .WithTags("TodoItems")
            .WithName("PutTodoItem")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapPost("/api/TodoItems", PostTodoItemAsync)
            .RequireAuthorization()
            .WithTags("TodoItems")
            .WithName("PostTodoItem")
            .Produces<TodoItem>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapDelete("/api/TodoItems/{id:long}", DeleteTodoItemAsync)
            .RequireAuthorization()
            .WithTags("TodoItems")
            .WithName("DeleteTodoItem")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized);

        return app;
    }

    private static async Task<IResult> GetTodoItemsAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CurrentUser user,
        ITodoQueryService todoQueryService,
        IUriService uriService,
        HttpContext httpContext,
        ILogger<TodoEndpointTimingFilter> logger,
        CancellationToken cancellationToken)
    {
        logger.LogDebug("Fetching todos for user {UserName}", user.Name);
        var validFilter = new PaginationFilter(pageNumber, pageSize);
        var result = await todoQueryService.GetTodoItemsAsync(validFilter, cancellationToken);

        var response = PaginationHelper.CreatePagedResponse(
            result.Items.ToList(),
            validFilter,
            result.TotalRecords,
            uriService,
            httpContext.Request.Path.ToString());

        return Results.Ok(response);
    }

    private static async Task<IResult> GetTodoItemAsync(
        long id,
        ITodoQueryService todoQueryService,
        CancellationToken cancellationToken)
    {
        var todoItem = await todoQueryService.GetTodoItemAsync(id, cancellationToken);
        return todoItem is null ? Results.NotFound() : Results.Ok(todoItem);
    }

    private static async Task<IResult> PutTodoItemAsync(
        long id,
        TodoItem? todoItem,
        ITodoCommandService todoCommandService,
        CancellationToken cancellationToken)
    {
        if (!RequestValidation.TryValidate(todoItem, out var validationErrors))
        {
            return Results.ValidationProblem(validationErrors);
        }

        if (id != todoItem!.Id)
        {
            return Results.BadRequest();
        }

        var result = await todoCommandService.UpdateTodoItemAsync(id, todoItem, cancellationToken);
        return result == TodoCommandStatus.NotFound ? Results.NotFound() : Results.NoContent();
    }

    private static async Task<IResult> PostTodoItemAsync(
        TodoItem? todoItem,
        ITodoCommandService todoCommandService,
        CancellationToken cancellationToken)
    {
        if (!RequestValidation.TryValidate(todoItem, out var validationErrors))
        {
            return Results.ValidationProblem(validationErrors);
        }

        var createdTodoItem = await todoCommandService.CreateTodoItemAsync(todoItem!, cancellationToken);
        return Results.Created($"/api/TodoItems/{createdTodoItem.Id}", createdTodoItem);
    }

    private static async Task<IResult> DeleteTodoItemAsync(
        long id,
        ITodoCommandService todoCommandService,
        CancellationToken cancellationToken)
    {
        var result = await todoCommandService.DeleteTodoItemAsync(id, cancellationToken);
        return result == TodoCommandStatus.NotFound ? Results.NotFound() : Results.NoContent();
    }
}
