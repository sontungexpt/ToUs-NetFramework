using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.ServiceModel.Channels;
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
            {
                foreach (DataScheduleRow row in AppConfig.TimeTableInfo.SelectedRows)
                {
                    if (IsSameLesson(dataRow.Class.DayInWeek,
                                     row.Class.DayInWeek,
                                     dataRow.Class.Lession,
                                     row.Class.Lession) || row.Subject.Id == dataRow.Subject.Id)
                    {
                        MessageBox.Show($"Lớp vừa chọn đã trùng với lớp {row.Class.ClassId} - {row.Subject.Name}");
                        ckb.IsChecked = false;
                        return;
                    }
                }
                AppConfig.TimeTableInfo.SelectedRows.Add(dataRow);
            }
            else
                AppConfig.TimeTableInfo.SelectedRows.Remove(dataRow);
        }

        private List<char[]> SplitLessionString(string lession)
        {
            var result = new List<char[]>();
            var lessionsStr = lession.Split(new char[] { '|' });
            foreach (var lessionStr in lessionsStr)
            {
                char[] items = new char[lession.Length];
                for (int i = 0; i < lession.Length; i++)
                {
                    items[i] = lession[i];
                }
                result.Add(items);
            }
            return result;
        }

        //private char[] SplitLessionString( string lesson)
        //{
        //    char[] result = new char[lesson.Length];
        //    for (int i = 0; i < lesson.Length; i++)
        //    {
        //        result[i] = lesson[i];
        //    }
        //    return result;
        //}

        //private bool IsSameLesson(string lesson)
        //{
        //    char[] check = SplitLessionString(lesson);
        //    for (int i = 0; i < check.Length; i++)
        //    {
        //        if (lesson.Contains(check[i]))
        //            return true;
        //    }
        //    return false;
        //}

        private bool IsSameLesson(string date1, string date2, string lession1, string lession2)
        {
            var date1Strs = date1.Split(new char[] { '|' });
            var date2Strs = date2.Split(new char[] { '|' });
            List<char[]> lession2Check = SplitLessionString(lession2);
            var lession1Strs = lession1.Split(new char[] { '|' });
            for (int i = 0; i < date1Strs.Length; i++)
            {
                int temp = 0;
                if (int.TryParse(date1Strs[i], out temp))
                {
                    for (int j = 0; j < date2Strs.Length; j++)
                    {
                        if (date1Strs[i] == date2Strs[j])
                        {
                            foreach (var item in lession2Check[j])
                                if (lession1Strs[i].Contains(item))
                                    return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}