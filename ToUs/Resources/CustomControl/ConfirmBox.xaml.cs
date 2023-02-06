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
    /// Interaction logic for ConfirmBox.xaml
    /// </summary>
    public partial class ConfirmBox : UserControl
    {

        //Code

        public new string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Code. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty CodeProperty =
            DependencyProperty.Register("Code", typeof(string), typeof(ConfirmBox), new PropertyMetadata(string.Empty));

        public ConfirmBox()
        {
            InitializeComponent();
        }

        private void TBCode1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TBCode1.Text) && TBCode1.Text.Length > 0)
            {
                TBCode2.Focus();
                Code += TBCode1.Text;
            }
        }

        private void TBCode2_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(TBCode2.Text) && TBCode2.Text.Length > 0)
            {
                TBCode3.Focus();
                Code += TBCode2.Text;
            }
        }

        private void TBCode3_TextChanged(object sender, TextChangedEventArgs e)
        {
                if (!string.IsNullOrEmpty(TBCode3.Text) && TBCode3.Text.Length > 0)
                {
                    TBCode4.Focus();
                    Code += TBCode3.Text;


                }
        }

        private void TBCode4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TBCode4.Text) && TBCode4.Text.Length > 0)
            {
                TBCode5.Focus();
                Code += TBCode4.Text;

            }
        }

        private void TBCode5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TBCode5.Text) && TBCode5.Text.Length > 0)
            {
                TBCode6.Focus();
                Code += TBCode5.Text;

            }

        }

        private void TBCode6_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TBCode5.Text) && TBCode5.Text.Length > 0)
            {
                Code += TBCode6.Text;
            }

        }



    }
}
