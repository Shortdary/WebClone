using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.JWT;


namespace WebApplication1.Controllers
{
    public class CredentialsContorller : Controller
    {
        private readonly IConfiguration _config;
        private readonly UserService _userService = new();

        public CredentialsContorller(IConfiguration config)
        {
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

        [HttpPost("login")]
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
                    SameSite = SameSiteMode.Strict
                };
                Response.Cookies.Append("Authorization", $"Bearer {tokenString}", cookieOptions);

                if(!string.IsNullOrEmpty(loginUser.RedirectUrl))
                {
                    return Redirect(loginUser.RedirectUrl);

                }
                else
                {
                    return RedirectToRoute("home");
                }
            }

            return View("Login");
        }

        [Authorize]
        [Route("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Authorization");
            return RedirectToRoute("home");
        }
    }
}