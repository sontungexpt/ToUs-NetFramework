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
        
        private bool _isSendCode = false;
        private string _emailForgotPassword;

        //Properties:
        public bool IsSendCode
        {
            get { return _isSendCode; }
            set
            {
                _isSendCode = value;
                OnPropertyChanged(nameof(IsSendCode));
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

        //Command:
        public ICommand SwitchToSignInCommand { get; set; }
        public ICommand SwitchToResetPasswordConfirmCommand { get; set; }
        public ICommand SendCodeCommand { get; set; }


        //Constructor:
        public ResetPasswordViewModel()
        {
            SwitchToSignInCommand = AuthenticateViewModel.SignInCommand;
            SwitchToResetPasswordConfirmCommand = AuthenticateViewModel.ResetPasswordConfirmCommand;
            SendCodeCommand = new RelayCommand(SendCode);

        }

        private void SendCode(object obj)
        {
            string FromEmail = "UitToUs2003@outlook.com";
            string pass = "ToUs2003";
            AppConfiguration.CodeSent = (AppConfiguration.Rand.Next(999999)).ToString();

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FromEmail);
            message.To.Add(EmailForgotPassword);
            message.Subject = "ToUs's password reseting code";
            message.Body = "Your reset code is " + AppConfiguration.CodeSent;

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

            IsSendCode = true;
        }
    }
}
