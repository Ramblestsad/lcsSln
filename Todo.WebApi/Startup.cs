using System.Text;
using static System.Net.Mime.MediaTypeNames; // Production Env Exception Content-Type construct
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using Mapster;
using Todo.DAL.Context;
using Todo.WebApi.Configuration;
using Todo.WebApi.Helper;
using Todo.WebApi.Response.Pagination;


namespace Todo.WebApi;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) => Configuration = configuration;

    /// <summary>
    /// 注册容器中的服务（相当于原先的 builder.Services.xxx）
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationIdentityDbContext>(opt =>
                                                                opt.UseNpgsql(
                                                                    Configuration
                                                                        .GetConnectionString(
                                                                            "postgres")
                                                                    ?? throw new Exception(
                                                                        "No db connection found in appsettings.json.")
                                                                )
        );

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
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = Text.Plain;
                    await context.Response.WriteAsync("An exception was thrown.");

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                    {
                        await context.Response.WriteAsync(" The file was not found.");
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
        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet(
                "/", async context => { await context.Response.WriteAsync("Temp Index!"); });
            if (env.IsDevelopment())
            {
                endpoints.MapOpenApi();
                endpoints.MapScalarApiReference("/apidocs", options =>
                {
                    options
                        .WithTitle("Todo.WebApi")
                        .WithModels(false);
                });
            }
        });
    }
}
