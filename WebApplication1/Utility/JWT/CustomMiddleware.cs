﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Utility.JWT
{
    public class AddHeaderMiddleware : Controller
    {
        private readonly RequestDelegate _next;

        public AddHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // 인증을 위해 쿠키에 있는 Authorization을 Request.Header로 옮겨담음
            context.Request.Headers.Add("Authorization", context.Request.Cookies["Authorization"]);
            // context.Session.SetString("PreviousUrl", context.Request.Headers["Referer"].ToString());
            // System.Diagnostics.Debug.WriteLine(context.Session);
            await _next(context);
        }
    }
}
