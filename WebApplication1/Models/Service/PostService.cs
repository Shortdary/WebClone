using WebApplication1.Models.Common;
using WebApplication1.Models.Dao;

namespace WebApplication1.Models;

public class PostService
{
    private readonly PostDao _postDao = new();

    // 글 쓰기
    public (int, string) CreatePost(PostInsert p)
    {
        return _postDao.CreatePost(p);
    }

    // 게시판ID를 통한 게시물 조회
    public BoardInfoWithPostList GetPostsByBoadId(BoardServiceCommonParameter serviceParamter)
    {
        return _postDao.GetPostsByBoardId(serviceParamter);
    }

    // 글 내용 조회
    public PostWithUser GetPostDetail(int postId)
    {
        return _postDao.GetPostDetail(postId);
    }

    public void EditPost(PostEdit p)
    {
        _postDao.EditPost(p);
    }

    public void DeletePost(PostDelete p)
    {
        _postDao.DeletePost(p);
    }
}
