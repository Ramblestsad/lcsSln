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
using static System.Net.Mime.MediaTypeNames; // Production Env Exception Content-Type construct


namespace Todo.WebApi;

public class Startup
{
    IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) => Configuration = configuration;

    /// <summary>
    /// 注册容器中的服务（相当于原先的 builder.Services.xxx）
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationIdentityDbContext>(options =>
        {
            options.UseNpgsql(
                Configuration.GetConnectionString("postgres") ??
                throw new Exception("No connection string!"));
        });

        services.AddSingleton(MappingConfig.Config);
        services.AddMapster();

        services
            .AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn
                    .RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddDefaultTokenProviders();
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        });

        // pagination uri generate service
        services.AddHttpContextAccessor();
        services.AddSingleton<IUriService>(provider =>
        {
            var accessor = provider.GetRequiredService<IHttpContextAccessor>();
            var request = accessor.HttpContext?.Request;
            var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
            return new UriService(uri);
        });

        services.AddScoped<CurrentUser>(sp =>
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

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policyBuilder =>
            {
                policyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddControllers();
        services.AddGrpc();
        services.AddTransient<RequestTimingMiddleware>();
        services.AddScoped<ActionTimingFilter>();

        // jwt authentication
        var jwtSection = Configuration.GetSection("Jwt");
        services.Configure<JwtSettings>(jwtSection);
        var jwtSettings = jwtSection.Get<JwtSettings>()!;
        var encryptedKey = Encoding.ASCII.GetBytes(jwtSettings.Key!);

        services
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
        services.AddAuthorization();

        // openapi scalar
        services.AddOpenApi();
    }

    /// <summary>
    /// 配置中间件管道（相当于原先的 app.Build() 后 app.xxx）
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // 生产环境全局异常处理
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
                // 注意：框架可能已经设置了 Allow header，这里不动它
                ctx.Response.ContentType = "application/json";
                await ctx.Response.WriteAsJsonAsync(new { code = 1005, msg = "Method Not Allowed" });
            }
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGrpcServices();
            if (env.IsDevelopment())
            {
                endpoints.MapOpenApi();
                endpoints.MapScalarApiReference("/apidocs", options =>
                {
                    options
                        .WithTitle("Todo.WebApi")
                        .HideModels();
                });
            }
        });
    }
}
