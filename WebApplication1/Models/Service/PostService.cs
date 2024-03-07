using WebApplication1.Models.Common;
using WebApplication1.Models.Dao;

namespace WebApplication1.Models.Service
{
    public class PostService
    {
        private readonly PostDao postDao = new();

        // 글 쓰기
        public (int, string) CreatePost(PostInsert p)
        {
            return postDao.CreatePost(p);
        }

        // 인기글 조회
        public List<PostWithUser> GetPopularPosts()
        {
            return postDao.GetPopularPosts();
        }

        // 게시판ID를 통한 게시물 조회
        public BoardInfoWithPostList GetPostsByBoadId(int boardId)
        {
            return postDao.GetPostsByBoardId(boardId);
        }

        // 글 내용 조회
        public PostWithUser GetPostDetail(int postId)
        {
            return postDao.GetPostDetail(postId);
        }
    }
}
