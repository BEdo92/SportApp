using Microsoft.AspNetCore.Mvc;
using SportAndStepsApps.Interfaces;

namespace SportAndStepsApps.Controllers;

public class SportTypesController(IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetSportTypesAsync()
    {
        var sportTypes = await unitOfWork.SportRepository.GetSportTypesAsync(); ;

        return Ok(sportTypes);
    }
}
