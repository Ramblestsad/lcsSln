using System.Security.Claims;
using System.Text;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using Todo.DAL.Context;
using Todo.WebApi.Configuration;
using Todo.WebApi.Filters;
using Todo.WebApi.Helper;
using Todo.WebApi.Middleware;
using Todo.WebApi.Models;
using Todo.WebApi.Response.Pagination;
using static System.Net.Mime.MediaTypeNames;

#region Builder

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services);
});

#endregion

#region Services

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("postgres") ??
        throw new Exception("No connection string!"));
});

builder.Services.AddSingleton(MappingConfig.Config);
builder.Services.AddMapster();

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.SignIn
            .RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUriService>(provider =>
{
    var accessor = provider.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext?.Request;
    var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
    return new UriService(uri);
});

builder.Services.AddScoped<CurrentUser>(sp =>
{
    var accessor = sp.GetRequiredService<IHttpContextAccessor>();
    var ctx = accessor.HttpContext;

    var cu = new CurrentUser();
    var user = ctx?.User;

    if (user?.Identity?.IsAuthenticated == true)
    {
        cu.IsAuthenticated = true;
        cu.Subject = user.FindFirst("sub")?.Value
                     ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        cu.Name = user.Identity?.Name;
        cu.Roles = user.FindAll(ClaimTypes.Role)
            .Select(c => c.Value).Distinct().ToArray();
    }

    return cu;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddGrpc();
builder.Services.AddTransient<RequestTimingMiddleware>();
builder.Services.AddScoped<ActionTimingFilter>();

var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtSettings>(jwtSection);
var jwtSettings = jwtSection.Get<JwtSettings>()!;
var encryptedKey = Encoding.ASCII.GetBytes(jwtSettings.Key!);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(encryptedKey)
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddOpenApi();

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
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;
            context.Response.ContentType = Text.Plain;
            await context.Response.WriteAsync(
                "An exception was thrown.");

            var exceptionHandlerPathFeature =
                context.Features
                    .Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is
                FileNotFoundException)
            {
                await context.Response.WriteAsync(
                    " The file was not found.");
            }

            if (exceptionHandlerPathFeature?.Path == "/")
            {
                await context.Response.WriteAsync(" Page: Home.");
            }
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
