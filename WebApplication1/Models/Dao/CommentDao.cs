﻿using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models.Common;

namespace WebApplication1.Models.Dao
{
    public class CommentDao : DBHelper
    {
        public List<Comment> GetCommentListByPostId(int postId)
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
                       Nickname = row.Field<string>("nickname")!,
                       IsDeleted = row.Field<bool>("is_deleted")
                   }).ToList();
                conn.Close();
            }
            return comments;
        }

        public (List<Comment>, int) GetCommentListByUserId(AdminUserDetailQuery q)
        {
            List<Comment> commentList = new();
            int totalRowNum;

            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new("spSelectCommentListByUserId", conn)
                {
                    CommandType = CommandType.StoredProcedure
                }; 
                cmd.Parameters.AddWithValue("@user_id", q.Id);
                cmd.Parameters.AddWithValue("@page_number", q.PageNumber);
                cmd.Parameters.AddWithValue("@page_size", q.PageSize);
                cmd.Parameters.AddWithValue("@search_target", q.SearchTarget ?? "");
                cmd.Parameters.AddWithValue("@search_keyword", q.SearchKeyword is null ? DBNull.Value : q.SearchKeyword);

                conn.Open();

                DataSet ds = new();
                SqlDataAdapter da = new(cmd);
                da.Fill(ds);

                commentList = ds.Tables[0].AsEnumerable().Select(row =>
                   new Comment
                   {
                       Id = row.Field<int>("id"),
                       PostId = row.Field<int>("post_id"),
                       Comment1 = row.Field<string>("comment")!,
                       LikeCount = row.Field<int>("like_count"),
                       CreatedTime = row.Field<DateTime>("created_time"),
                       CreatedUid = row.Field<int>("created_uid"),
                       ParentCommentId = row.Field<int?>("parent_comment_id"),
                       Nickname = row.Field<string>("nickname")!,
                       IsDeleted = row.Field<bool>("is_deleted")
                   }).ToList();

                totalRowNum = ds.Tables[1].Select().FirstOrDefault()!.Field<int>("total_row_count");
                conn.Close();
            }
            return (commentList, totalRowNum);
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

        // TODO
        public void EditComment(CommentEdit commentData)
        {
            using SqlConnection conn = GetConnection();
            SqlCommand cmd = new("spUpdateComment", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteComment(CommentDelete commentData)
        {
            using SqlConnection conn = GetConnection();
            SqlCommand cmd = new("spDeleteComment", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@comment_id", commentData.Id);
            
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
