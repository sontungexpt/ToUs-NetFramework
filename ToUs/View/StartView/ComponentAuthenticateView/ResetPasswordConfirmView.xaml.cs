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
using System.Windows.Threading;

namespace ToUs.View.StartView.ComponentAuthenticateView
{
    /// <summary>
    /// Interaction logic for ResetPasswordConfirmView.xaml
    /// </summary>
    public partial class ResetPasswordConfirmView : UserControl
    {

        public ResetPasswordConfirmView()
        {
            InitializeComponent();
            Countdown(15, TimeSpan.FromSeconds(1), cur => TextCountdown.Text = cur.ToString());

        }

        void Countdown(int count, TimeSpan interval, Action<int> ts)
        {
            var dt = new DispatcherTimer();
            dt.Interval = interval;
            dt.Tick += (_, a) =>
            {
                if (count-- == 0)
                    dt.Stop();
                else
                    ts(count);
            };
            ts(count);
            dt.Start();
        }
    }
}
