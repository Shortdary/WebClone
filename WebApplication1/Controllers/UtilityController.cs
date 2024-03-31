using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UtilityController: Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetPagination(PaginationModel pm)
        {
            return await Task.Run(() => PartialView("~/Views/Utility/_Pagination.cshtml", pm));
        }

        [HttpGet]
        public async Task<IActionResult> GetSearch(SearchModel sm)
        {
            return await Task.Run(() => PartialView("~/Views/Utility/_Search.cshtml", sm));
        }
    }
    
}
