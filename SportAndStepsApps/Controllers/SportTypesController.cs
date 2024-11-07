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

    [HttpPut]
    public async Task<ActionResult<IEnumerable<string>>> UpdateSportTypesAsync(List<string> sportTypes)
    {
        await unitOfWork.SportRepository.UpdateAsync(sportTypes);

        if (!await unitOfWork.CompleteAsync())
        {
            return BadRequest("Failed to update sport types.");
        }

        var updatedSportTypes = await unitOfWork.SportRepository.GetSportTypesAsync();

        return Ok(updatedSportTypes);
    }
}
