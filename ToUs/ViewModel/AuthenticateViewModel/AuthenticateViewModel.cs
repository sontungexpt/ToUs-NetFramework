using System;
using System.Windows.Forms;
using System.Windows.Input;
using ToUs.Utilities;
using ToUs.Models;
using System.Linq;

namespace ToUs.ViewModel.AuthenticateViewModel
{
    public class AuthenticateViewModel : ViewModelBase
    {//Sign in:
        private string _usernameSignIn;
        private string _passwordSignIn;
        private string _passwordSignInErrorMessage;

        public string UsernameSignIn
        {
            get { return _usernameSignIn; }
            set
            {
                _usernameSignIn = value;
                OnPropertyChanged(nameof(UsernameSignIn));
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
        private string _usernameSignUp;
        private string _passwordSignUp;
        private string _confirmPassword;

        private string _lastNameErrorMessage;
        private string _firstNameErrorMessage;
        private string _usernameSignUpErrorMessage;
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

        public string UsernameSignUp
        {
            get { return _usernameSignUp; }
            set
            {
                _usernameSignUp = value;
                OnPropertyChanged(nameof(UsernameSignUp));
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

        public string UsernameSignUpErrorMessage
        {
            get { return _usernameSignUpErrorMessage; }
            set
            {
                _usernameSignUpErrorMessage = value;
                OnPropertyChanged(nameof(UsernameSignUpErrorMessage));
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

        //General
        private bool _isSignIn = true;
        private bool _isViewVisible = true;
        private float _scaleWidth;
        private float _scaleHeight;
        private bool _isExit;
        public static int ourScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        public static int ourScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;


        public bool IsSignIn
        {
            get { return _isSignIn; }
            set
            {
                _isSignIn = value;
                OnPropertyChanged(nameof(IsSignIn));
            }
        }

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


        public AuthenticateViewModel()
        {
            ScaleWidth = (float)ourScreenWidth / 1920f;
            ScaleHeight = (float)ourScreenHeight / 1080f;

            CloseAppCommand = new RelayCommand(CloseApp);
            NotCloseAppCommand = new RelayCommand(NotCloseApp);
            SignInSignUpCommand = new RelayCommand(SignInSignUp);
            SwitchToSignUpCommand = new RelayCommand(SwitchToSignUp);
            SwitchToSignInCommand = new RelayCommand(SwitchToSignIn);
        }

        private void SignInSignUp(object obj)
        {
            if (IsSignIn)
            {
                int count = DataProvider.Instance.entities.Users.Where(x => x.Username == UsernameSignIn && x.Password == PasswordSignIn && x.IsExist == true).Count();
                if (count > 0)
                {
                    PasswordSignInErrorMessage = "";
                    IsViewVisible = false;
                }
                else
                {
                    PasswordSignInErrorMessage = "* Tên đăng nhập hoặc mật khẩu không đúng *";
                    UsernameSignIn = "";
                    PasswordSignIn = "";
                }
            }
            else
            {
                bool isValidLastName, isValidFirstName, isValidUsername, isValidPassword, isValidConfirmPassword;
                if (string.IsNullOrWhiteSpace(Lastname))
                {
                    LastNameErrorMessage = "* Vui lòng nhập họ *";
                    isValidLastName = false;
                }
                else
                {
                    isValidLastName = true;
                    LastNameErrorMessage = "";
                }

                if (string.IsNullOrWhiteSpace(Firstname))
                {
                    FirstNameErrorMessage = "* Vui lòng nhập tên *";
                    isValidFirstName = false;
                }
                else
                {
                    isValidFirstName = true;
                    FirstNameErrorMessage = "";
                }


                if (string.IsNullOrWhiteSpace(UsernameSignUp))
                {
                    UsernameSignUpErrorMessage = "* Vui lòng nhập tên đăng nhập *";
                    isValidUsername = false;
                }
                else if (UsernameSignUp.Length < 6)
                {
                    UsernameSignUpErrorMessage = "* Tên đăng nhập hợp lệ phải có tối thiểu 7 ký tự *";
                    UsernameSignUp = "";
                    isValidUsername = false;
                }
                else if (IsUsernameAlreadyExist(UsernameSignUp))
                {
                    UsernameSignUpErrorMessage = "* Tên đăng nhập đã tồn tại, vui lòng nhập tên đăng nhập khác *";
                    UsernameSignUp = "";
                    isValidUsername = false;
                }
                else
                {
                    isValidUsername = true;
                    UsernameSignUpErrorMessage = "";
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
                    PasswordSignUpErrorMessage = "";
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
                    ConfirmPasswordErrorMessage = "";
                }

                if (isValidLastName && isValidFirstName && isValidUsername && isValidPassword && isValidConfirmPassword)
                {
                    User newUser = new User() { IsExist = true, Username = UsernameSignUp, Password = PasswordSignUp };
                    DataProvider.Instance.entities.Users.Add(newUser);
                    DataProvider.Instance.entities.SaveChanges();

                    User constraintUser = DataProvider.Instance.entities.Users.Where(x => x.Username == UsernameSignUp).First();
                    UserDetail newUserDetail = new UserDetail() { UserId = constraintUser.Id, FirstName = Firstname, LastName = Lastname, AvatarLink = null };
                    DataProvider.Instance.entities.UserDetails.Add(newUserDetail);
                    DataProvider.Instance.entities.SaveChanges();
                    IsViewVisible = false;
                    IsSignIn = true;
                    LastNameErrorMessage = FirstNameErrorMessage = UsernameSignUpErrorMessage = PasswordSignUpErrorMessage = ConfirmPasswordErrorMessage = "";
                }
            }
        }

        private void SwitchToSignIn(object obj)
        {
            IsSignIn = true;
        }

        private void SwitchToSignUp(object obj)
        {
            IsSignIn = false;
        }

        private bool IsUsernameAlreadyExist(string username)
        {
            var count = DataProvider.Instance.entities.Users.Where(x => x.Username == username).Count();
            if(count > 0)
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
    }
}