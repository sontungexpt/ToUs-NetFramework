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

namespace ToUs.ViewModel.AuthenticateViewModel
{
    public class AuthenticateViewModel : ViewModelBase
    {
        //Static: 
        private static Random _rand = new Random();
        private static string _codeSent;
        public static int ourScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        public static int ourScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;

        //Sign in:
        private string _emailSignIn;
        private string _passwordSignIn;
        private string _passwordSignInErrorMessage;

        public string EmailSignIn
        {
            get { return _emailSignIn; }
            set
            {
                _emailSignIn = value;
                OnPropertyChanged(nameof(EmailSignIn));
            }
        }

        public string PasswordSignIn
        {
            get { return _passwordSignIn; }
            set
            {
                _passwordSignIn = value;
                OnPropertyChanged(nameof(PasswordSignIn));
            }
        }

        public string PasswordSignInErrorMessage
        {
            get { return _passwordSignInErrorMessage; }
            set
            {
                _passwordSignInErrorMessage = value;
                OnPropertyChanged(nameof(PasswordSignInErrorMessage));
            }
        }

        //Sign up:
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

        //Forgot password:
        private bool _isSendCode = false;
        private string _emailForgotPassword;

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

        //General
        private AuthenticateViewOption checkOption = AuthenticateViewOption.SignIn;
        private bool _isViewVisible = true;
        private float _scaleWidth;
        private float _scaleHeight;
        private bool _isExit;


        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public bool IsExit
        {
            get { return _isExit; }
            set { _isExit = value; OnPropertyChanged(); }
        }

        public float ScaleWidth
        {
            get { return _scaleWidth; }
            set { _scaleWidth = value; OnPropertyChanged(); }
        }

        public float ScaleHeight
        {
            get { return _scaleHeight; }
            set { _scaleHeight = value; OnPropertyChanged(); }
        }

        //Command:
        public ICommand CloseAppCommand { get; set; }

        public ICommand NotCloseAppCommand { get; set; }
        public ICommand SignInSignUpCommand { get; }
        public ICommand SwitchToSignUpCommand { get; }
        public ICommand SwitchToSignInCommand { get; }
        public ICommand SwitchToForgotPasswordCommand { get; }

        public AuthenticateViewModel()
        {
            ScaleWidth = (float)ourScreenWidth / 1920f;
            ScaleHeight = (float)ourScreenHeight / 1080f;

            CloseAppCommand = new RelayCommand(CloseApp);
            NotCloseAppCommand = new RelayCommand(NotCloseApp);
            SignInSignUpCommand = new RelayCommand(SignInSignUp);
            SwitchToSignUpCommand = new RelayCommand(SwitchToSignUp);
            SwitchToSignInCommand = new RelayCommand(SwitchToSignIn);
            SwitchToForgotPasswordCommand = new RelayCommand(SwitchToForgotPassword);
        }

        private void SignInSignUp(object obj)
        {
            if (checkOption == AuthenticateViewOption.SignIn)
            {
                if (string.IsNullOrWhiteSpace(EmailSignIn) && string.IsNullOrWhiteSpace(PasswordSignIn))
                {
                    PasswordSignInErrorMessage = "* Vui lòng nhập tên đăng nhập và mật khẩu *";
                }
                else if (string.IsNullOrWhiteSpace(EmailSignIn) && !string.IsNullOrWhiteSpace(PasswordSignIn))
                {
                    PasswordSignInErrorMessage = "* Vui lòng nhập tên đăng nhập *";
                }
                else if (!string.IsNullOrWhiteSpace(EmailSignIn) && string.IsNullOrWhiteSpace(PasswordSignIn))
                {
                    PasswordSignInErrorMessage = "* Vui lòng nhập tên mật khẩu *";
                }
                else
                {
                    bool authenticateAccount = DataSupporter.AuthenticateAccount(EmailSignIn, PasswordSignIn);
                    if (authenticateAccount)
                    {
                        IsViewVisible = false;
                    }
                    else
                    {
                        PasswordSignInErrorMessage = "* Tên đăng nhập hoặc mật khẩu không chính xác *";
                        EmailSignIn = string.Empty;
                        PasswordSignIn = string.Empty;
                    }
                }
            }
            else if(checkOption == AuthenticateViewOption.SignUp)
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
                    EmailSignUpErrorMessage = "* Vui lòng nhập tên đăng nhập *";
                    isValidEmail = false;
                }
                else if (EmailSignUp.Length < 6)
                {
                    EmailSignUpErrorMessage = "* Tên đăng nhập hợp lệ phải có tối thiểu 7 ký tự *";
                    EmailSignUp = string.Empty;
                    isValidEmail = false;
                }
                else if (IsEmailAlreadyExist(EmailSignUp))
                {
                    EmailSignUpErrorMessage = "* Tên đăng nhập đã tồn tại, vui lòng nhập tên đăng nhập khác *";
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
                    User newUser = new User() { IsExist = true, Username = EmailSignUp, Password = PasswordSignUp };
                    DataSupporter.AddUser(newUser);

                    User constraintUser = DataSupporter.GetUserByEmail(EmailSignUp);
                    UserDetail newUserDetail = new UserDetail() { UserId = constraintUser.Id, FirstName = Firstname, LastName = Lastname, AvatarLink = null };
                    DataSupporter.AddUserDetail(newUserDetail);

                    IsViewVisible = false;
                    LastNameErrorMessage = FirstNameErrorMessage = EmailSignUpErrorMessage = PasswordSignUpErrorMessage = ConfirmPasswordErrorMessage = string.Empty;
                }
            }
            else
            {
                string FromEmail = "UitToUs2003@outlook.com";
                string pass = "ToUs2003";
                _codeSent = (_rand.Next(999999)).ToString();

                MailMessage message = new MailMessage();
                message.From = new MailAddress(FromEmail);
                message.To.Add(EmailForgotPassword);
                message.Subject = "ToUs's password reseting code";
                message.Body = "Your reset code is " + _codeSent;

                SmtpClient smtp = new SmtpClient("smtp.outlook.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(FromEmail, pass);

                try
                {
                    smtp.Send(message);
                    IsSendCode = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SwitchToSignIn(object obj)
        {
            checkOption = AuthenticateViewOption.SignIn;
            Lastname = Firstname = EmailSignUp = PasswordSignUp = ConfirmPassword = string.Empty;
            LastNameErrorMessage = FirstNameErrorMessage = EmailSignUpErrorMessage = PasswordSignUpErrorMessage = ConfirmPasswordErrorMessage = string.Empty;
        }

        private void SwitchToSignUp(object obj)
        {
            checkOption = AuthenticateViewOption.SignUp;
            EmailSignIn = PasswordSignIn = string.Empty;
            PasswordSignInErrorMessage = string.Empty;
        }

        private void SwitchToForgotPassword(object obj)
        {
            checkOption = AuthenticateViewOption.ForgotPassword;
        }

        private bool IsEmailAlreadyExist(string Email)
        {
            var count = DataProvider.Instance.entities.Users.Where(x => x.Username == Email).Count();
            if (count > 0)
                return true;
            return false;
        }

        private void NotCloseApp(object obj)
        {
            IsExit = false;
        }

        private void CloseApp(object obj)
        {
            IsExit = true;
        }

        public bool IsValidEmailAddress(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }

    public enum AuthenticateViewOption
    { 
        SignIn,
        SignUp,
        ForgotPassword
    }

}