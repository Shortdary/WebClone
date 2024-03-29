using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models.Common;

namespace WebApplication1.Models.Dao
{
    public class UserDao: DBHelper
    {
        private readonly CopycatContext _dbContext = new();

        public User? GetUserByLoginCredentials(UserLoginCredentials ulc)
        {
            User? user;
            using SqlConnection conn = GetConnection();
            {
                SqlCommand cmd = new("spSelectUserByLoginCredentials", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@login_id", ulc.LoginId);
                cmd.Parameters.AddWithValue("@password", ulc.Password);
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
                        Nickname = row.Field<string>("nickname")!
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
            cmd.Parameters.AddWithValue("@search_keyword", q.SearchKeyword ?? "");
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
    }
}
