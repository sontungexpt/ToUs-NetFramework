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
using System.Windows.Shapes;

namespace ToUs.View.AuthenticateView
{
    /// <summary>
    /// Interaction logic for AuthenticateView.xaml
    /// </summary>
    public partial class AuthenticateView : Window
    {
        public AuthenticateView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            PnlControlBar.Window = this;
        }

        private void TextBlockFilter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBoxCodeEmail.Focus();
        }

        private void TextBoxCodeEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxCodeEmail.Text) && TextBoxCodeEmail.Text.Length > 0)
            {
                TextBlockPlaceHolderCodeEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBlockPlaceHolderCodeEmail.Visibility = Visibility.Visible;
            }
        }

        private void TextBlockPlaceHolderEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBoxEmail.Focus();
        }

        private void TextBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxEmail.Text) && TextBoxEmail.Text.Length > 0)
            {
                TextBlockPlaceHolderEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBlockPlaceHolderEmail.Visibility = Visibility.Visible;
            }
        }
    }
}
