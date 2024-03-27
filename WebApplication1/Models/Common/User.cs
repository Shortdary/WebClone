using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models;

public class User: IdentityUser
{
    public int UserId { get; set; }
    public string? Nickname { get; set; }

}

public class UserLoginCredentials
{
    public string? LoginId { get; set; }

    public string? Password { get; set; }

    public string? ReturnUrl { get; set; } = null;
}

public class Role: IdentityRole
{
    public int RoleId { get; set; }
}