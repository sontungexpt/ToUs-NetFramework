using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;
using ToUs.ViewModel.StartViewModel;

namespace ToUs.ViewModel.StartViewModel.ComponentAuthenticateViewModel
{
    public class SignInViewModel:ViewModelBase
    {
        //Static: 
        public static int ourScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        public static int ourScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;

        //Sign in:
        private string _emailSignIn;
        private string _passwordSignIn;
        private string _passwordSignInErrorMessage;
        private bool _isSignInSucess;

        public bool IsSignInSucess
        {
            get { return _isSignInSucess; }
            set
            {
                _isSignInSucess = value;
                OnPropertyChanged(nameof(IsSignInSucess));
            }
        }


        public string EmailSignIn
        {
            get { return _emailSignIn; }
            set
            {
                _emailSignIn = value;
                OnPropertyChanged(nameof(EmailSignIn));
            }
        }

        public string PasswordSignIn
        {
            get { return _passwordSignIn; }
            set
            {
                _passwordSignIn = value;
                OnPropertyChanged(nameof(PasswordSignIn));
            }
        }

        public string PasswordSignInErrorMessage
        {
            get { return _passwordSignInErrorMessage; }
            set
            {
                _passwordSignInErrorMessage = value;
                OnPropertyChanged(nameof(PasswordSignInErrorMessage));
            }
        }

        //General 
        private float _scaleWidth;
        private float _scaleHeight;
        private bool _isExit;

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

        //Command:
        public ICommand SignInCommand { get; set; }
        public ICommand ChangeStartViewIsViewVisibleCommand { get; set; }
        public ICommand SwitchToSignUpCommand { get; set; }
        public ICommand SwitchToResetPasswordCommand { get; set; }

        public SignInViewModel()
        {
            ScaleWidth = (float)ourScreenWidth / 1920f;
            ScaleHeight = (float)ourScreenHeight / 1080f;

            IsSignInSucess = false;

            SignInCommand = new RelayCommand(SignIn);
            SwitchToSignUpCommand = AuthenticateViewModel.SignUpCommand;
            SwitchToResetPasswordCommand = AuthenticateViewModel.ResetPasswordCommand;
            ChangeStartViewIsViewVisibleCommand = StartViewModel.ChangeIsViewVisibleFromSignInCommand;

        }

        private void SignIn(object obj)
        {
            if (string.IsNullOrWhiteSpace(EmailSignIn) && string.IsNullOrWhiteSpace(PasswordSignIn))
            {
                PasswordSignInErrorMessage = "* Vui lòng nhập email và mật khẩu *";
            }
            else if (string.IsNullOrWhiteSpace(EmailSignIn) && !string.IsNullOrWhiteSpace(PasswordSignIn))
            {
                PasswordSignInErrorMessage = "* Vui lòng nhập email *";
            }
            else if (!string.IsNullOrWhiteSpace(EmailSignIn) && string.IsNullOrWhiteSpace(PasswordSignIn))
            {
                PasswordSignInErrorMessage = "* Vui lòng nhập mật khẩu *";
            }
            else
            {
                try
                {
                    bool authenticateAccount = DataQuery.AuthenticateAccount(EmailSignIn, PasswordSignIn);
                    if (authenticateAccount)
                    {
                        AppConfig.UserEmail = EmailSignIn;

                        User user = DataQuery.GetUserByEmail(AppConfig.UserEmail);
                        AppConfig.UserDetail = DataQuery.GetUserDetailByUserID(user.Id);

                        IsSignInSucess = true;

                    }
                    else
                    {
                        PasswordSignInErrorMessage = "* Email hoặc mật khẩu không chính xác *";
                        EmailSignIn = string.Empty; 
                        PasswordSignIn = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

    }
}
