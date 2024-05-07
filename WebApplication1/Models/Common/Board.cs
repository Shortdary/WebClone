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
    [Required(ErrorMessage = "게시판 이름은 필수입니다.")]
    public string BoardName { get; set; } = null!;

    public string? BoardNameEng { get; set; }
    
    public string? Description { get; set; }

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

    public int TotalRowNum { get; set; }

    public PaginationModel Pagination { get; set; } = new();

    public SearchModel Search { get; set; } = new();

    public BoardControllerCommonParameter? Parameters { get; set; } = new();
}

public class BoardControllerCommonParameter : CommonQueryFilter
{
    public int Id { get; set; }

    public int BoardId { get; set; }
}

