using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToUs.Utilities;

namespace ToUs.ViewModel.StartViewModel.ComponentAuthenticateViewModel
{
    public class SignUpConfirmViewModel: ViewModelBase
    {
        
        //Fields:

        //Properties:

        //Commands:
        public ICommand SwitchToSignUpCommand { get; set; }

        //Constructor:
        public SignUpConfirmViewModel()
        {
            SwitchToSignUpCommand = AuthenticateViewModel.SignUpCommand;
        }
    }
}
