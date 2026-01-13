using System.Diagnostics;

namespace Todo.WebApi.Middleware;

public sealed class RequestTimingMiddleware: IMiddleware
{
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(ILogger<RequestTimingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();
        await next(context);
        stopwatch.Stop();

        _logger.LogInformation(
            "Request {Method} {Path} responded {StatusCode} in {ElapsedMs}ms",
            context.Request.Method,
            context.Request.Path.Value,
            context.Response.StatusCode,
            stopwatch.Elapsed.TotalMilliseconds);
    }
}
