﻿using Microsoft.AspNetCore.Identity;

namespace SportAndStepsApps.Models;

public class User : IdentityUser<int>
{
    public string Location { get; set; } = string.Empty;
    public ICollection<UserRole> UserRoles { get; set; } = [];
}
