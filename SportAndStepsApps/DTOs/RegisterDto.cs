using System.ComponentModel.DataAnnotations;

namespace SportAndStepsApps.DTOs;

public class RegisterDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(15, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;
}