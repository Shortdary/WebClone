using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Models;

public partial class Board
{
    public int Id { get; set; }

    public string BoardName { get; set; } = null!;

    public string BoardNameEng { get; set; } = null!;

    public string Description { get; set; } = null!;

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
}

public class BoardControllerCommonParameter
{
    [FromQuery(Name ="page_number")]
    public int PageNumber { get; set; } = 1;

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

public class BoardServiceCommonParameter : BoardControllerCommonParameter
{
    public int BoardId { get; set; }
}
