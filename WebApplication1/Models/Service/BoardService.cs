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

        public CommonResponseModel<string> AddBoard(BoardAdd p)
        {
            return _boardDao.AddBoard(p);
        }

        public CommonResponseModel<string> EditBoard(BoardEdit p)
        {
            return _boardDao.EditBoard(p);
        }

        public CommonResponseModel<string> DeleteBoard(BoardDelete p)
        {
            return _boardDao.DeleteBoard(p);
        }
    }
}
