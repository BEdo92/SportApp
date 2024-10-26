namespace SportAndStepsApps.Interfaces;

public interface ISportRepository
{
    Task<int> GetSportIdAsync(string sportType);

    Task<IEnumerable<string>> GetSportTypesAsync();
}
