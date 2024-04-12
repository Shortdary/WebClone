using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Utility
{
    public class BoardListViewComponent : ViewComponent
    {
        private readonly BoardService _boardService;

        public BoardListViewComponent(BoardService boardService)
        {
            _boardService = boardService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Board> boardList = await _boardService.GetBoardListAsync();
            return View(boardList);
        }
    }
}
