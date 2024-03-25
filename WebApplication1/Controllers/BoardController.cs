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

        private IActionResult BoardCommonMethod(BoardServiceCommonParameter serviceParameter)
        {
            if (serviceParameter.Id is not null)
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                ViewBag.RequestPath = request?.Path.ToString();
                PostDetailWithUser? postDetail = _postService.GetPostDetail(serviceParameter.Id);
                return View("Detail", postDetail);
            }
            else
            {
                BoardInfoWithPostList boardWithPosts = _postService.GetPostListByBoadId(serviceParameter);
                return View("Index", boardWithPosts);
            }
        }

        [HttpGet]
        public IActionResult Best(BoardControllerCommonParameter controllerParameter)
        {
            
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 24,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize,
                Id = controllerParameter.Id
            };
            return BoardCommonMethod(serviceParams);
        }

        [HttpGet]
        public IActionResult New(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 25,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize,
                Id = controllerParameter.Id
            };
            return BoardCommonMethod(serviceParams);
        }

        [HttpGet]
        public IActionResult Notice(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 1,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize,
                Id = controllerParameter.Id
            };
            return BoardCommonMethod(serviceParams);
        }

        [HttpGet]
        public IActionResult StreamFree(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 2,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize,
                Id = controllerParameter.Id
            };
            return BoardCommonMethod(serviceParams);
        }

        [HttpGet]
        public IActionResult StreamMeme(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 3,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize,
                Id = controllerParameter.Id
            };
            return BoardCommonMethod(serviceParams);
        }
    }
}
