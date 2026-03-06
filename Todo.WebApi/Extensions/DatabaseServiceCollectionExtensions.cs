using Microsoft.EntityFrameworkCore;
using Todo.DAL.Context;
using Todo.WebApi.Configuration;

namespace Todo.WebApi.Extensions;

public static class DatabaseServiceCollectionExtensions
{
    public static IServiceCollection AddTodoPostgresDatabases(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionStrings = PostgresConnectionStringResolver.Resolve(configuration);

        services.AddDbContext<ApplicationIdentityDbContext>(options =>
        {
            options.UseNpgsql(connectionStrings.WriteConnectionString);
        });

        services.AddDbContext<ApplicationReadDbContext>(options =>
        {
            options.UseNpgsql(connectionStrings.ReadConnectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        return services;
    }
}
