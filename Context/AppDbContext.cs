using Entities.User;
using Microsoft.EntityFrameworkCore;

namespace FastEndpointsBasicAuthEf.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> User { get; set; }
    
    //TODO fix this, it doesn't works at all
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // => optionsBuilder
    // .UseSeeding((context, _) =>
    // {
    //     var user = context.Set<User>().FirstOrDefault(b => b.UserName == "Guppy");
    //     if (user == null)
    //     {
    //         context.Set<User>().Add(new User
    //         {
    //             Id = Guid.NewGuid(),
    //             UserName = "Guppy",
    //             PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
    //             Roles = "admin"
    //         });
    //     }
    // })
    // .UseAsyncSeeding(async (context, _, cancellationToken) =>
    // {
    //     var user = await context.Set<User>().FirstOrDefaultAsync(b => b.UserName == "Guppy", cancellationToken);
    //     if (user == null)
    //     {
    //         context.Set<User>().Add(new User
    //         {
    //             Id = Guid.NewGuid(),
    //             UserName = "Guppy",
    //             PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
    //             Roles = "admin"
    //         });
    //         await context.SaveChangesAsync(cancellationToken);
    //     }
    // });
}
