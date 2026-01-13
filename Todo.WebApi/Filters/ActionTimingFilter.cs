using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Todo.WebApi.Filters;

public sealed class ActionTimingFilter: IAsyncActionFilter
{
    private readonly ILogger<ActionTimingFilter> _logger;

    public ActionTimingFilter(ILogger<ActionTimingFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();
        var executedContext = await next();
        stopwatch.Stop();

        var actionName = executedContext.ActionDescriptor.DisplayName ?? "UnknownAction";
        _logger.LogInformation(
            "Action {Action} completed in {ElapsedMs}ms",
            actionName,
            stopwatch.Elapsed.TotalMilliseconds);
    }
}
