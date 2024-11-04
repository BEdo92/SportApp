using SportAndStepsApps.DTOs;
using SportAndStepsApps.Helpers;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Interfaces;

public interface IUserActivityRepository
{
    Task<IEnumerable<UserActivity>> GetUserActivitiesAsync();
    Task<UserActivity?> GetUserActivityByIdAsync(int sportId);
    Task<PagedList<SportDto?>> GetUserActivitiesByUserIdAsync(string userId, SportParams sportParams);
    Task<IEnumerable<SportSummaryDto>> GetSportSummaryByUserNameAsync(string username);
    Task AddUserActivityAsync(UserActivity userActivity);
    Task<SportSummaryDto?> GetSummarizedDistanceBySportTypeAsync(string sportType);
    Task<int> GetLongestDistanceBySportTypeAsync(string sportType);
}
