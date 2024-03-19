using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService = new();

        [HttpGet("new_post")]
        public IActionResult Add()
        {
            return View("New");
        }

        [Authorize]
        [HttpPost("new_post")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Models.PostInsert p)
        {
            p.CreatedUid = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (ModelState.IsValid)
            {
                (int newPostId, string newPostBoardName) = _postService.CreatePost(p);
                return RedirectToRoute(newPostBoardName, new { postId = newPostId });
            }

            return View("New");
        }

        [Authorize]
        [HttpGet("{boardName}/{postId}/edit")]
        public IActionResult GetEdit(int postId)
        {
            int editorUid = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            PostWithUser p = _postService.GetPostDetail(postId);

            if (editorUid != p.CreatedUid)
            {
                return RedirectToRoute("home");
            }

            return View("Edit", p);
        }

        [Authorize]
        [HttpPost("{boardName}/{postId}/edit")]
        [ValidateAntiForgeryToken]
        public IActionResult PostEdit(PostEdit p)
        {
            PostWithUser postDetail = _postService.GetPostDetail(p.PostId);

            p.UpdatedUid = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            p.BoardNameEng = postDetail.BoardNameEng;
            if (ModelState.IsValid)
            {
                _postService.EditPost(p);
                return RedirectToRoute(p.BoardNameEng!.Trim(), new { postId = p.PostId });
            }

            return View("Edit", postDetail);
        }

        [Authorize]
        [HttpPost("delete_post")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PostDelete p)
        {
            if (ModelState.IsValid)
            {
                _postService.DeletePost(p);
                return RedirectToRoute("home");
            }

            return View("Index");
        }
    }

}
