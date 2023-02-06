using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using ToUs.Resources.CustomControl;


namespace ToUs.View.StartView.ComponentAuthenticateView
{
    /// <summary>
    /// Interaction logic for ResetPassView.xaml
    /// </summary>
    public partial class ResetPassView : UserControl
    {
        public ResetPassView()
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
