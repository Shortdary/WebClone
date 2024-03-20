using Azure.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

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
        public IActionResult Detail(string _, int postId)
        {
            PostDetailWithUser? postDetail = _postService.GetPostDetail(postId);
            if (postDetail is null)
            {
                return View("home");
            }
            else
            {
                return View(postDetail);
            }
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
            BoardInfoWithPostList boardWithPosts = _postService.GetPostListByBoadId(serviceParameter);
            return View("Index", boardWithPosts);
        }

        [HttpGet]
        [Route("notice")]
        public IActionResult Notice(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParameter = new()
            {
                BoardId = 1,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            BoardInfoWithPostList boardWithPosts = _postService.GetPostListByBoadId(serviceParameter);
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
            BoardInfoWithPostList boardWithPosts = _postService.GetPostListByBoadId(serviceParameter);
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
            BoardInfoWithPostList boardWithPosts = _postService.GetPostListByBoadId(serviceParameter);
            return View("Index", boardWithPosts);
        }
    }
}
