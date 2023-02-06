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
        private bool _isAlreadyResetPassword;
        private string _newPassword;
        private string _newPasswordErrorMessage;
        private string _confirmNewPassword;
        private string _confirmNewPasswordErrorMessage;

        //Properties:
        public bool IsAlreadyResetPassword
        {
            get { return _isAlreadyResetPassword; }
            set
            {
                _isAlreadyResetPassword = value;
                OnPropertyChanged(nameof(IsAlreadyResetPassword));
            }
        }

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

        public string ConfirmNewPassword
        {
            get { return _confirmNewPassword; }
            set
            {
                _confirmNewPassword = value;
                OnPropertyChanged(nameof(ConfirmNewPassword));
            }
        }

        public string ConfirmNewPasswordErrorMessage
        {
            get { return _confirmNewPasswordErrorMessage; }
            set
            {
                _confirmNewPasswordErrorMessage = value;
                OnPropertyChanged(nameof(ConfirmNewPasswordErrorMessage));
            }
        }

        //Commands:
        public ICommand SwitchToResetPasswordConfirmCommand { get; set; }
        public ICommand SwitchToSignInCommand { get; set; }
        public ICommand ResetPasswordCommand { get; set; }

        //Constructor:
        public ResetPassViewModel()
        {
            IsAlreadyResetPassword = false;

            SwitchToResetPasswordConfirmCommand = AuthenticateViewModel.ResetPasswordConfirmCommand;
            ResetPasswordCommand = new RelayCommand(ResetPassword);
            SwitchToSignInCommand = AuthenticateViewModel.SignInCommand;
        }

        private void ResetPassword(object obj)
        {
            bool isValidNewPassword, isValidConfirmPassword;

            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                NewPasswordErrorMessage = "* Vui lòng nhập mật khẩu *";
                isValidNewPassword = false;
            }
            else if (NewPassword.Length < 6)
            {
                NewPasswordErrorMessage = "* Mật khẩu hợp lệ phải có tối thiểu 7 ký tự *";
                isValidNewPassword = false;
            }
            else
                isValidNewPassword = true;

            if (string.IsNullOrWhiteSpace(ConfirmNewPassword))
            {
                ConfirmNewPasswordErrorMessage = "* Vui lòng xác nhận mật khẩu *";
                isValidConfirmPassword = false;
            }
            else if (NewPassword != ConfirmNewPassword)
            {
                ConfirmNewPasswordErrorMessage = "* Mật khẩu đã nhập không trùng khớp *";
                isValidConfirmPassword = false;
            }
            else
            {
                isValidConfirmPassword = true;
            }


            if (isValidNewPassword && isValidConfirmPassword)
            {
                try
                {
                    DataSupporter.UpdatePasswordByEmail(AppConfiguration.TempSignUpDetail.Email, NewPassword);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                MessageBox.Show("Doi mat khau thanh cong!!");
                IsAlreadyResetPassword = true;

                AppConfiguration.TempSignUpDetail.DeleteTempDetail();
            }
        }
    }
}
