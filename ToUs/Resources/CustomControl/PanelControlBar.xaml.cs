using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToUs.Resources.CustomControl
{
    /// <summary>
    /// Interaction logic for PanelControlBar.xaml
    /// </summary>
    public partial class PanelControlBar : UserControl
    {
        //Window property

        public Window Window
        {
            get { return (Window)GetValue(WindowProperty); }
            set { SetValue(WindowProperty, value); }
        }

        public static readonly DependencyProperty WindowProperty =
            DependencyProperty.Register("Window", typeof(Window), typeof(PanelControlBar));

        //Color

        public Brush IconForeground
        {
            get
            {
                return (Brush)GetValue(IconForegroundProperty);
            }
            set
            {
                SetValue(IconForegroundProperty, value);
            }
        }

        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register("IconForeground", typeof(Brush), typeof(PanelControlBar));

        //Command property

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(PanelControlBar));

        public PanelControlBar()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void PnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(Window);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window.WindowState = WindowState.Minimized;
        }

        private void PnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
    }
}
