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
        //Fields:
        private bool _isUser;
        private bool _oppositeIsUser;

        //Properties
        public string TableName { get; set; }

        public bool IsUser
        {
            get { return _isUser; }
            set
            {
                _isUser = value;
                OnPropertyChanged(nameof(IsUser));
            }
        }

        public bool OppositeIsUser
        {
            get { return _oppositeIsUser; }
            set
            {
                _oppositeIsUser = value;
                OnPropertyChanged(nameof(OppositeIsUser));
            }
        }

        //Commands:
        public ICommand SaveCommand { get; set; }
        public ICommand SwitchToSignInCommand { get; set; }

        //Constructor
        public PreviewViewModel()
        {
            if(AppConfig.UserEmail == null)
            {
                IsUser = false;
                OppositeIsUser = true;
            }
            else
            {
                IsUser = true;
                OppositeIsUser = false;
            }

            SwitchToSignInCommand = MainViewViewModel.ChangeMainViewIsViewVisibleCommand;
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
            catch (SaveChangesException)
            {
                MessageBox.Show("Không thể lưu thời khoá biểu");
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể lưu thời khoá biểu");
            }
        }
    }
}