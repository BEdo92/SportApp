using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportAndStepsApps.DTOs;
using SportAndStepsApps.Interfaces;
using SportAndStepsApps.Models;
using System.Security.Claims;

namespace SportAndStepsApps.Controllers;

[Authorize]
public class UserActivitiesController(IUserActivityRepository userActivityRepository, ISportRepository sportRepository, IMapper mapper) : BaseApiController
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
    public async Task<ActionResult<UserActivity>> AddUserActivityAsync(SportDto sport)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("No user ID was found in token.");
        }

        //var userActivity = mapper.Map<UserActivity>(sport);
        //userActivity.UserId = int.Parse(userId);

        int sportTypeId = await sportRepository.GetSportIdAsync(sport.SportType);

        var userActivity = new UserActivity
        {
            UserId = int.Parse(userId),
            Date = sport.Date,
            Distance = sport.Distance,
            SportTypeId = sportTypeId
        };

        await userActivityRepository.AddUserActivityAsync(userActivity);

        if (await userActivityRepository.SaveAllAsync())
        {
            return Ok(userActivity);
        }

        return BadRequest("Failed to add user activity");
    }
}
