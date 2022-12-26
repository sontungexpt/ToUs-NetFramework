using System.Windows.Forms;
using System.Windows.Input;
using ToUs.Utilities;
using ToUs.ViewModel.HomePageViewModel;
using ToUs.ViewModel.ScheduleViewModel;

namespace ToUs.ViewModel
{
    class MainViewViewModel:ViewModelBase
    {


        private object _currentView;
        private bool _isLoaded;
        private bool _isScale;
        private float _scaleWidth;
        private float _scaleHeight;
        private bool _isExit;
        public static int ourScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        public static int ourScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;



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


        public ICommand HomeUserCommand { get; set; }
        public ICommand HomeClientCommand { get; set; }
        //public ICommand PreviewCommand { get; set; }
        public ICommand AutomaticScheduleCommand { get; set; }
        public ICommand NormalScheduleCommand { get; set; }
        public ICommand LoadedMainViewCommand { get; set; }
        public ICommand SidebarOutCommand { get; set; }
        public ICommand SidebarInCommand { get; set; }
        public ICommand CloseAppCommand { get; set; }
        public ICommand NotCloseAppCommand { get; set; }





        public MainViewViewModel()
        {

            HomeUserCommand = new RelayCommand(HomeUser);
            //HomeClientCommand = new RelayCommand(HomeClient);
            //AutomaticScheduleCommand = new RelayCommand(AutomaticSchedule);
            NormalScheduleCommand = new RelayCommand(NormalSchedule);
            CloseAppCommand = new RelayCommand(CloseApp);
            NotCloseAppCommand = new RelayCommand(NotCloseApp);
            SidebarOutCommand = new RelayCommand(SidebarOut);
            SidebarInCommand = new RelayCommand(SidebarIn);

            LoadedMainViewCommand = new RelayCommand((p) => { LoadedMainView(); }, (p) => { return true; });

            // Startup Page
            CurrentView = new UserModeViewModel();
            ScaleWidth = (float)ourScreenWidth / 1920f;
            ScaleHeight = (float)ourScreenHeight / 1080f;
        }

        private void SidebarIn(object obj)
        {
            IsScale = false;
        }

        private void SidebarOut(object obj)
        {
            IsScale = true;
        }

        private void HomeUser(object obj)
        {
            CurrentView = new UserModeViewModel();
        }

        //private void HomeClient(object obj)
        //{
        //    CurrentView = new ClientModeVM();
        //}

        ////private void Preview(object obj) => CurrentView = new PreView();

        //private void AutomaticSchedule(object obj)
        //{
        //    CurrentView = new AutomaticScheduleVM();
        //}

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
