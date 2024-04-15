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
                       Description = row.Field<string?>("description"),
                       ParentBoardId = row.Field<int?>("parent_board_id"),
                       Priority = row.Field<int>("priority"),
                       IsDeleted = row.Field<bool>("is_deleted")!
                   }).ToList();
                conn.Close();
            }
            return boards;
        }

        public CommonResponseModel<string> AddBoard(BoardAdd p)
        {
            CommonResponseModel<string> result = new();
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    SqlCommand cmd = new("spInsertBoard", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@board_name", p.BoardName);
                    cmd.Parameters.AddWithValue("@board_name_eng", p.BoardNameEng);
                    cmd.Parameters.AddWithValue("@description", p.Description);
                    cmd.Parameters.AddWithValue("@parent_board_id", p.ParentBoardId is null ? DBNull.Value : p.ParentBoardId);
                    cmd.Parameters.AddWithValue("@priority", p.Priority is null ? DBNull.Value : p.Priority);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    switch (ex.Number)
                    {
                        default:
                            result.StatusCode = 500;
                            result.Data = "에러 발생";
                            break;
                    }
                }
                conn.Close();
            }
            return result;
        }

        public CommonResponseModel<string> EditBoard(BoardEdit p)
        {
            CommonResponseModel<string> result = new();
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    SqlCommand cmd = new("spUpdateBoard", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@id", p.Id);
                    cmd.Parameters.AddWithValue("@board_name", p.BoardName);
                    cmd.Parameters.AddWithValue("@description", p.Description);
                    cmd.Parameters.AddWithValue("@parent_board_id", p.ParentBoardId is null ? DBNull.Value : p.ParentBoardId);
                    cmd.Parameters.AddWithValue("@priority", p.Priority is null ? DBNull.Value : p.Priority);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    switch (ex.Number)
                    {
                        default:
                            result.StatusCode = 500;
                            result.Data = "에러 발생";
                            break;
                    }
                }
                conn.Close();
            }
            return result;
        }
    }
}
