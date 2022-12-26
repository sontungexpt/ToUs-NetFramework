//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ToUs.Models
//{
//    internal class DataProvider
//    {
//        private static DataProvider _instance;

//        public static DataProvider Instance
//        {
//            get
//            {
//                if (_instance == null)
//                    _instance = new DataProvider();
//                return _instance;
//            }
//            private set { }
//        }

//        public TOUSEntities entities { get; set; }

//        public DataProvider()
//        {
//            entities = new TOUSEntities();
//        }
//    }
//}