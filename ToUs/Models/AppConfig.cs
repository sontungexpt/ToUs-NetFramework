using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToUs.ViewModel.StartViewModel.ComponentAuthenticateViewModel;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ToUs.Models
{
    public static partial class AppConfig
    {
        //Static fields and properties:
        private static Random _rand = new Random();

        private static string _codeSent;
        private static string _connectionString;
        private static List<DataScheduleRow> _selectedRows = new List<DataScheduleRow>();
        private static List<DataScheduleRow> _allRows = new List<DataScheduleRow>();
        private static string _userEmail;
        private static UserDetail _userDetail;
        private static TimeTableInfo _timeTableInfo = new TimeTableInfo();

        //Static classes:

        public static class TempSignUpDetail
        {
            public static string FirstName;
            public static string LastName;
            public static string Email;
            public static string Password;
            public static string ConfirmPassword;

            public static void DeleteTempDetail()
            {
                FirstName = LastName = Email = Password = ConfirmPassword = null;
            }
        }

        public static TimeTableInfo TimeTableInfo
        {
            get
            {
                if (_timeTableInfo == null)
                    _timeTableInfo = new TimeTableInfo();
                return _timeTableInfo;
            }
            set
            {
                _timeTableInfo = value;
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
                if (_connectionString != null)
                    return _connectionString;
                return null;
            }
            set
            {
                _connectionString = value;
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

        public static Random Rand
        {
            get { return _rand; }
            set { _rand = value; }
        }

        public static string CodeSent
        {
            get { return _codeSent; }
            set { _codeSent = value; }
        }

        //Static funtions:
        public static bool IsValidEmailAddress(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}