using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Models;
using WebApplication1.Models.Service;

namespace WebApplication1.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService _commentService = new();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CommentAdd commentData)
        {
            string? refererUrl = _httpContextAccessor.HttpContext?.Request.Headers["Referer"];
            commentData.CreatedUid = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            _commentService.CreateComment(commentData);

            if (refererUrl is null)
            {
                return RedirectToRoute("home");
            } else
            {
                CommentPartialViewModel commentPartialViewModel = new();
                commentPartialViewModel.CommentList = _commentService.GetCommentListByPostId(commentData.PostId);
                commentPartialViewModel.PostId = commentData.PostId;
                return PartialView("index", commentPartialViewModel);
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit()
        {
            System.Diagnostics.Debug.WriteLine("edit");

            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CommentDelete commentData)
        {
            string? refererUrl = _httpContextAccessor.HttpContext?.Request.Headers["Referer"];
            _commentService.DeleteComment(commentData);
            if (refererUrl is null)
            {
                return RedirectToRoute("home");
            }
            else
            {
                return Redirect(refererUrl);
            }
        }
    }
}
