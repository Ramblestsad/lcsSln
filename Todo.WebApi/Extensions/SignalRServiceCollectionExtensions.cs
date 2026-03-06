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

        var redisOptions = configuration
            .GetSection(RedisOptions.SectionName)
            .Get<RedisOptions>()
            ?? new RedisOptions();
        if (string.IsNullOrWhiteSpace(redisOptions.ConnectionString))
        {
            throw new InvalidOperationException(
                "SignalR Redis backplane is enabled, but Redis connection string is missing.");
        }

        signalRBuilder.AddStackExchangeRedis(redisOptions.ConnectionString);
        return services;
    }
}
