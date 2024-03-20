using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using WebApplication1.Models.Common;

namespace WebApplication1.Models.Dao
{
    public class CommentDao : DBHelper
    {
        public List<Comment> GetCommentListByPostId(int postId)
        {
            List<Comment> comments = new();
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new("spSelectCommentsByPostId", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@post_id", postId));
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
            }
            return comments;
        }
    }
}
