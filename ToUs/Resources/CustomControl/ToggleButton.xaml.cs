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
    /// Interaction logic for ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        public ToggleButton()
        {
            InitializeComponent();
        }

        public bool MyIsChecked
        {
            get { return (bool)GetValue(MyIsCheckedProperty); }
            set { SetValue(MyIsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextInput. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty MyIsCheckedProperty =
            DependencyProperty.Register("MyIsChecked", typeof(bool), typeof(ToggleButton));
    }
}
