using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models.Common;


namespace WebApplication1.Models.Dao
{
    public class BoardDao : DBHelper
    {
        public List<Board> GetBoardList()
        {
            List<Board> boards = new();
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new("spSelectBoards", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                DataTable dt = new();
                SqlDataAdapter da = new(cmd);
                da.Fill(dt);

                boards = dt.AsEnumerable().Select(row =>
                   new Board
                   {
                       Id = row.Field<int>("id")!,
                       BoardName = row.Field<string>("board_name")!,
                       BoardNameEng = row.Field<string>("board_name_eng")!.Trim(),
                       ParentBoardId = row.Field<int?>("parent_board_id"),
                       Priority = row.Field<int>("priority"),
                       IsDeleted = row.Field<bool>("is_deleted")!
                   }).ToList();
                conn.Close();
            }
            return boards;
        }
    }
}
