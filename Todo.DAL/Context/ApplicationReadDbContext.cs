using Microsoft.EntityFrameworkCore;

namespace Todo.DAL.Context;

public sealed class ApplicationReadDbContext(DbContextOptions<ApplicationReadDbContext> options)
    : ApplicationIdentityDbContext(options);
