using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using WebApplication1.Models.Common;

namespace WebApplication1.Models.Dao
{
    public class PostDao : DBHelper
    {
        public (int, string) CreatePost(PostInsert p)
        {
            using var conn = GetConnection();
            SqlCommand cmd = new("spInsertPost", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@board_id", p.BoardId);
            cmd.Parameters.AddWithValue("@subject", p.Subject);
            cmd.Parameters.AddWithValue("@detail", p.Detail);
            cmd.Parameters.AddWithValue("@created_uid", p.CreatedUid);

            var returnId = cmd.Parameters.Add("@id", SqlDbType.Int);
            returnId.Direction = ParameterDirection.Output;
            var returnBoardName = cmd.Parameters.Add("@board_name_eng", SqlDbType.NChar, 20);
            returnBoardName.Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return ((int)returnId.Value, ((string)returnBoardName.Value).Trim());
        }
        public void EditPost(PostEdit p)
        {
            using var conn = GetConnection();
            SqlCommand cmd = new("spUpdatePost", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@id", p.PostId);
            cmd.Parameters.AddWithValue("@subject", p.Subject);
            cmd.Parameters.AddWithValue("@detail", p.Detail);
            cmd.Parameters.AddWithValue("@updated_uid", p.UpdatedUid);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeletePost(PostDelete p)
        {
            // TODO : 삭제 권한 있는지 확인
            using var conn = GetConnection();
            SqlCommand cmd = new("spDeletePost", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@id", p.PostId);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public BoardInfoWithPostList GetPostListByBoardId(BoardServiceCommonParameter p)
        {
            BoardInfoWithPostList boardWithPosts = new();
            using (var conn = GetConnection())
            using (var conn2 = GetConnection())
            {
                SqlCommand postCmd = new(string.Empty, conn);
                if (p.BoardId == 24)
                {
                    postCmd.CommandText = "spSelectPopularPosts";
                }
                else if (p.BoardId == 25)
                {
                    postCmd.CommandText = "spSelectAllPosts";
                }
                else
                {
                    postCmd.CommandText = "spSelectPostsByBoardId";
                    postCmd.Parameters.AddWithValue("@board_id", p.BoardId);
                }
                postCmd.CommandType = CommandType.StoredProcedure;
                postCmd.Parameters.AddWithValue("@page_number", p.PageNumber);
                postCmd.Parameters.AddWithValue("@page_size", p.PageSize);
                conn.Open();

                SqlCommand boardCmd = new("spSelectBoardInfoByBoardId", conn2)
                {
                    CommandType = CommandType.StoredProcedure
                };
                boardCmd.Parameters.AddWithValue("@board_id", p.BoardId);
                conn2.Open();

                DataSet pds = new();
                SqlDataAdapter pda = new(postCmd);
                pda.Fill(pds);

                DataTable bdt = new();
                SqlDataAdapter bda = new(boardCmd);
                bda.Fill(bdt);

                DataRow? row = bdt.Rows.Cast<DataRow>().FirstOrDefault();
                if (row is null)
                {

                }
                else
                {
                    boardWithPosts = new()
                    {
                        Id = row.Field<int>("id"),
                        BoardName = row.Field<string>("board_name")!,
                        BoardNameEng = row.Field<string>("board_name_eng")!.Trim(),
                        Description = row.Field<string>("description")!,
                        PageNumber = p.PageNumber,
                        PageSize = p.PageSize,
                    };
                }

                boardWithPosts.PostList = pds.Tables[0].AsEnumerable().Select(row =>
                    new PostDetailWithUser
                    {
                        Id = row.Field<int>("id"),
                        BoardId = row.Field<int>("board_id"),
                        BoardName = row.Field<string>("board_name")!,
                        BoardNameEng = row.Field<string>("board_name_eng")!.Trim(),
                        Subject = row.Field<string>("subject")!,
                        CommentCount = row.Field<int>("comment_count"),
                        ViewCount = row.Field<int>("view_count"),
                        LikeCount = row.Field<int>("like_count"),
                        CreatedTime = row.Field<DateTime>("created_time"),
                        CreatedUid = row.Field<int>("created_uid"),
                        Nickname = row.Field<string>("nickname")!
                    }).ToList();

                boardWithPosts.TotalRowNum = pds.Tables[1].Select().FirstOrDefault()!.Field<int>("total_row_count");
                conn.Close();
                conn2.Close();
            }
            return boardWithPosts;
        }

        public PostDetailWithUser? GetPostDetail(int postId)
        {
            PostDetailWithUser? p;
            using (var conn = GetConnection())
            using (var conn2 = GetConnection())
            {
                SqlCommand cmd = new("spSelectPostDetail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id", postId);
                conn.Open();

                DataTable dt = new();
                SqlDataAdapter da = new(cmd);
                da.Fill(dt);

                DataRow? row = dt.Rows.Cast<DataRow>().FirstOrDefault();
                if (row is null)
                {
                    p = null;
                }
                else
                {
                    p = new()
                    {
                        Id = row.Field<int>("id"),
                        BoardId = row.Field<int>("board_id"),
                        BoardName = row.Field<string>("board_name")!,
                        BoardNameEng = row.Field<string>("board_name_eng")!.Trim(),
                        Subject = row.Field<string>("subject")!,
                        Detail = row.Field<string>("detail")!,
                        CommentCount = row.Field<int>("comment_count"),
                        ViewCount = row.Field<int>("view_count"),
                        LikeCount = row.Field<int>("like_count"),
                        CreatedTime = row.Field<DateTime>("created_time"),
                        CreatedUid = row.Field<int>("created_uid"),
                        Nickname = row.Field<string>("nickname")!
                    };
                }
                conn.Close();
            }
            return p;
        }
    }
}
