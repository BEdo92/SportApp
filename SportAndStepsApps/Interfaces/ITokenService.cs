using SportAndStepsApps.Models;

namespace SportAndStepsApps.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
