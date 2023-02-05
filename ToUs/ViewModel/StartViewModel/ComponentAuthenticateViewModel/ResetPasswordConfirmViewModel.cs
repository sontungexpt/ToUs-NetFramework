using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToUs.Utilities;

namespace ToUs.ViewModel.StartViewModel.ComponentAuthenticateViewModel
{
    public class ResetPasswordConfirmViewModel: ViewModelBase
    {
        //Fields:

        //Properties:

        //Commands:
        public ICommand SwitchToResetPasswordCommand { get; set; }

        //Constructor:
        public ResetPasswordConfirmViewModel()
        {
            SwitchToResetPasswordCommand = AuthenticateViewModel.ResetPasswordCommand;
        }

    }
}
