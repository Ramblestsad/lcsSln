using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using Todo.DAL.Data;
using Todo.Inventory.Worker.Configuration;
using Todo.Inventory.Worker.Services;
using Todo.Observability;

var builder = Host.CreateApplicationBuilder(args);
builder.AddTodoOpenTelemetry(
    "Todo.Inventory.Worker",
    tracing => tracing.AddSource("Todo.Inventory.Worker"),
    metrics => metrics
        .AddRuntimeInstrumentation()
        .AddProcessInstrumentation());

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("postgres")
        ?? throw new InvalidOperationException("ConnectionStrings:postgres is required."));
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
await host.RunAsync();
