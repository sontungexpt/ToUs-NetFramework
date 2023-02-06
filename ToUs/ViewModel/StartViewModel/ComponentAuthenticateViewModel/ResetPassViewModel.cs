using System;
using System.Windows;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;

namespace ToUs.ViewModel.StartViewModel.ComponentAuthenticateViewModel 
{
    public class ResetPassViewModel : ViewModelBase
    {
        //Fields:
        private string _newPassword;
        private string _newPasswordErrorMessage;

        //Properties:
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword)); 
            }
        }

        public string NewPasswordErrorMessage
        {
            get { return _newPasswordErrorMessage; }
            set
            {
                _newPasswordErrorMessage = value;
                OnPropertyChanged(nameof(NewPasswordErrorMessage));
            }
        }

        //Commands:
        public ICommand SwitchToResetPasswordConfirmCommand { get; set; }
        public ICommand SwitchToSignInCommand { get; set; }
        public ICommand ResetPasswordCommand { get; set; }

        //Constructor:
        public ResetPassViewModel()
        {
            SwitchToResetPasswordConfirmCommand = AuthenticateViewModel.ResetPasswordConfirmCommand;
            SwitchToSignInCommand = AuthenticateViewModel.SignInCommand;
            ResetPasswordCommand = new RelayCommand(ResetPassword);
        }

        private void ResetPassword(object obj)
        {
            if (string.IsNullOrWhiteSpace(NewPassword))
                NewPasswordErrorMessage = "* Vui lòng nhập mật khẩu *";
            else if (NewPassword.Length < 6)
                NewPasswordErrorMessage = "* Mật khẩu hợp lệ phải có tối thiểu 7 ký tự *";
            else
            {
                //try
                //{
                //    DataSupporter.UpdatePasswordByEmail(AppConfiguration.TempSignUpDetail.Email, NewPassword);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString());
                //}
                DataSupporter.UpdatePasswordByEmail(AppConfiguration.TempSignUpDetail.Email, NewPassword);
                MessageBox.Show("Doi mat khau thanh cong!!");

                AppConfiguration.TempSignUpDetail.DeleteTempDetail();
            }
        }
    }
}
