using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

using Microsoft.Office.Interop.Excel;

using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Windows.Media.Media3D;

namespace ToUs.Models
{
    public class ExcelFactory
    {
        private const string dirPath = "../../../Resources/Clients/Excels/";
        private string _path;

        private Excel.Application _app;
        private Excel.Workbook _workbook;
        private Excel.Worksheet _worksheet;
        private Excel.Range _range;
        private int rowLength;
        private int colLength;
        public Dictionary<string, Excel.Range> _excelData;

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public ExcelFactory(string path)
        {
            _path = path;
            _app = new Excel.Application();
            _workbook = _app.Workbooks.Open(_path);
            if (_workbook != null)
            {
                _worksheet = (Worksheet)_workbook.Worksheets[1];
                _range = _worksheet.UsedRange;
            }
            else
            {
                throw new Exception("Workbook is null, check your path");
            }
            rowLength = _range.Rows.Count;
            colLength = _range.Columns.Count;
        }

        ~ExcelFactory()
        {
            Close();
        }

        public void Close()
        {
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //quy tắc của việc phát hành các đối tượng com:
            //không bao giờ sử dụng hai dấu chấm, tất cả các đối tượng COM phải được tham chiếu và phát hành riêng lẻ
            //  ví dụ: [somthing].[something].[something] ----> bad
            //xuất các đối tượng com để dừng hoàn toàn quá trình excel chạy trong nền
            if (_range != null)
                Marshal.ReleaseComObject(_range);
            if (_worksheet != null)
                Marshal.ReleaseComObject(_worksheet);
            //đóng lại và xuất thông tin
            _workbook?.Close();
            if (_workbook != null)
                Marshal.ReleaseComObject(_workbook);
            //thoát và xuất thông tin
            _app?.Quit();
            if (_app != null)
                Marshal.ReleaseComObject(_app);
        }

        public string GetCell(int row, int column)
            => _range.Cells[row, column].Value2?.ToString();

        public void SetCell(int row, int column, string value)
            => _range.Cells[row, column].Value2 = value;

        public void Save()
        {
            _workbook?.Save();
        }

        public void SaveAs(string fileName = "ToUs.xls")
        {
            string newPath = dirPath + fileName;
            if (_workbook != null)
                _workbook.SaveAs(newPath);
            else
                throw new ArgumentNullException("Workbook is null");
        }

        public bool IsRowValid()
        {
            int count = 0;
            foreach (string value in _range.Rows)
            {
                if (String.IsNullOrEmpty(value))
                    count++;
            }
            if (count < 10)
                return false;
            return true;
        }

        public void FormatToToUsStyle()
        {
            for (int i = 1; i <= rowLength; i++)
            {
                if (!IsRowValid())
                {
                    _range.Rows[i].Delete();
                    rowLength--;
                }
            }
        }
    }
}