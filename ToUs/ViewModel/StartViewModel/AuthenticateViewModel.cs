using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;
using ToUs.ViewModel.StartViewModel.ComponentAuthenticateViewModel;

namespace ToUs.ViewModel.StartViewModel
{
    class AuthenticateViewModel: ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; }
        }
        public static ICommand ResetPasswordConfirmCommand { get; set; }
<<<<<<< HEAD
        public static ICommand ResetPasswordCommand { get; set; }
        public static ICommand SignInCommand { get; set; }
        public static ICommand SignUpConfirmCommand { get; set; }
        public static ICommand SignUpCommand { get; set; }
=======
        public static ICommand SignInCommand { get; set; }
        public static ICommand SignUpConfirmCommand { get; set; }
        public static ICommand SignUpCommand { get; set; }
        public static ICommand ResetPasswordCommand { get; set; }
>>>>>>> 613c5d8403fdc82d68965d786a813d03305c1c8d

        public AuthenticateViewModel() 
        {
            CurrentView = new SignInViewModel();

            ResetPasswordConfirmCommand = new RelayCommand(ResetPasswordConfirm);
            ResetPasswordCommand = new RelayCommand(ResetPassword);
            SignInCommand = new RelayCommand(SignIn);
            SignUpConfirmCommand = new RelayCommand(SignUpConfirm);
            SignUpCommand = new RelayCommand(SignUp);
            ResetPasswordCommand = new RelayCommand(ResetPassword);

            CurrentView = new SignInViewModel();

        }

        private void ResetPassword(object obj)
        {
            CurrentView = new ResetPasswordViewModel();
        }

        private void SignUp(object obj)
        {
            CurrentView = new SignUpViewModel();
        }

        private void SignUpConfirm(object obj)
        {
            CurrentView = new SignUpConfirmViewModel();
        }

        private void SignIn(object obj)
        {
            CurrentView = new SignInViewModel();
        }

        private void ResetPasswordConfirm(object obj)
        {
            CurrentView = new ResetPasswordConfirmViewModel();
        }
        private void ResetPassword(object obj)
        {

            CurrentView = new ResetPasswordViewModel();
        }
    }
}
