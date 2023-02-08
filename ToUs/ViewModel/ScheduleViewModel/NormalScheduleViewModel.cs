using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using ToUs.Models;
using ToUs.Utilities;

namespace ToUs.ViewModel.ScheduleViewModel
{
    internal class NormalScheduleViewModel : ViewModelBase
    {
        private ObservableCollection<DataScheduleRow> _dataRows;
        private string _textFilter = string.Empty;

        public ICollectionView DataRowsView { get; }

        public ICommand CheckItemCommand { get; set; }

        public string TextFilter
        {
            get { return _textFilter; }
            set
            {
                _textFilter = value;
                OnPropertyChanged(nameof(TextFilter));
                DataRowsView.Refresh();
            }
        }

        public ObservableCollection<DataScheduleRow> DataRows
        {
            get
            {
                if (_dataRows != null)
                {
                    return _dataRows;
                }
                return null;
            }
            set
            {
                _dataRows = value;
                OnPropertyChanged(nameof(DataRows));
            }
        }

        public NormalScheduleViewModel()
        {
            DataRows = new ObservableCollection<DataScheduleRow>(AppConfig.AllRows);
            DataRowsView = CollectionViewSource.GetDefaultView(DataRows);
            DataRowsView.Filter = FilterByNames;
        }

        private bool FilterByNames(object obj)
        {
            if (obj is DataScheduleRow dataRow)
            {
                return dataRow.Subject.Name.ToLower().Contains(TextFilter.ToLower()) ||
                    dataRow.Class.ClassId.ToLower().Contains(TextFilter.ToLower()) ||
                    dataRow.Teachers.Any(teacher => teacher.Name.ToLower().Contains(TextFilter.ToLower())) ||
                    dataRow.Subject.NumberOfDigits.ToString().Contains(TextFilter.ToLower()) ||
                    dataRow.Class.DayInWeek.ToLower().Contains(TextFilter.ToLower()) ||
                    dataRow.Class.Lession.ToLower().Contains(TextFilter.ToLower()) ||
                    dataRow.Class.System.ToLower().Contains(TextFilter.ToLower()) ||
                    dataRow.Subject.FacultyId.ToLower().Contains(TextFilter.ToLower()) ||
                    dataRow.Subject.HTGD.ToLower().Contains(TextFilter.ToLower()) ||
                    dataRow.Class.Frequency.ToString().Contains(TextFilter.ToLower());
            }

            return false;
        }
    }
}