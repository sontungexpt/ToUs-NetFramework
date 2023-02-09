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

            foreach (var row in AppConfig.TimeTableInfo.SelectedRows)
            {
                var dayInWeeksStr = row.Class.DayInWeek.Split(new char[] { '|' });
                var lessionsStr = row.Class.Lession.Split(new char[] { '|' });
                for (int i = 0; i < dayInWeeksStr.Length; i++)
                {
                    int day = 0;
                    var box = new BoxTimetableDetail();

                    if (int.TryParse(dayInWeeksStr[i], out day))
                    {
                        if (lessionsStr.Contains(","))
                        {
                            string[] lessions = lessionsStr[i].Split(new char[] { ',' });
                            box.SetValue(Grid.ColumnProperty, day - 1);
                            box.SetValue(Grid.RowProperty, int.Parse(lessions[i]));
                            box.SetValue(Grid.RowSpanProperty, lessions.Length);
                        }
                        else
                        {
                            box.SetValue(Grid.ColumnProperty, day - 1);
                            box.SetValue(Grid.RowProperty, int.Parse(lessionsStr[i].Substring(0, 1)));
                            box.SetValue(Grid.RowSpanProperty, lessionsStr[i].Length);
                        }
                        box.ClassId.Text = row.Class.ClassId;
                        box.SubjectName.Text = row.Subject.Name;
                        box.Room.Text = row.Class.Room;
                        box.TeacherName.Text = row.TeacherStr.Name;
                        DateTime begindate = (DateTime)row.Class.BeginDate;
                        DateTime enddate = (DateTime)row.Class.EndDate;
                        //box.Date.Text = $"{begindate.ToString("dd/MM/yyyy")} - {enddate.ToString("dd/MM/yyyy")}";
                        box.BeginDate.Text = begindate.ToString("dd/MM/yyyy");
                        box.Enddate.Text = enddate.ToString("dd/MM/yyyy");
                        gridTimeTable.Children.Add(box);
                    }
                    else
                    {
                        box.ClassId.Text = row.Class.ClassId;
                        box.SubjectName.Text = row.Subject.Name;
                        box.Room.Text = row.Class.Room;
                        box.TeacherName.Text = row.TeacherStr.Name;

                        ListSubject.Items.Add(box);
                    }

                    //box.BeginDate.Text = row.Class.BeginDate.ToString();
                    //box.EndDate.Text = row.Class.EndDate.ToString();
                }
            }
            if (AppConfig.TimeTableInfo.IsPreviewed)
            {
                AppConfig.TimeTableInfo.SelectedRows.Clear();
                AppConfig.TimeTableInfo.IsPreviewed = false;
            }
        }
    }
}