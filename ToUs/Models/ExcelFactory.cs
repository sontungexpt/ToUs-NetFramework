//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using System.Runtime.InteropServices;
//using System.Collections.Specialized;
//using System.Windows.Media.Media3D;
//using Excel = Microsoft.Office.Interop.Excel;

//using Microsoft.Office.Interop.Excel;

//namespace ToUs.Models
//{
//    public class ExcelFactory
//{
//    private Excel.Application _app;
//    private Excel.Workbook _workbook;
//    private Excel.Worksheet _worksheet;
//    private Excel.Range _range;

//    private const string dirPath = @"..\Resources\Clients\Excels\";
//    private const string _format = @".xlsx";
//    private const string _password = "VHJhbiBWbyBTb24gVHVuZyBsb3ZlIE5ndXllbiBUaGFuaCBYdWFu";

//    private string _path;
//    private int _rowLength;
//    private int _colLength;
//    private Dictionary<string, Excel.Range> _excelDataset;

//    public Dictionary<string, Excel.Range> ExcelDataset
//    {
//        get
//        {
//            return _excelDataset;
//        }
//    }

//    private string[] languages = { "EN", "VN", "JP" };

//    public string Path
//    {
//        get { return _path ?? string.Empty; }
//        set { _path = value; }
//    }

//    public ExcelFactory(string path)
//    {
//        _path = path;
//        _app = new Excel.Application();

//        //If open an system excel file enter password
//        if (path.StartsWith(dirPath))
//            _workbook = _app.Workbooks.Open(Filename: _path, Password: _password);

//        _workbook = _app.Workbooks.Open(_path);

//        if (_workbook != null)
//        {
//            // Success open excel file Get first sheet
//            _worksheet = _workbook.Worksheets[1];
//            // Get all data in excel file
//            _range = _worksheet.UsedRange;
//            // Get the number of rows
//            _rowLength = _range.Rows.Count;
//            // Get the number of columns
//            _colLength = _range.Columns.Count;
//        }
//        else
//        {
//            throw new WrongPathException();
//        }
//    }

//    ~ExcelFactory()
//    {
//        Close();
//    }

//    public void Close()
//    {
//        //cleanup
//        GC.Collect();
//        GC.WaitForPendingFinalizers();
//        //quy tắc của việc phát hành các đối tượng com:
//        //không bao giờ sử dụng hai dấu chấm, tất cả các đối tượng COM phải được tham chiếu và phát hành riêng lẻ
//        //  ví dụ: [somthing].[something].[something] ----> bad
//        //xuất các đối tượng com để dừng hoàn toàn quá trình excel chạy trong nền

//        Marshal.ReleaseComObject(_range);

//        Marshal.ReleaseComObject(_worksheet);

//        //đóng lại và xuất thông tin
//        _workbook?.Close();

//        Marshal.ReleaseComObject(_workbook);

//        //thoát và xuất thông tin
//        _app?.Quit();

//        Marshal.ReleaseComObject(_app);
//    }

//    public void CreateExcelDataset()
//    {
//        _excelDataset = new Dictionary<string, Excel.Range>();

//        for (int i = 1; i <= _colLength; i++)
//        {
//            _excelDataset.Add(GetCell(1, i), _workbook.Worksheets[i].UsedRange.Rows[i]);
//        }
//        for (int i = 1; i <= _colLength; i++)
//        {
//            _excelDataset[GetCell(1, i)].Rows[1].Delete(XlDeleteShiftDirection.xlShiftUp);
//        }
//    }

//    public string? GetCell(int row, int column)
//        => _range.Cells[row, column].Text;

//    public void SetCell(int row, int column, string value)
//        => _range.Cells[row, column].Value2 = value;

//    public void Save()
//    {
//        _workbook.Save();
//    }

//    public void SaveCopyAs(string fileName = "ToUs")
//    {
//        string newPath = dirPath + fileName;
//        _workbook?.SaveAs(Filename: newPath, AccessMode: XlSaveAsAccessMode.xlShared, Password: _password);
//    }

//    public bool IsRowValid(int rowIndex)
//    {
//        int spaceCount = 0;
//        for (int i = 1; i <= _colLength; i++)
//        {
//            if (String.IsNullOrEmpty(GetCell(rowIndex, i)))
//                spaceCount++;
//        }
//        if (spaceCount > 7)
//            return false;
//        return true;
//    }

//    public bool IsLanguageColumn(int col)
//    {
//        int numberOfRowChecking = 5 < _rowLength ? 5 : _rowLength - 1;
//        int checkIndex = 2;
//        while (numberOfRowChecking > 0)
//        {
//            bool isLanguage = false;
//            string? cellValue = GetCell(checkIndex, col);
//            foreach (string lang in languages)
//            {
//                if (cellValue == lang)
//                {
//                    isLanguage = true;
//                    break;
//                }
//            }
//            if (!isLanguage)
//                return false;
//            checkIndex++;
//            numberOfRowChecking--;
//        }
//        return true;
//    }

//    public void AddLanguageColumnTitle()
//    {
//        for (int i = 1; i <= _colLength; i++)
//        {
//            if (IsLanguageColumn(i))
//            {
//                SetCell(1, i, @"Ngôn ngữ");
//            }
//        }
//    }

//    public void RemoveRow(int rowIndex)
//    {
//        _worksheet.Rows[rowIndex].Delete();
//        _rowLength--;
//    }

//    public void RemoveColumn(int colIndex)
//    {
//        _worksheet.Columns[colIndex].Delete();
//        _colLength--;
//    }

//    public void RemoveAllInvalidRow()
//    {
//        while (!IsRowValid(1))
//        {
//            RemoveRow(1);
//        }
//        while (!IsRowValid(_rowLength))
//        {
//            RemoveRow(_rowLength);
//        }
//    }

//    public void ChangeSheetIndex(int index)
//    {
//        if (index > 0 && index <= _workbook.Sheets.Count)
//        {
//            _worksheet = _workbook.Sheets[index];
//            _range = _worksheet.UsedRange;
//            _rowLength = _range.Rows.Count;
//            _colLength = _range.Columns.Count;
//        }
//    }

//    public void MergeAllSheet()
//    {
//    }

//    public void FormatToToUsStyle()
//    {
//        //remove invalid row in all sheet
//        for (int i = 1; i <= _workbook.Sheets.Count; i++)
//        {
//            ChangeSheetIndex(i);
//            RemoveAllInvalidRow();
//            AddLanguageColumnTitle();
//        }
//        Save();
//    }

//    sync function

////        //private async Task<bool> IsRowValidSync(Excel.Worksheet worksheet, int rowIndex)
////        //{
////        //    Task<bool> task = new Task<bool>(() =>
////        //    {
////        //        int spaceCount = 0;

////        //        int colLength = worksheet.UsedRange.Columns.Count;
////        //        for (int i = 1; i <= colLength; i++)
////        //        {
////        //            if (String.IsNullOrEmpty(GetCell(rowIndex, i)))
////        //                spaceCount++;
////        //        }
////        //        if (spaceCount > 7)
////        //            return false;
////        //        return true;
////        //    });
////        //    task.Start();
////        //    return await task;
////        //}

////        //private async Task RemoveInvalidRowSync(Excel.Worksheet worksheet, int rowIndex)
////        //{
////        //    Task task = new Task(() =>
////        //    {
////        //        worksheet.Rows[rowIndex].Delete();
////        //    });
////        //    task.Start();
////        //    await task;
////        //}

////        //private async Task RemoveAllInvalidRowSync(Excel.Worksheet worksheet)
////        //{
////        //    Task task = new Task(() =>
////        //    {
////        //        int rowLength = worksheet.UsedRange.Rows.Count;

////        //        while (!IsRowValidSync(worksheet, 1).Result)
////        //        {
////        //            RemoveInvalidRowSync(worksheet, 1).Wait();
////        //            rowLength--;
////        //        }
////        //        while (!IsRowValidSync(worksheet, rowLength).Result)
////        //        {
////        //            RemoveInvalidRowSync(worksheet, rowLength).Wait();
////        //            rowLength--;
////        //        }
////        //    });
////        //    task.Start();
////        //    await task;
////        //}

////        //public async Task FormatSheetToToUsStyle(Excel.Worksheet worksheet)
////        //{
////        //    Task task = new Task(() =>
////        //    {
////        //        int rowLength = worksheet.UsedRange.Rows.Count;

////        //        while (!IsRowValid(worksheet, 1))
////        //        {
////        //            worksheet.Rows[1].Delete();
////        //            rowLength--;
////        //        }
////        //        while (!IsRowValid(worksheet, _rowLength))
////        //        {
////        //            worksheet.Rows[_rowLength].Delete();
////        //            rowLength--;
////        //        }
////        //    });
////        //    task.Start();
////        //    await task;
////        //}

////        //public void FormatToToUsStyleSync()
////        //{
////        //    foreach (Excel.Worksheet worksheet in _workbook.Worksheets)
////        //    {
////        //        Task task = FormatSheetToToUsStyle(worksheet);

////        //    }
////        //    Save();
////        //}
////    }
////}