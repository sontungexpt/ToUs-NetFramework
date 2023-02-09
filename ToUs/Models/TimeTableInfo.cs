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
        private string _previewedName;
        private string _semester;

        private int _year;
        private bool _isPreviewed;

        private List<DataScheduleRow> _selectedRows;
        private List<DataScheduleRow> _selectedPreviewRows;


        public string PreviewName
        {
            get => _previewedName;
            set => _previewedName = value;
        }

        public bool IsPreviewed
        {
            get => _isPreviewed;
            set => _isPreviewed = value;
        }

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

        public int DigitsCount
        {
            get
            {
                int count = 0;
                if (_selectedRows != null)
                {
                    for (int i = 0; i < _selectedRows.Count; i++)
                    {
                        if (_selectedRows[i].Subject.NumberOfDigits != null)
                            count += (int)_selectedRows[i].Subject.NumberOfDigits;
                    }
                }
                return count;
            }
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

        public List<DataScheduleRow> SelectedPreviewRows
        {
            get
            {
                if (_selectedPreviewRows == null)
                    return new List<DataScheduleRow>();
                return _selectedPreviewRows;
            }
            set
            {
                _selectedPreviewRows = value;
            }
        }

        public void Refresh()
        {
            _name = null;
            _previewedName = null;
            if (_selectedRows != null)
            {
                _selectedRows.Clear();
            }
            else
            {
                _selectedRows = new List<DataScheduleRow>();
            }
            if (_selectedPreviewRows != null)
            {
                _selectedPreviewRows.Clear();
            }
            else
            {
                _selectedPreviewRows = new List<DataScheduleRow>();
            }
            _semester = null;
            _year = 0;
            _isPreviewed = false;
        }
    }
}