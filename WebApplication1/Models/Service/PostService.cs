using WebApplication1.Models.Dao;

namespace WebApplication1.Models.Service
{
    public class PostService
    {
        private PostDao postDao = new PostDao();

        public List<Post> GetPopularPosts()
        {
            return postDao.GetPopularPosts();
        }

        public string GetPostsByBoadId(int boardId)
        {
            return postDao.GetPostsByBoardId(boardId);
        }
    }
}
