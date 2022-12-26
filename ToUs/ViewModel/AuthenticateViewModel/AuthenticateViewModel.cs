
using System.Windows.Forms;
using System.Windows.Input;
using ToUs.Utilities;

namespace ToUs.ViewModel.AuthenticateViewModel
{
    class AuthenticateViewModel:ViewModelBase
    {
        private float _scaleWidth;
        private float _scaleHeight;
        private bool _isExit;
        public static int ourScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        public static int ourScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;

        public bool IsExit
        {
            get { return _isExit; }
            set { _isExit = value; OnPropertyChanged(); }
        }

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

        public ICommand CloseAppCommand { get; set; }
        public ICommand NotCloseAppCommand { get; set; }

        public AuthenticateViewModel()
        {
            ScaleWidth = (float)ourScreenWidth / 1920f;
            ScaleHeight = (float)ourScreenHeight / 1080f;

            CloseAppCommand = new RelayCommand(CloseApp);
            NotCloseAppCommand = new RelayCommand(NotCloseApp);
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
