using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportAndStepsApps.DTOs;
using SportAndStepsApps.Interfaces;
using System.Security.Claims;

namespace SportAndStepsApps.Controllers;

[Authorize]
public class UsersController(IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsersAsync()
    {
        var users = await unitOfWork.UserRepository.GetUsersAsync();

        var usersToReturn = mapper.Map<IEnumerable<MemberDto>>(users);

        return Ok(usersToReturn);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUserAsync(string username)
    {
        var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(username);

        if (user is null)
        {
            return NotFound();
        }

        var userToReturn = mapper.Map<MemberDto>(user);

        return Ok(userToReturn);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUserAsync(MemberDto memberDto)
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;

        if (username is null)
        {
            return BadRequest("No username was found in token.");
        }

        var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(username);

        if (user is null)
        {
            return BadRequest("Could not find user.");
        }

        mapper.Map(memberDto, user);

        if (await unitOfWork.CompleteAsync())
        {
            return NoContent();
        }

        return BadRequest("Failed to update the user.");
    }
}
