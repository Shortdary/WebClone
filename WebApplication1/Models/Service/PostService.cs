using WebApplication1.Models.Common;
using WebApplication1.Models.Dao;

namespace WebApplication1.Models;

public class PostService
{
    private readonly PostDao postDao = new();

    // 글 쓰기
    public (int, string) CreatePost(PostInsert p)
    {
        return postDao.CreatePost(p);
    }

    // 게시판ID를 통한 게시물 조회
    public BoardInfoWithPostList GetPostsByBoadId(BoardServiceCommonParameter serviceParamter)
    {
        return postDao.GetPostsByBoardId(serviceParamter);
    }

    // 글 내용 조회
    public PostWithUser GetPostDetail(int postId)
    {
        return postDao.GetPostDetail(postId);
    }
}
