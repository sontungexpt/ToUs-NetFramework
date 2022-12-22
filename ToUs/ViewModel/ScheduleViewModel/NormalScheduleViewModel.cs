using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToUs.Utilities;

namespace ToUs.ViewModel.ScheduleViewModel
{
    class NormalScheduleViewModel: ViewModelBase
    {
        private List<Subject> _subjects;


        public List<Subject> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; OnPropertyChanged(); }
        }

        public NormalScheduleViewModel()
        {
            Subjects = new List<Subject>
            {
                new Subject { Mon = "Lịch sử Đảng Cộng sản", MaLop = "IT007.N12.PMCL.2", GiangVien = "Đặng Lê Bảo Chương", SoTC="3", Thu="5", Tiet="678", HeDT="CQUI", KhoaQL="CNPM", HTDG="QUU", CachTuan="2" },
                new Subject { Mon = "Lịch sử Đảng Cộng sản", MaLop = "IT007.N12.PMCL.2", GiangVien = "Đặng Lê Bảo Chương", SoTC="3", Thu="5", Tiet="678", HeDT="CQUI", KhoaQL="CNPM", HTDG="QUU", CachTuan="2" },
                new Subject { Mon = "Lịch sử Đảng Cộng sản", MaLop = "IT007.N12.PMCL.2", GiangVien = "Đặng Lê Bảo Chương", SoTC="3", Thu="5", Tiet="678", HeDT="CQUI", KhoaQL="CNPM", HTDG="QUU", CachTuan="2" },
                new Subject { Mon = "Lịch sử Đảng Cộng sản", MaLop = "IT007.N12.PMCL.2", GiangVien = "Đặng Lê Bảo Chương", SoTC="3", Thu="5", Tiet="678", HeDT="CQUI", KhoaQL="CNPM", HTDG="QUU", CachTuan="2" },
                new Subject { Mon = "Lịch sử Đảng Cộng sản", MaLop = "IT007.N12.PMCL.2", GiangVien = "Đặng Lê Bảo Chương", SoTC="3", Thu="5", Tiet="678", HeDT="CQUI", KhoaQL="CNPM", HTDG="QUU", CachTuan="2" },
                new Subject { Mon = "Lịch sử Đảng Cộng sản", MaLop = "IT007.N12.PMCL.2", GiangVien = "Đặng Lê Bảo Chương", SoTC="3", Thu="5", Tiet="678", HeDT="CQUI", KhoaQL="CNPM", HTDG="QUU", CachTuan="2" },
                new Subject { Mon = "Lịch sử Đảng Cộng sản", MaLop = "IT007.N12.PMCL.2", GiangVien = "Đặng Lê Bảo Chương", SoTC="3", Thu="5", Tiet="678", HeDT="CQUI", KhoaQL="CNPM", HTDG="QUU", CachTuan="2" },
                new Subject { Mon = "Lịch sử Đảng Cộng sản", MaLop = "IT007.N12.PMCL.2", GiangVien = "Đặng Lê Bảo Chương", SoTC="3", Thu="5", Tiet="678", HeDT="CQUI", KhoaQL="CNPM", HTDG="QUU", CachTuan="2" },
                new Subject { Mon = "Lịch sử Đảng Cộng sản", MaLop = "IT007.N12.PMCL.2", GiangVien = "Đặng Lê Bảo Chương", SoTC="3", Thu="5", Tiet="678", HeDT="CQUI", KhoaQL="CNPM", HTDG="QUU", CachTuan="2" },
            };
        }
    }

    class Subject
    {
        public string Mon { get; set; }
        public string MaLop { get; set; }
        public string GiangVien { get; set; }
        public string SoTC { get; set; }
        public string Thu { get; set; }
        public string Tiet { get; set; }
        public string HeDT { get; set; }
        public string KhoaQL { get; set; }
        public string HTDG { get; set; }
        public string CachTuan { get; set; }

    }
}
