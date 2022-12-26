using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToUs.Models
{
    internal class DataSupportor
    {
        public static void GetAllData()
        {
            var datas = (from manager in DataProvider.Instance.entities.SubjectManagers
                         join classItem in DataProvider.Instance.entities.Classes on manager.ClassId equals classItem.Id
                         join subject in DataProvider.Instance.entities.Subjects on manager.SubjectId equals subject.Id
                         join teacher in DataProvider.Instance.entities.Teachers on manager.TeacherId equals teacher.Id into results
                         from item in results.DefaultIfEmpty()
                         orderby subject.Name
                         select new
                         {
                             SubjectName = subject.Name,
                             ClassID = classItem.Id,
                             TeacherName = item.Name ?? "Chưa có thông tin",
                             NumberOfDigits = subject.NumberOfDigits,
                             DayInWeek = classItem.DayInWeek,
                             Time = classItem.Lession,
                             System = classItem.System,
                             Faculity = subject.Faculity,
                             HTGD = subject.HTGD,
                             Frequency = classItem.Frequency
                         });
        }
    }
}