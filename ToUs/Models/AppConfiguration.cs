using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToUs.ViewModel.StartViewModel.ComponentAuthenticateViewModel;

namespace ToUs.Models
{
    public static class AppConfiguration
    {
        private static string _userEmail;
        private static UserDetail _userDetail;
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
                return null;
            }
            set
            {
                _allRows = value;
            }
        }

        public static string UserEmail
        {
            get { return _userEmail; }
            set { _userEmail = value; }
        }

        public static UserDetail UserDetail
        {
            get { return _userDetail; }
            set { _userDetail = value; }
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