using WebApplication1.Models.Dao;
using System.Text.Json;
using WebApplication1.Utility.Encryption;

namespace WebApplication1.Models;


public class UserService
{
    private readonly UserDao _userDao = new();
    private readonly PostDao _postDao = new();
    private readonly CommentDao _commentDao = new();
    private readonly UserEncryption _userEncryption = new();

    /// <summary>
    /// ID와 PWD를 받아 유저를 검증하는 함수
    /// </summary>
    /// <param name="ulc">유저 로그인 정보</param>
    /// <returns>
    /// 일치하는 유저가 있을경우 User 인스턴스
    /// 없을 시 null
    /// </returns>
    public CommonResponseModel<UserLoginResponseModel> VerifyUser(UserLoginCredentials ulc)
    {
        CommonResponseModel<UserLoginResponseModel> result = new();
        User? user = _userDao.GetUserByLoginCredentials(ulc);
        UserLoginResponseModel userLoginResponseModel = new()
        {
            User1 = user
        };
        result.Data = userLoginResponseModel;

        // Sql에서 User를 얻어오지 못함.
        if (user is null)
        {
            result.StatusCode = 400;
            result.Data.ErrorMessage = "잘못된 정보를 입력하였거나 가입되지 않은 사용자입니다.";
            return result;
        }

        // User를 얻어왔으나 비밀번호가 틀렸을 때
        user = _userEncryption.VerifyUserPassword(ulc.Password, user.Password) ? user : null;
        if(user is null)
        {
            result.StatusCode = 400;
            result.Data.ErrorMessage = "비밀번호를 확인해주세요.";
        }

        return result;
    }


    public AdminUserListModel PopulateUserListModel(AdminUserListQuery q)
    {
        AdminUserListModel adminUserListModel = new();
        PaginationModel pagination = new()
        {
            PageNumber = q.PageNumber,
            PageSize = q.PageSize,
            FormId = "admin-user-page-form"
        };
        List<SelectListItemModel> a = new()
            {
                new() { Text = "사용자ID", Value = "id" },
                new() { Text = "닉네임", Value = "nickname" }
            };
        SearchModel search = new()
        {
            SearchTarget = q.SearchTarget,
            SearchKeyword = q.SearchKeyword,
            FormId = "admin-user-page-form",
            StringifiedSelectListItemList = JsonSerializer.Serialize(a)
        };
        (_, pagination.TotalRowNum) = _userDao.GetUserList(q);
        adminUserListModel.Pagination = pagination;
        adminUserListModel.Search = search;

        return adminUserListModel;
    }

    public List<UserForAdmin> GetAdminUserList(AdminUserListQuery q)
    {
        List<UserForAdmin> userList;
        (userList, _) = _userDao.GetUserList(q);
        return userList;
    }

    public AdminUserDetailModel PopulateUserDetailModel(AdminUserDetailQuery q)
    {
        AdminUserDetailModel adminUserDetailModel = new()
        {
            PageNumber = q.PageNumber,
            PageSize = q.PageSize,
            SearchKeyword = q.SearchKeyword,
            SearchTarget = q.SearchTarget,
            DetailType = q.DetailType,
            Id = q.Id
        };

        if (q.DetailType == "Post")
        {
            (adminUserDetailModel.PostList, adminUserDetailModel.TotalRowNum) = _postDao.GetPostListByUserId(q);
        }
        else if (q.DetailType == "Comment")
        {
            (adminUserDetailModel.CommentList, adminUserDetailModel.TotalRowNum) = _commentDao.GetCommentListByUserId(q);
        }

        return adminUserDetailModel;
    }

    public CommonResponseModel<string> CreateUser(UserAddParamter p)
    {
        if(p.IsEmpty())
        {
            return new CommonResponseModel<string>()
            {
                StatusCode = 400,
                Data = "입력하지 않은 정보가 있습니다."
            };
        }
        p.Password = _userEncryption.EncryptUserPassword(p.Password);
        return _userDao.CreateUser(p);
    }

    public string SuspendUser(UserSuspendParameter p)
    {
        CommonResponseModel<string> result = _userDao.SuspendUser(p);
        string jsonSerializedResult = JsonSerializer.Serialize(result);
        return jsonSerializedResult;
    }
}
