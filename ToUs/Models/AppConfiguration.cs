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
        private static User _userDetail;
        private static string connectionString;
        private static List<DataScheduleRow> _selectedRows = new List<DataScheduleRow>();
        private static List<DataScheduleRow> _allRows = new List<DataScheduleRow>();
        private static string _currentExcelPath = null; 
        
        public static string CurrentExcelPath
        {
            get 
            { 
               return _currentExcelPath;
            }
            set { _currentExcelPath = value; }
        }

        public static List<DataScheduleRow> AllRows
        {
            get
            {
                if (_allRows != null)
                    return _allRows;
                throw new ArgumentNullException(nameof(AllRows));
            }
            set
            {
                _allRows = value;
            }
        }

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
                if (_userDetail != null)
                    return _userDetail;
                return null;
            }
            set
            {
                _userDetail = value;
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