using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Data;
using SportAndStepsApps.DTOs;
using SportAndStepsApps.Helpers;
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

    public async Task<PagedList<SportDto?>> GetUserActivitiesByUserIdAsync(string userId, SportParams sportParams)
    {
        var query = context.UserActivities.Where(x => x.UserId == int.Parse(userId)).ProjectTo<SportDto>(mapper.ConfigurationProvider).AsQueryable();

        if (!string.IsNullOrEmpty(sportParams.SportType) && sportParams.SportType != "all")
        {
            query = query.Where(x => x.SportType == sportParams.SportType);
        }

        if (sportParams.DistanceFrom is not null && sportParams.DistanceTo is not null)
        {
            query = query.Where(x => x.Distance >= sportParams.DistanceFrom && x.Distance <= sportParams.DistanceTo);
        }

        if (sportParams.DateFrom is not null)
        {
            query = query.Where(x => x.Date >= sportParams.DateFrom);
        }

        if (sportParams.DateTo is not null)
        {
            query = query.Where(x => x.Date <= sportParams.DateTo);
        }

        query = sportParams.OrderBy switch
        {
            "date" => query.OrderByDescending(x => x.Date),
            _ => query.OrderByDescending(x => x.Distance)
        };

        return await PagedList<SportDto?>.CreateAsync(query, sportParams.PageNumber, sportParams.PageSize);
    }

    public async Task<UserActivity?> GetUserActivityByIdAsync(int id)
    {
        return await context.UserActivities.FindAsync(id);
    }

    public async Task<int> GetLongestDistanceBySportTypeAsync(string sportType)
    {
        var maxDistance = await context.UserActivities
        .Where(x => x.SportType.Name == sportType)
        .Select(x => (int?)x.Distance)
        .MaxAsync();

        return maxDistance ?? 0;
    }
}
