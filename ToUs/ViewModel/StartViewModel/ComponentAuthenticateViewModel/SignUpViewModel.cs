﻿using System;
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
    public class SignUpViewModel: ViewModelBase
    {
        //Fields:
        private bool _isValidDetail;
        private bool _isAlreadySendCode;
        private string _firstName;
        private string _lastName;
        private string _emailSignUp;
        private string _passwordSignUp;
        private string _confirmPassword;

        private string _lastNameErrorMessage;
        private string _firstNameErrorMessage;
        private string _emailSignUpErrorMessage;
        private string _passwordSignUpErrorMessage;
        private string _confirmPasswordErrorMessage;

        //Properties:
        public string Firstname
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }

        public string Lastname
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        public string EmailSignUp
        {
            get { return _emailSignUp; }
            set
            {
                _emailSignUp = value;
                OnPropertyChanged(nameof(EmailSignUp));
            }
        }

        public string PasswordSignUp
        {
            get { return _passwordSignUp; }
            set
            {
                _passwordSignUp = value;
                OnPropertyChanged(nameof(PasswordSignUp));
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public string LastNameErrorMessage
        {
            get { return _lastNameErrorMessage; }
            set
            {
                _lastNameErrorMessage = value;
                OnPropertyChanged(nameof(LastNameErrorMessage));
            }
        }

        public string FirstNameErrorMessage
        {
            get { return _firstNameErrorMessage; }
            set
            {
                _firstNameErrorMessage = value;
                OnPropertyChanged(nameof(FirstNameErrorMessage));
            }
        }

        public string EmailSignUpErrorMessage
        {
            get { return _emailSignUpErrorMessage; }
            set
            {
                _emailSignUpErrorMessage = value;
                OnPropertyChanged(nameof(EmailSignUpErrorMessage));
            }
        }

        public string PasswordSignUpErrorMessage
        {
            get { return _passwordSignUpErrorMessage; }
            set
            {
                _passwordSignUpErrorMessage = value;
                OnPropertyChanged(nameof(PasswordSignUpErrorMessage));
            }
        }

        public string ConfirmPasswordErrorMessage
        {
            get { return _confirmPasswordErrorMessage; }
            set
            {
                _confirmPasswordErrorMessage = value;
                OnPropertyChanged(nameof(ConfirmPasswordErrorMessage));
            }
        }

        public bool IsValidDetail
        {
            get { return _isValidDetail; }
            set
            {
                _isValidDetail = value;
                OnPropertyChanged(nameof(IsValidDetail));
            }
        }

        public bool IsAlreadySendCode
        {
            get { return _isAlreadySendCode; }
            set
            {
                _isAlreadySendCode = value;
                OnPropertyChanged(nameof(IsAlreadySendCode));
            }
        }

        //Commands:
        public ICommand SwitchToSignInCommand { get; set; }
        public ICommand SwitchToSignUpConfirmCommand { get; set; }
        public ICommand TemporarySaveSignUpDetailCommand { get; set; }
        public ICommand SendCodeCommand { get; set; }

        //Constructor:
        public SignUpViewModel() 
        {
            IsValidDetail = false;
            IsAlreadySendCode = false;

            SwitchToSignInCommand = AuthenticateViewModel.SignInCommand;
            SwitchToSignUpConfirmCommand = AuthenticateViewModel.SignUpConfirmCommand;
            TemporarySaveSignUpDetailCommand = new RelayCommand(TemporarySaveSignUpDetail);
            SendCodeCommand = new RelayCommand(SendCode);
        }

        private void TemporarySaveSignUpDetail(object obj)
        {
            bool isValidLastName, isValidFirstName, isValidEmail, isValidPassword, isValidConfirmPassword;
            if (string.IsNullOrWhiteSpace(Lastname))
            {
                LastNameErrorMessage = "* Vui lòng nhập họ *";
                isValidLastName = false;
            }
            else
            {
                isValidLastName = true;
                LastNameErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(Firstname))
            {
                FirstNameErrorMessage = "* Vui lòng nhập tên *";
                isValidFirstName = false;
            }
            else
            {
                isValidFirstName = true;
                FirstNameErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(EmailSignUp))
            {
                EmailSignUpErrorMessage = "* Vui lòng nhập email *";
                isValidEmail = false;
            }
            else if (EmailSignUp.Length < 6)
            {
                EmailSignUpErrorMessage = "* Email hợp lệ phải có tối thiểu 7 ký tự *";
                EmailSignUp = string.Empty;
                isValidEmail = false;
            }
            else if (DataQuery.IsEmailAlreadyExist(EmailSignUp))
            {
                EmailSignUpErrorMessage = "* Email đăng nhập đã tồn tại, vui lòng nhập email khác *";
                EmailSignUp = string.Empty;
                isValidEmail = false;
            }
            else
            {
                isValidEmail = true;
                EmailSignUpErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(PasswordSignUp))
            {
                PasswordSignUpErrorMessage = "* Vui lòng nhập mật khẩu *";
                isValidPassword = false;
            }
            else if (PasswordSignUp.Length < 6)
            {
                PasswordSignUpErrorMessage = "* Mật khẩu hợp lệ phải có tối thiểu 7 ký tự *";
                isValidPassword = false;
            }
            else
            {
                isValidPassword = true;
                PasswordSignUpErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ConfirmPasswordErrorMessage = "* Vui lòng xác nhận mật khẩu *";
                isValidConfirmPassword = false;
            }
            else if (PasswordSignUp != ConfirmPassword)
            {
                ConfirmPasswordErrorMessage = "* Mật khẩu đã nhập không trùng khớp *";
                isValidConfirmPassword = false;
            }
            else
            {
                isValidConfirmPassword = true;
                ConfirmPasswordErrorMessage = string.Empty;
            }

            if (isValidLastName && isValidFirstName && isValidEmail && isValidPassword && isValidConfirmPassword)
            {
                AppConfig.TempSignUpDetail.FirstName = Firstname;
                AppConfig.TempSignUpDetail.LastName = Lastname;
                AppConfig.TempSignUpDetail.Email = EmailSignUp;
                AppConfig.TempSignUpDetail.Password = PasswordSignUp;
                AppConfig.TempSignUpDetail.ConfirmPassword = ConfirmPassword;

                IsValidDetail = true;
            }
        }

        private void SendCode(object obj)
        {
            string FromEmail = "UitToUs2003@outlook.com";
            string pass = "ToUs2003";
            AppConfig.CodeSent = (AppConfig.Rand.Next(100000, 1000000)).ToString();

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FromEmail);
            message.To.Add(AppConfig.TempSignUpDetail.Email);
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

            IsAlreadySendCode = true;
        }
        
    }
}
