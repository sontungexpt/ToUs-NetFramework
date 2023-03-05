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
    /// Interaction logic for NotificationExitApp.xaml
    /// </summary>
    public partial class NotificationExitApp : UserControl
    {
        public new bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsVisible. This enables animation,
        // styling, binding, etc...
        public new static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(NotificationExitApp), new PropertyMetadata(false));
        //Message

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsVisible. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(NotificationExitApp));


        public ICommand CommandOK
        {
            get { return (ICommand)GetValue(CommandOKProperty); }
            set { SetValue(CommandOKProperty, value); }
        }

        public static DependencyProperty CommandOKProperty =
            DependencyProperty.Register("CommandOK", typeof(ICommand), typeof(NotificationExitApp));


        //Command property

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(NotificationExitApp));

        public NotificationExitApp()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}