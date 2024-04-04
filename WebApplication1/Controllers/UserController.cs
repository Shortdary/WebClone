﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService = new();

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserAddParamter p)
        {
            CommonResponseModel<string> result = _userService.CreateUser(p);
            if (result.StatusCode != 200)
            {
                ViewBag.ErrorMessage = result.Data;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        public IActionResult SuspendUser()
        {
            return View();
        }

        public IActionResult DeleteUser()
        {
            return View();
        }
    }
}
