using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;

namespace ToUs.ViewModel.PreviewViewModel
{
    public class PreviewViewModel : ViewModelBase
    {
        public ICommand SaveCommand { get; set; }

        public PreviewViewModel()
        {
            SaveCommand = new RelayCommand(SaveTimeTable);
        }

        private void SaveTimeTable(object obj)
        {
            DataQuery.CreateTimetable(AppConfig.TimeTableInfo.Name, AppConfig.UserDetail.Id);
        }
    }
}