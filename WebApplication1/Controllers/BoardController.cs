using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BoardController : Controller
    {
        private readonly PostService _postService = new();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BoardController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private IActionResult BoardCommonMethod(BoardServiceCommonParameter serviceParameter, int postId)
        {

            if (postId > 0)
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                ViewBag.RequestPath = request.Path.ToString();
                PostDetailWithUser? postDetail = _postService.GetPostDetail(postId);
                return View("Detail", postDetail);
            }
            else
            {
                BoardInfoWithPostList boardWithPosts = _postService.GetPostListByBoadId(serviceParameter);
                return View("Index", boardWithPosts);
            }
        }

        [HttpGet]
        [Route("best/{postId:int?}")]
        public IActionResult Best(BoardControllerCommonParameter controllerParameter, int postId)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 24,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            return BoardCommonMethod(serviceParams, postId);
        }

        [HttpGet]
        [Route("new/{postId:int?}")]
        public IActionResult New(BoardControllerCommonParameter controllerParameter, int postId)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 25,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            return BoardCommonMethod(serviceParams, postId);
        }

        [HttpGet]
        [Route("notice/{postId:int?}")]
        public IActionResult Notice(BoardControllerCommonParameter controllerParameter, int postId)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 1,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            return BoardCommonMethod(serviceParams, postId);
        }

        [HttpGet]
        [Route("stream_free/{postId:int?}")]
        public IActionResult StreamFree(BoardControllerCommonParameter controllerParameter, int postId)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 2,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            return BoardCommonMethod(serviceParams, postId);
        }

        [HttpGet]
        [Route("stream_meme/{postId:int?}")]
        public IActionResult StreamMeme(BoardControllerCommonParameter controllerParameter, int postId)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 3,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            return BoardCommonMethod(serviceParams, postId);
        }
    }
}
