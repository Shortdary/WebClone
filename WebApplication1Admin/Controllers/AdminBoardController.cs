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

        [HttpPost]
        public async Task<CommonResponseModel<string>> AddBoard(BoardAdd p)
        {
            CommonResponseModel<string> result = new();
            if (ModelState.IsValid)
            {
                result = _boardService.AddBoard(p);
                return await Task.Run(() => result);
            }
            else
            {
                result.StatusCode = 400;
                foreach (var entry in ModelState.Values)
                {
                    System.Diagnostics.Debug.WriteLine(entry.RawValue.ToString());
                    var errors = entry.Errors;
                    if(errors.Count > 0)
                    {
                        result.Data += $"{errors[0].ErrorMessage}\n";
                    }
                }
                return await Task.Run(() => result);
            }

        }

        [HttpPost]
        public async Task<CommonResponseModel<string>> EditBoard(BoardEdit p)
        {
            CommonResponseModel<string> result = _boardService.EditBoard(p);
            return await Task.Run(() => result);
        }

        [HttpPost]
        public async Task<CommonResponseModel<string>> DeleteBoard(BoardDelete p)
        {
            CommonResponseModel<string> result = _boardService.DeleteBoard(p);
            return await Task.Run(() => result);
        }
    }
}
