using System.Data.SqlClient;

namespace WebApplication1.Models.Common
{
    public class DBHelper
    {
        private static string SERVER = "DESKTOP-AEPN1UJ";
        private static string UID = "sa";
        private static string PWD = "RPSsql12345";
        private static string DATABASE = "Copycat";

        //private string CONNECTIONSTRING = $"server={SERVER};uid={UID};pwd={PWD};database={DATABASE};TrustServerCertificate=True";
        private string CONNECTIONSTRING = $"Data Source={SERVER};Initial Catalog={DATABASE};Integrated Security = true";

        private SqlConnection? connection { get; set; }

        public SqlConnection GetConnection()
        {
            if (connection != null)
            {
                return connection;
            }
            else
            {
                return new SqlConnection(CONNECTIONSTRING);
            }
        }

    }
}
