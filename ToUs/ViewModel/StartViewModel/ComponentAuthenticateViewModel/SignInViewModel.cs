using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;

namespace ToUs.ViewModel.StartViewModel.ComponentAuthenticateViewModel
{
    class SignInViewModel:ViewModelBase
    {
        public ICommand ResetPasswordCommand { get; set; }

        public SignInViewModel() 
        {
            ResetPasswordCommand = new RelayCommand(ResetPassword);
        }

        private void ResetPassword(object obj)
        {
        }
    }
}
