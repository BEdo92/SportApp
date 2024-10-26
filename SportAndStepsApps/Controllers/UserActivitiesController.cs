using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportAndStepsApps.DTOs;
using SportAndStepsApps.Interfaces;
using SportAndStepsApps.Models;
using System.Security.Claims;

namespace SportAndStepsApps.Controllers;

[Authorize]
public class UserActivitiesController(IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserActivity>>> GetUserActivitiesAsync()
    {
        var userActivities = await unitOfWork.UserActivityRepository.GetUserActivitiesAsync();

        return Ok(userActivities);
    }

    [HttpGet("user/{username}")]
    public async Task<ActionResult<IEnumerable<SportSummaryDto>>> GetSportSummaryByUsernameAsync(string username)
    {
        var userActivities = await unitOfWork.UserActivityRepository.GetSportSummaryByUserNameAsync(username);

        return Ok(userActivities);
    }

    [HttpGet("sport/{sporttype}")]
    public async Task<ActionResult<IEnumerable<SportSummaryDto>>> GetSummarizedDistanceBySportTypeAsync(string sportType)
    {
        var sportSummary = await unitOfWork.UserActivityRepository.GetSummarizedDistanceBySportTypeAsync(sportType);

        if (sportSummary == null)
        {
            return BadRequest($"There is no registered sport activity with sport type {sportType}.");
        }

        return Ok(sportSummary);
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

        int sportTypeId = await unitOfWork.SportRepository.GetSportIdAsync(sport.SportType);

        var userActivity = new UserActivity
        {
            UserId = int.Parse(userId),
            Date = sport.Date,
            Distance = sport.Distance,
            SportTypeId = sportTypeId
        };

        await unitOfWork.UserActivityRepository.AddUserActivityAsync(userActivity);

        if (await unitOfWork.CompleteAsync())
        {
            return Ok(userActivity);
        }

        return BadRequest("Failed to add user activity");
    }
}
