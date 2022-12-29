using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;

namespace ToUs.ViewModel.HomePageViewModel
{
    public class UserModeViewModel : ViewModelBase
    {
        private string[] _paths;
        private bool _isCheckedNormalMode = true;
        private bool _isCheckedAutomaticMode;
        private string _tableName;
        private object _currentView;

        public string[] Paths
        {
            get { return _paths; }
            set
            {
                _paths = value;
                OnPropertyChanged(nameof(_paths));
            }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; OnPropertyChanged(nameof(TableName)); }
        }

        public bool IsCheckedNormalMode
        {
            get { return _isCheckedNormalMode; }
            set { _isCheckedNormalMode = value; OnPropertyChanged(nameof(IsCheckedNormalMode)); }
        }

        public bool IsCheckedAutomaticMode
        {
            get { return _isCheckedAutomaticMode; }
            set { _isCheckedAutomaticMode = value; OnPropertyChanged(nameof(IsCheckedAutomaticMode)); } 
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView)); 
            }
        }

        public ICommand LoadExcelCommand { get; set; }
        public ICommand ChooseNormalModeCommand { get; set; }
        public ICommand ChooseAutomaticModeCommand { get; set; }
        public ICommand CreateTableCommand { get; set; }

        public UserModeViewModel()
        {
            LoadExcelCommand = new RelayCommand(LoadExcel);
            ChooseNormalModeCommand = new RelayCommand(ChooseNormalMode);
            ChooseAutomaticModeCommand = new RelayCommand(ChooseAutomaticMode);
            CreateTableCommand = new RelayCommand(CreateTable, CanCreateTable);

        }

        private bool CanCreateTable(object arg)
        {
            if (string.IsNullOrWhiteSpace(TableName))
                return false;
            return true;
        }

        private void CreateTable(object obj)
        {
            MessageBox.Show("Đã lưu thông tin tạo thời khóa biểu thành công, vui lòng chuyển sang chọn lớp học để tạo thời khóa biểu");
        }

        private void ChooseNormalMode(object obj)
        {
            IsCheckedAutomaticMode = false;
        }

        private void ChooseAutomaticMode(object obj)
        {
            IsCheckedNormalMode = false;
        }

        private async void LoadExcel(object obj)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.FileName = "Excel"; // Default file name
            openFileDialog.DefaultExt = ".xlsx"; // Default file extension
            openFileDialog.Filter = "XLS Worksheet|*.csv|Excel Workbook|*.xlsx";
            openFileDialog.FilterIndex = 2;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                _paths = openFileDialog.FileNames;
                foreach (string path in _paths)
                {
                    Stopwatch clock = Stopwatch.StartNew();
                    clock.Start();

                    if (ExcelReader.Open(path))
                    {
                        ExcelReader.FormatExcelDatas();
                        if (ExcelImportDB.Connect())
                        {
                            await ExcelImportDB.ImportToDBAsync();
                        }
                        //ExcelImportDB.ImportToDB();
                        //await ExcelImportDB.ImportToDbWithEnityAsync();
                        else
                            MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu");
                    }
                    else
                        MessageBox.Show("Không thể mở file excel");

                    clock.Stop();
                    TimeSpan ts = clock.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                    MessageBox.Show("RunTime " + elapsedTime);
                }
            }
        }
    }
}