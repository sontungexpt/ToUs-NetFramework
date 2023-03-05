﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToUs.Utilities;

namespace ToUs.ViewModel.StartViewModel
{
    public class StartViewModel:ViewModelBase
    {
        private object _currentView;
        private bool _isexit = false;
        private bool _isViewVisible;

        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public bool IsExit
        {
            get { return _isexit; }
            set { _isexit = value; OnPropertyChanged(); }
        }

        public static ICommand EntryCommand { get; set; }
        public static ICommand AuthenticateCommand { get; set; }
        public static ICommand ChangeIsViewVisibleFromSignInCommand { get; set; }
        public ICommand CloseAppCommand { get; set; }
        public ICommand NotCloseAppCommand { get; set; }

        public StartViewModel() 
        {
            IsViewVisible = true;

            EntryCommand = new RelayCommand(Entry);
            AuthenticateCommand = new RelayCommand(Authenticate);
            CloseAppCommand = new RelayCommand(CloseApp);
            NotCloseAppCommand = new RelayCommand(NotCloseApp);
            ChangeIsViewVisibleFromSignInCommand = new RelayCommand(ChangeIsViewVisibleFromSignIn);

            CurrentView = new EntryViewModel();

        }

        private void ChangeIsViewVisibleFromSignIn(object obj)
        {
            IsViewVisible = false;
        }

        private void NotCloseApp(object obj)
        {
            IsExit = false;
        }

        private void CloseApp(object obj)
        {
            IsExit = true;
        }

        private void Authenticate(object obj)
        {
            CurrentView = new AuthenticateViewModel();
        }

        private void Entry(object obj)
        {
            CurrentView = new EntryViewModel();
        }

    }
}
