using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.DAL.Context;
using Todo.DAL.Entity;
using Todo.WebApi.Filters;
using Todo.WebApi.Helper;
using Todo.WebApi.Response.Pagination;

namespace Todo.WebApi.Controllers;

/// <summary>
/// items controller
/// </summary>
[Authorize]
[Route("api/[controller]", Name = "GetTodoItems")]
[Produces("application/json")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly ApplicationIdentityDbContext _db;
    private readonly ILogger<TodoItemsController> _logger;
    private readonly IUriService _uriService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="uriService"></param>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    public TodoItemsController(
        ApplicationIdentityDbContext context,
        IUriService uriService,
        ILogger<TodoItemsController> logger,
        IMapper mapper)
    {
        _db = context;
        this._uriService = uriService;
        this._logger = logger;
        _mapper = mapper;
    }

    // GET: api/TodoItems
    /// <summary>
    /// Get all todos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems(
        [FromQuery] PaginationFilter paginationFilter
    )
    {
        // skip data in advance for performance reasons
        var data = await _db.TodoItems
            .OrderBy(e => e.Id)
            .Skip(( paginationFilter.PageNumber - 1 ) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync();

        // obtain route
        var route = Request.Path.Value;

        return Ok(PaginationHelper.CreatePagedResponse(
                      data, paginationFilter, data.Count, _uriService, Request.Path.ToString()));
    }

    // GET: api/TodoItems/5
    /// <summary>
    /// Get a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
    {
        var todoItem = await _db.TodoItems.FindAsync(id);

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
    public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
    {
        if (id != todoItem.Id)
        {
            return BadRequest();
        }

        _db.Entry(todoItem).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoItemExists(id))
            {
                return NotFound();
            }

            throw;
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
    public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
    {
        _db.TodoItems.Add(todoItem);
        await _db.SaveChangesAsync();

        // return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        return CreatedAtAction(
            nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
    }

    /// <summary>
    /// Deletes a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        var todoItem = await _db.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        _db.TodoItems.Remove(todoItem);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoItemExists(long id) =>
        _db.TodoItems.Any(e => e.Id == id);
}
