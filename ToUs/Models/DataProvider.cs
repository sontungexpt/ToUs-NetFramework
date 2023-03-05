namespace ToUs.Models
{
    public class DataProvider
    {
        private static DataProvider _instance;

        public static DataProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataProvider();
                return _instance;
            }
        }

        public TOUSEntities entities { get; set; }

        public DataProvider()
        {
            entities = new TOUSEntities();
        }
    }
}