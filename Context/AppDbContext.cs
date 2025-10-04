using Entities.User;
using Microsoft.EntityFrameworkCore;

namespace FastEndpointsBasicAuthEf.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<User> User { get; set; }
}
