using Microsoft.EntityFrameworkCore;

namespace Todo.DAL.Data;

public sealed class ApplicationReadDbContext(DbContextOptions<ApplicationReadDbContext> options)
    : ApplicationIdentityDbContext(options);
