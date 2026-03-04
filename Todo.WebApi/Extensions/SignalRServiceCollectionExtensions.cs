using Todo.WebApi.Configuration;

namespace Todo.WebApi.Extensions;

public static class SignalRServiceCollectionExtensions
{
    public static IServiceCollection AddTodoSignalR(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var signalRBuilder = services.AddSignalR();
        var useRedisBackplane = configuration.GetValue<bool?>("SignalR:UseRedisBackplane") ?? true;
        if (!useRedisBackplane)
        {
            return services;
        }

        var redisConnection = configuration
            .GetSection(RedisOptions.SectionName)
            .GetValue<string>(nameof(RedisOptions.ConnectionString));
        if (string.IsNullOrWhiteSpace(redisConnection))
        {
            throw new InvalidOperationException(
                "SignalR Redis backplane is enabled, but Redis connection string is missing.");
        }

        signalRBuilder.AddStackExchangeRedis(redisConnection);
        return services;
    }
}
