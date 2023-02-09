using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToUs.Models;
using ToUs.ViewModel;

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

        private void ViewOldTimeTable_Click(object sender, RoutedEventArgs e)
        {
            List<TimeTable> timeTables = ListTimeTableName.ItemsSource.Cast<TimeTable>().ToList();
            //Control control = sender as Control;
            var button = sender as Button;
            if (button == null)
                return;
            var data = button.DataContext as TimeTable;
            if (data == null)
                return;
            AppConfig.TimeTableInfo.SelectedRows = DataQuery.GetDatasInTable(data.Name, timeTables);
            AppConfig.TimeTableInfo.IsPreviewed = true;
            MainViewViewModel.PreviewCommand.Execute(null);
        }
    }
}