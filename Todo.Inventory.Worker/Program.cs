using Microsoft.EntityFrameworkCore;
using Todo.DAL.Context;
using Todo.Inventory.Worker.Configuration;
using Todo.Inventory.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);

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
    .ValidateOnStart();

builder.Services.AddHostedService<InventoryEventConsumerWorker>();
builder.Services.AddHostedService<InventoryOutboxDispatcherWorker>();

var host = builder.Build();
await host.RunAsync();
