using System.Data.SqlClient;

namespace WebApplication1.Models.Common
{
    public class DBHelper
    {
        private readonly static string SERVER = "DESKTOP-AEPN1UJ";
        private readonly static string DATABASE = "Copycat";

        private readonly  string CONNECTIONSTRING = $"Data Source={SERVER};Initial Catalog={DATABASE};Integrated Security = true";

        private SqlConnection? Connection { get; set; }

        public SqlConnection GetConnection()
        {
            if (Connection != null)
            {
                return Connection;
            }
            else
            {
                return new SqlConnection(CONNECTIONSTRING);
            }
        }

    }
}
