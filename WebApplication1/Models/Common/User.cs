using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class ApplicationUser
{
    public int Id { get; set; }

    public string LoginId { get; set; } = null!;

    public string? Password { get; set; }

    public string Nickname { get; set; } = null!;
}


public class User
{
    public int UserId { get; set; }
    public string Nickname { get; set; } = null!;

}

public class UserLoginCredentials
{
    [Required(AllowEmptyStrings = false)]
    public string? LoginId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string? Password { get; set; }

    public string? ReturnUrl { get; set; } = null;
}

public class UserForAdmin
{
    public int UserId { get; set; }
    public string Nickname { get; set; } = null!;
}

public class AdminUserListQuery
{
    [Display(Name = "page_number")]
    [FromQuery(Name = "page_number")]
    public int PageNumber { get; set; } = 1;

    [Display(Name = "page_size")]
    [FromQuery(Name = "page_size")]
    public int PageSize { get; set; } = 2;

    [Display(Name = "search_target")]
    [FromQuery(Name = "search_target")]
    public string SearchTarget { get; set; } = null!;

    [Display(Name = "search_keyword")]
    [FromQuery(Name = "search_keyword")]

    public string SearchKeyword { get; set; } = null!;

    public int Id { get; set; }
}

public class AdminUserListModel : AdminUserListQuery
{
    public int TotalRowNum { get; set; }

    public List<UserForAdmin> UserList { get; set; } = new();
}

public class AdminUserDetailQuery
{
    [Display(Name = "page_number")]
    [FromQuery(Name = "page_number")]
    public int PageNumber { get; set; } = 1;

    [Display(Name = "page_size")]
    [FromQuery(Name = "page_size")]
    public int PageSize { get; set; } = 5;

    [Display(Name = "search_target")]
    [FromQuery(Name = "search_target")]
    public string SearchTarget { get; set; } = null!;

    [Display(Name = "search_keyword")]
    [FromQuery(Name = "search_keyword")]
    public string SearchKeyword { get; set; } = null!;

    public string DetailType { get; set; } = null!;

    public int Id { get; set; }
}

public class AdminUserDetailModel : AdminUserDetailQuery
{
    public int TotalRowNum { get; set; }

    public List<PostDetailWithUser> PostList { get; set; } = new();

    public List<Comment> CommentList { get; set; } = new();
}