using WebApplication1.Models.Common;
using WebApplication1.Models.Dao;

namespace WebApplication1.Models;

public class PostService
{
    private readonly PostDao _postDao = new();
    private readonly CommentDao _commentDao = new();

    // 글 쓰기
    public (int, string) CreatePost(PostInsert p)
    {
        return _postDao.CreatePost(p);
    }

    /// <summary>
    /// 게시판 ID를 통해 게시글 리스트 조회
    /// </summary>
    /// <param name="serviceParamter"></param>
    /// <returns></returns>
    public BoardInfoWithPostList GetPostListByBoadId(BoardServiceCommonParameter serviceParamter)
    {
        return _postDao.GetPostListByBoardId(serviceParamter);
    }


    /// <summary>
    /// 게시글 상세내용과 댓글 조회
    /// </summary>
    /// <param name="postId">게시물 ID</param>
    /// <returns></returns>
    public PostDetailWithUser GetPostDetail(int postId)
    {
        PostDetailWithUser p = _postDao.GetPostDetail(postId);

        CommentPartialViewModel commentPartialViewModel = new()
        {
            CommentList = _commentDao.GetCommentListByPostId(postId),
            PostId = postId
        };
        p.CommentPartialViewModel = commentPartialViewModel;
        return p;
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
