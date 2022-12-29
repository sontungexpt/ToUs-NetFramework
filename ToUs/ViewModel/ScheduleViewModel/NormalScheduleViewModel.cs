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
            if (AppConfiguration.CurrentExcelPath != ExcelReader.FilePath)
            {
                AppConfiguration.CurrentExcelPath = ExcelReader.FilePath;
                AppConfiguration.AllRows = DataSupporter.GetAllDataRows();
            }
            if (!String.IsNullOrEmpty(AppConfiguration.CurrentExcelPath))
            {
                DataRows = new ObservableCollection<DataScheduleRow>(AppConfiguration.AllRows);
                DataRowsView = CollectionViewSource.GetDefaultView(DataRows);
                //DataRowsView.Filter = FilterByNames;
                //DataRowsView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(DataScheduleRow.Subject.Name)));
            }
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