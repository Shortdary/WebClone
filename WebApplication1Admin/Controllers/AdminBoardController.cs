using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetBoardListTable()
        {
            List<Board> boardList = _boardService.GetBoardList();
            return await Task.Run(() => PartialView("~/Views/Admin/Board/_BoardList.cshtml", boardList));
        }

        public async Task<IActionResult> AddBoard(BoardAdd p)
        {
            CommonResponseModel<string> result = _boardService.AddBoard(p);
            return await Task.Run(() => View("~/Views/Admin/Board/Index.cshtml"));
        }

        public async Task<CommonResponseModel<string>> EditBoard(BoardEdit p)
        {
            CommonResponseModel<string> result = _boardService.EditBoard(p);
            return await Task.Run(() => result);
        }
    }
}
