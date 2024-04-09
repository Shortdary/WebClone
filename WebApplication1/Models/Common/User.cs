using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class ApplicationUser
{
    public int Id { get; set; }

    public string LoginId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Nickname { get; set; } = null!;
}


public class User
{
    public int UserId { get; set; }

    public string Password { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public string Roles { get; set; } = null!;
}

public class UserAddParamter
{
    [Required(ErrorMessage = "아이디를 입력해주세요")]
    public string LoginId { get; set; } = null!;

    [Required(ErrorMessage = "비밀번호를 입력해주세요")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "닉네임을 입력해주세요")]
    public string Nickname { get; set; } = null!;

    public string Role { get; set; } = null!; 
    
    public bool IsEmpty()
    {
        return string.IsNullOrEmpty(LoginId) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Nickname);
    }
}

public class UserSuspendParameter
{
    [Required]
    public int UserId { get; set; }

    public string SuspensionTime { get; set; } = null!;
}

public class UserLoginCredentials
{
    [Required]
    public string? LoginId { get; set; }

    [Required]
    public string Password { get; set; } = null!;

    public string? ReturnUrl { get; set; } = null;
}

public partial class UserLoginResponseModel
{
    public User? User1 { get; set; }

    public string ErrorMessage { get; set; } = null!;
}

public class UserForAdmin
{
    public int UserId { get; set; }

    public string Nickname { get; set; } = null!;

    public DateTime? SuspensionTime { get; set; }
}

public class AdminUserListQuery : CommonQueryFilter
{ 
    public int Id { get; set; }
}

public class AdminUserListModel
{
    public List<UserForAdmin> UserList { get; set; } = new();

    public PaginationModel Pagination { get; set; } = new();

    public SearchModel Search { get; set; } = new();
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