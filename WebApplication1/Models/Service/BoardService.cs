using WebApplication1.Models.Dao;

namespace WebApplication1.Models
{
    public class BoardService
    {
        private readonly BoardDao _boardDao = new();
        public List<Board> GetBoardList()
        {
            return _boardDao.GetBoardList();
        }

        public async Task<List<Board>> GetBoardListAsync()
        {
            return await Task.Run(() => _boardDao.GetBoardList());
        }
    }
}
