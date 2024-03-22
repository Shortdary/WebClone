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
        [HttpPost("comment/add")]
        [ValidateAntiForgeryToken]
        public IActionResult PostAdd(CommentAdd commentData)
        {
            string? refererUrl = _httpContextAccessor.HttpContext?.Request.Headers["Referer"];
            commentData.CreatedUid = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            _commentService.CreateComment(commentData);

            if (refererUrl is null)
            {
                return RedirectToRoute("home");
            } else
            {
                return Redirect(refererUrl);
            }
        }

        [Authorize]
        [HttpPost("{boardName}/{postId}/comment/edit")]
        [ValidateAntiForgeryToken]
        public IActionResult PostEdit()
        {
            System.Diagnostics.Debug.WriteLine("edit");

            return View();
        }

        [Authorize]
        [HttpPost("{boardName}/{postId}/comment/delete")]
        [ValidateAntiForgeryToken]
        public IActionResult PostDelete()
        {
            System.Diagnostics.Debug.WriteLine("delete");

            return View();
        }
    }
}
