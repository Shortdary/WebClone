using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class CommonQueryFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 1;

    [Display(Name = "search_target")]
    [FromQuery(Name = "search_target")]
    public string SearchTarget { get; set; } = null!;

    [Display(Name = "search_keyword")]
    [FromQuery(Name = "search_keyword")]

    public string SearchKeyword { get; set; } = null!;
}

public partial class PaginationModel
{
    public string FormId { get; set; } = null!;

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 1;

    public int TotalRowNum { get; set; }
}

public partial class SearchModel
{
    public string FormId { get; set; } = null!;

    public string SearchTarget { get; set; } = null!;

    public string SearchKeyword { get; set; } = null!;

    public int TotalRowNum { get; set; }

    public List<string> SelectListItemList { get; set; } = null!;
}