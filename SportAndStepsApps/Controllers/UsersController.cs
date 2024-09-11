using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Data;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Controllers;

[ApiController]
public class UsersController(SportsContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
    {
        List<User> users = await context.Users.ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserAsync(int id)
    {
        var users = await context.Users.FindAsync(id);

        if (users is null)
        {
            return NotFound();
        }

        return Ok(users);
    }
}
