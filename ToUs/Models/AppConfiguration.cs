using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToUs.Models
{
    public static class AppConfiguration
    {
        private static User _user;
        private static User __userDetail;
        private static string connectionString;
        private static List<DataScheduleRow> _selectedRows = new List<DataScheduleRow>();

        public static User User
        {
            get
            {
                if (_user != null)
                    return _user;
                return null;
            }
            set
            {
                _user = value;
            }
        }

        public static User UserDetail
        {
            get
            {
                if (__userDetail != null)
                    return __userDetail;
                return null;
            }
            set
            {
                __userDetail = value;
            }
        }

        public static string ConnectionString
        {
            get
            {
                if (connectionString != null)
                    return connectionString;
                return null;
            }
            set
            {
                connectionString = value;
            }
        }

        public static List<DataScheduleRow> SelectedRows
        {
            get
            {
                if (_selectedRows != null)
                    return _selectedRows;
                return null;
            }
            set
            {
                _selectedRows = value;
            }
        }
    }
}