using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Data;
using SportAndStepsApps.Interfaces;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Repositories;

public class UserActivityRepository(SportsContext context) : IUserActivityRepository
{
    public async Task AddUserActivityAsync(UserActivity userActivity)
    {
        await context.UserActivities.AddAsync(userActivity);
    }

    public async Task<IEnumerable<UserActivity>> GetUserActivitiesAsync()
    {
        return await context.UserActivities.ToListAsync();
    }

    public async Task<IEnumerable<UserActivity>> GetUserActivitiesByUserNameAsync(string username)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null)
        {
            // NOTE: Handle the case when the user does not exist.
            return [];
        }

        return await context.UserActivities.Where(x => x.UserId == user.Id).ToListAsync();
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
