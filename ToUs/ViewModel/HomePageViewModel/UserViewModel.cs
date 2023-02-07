using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;


namespace ToUs.ViewModel.HomePageViewModel
{
    public class UserViewModel : ViewModelBase
    {
        //Fields:
        private int _selectedSchoolYear; //2 cái này để binding lựa chọn.
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

        //Properties:
        public List<int> SchoolYears = new List<int>() //2 cái List cho Combobox
        {
            2020,
            2021, 
            2022, 
            2023,
            2024
        }; 
        public List<string> Semesters = new List<string>()
        {
            "HK1",
            "HK2",
            "HK hè"
        };

        public string TableName
        {
            get { return _tableName; }
            set
            {
                _tableName = value;
                OnPropertyChanged(nameof(TableName));   
            }
        }

        public int SelectedSchoolYear
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
        public ICommand ClearChoicesCommand { get; set; }
        public ICommand CheckedAllCommand { get; set; }
        public ICommand UnCheckedAllCommand { get; set; }

        //Constructor:
        public UserViewModel()
        {

            MondayIsChecked = TuesdayIsChecked = WednesdayIsChecked = ThursdayIsChecked = FridayIsChecked = SaturdayIsChecked = AllIsChecked = false;

            SaveTableCommand = new RelayCommand(SaveTabe);
            ClearChoicesCommand = new RelayCommand(ClearChoices);
            CheckedAllCommand = new RelayCommand(CheckedAll);
            UnCheckedAllCommand = new RelayCommand(UnCheckedAll);
        }

        private void UnCheckedAll(object obj)
        {
            MondayIsChecked = TuesdayIsChecked = WednesdayIsChecked = ThursdayIsChecked = FridayIsChecked = SaturdayIsChecked = false;
        }

        private void CheckedAll(object obj)
        {
            MondayIsChecked = TuesdayIsChecked = WednesdayIsChecked = ThursdayIsChecked = FridayIsChecked = SaturdayIsChecked = true;
        }

        private void ClearChoices(object obj)
        {
            throw new NotImplementedException();
        }

        private void SaveTabe(object obj)
        {
            MessageBox.Show(MondayIsChecked.ToString());
            MessageBox.Show(TuesdayIsChecked.ToString());
            MessageBox.Show(WednesdayIsChecked.ToString());
            MessageBox.Show(ThursdayIsChecked.ToString());
            MessageBox.Show(FridayIsChecked.ToString());
            MessageBox.Show(SaturdayIsChecked.ToString());
        }
    }
}
