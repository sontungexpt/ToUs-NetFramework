using Microsoft.Xrm.Sdk;
using System;
using System.Windows;
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
            try
            {
                DataQuery.CreateTimetable(AppConfig.TimeTableInfo.Name, AppConfig.UserDetail.Id);
            }
            catch (SaveChangesException saveChangeException)
            {
                MessageBox.Show(saveChangeException.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}