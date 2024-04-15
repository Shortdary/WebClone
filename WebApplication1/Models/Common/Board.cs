using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Models;

public partial class Board
{
    public int Id { get; set; }

    public string BoardName { get; set; } = null!;

    public string BoardNameEng { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public int? ParentBoardId { get; set; }

    public int? Priority { get; set; }

    public bool IsDeleted { get; set; }

}

public partial class BoardAdd
{
    public string BoardName { get; set; } = null!;

    public string BoardNameEng { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? ParentBoardId { get; set; }

    public int? Priority { get; set; }
}

public partial class BoardEdit
{
    public int Id { get; set; }

    public string BoardName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? ParentBoardId { get; set; }

    public int? Priority { get; set; }
}

public partial class BoardDelete
{
    public int Id { get; set; }
}

public class BoardInfoWithPostList: Board
{
    public List<PostDetailWithUser> PostList { get; set; } = null!;

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 1;

    [Display(Name = "search_target")]
    [FromQuery(Name = "search_target")]
    public string SearchTarget { get; set; } = null!;

    [Display(Name = "search_keyword")]
    [FromQuery(Name = "search_keyword")]
    public string SearchKeyword { get; set; } = null!;

    public int TotalRowNum { get; set; } = 0;

    public BoardControllerCommonParameter? Parameters { get; set; } = new();
}

public class BoardControllerCommonParameter
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 2;

    public string SearchTarget { get; set; } = null!;

    public string SearchKeyword { get; set; } = null!;

    public int Id { get; set; }

    public int BoardId { get; set; }
}

