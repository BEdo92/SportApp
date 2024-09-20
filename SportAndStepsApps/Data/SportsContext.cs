using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Data;

public class SportsContext(DbContextOptions<SportsContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserActivity> UserActivities { get; set; }
    public DbSet<SportType> SportTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Populate SportTypes table with initial values
        modelBuilder.Entity<SportType>().HasData(
            new SportType { Id = 1, Name = "Run" },
            new SportType { Id = 2, Name = "Swim" },
            new SportType { Id = 3, Name = "Ride" },
            new SportType { Id = 4, Name = "Walk" },
            new SportType { Id = 5, Name = "Hike" },
            new SportType { Id = 6, Name = "Trail run" }
        );
    }
}