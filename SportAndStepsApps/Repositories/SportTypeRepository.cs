using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Data;
using SportAndStepsApps.Interfaces;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Repositories;

public class SportTypeRepository(SportsContext context) : ISportTypeRepository
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

    public async Task UpdateAsync(List<string> sportTypes)
    {
        var existingSportTypes = await context.SportTypes.Select(c => c.Name).ToListAsync();

        context.SportTypes.RemoveRange(context.SportTypes.Where(c => !sportTypes.Contains(c.Name)));
        context.SportTypes.AddRange(sportTypes.Where(c => !existingSportTypes.Contains(c)).Select(c => new SportType { Name = c }));
    }
}
