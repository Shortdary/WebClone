using Dapper;
using System.Data;
using System.Linq;
using WebApplication1.Models.Common;

namespace WebApplication1.Models.Dao
{
    public class SampleDao: DBHelper
    {
        public string GetDbVersion()
        {
            string rtnVal = null;
            using(var conn = base.GetConnection())
            {
                string sql = "SELECT @@VERSION";
                var p = new DynamicParameters();

                conn.Open();

                rtnVal = conn.Query<string>(sql, p, commandType: CommandType.Text).FirstOrDefault();
            }

            return rtnVal;
        }
    }
}
