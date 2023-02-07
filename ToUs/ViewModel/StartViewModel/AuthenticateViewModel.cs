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
            set { _currentView = value; OnPropertyChanged(); }
        }
        public static ICommand SignInCommand { get; set; }
        public static ICommand SignUpCommand { get; set; }
        public static ICommand SignUpConfirmCommand { get; set; }
        public static ICommand ResetPasswordCommand { get; set; }
        public static ICommand ResetPasswordConfirmCommand { get; set; }
        public static ICommand ResetPassCommand { get; set; }

        public ICommand SwitchToEntryCommand { get; set; }

        public AuthenticateViewModel() 
        {
            SignInCommand = new RelayCommand(SignIn);
            SignUpCommand = new RelayCommand(SignUp);
            SignUpConfirmCommand = new RelayCommand(SignUpConfirm);
            ResetPasswordCommand = new RelayCommand(ResetPassword);
            ResetPasswordConfirmCommand = new RelayCommand(ResetPasswordConfirm);
            ResetPassCommand = new RelayCommand(ResetPass);

            SwitchToEntryCommand = StartViewModel.EntryCommand;

            CurrentView = new SignInViewModel();    

        }

        private void ResetPass(object obj)
        {
            CurrentView = new ResetPassViewModel();
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
    }
}
