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

        [HttpGet]
        [Route("best")]
        public IActionResult Best(int pageNumber = 1)
        {
            BoardInfoWithPostList boardWithPosts = _postService.GetPostsByBoadId(24, pageNumber);
            return View("Index", boardWithPosts);
        }

        [HttpGet]
        [Route("stream_free")]
        public IActionResult StreamFree(int page_number = 1)
        {
            BoardInfoWithPostList boardWithPosts = _postService.GetPostsByBoadId(2, page_number);
            return View("Index", boardWithPosts);
        }

        [HttpGet]
        [Route("stream_meme")]
        public IActionResult StreamMeme(int pageNumber = 1)
        {
            BoardInfoWithPostList boardWithPosts = _postService.GetPostsByBoadId(3, pageNumber);
            return View("Index", boardWithPosts);
        }
    }
}
