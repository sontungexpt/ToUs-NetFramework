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

            foreach (var row in AppConfig.SelectedRows)
            {
                var dayInWeeksStr = row.Class.DayInWeek.Split(new char[] { '|' });
                var lessionsStr = row.Class.Lession.Split(new char[] { '|' });
                for (int i = 0; i < dayInWeeksStr.Length; i++)
                {
                    var box = new ClassDetailInfo();

                    if (lessionsStr.Contains(","))
                    {
                        string[] lessions = lessionsStr[i].Split(new char[] { ',' });
                        box.SetValue(Grid.ColumnProperty, int.Parse(dayInWeeksStr[i]) - 1);
                        box.SetValue(Grid.RowProperty, int.Parse(lessions[i]));
                        box.SetValue(Grid.RowSpanProperty, lessions.Length);
                    }
                    else
                    {
                        box.SetValue(Grid.ColumnProperty, int.Parse(dayInWeeksStr[i]) - 1);
                        box.SetValue(Grid.RowProperty, int.Parse(lessionsStr[i].Substring(0, 1)));
                        box.SetValue(Grid.RowSpanProperty, lessionsStr[i].Length);
                    }
                    box.ClassId.Text = row.Class.ClassId;
                    box.SubjectName.Text = row.Subject.Name;
                    box.Room.Text = row.Class.Room;
                    //string teacherName = "";
                    //if (row.Teachers != null)
                    //{
                    //    for (int j = 0; j < row.Teachers.Count; j++)
                    //    {
                    //        if (j >= 1)
                    //            teacherName += "\n";
                    //        teacherName += row.Teachers[j];
                    //    }
                    //}

                    //box.TeacherName.Text = teacherName;
                    box.TeacherName.Text = row.TeacherStr.Name;
                    //box.BeginDate.Text = row.Class.BeginDate.ToString();
                    //box.EndDate.Text = row.Class.EndDate.ToString();
                    gridTimeTable.Children.Add(box);
                }
            }
        }
    }
}