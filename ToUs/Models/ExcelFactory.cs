using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Navigation;

namespace ToUs.Models
{
    public static class ExcelImportDB
    {
        private static DataTableCollection _tableCollection;
        private static List<Class> _classes;
        private static List<Subject> _subjects;
        private static List<Teacher> _teachers;
        private static List<SubjectManager> _subjectManagers;

        private static readonly string[] _subjectHeaderComponents =
        {
            "MÃ MH", "TÊN MÔN HỌC", "SỐ TC", "HTGD","HỆ ĐT","KHOA QL", "THỰC HÀNH","GHICHU","NGÔN NGỮ"
        };

        private static readonly string[] _teacherHeaderComponents =
        {
            "MÃ GIẢNG VIÊN", "TÊN GIẢNG VIÊN"
        };

        private static readonly string[] _classHeaderComponents =
        {
            "MÃ LỚP", "HỌC KỲ", "NKT", "NBD", "NĂM HỌC", "THỨ","PHÒNG HỌC","TIẾT", "SĨ SỐ", "CÁCH TUẦN"
        };

        public static List<Class> ClassToUss => _classes ?? throw new ArgumentNullException("Classes does not exited");
        public static List<Subject> SubjectToUss => _subjects ?? throw new ArgumentNullException("Subjects does not exited");
        public static List<Teacher> TeacherToUss => _teachers ?? throw new ArgumentNullException("Teachers does not exited");
        public static List<SubjectManager> SubjectManagerToUss => _subjectManagers ?? throw new ArgumentNullException("SubjectManager does not exited");

        public static bool Connect()
        {
            try
            {
                _tableCollection = ExcelReader.ExcelDataTables;
                _subjects = null;
                _teachers = null;
                _classes = null;
                _subjectManagers = null;

                return true;
            }
            catch (NoDatasException)
            {
                MessageBox.Show("No data to import to db");
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private static List<Subject> GetAllSubjects()
        {
            List<Subject> subjects = new List<Subject>();
            foreach (DataTable dataTable in _tableCollection)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    string id = row["MÃ MH"].ToString();
                    //var list = DataProvider.Instance.entities.Subjects.BulkRead(oldSubjects);

                    if (!subjects.Any(subject => subject.Id == id))
                    {
                        Subject subject = new Subject()
                        {
                            Id = id,
                            Name = row["TÊN MÔN HỌC"].ToString(),
                            NumberOfDigits = int.Parse(row["SỐ TC"].ToString()),
                            HTGD = row["HTGD"].ToString(),
                            System = row["HỆ ĐT"].ToString(),
                            Faculity = row["KHOA QL"].ToString(),
                            IsLab = row["THỰC HÀNH"].ToString() == "1" ? true : false,
                            Language = row["NGÔN NGỮ"].ToString(),
                            Note = row["GHICHU"].ToString(),
                        };
                        subjects.Add(subject);
                    }
                }
            }
            return subjects;
        }

        private static List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            foreach (DataTable dataTable in _tableCollection)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    string id = row["MÃ GIẢNG VIÊN"].ToString();
                    //if not exitsted ma mh then add to subjects
                    if (!String.IsNullOrEmpty(id))
                    {
                        if (!teachers.Any(teacher => teacher.Id == id))
                        {
                            Teacher teacher = new Teacher()
                            {
                                Id = id,
                                Name = row["TÊN GIẢNG VIÊN"].ToString(),
                            };
                            teachers.Add(teacher);
                        }
                    }
                }
            }
            return teachers;
        }

        public static List<Class> GetAllClasses()
        {
            List<Class> classes = new List<Class>();
            //List<Class> classes = new List<Class>();
            foreach (DataTable dataTable in _tableCollection)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    string id = row["MÃ LỚP"].ToString();
                    //if not exitsted ma mh then add to subjects
                    if (!String.IsNullOrEmpty(id))
                    {
                        if (!classes.Any(classToUs => classToUs.Id == id))
                        {
                            Class classToUs = new Class()
                            {
                                Id = id,
                                Semester = int.Parse(row["HỌC KỲ"].ToString()),
                                BeginDate = DateTime.Parse(row["NBD"].ToString()),
                                EndDate = DateTime.Parse(row["NKT"].ToString()),
                                Year = int.Parse(row["NĂM HỌC"].ToString()),
                                DayInWeek = row["THỨ"].ToString(),
                                Room = row["PHÒNG HỌC"].ToString(),
                                Lession = row["TIẾT"].ToString(),
                                NumberOfStudents = int.Parse(row["SĨ SỐ"].ToString()),
                                Frequency = int.Parse(row["CÁCH TUẦN"].ToString())
                            };

                            classes.Add(classToUs);
                        }
                    }
                }
            }
            return classes;
        }

        public static List<SubjectManager> GetAllSubjectManager()
        {
            List<SubjectManager> subjectManagers = new List<SubjectManager>();
            foreach (DataTable dataTable in _tableCollection)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var subjectManager = new SubjectManager()
                    {
                        SubjectId = row["MÃ MH"].ToString(),
                        ClassId = row["MÃ LỚP"].ToString(),
                        TeacherId = String.IsNullOrEmpty(row["MÃ GIẢNG VIÊN"].ToString()) ? null : row["MÃ GIẢNG VIÊN"].ToString(),
                        ExcelPath = ExcelReader.FilePath,
                        IsDelete = false
                    };
                    subjectManagers.Add(subjectManager);
                }
            }

            return subjectManagers;
        }

        private static string GetDuplicateRecordId(string errorMessage)
        {
            string id = "";
            char[] seperatorChars = { '(', ')' };
            string[] result = errorMessage.Split(seperatorChars);
            if (result.Length == 3)
                id = result[1];
            //MessageBox.Show(id);

            return id;
        }

        public static async Task ImportSubjectManager()
        {
            _subjectManagers = null;

            var tokenSource = new CancellationTokenSource();

            // Lấy token - để sử dụng bởi task, khi task thực thi token.IsCancellationRequested là
            // true nếu có phát yêu cầu dừng bằng cách gọi tokenSource.Cancel
            var token = tokenSource.Token;
            Task task = new Task(async () =>
            {
                List<SubjectManager> subjectManagers = GetAllSubjectManager();
                string invalidRecordId;
                do
                {
                    try
                    {
                        if (token.IsCancellationRequested)
                        {
                            MessageBox.Show("Subject manager task STOP");
                            token.ThrowIfCancellationRequested();
                            return;
                        }
                        invalidRecordId = null;
                        if (subjectManagers.Count > 0)
                        {
                            await DataProvider.Instance.entities.BulkInsertAsync(subjectManagers);
                            await DataProvider.Instance.entities.BulkSaveChangesAsync();

                            _subjectManagers = subjectManagers;
                        }
                    }
                    catch (Exception e)
                    {
                        if (!(e.Message.Contains('(') && e.Message.Contains(')')))
                        {
                            MessageBox.Show(e.Message);
                            //return;
                        }
                        invalidRecordId = GetDuplicateRecordId(e.Message);
                        if (invalidRecordId != null)
                        {
                            SubjectManager subjectManager = subjectManagers.FirstOrDefault(subjectManagerChecked => subjectManagerChecked.Id.ToString() == invalidRecordId);
                            if (subjectManager != null)
                            {
                                subjectManagers.Remove(subjectManager);
                            }
                        }
                    }
                } while (invalidRecordId != null);
            });
            task.Start();
            await task;
        }

        public static async Task ImportClass()
        {
            _classes = null;
            var tokenSource = new CancellationTokenSource();

            // Lấy token - để sử dụng bởi task, khi task thực thi token.IsCancellationRequested là
            // true nếu có phát yêu cầu dừng bằng cách gọi tokenSource.Cancel
            var token = tokenSource.Token;
            Task task = new Task(async () =>
            {
                List<Class> classes = GetAllClasses();
                string invalidRecordId;
                do
                {
                    try
                    {
                        if (token.IsCancellationRequested)
                        {
                            MessageBox.Show("Class task STOP");
                            token.ThrowIfCancellationRequested();
                            return;
                        }
                        invalidRecordId = null;
                        if (classes.Count > 0)
                        {
                            await DataProvider.Instance.entities.BulkInsertAsync(classes);
                            await DataProvider.Instance.entities.BulkSaveChangesAsync();

                            _classes = classes;
                        }
                    }
                    catch (Exception e)
                    {
                        invalidRecordId = GetDuplicateRecordId(e.Message);
                        if (invalidRecordId != null)
                        {
                            Class classToUs = classes.FirstOrDefault(classChecked => classChecked.Id == invalidRecordId);
                            if (classToUs != null)
                            {
                                classes.Remove(classToUs);
                            }
                        }
                    }
                } while (invalidRecordId != null);
            });
            task.Start();
            await task;
        }

        public static async Task ImportTeacher()
        {
            _teachers = null;
            var tokenSource = new CancellationTokenSource();

            // Lấy token - để sử dụng bởi task, khi task thực thi token.IsCancellationRequested là
            // true nếu có phát yêu cầu dừng bằng cách gọi tokenSource.Cancel
            var token = tokenSource.Token;
            Task task = new Task(async () =>
            {
                List<Teacher> teachers = GetAllTeachers();

                string invalidRecordId;
                do
                {
                    try
                    {
                        if (token.IsCancellationRequested)
                        {
                            MessageBox.Show("Teacher task STOP");
                            token.ThrowIfCancellationRequested();
                            return;
                        }
                        invalidRecordId = null;
                        if (teachers.Count > 0)
                        {
                            await DataProvider.Instance.entities.BulkInsertAsync(teachers);
                            await DataProvider.Instance.entities.BulkSaveChangesAsync();

                            _teachers = teachers;
                        }
                    }
                    catch (Exception e)
                    {
                        invalidRecordId = GetDuplicateRecordId(e.Message);
                        if (invalidRecordId != null)
                        {
                            Teacher teacher = teachers.FirstOrDefault(teacherChecked => teacherChecked.Id == invalidRecordId);
                            if (teacher != null)
                            {
                                teachers.Remove(teacher);
                            }
                        }
                    }
                } while (invalidRecordId != null);
            });
            task.Start();
            await task;
        }

        public static async Task ImportSubject()
        {
            _subjects = null;
            var tokenSource = new CancellationTokenSource();

            // Lấy token - để sử dụng bởi task, khi task thực thi token.IsCancellationRequested là
            // true nếu có phát yêu cầu dừng bằng cách gọi tokenSource.Cancel
            var token = tokenSource.Token;
            Task task = new Task(async () =>
            {
                List<Subject> subjects = GetAllSubjects();

                string invalidRecordId;
                do
                {
                    try
                    {
                        if (token.IsCancellationRequested)
                        {
                            MessageBox.Show("Subject task STOP");
                            token.ThrowIfCancellationRequested();
                            return;
                        }
                        invalidRecordId = null;
                        if (subjects.Count > 0)
                        {
                            await DataProvider.Instance.entities.BulkInsertAsync(subjects);
                            await DataProvider.Instance.entities.BulkSaveChangesAsync();

                            _subjects = subjects;
                        }
                    }
                    catch (Exception e)
                    {
                        invalidRecordId = GetDuplicateRecordId(e.Message);
                        if (invalidRecordId != null)
                        {
                            Subject subject = subjects.FirstOrDefault(subjectChecked => subjectChecked.Id == invalidRecordId);
                            if (subject != null)
                            {
                                subjects.Remove(subject);
                            }
                        }
                    }
                } while (invalidRecordId != null);
            });
            task.Start();
            await task;
        }

        public static async Task ImportToDB()
        {
            var tokenSource = new CancellationTokenSource();

            // Lấy token - để sử dụng bởi task, khi task thực thi token.IsCancellationRequested là
            // true nếu có phát yêu cầu dừng bằng cách gọi tokenSource.Cancel
            var token = tokenSource.Token;
            try
            {
                Task subjectTask = ImportSubject();
                Task teacherTask = ImportTeacher();
                Task classTask = ImportClass();
                Task subjectManagerTask = ImportSubjectManager();

                await Task.Delay(3000);
                Task.WaitAll(subjectTask, teacherTask, classTask, subjectManagerTask);

                MessageBox.Show("File đã được load thành công");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            //await classTask;
        }
    }

    public static class ExcelReader
    {
        private const string STORAGE_RELATIVE_PATH = @".\..\..\Resources\Clients\Excels";
        private const string FORMAT = @".xlsx";
        private const string STORED_FILE_NAME_SUFFIEX = @"_ToUs_";
        private static readonly string[] LANGUAGES = { "EN", "VN", "JP" };
        private static DataTableCollection _dataTableCollection;
        private static string _path = String.Empty;
        private static bool _noInvalidRow = false;
        private static bool _noInvalidColumn = false;

        public static string FilePath
        {
            get
            {
                if (!File.Exists(_path))
                    throw new FileNotFoundException("File not found");
                return _path;
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

        public static DataTableCollection ExcelDataTables
        {
            get
            {
                if (_dataTableCollection.Count == 0)
                    throw new NoDatasException("No data to get");
                return _dataTableCollection;
            }
        }

        public static bool Open(string path)
        {
            try
            {
                _noInvalidColumn = false;
                _noInvalidRow = false;
                _path = SaveToSystem(path);

                //Open system file amd returns it as a stream
                using (FileStream stream = File.Open(_path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        //Get all the Tables
                        _dataTableCollection = result.Tables;
                    }
                }
                if (!IsTimeManagementExcelFile())
                {
                    throw new NotCorrectFileException();
                }
                return true;
            }
            catch (FileNotFoundException fileNotFound)
            {
                MessageBox.Show(fileNotFound.Message);
                File.Delete(_path);
                return false;
            }
            catch (NotCorrectFileException notCorrectFile)
            {
                MessageBox.Show(notCorrectFile.Message);
                File.Delete(_path);
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                File.Delete(_path);
                return false;
            }
        }

        private static bool IsTimeManagementExcelFile()
        {
            string[] wordKeys = { "Time", "Management", "THỜI KHÓA BIỂU", "DANH SÁCH LỚP" };
            foreach (DataTable dataTable in _dataTableCollection)
            {
                if (dataTable.Rows.Count < 10)
                    return false;
                for (int i = 0; i < 10; i++)
                {
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        if (wordKeys.Any(wordKey => dataTable.Rows[i][dataColumn].ToString().Contains(wordKey)))
                            return true;
                    }
                }
            }
            return false;
        }

        private static bool IsRowInvalid(DataRow row)
        {
            int spaceCount = 0;
            foreach (DataColumn col in row.Table.Columns)
            {
                if (String.IsNullOrEmpty(row[col].ToString()))
                {
                    spaceCount++;
                    if (spaceCount > 7)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Remove invalid rowdata use table index
        /// </summary>
        /// <param name="tableIndex"> </param>
        private static void RemoveInvalidRowData(int tableIndex)
        {
            DataTable dataTable = _dataTableCollection[tableIndex];
            dataTable.Rows.Cast<DataRow>().Where(IsRowInvalid).ToList().ForEach(r => r.Delete());
            dataTable.AcceptChanges();
        }

        /// <summary>
        /// Remove invalid rowdata use table name
        /// </summary>
        /// <param name="tableName"> </param>
        private static void RemoveInvalidRowData(string tableName)
        {
            DataTable dataTable = _dataTableCollection[tableName];
            dataTable.Rows.Cast<DataRow>().Where(IsRowInvalid).ToList().ForEach(r => r.Delete());
            dataTable.AcceptChanges();
        }

        private static void RemoveInvalidRowData(DataTable dataTable)
        {
            dataTable.Rows.Cast<DataRow>().Where(IsRowInvalid).ToList().ForEach(r => r.Delete());
            dataTable.AcceptChanges();
        }

        /// <summary>
        /// Remove all invalid row for all data table
        /// </summary>
        private static void RemoveInvalidRowData()
        {
            if (_noInvalidRow == false)
            {
                foreach (DataTable table in _dataTableCollection)
                {
                    table.Rows.Cast<DataRow>().Where(IsRowInvalid).ToList().ForEach(r => r.Delete());
                    table.AcceptChanges();
                    _noInvalidRow = true;
                }
            }
        }

        private static bool IsLangugeColumn(DataColumn dataColumn)
        {
            int rowCount = dataColumn.Table.Rows.Count;
            int numberOfRowChecking = 5 < rowCount ? 5 : rowCount - 1;
            int rowCheckIndex = 1;
            while (numberOfRowChecking > 0)
            {
                bool isLanguage = false;

                string valueChecked = dataColumn.Table.Rows[rowCheckIndex][dataColumn].ToString();
                foreach (string lang in LANGUAGES)
                {
                    if (valueChecked == lang)
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

        private static void ChangeHeaderData(Func<DataColumn> thingToDo, string newName)
        {
            DataColumn dataColumn = thingToDo();
            if (dataColumn != null)
                dataColumn.Table.Rows[0][dataColumn] = newName;
        }

        private static void FormatColumn(DataTable dataTable)
        {
            if (!_noInvalidRow)
                RemoveInvalidRowData(dataTable);

            //format language column
            ChangeHeaderData(() => dataTable.Columns.Cast<DataColumn>().Where(IsLangugeColumn).ToList().FirstOrDefault(), "NGÔN NGỮ");

            //format trợ giảng
            ChangeHeaderData(() => dataTable.Columns.Cast<DataColumn>()
                                                    .Where(c => c.Table.Rows[0][c].ToString().Trim() == "TÊN TRỢ GIẢNG")
                                                    .ToList().FirstOrDefault(), "TÊN GIẢNG VIÊN");
            ChangeHeaderData(() => dataTable.Columns.Cast<DataColumn>()
                                                    .Where(c => c.Table.Rows[0][c].ToString().Trim() == "TỐ TC")
                                                    .ToList().FirstOrDefault(), "SỐ TC");
            string invalidColumnName = dataTable.Columns.Cast<DataColumn>()
                                                        .Where(c => String.IsNullOrEmpty(c.Table.Rows[0][c].ToString().Trim()))
                                                        .ToList().FirstOrDefault()?.ColumnName;
            if (!String.IsNullOrEmpty(invalidColumnName))
                dataTable.Columns.Remove(invalidColumnName);
            dataTable.AcceptChanges();
        }

        private static void FormatColumn()
        {
            foreach (DataTable dataTable in _dataTableCollection)
            {
                FormatColumn(dataTable);
            }
        }

        /// <summary>
        /// Set the column name at one data table
        /// </summary>
        /// <param name="dataTable"> </param>
        private static void SetColumnName(DataTable dataTable)
        {
            if (!_noInvalidRow)
                RemoveInvalidRowData(dataTable);
            if (!_noInvalidColumn)
                FormatColumn(dataTable);
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                string newName = dataTable.Rows[0][i].ToString();
                if (!String.IsNullOrEmpty(newName))
                    dataTable.Columns[i].ColumnName = newName;
            }
            dataTable.Rows[0].Delete();

            dataTable.AcceptChanges();
        }

        /// <summary>
        /// Set column name for all table
        /// </summary>
        private static void SetColumnName()
        {
            foreach (DataTable dataTable in _dataTableCollection)
            {
                SetColumnName(dataTable);
            }
        }

        public static void FormatExcelDatas()
        {
            RemoveInvalidRowData();
            FormatColumn();
            SetColumnName();
        }

        public static int GetLastSystemNumFile()
        {
            int lastNum = 0;
            foreach (string path in Directory.GetFiles(StoragePath))
            {
                string fileName = Path.GetFileNameWithoutExtension(path);
                string[] splitCharators = { STORED_FILE_NAME_SUFFIEX };
                string[] result = fileName.Split(splitCharators, StringSplitOptions.RemoveEmptyEntries);

                if (result.Length == 2)
                {
                    string suffix = result[1];
                    int num = int.Parse(suffix);
                    if (num > lastNum)
                        lastNum = num;
                }
            }
            return lastNum;
        }

        /// <summary>
        /// Create a path to save to the excel storage follow ToUs rule
        /// </summary>
        /// <returns> </returns>
        public static string GenerateSystemPath(string oldPath)
        {
            int nextNum = GetLastSystemNumFile() + 1;
            string oldFileName = Path.GetFileNameWithoutExtension(oldPath);
            string newFileName = oldFileName + STORED_FILE_NAME_SUFFIEX + nextNum.ToString() + FORMAT;
            return Path.Combine(StoragePath, newFileName);
        }

        /// <summary>
        /// Save a copy of excel file to excel storage with the ToUs rule
        /// </summary>
        public static string SaveToSystem(string oldPath)
        {
            try
            {
                string newPath = GenerateSystemPath(oldPath);
                File.Copy(oldPath, newPath);
                return newPath;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
    }
}