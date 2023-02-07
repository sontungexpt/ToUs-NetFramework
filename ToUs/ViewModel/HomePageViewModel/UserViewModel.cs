using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using ToUs.Models;
using ToUs.Utilities;
using System.Linq;

namespace ToUs.ViewModel.HomePageViewModel
{
    public class UserViewModel : ViewModelBase
    {
        //Fields:
        private string _selectedSchoolYear; //2 cái này để binding lựa chọn.

        private string _selectedSemester;
        private string _tableName;
        private string _chosenSubjectID;
        private string _subjectIDErrorMessage;
        private string _chooseDayErrorMessage;

        private bool _isUser;
        private bool _mondayIsChecked;
        private bool _tuesdayIsChecked;
        private bool _wednesdayIsChecked;
        private bool _thursdayIsChecked;
        private bool _fridayIsChecked;
        private bool _saturdayIsChecked;
        private bool _allIsChecked;

        private List<string> _schoolYears;
        private List<string> _semesters;

        //Properties:
        public List<string> SchoolYears
        {
            get { return _schoolYears; }
            set
            {
                _schoolYears = value;
            }
        }

        public List<string> Semesters
        {
            get { return _semesters; }
            set
            {
                _semesters = value;
            }
        }

        public string TableName
        {
            get { return _tableName; }
            set
            {
                _tableName = value;
                OnPropertyChanged(nameof(TableName));
            }
        }

        public string SelectedSchoolYear
        {
            get { return _selectedSchoolYear; }
            set
            {
                _selectedSchoolYear = value;
                OnPropertyChanged(nameof(SelectedSchoolYear));
            }
        }

        public string SelectedSemester
        {
            get { return _selectedSemester; }
            set
            {
                _selectedSemester = value;
                OnPropertyChanged(nameof(SelectedSemester));
            }
        }

        public string ChosenSubjectID
        {
            get { return _chosenSubjectID; }
            set
            {
                _chosenSubjectID = value;
                OnPropertyChanged(nameof(ChosenSubjectID));
            }
        }

        public string SubjectIDErrorMessage
        {
            get { return _subjectIDErrorMessage; }
            set
            {
                _subjectIDErrorMessage = value;
                OnPropertyChanged(nameof(SubjectIDErrorMessage));
            }
        }

        public string ChooseDayErrorMessage
        {
            get { return _chooseDayErrorMessage; }
            set
            {
                _chooseDayErrorMessage = value;
                OnPropertyChanged(nameof(ChooseDayErrorMessage));
            }
        }

        public bool IsUser
        {
            get { return _isUser; }
            set
            {
                _isUser = value;
                OnPropertyChanged(nameof(IsUser));
            }
        }

        public bool MondayIsChecked
        {
            get { return _mondayIsChecked; }
            set
            {
                _mondayIsChecked = value;
                OnPropertyChanged(nameof(MondayIsChecked));
            }
        }

        public bool TuesdayIsChecked
        {
            get { return _tuesdayIsChecked; }
            set
            {
                _tuesdayIsChecked = value;
                OnPropertyChanged(nameof(TuesdayIsChecked));
            }
        }

        public bool WednesdayIsChecked
        {
            get { return _wednesdayIsChecked; }
            set
            {
                _wednesdayIsChecked = value;
                OnPropertyChanged(nameof(WednesdayIsChecked));
            }
        }

        public bool ThursdayIsChecked
        {
            get { return _thursdayIsChecked; }
            set
            {
                _thursdayIsChecked = value;
                OnPropertyChanged(nameof(ThursdayIsChecked));
            }
        }

        public bool FridayIsChecked
        {
            get { return _fridayIsChecked; }
            set
            {
                _fridayIsChecked = value;
                OnPropertyChanged(nameof(FridayIsChecked));
            }
        }

        public bool SaturdayIsChecked
        {
            get { return _saturdayIsChecked; }
            set
            {
                _saturdayIsChecked = value;
                OnPropertyChanged(nameof(SaturdayIsChecked));
            }
        }

        public bool AllIsChecked
        {
            get { return _allIsChecked; }
            set
            {
                _allIsChecked = value;
                OnPropertyChanged(nameof(AllIsChecked));
            }
        }

        //Commands:
        public ICommand SaveTableCommand { get; set; }

        public ICommand CheckedAllCommand { get; set; }
        public ICommand UnCheckedAllCommand { get; set; }
        public ICommand ClearAllTableInfoCommand { get; set; }

        //Constructor:
        public UserViewModel()
        {
            if (AppConfig.UserEmail == null)
                IsUser = false;
            else
                IsUser = true;

            MondayIsChecked =
            TuesdayIsChecked =
            WednesdayIsChecked =
            ThursdayIsChecked =
            FridayIsChecked =
            SaturdayIsChecked =
            AllIsChecked = false;

            SchoolYears = DataQuery.GetYears();
            Semesters = DataQuery.GetSemesters();
            SelectedSchoolYear = AppConfig.TimeTableInfo.Year.ToString();
            SelectedSemester = AppConfig.TimeTableInfo.Semester.ToString();
            TableName = AppConfig.TimeTableInfo.Name;

            SaveTableCommand = new RelayCommand(CreateTabe);
            CheckedAllCommand = new RelayCommand(CheckedAll);
            UnCheckedAllCommand = new RelayCommand(UnCheckedAll);
            ClearAllTableInfoCommand = new RelayCommand(ClearAllTableInfo);
        }

        private void ClearAllTableInfo(object obj)
        {
            TableName = ChosenSubjectID = ChooseDayErrorMessage = SubjectIDErrorMessage = string.Empty;
            SelectedSemester = SelectedSchoolYear = null;
            MondayIsChecked = TuesdayIsChecked = WednesdayIsChecked = ThursdayIsChecked = FridayIsChecked = SaturdayIsChecked = AllIsChecked = false;
        }

        private void UnCheckedAll(object obj)
        {
            MondayIsChecked = TuesdayIsChecked = WednesdayIsChecked = ThursdayIsChecked = FridayIsChecked = SaturdayIsChecked = false;
        }

        private void CheckedAll(object obj)
        {
            MondayIsChecked = TuesdayIsChecked = WednesdayIsChecked = ThursdayIsChecked = FridayIsChecked = SaturdayIsChecked = true;
        }

        private void CreateTabe(object obj)
        {
            bool validDayChecked, validSubjectID;
            validDayChecked = validSubjectID = true;
            if (MondayIsChecked == false && TuesdayIsChecked == false && WednesdayIsChecked == false && ThursdayIsChecked == false
               && FridayIsChecked == false && SaturdayIsChecked == false)
            {
                ChooseDayErrorMessage = "* Vui lòng chọn ít nhất 1 ngày để xếp thời khóa biểu *";
                validDayChecked = false;
            }

            if (string.IsNullOrWhiteSpace(ChosenSubjectID))
            {
                SubjectIDErrorMessage = "* Vui lòng nhập ít nhất 1 mã môn muốn học *";
                validSubjectID = false;
            }

            if (validDayChecked && validSubjectID)
            {
                //Save to database code goes here
                MessageBox.Show("Luu thong tin thanh cong!");
            }
            AppConfig.TimeTableInfo.Name = TableName;
            AppConfig.TimeTableInfo.Semester = int.Parse(SelectedSemester);
            AppConfig.TimeTableInfo.Year = int.Parse(SelectedSchoolYear);
            AppConfig.AllRows = DataQuery.GetAllDataRows(AppConfig.TimeTableInfo.Year, AppConfig.TimeTableInfo.Semester);
            MessageBox.Show("Tạo thời khoá biểu thành công");
        }
    }
}