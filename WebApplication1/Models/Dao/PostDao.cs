using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Hosting;
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

                DataTable dt = new();
                SqlDataAdapter da = new(cmd);
                da.Fill(dt);
                
                Posts = dt.AsEnumerable().Select(row =>
                    new PostWithUser
                    {
                        Id = row.Field<int>("id"),
                        BoardId = row.Field<int>("board_id"),
                        Subject = row.Field<string>("subject")!,
                        CommentCount = row.Field<int>("comment_count"),
                        ViewCount = row.Field<int>("view_count"),
                        LikeCount = row.Field<int>("like_count"),
                        CreatedTime = row.Field<DateTime>("created_time"),
                        CreatedUid = row.Field<int>("created_uid"),
                        Nickname = row.Field<string>("nickname")!
                    }).ToList();
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

        public PostWithUser GetPostDetail(int postId)
        {
            PostWithUser p = new();
            using (var conn = GetConnection())
            using (var conn2 = GetConnection())
            {
                SqlCommand postCmd = new("spSelectPostDetail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                postCmd.Parameters.Add(new SqlParameter("@id", postId));
                conn.Open();

                SqlCommand commentCmd = new("spSelectCommentsByPostId", conn2)
                {
                    CommandType = CommandType.StoredProcedure
                };
                commentCmd.Parameters.Add(new SqlParameter("@post_id", postId));
                conn2.Open();

                List<Comment> Comments = new();

                DataTable pdt = new();
                SqlDataAdapter pda = new(postCmd);
                pda.Fill(pdt);

                DataTable cdt = new();
                SqlDataAdapter cda = new(commentCmd);
                cda.Fill(cdt);

                Comments = cdt.AsEnumerable().Select(row =>
                    new Comment
                    {
                        Id = row.Field<int>("id"),
                        PostId = row.Field<int>("post_id"),
                        Comment1 = row.Field<string>("comment")!,
                        LikeCount = row.Field<int>("like_count"),
                        CreatedTime = row.Field<DateTime>("created_time"),
                        CreatedUid = row.Field<int>("created_uid"),
                        Nickname = row.Field<string>("nickname")!
                    }).ToList();

                p = pdt.AsEnumerable().Select(row =>
                new PostWithUser
                {
                    Id = row.Field<int>("id"),
                    BoardId = row.Field<int>("board_id"),
                    Subject = row.Field<string>("subject")!,
                    Detail = row.Field<string>("detail")!,
                    CommentCount = row.Field<int>("comment_count"),
                    ViewCount = row.Field<int>("view_count"),
                    LikeCount = row.Field<int>("like_count"),
                    CreatedTime = row.Field<DateTime>("created_time"),
                    CreatedUid = row.Field<int>("created_uid"),
                    Nickname = row.Field<string>("nickname")!
                }).ToList()[0];
                p.Comments = Comments;
            }

            return p;
        }
    }
}
