using Microsoft.Identity.Client;
using WebApplication1.Models.Dao;

namespace WebApplication1.Models;


public class UserService
{
    private readonly UserDao _userDao = new();

    /// <summary>
    /// ID와 PWD를 받아 유저를 검증하는 함수
    /// </summary>
    /// <param name="ulc">유저 로그인 정보</param>
    /// <returns>
    /// 일치하는 유저가 있을경우 User 인스턴스
    /// 없을 시 null
    /// </returns>
    public User? VerifyUser(UserLoginCredentials ulc)
    {
        User? user = _userDao.GetUserByLoginCredentials(ulc); ;

        if (user is null)
        {
            return null;
        }
        else
        {
            return user;
        }
    }
}
