using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToUs.Models
{
    public static partial class AppConfig
    {
        public static class AdminMode
        {
            public class ExcelPath
            {
                public string Path { get; set; }
                public bool IsChoosed { get; set; }
                //public string Type { get; set; }

                public ExcelPath(string path)
                {
                    Path = path.Trim();
                    IsChoosed = true;
                    //Type = type;
                }
            }
        }
    }
}