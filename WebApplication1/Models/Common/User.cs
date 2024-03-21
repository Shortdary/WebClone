using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class User
{
    public int UserId { get; set; }
    public string? Nickname { get; set; }

}

public class UserLoginCredentials
{
    public string? LoginId { get; set; }

    public string? Password { get; set; }

    public string? RedirectUrl { get; set; }
}
