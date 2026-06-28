using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using Todo.DAL.Data;
using Todo.Observability;
using Todo.Order.Worker.Configuration;
using Todo.Order.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.AddTodoOpenTelemetry(
    "Todo.Order.Worker",
    tracing => tracing.AddSource("Todo.Order.Worker"),
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

builder.Services.AddHostedService<OrderOutboxDispatcherWorker>();
builder.Services.AddHostedService<InventoryResultConsumerWorker>();

var host = builder.Build();
await host.RunAsync();
