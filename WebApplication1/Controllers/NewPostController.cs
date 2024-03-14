using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class NewPostController : Controller
    {
        private readonly PostService _postService = new();

        [HttpGet("new")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("new")]
        //[ValidateAntiForgeryToken]
        public IActionResult Add(Models.PostInsert p)
        {
            int newPostId;
            string newPostBoardName;
            // TODO : Delete here
            p.CreatedUid = 1;

            if (ModelState.IsValid)
            {
                (newPostId, newPostBoardName) = _postService.CreatePost(p);
                return RedirectToRoute("best", new { postId = newPostId });
            }

            return View("Index");
        }
    }
    
}
