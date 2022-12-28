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
using ToUs.Models;
using ToUs.Utilities;

namespace ToUs.ViewModel.ScheduleViewModel
{
    internal class NormalScheduleViewModel : ViewModelBase
    {
        private ObservableCollection<DataScheduleRow> _dataRows;

        public ICollectionView DataRowsView { get; }

        private string _textFilter = string.Empty;

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
            DataRows = new ObservableCollection<DataScheduleRow>(DataSupporter.GetAllDataRows());
            DataRowsView = CollectionViewSource.GetDefaultView(DataRows);
            DataRowsView.Filter = FilterByNames;
            DataRowsView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(DataScheduleRow.Subject.Name)));
        }

        private bool FilterByNames(object obj)
        {
            if (obj is DataScheduleRow dataRow)
            {
                return dataRow.Class.Id.Contains(TextFilter) ||
                    dataRow.Subject.Name.Contains(TextFilter) ||
                    dataRow.Teacher.Name.Contains(TextFilter);
            }
            return false;
        }
    }
}