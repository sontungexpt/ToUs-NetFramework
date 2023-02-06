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
        private string _codeConfirmSignUp;

        //Properties:
        public string CodeConfirmSignUp
        {
            get { return _codeConfirmSignUp; }
            set
            {
                _codeConfirmSignUp = value;
                OnPropertyChanged(nameof(CodeConfirmSignUp));
            }
        }

        //Commands:
        public ICommand SwitchToSignUpCommand { get; set; }
        public ICommand CheckConfirmCodeAndSaveToDbCommand { get; set; }

        //Constructor:
        public SignUpConfirmViewModel()
        {
            SwitchToSignUpCommand = AuthenticateViewModel.SignUpCommand;
            CheckConfirmCodeAndSaveToDbCommand = new RelayCommand(CheckConfirmCodeAndSaveToDb);
        }

        private void CheckConfirmCodeAndSaveToDb(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
