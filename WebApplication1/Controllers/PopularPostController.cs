using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using WebApplication1.Models;
using WebApplication1.Models.Service;

namespace MvcMovie.Controllers
{
    public class PopularPostController : Controller
    {
        PostService _postService = new PostService();
        // 
        // GET: /popularpost/

        public IActionResult Index()
        {
            List<PostWithUser> popularPosts = _postService.GetPopularPosts();
            return View(popularPosts);
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}