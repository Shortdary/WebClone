using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Utility.JWT;

namespace WebApplication1.Controllers
{
    public class CredentialsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserService _userService = new();

        public CredentialsController(
            IConfiguration config, 
            IHttpContextAccessor httpContextAccessor 
            )
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            ViewBag.ReturnUrl = _httpContextAccessor.HttpContext?.Request.Headers["Referer"];

            return View("Login");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserLoginCredentials loginUser)
        {
            TokenManager _tm = new(_config);
            CommonResponseModel<UserLoginResponseModel> result = _userService.VerifyUser(loginUser);

            if (result.StatusCode == 200)
            {
                string tokenString = _tm.GenerateJWTToken(result.Data.User1);
                CookieOptions cookieOptions = new()
                {
                    Expires = DateTime.UtcNow.AddHours(12),
                    //Domain = Request.Path.Value,
                    Path = "/",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                };
                Response.Cookies.Append("Authorization", $"Bearer {tokenString}", cookieOptions);
                
                if(!string.IsNullOrEmpty(loginUser.ReturnUrl))
                {
                    return Redirect(loginUser.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.ErrorMessage = result.Data.ErrorMessage;
                return View();
            }
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("Authorization");
            return RedirectToAction("Index", "Home");
        }
    }
}