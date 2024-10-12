namespace SportAndStepsApps.DTOs;

public class SportDto
{
    public required int Distance { get; set; }
    public required string SportType { get; set; } = string.Empty;
    public required DateTime Date { get; set; }
}
