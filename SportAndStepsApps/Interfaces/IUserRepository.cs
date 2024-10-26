using SportAndStepsApps.Models;

namespace SportAndStepsApps.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> GetUsersAsync();
    void Update(User user);
}
