using SportAndStepsApps.DTOs;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Interfaces;

public interface IUserActivityRepository
{
    Task<IEnumerable<UserActivity>> GetUserActivitiesAsync();
    Task<UserActivity?> GetUserActivityByIdAsync(int sportId);
    Task<IEnumerable<SportSummaryDto>> GetSportSummaryByUserNameAsync(string username);
    Task AddUserActivityAsync(UserActivity userActivity);
    Task<SportSummaryDto?> GetSummarizedDistanceBySportTypeAsync(string sportType);
}
