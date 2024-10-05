namespace SportAndStepsApps.Models;

public class UserActivity
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public int Distance { get; set; }

    // Navigation properties:
    public User User { get; set; } = null;
    public SportType SportType { get; set; } = null;
    public int UserId { get; set; }
    public int SportTypeId { get; set; }
}
