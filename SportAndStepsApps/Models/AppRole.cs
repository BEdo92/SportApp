using Microsoft.AspNetCore.Identity;

namespace SportAndStepsApps.Models;

public class AppRole : IdentityRole<int>
{
    public ICollection<UserRole> UserRoles { get; set; } = [];
}
