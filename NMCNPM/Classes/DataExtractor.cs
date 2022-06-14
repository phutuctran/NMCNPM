using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Classes
{
    class DataExtractor
    {
        private List<TaiKhoan> tk = new List<TaiKhoan>();
        private List<Khoi> khoi = new List<Khoi>();
        private List<MonHoc> mh = new List<MonHoc>();
        private List<GiaoVien> gv = new List<GiaoVien>();
        private List<Lop> lop = new List<Lop>();
        private List<HocSinh> hs = new List<HocSinh>();
        private List<HanhKiem> hk = new List<HanhKiem>();
        private List<DiemMon> dm = new List<DiemMon>();
        private List<LoaiKiemTra> lkt = new List<LoaiKiemTra>();
        private List<ChiTietDiem> ctd = new List<ChiTietDiem>();
        private List<GiangDay> gd = new List<GiangDay>();

        public DataExtractor() { }

        public void ReadCSVFile()
        {
            string tkFile = "";
            string khoiFile = "";
            string mhFile = "";
            string gvFile = "";
            string lopFile = "";
            string hsFile = "";
            string hkFile = "";
            string dmFile = "";
            string lktFile = "";
            string ctdFile = "";
            string gdFile = "";

            tk = TaiKhoan.ReadCSVFile(tkFile);
            khoi = Khoi.ReadCSVFile(khoiFile);
            mh = MonHoc.readCSVFile(mhFile);
            gv = GiaoVien.readCSVFile(gvFile);
            lop = Lop.ReadCSVFile(lopFile);
            hs = HocSinh.readCSVFile(hsFile);
            hk = HanhKiem.ReadCSVFile(hkFile);
            dm = DiemMon.ReadCSVFile(dmFile);
            lkt = LoaiKiemTra.ReadCSVFile(lktFile);
            ctd = ChiTietDiem.ReadCSVFile(ctdFile);
            gd = GiangDay.ReadCSVFile(gdFile);

            TaiKhoan.InsertToDB(tk);
            Khoi.InsertToDB(khoi);
            MonHoc.InsertToDB(mh);
            GiaoVien.InsertToDB(gv);
            Lop.InsertToDB(lop);
            HocSinh.InsertToDB(hs);
            HanhKiem.InsertToDB(hk);
            DiemMon.InsertToDB(dm);
            LoaiKiemTra.InsertToDB(lkt);
            ChiTietDiem.InsertToDB(ctd);
            GiangDay.InsertToDB(gd);
        }
    }
}
