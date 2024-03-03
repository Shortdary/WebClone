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

        [HttpPost("new/add")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Models.Post p)
        {
            p.CommentCount = 0;
            p.LikeCount = 0;
            p.CreatedTime= DateTime.UtcNow;
            p.CreatedUid = 1;
            p.BoardId = 33;

            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine($"valid에 {p} 제목이 있나? {p.Subject}");
                //_postService.CreatePost(p);
            }

            foreach (PropertyInfo propertyInfo in p.GetType().GetProperties())
            {
                System.Diagnostics.Debug.WriteLine($"{propertyInfo.Name}: {propertyInfo.GetValue(p, null).ToString()}");
            }

            System.Diagnostics.Debug.WriteLine($"{ModelState.IsValid}");
            
            
            return RedirectToAction("Index");
        }
    }
    
}
