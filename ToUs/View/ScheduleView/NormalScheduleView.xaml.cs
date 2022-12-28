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
using ToUs.Models;
using ToUs.Utilities;

namespace ToUs.View.ScheduleView
{
    /// <summary>
    /// Interaction logic for NormalScheduleView.xaml
    /// </summary>
    public partial class NormalScheduleView : UserControl
    {
        public NormalScheduleView()
        {
            InitializeComponent();
        }

        private void CkbClassIdClick_HandleEvent(object sender, RoutedEventArgs e)
        {
            var ckb = sender as CheckBox;
            if (ckb == null)
                return;
            var dataRow = ckb.DataContext as DataScheduleRow;
            if (dataRow == null)
                return;
            dataRow.IsChecked = ckb.IsChecked.Value;
            if (ckb.IsChecked.Value)
                AppConfiguration.SelectedRows.Add(dataRow);
            else
                AppConfiguration.SelectedRows.Remove(dataRow);
            foreach (var item in AppConfiguration.SelectedRows)
            {
                MessageBox.Show(item.Class.Id.ToString());
            }
        }
    }
}