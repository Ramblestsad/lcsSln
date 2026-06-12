using System.Diagnostics;

namespace Todo.WebApi.Todos;

public sealed class TodoEndpointTimingFilter(ILogger<TodoEndpointTimingFilter> logger): IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();
        var result = await next(context);
        stopwatch.Stop();

        var endpointName = context.HttpContext.GetEndpoint()?.DisplayName ?? "UnknownEndpoint";
        logger.LogInformation(
            "Action {Action} completed in {ElapsedMs}ms",
            endpointName,
            stopwatch.Elapsed.TotalMilliseconds);

        return result;
    }
}
