using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Todo.DAL.Entity;

namespace Todo.DAL.Context;

public class ApplicationIdentityDbContext: IdentityDbContext<IdentityUser> {
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
        : base(options) {
    }

    public virtual DbSet<TodoItem> TodoItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        builder.Entity<TodoItem>().ToTable("todoitems");
    }
}

public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<ApplicationIdentityDbContext> {
    public ApplicationIdentityDbContext CreateDbContext(string[] args) {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(
                @Directory.GetCurrentDirectory() + "/../Todo.WebApi/appsettings.json")
            .Build();
        var builder =
            new DbContextOptionsBuilder<ApplicationIdentityDbContext>();
        var connStr = configuration.GetSection("ConnectionStrings:postgres").Value;
        builder.UseNpgsql(connStr);
        return new ApplicationIdentityDbContext(builder.Options);
    }
}
