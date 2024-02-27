using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models.Common;

namespace WebApplication1.Models.Dao
{
    public class PostDao: DBHelper
    {
        public string GetPopularPosts()
        {
            string rtnVal = null;
            //using(var db = new CopycatContext())
            using (var conn = base.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spSelectPopularPosts", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<Post> Posts = new List<Post>();
                Post post = null;

                while (reader.Read())
                {
                    rtnVal += $"{reader["subject"].ToString()}\n\r";
                }
            }
            return rtnVal;
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
