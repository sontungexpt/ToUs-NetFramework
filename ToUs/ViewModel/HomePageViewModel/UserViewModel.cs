using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using ToUs.Models;
using ToUs.Utilities;
using System.Linq;
using System.Diagnostics;
using ToUs.View;

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
        private string _currenUserName;

        private bool _isUser;
        private bool _isAutomaticMode;
        private bool _isDoneCreateTable;
        private bool _mondayIsChecked;
        private bool _tuesdayIsChecked;
        private bool _wednesdayIsChecked;
        private bool _thursdayIsChecked;
        private bool _fridayIsChecked;
        private bool _saturdayIsChecked;
        private bool _allIsChecked;

        private List<string> _schoolYears;
        private List<string> _semesters;
        private List<TimeTable> _timeTables;

        //Properties:

        public List<string> SchoolYears
        {
            get { return _schoolYears; }
            set
            {
                _schoolYears = value;
                OnPropertyChanged(nameof(SchoolYears));
            }
        }

        public List<string> Semesters
        {
            get { return _semesters; }
            set
            {
                _semesters = value;
                OnPropertyChanged(nameof(Semesters));
            }
        }

        public List<TimeTable> TimeTables
        {
            get { return _timeTables; }
            set
            {
                _timeTables = value;
                OnPropertyChanged(nameof(TimeTables));
            }
        }

        public string CurrenUserName
        {
            get { return _currenUserName; }
            set
            {
                _currenUserName = value;
                OnPropertyChanged(nameof(CurrenUserName));
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

        public bool IsAutomaticMode
        {
            get { return _isAutomaticMode; }
            set
            {
                _isAutomaticMode = value;
                OnPropertyChanged(nameof(IsAutomaticMode));
            }
        }

        public bool IsDoneCreateTable
        {
            get { return _isDoneCreateTable; }
            set
            {
                _isDoneCreateTable = value;
                OnPropertyChanged(nameof(IsDoneCreateTable));
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
        public ICommand SwitchToPreviewCommand { get; set; }

        public ICommand GetDatasTableCommand { get; set; }
        public ICommand SwitchToNormalScheduleViewCommand { get; set; }

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
            IsDoneCreateTable = false;

            MondayIsChecked =
            TuesdayIsChecked =
            WednesdayIsChecked =
            ThursdayIsChecked =
            FridayIsChecked =
            SaturdayIsChecked =
            AllIsChecked = false;

            if (IsUser)
            {
                if (AppConfig.UserDetail != null)
                {
                    CurrenUserName = AppConfig.UserDetail.FirstName;
                    TimeTables = DataQuery.GetOldTimeTables(AppConfig.UserDetail.Id);
                }
            }

            Semesters = DataQuery.GetSemesters();
            SchoolYears = DataQuery.GetYears();
            if (AppConfig.TimeTableInfo.Year == 0)
                SelectedSemester = null;
            else
                SelectedSchoolYear = AppConfig.TimeTableInfo.Year.ToString();
            SelectedSemester = AppConfig.TimeTableInfo.Semester;
            TableName = AppConfig.TimeTableInfo.Name;

            SwitchToPreviewCommand = MainViewViewModel.PreviewCommand;
            SwitchToNormalScheduleViewCommand = MainViewViewModel.NormalScheduleCommand;
            SaveTableCommand = new RelayCommand(CreateTabe, CanCreateTable);
            CheckedAllCommand = new RelayCommand(CheckedAll);
            UnCheckedAllCommand = new RelayCommand(UnCheckedAll);
            ClearAllTableInfoCommand = new RelayCommand(ClearAllTableInfo);

            if (!String.IsNullOrEmpty(AppConfig.UserEmail))
                TimeTables = DataQuery.GetOldTimeTables(AppConfig.UserDetail.Id);
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

        private bool CanCreateTable(object arg)
        {
            if (string.IsNullOrWhiteSpace(SelectedSemester) ||
                string.IsNullOrWhiteSpace(SelectedSchoolYear) ||
                string.IsNullOrWhiteSpace(TableName))
                return false;
            return true;
        }

        private void CreateTabe(object obj)
        {
            bool validDayChecked, validSubjectID;
            validDayChecked = validSubjectID = true;
            if (IsAutomaticMode)
            {
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
                    using (var context = new TOUSEntities())
                    {
                        if (context.TimeTables.Any(table => table.Name == TableName))
                        {
                            MessageBox.Show("Tên thời khoá biểu đã tồn tại, vui lòng đặt tên khác");
                            TableName = null;
                        }
                        else
                        {
                            AppConfig.TimeTableInfo.Name = TableName;
                            AppConfig.TimeTableInfo.Semester = SelectedSemester;
                            AppConfig.TimeTableInfo.Year = int.Parse(SelectedSchoolYear);
                            AppConfig.AllRows = DataQuery.GetAllDataRows(AppConfig.TimeTableInfo.Year, AppConfig.TimeTableInfo.Semester);
                            //Saving automatic mode info goes here
                            MessageBox.Show("Đã lưu thông tin thời khoá biểu tự động.");
                            IsDoneCreateTable = true;
                        }
                    }
                }
            }
            else
            {
                using (var context = new TOUSEntities())
                {
                    if (context.TimeTables.Any(table => table.Name == TableName))
                    {
                        MessageBox.Show("Tên thời khoá biểu đã tồn tại, vui lòng đặt tên khác");
                        TableName = null;
                    }
                    else
                    {
                        AppConfig.TimeTableInfo.Name = TableName;
                        AppConfig.TimeTableInfo.Semester = SelectedSemester;
                        AppConfig.TimeTableInfo.Year = int.Parse(SelectedSchoolYear);
                        AppConfig.AllRows = DataQuery.GetAllDataRows(AppConfig.TimeTableInfo.Year, AppConfig.TimeTableInfo.Semester);
                        MessageBox.Show("Đã lưu thông tin thời khoá biểu thủ công.");
                        IsDoneCreateTable = true;
                    }
                }
            }
        }
    }
}