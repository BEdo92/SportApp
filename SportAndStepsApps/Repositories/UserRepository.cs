using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Data;
using SportAndStepsApps.Interfaces;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Repositories;

public class UserRepository(SportsContext context) : IUserRepository
{
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Update(User user)
    {
        // NOTE: This method is not async because it only updates the state of the entity.
        // EF tracks the changes and saves them to the database when SaveChangesAsync is called.
        context.Entry(user).State = EntityState.Modified;
    }
}
