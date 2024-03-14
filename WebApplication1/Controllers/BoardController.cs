﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BoardController : Controller
    {
        private readonly PostService _postService = new();

        public IActionResult Index(BoardInfoWithPostList model)
        {
            return View(model);
        }

        // GET: /{boardName}/{postId}
        [HttpGet("{boardName}/{postId}")]
        public IActionResult Detail(string boardName, int postId)
        {
            PostWithUser postDetail = _postService.GetPostDetail(postId);
            return View(postDetail);
        }

        [HttpGet]
        [Route("best")]
        public IActionResult Best(BoardControllerCommonParameter controllerParameter)
        {

            System.Diagnostics.Debug.WriteLine($"Best!!");
            System.Diagnostics.Debug.WriteLine(Request.Cookies["Authorization"]);

            BoardServiceCommonParameter serviceParameter = new()
            {
                BoardId = 24,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            BoardInfoWithPostList boardWithPosts = _postService.GetPostsByBoadId(serviceParameter);
            return View("Index", boardWithPosts);
        }

        [HttpGet]
        [Route("stream_free")]
        public IActionResult StreamFree(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParameter = new()
            {
                BoardId = 2,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            BoardInfoWithPostList boardWithPosts = _postService.GetPostsByBoadId(serviceParameter);
            return View("Index", boardWithPosts);
        }

        [HttpGet]
        [Route("stream_meme")]
        public IActionResult StreamMeme(BoardControllerCommonParameter controllerParameter)
        {
            BoardServiceCommonParameter serviceParameter = new()
            {
                BoardId = 3,
                PageNumber = controllerParameter.PageNumber,
                PageSize = controllerParameter.PageSize
            };
            BoardInfoWithPostList boardWithPosts = _postService.GetPostsByBoadId(serviceParameter);
            return View("Index", boardWithPosts);
        }
    }
}
