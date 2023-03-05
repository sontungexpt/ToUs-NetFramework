using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ToUs.Models
{
    public class DataQuery
    {
        //Not testing yet, waiting for changing password and forgot password view to be done:
        public static List<TimeTable> GetOldTimeTables(long ownerId)
        {
            using (var db = new TOUSEntities())
            {
                return db.TimeTables.Where(table => table.UserDetailId == ownerId)
                                    .Include(table => table.ClassManagers)
                                    .ToList();
            }
        }

        public static List<DataScheduleRow> GetDatasInTable(string tableName, List<TimeTable> timeTables)
        {
            var datas = new List<DataScheduleRow>();

            using (var db = new TOUSEntities())
            {
                var table = timeTables.FirstOrDefault(item => item.Name == tableName);
                var query = (from manager in table.ClassManagers
                             join classItem in db.Classes on manager.ClassId equals classItem.Id
                             join subject in db.Subjects on manager.SubjectId equals subject.Id
                             join teacher in db.Teachers on manager.TeacherId equals teacher.Id into results
                             from item in results.DefaultIfEmpty()
                             select new
                             {
                                 Subject = subject,
                                 Class = classItem,
                                 Teacher = item
                             }).ToList();
                foreach (var item in query)
                {
                    int index;
                    if (item.Teacher != null)
                    {
                        if (-1 != (index = datas.FindIndex(itemChecked
                            => itemChecked.Class.ClassId == item.Class.ClassId)))
                        {
                            if (datas[index].Teachers != null)
                                datas[index].Teachers.Add(item.Teacher);
                        }
                        else
                        {
                            var dataRow = new DataScheduleRow
                            {
                                Subject = item.Subject,
                                Class = item.Class,
                                Teachers = new List<Teacher>() { item.Teacher }
                            };
                            datas.Add(dataRow);
                        }
                    }
                    else
                    {
                        var dataRow = new DataScheduleRow
                        {
                            Subject = item.Subject,
                            Class = item.Class,
                            Teachers = null
                        };
                        datas.Add(dataRow);
                    }
                }

                return datas;
            }
        }

        public static void UpdatePasswordByEmail(string email, string password)
        {
            using (var db = new TOUSEntities())
            {
                var query = from user in db.Users
                            where user.Password == password
                            select user;
                foreach (var user in query)
                    user.Password = password;
                db.SaveChanges();
            }
        }

        public static List<DataScheduleRow> GetAllDataRows(int year, string semester)
        {
            try
            {
                using (var db = new TOUSEntities())
                {
                    var rows = new List<DataScheduleRow>();

                    var query = (from manager in db.ClassManagers
                                 join classItem in db.Classes on manager.ClassId equals classItem.Id
                                 join subject in db.Subjects on manager.SubjectId equals subject.Id
                                 join teacher in db.Teachers on manager.TeacherId equals teacher.Id into results
                                 where classItem.Year == year && classItem.Semester == semester
                                 from item in results.DefaultIfEmpty()
                                 select new
                                 {
                                     Subject = subject,
                                     Class = classItem,
                                     Teacher = item
                                 }).ToList();

                    foreach (var item in query)
                    {
                        int index = -1;
                        if (item.Teacher != null)
                        {
                            if (-1 != (index = rows.FindIndex(itemChecked
                                => itemChecked.Class.ClassId == item.Class.ClassId)))
                            {
                                if (rows[index].Teachers != null)
                                    rows[index].Teachers.Add(item.Teacher);
                            }
                            else
                            {
                                var dataRow = new DataScheduleRow
                                {
                                    Subject = item.Subject,
                                    Class = item.Class,
                                    Teachers = new List<Teacher>() { item.Teacher }
                                };
                                rows.Add(dataRow);
                            }
                        }
                        else
                        {
                            var dataRow = new DataScheduleRow
                            {
                                Subject = item.Subject,
                                Class = item.Class,
                                Teachers = null
                            };
                            rows.Add(dataRow);
                        };
                    }
                    return rows;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static async Task<List<DataScheduleRow>> GetAllDataRowsAsync(int year, string semester)
        {
            var rows = new List<DataScheduleRow>();

            Task task = new Task(() =>
            {
                using (var db = new TOUSEntities())
                {
                    var query = (from manager in db.ClassManagers
                                 join classItem in db.Classes on manager.ClassId equals classItem.Id
                                 join subject in db.Subjects on manager.SubjectId equals subject.Id
                                 join teacher in db.Teachers on manager.TeacherId equals teacher.Id into results
                                 where classItem.Year == year && classItem.Semester == semester

                                 from item in results.DefaultIfEmpty()
                                 select new
                                 {
                                     Subject = subject,
                                     Class = classItem,
                                     Teacher = item
                                 }).ToList();

                    foreach (var item in query)
                    {
                        int index = -1;
                        if (item.Teacher != null)
                        {
                            if (-1 != (index = rows.FindIndex(itemChecked
                                => itemChecked.Class.ClassId == item.Class.ClassId)))
                            {
                                rows[index].Teachers.Add(item.Teacher);
                            }
                            else
                            {
                                var dataRow = new DataScheduleRow
                                {
                                    Subject = item.Subject,
                                    Class = item.Class,
                                    Teachers = new List<Teacher>() { item.Teacher }
                                };
                                rows.Add(dataRow);
                            }
                        }
                        else
                        {
                            var dataRow = new DataScheduleRow
                            {
                                Subject = item.Subject,
                                Class = item.Class,
                                Teachers = null
                            };
                            rows.Add(dataRow);
                        };
                    }
                }
            });
            task.Start();
            await task;
            return rows;
        }

        public static List<string> GetYears(string semester = null)
        {
            try
            {
                using (var db = new TOUSEntities())
                {
                    List<string> list = new List<string>();
                    if (semester != null)
                    {
                        list = (from classChecked in db.Classes
                                where classChecked.Semester == semester
                                group classChecked by classChecked.Year into yearGroup
                                select yearGroup.Key.ToString()).ToList();
                    }
                    else
                    {
                        list = (from classChecked in db.Classes
                                group classChecked by classChecked.Year into yearGroup
                                select yearGroup.Key.ToString()).ToList();
                    }

                    return list;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public static List<string> GetSemesters(int year = 0)
        {
            try
            {
                using (var db = new TOUSEntities())
                {
                    List<string> list = new List<string>();
                    if (year > 0)
                    {
                        list = (from classChecked in db.Classes
                                where classChecked.Year == year
                                group classChecked by classChecked.Semester into semesterGroup
                                select semesterGroup.Key.ToString()).ToList();
                    }
                    else
                    {
                        list = (from classChecked in db.Classes
                                group classChecked by classChecked.Semester into semesterGroup
                                select semesterGroup.Key.ToString()).ToList();
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public static void SaveTimeTable(string name, long ownerId, List<DataScheduleRow> selectedClass, string picturePreviewPath = null)
        {
            if (string.IsNullOrEmpty(name) || ownerId < 0)
                throw new SaveChangesException("Can't create time table");
            using (var context = new TOUSEntities())
            {
                var timeTable = new TimeTable()
                {
                    Name = name,
                    UserDetailId = ownerId,
                    PicturePath = picturePreviewPath,
                };

                foreach (var classItem in selectedClass)
                {
                    var classId = classItem.Class.Id;
                    var subjectId = classItem.Subject.Id;

                    if (classItem.Teachers != null)
                    {
                        if (classItem.Teachers.Count > 0)
                        {
                            foreach (var teacher in classItem.Teachers)
                            {
                                var classManager = context.ClassManagers
                                    .First(manager => manager.ClassId == classId &&
                                                      manager.SubjectId == subjectId &&
                                                      manager.TeacherId == teacher.Id);
                                timeTable.ClassManagers.Add(classManager);
                            }
                        }
                    }
                    else
                    {
                        var classManager = context.ClassManagers
                            .First(manager => manager.ClassId == classId &&
                                              manager.SubjectId == subjectId &&
                                              String.IsNullOrEmpty(manager.TeacherId));
                        timeTable.ClassManagers.Add(classManager);
                    }
                }
                context.TimeTables.Add(timeTable);
                context.SaveChanges();
            }
        }

        //Authenticate:
        public static bool AuthenticateAccount(string email, string password)
        {
            password = Encode.EncodePassword(password);
            return DataProvider.Instance.entities.Users.Any(x => x.Username == email && x.Password == password && x.IsExist == true);
        }

        //Add:
        public static void AddUser(User newUser)
        {
            DataProvider.Instance.entities.Users.Add(newUser);
            DataProvider.Instance.entities.SaveChanges();
        }

        public static void AddUserDetail(UserDetail newUserDetail)
        {
            DataProvider.Instance.entities.UserDetails.Add(newUserDetail);
            DataProvider.Instance.entities.SaveChanges();
        }

        public static User GetUserByEmail(string email)
        {
            return DataProvider.Instance.entities.Users.Where(x => x.Username == email).FirstOrDefault();
        }

        public static UserDetail GetUserDetailByUserID(long id)
        {
            return DataProvider.Instance.entities.UserDetails.Where(x => x.UserId == id).FirstOrDefault();
        }

        public static bool IsEmailAlreadyExist(string Email)
        {
            var count = DataProvider.Instance.entities.Users.Where(x => x.Username == Email).Count();
            if (count > 0)
                return true;
            return false;
        }
    }
}