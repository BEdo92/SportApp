using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportAndStepsApps.Interfaces;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Controllers;

[Authorize]
public class UserActivitiesController(IUserActivityRepository userActivityRepository) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserActivity>>> GetUserActivitiesAsync()
    {
        var userActivities = await userActivityRepository.GetUserActivitiesAsync();

        return Ok(userActivities);
    }

    //[HttpGet]
    //public async Task<ActionResult<UserActivity>> GetUserActivityByIdAsync(int sportId)
    //{
    //    var userActivity = await userActivityRepository.GetUserActivityByIdAsync(sportId);

    //    if (userActivity is null)
    //    {
    //        return NotFound();
    //    }

    //    return Ok(userActivity);
    //}

    [HttpGet("{username}")]
    public async Task<ActionResult<IEnumerable<UserActivity>>> GetUserActivitiesByUsernameAsync(string username)
    {
        var userActivities = await userActivityRepository.GetUserActivitiesByUserNameAsync(username);

        return Ok(userActivities);
    }

    [HttpPost]
    public async Task<ActionResult<UserActivity>> AddUserActivityAsync(UserActivity userActivity)
    {
        await userActivityRepository.AddUserActivityAsync(userActivity);

        if (await userActivityRepository.SaveAllAsync())
        {
            return Ok(userActivity);
        }

        return BadRequest("Failed to add user activity");
    }
}
