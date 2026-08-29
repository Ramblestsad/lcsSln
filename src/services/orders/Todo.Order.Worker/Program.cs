using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using Todo.Observability;
using Todo.Orders.Data;
using Todo.Order.Worker.Configuration;
using Todo.Order.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.AddTodoOpenTelemetry(
    "Todo.Order.Worker",
    tracing => tracing.AddSource("Todo.Order.Worker"),
    metrics => metrics
        .AddRuntimeInstrumentation()
        .AddProcessInstrumentation());

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("postgres")
        ?? throw new InvalidOperationException("ConnectionStrings:postgres is required."),
        postgres => postgres.MigrationsHistoryTable("__EFMigrationsHistory_orders"));
});

builder.Services
    .AddOptions<RabbitMqOptions>()
    .Bind(builder.Configuration.GetSection(RabbitMqOptions.SectionName))
    .Validate(options => !string.IsNullOrWhiteSpace(options.Host), "RabbitMq:Host is required.")
    .Validate(options => !string.IsNullOrWhiteSpace(options.Username), "RabbitMq:Username is required.")
    .Validate(options => !string.IsNullOrWhiteSpace(options.Password), "RabbitMq:Password is required.")
    .ValidateOnStart();

builder.Services.AddHostedService<OrderOutboxDispatcherWorker>();
builder.Services.AddHostedService<InventoryResultConsumerWorker>();

var host = builder.Build();
// ponytail: startup migration is for the local learning cluster; use a migration Job before multi-replica deployment.
await using (var scope = host.Services.CreateAsyncScope())
{
    await scope.ServiceProvider.GetRequiredService<OrderDbContext>().Database.MigrateAsync();
}
await host.RunAsync();
