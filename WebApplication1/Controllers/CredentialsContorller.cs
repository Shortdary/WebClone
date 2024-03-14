using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.JWT;
using System.Runtime.InteropServices;
using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;


namespace WebApplication1.Controllers
{
    public class CredentialsContorller : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly UserService _userService = new();

        public CredentialsContorller(IConfiguration config, ILogger<HomeController> logger)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("Credentials/login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserLoginCredentials loginUser)
        {
            TokenManager _tm = new(_config);
            User? authUser = _userService.VerifyUser(loginUser);

            if (authUser != null)
            {
                var tokenString = _tm.GenerateJWTToken(authUser);

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddHours(12),
                    //Domain = Request.Path.Value,
                    Path = "/",
                    HttpOnly = true,
                };
                Response.Cookies.Append("Authorization", $"Bearer {tokenString}", cookieOptions);
                //Response.Cookies.Append("Authorization", tokenString, cookieOptions);

                return RedirectToRoute("");
            }
            return View("Login");
        }
    }
}
