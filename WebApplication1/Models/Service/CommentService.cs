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

        public void CreateComment(CommentAdd commentData)
        {
            _commentDao.CreateComment(commentData);
        }

        public void EditComment(CommentEdit commentData)
        {
            _commentDao.EditComment(commentData);
        }

        public void DeleteComment(CommentDelete commentData)
        {
            _commentDao.DeleteComment(commentData);
        }
    }
}
