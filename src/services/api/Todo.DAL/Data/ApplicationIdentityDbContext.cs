using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Todo.DAL.Todos;

namespace Todo.DAL.Data;

public class ApplicationIdentityDbContext: IdentityDbContext<IdentityUser>
{
    protected ApplicationIdentityDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TodoItem> TodoItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<TodoItem>().ToTable("todoitems");
    }
}

public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<ApplicationIdentityDbContext>
{
    public ApplicationIdentityDbContext CreateDbContext(string[] args)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var webApiPath = Path.GetFullPath(Path.Combine(currentDirectory, "..", "Todo.WebApi"));
        if (!Directory.Exists(webApiPath))
        {
            webApiPath = Path.GetFullPath(Path.Combine(currentDirectory, "src", "Todo.WebApi"));
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(webApiPath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var builder = new DbContextOptionsBuilder<ApplicationIdentityDbContext>();
        var connStr = configuration.GetConnectionString("postgresWrite");
        if (string.IsNullOrWhiteSpace(connStr))
        {
            throw new InvalidOperationException(
                "Connection string 'ConnectionStrings:postgresWrite' is missing for design-time DbContext.");
        }

        builder.UseNpgsql(
            connStr,
            postgres => postgres.MigrationsHistoryTable("__EFMigrationsHistory_webapi"));
        return new ApplicationIdentityDbContext(builder.Options);
    }
}
