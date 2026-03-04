using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Todo.DAL.Context;
using Todo.WebApi.Configuration;

namespace Todo.WebApi.Extensions;

public static class AuthServiceCollectionExtensions
{
    public static IServiceCollection AddTodoAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddIdentity<IdentityUser, IdentityRole>(options =>
                                                         options.SignIn.RequireConfirmedAccount = true)
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

        services
            .AddOptions<JwtSettings>()
            .Bind(configuration.GetSection("Jwt"))
            .Validate(options =>
                          !string.IsNullOrWhiteSpace(options.Key), "Jwt:Key is required.")
            .Validate(options =>
                          !string.IsNullOrWhiteSpace(options.Issuer), "Jwt:Issuer is required.")
            .Validate(options =>
                          !string.IsNullOrWhiteSpace(options.Audience), "Jwt:Audience is required.")
            .ValidateOnStart();

        var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>()
                          ?? throw new InvalidOperationException("Jwt settings are missing.");
        var encryptedKey = Encoding.ASCII.GetBytes(jwtSettings.Key!);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrWhiteSpace(accessToken)
                            && path.StartsWithSegments("/hubs/chat"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization();

        return services;
    }
}
