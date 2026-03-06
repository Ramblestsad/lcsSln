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

        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>()
                          ?? throw new InvalidOperationException("Jwt settings are missing.");
        if (string.IsNullOrWhiteSpace(jwtSettings.Key))
        {
            throw new InvalidOperationException("Jwt:Key is required.");
        }

        if (string.IsNullOrWhiteSpace(jwtSettings.Issuer))
        {
            throw new InvalidOperationException("Jwt:Issuer is required.");
        }

        if (string.IsNullOrWhiteSpace(jwtSettings.Audience))
        {
            throw new InvalidOperationException("Jwt:Audience is required.");
        }

        var encryptedKey = Encoding.ASCII.GetBytes(jwtSettings.Key);

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
