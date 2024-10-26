using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Models;
using System.Text.Json;

namespace SportAndStepsApps.Data;

public class Seed
{
    public static async Task SeedSportTypes(SportsContext context)
    {
        // NOTE: To prevent duplicate data.
        if (await context.SportTypes.AnyAsync())
        {
            return;
        }

        var sportTypes = await File.ReadAllTextAsync("Data/SportSeedData.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var sportTypesData = JsonSerializer.Deserialize<List<SportType>>(sportTypes, options);

        if (sportTypesData == null)
        {
            return;
        }

        await context.SportTypes.AddRangeAsync(sportTypesData);
        await context.SaveChangesAsync();
    }

    public static async Task SeedUsers(UserManager<User> userManager, RoleManager<AppRole> roleManager)
    {
        if (await userManager.Users.AnyAsync())
        {
            return;
        }

        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var users = JsonSerializer.Deserialize<List<User>>(userData, options);

        if (users == null)
        {
            return;
        }

        var roles = new List<AppRole>
        {
            new() { Name = "Member" },
            new() { Name = "Admin" }
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        foreach (var user in users)
        {
            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Member");
        }

        var admin = new User
        {
            UserName = "admin",
            Email = "admin@admin",
            Location = ""
        };

        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRolesAsync(admin, ["Admin"]);
    }
}
