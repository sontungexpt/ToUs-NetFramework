using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToUs.Utilities;
using ToUs.Models;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace ToUs.ViewModel.StartViewModel.ComponentAuthenticateViewModel
{
    public class SignUpConfirmViewModel: ViewModelBase
    {
        ///Fields:
        private string _codeConfirm;
        private string _codeConfirmErrorMessage;
        private string _currentEmail;
        private bool _isValidCode;

        private string _myForeground;

        //Properties:
        public string CurrenEmail
        {
            get { return _currentEmail; }
            set
            {
                _currentEmail = value;
                OnPropertyChanged(nameof(CurrenEmail));
            }
        }

        public string CodeConfirm
        {
            get { return _codeConfirm; }
            set
            {
                _codeConfirm = value;
                OnPropertyChanged(nameof(CodeConfirm));
            }
        }

        public string CodeConfirmErrorMessage
        {
            get { return _codeConfirmErrorMessage; }
            set
            {
                _codeConfirmErrorMessage = value;
                OnPropertyChanged(nameof(CodeConfirmErrorMessage));
            }
        }

        public string MyForeground
        {
            get { return _myForeground; }
            set
            {
                _myForeground = value;
                OnPropertyChanged(nameof(MyForeground));
            }
        }

        public bool IsValidCode
        {
            get { return _isValidCode; }
            set
            {
                _isValidCode = value;
                OnPropertyChanged(nameof(IsValidCode));
            }
        }

        //Commands:
        public ICommand SwitchToSignUpCommand { get; set; }
        public ICommand ReSendCodeCommand { get; set; }
        public ICommand VerifyConfirmCodeCommand { get; set; }
        public ICommand SaveAccountInfoToDbCommand { get; set; }

        //Constructor:
        public SignUpConfirmViewModel()
        {
            CodeConfirmErrorMessage = "Mã OTP đã được gửi đến email ";
            MyForeground = "Gray";
            CurrenEmail = AppConfiguration.TempSignUpDetail.Email;
            IsValidCode = false;

            SwitchToSignUpCommand = AuthenticateViewModel.SignUpCommand;
            ReSendCodeCommand = new RelayCommand(ReSendCode);
            VerifyConfirmCodeCommand = new RelayCommand(VerifyConfirmCode);
            SaveAccountInfoToDbCommand = new RelayCommand(SaveAccountInfoToDb);

        }

        private void SaveAccountInfoToDb(object obj)
        {
            try
            {
                User newUser = new User() { IsExist = true, Username = AppConfiguration.TempSignUpDetail.Email, Password = Encode.EncodePassword(AppConfiguration.TempSignUpDetail.Password) };
                DataSupporter.AddUser(newUser);
                AppConfiguration.UserEmail = AppConfiguration.TempSignUpDetail.Email;

                User constraintUser = DataSupporter.GetUserByEmail(AppConfiguration.TempSignUpDetail.Email);
                UserDetail newUserDetail = new UserDetail() { UserId = constraintUser.Id, FirstName = AppConfiguration.TempSignUpDetail.FirstName, LastName = AppConfiguration.TempSignUpDetail.LastName, AvatarLink = null };
                DataSupporter.AddUserDetail(newUserDetail);
                AppConfiguration.UserDetail = newUserDetail;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            MessageBox.Show("Dang ky thanh cong!!");

        }

        private void ReSendCode(object obj)
        {
            string FromEmail = "UitToUs2003@outlook.com";
            string pass = "ToUs2003";
            AppConfiguration.CodeSent = (AppConfiguration.Rand.Next(100000, 1000000)).ToString();

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FromEmail);
            message.To.Add(AppConfiguration.TempSignUpDetail.Email);
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
            MessageBox.Show("Da gui lai ma OTP, vui long kiem tra email");
        }

        private void VerifyConfirmCode(object obj)
        {
            if (string.IsNullOrWhiteSpace(CodeConfirm))
            {
                CodeConfirmErrorMessage = "* Vui lòng nhập mã OTP *";
                CurrenEmail = string.Empty;
                MyForeground = "Red";
            }
            else if (AppConfiguration.CodeSent != CodeConfirm)
            {
                CodeConfirmErrorMessage = "* Mã OTP không đúng, vui lòng kiếm tra lại *";
                CurrenEmail = string.Empty;
                MyForeground = "Red";
            }
            else
            {
                IsValidCode = true;
            }
        }
    }
}
