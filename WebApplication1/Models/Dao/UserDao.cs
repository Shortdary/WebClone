using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication1.Models.Dao
{
    public class UserDao : DBHelper
    {
        public User? GetUserByLoginCredentials(UserLoginCredentials ulc)
        {
            User? user;
            using SqlConnection conn = GetConnection();
            {
                SqlCommand cmd = new("spSelectUserByLoginCredentials", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@login_id", string.IsNullOrEmpty(ulc.LoginId) ? DBNull.Value : ulc.LoginId);
                conn.Open();

                DataTable dt = new();
                SqlDataAdapter da = new(cmd);
                da.Fill(dt);

                DataRow? row = dt.Rows.Cast<DataRow>().FirstOrDefault();
                if (row is null)
                {
                    user = null;
                }
                else
                {
                    user = new User()
                    {
                        UserId = row.Field<int>("id"),
                        Password = row.Field<string>("password")!,
                        Nickname = row.Field<string>("nickname")!,
                        Roles = row.Field<string>("roles")!
                    };
                }

            }
            return user;
        }

        public (List<UserForAdmin>, int) GetUserList(AdminUserListQuery q)
        {
            List<UserForAdmin> userList;
            int totalRowNum;

            using SqlConnection conn = GetConnection();
            SqlCommand cmd = new("spSelectAllUsers", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@page_number", q.PageNumber);
            cmd.Parameters.AddWithValue("@page_size", q.PageSize);
            cmd.Parameters.AddWithValue("@search_target", q.SearchTarget ?? "");
            cmd.Parameters.AddWithValue("@search_keyword", q.SearchKeyword is null ? DBNull.Value : q.SearchKeyword);
            conn.Open();

            DataSet ds = new();
            SqlDataAdapter da = new(cmd);
            da.Fill(ds);

            userList = ds.Tables[0].AsEnumerable().Select(row =>
                    new UserForAdmin
                    {
                        UserId = row.Field<int>("id"),
                        Nickname = row.Field<string>("nickname"),
                    }).ToList();
            totalRowNum = ds.Tables[1].Select().FirstOrDefault()!.Field<int>("total_row_count");
            return (userList, totalRowNum);
        }


        public CommonResponseModel<string> CreateUser(UserAddParamter p)
        {
            CommonResponseModel<string> result = new();
            try
            {
                using SqlConnection conn = GetConnection();
                SqlCommand cmd = new("spInsertUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@login_id", p.LoginId);
                cmd.Parameters.AddWithValue("@password", p.Password);
                cmd.Parameters.AddWithValue("@nickname", p.Nickname);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        result.StatusCode = 400;
                        if (ex.Message.Contains("UQ__User__C2C971DAB52FC2FB"))
                        {
                            result.Data = "중복된 아이디가 존재합니다.";
                        }
                        else if (ex.Message.Contains("UQ__User__5CF1C59B4F66B7DB"))
                        {
                            result.Data = "중복된 닉네임이 존재합니다.";
                        }
                        break;
                    default:
                        result.StatusCode = 500;
                        result.Data = "에러 발생";
                        break;
                }
            }
            return result;
        }

        public string ResetPassword()
        {
            return "";
        }
    }
}
