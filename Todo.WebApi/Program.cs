using Mapster;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using Todo.DAL.Context;
using Todo.WebApi.Extensions;
using Todo.WebApi.Filters;
using Todo.WebApi.Helper;
using Todo.WebApi.Hubs;
using Todo.WebApi.Middleware;
using Todo.WebApi.Services.Engagement;
using Todo.WebApi.Services.Realtime;

var builder = WebApplication.CreateBuilder(args);

builder.AddTodoObservability();

#region Services

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("postgres") ??
        throw new Exception("No connection string!"));
});
builder.Services.AddSingleton(MappingConfig.Config);
builder.Services.AddMapster();
builder.Services.AddTodoRedis(builder.Configuration);
builder.Services.AddTodoRequestContext();
builder.Services.AddTodoDefaultCors();
builder.Services.AddTodoAuth(builder.Configuration);
builder.Services.AddTodoSignalR(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddGrpc();
builder.Services.AddOpenApi();

// middlewares/filters
builder.Services.AddScoped<ITodoEngagementRedisService, TodoEngagementRedisService>();
builder.Services.AddScoped<IChatRoomRedisService, ChatRoomRedisService>();

// services
builder.Services.AddTransient<RequestTimingMiddleware>();
builder.Services.AddScoped<ActionTimingFilter>();

#endregion

#region App

var app = builder.Build();

Log.Information("Serilog initialized");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var logger = context.RequestServices
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger("GlobalExceptionHandler");
            if (exceptionHandlerPathFeature?.Error is not null)
            {
                logger.LogError(
                    exceptionHandlerPathFeature.Error,
                    "Unhandled exception. RequestPath: {RequestPath}",
                    exceptionHandlerPathFeature.Path);
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { code = 500, msg = "Internal Server Error" });
        });
    });
    app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseRouting();
app.UseMiddleware<RequestTimingMiddleware>();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

// global 404/405 but bypass grpc
app.Use(async (ctx, next) =>
{
    await next();

    if (ctx.Response.HasStarted)
    {
        return;
    }

    var isGrpc = ( ctx.Request.ContentType ?? string.Empty )
        .StartsWith("application/grpc", StringComparison.OrdinalIgnoreCase);

    if (isGrpc)
        return;

    if (ctx.Response.StatusCode == StatusCodes.Status404NotFound)
    {
        ctx.Response.ContentType = "application/json";
        await ctx.Response.WriteAsJsonAsync(new { code = 1004, msg = "Nothing flourished here" });
    }
    else if (ctx.Response.StatusCode == StatusCodes.Status405MethodNotAllowed)
    {
        ctx.Response.ContentType = "application/json";
        await ctx.Response.WriteAsJsonAsync(new { code = 1005, msg = "Method Not Allowed" });
    }
});

app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat").RequireAuthorization();
app.MapGrpcServices();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/apidocs", options =>
    {
        options
            .WithTitle("Todo.WebApi")
            .HideModels();
    });
}

#endregion

await app.RunAsync();
