namespace SportAndStepsApps.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IUserActivityRepository UserActivityRepository { get; }
    ISportTypeRepository SportRepository { get; }

    Task<bool> CompleteAsync();
    bool HasChanges();
}
