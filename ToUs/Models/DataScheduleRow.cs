using Microsoft.Crm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ToUs.Models
{
    public class DataScheduleRow
    {
        private List<Teacher> _teachers;
        private Subject _subject;
        private Class _class;
        private Faculty _faculty;
        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
            }
        }

        public Teacher TeacherStr
        {
            get
            {
                Teacher teacher = new Teacher()
                {
                    Name = "",
                    Id = ""
                };

                if (_teachers != null)
                {
                    for (int i = 0; i < _teachers.Count; i++)
                    {
                        if (i >= 1)
                        {
                            teacher.Name += "\n";
                            teacher.Id += "\n";
                        }

                        teacher.Name += _teachers[i].Name;
                        teacher.Id += _teachers[i].Id;
                    }
                }
                else
                {
                    teacher.Name = "Chưa xác định giáo viên";
                    teacher.Id = "*";
                }
                return teacher;
            }
        }

        public List<Teacher> Teachers
        {
            get
            {
                if (_teachers != null)
                    return _teachers;
                return null;
            }
            set
            {
                _teachers = value;
            }
        }

        public Subject Subject
        {
            get
            {
                if (_subject != null)
                    return _subject;
                return null;
            }
            set { _subject = value; }
        }

        public Class Class
        {
            get
            {
                if (_class != null)
                    return _class;
                return null;
            }
            set { _class = value; }
        }

        public Faculty Faculty
        {
            get
            {
                if (_faculty != null)
                    return _faculty;
                return null;
            }
            set { _faculty = value; }
        }

        public DataScheduleRow()
        {
            _teachers = null;
            _subject = null;
            _class = null;
            _isChecked = false;
        }

        public DataScheduleRow(Subject subject, Class @class, List<Teacher> teachers)
        {
            _teachers = teachers;
            _subject = subject;
            _class = @class;
            _isChecked = false;
        }
    }
}