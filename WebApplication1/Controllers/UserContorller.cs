using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.JWT;


namespace WebApplication1.Controllers
{
    public class UserContorller : Controller
    {
        private readonly TokenManager _tm = new();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User loginUser)
        {
            IActionResult response = Unauthorized();

            User authUser = _tm.AuthenticateUser(loginUser);
            if (authUser != null)
            {
                var tokenString = _tm.GenerateJWTToken(authUser);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = authUser,
                });
            }
            return response;
        }

        
    }
}
