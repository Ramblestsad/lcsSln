using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using Todo.Inventory.Worker.Configuration;
using Todo.Inventory.Worker.Data;
using Todo.Inventory.Worker.Services;
using Todo.Observability;

var builder = Host.CreateApplicationBuilder(args);
builder.AddTodoOpenTelemetry(
    "Todo.Inventory.Worker",
    tracing => tracing.AddSource("Todo.Inventory.Worker"),
    metrics => metrics
        .AddRuntimeInstrumentation()
        .AddProcessInstrumentation());

builder.Services.AddDbContext<InventoryDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("postgres")
        ?? throw new InvalidOperationException("ConnectionStrings:postgres is required."),
        postgres => postgres.MigrationsHistoryTable("__EFMigrationsHistory_inventory"));
});

builder.Services
    .AddOptions<RabbitMqOptions>()
    .Bind(builder.Configuration.GetSection(RabbitMqOptions.SectionName))
    .Validate(options => !string.IsNullOrWhiteSpace(options.Host), "RabbitMq:Host is required.")
    .Validate(options => !string.IsNullOrWhiteSpace(options.Username), "RabbitMq:Username is required.")
    .Validate(options => !string.IsNullOrWhiteSpace(options.Password), "RabbitMq:Password is required.")
    .ValidateOnStart();

builder.Services.AddHostedService<InventoryEventConsumerWorker>();
builder.Services.AddHostedService<InventoryOutboxDispatcherWorker>();

var host = builder.Build();
// ponytail: startup migration is for the local learning cluster; use a migration Job before multi-replica deployment.
await using (var scope = host.Services.CreateAsyncScope())
{
    await scope.ServiceProvider.GetRequiredService<InventoryDbContext>().Database.MigrateAsync();
}
await host.RunAsync();
