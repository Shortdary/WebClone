using WebApplication1.Models.Dao;

namespace WebApplication1.Models.Service
{
    public class SampleService
    {
        private SampleDao sampleDao = new SampleDao();
        public string GetDbVersion()
        {
            return sampleDao.GetDbVersion();
        }
    }
}
