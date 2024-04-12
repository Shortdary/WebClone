using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Text.Json;

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

        [HttpGet]
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> PostList(BoardControllerCommonParameter controllerParameter)
        {
            System.Diagnostics.Debug.WriteLine(controllerParameter.BoardId);
            if (!(controllerParameter.Id > 0))
            {
                System.Diagnostics.Debug.WriteLine("TEEELSKJSEIJOI");
                _httpContextAccessor.HttpContext.Session.SetString("QueryParams", JsonSerializer.Serialize<BoardControllerCommonParameter>(controllerParameter));
                System.Diagnostics.Debug.WriteLine(_httpContextAccessor.HttpContext.Session.GetString("QueryParams"));
            }

            if (controllerParameter.Id > 0)
            {
                PostDetailWithUser? postDetail = _postService.GetPostDetail(controllerParameter.Id);
                postDetail.Parameters = controllerParameter;
                return await Task.Run(() => View("Detail", postDetail));
            }
            else
            {
                BoardInfoWithPostList boardWithPosts = _postService.GetPostListByBoadId(controllerParameter);
                boardWithPosts.Parameters = controllerParameter;
                return await Task.Run(() => View("Index", boardWithPosts));
            }
        }
    }
}
