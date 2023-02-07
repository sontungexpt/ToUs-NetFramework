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
    internal class NavigationViewModel : Utilities.ViewModelBase
    {
        private object _currentView;

        private object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        //These are commands
        public ICommand BackToEntryCommand { get; set; }

        public ICommand NavigateToSignInCommand { get; set; }

        public ICommand NavigateToSignUpCommand { get; set; }
        public ICommand NextToSignUpConfirmCommand { get; set; }

        public ICommand NavigateToResetPasswordCommand { get; set; }
        public ICommand NextToResetPasswordConfirmCommand { get; set; }

        public ICommand BackToSignInCommand { get; set; }
        public ICommand BackToResetPasswordCommand { get; set; }

        //These are functions
        private void BackToEntry(object obj) => CurrentView = new EntryViewModel();

        private void NavigateToSignIn(object obj) => CurrentView = new SignInViewModel();

        private void NavigateToSignUp(object obj) => CurrentView = new SignUpViewModel();
        private void NextToSignUpConfirm(object obj) => CurrentView = new SignUpConfirmViewModel();

        private void NavigateToResetPassword(object obj) => CurrentView = new ResetPasswordViewModel();
        private void NextToResetPasswordConfirm(object obj) => CurrentView = new ResetPasswordConfirmViewModel();

        private void BackToSignIn(object obj) => CurrentView = new SignInViewModel();
        private void BackToResetPassword(object obj) => CurrentView = new ResetPasswordViewModel();


        //Navigate
        public NavigationViewModel()
        {
            BackToEntryCommand = new RelayCommand(BackToEntry);
            NavigateToSignInCommand = new RelayCommand(NavigateToSignIn);

            NavigateToSignUpCommand = new RelayCommand(NavigateToSignUp);
            NextToSignUpConfirmCommand = new RelayCommand(NextToSignUpConfirm);

            NavigateToResetPasswordCommand = new RelayCommand(NavigateToResetPassword);
            NextToResetPasswordConfirmCommand = new RelayCommand(NextToResetPasswordConfirm);

            BackToSignInCommand = new RelayCommand(BackToSignIn);
            BackToResetPasswordCommand = new RelayCommand(BackToResetPassword);

            //Startup Page
            CurrentView = new EntryViewModel();
        }
    }
}
