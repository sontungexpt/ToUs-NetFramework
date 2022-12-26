using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToUs.Resources.CustomControl
{
    /// <summary>
    /// Interaction logic for TextBoxPlaceHolder.xaml
    /// </summary>
    public partial class TextBoxPlaceHolder : UserControl
    {
        //TextInput


        public string TextInput
        {
            get { return (string)GetValue(TextInputProperty); }
            set { SetValue(TextInputProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextInput. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty TextInputProperty =
            DependencyProperty.Register("TextInput", typeof(string), typeof(TextBoxPlaceHolder));



        //Title
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(TextBoxPlaceHolder),
                new PropertyMetadata(string.Empty));

        //Error
        public string Error
        {
            get { return (string)GetValue(ErrorProperty); }
            set { SetValue(ErrorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Error. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty ErrorProperty =
            DependencyProperty.Register("Error", typeof(string),
                typeof(TextBoxPlaceHolder), new PropertyMetadata(string.Empty));

        //Width
        public int WidthSet
        {
            get { return (int)GetValue(WidthSetProperty); }
            set { SetValue(WidthSetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty WidthSetProperty =
            DependencyProperty.Register("Width", typeof(int),
                typeof(TextBoxPlaceHolder), new PropertyMetadata(0));

        //isPassword
        public bool isPassword
        {
            get { return (bool)GetValue(isPasswordProperty); }
            set { SetValue(isPasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for isPassword. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty isPasswordProperty =
            DependencyProperty.Register("isPassword", typeof(bool),
                typeof(TextBoxPlaceHolder), new PropertyMetadata(false));

        //Password
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(TextBoxPlaceHolder));

        public TextBoxPlaceHolder()
        {
            InitializeComponent();
            passBox.PasswordChanged += OnPasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = passBox.Password;
        }

        private void passBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            placeHolder.Text = passBox.Password;
        }

        //private void placeHolder_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(placeHolder.Text) && placeHolder.Text.Length > 0)
        //    {
        //        placeHolder.Visibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //       placeHolder.Visibility = Visibility.Visible;
        //    }
        //}
    }
}