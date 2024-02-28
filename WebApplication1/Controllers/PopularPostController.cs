using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using WebApplication1.Models;
using WebApplication1.Models.Service;

namespace MvcMovie.Controllers
{
    public class PopularPostController : Controller
    {
        public PostService postService = new PostService();
        // 
        // GET: /popularpost/

        public IActionResult Index()
        {
            List<Post> popularPosts = postService.GetPopularPosts();
            System.Diagnostics.Debug.WriteLine(popularPosts);
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