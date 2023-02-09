using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;
using static ToUs.Models.AppConfig.AdminMode;

namespace ToUs.ViewModel.AdminViewModel
{
    public class TableControlViewModel : ViewModelBase
    {
        private bool _addClassIsViewVisible;

        public bool AddClassIsViewVisible
        {
            get { return _addClassIsViewVisible; }
            set
            {
                _addClassIsViewVisible = value;
                OnPropertyChanged(nameof(AddClassIsViewVisible));
            }
        }

        private void CancelAddManual(object obj)
        {
            AddClassIsViewVisible = false;
        }

        private void OpenAddManual(object obj)
        {
            AddClassIsViewVisible = true;
        }

        private ObservableCollection<ExcelPath> _paths;
        private string _tableName;
        private object _currentView;

        public ObservableCollection<ExcelPath> Paths
        {
            get { return _paths; }
            set
            {
                _paths = value;
                OnPropertyChanged(nameof(Paths));
            }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; OnPropertyChanged(nameof(TableName)); }
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand LoadExcelCommand { get; set; }
        public ICommand OpenAddManualCommand { get; set; }

        private async void LoadExcel(object obj)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.FileName = "Excel"; // Default file name
            openFileDialog.DefaultExt = ".xlsx"; // Default file extension
            openFileDialog.Filter = "XLS Worksheet|*.csv|Excel Workbook|*.xlsx";
            openFileDialog.FilterIndex = 2;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                //_paths = openFileDialog.FileNames;
                List<ExcelPath> excelPathChoosed = new List<ExcelPath>();
                foreach (string path in openFileDialog.FileNames)
                {
                    excelPathChoosed.Add(new ExcelPath(Path.GetFileName(path)));
                    Paths = new ObservableCollection<ExcelPath>(excelPathChoosed);
                }

                foreach (string path in openFileDialog.FileNames)
                {
                    //Stopwatch clock = Stopwatch.StartNew();
                    //clock.Start();

                    if (ExcelReader.Open(path, "Đại trà"))
                    {
                        ExcelReader.FormatExcelDatas();
                        if (ExcelImportDB.Connect())
                        {
                            await ExcelImportDB.ImportToDBAsync();
                        }
                        else
                            MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu");
                    }

                    //clock.Stop();
                    //TimeSpan ts = clock.Elapsed;
                    //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    //ts.Hours, ts.Minutes, ts.Seconds,
                    //ts.Milliseconds / 10);
                    //MessageBox.Show("RunTime " + elapsedTime);
                }
                MessageBox.Show("File đã được load thành công");
            }
        }

        //Fields and properties:
        private string _subjectId;

        public string SubjectId
        {
            get { return _subjectId; }
            set
            {
                _subjectId = value;
                OnPropertyChanged(nameof(SubjectId));
            }
        }

        private string _classId;

        public string ClassId
        {
            get { return _classId; }
            set
            {
                _classId = value;
                OnPropertyChanged(nameof(ClassId));
            }
        }

        private string _subjectName;

        public string SubjectName
        {
            get { return _subjectName; }
            set
            {
                _subjectName = value;
                OnPropertyChanged(nameof(SubjectName));
            }
        }

        private string _teacherId;

        public string TeacherId
        {
            get { return _teacherId; }
            set
            {
                _teacherId = value;
                OnPropertyChanged(nameof(TeacherId));
            }
        }

        private string _teacherName;

        public string TeacherName
        {
            get { return _teacherName; }
            set
            {
                _teacherName = value;
                OnPropertyChanged(nameof(TeacherName));
            }
        }

        private int _numberOfStudents;

        public int NumberOfStudents
        {
            get { return _numberOfStudents; }
            set
            {
                _numberOfStudents = value;
                OnPropertyChanged(nameof(NumberOfStudents));
            }
        }

        private int _numberOfDigits;

        public int NumberOfDigits
        {
            get { return _numberOfDigits; }
            set
            {
                _numberOfDigits = value;
                OnPropertyChanged(nameof(NumberOfDigits));
            }
        }

        private int _isLab;

        public int IsLab
        {
            get { return _isLab; }
            set
            {
                _isLab = value;
                OnPropertyChanged(nameof(IsLab));
            }
        }

        private List<string> _isLabList;

        public List<string> IsLabList
        {
            get { return _isLabList; }
            set
            {
                _isLabList = value;
                OnPropertyChanged(nameof(IsLabList));
            }
        }

        private string _hTGD;

        public string HTGD
        {
            get { return _hTGD; }
            set
            {
                _hTGD = value;
                OnPropertyChanged(nameof(HTGD));
            }
        }

        private List<string> _hTGDList;

        public List<string> HTGDList
        {
            get { return _hTGDList; }
            set
            {
                _hTGDList = value;
                OnPropertyChanged(nameof(HTGDList));
            }
        }

        private string _dayInWeek;

        public string DayInWeek
        {
            get { return _dayInWeek; }
            set
            {
                _dayInWeek = value;
                OnPropertyChanged(nameof(DayInWeek));
            }
        }

        private List<string> _dayInWeekList;

        public List<string> DayInWeekList
        {
            get { return _dayInWeekList; }
            set
            {
                _dayInWeekList = value;
                OnPropertyChanged(nameof(DayInWeekList));
            }
        }

        private string _lesson;

        public string Lesson
        {
            get { return _lesson; }
            set
            {
                _lesson = value;
                OnPropertyChanged(nameof(Lesson));
            }
        }

        private string _frequency;

        public string Frequency
        {
            get { return _frequency; }
            set
            {
                _frequency = value;
                OnPropertyChanged(nameof(Frequency));
            }
        }

        private List<string> _frequencyList;

        public List<string> FrequencyList
        {
            get { return _frequencyList; }
            set
            {
                _frequencyList = value;
                OnPropertyChanged(nameof(FrequencyList));
            }
        }

        private string _room;

        public string Room
        {
            get { return _room; }
            set
            {
                _room = value;
                OnPropertyChanged(nameof(Room));
            }
        }

        private string _semester;

        public string Semester
        {
            get { return _semester; }
            set
            {
                _semester = value;
                OnPropertyChanged(nameof(Semester));
            }
        }

        private List<string> _semesterList;

        public List<string> SemesterList
        {
            get { return _semesterList; }
            set
            {
                _semesterList = value;
                OnPropertyChanged(nameof(SemesterList));
            }
        }

        private string _year;

        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        private string _system;

        public string System
        {
            get { return _system; }
            set
            {
                _system = value;
                OnPropertyChanged(nameof(System));
            }
        }

        private List<string> _systemList;

        public List<string> SystemList
        {
            get { return _systemList; }
            set
            {
                _systemList = value;
                OnPropertyChanged(nameof(SystemList));
            }
        }

        private string _faculty;

        public string Faculty
        {
            get { return _faculty; }
            set
            {
                _faculty = value;
                OnPropertyChanged(nameof(Faculty));
            }
        }

        private List<string> _facultyList;

        public List<string> FacultyList
        {
            get { return _facultyList; }
            set
            {
                _facultyList = value;
                OnPropertyChanged(nameof(FacultyList));
            }
        }

        private string _language;

        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        private List<string> _languageList;

        public List<string> LanguageList
        {
            get { return _languageList; }
            set
            {
                _languageList = value;
                OnPropertyChanged(nameof(LanguageList));
            }
        }

        private string _note;

        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged(nameof(Note));
            }
        }

        private DateTime _beginDate;

        public DateTime BeginDate
        {
            get { return _beginDate; }
            set
            {
                _beginDate = value;
                OnPropertyChanged(nameof(BeginDate));
            }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        //Commands:
        public ICommand SaveDataCommand { get; set; }

        public ICommand CancelAddManualCommand { get; set; }

        private void SaveData(object obj)
        {
            MessageBox.Show(SubjectId);
            MessageBox.Show(SubjectName);
            MessageBox.Show(IsLab.ToString());
            MessageBox.Show(DayInWeek);
            MessageBox.Show(Faculty);
            MessageBox.Show(System);
        }

        public TableControlViewModel()
        {
            AddClassIsViewVisible = false;

            IsLabList = new List<string> { "0", "1" };
            HTGDList = new List<string> { "ĐA", "KLTN", "LT", "TTTN" };
            DayInWeekList = new List<string> { "2", "3", "4", "5", "6", "7", "*" };
            FrequencyList = new List<string> { "1", "2" };
            SemesterList = new List<string> { "1", "2", "Hè" };
            SystemList = new List<string> { "CLC", "CQUI", "KSTN", "BCU", "CNTN", "CTTT", "CB2CQ" };
            FacultyList = new List<string> { "HTTT", "MMT&TT", "KTMT", "BMTL", "CNPM", "P.DTDH", "HTTT", "KHMT", "KTTT", "TTNN" };
            LanguageList = new List<string> { "EN", "VN", "JP" };

            LoadExcelCommand = new RelayCommand(LoadExcel);
            OpenAddManualCommand = new RelayCommand(OpenAddManual);
            SaveDataCommand = new RelayCommand(SaveData);
            CancelAddManualCommand = new RelayCommand(CancelAddManual);
        }
    }
}