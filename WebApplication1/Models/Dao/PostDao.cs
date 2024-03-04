using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models.Common;

namespace WebApplication1.Models.Dao
{
    public class PostDao: DBHelper
    {
        public string CreatePost(PostInsert p)
        {
            using var conn = GetConnection();
            SqlCommand cmd = new("spInsertPost", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@board_id", p.BoardId));
            cmd.Parameters.Add(new SqlParameter("@subject", p.Subject));
            cmd.Parameters.Add(new SqlParameter("@detail", p.Detail));
            cmd.Parameters.Add(new SqlParameter("@created_uid", p.CreatedUid));

            var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            conn.Open();
            cmd.ExecuteNonQuery();
            var result = returnParameter.Value;
            System.Diagnostics.Debug.WriteLine(result);
            return "";
        }

        public List<PostWithUser> GetPopularPosts()
        {
            List<PostWithUser> Posts = new();
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new("spSelectPopularPosts", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // TODO : 읽기 방식 ? 변경
                while (reader.Read())
                {
                    PostWithUser p = new()
                    {
                        Id = Int32.Parse(reader["id"].ToString()!),
                        BoardId = Int32.Parse(reader["board_id"].ToString()!),
                        Subject = reader["subject"].ToString()!,
                        CommentCount = Int32.Parse(reader["comment_count"].ToString()!),
                        ViewCount = Int32.Parse(reader["view_count"].ToString()!),
                        LikeCount = Int32.Parse(reader["id"].ToString()!),
                        CreatedTime = DateTime.Parse(reader["created_time"].ToString()!),
                        CreatedUid = Int32.Parse(reader["created_uid"].ToString()!),
                        Nickname = reader["nickname"].ToString()!
                    };
                    Posts.Add(p);
                }
            }
            return Posts;
        }

        public List<PostWithUser> GetPostsByBoardId(int boardId)
        {
            List<PostWithUser> Posts = new();
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new("spSelectPostDetail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@id", boardId));


                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PostWithUser p = new()
                    {
                        Id = Int32.Parse(reader["id"].ToString()!),
                        BoardId = Int32.Parse(reader["board_id"].ToString()!),
                        Subject = reader["subject"].ToString()!,
                        CommentCount = Int32.Parse(reader["comment_count"].ToString()!),
                        ViewCount = Int32.Parse(reader["view_count"].ToString()!),
                        LikeCount = Int32.Parse(reader["id"].ToString()!),
                        CreatedTime = DateTime.Parse(reader["created_time"].ToString()!),
                        CreatedUid = Int32.Parse(reader["created_uid"].ToString()!),
                        Nickname = reader["nickname"].ToString()!
                    };
                    Posts.Add(p);
                }
            }
            return Posts;
        }

        public PostWithUser GetPostDetail(int postId)
        {
            PostWithUser p = new();
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new("spSelectPostDetail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@id", postId));

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    p.Id = Int32.Parse(reader["id"].ToString()!);
                    p.BoardId = Int32.Parse(reader["board_id"].ToString()!);
                    p.Subject = reader["subject"].ToString()!;
                    p.CommentCount = Int32.Parse(reader["comment_count"].ToString()!);
                    p.ViewCount = Int32.Parse(reader["view_count"].ToString()!);
                    p.LikeCount = Int32.Parse(reader["id"].ToString()!);
                    p.CreatedTime = DateTime.Parse(reader["created_time"].ToString()!);
                    p.CreatedUid = Int32.Parse(reader["created_uid"].ToString()!);
                    p.Nickname = reader["nickname"].ToString()!;
                    p.Detail = reader["detail"].ToString()!;
                }
            }

            return p;
        }
    }
}
