using Microsoft.Win32;
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

namespace ToUs.View.HomePageView
{
    /// <summary>
    /// Interaction logic for UserModeView.xaml
    /// </summary>
    public partial class UserModeView : UserControl
    {
        public UserModeView()
        {
            InitializeComponent();
            TextDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }

        private void uploadBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Excel"; // Default file name
            dialog.DefaultExt = ".xlsx"; // Default file extension
            dialog.Filter = "XLS Worksheet|*.csv|Excel Workbook|*.xlsx"; // Filter files by extension
            

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
            }          



        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
