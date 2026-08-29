using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Todo.Observability;
using Todo.Orders;
using Todo.Orders.Api;
using Todo.Orders.Data;

var builder = WebApplication.CreateBuilder(args);
builder.AddTodoOpenTelemetry(
    "Todo.Orders.Api",
    tracing => tracing.AddAspNetCoreInstrumentation().AddHttpClientInstrumentation(),
    metrics => metrics.AddRuntimeInstrumentation().AddProcessInstrumentation());

var connectionString = builder.Configuration.GetConnectionString("postgres")
    ?? throw new InvalidOperationException("ConnectionStrings:postgres is required.");
builder.Services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(
    connectionString,
    postgres => postgres.MigrationsHistoryTable("__EFMigrationsHistory_orders")));
builder.Services.AddScoped<OrderService>();
builder.Services.AddOpenApi();

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
if (string.IsNullOrWhiteSpace(jwtKey)
    || string.IsNullOrWhiteSpace(jwtIssuer)
    || string.IsNullOrWhiteSpace(jwtAudience))
{
    throw new InvalidOperationException("Jwt:Key, Jwt:Issuer, and Jwt:Audience are required.");
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// ponytail: startup migration is for the local learning cluster; use a migration Job before multi-replica deployment.
await using (var scope = app.Services.CreateAsyncScope())
{
    await scope.ServiceProvider.GetRequiredService<OrderDbContext>().Database.MigrateAsync();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapOrderEndpoints();
app.MapGet("/healthz", () => Results.Ok()).AllowAnonymous();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

await app.RunAsync();
