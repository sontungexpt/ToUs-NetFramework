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
    /// Interaction logic for ClassDetailInfo.xaml
    /// </summary>
    public partial class ClassDetailInfo : UserControl
    {
        public string SubjectID
        {
            get { return (string)GetValue(SubjectIDProperty); }
            set { SetValue(SubjectIDProperty, value); }
        }

        public static readonly DependencyProperty SubjectIDProperty =
            DependencyProperty.Register("SubjectID", typeof(string), typeof(ClassDetailInfo));

        //Subject
        public string Subject

        {
            get { return (string)GetValue(SubjectProperty); }
            set { SetValue(SubjectProperty, value); }
        }

        public static readonly DependencyProperty SubjectProperty =
            DependencyProperty.Register("Subject", typeof(string), typeof(ClassDetailInfo));

        //TeacherName
        public string Teacher
        {
            get { return (string)GetValue(TeacherProperty); }
            set { SetValue(TeacherProperty, value); }
        }

        public static readonly DependencyProperty TeacherProperty =
            DependencyProperty.Register("Teacher", typeof(string), typeof(ClassDetailInfo));

        //Room
        public string ClassRoom
        {
            get { return (string)GetValue(ClassRoomProperty); }
            set { SetValue(ClassRoomProperty, value); }
        }

        public static readonly DependencyProperty ClassRoomProperty =
            DependencyProperty.Register("ClassRoom", typeof(string), typeof(ClassDetailInfo));

        //Day
        public string Day
        {
            get { return (string)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }

        public static readonly DependencyProperty DayProperty =
            DependencyProperty.Register("Day", typeof(string), typeof(ClassDetailInfo));

        public ClassDetailInfo()
        {
            InitializeComponent();
        }
    }
}