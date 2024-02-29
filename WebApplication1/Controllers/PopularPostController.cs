using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using WebApplication1.Models;
using WebApplication1.Models.Service;

namespace MvcMovie.Controllers
{
    public class PopularPostController : Controller
    {
        PostService _postService = new PostService();

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
            PostDetailWithUser postDetail = _postService.GetPostDetail(postId);
            return View(postDetail);
        }
    }
}