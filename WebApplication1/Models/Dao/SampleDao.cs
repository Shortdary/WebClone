using Microsoft.EntityFrameworkCore;
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
            using(var db = new CopycatContext())
            {
                var comments = db.Comments.ToList();


                rtnVal = comments[0].Comment1;
            }
            return rtnVal;
        }
    }
}
