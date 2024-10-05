namespace SportAndStepsApps.Models;

public class User
{
    public int Id { get; set; }

    public required string UserName { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public required byte[] PasswordHash { get; set; }

    public required byte[] PasswordSalt { get; set; }
}
