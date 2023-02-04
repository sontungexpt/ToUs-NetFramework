using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ToUs.ViewModel.HomePageViewModel;

using System.Windows.Input;


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

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            srcbChosenSubjectID.IsEnabled = true;
            ckbAllIsChecked.IsEnabled = ckbMondayIsChecked.IsEnabled = ckbTuesdayIsChecked.IsEnabled = ckbWednesdayIsChecked.IsEnabled = ckbThursdayIsChecked.IsEnabled = ckbFridayIsChecked.IsEnabled = ckbSaturdayIsChecked.IsEnabled = true;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            srcbChosenSubjectID.IsEnabled = false;
            ckbAllIsChecked.IsEnabled = ckbMondayIsChecked.IsEnabled = ckbTuesdayIsChecked.IsEnabled = ckbWednesdayIsChecked.IsEnabled = ckbThursdayIsChecked.IsEnabled = ckbFridayIsChecked.IsEnabled = ckbSaturdayIsChecked.IsEnabled = false;
        }
    }
}
