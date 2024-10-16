using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Data;
using SportAndStepsApps.DTOs;
using SportAndStepsApps.Interfaces;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Repositories;

public class UserActivityRepository(SportsContext context, IMapper mapper) : IUserActivityRepository
{
    public async Task AddUserActivityAsync(UserActivity userActivity)
    {
        await context.UserActivities.AddAsync(userActivity);
    }

    public async Task<SportSummaryDto?> GetSummarizedDistanceBySportTypeAsync(string sportType)
    {
        var sportSummary = await context.UserActivities
            .Include(x => x.SportType)
            .Where(x => x.SportType.Name == sportType)
            .GroupBy(x => x.SportType)
            .ProjectTo<SportSummaryDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (sportSummary == null)
        {
            return new SportSummaryDto
            {
                SportType = sportType,
                Distance = 0
            };
        }

        return sportSummary;
    }

    public async Task<IEnumerable<UserActivity>> GetUserActivitiesAsync()
    {
        return await context.UserActivities.ToListAsync();
    }

    public async Task<IEnumerable<SportSummaryDto>> GetSportSummaryByUserNameAsync(string username)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null)
        {
            // NOTE: Handle the case when the user does not exist.
            return [];
        }

        return await context.UserActivities
            .Where(x => x.UserId == user.Id)
            .GroupBy(x => x.SportType)
            .ProjectTo<SportSummaryDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<UserActivity?> GetUserActivityByIdAsync(int id)
    {
        return await context.UserActivities.FindAsync(id);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
