using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Common;
using WebApplication1.Models.Service;

namespace WebApplication1.Controllers
{
    public class BoardController : Controller
    {
        private readonly PostService _postService = new();

        public IActionResult Index(BoardInfoWithPostList model)
        {
            return View(model);
        }

        // GET: /{boardName}/{postId}
        [HttpGet("{boardName}/{postId}")]
        public IActionResult Detail(string boardName, int postId)
        {
            PostWithUser postDetail = _postService.GetPostDetail(postId);
            return View(postDetail);
        }

        [HttpGet("best")]
        public IActionResult Best()
        {
            BoardInfoWithPostList boardWithPosts = _postService.GetPostsByBoadId(24);
            return View("Index", boardWithPosts);
        }

        [HttpGet("stream_free")]
        public IActionResult StreamFree()
        {
            BoardInfoWithPostList boardWithPosts = _postService.GetPostsByBoadId(2);
            return View("Index", boardWithPosts);
        }

        [HttpGet("stream_meme")]
        public IActionResult StreamMeme()
        {
            BoardInfoWithPostList boardWithPosts = _postService.GetPostsByBoadId(3);
            return View("Index", boardWithPosts);
        }
    }
}
