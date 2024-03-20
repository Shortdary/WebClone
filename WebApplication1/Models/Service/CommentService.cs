using WebApplication1.Models.Dao;

namespace WebApplication1.Models.Service
{
    public class CommentService
    {
        private readonly CommentDao _commentDao = new();
        public List<Comment> GetCommentListByPostId(int postId)
        {
            return _commentDao.GetCommentListByPostId(postId);
        }
    }
}
