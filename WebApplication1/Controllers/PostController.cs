using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService = new();

        [HttpGet("new")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost("new")]
        //[ValidateAntiForgeryToken]
        public IActionResult Add(Models.PostInsert p)
        {
            p.CreatedUid = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (ModelState.IsValid)
            {
                (int newPostId, string newPostBoardName) = _postService.CreatePost(p);
                return RedirectToRoute(newPostBoardName, new { postId = newPostId });
            }

            return View("Index");
        }

        [Authorize]
        [HttpPost("edit")]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(PostEdit p)
        {
            p.UpdatedUid = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (ModelState.IsValid)
            {
                (int editPostId, string editPostBoardName) = _postService.EditPost(p);
                return RedirectToRoute(editPostBoardName, new { postId = editPostId });
            }

            return View("Index");
        }
    }
    
}
