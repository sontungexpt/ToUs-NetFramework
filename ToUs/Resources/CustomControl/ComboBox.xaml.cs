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
    /// Interaction logic for ComboBox.xaml
    /// </summary>
    public partial class ComboBox : UserControl
    {
        public ComboBox()
        {
            InitializeComponent();
        }

        public string MySelectedItem
        {
            get { return (string)GetValue(MySelectedItemProperty); }
            set { SetValue(MySelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty MySelectedItemProperty =
            DependencyProperty.Register("MySelectedItem", typeof(string), typeof(ComboBox));

        public List<string> MyItemSource
        {
            get { return (List<string>)GetValue(MyItemSourceProperty); }
            set { SetValue(MyItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty MyItemSourceProperty =
            DependencyProperty.Register("Title", typeof(List<string>), typeof(ComboBox));
    }
}