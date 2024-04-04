using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SharedController: Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetPagination(PaginationModel pm)
        {
            return await Task.Run(() => PartialView("~/Views/Shared/_Pagination.cshtml", pm));
        }

        [HttpGet]
        public async Task<IActionResult> GetSearch(SearchModel sm)
        {
            return await Task.Run(() => PartialView("~/Views/Shared/_Search.cshtml", sm));
        }
    }
    
}
