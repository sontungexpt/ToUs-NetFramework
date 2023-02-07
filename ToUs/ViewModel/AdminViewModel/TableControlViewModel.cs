using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;
using static ToUs.Models.AppConfig.AdminMode;

namespace ToUs.ViewModel.AdminViewModel
{
    public class TableControlViewModel : ViewModelBase
    {
        public TableControlViewModel()
        {
            LoadExcelCommand = new RelayCommand(LoadExcel);
        }

        private ObservableCollection<ExcelPath> _paths;
        private string _tableName;
        private object _currentView;

        public ObservableCollection<ExcelPath> Paths
        {
            get { return _paths; }
            set
            {
                _paths = value;
                OnPropertyChanged(nameof(Paths));
            }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; OnPropertyChanged(nameof(TableName)); }
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
                //_paths = openFileDialog.FileNames;
                List<ExcelPath> excelPathChoosed = new List<ExcelPath>();
                foreach (string path in openFileDialog.FileNames)
                {
                    excelPathChoosed.Add(new ExcelPath(Path.GetFileName(path)));
                    Paths = new ObservableCollection<ExcelPath>(excelPathChoosed);
                }

                foreach (string path in openFileDialog.FileNames)
                {
                    //Stopwatch clock = Stopwatch.StartNew();
                    //clock.Start();

                    if (ExcelReader.Open(path, "Đại trà"))
                    {
                        ExcelReader.FormatExcelDatas();
                        if (ExcelImportDB.Connect())
                        {
                            await ExcelImportDB.ImportToDBAsync();
                        }
                        else
                            MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu");
                    }

                    //clock.Stop();
                    //TimeSpan ts = clock.Elapsed;
                    //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    //ts.Hours, ts.Minutes, ts.Seconds,
                    //ts.Milliseconds / 10);
                    //MessageBox.Show("RunTime " + elapsedTime);
                }
                MessageBox.Show("File đã được load thành công");
            }
        }
    }
}