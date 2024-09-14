using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Data;
using SportAndStepsApps.DTOs;
using SportAndStepsApps.Models;
using System.Security.Cryptography;
using System.Text;

namespace SportAndStepsApps.Controllers;

public class AccountController(SportsContext context) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterAsync(RegisterDto registerDto)
    {
        if (await UserExistsAsync(registerDto.Username))
        {
            return BadRequest("Username is taken");
        }

        using var hmac = new HMACSHA512();

        var user = new User
        {
            UserName = registerDto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> LoginAsync(LoginDto loginDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(x =>
        x.UserName == loginDto.Username.ToLower());

        if (user is null)
        {
            return Unauthorized("Invalid username");
        }

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Invalid password");
            }
        }

        return user;
    }

    private async Task<bool> UserExistsAsync(string username)
    {
        return await context.Users.AnyAsync(x => 
            x.UserName.ToLower() == username.ToLower());
        // NOTE: For EF, operator '==' should be used instead of 'Equals'.
    }
}
