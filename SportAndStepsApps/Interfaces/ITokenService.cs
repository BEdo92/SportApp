using SportAndStepsApps.Models;

namespace SportAndStepsApps.Interfaces;

public interface ITokenService
{
    Task<string> CreateTokenAsync(User user);
}
