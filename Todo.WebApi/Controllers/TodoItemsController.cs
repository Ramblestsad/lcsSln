using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.DAL.Entity;
using Todo.WebApi.Filters;
using Todo.WebApi.Helper;
using Todo.WebApi.Models;
using Todo.WebApi.Response;
using Todo.WebApi.Response.Pagination;
using Todo.WebApi.Services.Todo;

namespace Todo.WebApi.Controllers;

/// <summary>
/// items controller
/// </summary>
[Authorize]
[Route("api/[controller]", Name = "GetTodoItems")]
[Produces("application/json")]
[ApiController]
public class TodoItemsController: ControllerBase
{
    private readonly ITodoCommandService _todoCommandService;
    private readonly ITodoQueryService _todoQueryService;
    private readonly ILogger<TodoItemsController> _logger;
    private readonly IUriService _uriService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="uriService"></param>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    public TodoItemsController(
        ITodoQueryService todoQueryService,
        ITodoCommandService todoCommandService,
        IUriService uriService,
        ILogger<TodoItemsController> logger)
    {
        _todoQueryService = todoQueryService;
        _todoCommandService = todoCommandService;
        _uriService = uriService;
        _logger = logger;
    }

    // GET: api/TodoItems
    /// <summary>
    /// Get all todos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ServiceFilter(typeof(ActionTimingFilter))]
    public async Task<ActionResult<PagedResponse<List<TodoItem>>>> GetTodoItems(
        [FromQuery] PaginationFilter paginationFilter,
        [FromServices] CurrentUser user,
        CancellationToken cancellationToken
    )
    {
        _logger.LogDebug("Fetching todos for user {UserName}", user.Name);
        var validFilter = new PaginationFilter(
            paginationFilter.PageNumber,
            paginationFilter.PageSize);
        var result = await _todoQueryService.GetTodoItemsAsync(validFilter, cancellationToken);

        return Ok(PaginationHelper.CreatePagedResponse(
                      result.Items.ToList(),
                      validFilter,
                      result.TotalRecords,
                      _uriService,
                      Request.Path.ToString()));
    }

    // GET: api/TodoItems/5
    /// <summary>
    /// Get a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodoItem(long id, CancellationToken cancellationToken)
    {
        var todoItem = await _todoQueryService.GetTodoItemAsync(id, cancellationToken);

        if (todoItem == null)
        {
            return NotFound();
        }

        return todoItem;
    }

    // PUT: api/TodoItems/5
    /// <summary>
    /// Updates a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="todoItem"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem, CancellationToken cancellationToken)
    {
        if (id != todoItem.Id)
        {
            return BadRequest();
        }

        var result = await _todoCommandService.UpdateTodoItemAsync(id, todoItem, cancellationToken);
        if (result == TodoCommandStatus.NotFound)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Creates a TodoItem.
    /// </summary>
    /// <param name="todoItem"></param>
    /// <returns>A newly created TodoItem</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/todoItems
    ///     {
    ///        "id": 1,
    ///        "name": "Item #1",
    ///        "isComplete": true
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem, CancellationToken cancellationToken)
    {
        var createdTodoItem = await _todoCommandService.CreateTodoItemAsync(todoItem, cancellationToken);

        // return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        return CreatedAtAction(
            nameof(GetTodoItem), new { id = createdTodoItem.Id }, createdTodoItem);
    }

    /// <summary>
    /// Deletes a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id, CancellationToken cancellationToken)
    {
        var result = await _todoCommandService.DeleteTodoItemAsync(id, cancellationToken);
        if (result == TodoCommandStatus.NotFound)
        {
            return NotFound();
        }

        return NoContent();
    }
}
