using WebApplication1.Models.Dao;

namespace WebApplication1.Models.Service
{
    public class PostService
    {
        private PostDao postDao = new PostDao();

        // 인기글 조회
        public List<PostWithUser> GetPopularPosts()
        {
            return postDao.GetPopularPosts();
        }

        // 게시판ID를 통한 게시물 조회
        public string GetPostsByBoadId(int boardId)
        {
            return postDao.GetPostsByBoardId(boardId);
        }

        public PostDetailWithUser GetPostDetail(int postId)
        {
            return postDao.GetPostDetail(postId);
        }
    }
}
