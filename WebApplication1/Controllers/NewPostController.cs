using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;
using WebApplication1.Models.Service;

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
            // TODO : Delete here
            p.CreatedUid = 1;

            if (ModelState.IsValid)
            {
                _postService.CreatePost(p);
            }
            return View("Index");
        }
    }
    
}
