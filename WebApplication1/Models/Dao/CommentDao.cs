using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models.Common;

namespace WebApplication1.Models.Dao
{
    public class CommentDao: DBHelper
    {
        public string GetCommentByPostId(int postId)
        {
            string rtnVal = null;
            //using(var db = new CopycatContext())
            using(var conn = base.GetConnection())
            {
                // var comments = db.Comments.ToList();
                SqlCommand cmd = new SqlCommand("spSelectCommentsByPostId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@post_id", postId);


                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<Comment> TestComment = new List<Comment>();
                Comment comment = null;

                while(reader.Read())
                {
                    comment = new Comment();
                    comment.Id = int.Parse(reader["id"].ToString());
                    comment.Comment1 = reader["comment"].ToString();
                    comment.LikeCount = int.Parse(reader["like_count"].ToString());
                    TestComment.Add(comment);
                }

                foreach(var c in TestComment)
                {
                    rtnVal += $"{c.Comment1} ";
                }

                //rtnVal = returno.Value.ToString();
            }
            return rtnVal;
        }
    }
}
