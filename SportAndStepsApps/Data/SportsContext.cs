using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Data;

public class SportsContext(DbContextOptions<SportsContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Sports> Sports { get; set; }
}