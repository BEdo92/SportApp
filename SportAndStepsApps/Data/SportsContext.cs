using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Data;

public class SportsContext(DbContextOptions<SportsContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserActivity> UserActivities { get; set; }
    public DbSet<SportType> SportTypes { get; set; }
}