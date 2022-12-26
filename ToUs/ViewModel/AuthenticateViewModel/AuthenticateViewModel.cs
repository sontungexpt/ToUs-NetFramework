
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
        private string _usernameSignInErrorMessage;
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

        public string UsernameSignInErrorMessage
        {
            get { return _usernameSignInErrorMessage; }
            set
            {
                _usernameSignInErrorMessage = value;
                OnPropertyChanged(nameof(UsernameSignInErrorMessage));
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
                OnPropertyChanged(nameof(UsernameSignIn));
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
        private bool _isViewVisible = true;
        private float _scaleWidth;
        private float _scaleHeight;
        private bool _isExit;
        public static int ourScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        public static int ourScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;


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
        public ICommand SignInCommand { get; }
        public ICommand SignUpCommand { get; }


        public AuthenticateViewModel()
        {
            ScaleWidth = (float)ourScreenWidth / 1920f;
            ScaleHeight = (float)ourScreenHeight / 1080f;

            CloseAppCommand = new RelayCommand(CloseApp);
            NotCloseAppCommand = new RelayCommand(NotCloseApp);
            SignInCommand = new RelayCommand(SignIn);
            //SignUpCommand = new RelayCommand(SignUp);
        }

        //private void SignUp(object obj)
        //{
        //    bool isValid = false;
        //    if (string.IsNullOrWhiteSpace(UsernameSignUp))
        //        UsernameSignUpErrorMessage = "* Vui lòng nhập tên đăng nhập *";
        //    else if (UsernameSignUp.Length < 6)
        //        UsernameSignUpErrorMessage = "* Tên đăng nhập hợp lệ phải có tối thiểu 6 ký tự *";
        //    else if (IsUsernameAlreadyExist(UsernameSignUp))
        //        UsernameSignUpErrorMessage = "* Tên đăng nhập đã tồn tại, vui lòng nhập tên đăng nhập khác *";
        //    else
        //        isValid = true;

        //    if (string.IsNullOrWhiteSpace(PasswordSignUp))
        //        PasswordSignUpErrorMessage = "* Vui lòng nhập mật khẩu *";
        //    else if (PasswordSignUp.Length < 6)
        //        PasswordSignUpErrorMessage = "* Mật khẩu hợp lệ phải có tối thiểu 6 ký tự *";
        //    else
        //        isValid = true;

        //    if (PasswordSignUp != ConfirmPassword)
        //        ConfirmPasswordErrorMessage = "* Mật khẩu đã nhập không trùng khớp *";
        //    else
        //        isValid = true;

        //    if(isValid)
        //    {
        //        User newUser = new User() { IsExist = true, Username = UsernameSignUp, Password = PasswordSignUp };
        //        DataProvider.Instance.entities.Users.Add(newUser);
        //        DataProvider.Instance.entities.SaveChanges();

        //        User latestUser = DataProvider.Instance.entities.Users.Last();
        //        UserDetail newUserDetail = new UserDetail() { UserId = latestUser.Id, FirstName = Firstname, LastName = Lastname, AvatarLink = null };
        //        DataProvider.Instance.entities.UserDetails.Add(newUserDetail);
        //        DataProvider.Instance.entities.SaveChanges();
        //        IsViewVisible = false;
        //    }       
        //}

        //private bool IsUsernameAlreadyExist(string username)
        //{
        //    var count = dataprovider.instance.entities.users.where(x => x.username == username).count();
        //    if (count > 0)
        //        return false;
        //    return true;
        //}

        private void SignIn(object obj)
        {
            //var count = DataProvider.Instance.entities.Users.Where(x => x.Username == UsernameSignIn && x.Password == PasswordSignIn).Count();
            //if(count > 0)
            //{
            //    IsViewVisible = false;
            //}
            //else
            //{
            //    UsernameSignInErrorMessage = "* Tên đăng nhập hoặc mật khẩu không đúng *";
            //    PasswordSignInErrorMessage = "* Tên đăng nhập hoặc mật khẩu không đúng *";
            //    UsernameSignIn = PasswordSignIn = null;
            //}
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