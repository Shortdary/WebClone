using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class CommonQueryFilter
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 1;

    public string SearchTarget { get; set; } = null!;

    public string SearchKeyword { get; set; } = null!;
}

public partial class PaginationModel
{
    public string FormId { get; set; } = null!;

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 1;

    public int TotalRowNum { get; set; }
}

public class SelectListItemModel
{
    public string Text { get; set; } = null!;
    public string Value { get; set; } = null!;
}

public partial class SearchModel
{
    public string FormId { get; set; } = null!;

    public string SearchTarget { get; set; } = null!;

    public string SearchKeyword { get; set; } = null!;

    public int TotalRowNum { get; set; }

    public string StringifiedSelectListItemList { get; set; } = null!;
}