using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using WebApplication1.Models.Common;

namespace WebApplication1.Models.Dao
{
    public class CommentDao : DBHelper
    {
        public List<Comment> GetCommentListByPostId(int? postId)
        {
            List<Comment> comments = new();
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new("spSelectCommentsByPostId", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@post_id", postId);
                conn.Open();

                DataTable dt = new();
                SqlDataAdapter da = new(cmd);
                da.Fill(dt);

                comments = dt.AsEnumerable().Select(row =>
                   new Comment
                   {
                       Id = row.Field<int>("id"),
                       PostId = row.Field<int>("post_id"),
                       Comment1 = row.Field<string>("comment")!,
                       LikeCount = row.Field<int>("like_count"),
                       CreatedTime = row.Field<DateTime>("created_time"),
                       CreatedUid = row.Field<int>("created_uid"),
                       ParentCommentId = row.Field<int?>("parent_comment_id"),
                       Nickname = row.Field<string>("nickname")!
                   }).ToList();
                conn.Close();
            }
            return comments;
        }

        public void CreateComment(CommentAdd commentData)
        {
            using SqlConnection conn = GetConnection();
            SqlCommand cmd = new("spInsertComment", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@post_id", commentData.PostId);
            cmd.Parameters.AddWithValue("@comment", commentData.Comment1);
            cmd.Parameters.AddWithValue("@created_uid", commentData.CreatedUid);
            cmd.Parameters.AddWithValue("@parent_comment_id", commentData.ParentCommentId is null ? DBNull.Value : commentData.ParentCommentId);
            
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
