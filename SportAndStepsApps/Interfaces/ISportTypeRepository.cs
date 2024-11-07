namespace SportAndStepsApps.Interfaces;

public interface ISportTypeRepository
{
    Task<int> GetSportIdAsync(string sportType);

    Task<IEnumerable<string>> GetSportTypesAsync();

    Task UpdateAsync(List<string> sportTypes);
}
