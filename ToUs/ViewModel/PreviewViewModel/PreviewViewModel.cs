using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;

namespace ToUs.ViewModel.PreviewViewModel
{
    public class PreviewViewModel : ViewModelBase
    {
        public string TableName { get; set; }

        public ICommand SaveCommand { get; set; }

        public PreviewViewModel()
        {
            SaveCommand = new RelayCommand(SaveTimeTable);
            TableName = AppConfig.TimeTableInfo.Name;
        }

        private void SaveTimeTable(object obj)
        {
            try
            {
                DataQuery.SaveTimeTable(AppConfig.TimeTableInfo.Name,
                                        AppConfig.UserDetail.Id,
                                        AppConfig.TimeTableInfo.SelectedRows);
                AppConfig.TimeTableInfo.Refresh();
                AppConfig.AllRows = new List<DataScheduleRow>();

                MessageBox.Show("Lưu thời khoá biểu thành công");
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