using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CommentController : Controller
    {
        [Authorize]
        [HttpPost("new_comment")]
        [ValidateAntiForgeryToken]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost("edit_comment")]
        [ValidateAntiForgeryToken]
        public IActionResult PostEdit()
        {
            return View();
        }

        [Authorize]
        [HttpPost("delete_comment")]
        [ValidateAntiForgeryToken]
        public IActionResult PostDelete()
        {
            return View();
        }
    }
}
