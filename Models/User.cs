using System;
using System.Collections.Generic;

namespace NZFTC_Portal.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual Admin? Admin { get; set; }

    public virtual Employee? Employee { get; set; }
}
