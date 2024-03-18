using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models.Common;

namespace WebApplication1.Models.Dao
{
    public class UserDao: DBHelper
    {
        public User? GetUserByLoginCredentials(UserLoginCredentials ulc)
        {
            User? user;
            using var conn = GetConnection();
            {
                SqlCommand cmd = new("spSelectUserByLoginCredentials", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@login_id", ulc.LoginId));
                cmd.Parameters.Add(new SqlParameter("@password", ulc.Password));
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
    }
}
