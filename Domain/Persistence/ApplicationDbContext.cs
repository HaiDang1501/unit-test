using Domain.Followers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Domain.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) 
        : base(context)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Follower> Follower { get; set; }
    public DbSet<User> User { get; set; }
}