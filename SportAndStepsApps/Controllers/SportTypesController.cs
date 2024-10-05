using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Data;

namespace SportAndStepsApps.Controllers;

public class SportTypesController(SportsContext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetSportTypesAsync()
    {
        var sportTypes = await context.SportTypes.Select(c => c.Name).OrderBy(c => c).ToListAsync();

        return Ok(sportTypes);
    }
}
