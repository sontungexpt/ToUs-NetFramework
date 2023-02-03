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
using ToUs.Resources.CustomControl;

namespace ToUs.View.HomePageView
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
            TextDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }

        private void SearchBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txblSubjectIDError.Text = string.Empty;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
           txblChooseDayError.Text = string.Empty;
        }
    }
}
