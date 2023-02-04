using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToUs.Utilities;

namespace ToUs.ViewModel.StartViewModel
{
    class StartViewModel:ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand EntryCommand { get; set; }
        public ICommand AuthenticateCommand { get; set; }


        public StartViewModel() 
        {

            EntryCommand = new RelayCommand(Entry);
            AuthenticateCommand = new RelayCommand(Authenticate);

            CurrentView = new EntryViewModel();

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
