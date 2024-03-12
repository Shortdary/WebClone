using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using WebApplication1.Models.Common;

namespace WebApplication1.Models.Dao
{
    public class PostDao: DBHelper
    {
        public (int, string) CreatePost(PostInsert p)
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

            var returnId = cmd.Parameters.Add("@id", SqlDbType.Int);
            returnId.Direction = ParameterDirection.Output;
            var returnBoardName = cmd.Parameters.Add("@board_name_eng", SqlDbType.NChar, 20);
            returnBoardName.Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            return ((int)returnId.Value, ((string)returnBoardName.Value).Trim());
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
                        BoardName = row.Field<string>("board_name")!,
                        BoardNameEng = row.Field<string>("board_name_eng")!,
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

        public BoardInfoWithPostList GetPostsByBoardId(BoardServiceCommonParameter p)
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
                else
                {
                    postCmd.CommandText = "spSelectPostsByBoardId";
                    postCmd.Parameters.Add(new SqlParameter("@board_id", p.BoardId));
                }
                postCmd.CommandType = CommandType.StoredProcedure;
                postCmd.Parameters.Add(new SqlParameter("@page_number", p.PageNumber));
                postCmd.Parameters.Add(new SqlParameter("@page_size", p.PageSize));
                conn.Open();

                SqlCommand boardCmd = new("spSelectBoardInfoByBoardId", conn2)
                {
                    CommandType = CommandType.StoredProcedure
                };
                boardCmd.Parameters.Add(new SqlParameter("@board_id", p.BoardId));
                conn2.Open();

                DataSet pds = new();
                SqlDataAdapter pda = new(postCmd);
                pda.Fill(pds);

                DataTable bdt = new();
                SqlDataAdapter bda = new(boardCmd);
                bda.Fill(bdt);


                DataRow? row = bdt.Rows.Cast<DataRow>().FirstOrDefault();
                if(row is null)
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

                // if (boardInfo.Count > 0) boardWithPosts = boardInfo[0];

                boardWithPosts.PostList = pds.Tables[0].AsEnumerable().Select(row =>
                    new PostWithUser
                    {
                        Id = row.Field<int>("id"),
                        BoardId = row.Field<int>("board_id"),
                        BoardName = row.Field<string>("board_name")!,
                        BoardNameEng = row.Field<string>("board_name_eng")!,
                        Subject = row.Field<string>("subject")!,
                        CommentCount = row.Field<int>("comment_count"),
                        ViewCount = row.Field<int>("view_count"),
                        LikeCount = row.Field<int>("like_count"),
                        CreatedTime = row.Field<DateTime>("created_time"),
                        CreatedUid = row.Field<int>("created_uid"),
                        Nickname = row.Field<string>("nickname")!
                    }).ToList();

                boardWithPosts.TotalRowNum = pds.Tables[1].Select().FirstOrDefault()!.Field<int>("total_row_count");
            }
            return boardWithPosts;
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

                List<Comment> comments = new();

                DataTable pdt = new();
                SqlDataAdapter pda = new(postCmd);
                pda.Fill(pdt);

                DataTable cdt = new();
                SqlDataAdapter cda = new(commentCmd);
                cda.Fill(cdt);

                comments = cdt.AsEnumerable().Select(row =>
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

                var postContents = pdt.AsEnumerable().Select(row =>
                new PostWithUser
                {
                    Id = row.Field<int>("id"),
                    BoardId = row.Field<int>("board_id"),
                    BoardName = row.Field<string>("board_name")!,
                    BoardNameEng = row.Field<string>("board_name_eng")!,
                    Subject = row.Field<string>("subject")!,
                    Detail = row.Field<string>("detail")!,
                    CommentCount = row.Field<int>("comment_count"),
                    ViewCount = row.Field<int>("view_count"),
                    LikeCount = row.Field<int>("like_count"),
                    CreatedTime = row.Field<DateTime>("created_time"),
                    CreatedUid = row.Field<int>("created_uid"),
                    Nickname = row.Field<string>("nickname")!
                }).ToList();
                if (postContents.Count > 0) p = postContents[0];

                p.Comments = comments;
            }

            return p;
        }
    }
}
