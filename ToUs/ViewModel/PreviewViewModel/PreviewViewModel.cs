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
        private string _tableName = "Tên thời khoá biểu: ";

        //Properties
        public string TableName
        {
            get => _tableName;
            set => _tableName = "Tên thời khoá biểu: " + value;
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
            if (AppConfig.UserEmail == null)
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
            SaveCommand = new RelayCommand(SaveTimeTable, CanSaveTimeTable);
            TableName = AppConfig.TimeTableInfo.Name;
        }

        private bool CanSaveTimeTable(object arg)
        {
            if (AppConfig.TimeTableInfo.SelectedRows != null && AppConfig.TimeTableInfo.SelectedRows.Count > 0)
                return true;
            return false;
        }

        private void SaveTimeTable(object obj)
        {
            try
            {
                //if (AppConfig.TimeTableInfo.SelectedRows != null && AppConfig.TimeTableInfo.SelectedRows.Count > 0)
                //{
                DataQuery.SaveTimeTable(AppConfig.TimeTableInfo.Name,
                                        AppConfig.UserDetail.Id,
                                        AppConfig.TimeTableInfo.SelectedRows);
                AppConfig.TimeTableInfo.Refresh();
                AppConfig.AllRows = new List<DataScheduleRow>();

                MessageBox.Show("Lưu thời khoá biểu thành công");
                //}
                //else
                //{
                //    MessageBox.Show("Bạn chưa chọn môn học nào, hãy chọn môn học để lưu thời khoá biểu");
                //}
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