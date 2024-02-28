using Microsoft.Extensions.Hosting;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models.Common;

namespace WebApplication1.Models.Dao
{
    public class PostDao: DBHelper
    {
        public List<Post> GetPopularPosts()
        {
            List<Post> Posts = new();
            using (var conn = base.GetConnection())
            {
                SqlCommand cmd = new("spSelectPopularPosts", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Post p = new()
                    {
                        Id = Int32.Parse(reader["id"].ToString()),
                        BoardId = Int32.Parse(reader["board_id"].ToString()),
                        Subject = reader["subject"].ToString(),
                        CommentCount = Int32.Parse(reader["comment_count"].ToString()),
                        ViewCount = Int32.Parse(reader["view_count"].ToString()),
                        LikeCount = Int32.Parse(reader["id"].ToString()),
                        CreatedTime = DateTime.Parse(reader["created_time"].ToString()),
                        CreatedUid = Int32.Parse(reader["created_uid"].ToString())
                    };
                    Posts.Add(p);
                }
            }
            return Posts;
        }

        public string GetPostsByBoardId(int boardId)
        {
            string rtnVal = null;
            //using(var db = new CopycatContext())
            using(var conn = base.GetConnection())
            {
                // var comments = db.Comments.ToList();
                SqlCommand cmd = new SqlCommand("spSelectPostsByBoardId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@board_id", boardId);


                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<Post> TestPost = new List<Post>();
                Post post = null;

                while(reader.Read())
                {
                    post = new Post();
                    post.Id = int.Parse(reader["id"].ToString());
                    post.Subject = reader["subject"].ToString();
                    TestPost.Add(post);
                }

                foreach(var p in TestPost)
                {
                    rtnVal += $"{p.Subject} ";
                }

                //rtnVal = returno.Value.ToString();
            }
            return rtnVal;
        }
    }
}
