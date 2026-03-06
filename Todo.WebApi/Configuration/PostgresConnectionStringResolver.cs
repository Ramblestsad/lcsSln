namespace Todo.WebApi.Configuration;

public sealed record PostgresConnectionStrings(
    string WriteConnectionString,
    string ReadConnectionString);

public static class PostgresConnectionStringResolver
{
    public static PostgresConnectionStrings Resolve(IConfiguration configuration)
    {
        var writeConnectionString = configuration.GetConnectionString("postgresWrite");
        if (string.IsNullOrWhiteSpace(writeConnectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'ConnectionStrings:postgresWrite' is required.");
        }

        var readConnectionString = configuration.GetConnectionString("postgresRead");
        if (string.IsNullOrWhiteSpace(readConnectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'ConnectionStrings:postgresRead' is required.");
        }

        return new PostgresConnectionStrings(writeConnectionString, readConnectionString);
    }
}
