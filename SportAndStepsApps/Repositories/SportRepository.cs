using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Data;
using SportAndStepsApps.Interfaces;

namespace SportAndStepsApps.Repositories;

public class SportRepository(SportsContext context) : ISportRepository
{
    public async Task<int> GetSportIdAsync(string sportType)
    {
        var sportTypeObj = await context.SportTypes.FirstOrDefaultAsync(c => c.Name == sportType);
        return sportTypeObj!.Id;
    }

    public async Task<IEnumerable<string>> GetSportTypesAsync()
    {
        return await context.SportTypes.Select(c => c.Name).OrderBy(c => c).ToListAsync();
    }
}
