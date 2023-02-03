using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToUs.Utilities;
using ToUs.ViewModel.StartViewModel;
using ToUs.ViewModel.LoginViewModel;

namespace ToUs.ViewModel.NavigationViewModel
{
    internal class NavigationViewModel : ViewModelBase
    {
        private object _currentView;

        private object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand EntryCommand { get; set; }
        public ICommand SignInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand SignUpConfirmCommand { get; set; }
        public ICommand ResetPasswordCommand { get; set; }
        public ICommand ResetPasswordConfirmCommand { get; set; }

        private void Entry(object obj) => CurrentView = new EntryViewModel();
        private void SignIn(object obj) => CurrentView = new SignInViewModel();
        private void SignUp(object obj) => CurrentView = new SignUpViewModel();
        private void SignUpConfirm(object obj) => CurrentView = new SignUpConfirmViewModel();
        private void ResetPassword(object obj) => CurrentView = new ResetPasswordViewModel();
        private void ResetPasswordConfirm(object obj) => CurrentView = new ResetPasswordConfirmViewModel();

        public NavigationViewModel()
        {
            EntryCommand = new RelayCommand(Entry);
            SignInCommand = new RelayCommand(SignIn);
            SignUpCommand = new RelayCommand(SignUp);
            SignUpConfirmCommand = new RelayCommand(SignUpConfirm);
            ResetPasswordCommand = new RelayCommand(ResetPassword);
            ResetPasswordConfirmCommand = new RelayCommand(ResetPasswordConfirm);

            //Startup Page
            CurrentView = new EntryViewModel();
        }
    }
}
