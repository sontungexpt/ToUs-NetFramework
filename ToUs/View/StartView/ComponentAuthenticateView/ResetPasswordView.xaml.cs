using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using ToUs.Resources.CustomControl;

namespace ToUs.View.StartView.ComponentAuthenticateView
{
    /// <summary>
    /// Interaction logic for ResetPasswordView.xaml
    /// </summary>
    public partial class ResetPasswordView : UserControl
    {
        public ResetPasswordView()
        {
            InitializeComponent();
        }

        private void TextBoxPlaceHolder_KeyDown(object sender, KeyEventArgs e)
        {
            TextBoxPlaceHolder temp = sender as TextBoxPlaceHolder;
            temp.Error = string.Empty;
        }
    }
}
