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
        private bool _isSucessSaveToDb;

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

        public bool IsSucessSaveToDb
        {
            get { return _isSucessSaveToDb; }
            set
            {
                _isSucessSaveToDb = value;
                OnPropertyChanged(nameof(IsSucessSaveToDb));
            }
        }

        //Commands:
        public ICommand SwitchToSignUpCommand { get; set; }
        public ICommand SwitchToSignInCommand { get; set; } 
        public ICommand ReSendCodeCommand { get; set; }
        public ICommand VerifyConfirmCodeCommand { get; set; }
        public ICommand SaveAccountInfoToDbCommand { get; set; }

        //Constructor:
        public SignUpConfirmViewModel()
        {
            CodeConfirmErrorMessage = "Mã OTP đã được gửi đến email ";
            MyForeground = "Gray";
            CurrenEmail = AppConfig.TempSignUpDetail.Email;
            IsValidCode = false;
            IsSucessSaveToDb = false;

            SwitchToSignUpCommand = AuthenticateViewModel.SignUpCommand;
            SwitchToSignInCommand = AuthenticateViewModel.SignInCommand;
            ReSendCodeCommand = new RelayCommand(ReSendCode);
            VerifyConfirmCodeCommand = new RelayCommand(VerifyConfirmCode);
            SaveAccountInfoToDbCommand = new RelayCommand(SaveAccountInfoToDb);

        }

        private void SaveAccountInfoToDb(object obj)
        {
            try
            {
                User newUser = new User() { IsExist = true, Username = AppConfig.TempSignUpDetail.Email, Password = Encode.EncodePassword(AppConfig.TempSignUpDetail.Password) };
                DataQuery.AddUser(newUser);
                AppConfig.UserEmail = AppConfig.TempSignUpDetail.Email;

                User constraintUser = DataQuery.GetUserByEmail(AppConfig.TempSignUpDetail.Email);
                UserDetail newUserDetail = new UserDetail() { UserId = constraintUser.Id, FirstName = AppConfig.TempSignUpDetail.FirstName, LastName = AppConfig.TempSignUpDetail.LastName, AvatarLink = null };
                DataQuery.AddUserDetail(newUserDetail);
                AppConfig.UserDetail = newUserDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            IsSucessSaveToDb = true;
            MessageBox.Show("Dang ky thanh cong!!");

        }

        private void ReSendCode(object obj)
        {
            string FromEmail = "UitToUs2003@outlook.com";
            string pass = "ToUs2003";
            AppConfig.CodeSent = (AppConfig.Rand.Next(100000, 1000000)).ToString();

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FromEmail);
            message.To.Add(AppConfig.TempSignUpDetail.Email);
            message.Subject = "ToUs's password validating email code";
            message.Body = "Your validate code is " + AppConfig.CodeSent;

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
            else if (AppConfig.CodeSent != CodeConfirm)
            {
                CodeConfirmErrorMessage = "* Mã OTP không đúng, vui lòng kiếm tra *";
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
