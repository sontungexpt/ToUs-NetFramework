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
        private static object _currentView = AppConfiguration.FLagView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public ICommand ResetPasswordConfirmCommand { get; set; }
        public ICommand SignInCommand { get; set; }
        public ICommand SignUpConfirmCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        public AuthenticateViewModel() 
        {
            ResetPasswordConfirmCommand = new RelayCommand(ResetPasswordConfirm);
            SignInCommand = new RelayCommand(SignIn);
            SignUpConfirmCommand = new RelayCommand(SignUpConfirm);
            SignUpCommand = new RelayCommand(SignUp);
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
