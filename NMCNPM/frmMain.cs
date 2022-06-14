using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentManagementSystem.Classes;
using StudentManagementSystem.DatabaseCore;

namespace StudentManagementSystem
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        class DiemHocSinh
        {
            public HocSinh HS;
            public DiemThanhPhan DTP;
        }

        //-----------------tabPag1----------------------------
        string curNamHoc_page1 = "";
        string curMaLop_page1 = "";
        string curHK_page1 = "";
        int curCB_NamHoc_page1 = -1, cur_CBKhoi_page1 = -1, cur_CBLop_page1 = -1, curCB_HK_page1 = -1, cur_CB_Mon_page1 = -1;
        List<Lop> listLop_page1 = new List<Lop>();
        List<DiemHocSinh> listHocSinh_page1 = new List<DiemHocSinh>();

        Dictionary<string, int> listNamHoc_page1 = new Dictionary<string, int>();
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
      
            GetandShowMaNamHoc();
            GetandShowMaNamHocpage2();
            GetandShowMaNamHocpage3();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (panel_Menu.Width > 75)
            {
                panel_Menu.Width = 75;
                btn_Menu.Location = new Point(16, 10);

            }
            else
            {
                panel_Menu.Width = 200;
                btn_Menu.Location = new Point(141, 10);
            }
        }

        void GetandShowMaNamHoc()
        {
            CB_NamHoc.SelectedIndex = -1;
            CB_NamHoc.Items.Clear();
            CB_Lop.SelectedIndex = -1;
            CB_Lop.Items.Clear();
            listHocSinh_page1.Clear();
            GetNamHoc(out listNamHoc_page1);

            foreach (KeyValuePair<string, int> kvp in listNamHoc_page1)
            {
                CB_NamHoc.Items.Add(kvp.Key);
            }
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CB_NamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (curCB_NamHoc_page1 != -1)
            {
                if (!CheckDataGridView())
                {
                    CB_NamHoc.SelectedIndex = curCB_NamHoc_page1;
                    return;
                }
            }

            curCB_NamHoc_page1 = CB_NamHoc.SelectedIndex;

            CB_Lop.SelectedIndex = -1;
            CB_Lop.Items.Clear();
            if (CB_Khoi.SelectedIndex != -1)
            {
                listLop_page1.Clear();
                GetMaLop(CB_Khoi.SelectedItem.ToString(), CB_NamHoc.SelectedItem.ToString(), out listLop_page1);
                foreach (Lop p in listLop_page1)
                    CB_Lop.Items.Add(p.TenLop);
            }
            curHK_page1 = CB_NamHoc.SelectedItem.ToString();
        }

        private void CB_NamHoc_Click(object sender, EventArgs e)
        {
        }

        private void CB_Khoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cur_CBKhoi_page1 != -1)
            {
                if (!CheckDataGridView())
                {
                    CB_Khoi.SelectedIndex = cur_CBKhoi_page1;
                    return;
                }
            }

            cur_CBKhoi_page1 = CB_Khoi.SelectedIndex;
            CB_Lop.SelectedIndex = -1;
            CB_Lop.Items.Clear();
            if (CB_NamHoc.SelectedIndex != -1)
            {
                GetMaLop(CB_Khoi.SelectedItem.ToString(), CB_NamHoc.SelectedItem.ToString(), out listLop_page1);
                foreach (Lop p in listLop_page1)
                    CB_Lop.Items.Add(p.TenLop);
            }
        }

        private void CB_Lop_Click(object sender, EventArgs e)
        {
            if (CB_NamHoc.SelectedIndex == -1 || CB_Khoi.SelectedIndex == -1)
            {
                MessageBox.Show("Chọn năm học và khối trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CB_Lop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cur_CBLop_page1 != -1)
            {
                if (!CheckDataGridView())
                {
                    CB_Lop.SelectedIndex = cur_CBLop_page1;
                    return;
                }
            }

            cur_CBLop_page1 = CB_Lop.SelectedIndex;
            if (CB_Lop.SelectedIndex == -1)
            {
                return;
            }
            CB_HocKi.SelectedIndex = -1;
            curCB_HK_page1 = -1;
            CB_MonHoc.SelectedIndex = -1;
            curCB_NamHoc_page1 = -1;

            int stt = 0;
            listHocSinh_page1.Clear();
            dataGridView_BangDiem.Rows.Clear();

            //lấy thông tin học sinh
            string maLop = listLop_page1[CB_Lop.SelectedIndex].MaLop;
            curMaLop_page1 = maLop;
            string query = $"SELECT MAHS, HotenHS FROM HOCSINH WHERE MALOP = '{maLop}' OR EXISTS (SELECT * FROM LOPDAHOC WHERE LOPDAHOC.MAHS = HOCSINH.MAHS AND LOPDAHOC.MALOP = '{maLop}')";
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        string _maHS = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                        string _hoTen = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                        DiemHocSinh dhs = new DiemHocSinh();
                        dhs.HS = new HocSinh(_maHS, _hoTen);
                        listHocSinh_page1.Add(dhs);
                        var index = dataGridView_BangDiem.Rows.Add();

                        dataGridView_BangDiem.Rows[index].Cells[0].Value = (++stt).ToString();//Số thứ tự
                        dataGridView_BangDiem.Rows[index].Cells[1].Value = _maHS;
                        dataGridView_BangDiem.Rows[index].Cells[2].Value = _hoTen;
                    }
                }
            }

            query = $"SELECT L.TENLOP, L.SISO, GV.TENGV FROM LOP AS L, GIAOVIEN AS GV WHERE L.MALOP = '{maLop}' AND L.MAGVCN = GV.MAGV";
            cmd = new SqlCommand(query, GlobalProperties.conn);
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    rdr.Read();
                    string tenLop = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                    string siSo = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                    string tenGV = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetString(2).Trim();
                    lb_SiSo_page1.Text = "Sĩ số: " + siSo;
                    lb_TenLop_page1.Text = "Lớp: " + tenLop;
                    lb_GVCN_page1.Text = "GVCN: " + tenGV;
                }
            }
        }

        private void CB_HocKi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (curCB_HK_page1 != -1)
            {
                if (!CheckDataGridView(false))
                {
                    CB_HocKi.SelectedIndex = curCB_HK_page1;
                    return;
                }
            }

            curCB_HK_page1 = CB_HocKi.SelectedIndex;
            if (CB_HocKi.SelectedIndex == -1)
            {
                return;
            }
            
            if (CB_MonHoc.SelectedIndex == -1)
            {
                return;
            }
            GetDiemHocSinh();
            
        }

        private void CB_MonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cur_CB_Mon_page1 != -1)
            {
                if (!CheckDataGridView(false))
                {
                    CB_MonHoc.SelectedIndex = cur_CB_Mon_page1;
                    return;
                }
            }

            cur_CB_Mon_page1 = CB_MonHoc.SelectedIndex;
            if (CB_MonHoc.SelectedIndex == -1)
            {
                return;
            }
            if (CB_HocKi.SelectedIndex == -1)
            {
                MessageBox.Show("Chọn thêm học kì!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            GetDiemHocSinh();
        }

        void GetDiemHocSinh()
        {
            string _maMon = GlobalProperties.listMaMH[CB_MonHoc.SelectedIndex];
            string _tenMH = GlobalProperties.listTenMH[CB_MonHoc.SelectedIndex];
            string query;
            SqlCommand cmd;
            for (int i = 0; i < listHocSinh_page1.Count; i++)
            {
                DiemHocSinh p = listHocSinh_page1[i];
                
                string _maHS = p.HS.MaHS;
                string _maHK = CB_HocKi.SelectedItem.ToString();
                string _namHoc = CB_NamHoc.SelectedItem.ToString();
                listHocSinh_page1[i].DTP = new DiemThanhPhan(_maMon, _tenMH);
                query = @"SELECT CTD.MADIEMMON, CTD.DIEM, LKT.TENLOAIKT, DM.TRUNGBINH
                            FROM CHITIETDIEM AS CTD
                            INNER JOIN DIEMMON AS DM ON CTD.MADIEMMON = DM.MADIEMMON 
                            LEFT JOIN LOAIKIEMTRA AS LKT ON LKT.MALOAIKT = CTD.MALOAIKT
                            LEFT JOIN HOCSINH AS HS ON HS.MAHS = DM.MAHOCSINH
                            LEFT JOIN MONHOC AS MN ON MN.MAMH = DM.MAMONHOC " +
                $"WHERE DM.MAHOCSINH = '{_maHS}' AND DM.MAHK = '{_maHK}' AND DM.NAMHOC = '{_namHoc}' AND DM.MAMONHOC = '{_maMon}'";

                string maDiemMon;
                cmd = new SqlCommand(query, GlobalProperties.conn);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            maDiemMon = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0);
                            string loaiKT = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetString(2);
                            double diemtp = rdr.IsDBNull(1) ? -1 : rdr.GetDouble(1);
                            double diemTB = rdr.IsDBNull(3) ? -1 : rdr.GetDouble(3);
                           // MessageBox.Show(_tenMH + " " + maDiemMon + " " + loaiKT + " " + diemtp + " " + diemTB);
                            if (diemtp != -1)
                            {
                                listHocSinh_page1[i].DTP.MaMH = _maMon;
                                listHocSinh_page1[i].DTP.HaveTableDiemMon = true;
                                listHocSinh_page1[i].DTP.MaDiemMon = maDiemMon;
                                if (loaiKT == "DDGTX1")
                                {
                                    listHocSinh_page1[i].DTP.DDGTX1 = new DTP(diemtp, maDiemMon);
                                }
                                else if (loaiKT == "DDGTX2")
                                {
                                    listHocSinh_page1[i].DTP.DDGTX2 = new DTP(diemtp, maDiemMon);
                                }
                                else if (loaiKT == "DDGTX3")
                                {
                                    listHocSinh_page1[i].DTP.DDGTX3 = new DTP(diemtp, maDiemMon);
                                }
                                else if (loaiKT == "DDGTX4")
                                {
                                    listHocSinh_page1[i].DTP.DDGTX4 = new DTP(diemtp, maDiemMon);
                                }
                                else if (loaiKT == "DDGGK")
                                {
                                    listHocSinh_page1[i].DTP.DDGGK = new DTP(diemtp, maDiemMon);
                                }
                                else if (loaiKT == "DDGCK")
                                {
                                    listHocSinh_page1[i].DTP.DDGCK = new DTP(diemtp, maDiemMon);
                                }

                                listHocSinh_page1[i].DTP.DDGTRB = new DTP(diemTB, maDiemMon);

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chưa có thông tin! Tạo mới tại mục Thiết lập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
            }
            ShowBangDiem();

            //Thông tin giáo viên giảng dạy

            query = $"SELECT GV.TENGV FROM GIAOVIEN AS GV, GIANGDAY AS GD WHERE GD.MALOP = '{curMaLop_page1}' AND GD.MAGV = GV.MAGV AND GV.MAMH = '{_maMon}'";
            cmd = new SqlCommand(query, GlobalProperties.conn);
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    rdr.Read();
                    string tenGVBM = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                    lb_GVBM_page1.Text = "GVBM: " +  tenGVBM;
                }
            }
            lb_HK_page1.Text =  "Học kì: " + CB_HocKi.SelectedItem.ToString();
            lb_MonHoc_page1.Text = "Môn học: " + CB_MonHoc.SelectedItem.ToString();
        }

        void ShowBangDiem()
        {
            for (int i = 0; i < listHocSinh_page1.Count; i++)
            {
                DiemHocSinh p = listHocSinh_page1[i];
                dataGridView_BangDiem.Rows[i].Cells[3].Value = p.DTP.DDGTX1.diem == -1 ? GlobalProperties.NULLFIELD : p.DTP.DDGTX1.diem.ToString();
                dataGridView_BangDiem.Rows[i].Cells[4].Value = p.DTP.DDGTX2.diem == -1 ? GlobalProperties.NULLFIELD : p.DTP.DDGTX2.diem.ToString(); 
                dataGridView_BangDiem.Rows[i].Cells[5].Value = p.DTP.DDGTX3.diem == -1 ? GlobalProperties.NULLFIELD : p.DTP.DDGTX3.diem.ToString(); 
                dataGridView_BangDiem.Rows[i].Cells[6].Value = p.DTP.DDGTX4.diem == -1 ? GlobalProperties.NULLFIELD : p.DTP.DDGTX4.diem.ToString(); 
                dataGridView_BangDiem.Rows[i].Cells[7].Value = p.DTP.DDGGK.diem == -1 ? GlobalProperties.NULLFIELD : p.DTP.DDGGK.diem.ToString(); 
                dataGridView_BangDiem.Rows[i].Cells[8].Value = p.DTP.DDGCK.diem == -1 ? GlobalProperties.NULLFIELD : p.DTP.DDGCK.diem.ToString(); 
                dataGridView_BangDiem.Rows[i].Cells[9].Value = p.DTP.DDGTRB.diem == -1 ? GlobalProperties.NULLFIELD : p.DTP.DDGTRB.diem.ToString();
            }
        }

        private void btn_tinhDTB_pag1_Click(object sender, EventArgs e)
        {
            int[] heSo = { 1, 1, 1, 1, 2, 3 };
            for (int i = 0; i < listHocSinh_page1.Count; i++)
            {
                int tongHeSo = 0;
                double tongDiem = 0;
                int tongCotDiem = 0;
                for (int j = 3; j <= 8; j++)
                {
                    string diem = dataGridView_BangDiem.Rows[i].Cells[j].Value == null ? string.Empty : dataGridView_BangDiem.Rows[i].Cells[j].Value.ToString();
                    //MessageBox.Show(diem);
                    if (string.IsNullOrEmpty(diem.Trim()))
                    {
                        continue;
                    }
                    double diemthuc = GlobalFunction.CheckDiem(diem.Trim());
                    if (diemthuc == -1)
                    {
                        MessageBox.Show($"Điểm nhập không hợp lệ ở STT {i + 1}", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    tongCotDiem = (j >= 3 && j <= 6) ? 1 : tongCotDiem;
                    tongCotDiem += (j > 6) ? heSo[j - 3] : 0;
                    tongDiem += diemthuc * heSo[j - 3];
                    tongHeSo += heSo[j - 3];
                }

                if (tongCotDiem == 6)
                {
                    double tmp = Math.Round(tongDiem / tongHeSo, 1);
                    dataGridView_BangDiem.Rows[i].Cells[9].Value = tmp.ToString();
                }
                else
                {
                    dataGridView_BangDiem.Rows[i].Cells[9].Value = "";
                }
            }
            
        }

        private void btn_HoanTac_page1_Click(object sender, EventArgs e)
        {
            ShowBangDiem();
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e) // Lưu điểm hs
        {
            for (int i = 0; i < listHocSinh_page1.Count; i++)
            {
                for (int j = 3; j <= 9; j++)
                {
                    string diem = dataGridView_BangDiem.Rows[i].Cells[j].Value == null ? string.Empty : dataGridView_BangDiem.Rows[i].Cells[j].Value.ToString();
                    //MessageBox.Show(diem);
                    if (string.IsNullOrEmpty(diem.Trim()))
                    {
                        continue;
                    }
                    double diemthuc = GlobalFunction.CheckDiem(diem.Trim());
                    if (diemthuc == -1)
                    {
                        MessageBox.Show($"Điểm nhập không hợp lệ ở {i}", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            btn_tinhDTB_pag1.PerformClick();
            for (int i = 0; i < listHocSinh_page1.Count; i++)
            {
                CapNhatDiem(i);
            }
            GetDiemHocSinh();
            MessageBox.Show("Đã lưu", "Thông báo");
        }

        void CapNhatDiem(int i)
        {
            //các cột trừ cột dtrb
            for (int j = 3; j <= 8; j++)
            {

                string _diem = dataGridView_BangDiem.Rows[i].Cells[j].Value == null ? string.Empty : dataGridView_BangDiem.Rows[i].Cells[j].Value.ToString();
                //MessageBox.Show(diem);
                double _diemthuc = GlobalFunction.CheckDiem(_diem.Trim());
                string maLoaiKT = GetMaLoaiKT(j);
                string maDiemMon = GetMaDiem(i, j);
                if (string.IsNullOrEmpty(_diem.Trim()))
                {
                    if (checkDiemTonTai(i, j))
                    {
                        //Xóa khỏi db;
                        DeleteChiTietDiem(maDiemMon, maLoaiKT);
                    }
                    continue;
                }
                if (maDiemMon != "")
                {
                    if (UpdateDiem(maDiemMon, maLoaiKT, _diemthuc))
                    {

                    }
                    else
                    {
                        ResetUpdateDiem(i, j);
                        return;

                    }
                }
                else
                {
                    string keyMaMonHoc = GlobalProperties.listMaMH[CB_MonHoc.SelectedIndex];
                    //Tạo table DIEMMON
                    if (!listHocSinh_page1[i].DTP.HaveTableDiemMon)
                    {
                        string keyMaDiemMon = GetMaDiemMonMoi();
                        if (InsertTableDiemMon(keyMaDiemMon, keyMaMonHoc, curHK_page1, listHocSinh_page1[i].HS.MaHS))
                        {
                            listHocSinh_page1[i].DTP.HaveTableDiemMon = true;
                            listHocSinh_page1[i].DTP.MaDiemMon = keyMaDiemMon;
                            listHocSinh_page1[i].DTP.MaMH = keyMaMonHoc;
                        }
                        else
                        {
                            ResetUpdateDiem(i, j);
                            return;
                        }

                    }
                    //MessageBox.Show(listHocSinh_page1[i].DTP.MaDiemMon + " " + maLoaiKT);
                    //Thêm CHITIETDIEM
                    if (InsertChiTietDiem(listHocSinh_page1[i].DTP.MaDiemMon, maLoaiKT, _diemthuc))
                    {

                    }
                    else
                    {
                        ResetUpdateDiem(i, j);
                        return;
                    }
                }
            }

            //Cập nhật điểm Trb
            string __diem = dataGridView_BangDiem.Rows[i].Cells[9].Value == null ? string.Empty : dataGridView_BangDiem.Rows[i].Cells[9].Value.ToString();
            //MessageBox.Show(diem);
            double __diemthuc = GlobalFunction.CheckDiem(__diem.Trim());
            string maDiem = GetMaDiem(i, 9);
            if (string.IsNullOrEmpty(__diem.Trim()))
            {
                if (checkDiemTonTai(i, 9))
                {
                    if (UpdateDiemTrB(maDiem, -1))
                    {

                    }
                    else
                    {
                        ResetUpdateDiem(i, 9);
                        return;
                    }
                }
                return;
            }
            if (maDiem != "")
            {
                if (UpdateDiemTrB(maDiem, __diemthuc))
                {

                }
                else
                {
                    ResetUpdateDiem(i, 9);
                    return;
                }
            }
        }

        bool UpdateDiem(string maDiem, string maLoaiKT, double diemThuc)
        {
            string sqlUpdateDiem = @"UPDATE CHITIETDIEM
                                    SET DIEM = @diem
                                    WHERE MADIEMMON = @madiem AND MALOAIKT = @maloaikt";
            try
            {
                SqlCommand cmd = new SqlCommand(sqlUpdateDiem, GlobalProperties.conn);

                cmd.Parameters.Add("@madiem", SqlDbType.Char).Value = maDiem.ToString();
                cmd.Parameters.Add("@maloaikt", SqlDbType.Char).Value = maLoaiKT.ToString();
                cmd.Parameters.Add("@diem", SqlDbType.Float).Value = diemThuc;
                int rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception w)
            {
                DialogResult dialogResult = MessageBox.Show("Có lỗi trong quá trình lưu. Hiển thị lỗi?", "Lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show(w.ToString());
                }
                return false;
            }
            return true;
        }

        bool InsertTableDiemMon(string keyMaDiemMon, string keyMaMonHoc, string _hocKi, string _MaHS)
        {
            string sqlTaoTableDiemMon = @"INSERT INTO DIEMMON(MADIEMMON, MAMONHOC, MAHK, NAMHOC, MAHOCSINH)
	                                    VALUES(@madiemmon, @mamonhoc, @mahk, @manamhoc, @mahs)";
            try
            {
                SqlCommand cmd = new SqlCommand(sqlTaoTableDiemMon, GlobalProperties.conn);

                cmd.Parameters.Add("@madiemmon", SqlDbType.Char).Value = keyMaDiemMon.ToString();
                cmd.Parameters.Add("@mamonhoc", SqlDbType.Char).Value = keyMaMonHoc.ToString();
                cmd.Parameters.Add("@manamhoc", SqlDbType.Char).Value = curNamHoc_page1;
                cmd.Parameters.Add("@mahk", SqlDbType.Char).Value = _hocKi;
                cmd.Parameters.Add("@mahs", SqlDbType.Char).Value = _MaHS;

                int rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception w)
            {
                DialogResult dialogResult = MessageBox.Show("Có lỗi trong quá trình lưu. Hiển thị lỗi?", "Lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show(w.ToString());
                }
                return false;
            }
            return true;

        }

        bool UpdateDiemTrB(string maDiem, double diemThuc)
        {
            string sqlUpdateDiem = @"UPDATE DIEMMON
                                    SET TRUNGBINH = @diem
                                    WHERE MADIEMMON = @madiem";
            try
            {
                SqlCommand cmd = new SqlCommand(sqlUpdateDiem, GlobalProperties.conn);

                cmd.Parameters.Add("@madiem", SqlDbType.Char).Value = maDiem.ToString();
                if (diemThuc != -1)
                {
                    cmd.Parameters.Add("@diem", SqlDbType.Float).Value = diemThuc;
                }
                else
                {
                    cmd.Parameters.Add("@diem", SqlDbType.Float).Value = DBNull.Value;
                }

                int rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception w)
            {
                DialogResult dialogResult = MessageBox.Show("Có lỗi trong quá trình lưu. Hiển thị lỗi?", "Lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show(w.ToString());
                }
                return false;
            }
            return true;
        }

        void ResetUpdateDiem(int x, int y)
        { }

        string GetMaLoaiKT(int y)//Mã loại Kiểm tra của cột x
        {
            y--;
            if (y == 2)
            {
                return GlobalProperties.listMaLoaiKT[0];
            }
            if (y == 3)
            {
                return GlobalProperties.listMaLoaiKT[1];
            }
            if (y == 4)
            {
                return GlobalProperties.listMaLoaiKT[2];
            }
            if (y == 5)
            {
                return GlobalProperties.listMaLoaiKT[3];
            }
            if (y == 6)
            {
                return GlobalProperties.listMaLoaiKT[4];
            }
            if (y == 7)
            {
                return GlobalProperties.listMaLoaiKT[5];
            }
            if (y == 8)
            {
                return GlobalProperties.listMaLoaiKT[6];
            }
            return "";
        }

        string GetMaDiem(int x, int y) //Lấy mã điểm tại học sinh thứ i và cột thứ j
        {
            y--;
            DiemThanhPhan d = listHocSinh_page1[x].DTP;
            if (y == 2)
            {
                return d.DDGTX1.maDiem;
            }
            if (y == 3)
            {
                return d.DDGTX2.maDiem;
            }
            if (y == 4)
            {
                return d.DDGTX3.maDiem;
            }
            if (y == 5)
            {
                return d.DDGTX4.maDiem;
            }
            if (y == 6)
            {
                return d.DDGGK.maDiem;
            }
            if (y == 7)
            {
                return d.DDGCK.maDiem;
            }
            if (y == 8)
            {
                return d.DDGTRB.maDiem;
            }
            return "";

        }

        bool DeleteChiTietDiem(string maDiem, string maLoaiKT)
        {
            string sqlUpdateDiem = @"DELETE FROM CHITIETDIEM
                                    WHERE MADIEMMON = @madiem AND MALOAIKT = @maloaikt";
            try
            {
                SqlCommand cmd = new SqlCommand(sqlUpdateDiem, GlobalProperties.conn);

                cmd.Parameters.Add("@madiem", SqlDbType.Char).Value = maDiem.ToString();
                cmd.Parameters.Add("@maloaikt", SqlDbType.Char).Value = maLoaiKT.ToString();
                int rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception w)
            {
                DialogResult dialogResult = MessageBox.Show("Có lỗi trong quá trình lưu. Hiển thị lỗi?", "Lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show(w.ToString());
                }
                return false;
            }
            return true;
        }

        bool checkDiemTonTai(int x, int y) //Check có tồn tại điểm học sinh x, cột y không?
        {
            y--;
            DiemThanhPhan _diem = listHocSinh_page1[x].DTP;
            return (y == 2 && _diem.DDGTX1.diem != -1)
                || (y == 3 && _diem.DDGTX2.diem != -1)
                || (y == 4 && _diem.DDGTX3.diem != -1)
                || (y == 5 && _diem.DDGTX4.diem != -1)
                || (y == 6 && _diem.DDGGK.diem != -1)
                || (y == 7 && _diem.DDGCK.diem != -1)
                || (y == 8 && _diem.DDGTRB.diem != -1);
        }

        string GetMaDiemMonMoi()
        {
            string keyMaDiemMon = "";
            bool f = false;
            //Tạo mới.
            do
            {

                keyMaDiemMon = GlobalFunction.RandomString(10);
                //MessageBox.Show(keyMaDiemMon);
                string sql = $"SELECT COUNT(*) FROM CHITIETDIEM WHERE MADIEMMON = '{keyMaDiemMon}'";
                SqlCommand cmd = new SqlCommand(sql, GlobalProperties.conn);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        int count = rdr.GetInt32(0);
                        if (count > 0)
                            f = false;
                        else
                            f = true;

                    }
                }

            } while (!f);
            //MessageBox.Show(keyMaDiemMon);
            return keyMaDiemMon;
        }

        private void tbn_reset_Click(object sender, EventArgs e)
        {
            curNamHoc_page1 = "";
            curMaLop_page1 = "";
            curHK_page1 = "";
            curCB_NamHoc_page1 = -1;
            cur_CBKhoi_page1 = -1;
            cur_CBLop_page1 = -1;
            curCB_HK_page1 = -1;
            cur_CB_Mon_page1 = -1;
            listLop_page1.Clear();
            listHocSinh_page1.Clear();
            listNamHoc_page1.Clear();
            
            CB_NamHoc.Items.Clear();
            CB_NamHoc.Text = "";
            CB_Lop.Items.Clear();
            CB_Lop.Text = "";
            CB_Khoi.SelectedIndex = -1;
            CB_HocKi.SelectedIndex = -1;
            CB_MonHoc.SelectedIndex = -1;
            GetandShowMaNamHoc();
            dataGridView_BangDiem.Rows.Clear();
        }

        bool InsertChiTietDiem(string maDiemMon, string maLoaiKT, double diemThuc)
        {
            string sqlTaoDiem = @"INSERT INTO CHITIETDIEM(MADIEMMON, MALOAIKT, DIEM)
	                            VALUES(@madiemmon, @maloaikt, @diem)";
            try
            {
                SqlCommand cmd = new SqlCommand(sqlTaoDiem, GlobalProperties.conn);

                cmd.Parameters.Add("@madiemmon", SqlDbType.Char).Value = maDiemMon.ToString();
                cmd.Parameters.Add("@maloaikt", SqlDbType.Char).Value = maLoaiKT.ToString();
                cmd.Parameters.Add("@diem", SqlDbType.Float).Value = diemThuc;

                int rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception w)
            {
                DialogResult dialogResult = MessageBox.Show("Có lỗi trong quá trình lưu. Hiển thị lỗi?", "Lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show(w.ToString());
                }

                return false;
            }
            return true;
        }

        bool CheckDataGridView(bool del = true)
        {
            //MessageBox.Show(dataGridView_BangDiem.Rows.Count.ToString());
            if (dataGridView_BangDiem.RowCount > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Thay đổi sẽ làm thay đổi dữ liệu đang hiển thị", "Cảnh báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    if (del)
                        dataGridView_BangDiem.Rows.Clear();
                    return true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }

        //----------------------tabPage2---------------------------

        List<Lop> listLop_page2 = new List<Lop>();
        Dictionary<string, int> listNamHoc_page2 = new Dictionary<string, int>();

        void GetandShowMaNamHocpage2()
        {
            CB_NamHoc_page2.Items.Clear();
            CB_Lop_page2.Items.Clear();
            CB_NamHoc_page2.Items.Add("*");

            GetNamHoc(out listNamHoc_page2);
            foreach (KeyValuePair<string, int> kvp in listNamHoc_page2)
            {
                CB_NamHoc_page2.Items.Add(kvp.Key);
            }
        }

        private void CB_Khoi_page2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_Lop_page2.SelectedIndex = -1;
            CB_Lop_page2.Items.Clear();
            if (CB_NamHoc_page2.SelectedIndex == -1 || CB_NamHoc_page2.SelectedIndex == 0)
            {
                if (CB_Khoi_page2.SelectedIndex == -1 || CB_Khoi_page2.SelectedIndex == 0)//
                {
                    GetInfoHocSinh(""); //get toàn bộ học sinh
                }
                else
                {
                    string query = $" AND LOP.MAKHOI = '{CB_Khoi_page2.SelectedItem.ToString()}' ";
                    GetInfoHocSinh(query);//get học sinh theo khối.
                }
            }
            else
            {
                if (CB_Khoi_page2.SelectedIndex == -1 || CB_Khoi_page2.SelectedIndex == 0)//
                {
                    string query = $" AND LOP.NAMHOC = '{CB_NamHoc_page2.SelectedItem.ToString()}'";
                    GetInfoHocSinh(query); //get học sinh theo năm học
                }
                else
                {
                    string query = $" AND LOP.NAMHOC = '{CB_NamHoc_page2.SelectedItem.ToString()}' AND LOP.MAKHOI = '{CB_Khoi_page2.SelectedItem.ToString()}'";
                    GetInfoHocSinh(query);//get học sinh theo khối và theo năm học
                    GetMaLop(CB_Khoi_page2.SelectedItem.ToString(), CB_NamHoc_page2.SelectedItem.ToString(), out listLop_page2);
                    CB_Lop_page2.Items.Clear();
                    CB_Lop_page2.Items.Add("*");
                    foreach (Lop p in listLop_page2)
                        CB_Lop_page2.Items.Add(p.TenLop);
                }
            }
        }

        private void CB_Lop_page2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_NamHoc_page2.SelectedIndex < 1 || CB_Khoi_page2.SelectedIndex < 1)
            {
                //MessageBox.Show("Hãy chọn năm học và khối trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (CB_Lop_page2.SelectedIndex < 1)
            {
                string query = $" AND LOP.NAMHOC = '{CB_NamHoc_page2.SelectedItem.ToString()}' AND LOP.MAKHOI = '{CB_Khoi_page2.SelectedItem.ToString()}'";
                GetInfoHocSinh(query);//get học sinh theo khối và theo năm học
            }
            else
            {
                string query = $" AND LOP.NAMHOC = '{CB_NamHoc_page2.SelectedItem.ToString()}' AND LOP.MAKHOI = '{CB_Khoi_page2.SelectedItem.ToString()}' AND LOP.TENLOP = '{CB_Lop_page2.SelectedItem.ToString()}'";
                GetInfoHocSinh(query);//get học sinh theo khối và theo năm học và lớp
            }

        }

        private void CB_Lop_page2_Click(object sender, EventArgs e)
        {
            if (CB_NamHoc_page2.SelectedIndex < 1 || CB_Khoi_page2.SelectedIndex < 1)
            {
                MessageBox.Show("Hãy chọn năm học và khối trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void dataGridView_ThongTinHocSinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string maHS = dataGridView_ThongTinHocSinh.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (!string.IsNullOrEmpty(maHS))
            {/////////////////////////////////////////
                using (Form frm = new StudentInfoEdit(maHS))
                {
                    frm.ShowDialog();
                    GC.Collect();
                }
                /////////////////////////////////////////
            }
        }

        private void btn_reset_page2_Click(object sender, EventArgs e)
        {
            CB_NamHoc_page2.Text = "";
            GetandShowMaNamHocpage2();
            CB_Khoi.Text = "";
            CB_Khoi_page2.SelectedIndex = -1;
            CB_Lop_page2.Text = "";
            CB_Lop_page2.SelectedIndex = -1;
            CB_Lop_page2.Items.Clear();
            dataGridView_ThongTinHocSinh.Rows.Clear();
            
        }

        private void TB_Search_page2_TextChanged(object sender, EventArgs e)
        {
           
            string text = TB_Search_page2.Text;
            if (string.IsNullOrEmpty(text))
            {
                for (int i = 0; i < dataGridView_ThongTinHocSinh.RowCount; i++)
                {
                    dataGridView_ThongTinHocSinh.Rows[i].Visible = true;
                }
            }
            for (int i = 0; i < dataGridView_ThongTinHocSinh.RowCount; i++)
            {
                string mahs = dataGridView_ThongTinHocSinh.Rows[i].Cells[1].Value.ToString();
                string tenhs = dataGridView_ThongTinHocSinh.Rows[i].Cells[2].Value.ToString();
                if (!mahs.Contains(text) && !tenhs.Contains(text))
                {
                    dataGridView_ThongTinHocSinh.Rows[i].Visible = false;
                }
                else
                {
                    dataGridView_ThongTinHocSinh.Rows[i].Visible = true;
                }
            }
        }

        private void CB_NamHoc_page2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_Lop_page2.SelectedIndex = -1;
            CB_Lop_page2.Items.Clear();
            if (CB_NamHoc_page2.SelectedIndex == -1 || CB_NamHoc_page2.SelectedIndex == 0)
            {
                if (CB_Khoi_page2.SelectedIndex == -1 || CB_Khoi_page2.SelectedIndex == 0)//
                {
                    GetInfoHocSinh(""); //get toàn bộ học sinh
                }
                else
                {
                    string query = $" AND LOP.MAKHOI = '{CB_Khoi_page2.SelectedItem.ToString()}' ";
                    GetInfoHocSinh(query);//get học sinh theo khối.
                }
            }
            else
            {
                if (CB_Khoi_page2.SelectedIndex == -1 || CB_Khoi_page2.SelectedIndex == 0)//
                {
                    string query = $" AND LOP.NAMHOC = '{CB_NamHoc_page2.SelectedItem.ToString()}'";
                    GetInfoHocSinh(query); //get học sinh theo năm học
                }
                else
                {
                    string query = $" AND LOP.NAMHOC = '{CB_NamHoc_page2.SelectedItem.ToString()}' AND LOP.MAKHOI = '{CB_Khoi_page2.SelectedItem.ToString()}'";
                    GetInfoHocSinh(query);//get học sinh theo khối và theo năm học
                    GetMaLop(CB_Khoi_page2.SelectedItem.ToString(), CB_NamHoc_page2.SelectedItem.ToString(), out listLop_page2);
                    CB_Lop_page2.Items.Clear();
                    foreach (Lop p in listLop_page2)
                        CB_Lop_page2.Items.Add(p.TenLop);
                }
            }
        }

        void GetInfoHocSinh(string addtoQuery)
        {
            string query = "SELECT MAHS, HotenHS, gioitinh, ngaysinh, LOP.TENLOP, noisinh, diachi, sodt, email, Ghichu FROM HOCSINH, LOP WHERE LOP.MALOP = HOCSINH.MALOP" + addtoQuery;
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);
            int stt = 0;
            dataGridView_ThongTinHocSinh.Rows.Clear();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        string maHs = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                        string hoTen = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                        string gioiTinh = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetString(2).Trim();
                        string ngaySinh = rdr.IsDBNull(3) ? GlobalProperties.NULLFIELD : rdr.GetDateTime(3).ToString();
                        string lop = rdr.IsDBNull(4) ? GlobalProperties.NULLFIELD : rdr.GetString(4).Trim();
                        string noiSinh = rdr.IsDBNull(5) ? GlobalProperties.NULLFIELD : rdr.GetString(5).Trim();
                        string diaChi = rdr.IsDBNull(6) ? GlobalProperties.NULLFIELD : rdr.GetString(6).Trim();
                        string soDt = rdr.IsDBNull(7) ? GlobalProperties.NULLFIELD : rdr.GetString(7).Trim();
                        string email = rdr.IsDBNull(8) ? GlobalProperties.NULLFIELD : rdr.GetString(8).Trim();
                        string ghiChu = rdr.IsDBNull(9) ? GlobalProperties.NULLFIELD : rdr.GetString(9).Trim();

                        var index = dataGridView_ThongTinHocSinh.Rows.Add();
                        dataGridView_ThongTinHocSinh.Rows[index].Cells[0].Value = (++stt).ToString();//Số thứ tự
                        dataGridView_ThongTinHocSinh.Rows[index].Cells[1].Value = maHs;
                        dataGridView_ThongTinHocSinh.Rows[index].Cells[2].Value = hoTen;
                        dataGridView_ThongTinHocSinh.Rows[index].Cells[3].Value = gioiTinh == "Nam" ? false : true;
                        dataGridView_ThongTinHocSinh.Rows[index].Cells[4].Value = ngaySinh;
                        dataGridView_ThongTinHocSinh.Rows[index].Cells[5].Value = lop;
                        dataGridView_ThongTinHocSinh.Rows[index].Cells[6].Value = noiSinh;
                        dataGridView_ThongTinHocSinh.Rows[index].Cells[7].Value = diaChi;
                        dataGridView_ThongTinHocSinh.Rows[index].Cells[8].Value = soDt;
                        dataGridView_ThongTinHocSinh.Rows[index].Cells[9].Value = email;
                        dataGridView_ThongTinHocSinh.Rows[index].Cells[10].Value = ghiChu;
                    }
                }
            }
        }

        //-----------page3------------
        List<Lop> listLop_page3 = new List<Lop>();
        Dictionary<string, int> listNamHoc_page3 = new Dictionary<string, int>();

        private void CB_Lop_page3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CB_Khoi_page3_SelectedIndexChanged(object sender, EventArgs e)
        {

            cur_CBKhoi_page1 = CB_Khoi.SelectedIndex;
            CB_Lop.SelectedIndex = -1;
            CB_Lop.Items.Clear();
            if (CB_NamHoc.SelectedIndex != -1)
            {
                GetMaLop(CB_Khoi.SelectedItem.ToString(), CB_NamHoc.SelectedItem.ToString(), out listLop_page1);
                foreach (Lop p in listLop_page1)
                    CB_Lop.Items.Add(p.TenLop);
            }
        }

        private void CB_NamHoc_page3_SelectedIndexChanged(object sender, EventArgs e)
        {

            CB_Lop_page3.SelectedIndex = -1;
            CB_Lop_page3.Items.Clear();
            if (CB_Khoi_page3.SelectedIndex != -1)
            {
                listLop_page3.Clear();
                GetMaLop(CB_Khoi_page3.SelectedItem.ToString(), CB_NamHoc_page3.SelectedItem.ToString(), out listLop_page3);
                foreach (Lop p in listLop_page3)
                    CB_Lop_page3.Items.Add(p.TenLop);
            }
            //curHK_page3 = CB_NamHoc_page3.SelectedItem.ToString();
        }

        void GetandShowMaNamHocpage3()
        {
            CB_NamHoc_page3.Items.Clear();
            CB_Lop_page3.Items.Clear();

            GetNamHoc(out listNamHoc_page3);
            foreach (KeyValuePair<string, int> kvp in listNamHoc_page3)
            {
                CB_NamHoc_page3.Items.Add(kvp.Key);
            }
        }


        //----------Dùng chung các tab-------------
        void GetNamHoc(out Dictionary<string, int> listNH)
        {
            listNH = new Dictionary<string, int>();
            //Get 3 năm học trong niên khóa
            string query = $"SELECT NAMBD, NAMKT FROM NIENKHOA";
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    string bd = "", kt = "";
                    while (rdr.Read())
                    {
                        bd = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                        kt = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                        int namBD = 0, namKT = 0;
                        Int32.TryParse(bd, out namBD);
                        Int32.TryParse(kt, out namKT);
                        if (namBD == 0 || namKT == 0)
                        {
                            continue;
                        }
                        listNH[namBD.ToString() + "-" + (namBD + 1).ToString()] = 1;
                        listNH[(namBD + 1).ToString() + "-" + (namBD + 2).ToString()] = 1;
                        listNH[(namBD + 2).ToString() + "-" + (namBD + 3).ToString()] = 1;
                    }

                }
            }
        }

        void GetMaLop(string maKhoi, string maNamHoc, out List<Lop> listLop)
        {
            listLop = new List<Lop>();
            //Get mã niên khóa:
            string query = $"SELECT MALOP, MAGVCN, TENLOP, SISO FROM LOP WHERE MAKHOI = '{maKhoi}' AND NAMHOC = '{maNamHoc}'";
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        string _maLop = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                        string _maGVCN = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                        string _tenLop = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetString(2).Trim();
                        string _siSo = rdr.IsDBNull(3) ? GlobalProperties.NULLFIELD : rdr.GetString(3).Trim();

                        listLop.Add(new Lop(_maLop, maKhoi, _maGVCN, _tenLop, _siSo));
                    }
                }
            }
        }
    }
}