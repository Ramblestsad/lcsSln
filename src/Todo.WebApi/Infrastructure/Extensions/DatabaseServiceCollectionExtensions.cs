using Microsoft.EntityFrameworkCore;
using Todo.DAL.Data;
using Todo.WebApi.Infrastructure.Configuration;

namespace Todo.WebApi.Infrastructure.Extensions;

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
