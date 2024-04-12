using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostService _postService = new();

        public IActionResult Index(BoardControllerCommonParameter controllerParameter)
        {
            BoardControllerCommonParameter serviceParameter = new()
            {
                BoardId = 28,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            BoardInfoWithPostList boardWithPosts = _postService.GetPostListByBoadId(serviceParameter);
            return View(boardWithPosts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
