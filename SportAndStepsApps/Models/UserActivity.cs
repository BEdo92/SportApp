namespace SportAndStepsApps.Models;

public class UserActivity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SportId { get; set; }
    public DateTime Date { get; set; }
    public int Distance { get; set; }
    public int NumOfSteps { get; set; }
}
