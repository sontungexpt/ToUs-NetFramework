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
            PreviewViewModel previewViewModel = new PreviewViewModel();
            foreach (Class i in previewViewModel._class)
            {
                BoxTimetableDetail table = new BoxTimetableDetail();
                if (i.day == 2 && i.lesson.Length == 2)
                {
                    table.SetValue(Grid.ColumnProperty, i.day - 1);
                    table.SetValue(Grid.RowSpanProperty, 2);
                    grid.Children.Add(table);

                }
               
            }

        }
    }
}
