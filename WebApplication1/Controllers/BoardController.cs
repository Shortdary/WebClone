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

        private async Task<IActionResult> BoardCommonMethod(BoardServiceCommonParameter serviceParameter)
        {
            if (serviceParameter.Id is not null)
            {
                HttpRequest? request = _httpContextAccessor.HttpContext?.Request;
                ViewBag.RequestPath = request?.Path.ToString();
                PostDetailWithUser? postDetail = _postService.GetPostDetail(serviceParameter.Id);
                return await Task.Run(() => View("Detail", postDetail));
            }
            else
            {
                BoardInfoWithPostList boardWithPosts = _postService.GetPostListByBoadId(serviceParameter);
                return await Task.Run(() => View("Index", boardWithPosts));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Best(BoardControllerCommonParameter controllerParameter)
        {
            
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 24,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize,
                Id = controllerParameter.Id
            };
            return await Task.Run(() => BoardCommonMethod(serviceParams));
        }

        [HttpGet]
        public async Task<IActionResult> New(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 25,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize,
                Id = controllerParameter.Id
            };
            return await Task.Run(() => BoardCommonMethod(serviceParams));
        }

        [HttpGet]
        public async Task<IActionResult> Notice(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 1,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize,
                Id = controllerParameter.Id
            };
            return await Task.Run(() => BoardCommonMethod(serviceParams));
        }

        [HttpGet]
        public async Task<IActionResult> StreamFree(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 2,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize,
                Id = controllerParameter.Id
            };
            return await Task.Run(() => BoardCommonMethod(serviceParams));
        }

        [HttpGet]
        public async Task<IActionResult> StreamMeme(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParams = new()
            {
                BoardId = 3,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize,
                Id = controllerParameter.Id
            };
            return await Task.Run(() => BoardCommonMethod(serviceParams));
        }
    }
}
