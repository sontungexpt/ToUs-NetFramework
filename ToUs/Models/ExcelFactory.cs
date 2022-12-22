using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace ToUs.Models
{
    public static class ExcelFactory
    {
        #region Feilds

        private static readonly string[] LANGUAGES = { "EN", "VN", "JP" };
        private const string STORAGE_RELATIVE_PATH = @".\..\..\..\Resources\Clients\Excels";
        private const string FORMAT = @".xlsx";
        private const string PASSWORD = @"VHJhbiBWbyBTb24gVHVuZyBsb3ZlIE5ndXllbiBUaGFuaCBYdWFu";
        private const string STORED_FILE_NAME_SUFFIEX = @"_ToUs_";

        private static Excel.Application _app;
        private static Excel.Workbook _workbook;
        private static string _path;
        private static bool _isSaved;
        private static Dictionary<string, List<Excel.Range>> _excelDatas;

        #endregion Feilds

        #region Properties

        public static Dictionary<string, List<Excel.Range>> ExcelDatas
        {
            get
            {
                if (_excelDatas == null)
                {
                    _excelDatas = new Dictionary<string, List<Excel.Range>>();
                }
                return _excelDatas;
            }
        }

        private static bool IsSaveToSystem
        {
            get => _isSaved;
        }

        public static string SourcePath
        {
            get => File.Exists(_path) ? _path : throw new NotExistedPathException();
        }

        public static string StoragePath
        {
            get
            {
                return Directory.Exists(STORAGE_RELATIVE_PATH) ?
                    Path.GetFullPath(STORAGE_RELATIVE_PATH) :
                    Directory.CreateDirectory(STORAGE_RELATIVE_PATH).ToString();
            }
        }

        public static string FormatType
        {
            get => FORMAT;
        }

        public static int SheetLength
        {
            get
            {
                if (_workbook != null)
                    return _workbook.Worksheets.Count;
                throw new NotExistedPathException("The work book is null, please open the work book first or check if the path is correct");
            }
        }

        #endregion Properties

        #region Methods

        static ExcelFactory()
        {
            _isSaved = false;
            _app = null;
            _workbook = null;
            _path = String.Empty;
            _excelDatas = null;
        }

        /// <summary>
        /// Open a excel file
        /// </summary>
        /// <param name="path"> The path of the excel file need to open </param>
        /// <exception cref="NotExistedPathException"> </exception>
        public static void Open(string path)
        {
            //Init the first status of saving to system
            _isSaved = false;
            _path = path;
            _app = new Excel.Application();
            _excelDatas = new Dictionary<string, List<Excel.Range>>();
            //If open an system excel file, auto enter password
            try
            {
                if (path.StartsWith(StoragePath))
                    _workbook = _app.Workbooks.Open(Filename: _path, Password: PASSWORD, Editable: true);
                else
                    _workbook = _app.Workbooks.Open(_path);
            }
            catch (Exception e)
            {
                //Open workbook not success, close app and throw exception
                Close();
                throw new NotExistedPathException("Can not open the workbook, is the path is correct");
            }
        }

        /// <summary>
        /// Close the excel file and clean up the memory
        /// </summary>
        public static void Close()
        {
            _isSaved = false;
            //thoát và xuất thông tin
            _workbook.Close(0);
            _app.Quit();

            if (_workbook != null)
                Marshal.ReleaseComObject(_workbook);
            if (_app != null)
                Marshal.ReleaseComObject(_app);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static Excel.Worksheet GetSheet(int index = 1)
        {
            if (index > 0 && index <= SheetLength)
                return _workbook.Worksheets[index];
            throw new IndexOutOfRangeException("The index of the sheet must be greater than 0 and less than number of sheets in workbook");
        }

        private static Excel.Range GetUsedRange(Excel.Worksheet worksheet)
           => worksheet.UsedRange;

        public static string GetValueCell(int row, int column, Excel.Worksheet worksheet)
            => GetUsedRange(worksheet).Cells[row, column].Text;

        public static string GetValueCell(int row, int column, Excel.Range range)
            => range.Cells[row, column].Text;

        private static void SetValueCell(int row, int column, string value, Excel.Worksheet worksheet)
            => GetUsedRange(worksheet).Cells[row, column].Value2 = value;

        private static void SetValueCell(int row, int column, string value, Excel.Range range)
            => range.Cells[row, column].Value2 = value;

        public static int GetColumnLength(Excel.Worksheet worksheet)
            => GetUsedRange(worksheet).Columns.Count;

        public static int GetColumnLength(Excel.Range range)
        {
            if (range != null)
                return range.Columns.Count;
            throw new NullReferenceException("The range is null");
        }

        public static int GetRowLength(Excel.Worksheet worksheet)
           => GetUsedRange(worksheet).Rows.Count;

        public static int GetRowLength(Excel.Range range)
        {
            if (range != null)
                return range.Rows.Count;
            throw new NullReferenceException("The range is null");
        }

        private static void Save()
        {
            if (_workbook != null)
                _workbook.Save();
            else
                throw new NotExistedPathException("Save file failed, the workbook is null, please open a file first");
        }

        private static void RemoveRow(int rowIndex, Excel.Worksheet worksheet)
        {
            if (rowIndex > 0 && rowIndex <= GetRowLength(worksheet))
            {
                worksheet.Rows[rowIndex].Delete();
            }
        }

        private static void RemoveRow(int rowIndex, Excel.Range range)
        {
            if (rowIndex > 0 && rowIndex <= GetRowLength(range))
            {
                range.Rows[rowIndex].Delete();
            }
        }

        private static void RemoveCol(int colIndex, Excel.Worksheet worksheet)
        {
            if (colIndex > 0 && colIndex <= GetColumnLength(worksheet))
            {
                worksheet.Columns[colIndex].Delete();
            }
        }

        private static void RemoveCol(int colIndex, Excel.Range range)
        {
            if (colIndex > 0 && colIndex <= GetColumnLength(range))
            {
                range.Columns[colIndex].Delete();
            }
        }

        public static async Task<Excel.Range> GetDataRangeAsync(Excel.Worksheet worksheet)
        {
            List<int> validRows = await FindSomeHeadAndTailValidRowAsync(worksheet);
            validRows.Sort();
            //the min item is the first valid row, the max item is the last valid row
            int startRowIndex = 0;
            int endRowIndex = 0;
            int startColIndex = 1;
            int endColIndex = GetColumnLength(worksheet);
            Excel.Range startCell = null;
            Excel.Range endCell = null;
            Excel.Range result = null;
            if (validRows.Any())
            {
                startRowIndex = validRows[0];
                endRowIndex = validRows[validRows.Count - 1];
                if (endColIndex > 0)
                {
                    startCell = worksheet.Cells[startRowIndex, startColIndex];
                    endCell = worksheet.Cells[endRowIndex, endColIndex];
                    result = worksheet.Range[startCell, endCell];
                }
            }
            return result;
        }

        public static Excel.Range GetDataRangeInColumn(int colIndex, Excel.Range dataRange, Excel.Worksheet worksheet)
        {
            Excel.Range result = null;
            Excel.Range startCell = null;
            Excel.Range endCell = null;
            int rowLength = GetRowLength(dataRange);
            if (dataRange != null)
            {
                startCell = dataRange.Cells[2, colIndex];
                endCell = dataRange.Cells[rowLength, colIndex];
                result = worksheet.Range[startCell, endCell];
                return result;
            }
            else
                throw new NullReferenceException("Excel range is null");
        }

        public static string GetHeaderColumn(int colIndex, Excel.Range dataRanges)
        {
            if (dataRanges != null)
            {
                return dataRanges.Cells[1, colIndex].Text;
            }
            else
                throw new NullReferenceException("The range is null");
        }

        public static int IsRowValid(int rowIndex, Excel.Worksheet worksheet)
        {
            if (rowIndex < 1 || rowIndex > GetRowLength(worksheet))
                throw new IndexOutOfRangeException("The index of the row must be greater than 0 and less than number of rows in sheet");
            int spaceCount = 0;
            int colLength = GetColumnLength(worksheet);

            for (int i = 1; i <= colLength; i++)
            {
                if (String.IsNullOrEmpty(GetValueCell(rowIndex, i, worksheet)))
                {
                    if (spaceCount++ > 7) // invalid row
                        return 0;
                }
            }
            return rowIndex;
        }

        /// <summary> Get the index of some valid row in the sheet from head and tail sheet
        /// </summary> <param name="worksheet"></param> <returns>List<int> contains the valid row
        /// index from head and tail sheet</returns> <exception cref="ArgumentException"></exception>
        public static async Task<List<int>> FindSomeHeadAndTailValidRowAsync(Excel.Worksheet worksheet)
        {
            int rowLength = GetRowLength(worksheet);
            List<int> validRows = new List<int>();
            //Task 1 that check the invalid row from the head sheet
            Action<object> addValidRow = (object taskName) =>
            {
                int startRowIndex;
                bool isValidRowFound = false;
                Predicate<int> outOfRange;
                int loopNumbers = rowLength >= 5 ? 5 : rowLength;
                switch (taskName)
                {
                    case "HeadTask":
                        startRowIndex = 0;
                        outOfRange = (index) => index + loopNumbers <= rowLength;
                        break;

                    case "TailTask":
                        startRowIndex = rowLength + 1;
                        outOfRange = (index) => index - loopNumbers >= 1;

                        break;

                    default:
                        throw new ArgumentException("The task name is not correct, task name just can be HeadTask or TailTask");
                }

                while (!isValidRowFound && outOfRange(startRowIndex))
                {
                    Parallel.For(0, loopNumbers, (index) =>
                    {
                        int rowValidIndex;
                        // Found the the valid row then stop the loop after all task is completed
                        if (taskName as string == "HeadTask")
                            Interlocked.Increment(ref startRowIndex);
                        else
                            Interlocked.Decrement(ref startRowIndex);
                        rowValidIndex = IsRowValid(startRowIndex, worksheet);
                        if (rowValidIndex > 0)
                        {
                            isValidRowFound = true;
                            validRows.Add(rowValidIndex);
                        }
                    });
                }
            };
            Task task = new Task(addValidRow, "HeadTask");
            task.Start();
            addValidRow("TailTask");
            await task;
            return validRows;
        }

        public static bool IsLanguageColumn(int colIndex, Excel.Worksheet worksheet)
        {
            int rowLength = GetRowLength(worksheet);
            int numberOfRowChecking = 5 < rowLength ? 5 : rowLength - 1;
            int rowCheckIndex = 2;
            while (numberOfRowChecking > 0)
            {
                bool isLanguage = false;
                string cellValue = GetValueCell(rowCheckIndex, colIndex, worksheet);
                foreach (string lang in LANGUAGES)
                {
                    if (cellValue == lang)
                    {
                        isLanguage = true;
                        break;
                    }
                }
                if (!isLanguage)
                    return false;
                rowCheckIndex++;
                numberOfRowChecking--;
            }
            return true;
        }

        public static bool IsLanguageColumn(int colIndex, Excel.Range range)
        {
            int rowLength = GetRowLength(range);
            int numberOfRowChecking = 5 < rowLength ? 5 : rowLength - 1;
            int rowCheckIndex = 2;
            while (numberOfRowChecking > 0)
            {
                bool isLanguage = false;
                string cellValue = GetValueCell(rowCheckIndex, colIndex, range);
                foreach (string lang in LANGUAGES)
                {
                    if (cellValue == lang)
                    {
                        isLanguage = true;
                        break;
                    }
                }
                if (!isLanguage)
                    return false;
                rowCheckIndex++;
                numberOfRowChecking--;
            }
            return true;
        }

        /// <summary>
        /// The method will check if the row is valid in a sheet
        /// </summary>
        /// <param name="rowIndex"> </param>
        /// <param name="worksheet"> </param>
        /// <returns>
        /// return: the index of the row if the row is invalid
        /// return: 0 if the row is valid
        /// </returns>
        /// <exception cref="IndexOutOfRangeException"> </exception>
        public static int IsRowInvalid(int rowIndex, Excel.Worksheet worksheet)
        {
            if (rowIndex < 1 || rowIndex > GetRowLength(worksheet))
                throw new IndexOutOfRangeException("The index of the row must be greater than 0 and less than number of rows in sheet");
            int spaceCount = 0;
            int colLength = GetColumnLength(worksheet);

            for (int i = 1; i <= colLength; i++)
            {
                if (String.IsNullOrEmpty(GetValueCell(rowIndex, i, worksheet)))
                {
                    if (spaceCount++ > 7) // invalid row
                        return rowIndex;
                }
            }
            return 0;
        }

        /// <summary>
        /// Find the invalid row in a sheet
        /// </summary>
        /// <param name="worksheet"> </param>
        /// <returns>
        /// A list of invalid row index in a sheet, The invalid row index in list is greater than 0,
        /// If an item in list = 0 this is the valid row
        /// </returns>
        public static async Task<List<int>> FindTheInvalidRowAsync(Excel.Worksheet worksheet)
        {
            int rowLength = GetRowLength(worksheet);
            List<int> invalidRows = new List<int>();
            //Task 1 that check the invalid row from the head sheet
            Action<object> addInvalidRow = (object taskName) =>
            {
                int startRowIndex;
                bool isValidRowFound = false;
                Predicate<int> outOfRange;
                int loopNumbers = rowLength >= 5 ? 5 : rowLength;
                switch (taskName)
                {
                    case "HeadTask":
                        startRowIndex = 0;
                        outOfRange = (index) => index + loopNumbers <= rowLength;
                        break;

                    case "TailTask":
                        startRowIndex = rowLength + 1;
                        outOfRange = (index) => index - loopNumbers >= 1;

                        break;

                    default:
                        throw new ArgumentException("The task name is not correct, task name just can be HeadTask or TailTask");
                }

                while (!isValidRowFound && outOfRange(startRowIndex))
                {
                    Parallel.For(0, loopNumbers, (index) =>
                    {
                        int rowInvalidIndex;
                        // Found the the valid row then stop the loop after all task is completed
                        if (taskName as string == "HeadTask")
                            Interlocked.Increment(ref startRowIndex);
                        else
                            Interlocked.Decrement(ref startRowIndex);
                        rowInvalidIndex = IsRowInvalid(startRowIndex, worksheet);
                        if (rowInvalidIndex == 0)
                            isValidRowFound = true;
                        invalidRows.Add(rowInvalidIndex);
                    });
                }
            };
            Task task = new Task(addInvalidRow, "HeadTask");
            task.Start();
            addInvalidRow("TailTask");
            await task;
            return invalidRows;
        }

        private static async Task RemoveAllInValidRowAsync(Excel.Worksheet worksheet)
        {
            //Find the invalid row index
            List<int> invalidRows = await FindTheInvalidRowAsync(worksheet);
            // add the value 0 to certain that there are an valid row in the list
            invalidRows.Add(0);
            invalidRows.Sort();
            int index = invalidRows.Count - 1;

            while (invalidRows[index--] != 0)
            {
                RemoveRow(invalidRows[index], worksheet);
            }
        }

        /// <summary>
        /// Find the index of language column in a sheet
        /// </summary>
        /// <param name="worksheet"> </param>
        /// <returns> The index of column if found, 0 if not found </returns>
        public static int FindLanguageColumn(Excel.Worksheet worksheet)
        {
            int colLength = GetColumnLength(worksheet);
            for (int colIndex = colLength; colIndex > 0; colIndex--)
            {
                if (IsLanguageColumn(colIndex, worksheet))
                {
                    return colIndex;
                }
            }
            //not find the language column
            return 0;
        }

        /// <summary>
        /// Find the index of language column in a range of sheet
        /// </summary>
        /// <param name="worksheet"> </param>
        /// <returns> The index of column if found, 0 if not found </returns>
        public static int FindLanguageColumn(Excel.Range range)
        {
            int colLength = GetColumnLength(range);
            for (int colIndex = colLength; colIndex > 0; colIndex--)
            {
                if (IsLanguageColumn(colIndex, range))
                {
                    return colIndex;
                }
            }
            //not find the language column
            return 0;
        }

        /// <summary>
        /// Format a range in sheet to ToUs style
        /// </summary>
        /// <param name="worksheet"> </param>
        /// <returns> </returns>
        public static async Task FormatSheetToToUsStyleAsync(Excel.Worksheet worksheet)
        {
            Excel.Range range = await GetDataRangeAsync(worksheet);
            if (range != null)
            {
                int colLength = GetColumnLength(range);
                if (String.IsNullOrEmpty(GetValueCell(1, colLength, range)))
                    RemoveCol(colLength, range);
                SetValueCell(1, FindLanguageColumn(range), "Ngôn ngữ", range);
                SetExcelDatas(range, worksheet);
            }
            else
                throw new ArgumentNullException("Excel range is null");
        }

        public static void SetExcelDatas(Excel.Range range, Excel.Worksheet worksheet)
        {
            int colLength = GetColumnLength(range);
            for (int i = 1; i <= colLength; i++)
            {
                string header = GetHeaderColumn(i, range);
                Excel.Range columnData = GetDataRangeInColumn(i, range, worksheet);
                if (_excelDatas.ContainsKey(header))
                    _excelDatas[header].Add(columnData);
                else
                {
                    _excelDatas.Add(header, new List<Excel.Range>());
                    _excelDatas[header].Add(columnData);
                }
            }
        }

        /// <summary>
        /// Format all sheet to ToUs style
        /// </summary>
        /// <returns> </returns>
        public static async Task FormatToToUsStyleAsync()
        {
            Stopwatch clock = Stopwatch.StartNew();
            clock.Start();
            if (_workbook != null)
            {
                //var tasks = _workbook.Worksheets.Cast<Excel.Worksheet>().Select(FormatSheetToToUsStyleAsync);
                foreach (Excel.Worksheet worksheet in _workbook.Worksheets)
                {
                    await FormatSheetToToUsStyleAsync(worksheet as Excel.Worksheet);
                }
                SaveToSystem();
            }

            clock.Stop();
            TimeSpan ts = clock.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        // Handle the file name and directory

        /// <summary>
        /// Get the last index of file in excel storage
        /// </summary>
        /// <returns> </returns>
        public static int GetLastSystemNumFile()
        {
            int lastNum = 0;
            foreach (string path in Directory.GetFiles(StoragePath))
            {
                string fileName = Path.GetFileNameWithoutExtension(path);
                string[] splitCharators = { STORED_FILE_NAME_SUFFIEX };
                string suffix = fileName.Split(splitCharators, StringSplitOptions.RemoveEmptyEntries)[1];
                int num = int.Parse(suffix);
                if (num > lastNum)
                    lastNum = num;
            }
            return lastNum;
        }

        /// <summary>
        /// Create a path to save to the excel storage follow ToUs rule
        /// </summary>
        /// <returns> </returns>
        public static string GenerateSystemPath()
        {
            int nextNum = GetLastSystemNumFile() + 1;
            string oldFileName = Path.GetFileNameWithoutExtension(_path);
            string newFileName = oldFileName + STORED_FILE_NAME_SUFFIEX + nextNum.ToString() + FORMAT;
            return Path.Combine(StoragePath, newFileName);
        }

        /// <summary>
        /// Save a copy of excel file to excel storage with the ToUs rule
        /// </summary>
        public static void SaveToSystem()
        {
            string newPath = GenerateSystemPath();
            _workbook?.SaveAs(Filename: newPath, AccessMode: Excel.XlSaveAsAccessMode.xlShared, Password: PASSWORD);
            _isSaved = true;
        }

        public static void PrintDataRange()
        {
            Excel.Range range = GetDataRangeAsync(_workbook.Worksheets[1] as Excel.Worksheet).Result;
            if (range != null)
                Console.WriteLine("success");

            int rowLength = range.Rows.Count;
            Console.WriteLine(rowLength);

            int colLength = range.Columns.Count;
            Console.WriteLine(colLength);

            for (int i = 1; i <= rowLength; i++)
            {
                for (int j = 1; j <= colLength; j++)
                {
                    Console.Write(GetValueCell(i, j, range) + " ");
                }
                Console.WriteLine();
            }
        }

        #endregion Methods
    }
}