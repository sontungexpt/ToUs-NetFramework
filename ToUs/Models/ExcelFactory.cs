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

using Microsoft.Office.Interop.Excel;

namespace ToUs.Models
{
    public static class ExcelFactory
    {
        #region Feilds

        private class DataRangePosition
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public int RowCount { get; set; }
            public int ColumnCount { get; set; }

            public DataRangePosition()
            {
                Row = 0;
                Column = 0;
                RowCount = 0;
                ColumnCount = 0;
            }

            public DataRangePosition(int row, int column, int rowCount, int columnCount)
            {
                Row = row;
                Column = column;
                RowCount = rowCount;
                ColumnCount = columnCount;
            }
        }

        private static Dictionary<string, DataRangePosition> _dataRangePositions;
        private static readonly string[] LANGUAGES = { "EN", "VN", "JP" };
        private const string STORAGE_RELATIVE_PATH = @".\..\..\Resources\Clients\Excels";
        private const string FORMAT = @".xlsx";
        private const string PASSWORD = @"VHJhbiBWbyBTb24gVHVuZyBsb3ZlIE5ndXllbiBUaGFuaCBYdWFu";
        private const string STORED_FILE_NAME_SUFFIEX = @"_ToUs_";

        private static Excel.Application _app;
        private static Excel.Workbook _workbook;
        private static string _path;
        private static Dictionary<string, List<string>> _dataColumns;
        private static string _pathAfterSave;

        #endregion Feilds

        #region Properties

        public static string SystemPath
        {
            get => _pathAfterSave;
        }

        public static Dictionary<string, List<string>> DataColumns
        {
            get
            {
                if (_dataColumns == null)
                {
                    _dataColumns = new Dictionary<string, List<string>>();
                }
                return _dataColumns;
            }
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
            _app = null;
            _workbook = null;
            _path = String.Empty;
            _dataColumns = null;
            _dataRangePositions = null;
        }

        /// <summary>
        /// Open a excel file
        /// </summary>
        /// <param name="path"> The path of the excel file need to open </param>
        /// <exception cref="NotExistedPathException"> </exception>
        public static void Open(string path)
        {
            //Init the first status of saving to system

            _path = path;
            _app = new Excel.Application();
            _dataColumns = new Dictionary<string, List<string>>();
            _dataRangePositions = new Dictionary<string, DataRangePosition>();
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

        private static void Save()
        {
            if (_workbook != null)
                _workbook.Save();
            else
                throw new NotExistedPathException("Save file failed, the workbook is null, please open a file first");
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

        private static void SetValueCell(int row, int column, string value, Excel.Worksheet worksheet)
            => GetUsedRange(worksheet).Cells[row, column].Value2 = value;

        public static int GetColumnLength(Excel.Worksheet worksheet)
            => GetUsedRange(worksheet).Columns.Count;

        public static int GetRowLength(Excel.Worksheet worksheet)
           => GetUsedRange(worksheet).Rows.Count;

        private static void RemoveRow(int rowIndex, Excel.Worksheet worksheet)
        {
            if (rowIndex > 0 && rowIndex <= GetRowLength(worksheet))
            {
                worksheet.Rows[rowIndex].Delete();
            }
        }

        private static void RemoveCol(int colIndex, Excel.Worksheet worksheet)
        {
            if (colIndex > 0 && colIndex <= GetColumnLength(worksheet))
            {
                worksheet.Columns[colIndex].Delete();
            }
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
        public static async Task<List<int>> FindSomeHeadAndTailValidRowIndexAsync(Excel.Worksheet worksheet)
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
            int rowLength = _dataRangePositions[worksheet.Name].RowCount;
            int numberOfRowChecking = 5 < rowLength ? 5 : rowLength - 1;
            int rowCheckIndex = _dataRangePositions[worksheet.Name].Row + 1;
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

        public static void FormatLanguageColumn(Excel.Worksheet worksheet)
        {
            for (int colIndex = _dataRangePositions[worksheet.Name].ColumnCount; colIndex > 0; colIndex--)
            {
                if (IsLanguageColumn(colIndex, worksheet))
                {
                    SetValueCell(_dataRangePositions[worksheet.Name].Row, colIndex, "NGÔN NGỮ", worksheet);
                    return;
                }
            }
        }

        public static int FindHeaderColumnIndex(string header, Excel.Worksheet worksheet)
        {
            for (int colIndex = 1; colIndex <= _dataRangePositions[worksheet.Name].ColumnCount; colIndex++)
            {
                if (GetValueCell(_dataRangePositions[worksheet.Name].Row, colIndex, worksheet) == header)
                    return colIndex;
            }
            return 0;
        }

        public static void FormatNumberOfCreditsHeader(Excel.Worksheet worksheet)
        {
            int colIndex = FindHeaderColumnIndex("TỐ TC", worksheet);
            if (colIndex != 0)
                SetValueCell(_dataRangePositions[worksheet.Name].Row, colIndex, "SỐ TC", worksheet);
        }

        public static void FormatNameOfTeacherHeader(Excel.Worksheet worksheet)
        {
            int colIndex = FindHeaderColumnIndex("TÊN TRỢ GIẢNG", worksheet);
            if (colIndex != 0)
            {
                SetValueCell(_dataRangePositions[worksheet.Name].Row, colIndex, "TÊN GIẢNG VIÊN", worksheet);
                Console.WriteLine(worksheet.Name);
            }
        }

        public static void RemoveLastNoHeaderColumn(Excel.Worksheet worksheet)
        {
            for (int colIndex = _dataRangePositions[worksheet.Name].ColumnCount; colIndex > 0; colIndex--)
            {
                if (String.IsNullOrEmpty(GetValueCell(_dataRangePositions[worksheet.Name].Row, colIndex, worksheet)))
                {
                    RemoveCol(colIndex, worksheet);
                    _dataRangePositions[worksheet.Name].ColumnCount--;
                    return;
                }
            }
        }

        public static async Task SetDataRangePosition(Excel.Worksheet worksheet)
        {
            List<int> validRowIndexs = await FindSomeHeadAndTailValidRowIndexAsync(worksheet);
            validRowIndexs.Sort();

            if (validRowIndexs.Any())
            {
                int rowStartIndex = validRowIndexs[0];
                int columnStartIndex = 1;
                int rowCount = validRowIndexs[validRowIndexs.Count - 1] - validRowIndexs[0] + 1;
                int columnCount = GetColumnLength(worksheet);
                _dataRangePositions.Add(worksheet.Name, new DataRangePosition(rowStartIndex, columnStartIndex, rowCount, columnCount));
            }
        }

        public static async Task FormatASheetToToUsStyleAsync(Excel.Worksheet worksheet)
        {
            await SetDataRangePosition(worksheet);
            FormatLanguageColumn(worksheet);
            FormatNumberOfCreditsHeader(worksheet);
            FormatNameOfTeacherHeader(worksheet);
            RemoveLastNoHeaderColumn(worksheet);
        }

        public static async Task FormatAllSheetToToUsStyleAsync()
        {
            if (_workbook != null)
            {
                //var tasks = _workbook.Worksheets.Cast<Excel.Worksheet>().Select(FormatSheetToToUsStyleAsync);
                foreach (Excel.Worksheet worksheet in _workbook.Worksheets)
                {
                    await FormatASheetToToUsStyleAsync(worksheet as Excel.Worksheet);
                    //SetEntityDataColumn(worksheet);
                }
                SaveToSystem();
            }
        }

        public static void SetEntityDataColumn(Excel.Worksheet worksheet)
        {
            int colLength = _dataRangePositions[worksheet.Name].ColumnCount;
            Parallel.For(1, colLength + 1,
            (colIndex) =>
            {
                string header = GetValueCell(_dataRangePositions[worksheet.Name].Row, colIndex, worksheet);
                for (int rowIndex = _dataRangePositions[worksheet.Name].Row + 1; rowIndex <= _dataRangePositions[worksheet.Name].RowCount; rowIndex++)
                {
                    string value = GetValueCell(rowIndex, colIndex, worksheet);
                    if (_dataColumns.ContainsKey(header))
                    {
                        if (!_dataColumns[header].Contains(value))
                            _dataColumns[header].Add(value);
                    }
                    else
                    {
                        _dataColumns.Add(header, new List<string>());
                        _dataColumns[header].Add(value);
                    }
                }
            });
        }

        public static async Task ImportToDB()
        {
            //import name of subject id
            foreach (Excel.Worksheet worksheet in _workbook.Worksheets)
            {
                AddSubjectToEntity(worksheet);
                //MessageBox.Show("subject");
                AddClassToEntity(worksheet);
                //MessageBox.Show("class");

                AddTeacherToEntity(worksheet);
                //MessageBox.Show("teacher");

                AddSubjectManagerToEntity(worksheet);
                //MessageBox.Show("manager");
            }
        }

        public static void AddClassToEntity(Excel.Worksheet worksheet)
        {
            int idIndex = FindHeaderColumnIndex("MÃ LỚP", worksheet);
            int semesterIndex = FindHeaderColumnIndex("HỌC KỲ", worksheet);
            int endDateIndex = FindHeaderColumnIndex("NKT", worksheet);
            int startDateIndex = FindHeaderColumnIndex("NBD", worksheet);
            int yearIndex = FindHeaderColumnIndex("NĂM HỌC", worksheet);
            int dayOfWeekIndex = FindHeaderColumnIndex("THỨ", worksheet);
            int roomIndex = FindHeaderColumnIndex("PHÒNG HỌC", worksheet);
            int timeIndex = FindHeaderColumnIndex("TIẾT", worksheet);
            int numberOfStudentIndex = FindHeaderColumnIndex("SĨ SỐ", worksheet);
            int frequencyIndex = FindHeaderColumnIndex("CÁCH TUẦN", worksheet);
            int rowLength = _dataRangePositions[worksheet.Name].RowCount;
            for (int rowIndex = 1; rowIndex < rowLength; rowIndex++)
            {
                string classId = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, idIndex, worksheet);
                if (DataProvider.Instance.entities.Classes.Any(classToUs => classToUs.Id == classId))
                {
                    string dayOfWeek = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex,
                                                 dayOfWeekIndex,
                                                 worksheet);
                    var lession = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex,
                                               timeIndex,
                                               worksheet);

                    //MessageBox.Show(classId);

                    var firstClass = DataProvider.Instance.entities.Classes.Where(classToUs => classToUs.Id == classId).FirstOrDefault();

                    string[] seperator = new string[] { ", " };

                    string[] days = firstClass.DayInWeek.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                    string[] lessions = firstClass.Lession.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                    bool isExist = false;
                    for (int i = 0; i < days.Length; i++)
                    {
                        if (dayOfWeek == days[i] && lession == lessions[i])
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        firstClass.DayInWeek += ", " + dayOfWeek;
                        firstClass.Lession += ", " + lession;
                    }
                }
                else
                {
                    var classToUs = new Class();
                    classToUs.Id = classId;
                    classToUs.Semester = int.Parse(GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, semesterIndex, worksheet));
                    classToUs.EndDate = DateTime.Parse(GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, endDateIndex, worksheet));
                    classToUs.BeginDate = DateTime.Parse(GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, startDateIndex, worksheet));
                    classToUs.Year = int.Parse(GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, yearIndex, worksheet));
                    classToUs.DayInWeek = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, dayOfWeekIndex, worksheet);
                    classToUs.Frequency = int.Parse(GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, frequencyIndex, worksheet));
                    classToUs.Lession = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, timeIndex, worksheet);
                    classToUs.NumberOfStudents = int.Parse(GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, numberOfStudentIndex, worksheet));

                    DataProvider.Instance.entities.Classes.Add(classToUs);
                }
                DataProvider.Instance.entities.SaveChanges();
            }
        }

        public static void AddTeacherToEntity(Excel.Worksheet worksheet)
        {
            int idIndex = FindHeaderColumnIndex("MÃ GIẢNG VIÊN", worksheet);
            int teacherNameIndex = FindHeaderColumnIndex("TÊN GIẢNG VIÊN", worksheet);
            int rowLength = _dataRangePositions[worksheet.Name].RowCount;

            for (int rowIndex = 1; rowIndex < rowLength; rowIndex++)
            {
                string teacherId = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, idIndex, worksheet);
                //if (!String.IsNullOrEmpty(teacherId))
                //{
                if (!DataProvider.Instance.entities.Teachers.Any(teacher => teacher.Id == teacherId))
                {
                    var teacher = new Teacher();
                    teacher.Id = teacherId;
                    teacher.Name = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, teacherNameIndex, worksheet);

                    DataProvider.Instance.entities.Teachers.Add(teacher);
                    DataProvider.Instance.entities.SaveChanges();
                }
                //}
            }
        }

        public static void AddSubjectToEntity(Excel.Worksheet worksheet)
        {
            int idIndex = FindHeaderColumnIndex("MÃ MH", worksheet);
            int nameIndex = FindHeaderColumnIndex("TÊN MÔN HỌC", worksheet);
            int creditIndex = FindHeaderColumnIndex("SỐ TC", worksheet);
            int htgdIndex = FindHeaderColumnIndex("HTGD", worksheet);
            int systemIndex = FindHeaderColumnIndex("HỆ ĐT", worksheet);
            int facultyIndex = FindHeaderColumnIndex("KHOA QL", worksheet);
            int isLabIndex = FindHeaderColumnIndex("THỰC HÀNH", worksheet);
            int noteIndex = FindHeaderColumnIndex("GHICHU", worksheet);
            int languageIndex = FindHeaderColumnIndex("NGÔN NGỮ", worksheet);
            int rowLength = _dataRangePositions[worksheet.Name].RowCount;

            for (int rowIndex = 1; rowIndex < rowLength; rowIndex++)
            {
                string subjectId = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, idIndex, worksheet);

                if (!DataProvider.Instance.entities.Subjects.Any(subject => subject.Id == subjectId))
                {
                    var subject = new Subject();
                    subject.Id = subjectId;
                    subject.Name = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, nameIndex, worksheet);
                    subject.NumberOfDigits = int.Parse(GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, creditIndex, worksheet));
                    subject.HTGD = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, htgdIndex, worksheet);
                    subject.System = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, systemIndex, worksheet);
                    subject.Faculity = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, facultyIndex, worksheet);
                    subject.IsLab = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, isLabIndex, worksheet) == "1" ? true : false;
                    subject.Note = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, noteIndex, worksheet);
                    subject.Language = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, languageIndex, worksheet);
                    DataProvider.Instance.entities.Subjects.Add(subject);
                    DataProvider.Instance.entities.SaveChanges();
                }
            }
        }

        public static void AddSubjectManagerToEntity(Excel.Worksheet worksheet)
        {
            int rowLength = _dataRangePositions[worksheet.Name].RowCount;
            int subjectIndex = FindHeaderColumnIndex("MÃ MH", worksheet);
            int teacherIndex = FindHeaderColumnIndex("MÃ GIẢNG VIÊN", worksheet);
            int classIndex = FindHeaderColumnIndex("MÃ LỚP", worksheet);

            for (int rowIndex = 1; rowIndex < rowLength; rowIndex++)
            {
                string subjectId = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, subjectIndex, worksheet);
                string teacherId = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, teacherIndex, worksheet);
                string classId = GetValueCell(_dataRangePositions[worksheet.Name].Row + rowIndex, classIndex, worksheet);

                var subjectManager = new SubjectManager();
                subjectManager.SubjectId = subjectId;
                subjectManager.TeacherId = teacherId;
                subjectManager.ClassId = classId;
                subjectManager.ExcelPath = _pathAfterSave;
                DataProvider.Instance.entities.SubjectManagers.Add(subjectManager);
                DataProvider.Instance.entities.SaveChanges();
            }
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
            _pathAfterSave = newPath;
            _workbook?.SaveAs(Filename: newPath, AccessMode: Excel.XlSaveAsAccessMode.xlShared, Password: PASSWORD);
        }

        #endregion Methods
    }
}