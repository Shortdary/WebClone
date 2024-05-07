using System.Text.Json;
using WebApplication1.Models.Common;
using WebApplication1.Models.Dao;

namespace WebApplication1.Models;

public class PostService
{
    private readonly PostDao _postDao = new();
    private readonly CommentDao _commentDao = new();

    // 글 쓰기
    public (int, int) CreatePost(PostInsert p)
    {
        return _postDao.CreatePost(p);
    }

    /// <summary>
    /// 게시판 ID를 통해 게시글 리스트 조회
    /// </summary>
    /// <param name="serviceParamter"></param>
    /// <returns></returns>
    public BoardInfoWithPostList GetPostListByBoadId(BoardControllerCommonParameter serviceParamter)
    {
        BoardInfoWithPostList boardInfoWithPostListModel;
        PaginationModel pagination = new()
        {
            PageNumber = serviceParamter.PageNumber,
            PageSize = serviceParamter.PageSize,
            FormId = "board-page-form"
        };
        List<SelectListItemModel> selectItemList = new()
            {
                new() { Text = "게시글ID", Value = "id" },
                new() { Text = "닉네임", Value = "nickname" }
            };
        SearchModel search = new()
        {
            SearchTarget = serviceParamter.SearchTarget,
            SearchKeyword = serviceParamter.SearchKeyword,
            FormId = "board-page-form",
            StringifiedSelectListItemList = JsonSerializer.Serialize(selectItemList)
        };
        boardInfoWithPostListModel = _postDao.GetPostListByBoardId(serviceParamter);
        pagination.TotalRowNum = boardInfoWithPostListModel.TotalRowNum;
        boardInfoWithPostListModel.Pagination = pagination;
        boardInfoWithPostListModel.Search = search;

        return boardInfoWithPostListModel;
    }

    public BoardInfoWithPostList GetPostList(BoardControllerCommonParameter serviceParamter)
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

    public List<PostDetailWithUser> GetAdminDetailPostList(AdminUserDetailQuery q)
    {
        List<PostDetailWithUser> postList;
        (postList, _) = _postDao.GetPostListByUserId(q);
        return postList;
    }
}
