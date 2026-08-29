using Microsoft.EntityFrameworkCore;
using Todo.DAL.Data;
using Todo.WebApi.Engagement.Contracts;
using Todo.WebApi.Infrastructure.Validation;

namespace Todo.WebApi.Engagement;

public static class TodoEngagementEndpoints
{
    public static WebApplication MapTodoEngagementEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/todo-engagement")
            .AllowAnonymous()
            .WithTags("TodoEngagement");

        group.MapPost("/hot/{todoId:long}/view", AddTodoHotScoreAsync)
            .WithName("AddTodoHotScore")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/hot/top", GetHotTodosAsync)
            .WithName("GetHotTodos")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem();

        group.MapPost("/checkin/claim", ClaimCheckInAsync)
            .WithName("ClaimCheckIn")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem();

        group.MapGet("/checkin/summary", GetCheckInSummaryAsync)
            .WithName("GetCheckInSummary")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("/apply/{todoId:long}/stock", SetApplyStockAsync)
            .WithName("SetApplyStock")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();

        group.MapPost("/apply/{todoId:long}/claim", ApplyTodoSlotAsync)
            .WithName("ApplyTodoSlot")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesValidationProblem();

        return app;
    }

    private static async Task<IResult> AddTodoHotScoreAsync(
        long todoId,
        ITodoEngagementRedisService todoEngagementRedisService,
        ApplicationIdentityDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var exists = await dbContext.TodoItems.AnyAsync(e => e.Id == todoId, cancellationToken);
        if (!exists)
        {
            return Results.NotFound(new { Message = "Todo not found.", TodoId = todoId });
        }

        var score = await todoEngagementRedisService.IncrementTodoHotScoreAsync(
            todoId,
            1,
            TimeSpan.FromDays(7));
        return Results.Ok(new { TodoId = todoId, Score = score });
    }

    private static async Task<IResult> GetHotTodosAsync(
        int? topN,
        ITodoEngagementRedisService todoEngagementRedisService,
        ApplicationIdentityDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var requestedTopN = topN ?? 10;
        if (requestedTopN is < 1 or > 50)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(topN)] = ["topN must be between 1 and 50."]
            });
        }

        var scores = await todoEngagementRedisService.GetTopHotTodoScoresAsync(requestedTopN);
        var todoIds = scores.Select(s => s.TodoId).Distinct().ToList();

        var todoMap = await dbContext.TodoItems
            .Where(x => todoIds.Contains(x.Id))
            .Select(x => new { x.Id, x.Name, x.Completed })
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        var items = scores
            .Select(score => new
            {
                score.TodoId,
                score.Score,
                Todo = todoMap.TryGetValue(score.TodoId, out var todo) ? todo : null
            })
            .ToList();

        return Results.Ok(new { TopN = requestedTopN, Items = items });
    }

    private static async Task<IResult> ClaimCheckInAsync(
        CheckInClaimRequest? request,
        ITodoEngagementRedisService todoEngagementRedisService)
    {
        if (!RequestValidation.TryValidate(request, out var validationErrors))
        {
            return Results.ValidationProblem(validationErrors);
        }

        var date = request!.Date ?? DateOnly.FromDateTime(DateTime.UtcNow);
        var result = await todoEngagementRedisService.ClaimDailyCheckInAsync(
            request.UserKey,
            date,
            request.RewardPoints);

        return Results.Ok(new
        {
            result.Claimed,
            result.TotalPoints,
            result.CheckInDate,
            Message = result.Claimed ? "签到成功，已发放积分。" : "今天已经签到，未重复发放积分。"
        });
    }

    private static async Task<IResult> GetCheckInSummaryAsync(
        string? userKey,
        int year,
        int month,
        ITodoEngagementRedisService todoEngagementRedisService)
    {
        if (string.IsNullOrWhiteSpace(userKey))
        {
            return Results.BadRequest(new { Message = "userKey is required." });
        }

        if (year is < 2000 or > 2100 || month is < 1 or > 12)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(year)] = ["year must be between 2000 and 2100."],
                [nameof(month)] = ["month must be between 1 and 12."]
            });
        }

        var summary = await todoEngagementRedisService.GetMonthlyCheckInSummaryAsync(
            userKey,
            year,
            month);
        return Results.Ok(summary);
    }

    private static async Task<IResult> SetApplyStockAsync(
        long todoId,
        SetApplyStockRequest? request,
        ITodoEngagementRedisService todoEngagementRedisService,
        ApplicationIdentityDbContext dbContext,
        CancellationToken cancellationToken)
    {
        if (!RequestValidation.TryValidate(request, out var validationErrors))
        {
            return Results.ValidationProblem(validationErrors);
        }

        var exists = await dbContext.TodoItems.AnyAsync(e => e.Id == todoId, cancellationToken);
        if (!exists)
        {
            return Results.NotFound(new { Message = "Todo not found.", TodoId = todoId });
        }

        var stock = await todoEngagementRedisService.SetTodoApplyStockAsync(todoId, request!.Stock);
        return Results.Ok(new { TodoId = todoId, Stock = stock });
    }

    private static async Task<IResult> ApplyTodoSlotAsync(
        long todoId,
        ApplyTodoSlotRequest? request,
        ITodoEngagementRedisService todoEngagementRedisService,
        ApplicationIdentityDbContext dbContext,
        CancellationToken cancellationToken)
    {
        if (!RequestValidation.TryValidate(request, out var validationErrors))
        {
            return Results.ValidationProblem(validationErrors);
        }

        var exists = await dbContext.TodoItems.AnyAsync(e => e.Id == todoId, cancellationToken);
        if (!exists)
        {
            return Results.NotFound(new { Message = "Todo not found.", TodoId = todoId });
        }

        var result = await todoEngagementRedisService.TryApplyTodoSlotAsync(todoId, request!.UserKey);
        if (result.Applied)
        {
            return Results.Ok(new { result.Applied, result.Duplicate, result.RemainingStock, Message = "申请成功。" });
        }

        if (result.Duplicate)
        {
            return Results.Conflict(new { result.Applied, result.Duplicate, result.RemainingStock, Message = "请勿重复申请。" });
        }

        return Results.BadRequest(new { result.Applied, result.Duplicate, result.RemainingStock, Message = "名额已满或未初始化库存。" });
    }
}
