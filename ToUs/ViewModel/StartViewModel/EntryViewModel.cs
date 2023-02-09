using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;

namespace ToUs.ViewModel.StartViewModel
{
    public class EntryViewModel : ViewModelBase
    {
        public ICommand SwitchToAuthenticateCommand { get; set; }

        public EntryViewModel()
        {
            SwitchToAuthenticateCommand = StartViewModel.AuthenticateCommand;
            AppConfig.Refresh();
        }
    }
}