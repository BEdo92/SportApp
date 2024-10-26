using SportAndStepsApps.Data;
using SportAndStepsApps.Interfaces;

namespace SportAndStepsApps.Repositories;

public class UnitOfWork(SportsContext context, IUserRepository userRepository,
    IUserActivityRepository userActivityRepository, ISportRepository sportRepository) : IUnitOfWork
{
    public IUserRepository UserRepository => userRepository;

    public IUserActivityRepository UserActivityRepository => userActivityRepository;

    public ISportRepository SportRepository => sportRepository;

    public async Task<bool> CompleteAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
}
