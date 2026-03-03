using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.DAL.Context;
using Todo.WebApi.Services.Redis;

namespace Todo.WebApi.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/todo-engagement")]
public class TodoEngagementController: ControllerBase
{
    private readonly ITodoEngagementRedisService todoEngagementRedisService;
    private readonly ApplicationIdentityDbContext dbContext;

    public TodoEngagementController(
        ITodoEngagementRedisService todoEngagementRedisService,
        ApplicationIdentityDbContext dbContext)
    {
        this.todoEngagementRedisService = todoEngagementRedisService;
        this.dbContext = dbContext;
    }

    [HttpPost("hot/{todoId:long}/view")]
    public async Task<IActionResult> AddTodoHotScore([FromRoute] long todoId)
    {
        var exists = await dbContext.TodoItems.AnyAsync(e => e.Id == todoId);
        if (!exists)
        {
            return NotFound(new { Message = "Todo not found.", TodoId = todoId });
        }

        var score = await todoEngagementRedisService.IncrementTodoHotScoreAsync(
            todoId,
            1,
            TimeSpan.FromDays(7)
        );
        return Ok(new { TodoId = todoId, Score = score });
    }

    [HttpGet("hot/top")]
    public async Task<IActionResult> GetHotTodos([FromQuery, Range(1, 50)] int topN = 10)
    {
        var scores = await todoEngagementRedisService.GetTopHotTodoScoresAsync(topN);
        var todoIds = scores.Select(s => s.TodoId).Distinct().ToList();

        var todoMap = await dbContext.TodoItems
            .Where(x => todoIds.Contains(x.Id))
            .Select(x => new { x.Id, x.Name, x.Completed })
            .ToDictionaryAsync(x => x.Id);

        var items = scores
            .Select(score => new
            {
                score.TodoId,
                score.Score,
                Todo = todoMap.TryGetValue(score.TodoId, out var todo) ? todo : null
            })
            .ToList();

        return Ok(new { TopN = topN, Items = items });
    }

    [HttpPost("checkin/claim")]
    public async Task<IActionResult> ClaimCheckIn([FromBody] CheckInClaimRequest request)
    {
        var date = request.Date ?? DateOnly.FromDateTime(DateTime.UtcNow);
        var result = await todoEngagementRedisService.ClaimDailyCheckInAsync(
            request.UserKey,
            date,
            request.RewardPoints);

        return Ok(new
        {
            result.Claimed,
            result.TotalPoints,
            result.CheckInDate,
            Message = result.Claimed ? "签到成功，已发放积分。" : "今天已经签到，未重复发放积分。"
        });
    }

    [HttpGet("checkin/summary")]
    public async Task<IActionResult> GetCheckInSummary(
        [FromQuery] string userKey,
        [FromQuery, Range(2000, 2100)] int year,
        [FromQuery, Range(1, 12)] int month)
    {
        if (string.IsNullOrWhiteSpace(userKey))
        {
            return BadRequest(new { Message = "userKey is required." });
        }

        var summary = await todoEngagementRedisService.GetMonthlyCheckInSummaryAsync(
            userKey,
            year,
            month
        );
        return Ok(summary);
    }

    [HttpPost("apply/{todoId:long}/stock")]
    public async Task<IActionResult> SetApplyStock([FromRoute] long todoId, [FromBody] SetApplyStockRequest request)
    {
        var exists = await dbContext.TodoItems.AnyAsync(e => e.Id == todoId);
        if (!exists)
        {
            return NotFound(new { Message = "Todo not found.", TodoId = todoId });
        }

        var stock = await todoEngagementRedisService.SetTodoApplyStockAsync(todoId, request.Stock);
        return Ok(new { TodoId = todoId, Stock = stock });
    }

    [HttpPost("apply/{todoId:long}/claim")]
    public async Task<IActionResult> ApplyTodoSlot([FromRoute] long todoId, [FromBody] ApplyTodoSlotRequest request)
    {
        var exists = await dbContext.TodoItems.AnyAsync(e => e.Id == todoId);
        if (!exists)
        {
            return NotFound(new { Message = "Todo not found.", TodoId = todoId });
        }

        var result = await todoEngagementRedisService.TryApplyTodoSlotAsync(todoId, request.UserKey);
        if (result.Applied)
        {
            return Ok(new { result.Applied, result.Duplicate, result.RemainingStock, Message = "申请成功。" });
        }

        if (result.Duplicate)
        {
            return Conflict(new { result.Applied, result.Duplicate, result.RemainingStock, Message = "请勿重复申请。" });
        }

        return BadRequest(new { result.Applied, result.Duplicate, result.RemainingStock, Message = "名额已满或未初始化库存。" });
    }
}

public record CheckInClaimRequest(
    [property: Required]
    [property: StringLength(128, MinimumLength = 1)]
    string UserKey,
    [property: Range(1, 1000)] int RewardPoints = 5,
    DateOnly? Date = null);

public record SetApplyStockRequest(
    [property: Range(0, 1_000_000)] int Stock);

public record ApplyTodoSlotRequest(
    [property: Required]
    [property: StringLength(128, MinimumLength = 1)]
    string UserKey);
