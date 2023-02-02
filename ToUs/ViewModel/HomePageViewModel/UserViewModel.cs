using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private bool[] _isCheckedArray;

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

        public bool[] IsCheckedArray
        {
            get { return _isCheckedArray; }
            set
            {
                _isCheckedArray = value;
                OnPropertyChanged(nameof(IsCheckedArray));
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

        //Commands:
        public ICommand SaveTableCommand { get; set; }

        //Constructor:
        public UserViewModel()
        {
            SaveTableCommand = new RelayCommand(SaveTabe);
        }

        private void SaveTabe(object obj)
        {
            for(int i = 0; i < 6; i++)
            {
                MessageBox.Show(IsCheckedArray[i].ToString());
            }
        }
    }
}
