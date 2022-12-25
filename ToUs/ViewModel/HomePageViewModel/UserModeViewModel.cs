using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;

namespace ToUs.ViewModel.HomePageViewModel
{
    internal class UserModeViewModel : ViewModelBase
    {
        private string[] _paths;

        public string[] Paths
        {
            get { return _paths; }
            set
            {
                _paths = value;
                OnPropertyChanged(nameof(_paths));
            }
        }

        public ICommand LoadExcelCommand { get; set; }

        public UserModeViewModel()
        {
            LoadExcelCommand = new RelayCommand(LoadExcel);
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
                    ExcelReader.Open(path);
                    ExcelReader.FormatExcelDatas();
                    ExcelImportDB.Connect();
                    await ExcelImportDB.ImportToDB();
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