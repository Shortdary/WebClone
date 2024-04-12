using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1Admin.Controllers
{
    public class AdminBoardController : Controller
    {
        private readonly BoardService _boardService = new();

        [HttpGet]
        public async Task<IActionResult> BoardList()
        {
            List<Board> boardList = _boardService.GetBoardList(); 
            return await Task.Run(() => View("~/Views/Admin/Board/Index.cshtml", boardList));
        }
    }
}
