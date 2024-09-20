using Microsoft.AspNetCore.Mvc;
using SportAndStepsApps.Data;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Controllers;

public class BuggyController(SportsContext context) : BaseApiController
{
    [HttpGet("auth")]
    public ActionResult<string> GetAuth()
    {
        return "Secret text";
    }

    [HttpGet("not-found")]
    public ActionResult<User> GetNotFound()
    {
        var thing = context.Users.Find(-1);

        if (thing is null)
        {
            return NotFound();
        }

        return thing;
    }

    [HttpGet("server-error")]
    public ActionResult<User> GetServerError()
    {
        var thing = context.Users.Find(-1) ?? throw new NullReferenceException();

        return thing;
    }

    [HttpGet("bad-request")]
    public ActionResult<User> GetBadRequest()
    {
        return BadRequest();
    }
}
