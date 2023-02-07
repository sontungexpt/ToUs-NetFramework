using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;

namespace ToUs.ViewModel.StartViewModel.ComponentAuthenticateViewModel
{
    public class ResetPasswordViewModel: ViewModelBase
    {
        //Fields:
        private string _emailForgotPassword;
        private string _emailForgotPasswordErrorMessage;
        private bool _isAlreadySendCode;

        //Properties:
        public bool IsAlreadySendCode
        {
            get { return _isAlreadySendCode; }
            set
            {
                _isAlreadySendCode = value;
                OnPropertyChanged(nameof(IsAlreadySendCode));
            }
        }

        public string EmailForgotPassword
        {
            get { return _emailForgotPassword; }
            set
            {
                _emailForgotPassword = value;
                OnPropertyChanged(nameof(EmailForgotPassword));
            }
        }

        public string EmailForgotPasswordErrorMessage
        {
            get { return _emailForgotPasswordErrorMessage; }
            set
            {
                _emailForgotPasswordErrorMessage = value;
                OnPropertyChanged(nameof(EmailForgotPasswordErrorMessage));
            }
        }

        //Command:
        public ICommand SwitchToSignInCommand { get; set; }
        public ICommand SwitchToResetPasswordConfirmCommand { get; set; }
        public ICommand SendCodeCommand { get; set; }


        //Constructor:
        public ResetPasswordViewModel()
        {
            IsAlreadySendCode = false;

            SwitchToSignInCommand = AuthenticateViewModel.SignInCommand;
            SwitchToResetPasswordConfirmCommand = AuthenticateViewModel.ResetPasswordConfirmCommand;

            SendCodeCommand = new RelayCommand(SendCode);
        }

        private void SendCode(object obj)
        {
            if (string.IsNullOrWhiteSpace(EmailForgotPassword))
                EmailForgotPasswordErrorMessage = "* Vui lòng nhập email đã tạo tài khoản *";
            else if (AppConfig.IsValidEmailAddress(EmailForgotPassword) == false)
                EmailForgotPasswordErrorMessage = "* Email đã nhập không hợp lệ, vui lòng nhập lại *";
            else if (DataQuery.IsEmailAlreadyExist(EmailForgotPassword) == false)
                EmailForgotPasswordErrorMessage = "* Email đã nhập chưa được đăng ký tài khoản, vui lòng quay lại mục đăng ký *";
            else
            {
                string FromEmail = "UitToUs2003@outlook.com";
                string pass = "ToUs2003";
                AppConfig.CodeSent = (AppConfig.Rand.Next(100000, 1000000)).ToString();

                MailMessage message = new MailMessage();
                message.From = new MailAddress(FromEmail);
                message.To.Add(EmailForgotPassword);
                message.Subject = "ToUs's password reseting code";
                message.Body = "Your reset code is " + AppConfig.CodeSent;

                SmtpClient smtp = new SmtpClient("smtp.outlook.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(FromEmail, pass);

                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                AppConfig.TempSignUpDetail.Email = EmailForgotPassword;
                IsAlreadySendCode = true;
            }

        }
    }
}
