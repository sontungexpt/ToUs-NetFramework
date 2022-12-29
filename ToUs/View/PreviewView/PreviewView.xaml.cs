using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ToUs.Resources.CustomControl;
using ToUs.ViewModel.PreviewViewModel;
using static ToUs.ViewModel.PreviewViewModel.PreviewViewModel;

namespace ToUs.View.PreviewView
{
    /// <summary>
    /// Interaction logic for PreviewView.xaml
    /// </summary>
    public partial class PreviewView : UserControl
    {

        public PreviewView()
        {
            InitializeComponent();



            foreach (DataScheduleRow i in AppConfiguration.SelectedRows)
            {
                BoxTimetableDetail boxTimetableDetail = new BoxTimetableDetail();
                boxTimetableDetail.SetValue(Grid.ColumnProperty, int.Parse(i.Class.DayInWeek) - 1);
                boxTimetableDetail.SetValue(Grid.RowProperty, int.Parse(i.Class.Lession.Substring(0, 1)));
                boxTimetableDetail.SetValue(Grid.RowSpanProperty, i.Class.Lession.Length);
                gridTimeTable.Children.Add(boxTimetableDetail);


            }
        }
    }

}
