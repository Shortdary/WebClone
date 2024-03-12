using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Models;
using WebApplication1.Models.Common;
using WebApplication1.Models.Service;

namespace WebApplication1.Controllers
{
    public class BoardController : Controller
    {
        private readonly PostService _postService = new();
        private readonly int _defaultPageNumber = 1;
        private readonly int _defaultPageSize = 2;

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
        public IActionResult Best(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParameter = new()
            {
                BoardId = 24,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            BoardInfoWithPostList boardWithPosts = _postService.GetPostsByBoadId(serviceParameter);
            return View("Index", boardWithPosts);
        }

        [HttpGet]
        [Route("stream_free")]
        public IActionResult StreamFree(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParameter = new()
            {
                BoardId = 2,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            BoardInfoWithPostList boardWithPosts = _postService.GetPostsByBoadId(serviceParameter);
            return View("Index", boardWithPosts);
        }

        [HttpGet]
        [Route("stream_meme")]
        public IActionResult StreamMeme(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParameter = new()
            {
                BoardId = 3,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            BoardInfoWithPostList boardWithPosts = _postService.GetPostsByBoadId(serviceParameter);
            return View("Index", boardWithPosts);
        }
    }
}
