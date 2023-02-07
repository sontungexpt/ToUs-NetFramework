using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ToUs.Models
{
    public class DataQuery
    {
        //Not testing yet, waiting for changing password and forgot password view to be done:
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

        public static List<DataScheduleRow> GetAllDataRows(int year, int semester)
        {
            var rows = new List<DataScheduleRow>();

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
                    int index;
                    if (item.Teacher != null)
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
                return rows;
            }
        }

        public static async Task<List<DataScheduleRow>> GetAllDataRowsAsync(int year, int semester)
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
                                 from item in results.DefaultIfEmpty()
                                 where classItem.Year == year && classItem.Semester == semester
                                 select new
                                 {
                                     Subject = subject,
                                     Class = classItem,
                                     Teacher = item
                                 }).ToList();

                    foreach (var item in query)
                    {
                        int index;
                        if (-1 != (index = rows.FindIndex(itemChecked => itemChecked.Class.ClassId == item.Class.ClassId)))
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
                        }
                    }
                }
            });
            task.Start();
            await task;
            return rows;
        }

        public static List<string> GetYears()
        {
            try
            {
                using (var db = new TOUSEntities())
                {
                    List<string> list = new List<string>();

                    var result = from classChecked in db.Classes
                                 group classChecked by classChecked.Year into yearGroup
                                 select yearGroup.Key;
                    foreach (var year in result)
                    {
                        list.Add(year.ToString());
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

        public static List<string> GetSemesters()
        {
            try
            {
                using (var db = new TOUSEntities())
                {
                    List<string> list = new List<string>();

                    var result = from classChecked in db.Classes
                                 group classChecked by classChecked.Semester into semesterGroup
                                 select semesterGroup.Key;
                    foreach (var semester in result)
                    {
                        list.Add(semester.ToString());
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

        public static void CreateTimetable(string name, long ownerId, string picturePreviewPath = null)
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
                context.TimeTables.Add(timeTable);
                context.SaveChanges();
            }
        }

        public static void SaveTimeTable(List<DataScheduleRow> classChoosed, int timeTableId)
        {
            using (var context = new TOUSEntities())
            {
                var timeTable = context.TimeTables.First(item => item.UserDetailId == timeTableId);
                foreach (var item in classChoosed)
                {
                    var classId = item.Class.Id;
                    var subjectId = item.Subject.Id;

                    if (item.Teachers != null)
                    {
                        if (item.Teachers.Count > 0)
                        {
                            foreach (var teacher in item.Teachers)
                            {
                                var classManager = context.ClassManagers
                                    .First(manager => manager.Id == classId &&
                                                      manager.SubjectId == subjectId &&
                                                      manager.TeacherId == teacher.Id);
                                timeTable.ClassManagers.Add(classManager);
                            }
                        }
                    }
                    else
                    {
                        var classManager = context.ClassManagers
                            .First(manager => manager.Id == classId &&
                                              manager.SubjectId == subjectId &&
                                              manager.TeacherId == null);
                        timeTable.ClassManagers.Add(classManager);
                    }
                    context.SaveChanges();
                }
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