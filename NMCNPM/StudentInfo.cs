using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using StudentManagementSystem.CustomControls;
using StudentManagementSystem.DatabaseCore;

namespace StudentManagementSystem
{
    public partial class StudentInfo : MaterialForm
    {
        string MaHS;
        List<(string, string)> ttBangDiem = new List<(string, string)>(); //thông tin học kì, năm học cho từng bảng điểm
        public StudentInfo(string _MaHS) //_MaHS phải luôn tồn tại
        {
            InitializeComponent();
            MaHS = _MaHS;
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            //materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Indigo600, MaterialSkin.Primary.Indigo800, MaterialSkin.Primary.Indigo100, MaterialSkin.Accent.Pink200, MaterialSkin.TextShade.WHITE);
            //InitCustomLabelFont();
            //LV_BangDiem.Hide();
            GetDataStudent();

        }

        void GetDataStudent()
        {
            //Thông tin học sinh
            string query = @"SELECT HS.MAHS, HS.HotenHS, HS.NgaySinh, HS.diachi, HS.gioitinh, HS.nienkhoa, HS.dantoc,
                            HS.tongiao, HS.tencha, HS.nghenghiepcha, HS.ngaysinhcha, HS.tenme, HS.nghenghiepme, 
                            HS.ngaysinhme, HS.ghichu, L.TENLOP, HS.noisinh, HS.email, HS.sodt, HS.noisinh
                            FROM HOCSINH AS HS
                            LEFT JOIN LOP AS L ON L.MALOP = HS.MALOP
                            WHERE HS.MAHS = " + $"'{MaHS}'";

            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        LB_MaHS.Text = @"Mã học sinh: " + (rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim());
                        LB_HoTenHS.Text = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                        LB_NgaySinh.Text = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetDateTime(2).ToString("dd/MM/yyyy"); ;//DateTime.ParseExact(rdr.GetString(2), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                        LB_DiaChi.Text = rdr.IsDBNull(3) ? GlobalProperties.NULLFIELD : rdr.GetString(3);
                        LB_GioiTinh.Text = rdr.IsDBNull(4) ? GlobalProperties.NULLFIELD : rdr.GetString(4);
                        LB_NienKhoa.Text = rdr.IsDBNull(5) ? GlobalProperties.NULLFIELD : rdr.GetString(5);
                        LB_DanToc.Text = rdr.IsDBNull(6) ? GlobalProperties.NULLFIELD : rdr.GetString(6);
                        LB_TonGiao.Text = rdr.IsDBNull(7) ? GlobalProperties.NULLFIELD : rdr.GetString(7);
                        LB_TenCha.Text = rdr.IsDBNull(8) ? GlobalProperties.NULLFIELD : rdr.GetString(8);
                        LB_NgheNghiepCha.Text = rdr.IsDBNull(9) ? GlobalProperties.NULLFIELD : rdr.GetString(9);
                        LB_NamSinhCha.Text = rdr.IsDBNull(10) ? GlobalProperties.NULLFIELD : rdr.GetDateTime(10).ToString("yyyy");
                        LB_TenMe.Text = rdr.IsDBNull(11) ? GlobalProperties.NULLFIELD : rdr.GetString(11);
                        LB_NgheNghiepMe.Text = rdr.IsDBNull(12) ? GlobalProperties.NULLFIELD : rdr.GetString(12);
                        LB_NamSinhMe.Text = rdr.IsDBNull(13) ? GlobalProperties.NULLFIELD : rdr.GetDateTime(13).ToString("yyyy");
                        LB_GhiChu.Text = rdr.IsDBNull(14) ? GlobalProperties.NULLFIELD : rdr.GetString(14);
                        LB_Lop.Text = @"Lớp: " + (rdr.IsDBNull(15) ? GlobalProperties.NULLFIELD : rdr.GetString(15).Trim());
                        LB_NoiSinh.Text = rdr.IsDBNull(16) ? GlobalProperties.NULLFIELD : rdr.GetString(16);
                    }

                }
            }

            string sql = $"SELECT ANHHS FROM HOCSINH WHERE MAHS = '{MaHS}'";
            cmd = new SqlCommand(sql, GlobalProperties.conn);

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader["ANHHS"] != null)
                        {
                            byte[] byteBLOBData = (byte[])reader["ANHHS"];
                            Image img = PB_Avatar.Image;
                            PB_Avatar.Image = GlobalFunction.ToImage(byteBLOBData);
                            if (img != null)
                            {
                                img.Dispose();
                            }
                        }

                    }
                }
            }
            catch { }


            ////Thông tin điểm tuyển sinh
            //query = @"SELECT D.SBD, HS.HotenHS, HS.NgaySinh, D.NAMTHI, D.TOAN, D.VAN, D.ANH, D.MONCHUYEN
            //        FROM DIEMDAUVAO AS D
            //        RIGHT JOIN HOCSINH AS HS ON HS.MAHS = D.MAHS
            //        WHERE HS.MAHS = " + $"'{MaHS}'";
            //cmd = new SqlCommand(query, GlobalProperties.conn);

            //using (SqlDataReader rdr = cmd.ExecuteReader())
            //{
            //    if (rdr.HasRows)
            //    {
            //        while (rdr.Read())
            //        {

            //            LB_SBD_TS.Text = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
            //            LB_HoTen_TS.Text = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
            //            LB_NS_TS.Text = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetDateTime(2).ToString("dd/MM/yyyy"); //DateTime.ParseExact(rdr.GetString(2), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
            //            LB_NamThi_TS.Text = rdr.IsDBNull(3) ? GlobalProperties.NULLFIELD : rdr.GetString(3);
            //            LB_dToan_TS.Text = rdr.IsDBNull(4) ? GlobalProperties.NULLFIELD : rdr.GetDouble(4).ToString();
            //            LB_dVan_TS.Text = rdr.IsDBNull(5) ? GlobalProperties.NULLFIELD : rdr.GetDouble(5).ToString();
            //            LB_dAnh_TS.Text = rdr.IsDBNull(6) ? GlobalProperties.NULLFIELD : rdr.GetDouble(6).ToString();
            //            LB_dChuyen_TS.Text = rdr.IsDBNull(7) ? GlobalProperties.NULLFIELD : rdr.GetDouble(7).ToString();
            //        }

            //    }
            //}

            ////Thông tin điểm tốt nghiệp:
            //query = @"SELECT HS.HotenHS, HS.NgaySinh, D.SBD, D.NAMTHI, D.TOAN, D.VAN, D.ANH, TN.VATLI, TN.HOAHOC, TN.SINHHOC, XH.LICHSU, XH.DIALI, XH.GDCD
            //        FROM DIEMTHITN AS D
            //        LEFT JOIN BANGDIEMTOHOPTUNHIEN AS TN ON TN.MABDTN = D.MABDTN
            //        LEFT JOIN BANGDIEMTOHOPXAHOI AS XH ON XH.MABDTN = D.MABDTN
            //        RIGHT JOIN HOCSINH AS HS ON HS.MAHS = D.MAHS
            //        WHERE HS.MAHS = " + $"'{MaHS}'";
            //cmd = new SqlCommand(query, GlobalProperties.conn);

            //using (SqlDataReader rdr = cmd.ExecuteReader())
            //{
            //    if (rdr.HasRows)
            //    {
            //        while (rdr.Read())
            //        {
            //            LB_HoTen_TN.Text = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
            //            Lb_NgaySinh_TN.Text = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetDateTime(1).ToString("dd/MM/yyyy");
            //            LB_SBD_TN.Text = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetString(2).Trim();
            //            Lb_NamThi_TN.Text = rdr.IsDBNull(3) ? GlobalProperties.NULLFIELD : rdr.GetString(3).Trim();
            //            LB_dToan_TN.Text = rdr.IsDBNull(4) ? GlobalProperties.NULLFIELD : rdr.GetDouble(4).ToString();
            //            LB_dVan_TN.Text = rdr.IsDBNull(5) ? GlobalProperties.NULLFIELD : rdr.GetDouble(5).ToString();
            //            LB_dAnh_TN.Text = rdr.IsDBNull(6) ? GlobalProperties.NULLFIELD : rdr.GetDouble(6).ToString();
            //            LB_dVatLi_TN.Text = rdr.IsDBNull(7) ? GlobalProperties.NULLFIELD : rdr.GetDouble(7).ToString();
            //            LB_dHoaHoc_TN.Text = rdr.IsDBNull(8) ? GlobalProperties.NULLFIELD : rdr.GetDouble(8).ToString();
            //            LB_dSinhHoc_TN.Text = rdr.IsDBNull(9) ? GlobalProperties.NULLFIELD : rdr.GetDouble(9).ToString();
            //            LB_dLichSu_TN.Text = rdr.IsDBNull(10) ? GlobalProperties.NULLFIELD : rdr.GetDouble(10).ToString();
            //            LB_dDiaLi_TN.Text = rdr.IsDBNull(11) ? GlobalProperties.NULLFIELD : rdr.GetDouble(11).ToString();
            //            LB_dGDCD_TN.Text = rdr.IsDBNull(12) ? GlobalProperties.NULLFIELD : rdr.GetDouble(12).ToString();
            //        }
            //    }
            //}

            ////Lấy thông tin về học kì và năm học:
            query = @"SELECT DISTINCT DM.MAHK, DM.NAMHOC
                    FROM DIEMMON AS DM
                    LEFT JOIN HOCSINH AS HS ON HS.MAHS = DM.MAHOCSINH
                    WHERE HS.MAHS = " + $"'{MaHS}'" + @"ORDER BY DM.NAMHOC ASC, DM.MAHK ASC";
            cmd = new SqlCommand(query, GlobalProperties.conn);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        string hk = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                        string namHoc = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                        ttBangDiem.Add(((string, string))(hk, namHoc));
                    }
                }
            }
            if (ttBangDiem.Count > 0)
            {
                CB_ttHK_NH.Items.Clear();
                foreach ((string, string) p in ttBangDiem)
                {
                    CB_ttHK_NH.Items.Add(p.Item1 + ", " + p.Item2);
                }
            }
        }
        /*private void InitCustomLabelFont()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            int fontLength = Properties.Resources.Roboto_Light.Length;
            byte[] fontdata = Properties.Resources.Roboto_Light;
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);
            LB_HoTenHS.Font = new Font(pfc.Families[0], LB_HoTenHS.Font.Size);
            LB_HoTenHS.ForeColor = Color.FromArgb(165, 194, 214);
        }*/
        private void LB_HoTenHS_Paint(object sender, PaintEventArgs e)
        {
            LB_HoTenHS.Location = new Point(PB_Avatar.Location.X + PB_Avatar.Width / 2 - LB_HoTenHS.Width / 2, 265);
        }

        private void LB_MaHS_Paint(object sender, PaintEventArgs e)
        {
            LB_MaHS.Location = new Point(PB_Avatar.Location.X + PB_Avatar.Width / 2 - LB_MaHS.Width / 2, 305);
        }

        private void LB_Lop_Paint(object sender, PaintEventArgs e)
        {
            LB_Lop.Location = new Point(PB_Avatar.Location.X + PB_Avatar.Width / 2 - LB_Lop.Width / 2, 345);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            this.PB_Avatar.Dispose();
            //this.pictureBox1.Dispose();
        }

        private void btn_xem_Click(object sender, EventArgs e)
        {
            int idx = CB_ttHK_NH.SelectedIndex;
            if (idx < 0)
            {
                MessageBox.Show("Hãy chọn thông tin cần xem", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = @"SELECT MN.MAMH, MN.TENMH, CTD.MADIEMMON, CTD.DIEM, LKT.TENLOAIKT, DM.TRUNGBINH
                            FROM CHITIETDIEM AS CTD
                            INNER JOIN DIEMMON AS DM ON CTD.MADIEMMON = DM.MADIEMMON 
                            LEFT JOIN LOAIKIEMTRA AS LKT ON LKT.MALOAIKT = CTD.MALOAIKT
                            LEFT JOIN HOCSINH AS HS ON HS.MAHS = DM.MAHOCSINH
                            LEFT JOIN MONHOC AS MN ON MN.MAMH = DM.MAMONHOC " +
                            $"WHERE DM.MAHOCSINH = '{MaHS}' AND DM.MAHK = '{ttBangDiem[idx].Item1}' AND DM.NAMHOC = '{ttBangDiem[idx].Item2}'";

            LV_BangDiem.Items.Clear();
            List<DiemThanhPhan> diem = new List<DiemThanhPhan>();
            for (int i = 0; i < 13; i++)
            {
                diem.Add(new DiemThanhPhan(GlobalProperties.listMaMH[i], GlobalProperties.listTenMH[i]));
            }
            string maDiemMon;
            //13 columns
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);
            int stt = 0;
            string maMH;
            string tenMH = "N/A";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {

                        maMH = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                        tenMH = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1);
                        maDiemMon = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetString(2);
                        string loaiKT = rdr.IsDBNull(4) ? GlobalProperties.NULLFIELD : rdr.GetString(4);
                        double diemtp = rdr.IsDBNull(3) ? -1 : rdr.GetDouble(3);
                        double diemtrb = rdr.IsDBNull(5) ? -1 : rdr.GetDouble(5);
                        int f = -1;

                        if (diemtp != -1)
                        {
                            for (int i = 0; i < 13; i++)
                            {
                                if (maMH == GlobalProperties.listMaMH[i])
                                {
                                    f = i;
                                    break;
                                }
                            }
                            //MessageBox.Show(f.ToString());
                            if (f != -1)
                            {
                                if (loaiKT == "DDGTX1")
                                {
                                    diem[f].DDGTX1 = new DTP(diemtp, maDiemMon);
                                }
                                else if (loaiKT == "DDGTX2")
                                {
                                    diem[f].DDGTX2 = new DTP(diemtp, maDiemMon);
                                }
                                else if (loaiKT == "DDGTX3")
                                {
                                    diem[f].DDGTX3 = new DTP(diemtp, maDiemMon);
                                }
                                else if (loaiKT == "DDGTX4")
                                {
                                    diem[f].DDGTX4 = new DTP(diemtp, maDiemMon);
                                }
                                else if (loaiKT == "DDGGK")
                                {
                                    diem[f].DDGGK = new DTP(diemtp, maDiemMon);
                                }
                                else if (loaiKT == "DDGCK")
                                {
                                    diem[f].DDGCK = new DTP(diemtp, maDiemMon);
                                }

                                diem[f].DDGTRB = new DTP(diemtrb, maDiemMon);
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chưa có dữ liệu!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            for (int i = 0; i < diem.Count; i++)
            {
                string[] row = new string[9];

                row[0] = (++stt).ToString();//Số thứ tự
                row[1] = diem[i].TenMH;
                row[2] = diem[i].DDGTX1.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGTX1.diem.ToString();
                row[3] = diem[i].DDGTX2.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGTX2.diem.ToString();
                row[4] = diem[i].DDGTX3.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGTX3.diem.ToString();
                row[5] = diem[i].DDGTX4.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGTX4.diem.ToString();
                row[6] = diem[i].DDGGK.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGGK.diem.ToString();
                row[7] = diem[i].DDGCK.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGCK.diem.ToString();
                row[8] = diem[i].DDGTRB.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGTRB.diem.ToString(); 
                ListViewItem listViewItem = new ListViewItem(row);
                LV_BangDiem.Items.Add(listViewItem);
            }
            //LV_BangDiem.Show();
            ///hihi
            ///
        }

        private void LB_NamSinhMe_Click(object sender, EventArgs e)
        {

        }

        private void materialTabSelector1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
