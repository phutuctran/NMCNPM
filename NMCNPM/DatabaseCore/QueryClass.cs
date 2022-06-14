using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.DatabaseCore
{
    public struct DTP
    {
        public double diem;
        public string maDiem;
        public DTP(double _diem = -1, string _maDiem = "")
        {
            diem = _diem;
            maDiem = _maDiem;
        }
    }
    public class DiemThanhPhan
    {


        private bool haveTableDiemMon;
        private string maDiemMon;

        public string MaDiemMon
        {
            get { return maDiemMon; }
            set { maDiemMon = value; }
        }

        public bool HaveTableDiemMon
        {
            get { return haveTableDiemMon; }
            set { haveTableDiemMon = value; }
        }

        private string tenMH;
        public string TenMH
        {
            get { return tenMH; }
            set { tenMH = value; }
        }

        private string mamh;
        public string MaMH
        {
            get { return mamh; }
            set { mamh = value; }
        }

        private DTP ddgtx1;
        public DTP DDGTX1
        {
            get { return ddgtx1; }
            set { ddgtx1 = value; }
        }

        private DTP ddgtx2;
        public DTP DDGTX2
        {
            get { return ddgtx2; }
            set { ddgtx2 = value; }
        }

        private DTP ddgtx3;
        public DTP DDGTX3
        {
            get { return ddgtx3; }
            set { ddgtx3 = value; }
        }

        private DTP ddgtx4;
        public DTP DDGTX4
        {
            get { return ddgtx4; }
            set { ddgtx4 = value; }
        }

        private DTP ddggk;
        public DTP DDGGK
        {
            get { return ddggk; }
            set { ddggk = value; }
        }

        private DTP ddgck;
        public DTP DDGCK
        {
            get { return ddgck; }
            set { ddgck = value; }
        }

        private DTP ddgtrb;
        public DTP DDGTRB
        {
            get { return ddgtrb; }
            set { ddgtrb = value; }
        }

        public DiemThanhPhan(string _mamh, string _tenmh)
        {
            MaMH = _mamh;
            tenMH = _tenmh;
            ddgtx1 = new DTP(-1, "");
            ddgtx2 = new DTP(-1, "");
            ddgtx3 = new DTP(-1, "");
            ddgtx4 = new DTP(-1, "");
            ddggk = new DTP(-1, "");
            ddgck = new DTP(-1, "");
            ddgtrb = new DTP(-1, "");
        }

        public double UpdateDTB()
        {
            int f = 0;
            int heSo = 0;
            double tongDiem = 0;
            if (ddgtx1.diem != -1)
            {
                tongDiem += ddgtx1.diem;
                heSo++;
                f++;
            }
            if (ddgtx2.diem != -1)
            {
                tongDiem += ddgtx2.diem;
                heSo++;
                f++;
            }
            if (ddgtx3.diem != -1)
            {
                tongDiem += ddgtx3.diem;
                heSo++;
                f++;
            }
            if (ddgtx4.diem != -1)
            {
                tongDiem += ddgtx4.diem;
                heSo++;
                f++;
            }
            if (f == 0)
            {
                return -1;
            }
            if (ddggk.diem != -1)
            {
                tongDiem += ddggk.diem * 2;
                heSo += 2;
            }
            else
            {
                return -1;
            }    
            if (ddgck.diem != -1)
            {
                tongDiem += ddgck.diem * 3;
                heSo += 3;
            }
            else
            {
                return -1;
            }
            tongDiem = tongDiem / heSo;
            ddgtrb.diem = Math.Round(tongDiem, 1);
            return ddgtrb.diem;

        }
    }
    public class DiemTN
    {
        private string mabdtn;
        public string maBDTN
        {
            get { return mabdtn; }
            set { mabdtn = value; }
        }

        private string sbd;
        public string SBD
        {
            get { return sbd; }
            set { sbd = value; }
        }

        private string hotenHS;
        public string hoTenHS
        {
            get { return hotenHS; }
            set { hotenHS = value; }
        }

        private DateTime ngaysinh;
        public DateTime ngaySinh
        {
            get { return ngaysinh; }
            set { ngaysinh = value; }
        }

        private string mahs;
        public string maHS
        {
            get { return mahs; }
            set { mahs = value; }
        }

        private DateTime namthi;
        public DateTime namThi
        {
            get { return namthi; }
            set { namthi = value; }
        }

        private double toan;
        public double dToan
        {
            get { return toan; }
            set { toan = value; }
        }

        private double van;
        public double dVan
        {
            get { return van; }
            set { van = value; }
        }

        private double anh;
        public double dAnh
        {
            get { return anh; }
            set { anh = value; }
        }

        private double vatli;
        public double dVatLi
        {
            get { return vatli; }
            set { vatli = value; }
        }

        private double hoahoc;
        public double dHoaHoc
        {
            get { return hoahoc; }
            set { hoahoc = value; }
        }

        private double sinhhoc;
        public double dSinhhoc
        {
            get { return sinhhoc; }
            set { sinhhoc = value; }
        }

        private double lichsu;
        public double dLichSu
        {
            get { return lichsu; }
            set { lichsu = value; }
        }

        private double diali;
        public double dDiali
        {
            get { return diali; }
            set { diali = value; }
        }

        private double gdcd;
        public double dGDCD
        {
            get { return gdcd; }
            set { gdcd = value; }
        }

        public DiemTN(string _maBDTN, string _SBD, string _hoTen, string _maHS, DateTime _namThi)
        {
            mabdtn = _maBDTN;
            sbd = _SBD;
            hotenHS = _hoTen;
            maHS = _maHS;
            namthi = _namThi;
            toan = -1;
            van = -1;
            anh = -1;
            hoahoc = -1;
            vatli = -1;
            sinhhoc = -1;
            lichsu = -1;
            diali = -1;
            gdcd = -1;
        }

        public DiemTN()
        {
            mabdtn = "";
            sbd = "";
            hotenHS = "";
            maHS = "";
            toan = -1;
            van = -1;
            anh = -1;
            hoahoc = -1;
            vatli = -1;
            sinhhoc = -1;
            lichsu = -1;
            diali = -1;
            gdcd = -1;
        }
    }



}
