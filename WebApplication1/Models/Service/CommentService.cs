using WebApplication1.Models.Dao;

namespace WebApplication1.Models.Service
{
    public class CommentService
    {
        private CommentDao commentDao = new CommentDao();
        public string GetCommentByPostId(int postId)
        {
            return commentDao.GetCommentByPostId(postId);
        }
    }
}
