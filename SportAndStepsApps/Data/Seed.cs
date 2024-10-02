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
}
