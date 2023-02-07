using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToUs.Models
{
    public class TimeTableInfo
    {
        private string _name;
        private string _semester;
        private int _year;
        private List<DataScheduleRow> _selectedRows;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Semester
        {
            get => _semester;
            set => _semester = value;
        }

        public int Year
        {
            get => _year;
            set => _year = value;
        }

        public List<DataScheduleRow> SelectedRows
        {
            get
            {
                if (_selectedRows == null)
                    return new List<DataScheduleRow>();
                return _selectedRows;
            }
            set
            {
                _selectedRows = value;
            }
        }
    }
}