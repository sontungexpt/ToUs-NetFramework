using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToUs.Utilities;

namespace ToUs.ViewModel.ManualViewViewModel
{
    public class ManualAddViewViewModel : ViewModelBase
    {
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

        public List<int> IsLabList = new List<int> { 0, 1 };

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

        public List<string> HTGDList = new List<string> { "ĐA", "KLTN", "LT", "TTTN" };

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

        public List<string> DayInWeekList = new List<string> { "2", "3", "4", "5", "6", "7", "*" };

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

        public List<string> FrequencyList = new List<string> { "1", "2" };

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

        public List<string> SemesterList = new List<string> { "1", "2", "Hè" };

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

        public List<string> SystemList = new List<string> { "CLC", "CQUI", "KSTN", "BCU", "CNTN", "CTTT", "CB2CQ" };

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

        public List<string> FacultyList = new List<string> { "HTTT", "MMT&TT", "KTMT", "BMTL", "CNPM", "P.DTDH", "HTTT", "KHMT", "KTTT", "TTNN" };

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

        public List<string> LanguageList = new List<string> { "EN", "VN", "JP" };

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

        //Contructor:
        public ManualAddViewViewModel()
        {
            SaveDataCommand = new RelayCommand(SaveData);
        }

        private void SaveData(object obj)
        {
            MessageBox.Show(SubjectId);
            MessageBox.Show(IsLab.ToString());
            MessageBox.Show(DayInWeek);
            MessageBox.Show(Faculty);
            MessageBox.Show(System);
        }
    }
}