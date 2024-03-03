using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using WebApplication1.Models;
using WebApplication1.Models.Service;

namespace MvcMovie.Controllers
{
    public class PopularPostController : Controller
    {
        private readonly PostService _postService = new ();

        // GET: /best/
        [HttpGet("best")]
        public IActionResult Index()
        {
            List<PostWithUser> popularPosts = _postService.GetPopularPosts();
            return View(popularPosts);
        }


        // GET: /best/{postId}
        [HttpGet("best/{postId}")]
        public IActionResult Detail(int postId)
        {
            PostWithUser postDetail = _postService.GetPostDetail(postId);
            return View(postDetail);
        }
    }
}