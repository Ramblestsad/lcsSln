using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Todo.WebApi.Configuration;
using Todo.WebApi.Services.Engagement;

namespace Todo.WebApi.Extensions;

public static class RedisServiceCollectionExtensions
{
    public static IServiceCollection AddTodoRedis(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<RedisOptions>()
            .Bind(configuration.GetSection(RedisOptions.SectionName))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.ConnectionString),
                "Redis:ConnectionString is required.")
            .ValidateOnStart();

        services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
        {
            var redisOptions = serviceProvider
                .GetRequiredService<IOptions<RedisOptions>>()
                .Value;
            return ConnectionMultiplexer.Connect(redisOptions.ConnectionString);
        });

        services.AddScoped<ITodoEngagementRedisService, TodoEngagementRedisService>();

        return services;
    }
}
