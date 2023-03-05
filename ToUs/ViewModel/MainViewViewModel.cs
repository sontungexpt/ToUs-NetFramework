using System.Windows.Forms;
using System.Windows.Input;
using ToUs.Utilities;
using ToUs.ViewModel.HomePageViewModel;
using ToUs.ViewModel.ScheduleViewModel;
using ToUs.ViewModel.PreviewViewModel;
using System;
using ToUs.ViewModel.AdminViewModel;
using System.Collections.Generic;
using ToUs.Models;

namespace ToUs.ViewModel
{
    internal class MainViewViewModel : ViewModelBase
    {
        private object _currentView;
        private bool _isViewVisible;
        private bool _isAdmin;
        private bool _isLoaded;
        private bool _isLogOut;
        private bool _isScale;
        private float _scaleWidth;
        private float _scaleHeight;
        private bool _isExit;
        public static int ourScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        public static int ourScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;

        //Properties:

        public float ScaleWidth
        {
            get { return _scaleWidth; }
            set { _scaleWidth = value; OnPropertyChanged(); }
        }

        public float ScaleHeight
        {
            get { return _scaleHeight; }
            set { _scaleHeight = value; OnPropertyChanged(); }
        }

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public bool IsLogOut
        {
            get { return _isLogOut; }
            set
            {
                _isLogOut = value;
                OnPropertyChanged(nameof(IsLogOut));
            }
        }

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set { _isLoaded = value; OnPropertyChanged(); }
        }

        public bool IsExit
        {
            get { return _isExit; }
            set { _isExit = value; OnPropertyChanged(); }
        }

        public bool IsScale
        {
            get { return _isScale; }
            set { _isScale = value; OnPropertyChanged(); }
        }

        public static ICommand ChangeMainViewIsViewVisibleCommand { get; set; }
        public ICommand TableControlCommand { get; set; }
        public ICommand HomeUserCommand { get; set; }
        public ICommand HomeClientCommand { get; set; }
        public static ICommand PreviewCommand { get; set; }
        public static ICommand NormalScheduleCommand { get; set; }
        public ICommand LoadedMainViewCommand { get; set; }
        public ICommand SidebarOutCommand { get; set; }
        public ICommand SidebarInCommand { get; set; }
        public ICommand CloseAppCommand { get; set; }
        public ICommand NotCloseAppCommand { get; set; }
        public ICommand SaveTableCommand { get; set; }
        public ICommand ClearAllTableInfoCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand NotLogOutCommand { get; set; }

        public MainViewViewModel()
        {
            ChangeMainViewIsViewVisibleCommand = new RelayCommand(ChangeMainViewIsViewVisible);
            TableControlCommand = new RelayCommand(TableControl);
            HomeUserCommand = new RelayCommand(HomeUser);
            NormalScheduleCommand = new RelayCommand(NormalSchedule);
            PreviewCommand = new RelayCommand(Preview);
            CloseAppCommand = new RelayCommand(CloseApp);
            NotCloseAppCommand = new RelayCommand(NotCloseApp);
            SidebarOutCommand = new RelayCommand(SidebarOut);
            SidebarInCommand = new RelayCommand(SidebarIn);
            LogOutCommand = new RelayCommand(LogOut);
            NotLogOutCommand = new RelayCommand(NotLogOut);

            LoadedMainViewCommand = new RelayCommand((p) => { LoadedMainView(); }, (p) => { return true; });

            // Startup Page
            if (AppConfig.UserEmail == "uittous2003@gmail.com")
                IsAdmin = true;
            else
                IsAdmin = false;

            IsLogOut = false;
            IsViewVisible = true;
            CurrentView = new UserModeViewModel();
            ScaleWidth = (float)ourScreenWidth / 1920f;
            ScaleHeight = (float)ourScreenHeight / 1080f;
        }

        private void NotLogOut(object obj)
        {
            IsLogOut = false;
        }

        private void LogOut(object obj)
        {
            IsLogOut = true;
        }

        private void ChangeMainViewIsViewVisible(object obj)
        {
            IsViewVisible = false;
            AppConfig.UserEmail = null;
            AppConfig.UserDetail = null;
        }

        private void SidebarIn(object obj)
        {
            IsScale = false;
        }

        private void SidebarOut(object obj)
        {
            IsScale = true;
        }

        private void TableControl(object obj)
        {
            CurrentView = new TableControlViewModel();
        }

        private void HomeUser(object obj)
        {
            CurrentView = new UserModeViewModel();
        }

        private void Preview(object obj)
        {
            CurrentView = new PreviewViewModel.PreviewViewModel();
        }

        private void NormalSchedule(object obj)
        {
            CurrentView = new NormalScheduleViewModel();
        }

        private void LoadedMainView()
        {
            IsLoaded = true;
        }

        private void NotCloseApp(object obj)
        {
            IsExit = false;
        }

        private void CloseApp(object obj)
        {
            IsExit = true;
        }
    }
}