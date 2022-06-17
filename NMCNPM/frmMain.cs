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
using System.Text.RegularExpressions;

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
            tabMain.Appearance = TabAppearance.FlatButtons;
            tabMain.ItemSize = new Size(0, 1);
            tabMain.SizeMode = TabSizeMode.Fixed;
            GetandShowMaNamHoc();
            GetandShowMaNamHocpage2();
            GetandShowMaNamHocpage3();
            GetandShowMaNamHocpage4();
            LoadPage5();
        }

        //Event menu
        private void bunifuIconButton5_Click(object sender, EventArgs e)
        {
            btn_TabThemHS.PerformClick();
        }

        private void bunifuIconButton7_Click(object sender, EventArgs e)
        {
            btn_tabThemGV_GD.PerformClick();
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            btn_tabBangDiem.PerformClick();
        }

        private void bunifuIconButton2_Click(object sender, EventArgs e)
        {
            btn_tab_Thongtin.PerformClick();
        }

        private void bunifuIconButton3_Click(object sender, EventArgs e)
        {
            btn_tab_tongket.PerformClick();
        }

        private void bunifuIconButton4_Click(object sender, EventArgs e)
        {
            btn_tabChuyenLop.PerformClick();
        }

        private void bunifuIconButton6_Click(object sender, EventArgs e)
        {
            btn_tab_themNK_HK.PerformClick();
        }

        private void bunifuIconButton8_Click(object sender, EventArgs e)
        {
            btn_CaiDat.PerformClick();
        }

        private void btn_tab_Thongtin_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 1;
        }

        private void btn_tab_tongket_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 2;
        }

        private void btn_tabChuyenLop_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 3;
        }

        private void btn_tab_themNK_HK_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 4;
        }

        private void btn_tabBangDiem_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 0;
        }

        private void btn_TabThemHS_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 5;
        }

        private void btn_tabThemGV_GD_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 6;
        }

        private void btn_CaiDat_Click(object sender, EventArgs e)
        {
            tabMain.SelectedIndex = 7;
        }
        //----------------------Page1----------------------
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

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e) //search page1
        {
            string text = TB_search_page1.Text;
            if (string.IsNullOrEmpty(text))
            {
                for (int i = 0; i < dataGridView_BangDiem.RowCount; i++)
                {
                    dataGridView_BangDiem.Rows[i].Visible = true;
                }
            }
            for (int i = 0; i < dataGridView_BangDiem.RowCount; i++)
            {
                string mahs = dataGridView_BangDiem.Rows[i].Cells[1].Value.ToString();
                string tenhs = dataGridView_BangDiem.Rows[i].Cells[2].Value.ToString();
                if (!mahs.Contains(text) && !tenhs.Contains(text))
                {
                    dataGridView_BangDiem.Rows[i].Visible = false;
                }
                else
                {
                    dataGridView_BangDiem.Rows[i].Visible = true;
                }
            }
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
            //curHK_page1 = CB_NamHoc.SelectedItem.ToString();
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
                    int siSo = rdr.IsDBNull(1) ? 0 : rdr.GetInt32(1);
                    string tenGV = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetString(2).Trim();
                    lb_SiSo_page1.Text = "Sĩ số: " + siSo.ToString();
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
                string maDiemMon = "";
                query = $"SELECT MADIEMMON FROM DIEMMON AS DM WHERE DM.MAHOCSINH = '{_maHS}' AND DM.MAHK = '{_maHK}' AND DM.NAMHOC = '{_namHoc}' AND DM.MAMONHOC = '{_maMon}'";
                cmd = new SqlCommand(query, GlobalProperties.conn);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            maDiemMon = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0);
                            listHocSinh_page1[i].DTP.MaDiemMon = maDiemMon;
                        }
                    }
                }

                query = @"SELECT CTD.DIEM, LKT.TENLOAIKT, DM.TRUNGBINH
                            FROM CHITIETDIEM AS CTD
                            INNER JOIN DIEMMON AS DM ON CTD.MADIEMMON = DM.MADIEMMON 
                            LEFT JOIN LOAIKIEMTRA AS LKT ON LKT.MALOAIKT = CTD.MALOAIKT
                            LEFT JOIN HOCSINH AS HS ON HS.MAHS = DM.MAHOCSINH
                            LEFT JOIN MONHOC AS MN ON MN.MAMH = DM.MAMONHOC " +
                $"WHERE DM.MAHOCSINH = '{_maHS}' AND DM.MAHK = '{_maHK}' AND DM.NAMHOC = '{_namHoc}' AND DM.MAMONHOC = '{_maMon}'";


                cmd = new SqlCommand(query, GlobalProperties.conn);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            string loaiKT = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1);
                            double diemtp = rdr.IsDBNull(0) ? -1 : rdr.GetDouble(0);
                            double diemTB = rdr.IsDBNull(2) ? -1 : rdr.GetDouble(2);
                            // MessageBox.Show(_tenMH + " " + maDiemMon + " " + loaiKT + " " + diemtp + " " + diemTB);
                            if (diemtp != -1)
                            {
                                listHocSinh_page1[i].DTP.MaMH = _maMon;
                                listHocSinh_page1[i].DTP.HaveTableDiemMon = true;

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
                    lb_GVBM_page1.Text = "GVBM: " + tenGVBM;
                }
            }
            lb_HK_page1.Text = "Học kì: " + CB_HocKi.SelectedItem.ToString();
            lb_MonHoc_page1.Text = "Môn học: " + CB_MonHoc.SelectedItem.ToString();
        }

        void ShowBangDiem()
        {
            if (listHocSinh_page1.Count < 1)
            {
                return;
            }
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
            if (CB_MonHoc.SelectedIndex < 0)
            {
                MessageBox.Show("Chưa chọn môn học!", "Thông báo");
                return;
            }
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
        {
            MessageBox.Show("err");
        }

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
            string query = "SELECT MAHS, HotenHS, gioitinh, ngaysinh, LOP.TENLOP, noisinh, diachi, sodt, email, Ghichu FROM HOCSINH, LOP WHERE (LOP.MALOP = HOCSINH.MALOP" + addtoQuery + ") OR EXISTS(SELECT* FROM LOPDAHOC WHERE LOPDAHOC.MAHS = HOCSINH.MAHS AND LOPDAHOC.MALOP = LOP.MALOP " + addtoQuery + ")";
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

        class Diemtrb
        {
            public double diem;
            public string maDiemMon;
            public Diemtrb(double _diem, string _maDiemMon)
            {
                diem = _diem;
                maDiemMon = _maDiemMon;
            }
        }
        class DiemtrbHS
        {
            public string maHS;
            public string tenHS;
            public string hanhKiem1;
            public string hanhKiem2;
            public string hanhKiemCN;
            public string maHKiem;

            public List<Diemtrb> listdiemTrb1 = new List<Diemtrb>();
            public List<Diemtrb> listdiemTrb2 = new List<Diemtrb>();
            public DiemtrbHS(string maHs = "", string tenHs = "")
            {
                maHS = maHs;
                tenHS = tenHs;
            }
        }
        List<DiemtrbHS> listHS_page3 = new List<DiemtrbHS>();
        int cur_namHoc_page3 = -1, cur_khoi_page3 = -1, cur_lop_page3 = -1;
        List<Lop> listLop_page3 = new List<Lop>();
        Dictionary<string, int> listNamHoc_page3 = new Dictionary<string, int>();

        private void CB_Lop_page3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cur_lop_page3 != -1)
            {
                if (!CheckDataGridView_page3())
                {
                    CB_Lop_page3.SelectedIndex = cur_lop_page3;
                    return;
                }
            }
            cur_lop_page3 = CB_Lop_page3.SelectedIndex;
            GetDataHS();

        }

        private void CB_Khoi_page3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cur_khoi_page3 != -1)
            {
                if (!CheckDataGridView_page3())
                {
                    CB_Khoi_page3.SelectedIndex = cur_khoi_page3;
                    return;
                }
            }
            cur_khoi_page3 = CB_Khoi_page3.SelectedIndex;
            CB_Lop_page3.Text = "";
            CB_Lop_page3.Items.Clear();
            if (CB_NamHoc_page3.SelectedIndex != -1)
            {
                listLop_page3.Clear();
                GetMaLop(CB_Khoi_page3.SelectedItem.ToString(), CB_NamHoc_page3.SelectedItem.ToString(), out listLop_page3);
                foreach (Lop p in listLop_page3)
                    CB_Lop_page3.Items.Add(p.TenLop);
            }
        }

        private void CB_NamHoc_page3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cur_namHoc_page3 != -1)
            {
                if (!CheckDataGridView_page3())
                {
                    CB_NamHoc_page3.SelectedIndex = cur_namHoc_page3;
                    return;
                }
            }
            cur_namHoc_page3 = CB_NamHoc_page3.SelectedIndex;
            CB_Lop_page3.Text = "";
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

        void GetDataHS()
        {
            string maLop = listLop_page3[CB_Lop_page3.SelectedIndex].MaLop;
            string query = $"SELECT HS.MAHS, HS.HotenHS FROM HOCSINH AS HS WHERE HS.MALOP = '{maLop}' OR EXISTS (SELECT * FROM LOPDAHOC WHERE LOPDAHOC.MAHS = HS.MAHS AND LOPDAHOC.MALOP = '{maLop}')";
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);
            int stt = 0;
            dataGridView_Tongket.Rows.Clear();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        string maHs = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                        string hoTen = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                        listHS_page3.Add(new DiemtrbHS(maHs, hoTen));
                        var index = dataGridView_Tongket.Rows.Add();
                        dataGridView_Tongket.Rows[index].Cells[0].Value = (++stt).ToString();//Số thứ tự
                        dataGridView_Tongket.Rows[index].Cells[1].Value = maHs;
                        dataGridView_Tongket.Rows[index].Cells[2].Value = hoTen;
                    }
                }
            }

            for (int i = 0; i < listHS_page3.Count; i++)
            {
                string _mahs = listHS_page3[i].maHS;
                query = $"SELECT MAMONHOC, MADIEMMON, TRUNGBINH, MAHK FROM DIEMMON WHERE NAMHOC = '{CB_NamHoc_page3.SelectedItem.ToString()}' AND MAHOCSINH = '{_mahs}'";
                for (int j = 0; j < 13; j++)
                {
                    listHS_page3[i].listdiemTrb1.Add(new Diemtrb(-1, ""));
                    listHS_page3[i].listdiemTrb2.Add(new Diemtrb(-1, ""));
                }
                cmd = new SqlCommand(query, GlobalProperties.conn);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            string maMh = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                            double trb = rdr.IsDBNull(2) ? -1 : rdr.GetDouble(2);
                            string maDm = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                            string mahk = rdr.IsDBNull(3) ? GlobalProperties.NULLFIELD : rdr.GetString(3).Trim();
                            for (int j = 0; j < 13; j++)
                            {
                                if (maMh == GlobalProperties.listMaMH[j])
                                {
                                    if (mahk == "HK1")
                                    {
                                        listHS_page3[i].listdiemTrb1[j] = new Diemtrb(trb, maDm);
                                        break;
                                    }
                                    else
                                    {
                                        listHS_page3[i].listdiemTrb2[j] = new Diemtrb(trb, maDm);
                                        break;
                                    }

                                }
                            }

                        }
                    }
                }

                query = $"SELECT XEPLOAIHKI, XEPLOAIHKII, XEPLOAICN, MaHK FROM HANHKIEM WHERE MAHS = '{_mahs}' AND NAMHOC = '{CB_NamHoc_page3.SelectedItem.ToString()}'";
                cmd = new SqlCommand(query, GlobalProperties.conn);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            string hk1 = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                            string hk2 = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                            string hkcn = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetString(2).Trim();
                            string maHk = rdr.IsDBNull(3) ? GlobalProperties.NULLFIELD : rdr.GetString(3).Trim();
                            listHS_page3[i].hanhKiem1 = hk1;
                            listHS_page3[i].hanhKiem2 = hk2;
                            listHS_page3[i].hanhKiemCN = hkcn;
                            listHS_page3[i].maHKiem = maHk;
                            DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)dataGridView_Tongket.Rows[i].Cells[4];
                            
                            //dataGridView_Tongket.Rows[i].Cells[4].Value = GetLoaiHanhKiem(hk1);
                            //dataGridView_Tongket.Rows[i].Cells[7].Value = GetLoaiHanhKiem(hk2);
                            //dataGridView_Tongket.Rows[i].Cells[10].Value = GetLoaiHanhKiem(hkcn);

                        }
                    }
                }
            }

            query = $"SELECT L.TENLOP, L.SISO, GV.TENGV FROM LOP AS L, GIAOVIEN AS GV WHERE L.TENLOP = '{CB_Lop_page3.SelectedItem.ToString()}' AND L.NAMHOC = '{CB_NamHoc_page3.SelectedItem.ToString()}' AND L.MAGVCN = GV.MAGV";
            cmd = new SqlCommand(query, GlobalProperties.conn);
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    rdr.Read();
                    string tenLop = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                    int siSo = rdr.IsDBNull(1) ? 0 : rdr.GetInt32(1);
                    string tenGV = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetString(2).Trim();
                    lb_siso_p3.Text = "Sĩ số: " + siSo.ToString();
                    lb_tenlop_p3.Text = "Lớp: " + tenLop;
                    lb_tengvcn_p3.Text = "GVCN: " + tenGV;
                }
            }

            /*string str = "";
            for (int i = 0; i < 13; i++)
                str = str + "-(" + listHS_page3[0].listdiemTrb[i].diem + ";" + listHS_page3[0].listdiemTrb[i].maDiemMon + ")";
            str = str + "\n" + listHS_page3[0].hanhKiem1 + "\n" + listHS_page3[0].hanhKiem2 + "\n" + listHS_page3[0].maHKiem;
            MessageBox.Show(str);*/

        } 

        int GetLoaiHanhKiem(string hk)// 0: Tôt, 1: Khá, 2: Trung bình, 3: yếu
        {
            if (hk == "Tốt")
            {
                return 0;
            }
            if (hk == "Khá")
            {
                return 1;
            }
            if (hk == "Trung bình")
            {
                return 2;
            }
            if (hk == "Yếu")
            {
                return 3;
            }
            return -1;
        }
        bool CheckDataGridView_page3(bool del = true)
        {
            //MessageBox.Show(dataGridView_BangDiem.Rows.Count.ToString());
            if (dataGridView_Tongket.RowCount > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Thay đổi sẽ làm thay đổi dữ liệu đang hiển thị", "Cảnh báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    if (del)
                        dataGridView_Tongket.Rows.Clear();
                    return true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }

        private void CB_Lop_page3_Click(object sender, EventArgs e)
        {
            if (CB_Khoi_page3.SelectedIndex < 0 || CB_NamHoc_page3.SelectedIndex < 0)
            {
                MessageBox.Show("Chọn năm học và khối trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TB_search_page3_TextChanged(object sender, EventArgs e)
        {
            string text = TB_search_page3.Text;
            if (string.IsNullOrEmpty(text))
            {
                for (int i = 0; i < dataGridView_Tongket.RowCount; i++)
                {
                    dataGridView_Tongket.Rows[i].Visible = true;
                }
            }
            for (int i = 0; i < dataGridView_Tongket.RowCount; i++)
            {
                string mahs = dataGridView_Tongket.Rows[i].Cells[1].Value.ToString();
                string tenhs = dataGridView_Tongket.Rows[i].Cells[2].Value.ToString();
                if (!mahs.Contains(text) && !tenhs.Contains(text))
                {
                    dataGridView_Tongket.Rows[i].Visible = false;
                }
                else
                {
                    dataGridView_Tongket.Rows[i].Visible = true;
                }
            }
        }

        private void btn_Reset_p3_Click(object sender, EventArgs e)
        {
            cur_namHoc_page3 = -1;
            cur_khoi_page3 = -1;
            cur_lop_page3 = -1;
            listHS_page3 = new List<DiemtrbHS>();
            listLop_page3 = new List<Lop>();
            listNamHoc_page3 = new Dictionary<string, int>();


            CB_NamHoc_page3.Items.Clear();
            CB_NamHoc_page3.Text = "";
            CB_Lop_page3.Items.Clear();
            CB_Lop_page3.Text = "";
            CB_Khoi_page3.SelectedIndex = -1;
            GetandShowMaNamHocpage3();
            dataGridView_Tongket.Rows.Clear();
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

        private void btn_tinhtongket_p3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listHS_page3.Count; i++)
            {
                string _hk;
                bool tkhk1 = false;
                bool tkhk2 = false;
                if (dataGridView_Tongket.Rows[i].Cells[4].Value == null)
                {
                    _hk = "";
                }    
                else
                {
                   _hk = dataGridView_Tongket.Rows[i].Cells[4].Value.ToString();
                }    
                int hanhkiem1 = GetLoaiHanhKiem(_hk);
                bool tinh = true;
                List<double> diem1 = new List<double>();
                List<double> diem2 = new List<double>();
                for (int j = 0; j < 13; j++)
                {
                    if (listHS_page3[i].listdiemTrb1[j].diem == -1)
                    {
                        tinh = false;
                        break;
                    }
                    else
                    {
                        //str = str + "-" + listHS_page3[i].listdiemTrb1[j].diem;
                        diem1.Add(listHS_page3[i].listdiemTrb1[j].diem);
                    }
                }
                //MessageBox.Show(str);
                if (tinh == true && hanhkiem1 != -1)
                {
                    diemTongKet dtk = TinhDiemTongKetHocKy(diem1, hanhkiem1);
                    dataGridView_Tongket.Rows[i].Cells[3].Value = dtk.diemTrungBinh.ToString();
                    //dataGridView_Tongket.Rows[i].Cells[4].Value = hanhkiem;
                    dataGridView_Tongket.Rows[i].Cells[5].Value = dtk.xepLoai;
                    tkhk1 = true;
                }

                //Học ki2
                if (dataGridView_Tongket.Rows[i].Cells[7].Value == null)
                {
                    _hk = "";
                }
                else
                {
                    _hk = dataGridView_Tongket.Rows[i].Cells[7].Value.ToString();
                }
                int hanhkiem2 = GetLoaiHanhKiem(_hk);
                tinh = true;
                for (int j = 0; j < 13; j++)
                {
                    if (listHS_page3[i].listdiemTrb2[j].diem == -1)
                    {
                        tinh = false;
                        break;
                    }
                    else
                    {
                        //str = str + "-" + listHS_page3[i].listdiemTrb1[j].diem;
                        diem2.Add(listHS_page3[i].listdiemTrb2[j].diem);
                    }
                }
                //MessageBox.Show(str);
                if (tinh == true && hanhkiem2 != -1)
                {
                    diemTongKet dtk = TinhDiemTongKetHocKy(diem2, hanhkiem2);
                    dataGridView_Tongket.Rows[i].Cells[6].Value = dtk.diemTrungBinh.ToString();
                    //dataGridView_Tongket.Rows[i].Cells[4].Value = hanhkiem;
                    dataGridView_Tongket.Rows[i].Cells[8].Value = dtk.xepLoai;
                    tkhk2 = true;
                }

                if (tkhk1 && tkhk2)
                {
                    diemTongKet dtk = TinhDiemTongKetCaNam(diem1, hanhkiem1, diem2, hanhkiem2);
                    dataGridView_Tongket.Rows[i].Cells[9].Value = dtk.diemTrungBinh.ToString();
                    dataGridView_Tongket.Rows[i].Cells[10].Value = dtk.hanhKiem;
                    dataGridView_Tongket.Rows[i].Cells[11].Value = dtk.xepLoai;
                }
            }
        }

            //------------------page4-------------------

            List<Lop> listLopCu_page4 = new List<Lop>();
        List<Lop> listLopMoi_page4 = new List<Lop>();
        Dictionary<string, int> listNamHoc_page4 = new Dictionary<string, int>();
        Dictionary<int, int> listChuyen = new Dictionary<int, int>();//key = stt page mới, value = stt page cũ


        private void CB_NamHocCu_p4_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_LopCu_p4.Text = "";
            CB_LopCu_p4.Items.Clear();
            dataGridView_page4_lopcu.Rows.Clear();
            dataGridView_page4_lopmoi.Rows.Clear();
            if (Cb_KhoiCu_p4.SelectedIndex != -1)
            {
                GetLopCu_page4();
            }
        }

        private void Cb_KhoiCu_p4_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_LopCu_p4.Text = "";
            CB_LopCu_p4.Items.Clear();
            dataGridView_page4_lopcu.Rows.Clear();
            dataGridView_page4_lopmoi.Rows.Clear();
            if (CB_NamHocCu_p4.SelectedIndex != -1)
            {
                GetLopCu_page4();
            }
        }

        private void CB_LopCu_p4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = $"SELECT HS.MAHS, HS.HotenHS, HS.gioitinh, HS.Ghichu FROM HOCSINH AS HS, LOP AS L WHERE HS.MALOP = L.MALOP AND L.TENLOP = '{CB_LopCu_p4.SelectedItem.ToString()}' AND L.NAMHOC = '{CB_NamHocCu_p4.SelectedItem.ToString()}' AND L.MAKHOI = '{Cb_KhoiCu_p4.SelectedItem.ToString()}'";
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);
            int stt = 0;
            dataGridView_page4_lopcu.Rows.Clear();
            dataGridView_page4_lopmoi.Rows.Clear();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        string maHs = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                        string hoTen = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                        string gioiTinh = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetString(2).Trim();
                        string ghiChu = rdr.IsDBNull(3) ? GlobalProperties.NULLFIELD : rdr.GetString(3).Trim();

                        var index = dataGridView_page4_lopcu.Rows.Add();
                        dataGridView_page4_lopcu.Rows[index].Cells[0].Value = (++stt).ToString();//Số thứ tự
                        dataGridView_page4_lopcu.Rows[index].Cells[1].Value = maHs;
                        dataGridView_page4_lopcu.Rows[index].Cells[2].Value = hoTen;
                        dataGridView_page4_lopcu.Rows[index].Cells[3].Value = gioiTinh;
                        dataGridView_page4_lopcu.Rows[index].Cells[4].Value = ghiChu;
                    }
                }
            }
        }

        void GetandShowMaNamHocpage4()
        {
            CB_NamHocCu_p4.Items.Clear();
            CB_LopCu_p4.Items.Clear();

            GetNamHoc(out listNamHoc_page4);
            foreach (KeyValuePair<string, int> kvp in listNamHoc_page4)
            {
                CB_NamHocCu_p4.Items.Add(kvp.Key);
                CB_NamHocMoi_p4.Items.Add(kvp.Key);

            }
        }

        private void CB_LopCu_p4_Click(object sender, EventArgs e)
        {
            if (CB_NamHocCu_p4.SelectedIndex < 0 || Cb_KhoiCu_p4.SelectedIndex < 0)
            {
                MessageBox.Show("Chọn năm học và khối trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView_page4_lopcu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            int sttCu;
            Int32.TryParse(dataGridView_page4_lopcu.Rows[e.RowIndex].Cells[0].Value.ToString(), out sttCu);
            var index = dataGridView_page4_lopmoi.Rows.Add();
            for (int i = 1; i <= 4; i++)
            {
                dataGridView_page4_lopmoi.Rows[index].Cells[i].Value = dataGridView_page4_lopcu.Rows[e.RowIndex].Cells[i].Value;
            }

            dataGridView_page4_lopmoi.Rows[index].Cells[0].Value = (index + 1).ToString();

            listChuyen[index + 1] = sttCu;
            dataGridView_page4_lopcu.Rows[e.RowIndex].Visible = false;

        }
        private void dataGridView_page4_lopmoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            if (idx < 0)
            {
                return;
            }
            int sttCu = listChuyen[idx + 1];
            dataGridView_page4_lopmoi.Rows.RemoveAt(idx);
            dataGridView_page4_lopcu.Rows[sttCu - 1].Visible = true;
            listChuyen.Remove(idx + 1);
            for (int i = idx; i < dataGridView_page4_lopmoi.RowCount; i++)
            {
                int stt;
                Int32.TryParse(dataGridView_page4_lopmoi.Rows[i].Cells[0].Value.ToString(), out stt);
                dataGridView_page4_lopmoi.Rows[i].Cells[0].Value = (stt - 1).ToString();
                listChuyen[stt - 1] = listChuyen[stt];
                listChuyen.Remove(stt);
            }
        }

        private void CB_KhoiMoi_p4_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_LopMoi_p4.Text = "";
            CB_LopMoi_p4.Items.Clear();
            if (CB_NamHocMoi_p4.SelectedIndex != -1)
            {
                GetLopMoi_page4();
            }
        }

        private void CB_NamHocMoi_p4_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_LopMoi_p4.Text = "";
            CB_LopMoi_p4.Items.Clear();
            if (CB_KhoiMoi_p4.SelectedIndex != -1)
            {
                GetLopMoi_page4();
            }
        }

        private void CB_LopMoi_p4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void GetLopCu_page4()
        {
            CB_LopCu_p4.Items.Clear();
            listLopCu_page4.Clear();
            GetMaLop(Cb_KhoiCu_p4.SelectedItem.ToString(), CB_NamHocCu_p4.SelectedItem.ToString(), out listLopCu_page4);
            foreach (Lop p in listLopCu_page4)
                CB_LopCu_p4.Items.Add(p.TenLop);
        }

        private void btn_ChuyenLop_Click(object sender, EventArgs e)
        {
            if (CB_LopMoi_p4.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn lớp mới!", "Thông báo");
                return;
            }
            if (CB_LoaiChuyen.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Loại chuyển lớp!", "Thông báo");
                return;
            }
            if (CB_LoaiChuyen.SelectedIndex == 1) //Lên lớp
            {
                if (CB_NamHocCu_p4.SelectedItem.ToString() == CB_NamHocMoi_p4.SelectedItem.ToString())
                {
                    MessageBox.Show("Lên lớp không chọn cùng năm học!", "Thông báo");
                    return;
                }
            }
            if (CB_LoaiChuyen.SelectedIndex == 0) //Chuyển lớp
            {
                if (CB_NamHocCu_p4.SelectedItem.ToString() != CB_NamHocMoi_p4.SelectedItem.ToString() ||
                    Cb_KhoiCu_p4.SelectedItem.ToString() != CB_KhoiMoi_p4.SelectedItem.ToString())
                {
                    MessageBox.Show("Chuyển lớp vui chọn cùng năm học và cùng khối!", "Thông báo");
                    return;
                }
            }
            if (CB_NamHocCu_p4.SelectedItem.ToString() == CB_NamHocMoi_p4.SelectedItem.ToString() &&
                Cb_KhoiCu_p4.SelectedItem.ToString() == CB_KhoiMoi_p4.SelectedItem.ToString() &&
                CB_LopCu_p4.SelectedItem.ToString() == CB_LopMoi_p4.SelectedItem.ToString())
            {
                MessageBox.Show("Vui lòng không chọn trùng lớp cũ!", "Thông báo");
                return;
            }

            string maLopMoi = listLopMoi_page4[CB_LopMoi_p4.SelectedIndex].MaLop;
            string maLopCu = listLopCu_page4[CB_LopCu_p4.SelectedIndex].MaLop;
            string maNamHoc = CB_NamHocMoi_p4.SelectedItem.ToString();
            string query;
            SqlCommand cmd;
            for (int i = 0; i < dataGridView_page4_lopmoi.Rows.Count; i++)
            {
                string _maHS = dataGridView_page4_lopmoi.Rows[i].Cells[1].Value.ToString();

                //Set lại mã lớp cho hs
                query = $"UPDATE HOCSINH SET MALOP = '{maLopMoi}' WHERE MAHS = '{_maHS}'";
                try
                {
                    cmd = new SqlCommand(query, GlobalProperties.conn);
                    int rowCount = cmd.ExecuteNonQuery();
                    if (rowCount == 0)
                    {
                        MessageBox.Show($"Không thể thêm học sinh {dataGridView_page4_lopmoi.Rows[i].Cells[2].Value.ToString()}", "Thông báo");
                    }
                }
                catch (Exception ee)
                {
                    DialogResult dialogResult = MessageBox.Show("Lỗi trong quá trình thêm. Hiển thị lỗi?", "Thông báo", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        MessageBox.Show("Error: " + ee);
                    }
                    return;
                }
                if (CB_LoaiChuyen.SelectedIndex == 0)
                {
                    //Tạo thêm bảng DIEMMON cho HS nào còn thiếu

                }
                else
                {
                    //Lên lớp: Lưu tại LOPDAHOC, tạo 2 TABLE DIEMMON mới cho 2 học kì
                    //1.Check đã có tồn tại lớp này chưa, nếu có rồi thì ko thêm nữa
                    bool coLopCu = false;
                    query = $"SELECT COUNT(MALOPDAHOC) FROM LOPDAHOC WHERE MALOP = '{maLopCu}' AND MAHS = '{_maHS}'";
                    cmd = new SqlCommand(query, GlobalProperties.conn);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            rdr.Read();
                            int count = rdr.GetInt32(0);
                            if (count > 0)
                                coLopCu = true;
                        }
                    }

                    if (!coLopCu)// thêm vào LOPDAHOC
                    {
                        string key = GetKeyTable("SELECT COUNT(*) FROM LOPDAHOC WHERE MALOPDAHOC = ");
                        try
                        {
                            // Câu lệnh Insert.
                            query = $"INSERT INTO LOPDAHOC(MALOPDAHOC, MALOP, MAHS) VALUES('{key}', '{maLopCu}', '{_maHS}')";

                            cmd = new SqlCommand(query, GlobalProperties.conn);

                            int rowCount = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ee)
                        {
                            DialogResult dialogResult = MessageBox.Show("Lỗi trong quá trình thêm. Hiển thị lỗi?", "Thông báo", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                MessageBox.Show("Error: " + ee);
                            }
                        }
                    }
                    //đã có rồi thì thôi, ko thêm nữa 

                    //2.tạo 2 bang diem cho 2 hk năm học mới:
                    //TaoTableDiemMon2HK(maNamHoc, _maHS);
                }
                TaoTableDiemMon2HK(maNamHoc, _maHS);
            }
            MessageBox.Show("Đã lưu", "Thông báo!");
            CB_LopCu_p4_SelectedIndexChanged(sender, e);
            dataGridView_page4_lopmoi.Rows.Clear();

        }

        void GetLopMoi_page4()
        {
            CB_LopMoi_p4.Items.Clear();
            listLopMoi_page4.Clear();
            GetMaLop(CB_KhoiMoi_p4.SelectedItem.ToString(), CB_NamHocMoi_p4.SelectedItem.ToString(), out listLopMoi_page4);
            foreach (Lop p in listLopMoi_page4)
                CB_LopMoi_p4.Items.Add(p.TenLop);
        }

        private void g_TextChanged(object sender, EventArgs e) //Search_page4
        {
            string text = TB_search_page4.Text;
            if (string.IsNullOrEmpty(text))
            {
                for (int i = 0; i < dataGridView_page4_lopcu.RowCount; i++)
                {
                    dataGridView_page4_lopcu.Rows[i].Visible = true;
                }
            }
            for (int i = 0; i < dataGridView_page4_lopcu.RowCount; i++)
            {
                string mahs = dataGridView_page4_lopcu.Rows[i].Cells[1].Value.ToString();
                string tenhs = dataGridView_page4_lopcu.Rows[i].Cells[2].Value.ToString();
                if (!mahs.Contains(text) && !tenhs.Contains(text))
                {
                    dataGridView_page4_lopcu.Rows[i].Visible = false;
                }
                else
                {
                    dataGridView_page4_lopcu.Rows[i].Visible = true;
                }
            }
            foreach (KeyValuePair<int, int> kvp in listChuyen)
            {
                dataGridView_page4_lopcu.Rows[kvp.Value - 1].Visible = false;
            }
        }
        private void btn_reset_p4_Click(object sender, EventArgs e)
        {
            listLopCu_page4 = new List<Lop>();
            listLopMoi_page4 = new List<Lop>();
            listNamHoc_page4 = new Dictionary<string, int>();
            listChuyen = new Dictionary<int, int>();//key = stt page mới, value = stt page cũ
            CB_NamHocCu_p4.Text = "";
            CB_LopCu_p4.Text = "";
            CB_NamHocMoi_p4.Text = "";
            CB_LopMoi_p4.Text = "";

            GetandShowMaNamHocpage4();
            dataGridView_page4_lopcu.Rows.Clear();
            dataGridView_page4_lopmoi.Rows.Clear();
        }

        //---------page5------------------
        List<GiaoVien> listGV = new List<GiaoVien>();
        Dictionary<string, int> listNamHoc_page5 = new Dictionary<string, int>();
        string curKhoi_p5, curNamHoc_p5;
        void LoadPage5()
        {
            datetimepicker_nienkhoa_p5.Format = DateTimePickerFormat.Custom;
            datetimepicker_nienkhoa_p5.CustomFormat = "yyyy";
            datetimepicker_nienkhoa_p5.ShowUpDown = true;
            GetandShowMaNamHocpage5();
        }

        void GetandShowMaNamHocpage5()
        {
            CB_NamHoc_p5.Text = "";
            CB_NamHoc_p5.Items.Clear();
            GetNamHoc(out listNamHoc_page5);
            foreach (KeyValuePair<string, int> kvp in listNamHoc_page5)
            {
                CB_NamHoc_p5.Items.Add(kvp.Key);
            }
        }
        private void btn_hienthinienkhoap5_Click(object sender, EventArgs e)
        {
            dataGridView_nienkhoa_p5.Rows.Clear();
            string query = "SELECT MANK, NAMBD, NAMKT FROM NIENKHOA";
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        var index = dataGridView_nienkhoa_p5.Rows.Add();
                        for (int i = 0; i < 3; i++)
                        {
                            dataGridView_nienkhoa_p5.Rows[index].Cells[i].Value = rdr.IsDBNull(i) ? GlobalProperties.NULLFIELD : rdr.GetString(i).Trim();
                        }
                    }
                }
            }
        }

        private void datetimepicker_nienkhoa_p5_ValueChanged(object sender, EventArgs e)
        {
            TB_NamKT_p5.Text = (datetimepicker_nienkhoa_p5.Value.Year + 3).ToString();
            TB_MaNK_p5.Text = datetimepicker_nienkhoa_p5.Value.Year.ToString() + "-" + TB_NamKT_p5.Text;

        }

        private void btn_ThemNK_p5_Click(object sender, EventArgs e)
        {
            string query = $"INSERT INTO NIENKHOA(MANK, NAMBD, NAMKT) VALUES('{TB_MaNK_p5.Text}', '{datetimepicker_nienkhoa_p5.Value.Year.ToString()}', '{TB_NamKT_p5.Text}')";
            try
            {
                SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);
                int rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Không thể thêm niên khóa!", "Thông báo");
            }
            btn_hienthinienkhoap5.PerformClick();

        }

        private void btn_hienthi_Lop_p5_Click(object sender, EventArgs e)
        {

            if (CB_NamHoc_p5.SelectedIndex != -1 && CB_Khoi_p5.SelectedIndex != -1)
            {
                dataGridView_Lop_p5.Rows.Clear();
                string query = $"SELECT L.MALOP, L.TENLOP, GV.TENGV FROM LOP AS L, GIAOVIEN AS GV WHERE GV.MAGV =  L.MAGVCN AND L.MAKHOI = '{CB_Khoi_p5.SelectedItem.ToString()}' AND L.NAMHOC = '{CB_NamHoc_p5.SelectedItem.ToString()}'";
                SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var index = dataGridView_Lop_p5.Rows.Add();
                            for (int i = 0; i < 3; i++)
                            {
                                dataGridView_Lop_p5.Rows[index].Cells[i].Value = rdr.IsDBNull(i) ? GlobalProperties.NULLFIELD : rdr.GetString(i).Trim();
                            }
                        }
                    }
                }
                curKhoi_p5 = CB_Khoi_p5.SelectedItem.ToString();
                curNamHoc_p5 = CB_NamHoc_p5.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Chọn năm học và khối trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_Click(object sender, EventArgs e) //get thong tin giaos vieen
        {
            CB_gv_p5.Items.Clear();
            listGV.Clear();
            string query = $"SELECT GV.TENGV, GV.MAGV, GV.MAMH FROM  GIAOVIEN AS GV;";
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        string ten = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                        string magv = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                        string mamh = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetString(2).Trim();
                        listGV.Add(new GiaoVien(magv, mamh, ten));
                        CB_gv_p5.Items.Add(ten + " - " + magv);
                    }

                }
            }
        }

        private void CB_NamHoc_p5_Click(object sender, EventArgs e)
        {
            GetandShowMaNamHocpage5();
        }

        private void CB_gv_p5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //notthing
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e) //Thêm lớp mới
        {
            if (string.IsNullOrEmpty(TB_TenLopTao.Text) || CB_gv_p5.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập tên lớp và chọn giáo viên chủ nhiệm!", "Thông báo");
                return;
            }
            string key = GetKeyTable("SELECT COUNT(*) FROM LOP WHERE MALOP = ");
            for (int i = 0; i < dataGridView_Lop_p5.Rows.Count; i++)
            {
                if (dataGridView_Lop_p5.Rows[i].Cells[1].Value.ToString().ToLower() == TB_TenLopTao.Text.ToString().Trim().ToLower())
                {
                    MessageBox.Show("Tên lớp đã tồn tại!", "Thông báo");
                    return;
                }
            }
            try
            {
                string magv = listGV[CB_gv_p5.SelectedIndex].MaGV;
                // Câu lệnh Insert.
                MessageBox.Show(curKhoi_p5 + " " + magv + " " + TB_TenLopTao.Text.ToString() + " " + curNamHoc_p5);
                string query = $"INSERT INTO LOP(MALOP, MAKHOI, MAGVCN, TENLOP,  NAMHOC) VALUES('{key}', '{curKhoi_p5}', '{magv}', '{TB_TenLopTao.Text.ToString().Trim()}', '{curNamHoc_p5}')";

                SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);

                int rowCount = cmd.ExecuteNonQuery();
                MessageBox.Show("Đã lưu", "Thông báo");
                comboBox1_Click(sender, e);
                btn_hienthi_Lop_p5.PerformClick();
                CB_gv_p5.SelectedIndex = -1;
                CB_gv_p5.Text = "";


            }
            catch (Exception ee)
            {
                DialogResult dialogResult = MessageBox.Show("Lỗi trong quá trình thêm. Hiển thị lỗi?", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show("Error: " + ee);
                }
            }
        }

        //-------------page6------------------------------

        List<Lop> listLop_page6 = new List<Lop>();
        private void TB_MaHS_p6_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(TB_MaHS_p6.Text, @"^\d+$"))
            {
                Checkbox_Mahs.Checked = false;
                return;
            }
            if (!string.IsNullOrEmpty(TB_MaHS_p6.Text) && CheckMaHS(TB_MaHS_p6.Text))
            {
                Checkbox_Mahs.Checked = true;
                tb_Username_p6.Text = "HS" + TB_MaHS_p6.Text;
            }
            else
            {
                Checkbox_Mahs.Checked = false;
            }
        }

        private void btn_random_mahs_mk_Click(object sender, EventArgs e)
        {
            TB_matkhau_p6.Text = GlobalFunction.RandomString(6);
            string _maHS;
            do
            {
                _maHS = GlobalFunction.RandomStringInt(8);
                if (CheckMaHS(_maHS))
                {
                    TB_MaHS_p6.Text = _maHS;
                    tb_Username_p6.Text = "HS" + _maHS;
                    break;
                }
            } while (true);

        }

        private void btn_Luu_p6_Click(object sender, EventArgs e)
        {
            // Checkbox_Mah
            if (!Checkbox_Mahs.Checked)
            {
                MessageBox.Show("Mã học sinh không hợp lệ hoặc bị trùng. \n Mã học sinh chỉ bao gồm số", "Thông báo");
                return;
            }
            if (string.IsNullOrEmpty(TB_HoTen_p6.Text) ||
                string.IsNullOrEmpty(dateEdit_NgaySinh_p6.Text) ||
                CB_Gioitinh_p6.SelectedIndex < 0)
            {
                MessageBox.Show("Học sinh phải đảm bảo có tối thiểu 3 thông tin: Họ tên, Ngày sinh, Giới tính", "Thông báo");
                return;
            }
            if (string.IsNullOrEmpty(TB_SDT_p6.Text) ||
                Check_sdt.Checked == true)
            {

            }
            else
            {
                MessageBox.Show("Số điện thoại sai định dạng", "Thông báo");
                return;
            }
            if (CB_Lop_p6.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Lớp học", "Thông báo");
                return;
            }
            if (string.IsNullOrEmpty(TB_matkhau_p6.Text))
            {
                TB_matkhau_p6.Text = GlobalFunction.RandomString(6);
            }

            //Tạo tài khoản
            string _mataikhoan = GetKeyTable("SELECT COUNT(*) FROM TAIKHOAN WHERE MATK = ");
            string query = $"INSERT INTO TAIKHOAN(MATK, USERNAME, PASS) VALUES('{_mataikhoan}', '{tb_Username_p6.Text}', '{TB_matkhau_p6.Text}')";
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);
            int rowCount = cmd.ExecuteNonQuery();

            //Tạo học sinh:
            string _maHS = TB_MaHS_p6.Text;
            string _maLop = listLop_page6[CB_Lop_p6.SelectedIndex].MaLop;
            string _hoTen = TB_HoTen_p6.Text;
            string _ngaySinh = dateEdit_NgaySinh_p6.Text;
            string _diaChi = TB_DiaChi_p6.Text;
            string _gioiTinh = CB_Gioitinh_p6.SelectedItem.ToString();
            string _nienKhoa = CB_NienKhoa_p6.SelectedItem.ToString();
            string _maNamHoc = CB_NamHoc_p6.SelectedItem.ToString();
            string _sodt = TB_SDT_p6.Text;

            query = $"INSERT INTO HOCSINH(MAHS, MALOP, MATK, HotenHS, ngaysinh, diachi,	gioitinh, nienkhoa, sodt)	VALUES('{_maHS}', '{_maLop}', '{_mataikhoan}', N'{_hoTen}', '{_ngaySinh}', N'{_diaChi}', N'{_gioiTinh}', '{_nienKhoa}', '{_sodt}')";
            cmd = new SqlCommand(query, GlobalProperties.conn);
            rowCount = cmd.ExecuteNonQuery();

            TaoTableDiemMon2HK(_maNamHoc, _maHS);
            MessageBox.Show("Đã lưu", "Thông báo!");

            var index = dataGridView_HSThem_p6.Rows.Add();
            dataGridView_HSThem_p6.Rows[index].Cells[0].Value = _hoTen;
            dataGridView_HSThem_p6.Rows[index].Cells[1].Value = _ngaySinh;
            dataGridView_HSThem_p6.Rows[index].Cells[2].Value = _gioiTinh;
            dataGridView_HSThem_p6.Rows[index].Cells[3].Value = _sodt;
            dataGridView_HSThem_p6.Rows[index].Cells[4].Value = _diaChi;
            dataGridView_HSThem_p6.Rows[index].Cells[5].Value = _maHS;
            dataGridView_HSThem_p6.Rows[index].Cells[6].Value = tb_Username_p6.Text;
            dataGridView_HSThem_p6.Rows[index].Cells[7].Value = TB_matkhau_p6.Text;


            CB_Gioitinh_p6.SelectedIndex = -1;
            TB_MaHS_p6.Text = "";
            TB_HoTen_p6.Text = "";
            dateEdit_NgaySinh_p6.Text = "";
            TB_DiaChi_p6.Text = "";
            TB_matkhau_p6.Text = "";
            tb_Username_p6.Text = "";

        }

        private void CB_NamHoc_p6_Click(object sender, EventArgs e)
        {
            if (CB_NienKhoa_p6.SelectedIndex < 0)
            {
                MessageBox.Show("Chọn niên khóa trước", "Thông báo");
            }
            string nK = CB_NienKhoa_p6.SelectedItem.ToString();
            CB_NamHoc_p6.Items.Clear();
            string[] nam = nK.Split('-');
            int namBD = 0, namKT = 0;
            Int32.TryParse(nam[0], out namBD);
            Int32.TryParse(nam[1], out namKT);
            CB_NamHoc_p6.Items.Add(namBD.ToString() + "-" + (namBD + 1).ToString());
            CB_NamHoc_p6.Items.Add((namBD + 1).ToString() + "-" + (namBD + 2).ToString());
            CB_NamHoc_p6.Items.Add((namBD + 2).ToString() + "-" + (namBD + 3).ToString());
        }

        private void CB_Lop_p6_Click(object sender, EventArgs e)
        {
            CB_Lop_p6.Items.Clear();
            if (CB_NamHoc_p6.SelectedIndex < 0 && CB_Khoi_p6.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn năm học và khối!", "Thông báo");
            }
            listLop_page6.Clear();
            GetMaLop(CB_Khoi_p6.Text, CB_NamHoc_p6.Text, out listLop_page6);
            for (int i = 0; i < listLop_page6.Count; i++)
            {
                CB_Lop_p6.Items.Add(listLop_page6[i].TenLop);
            }
        }

        private void btn_delete_p6_Click(object sender, EventArgs e)
        {
            TB_HoTen_p6.Clear();
            dateEdit_NgaySinh_p6.Text = "";
            TB_DiaChi_p6.Clear();
            CB_Gioitinh_p6.Text = "";
            TB_SDT_p6.Clear();
            TB_MaHS_p6.Text = "";
            tb_Username_p6.Text = "";
            TB_matkhau_p6.Text = "";
        }

        private void CB_NienKhoa_p6_Click(object sender, EventArgs e)
        {
            CB_NienKhoa_p6.Items.Clear();
            string query = "SELECT MANK FROM NIENKHOA";
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        CB_NienKhoa_p6.Items.Add(rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim());
                    }

                }
            }

        }

        bool CheckMaHS(string mahs)
        {
            string query = $"SELECT COUNT(*) FROM HOCSINH WHERE MAHS = '{mahs}'";
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    rdr.Read();
                    int count = rdr.GetInt32(0);
                    if (count > 0)
                        return false;
                    else
                        return true;
                }
            }
            return false;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CB_NienKhoa_p6_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_NamHoc_p6.SelectedIndex = -1;
            CB_NamHoc_p6.Text = "";
            CB_NamHoc_p6.Items.Clear();
            CB_Lop_p6.SelectedIndex = -1;
            CB_Lop_p6.Text = "";
            CB_Lop_p6.Items.Clear();
        }

        private void CB_Khoi_p6_SelectedIndexChanged(object sender, EventArgs e)
        {

            CB_Lop_p6.SelectedIndex = -1;
            CB_Lop_p6.Text = "";
            CB_Lop_p6.Items.Clear();
        }

        private void TB_SDT_p6_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(TB_SDT_p6.Text, @"^\d+$"))
            {
                Checkbox_Mahs.Checked = false;
            }
            else
            {
                if (TB_SDT_p6.Text.Length == 10)
                {
                    Checkbox_Mahs.Checked = true;
                }
            }
        }


        //----------------page8-----------------------

        List<TaiKhoan> listTaiKhoan = new List<TaiKhoan>();
        private void btn_Hienthi_p8_Click(object sender, EventArgs e)
        {
            dataGridView_taikhoan_p8.Rows.Clear();
            listTaiKhoan.Clear();
            string query = "select HS.MAHS, HS.HotenHS,	HS.nienkhoa, L.TENLOP, TK.USERNAME, TK.PASS, HS.MATK from HOCSINH AS HS, LOP AS L, TAIKHOAN AS TK WHERE HS.MALOP = L.MALOP AND HS.MATK = TK.MATK";
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listTaiKhoan.Add(new TaiKhoan());
                        var idx = dataGridView_taikhoan_p8.Rows.Add();
                        dataGridView_taikhoan_p8.Rows[idx].Cells[0].Value =  rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                        dataGridView_taikhoan_p8.Rows[idx].Cells[1].Value = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                        dataGridView_taikhoan_p8.Rows[idx].Cells[2].Value = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetString(2).Trim();
                        dataGridView_taikhoan_p8.Rows[idx].Cells[3].Value = rdr.IsDBNull(3) ? GlobalProperties.NULLFIELD : rdr.GetString(3).Trim();
                        dataGridView_taikhoan_p8.Rows[idx].Cells[4].Value = rdr.IsDBNull(4) ? GlobalProperties.NULLFIELD : rdr.GetString(4).Trim();
                        dataGridView_taikhoan_p8.Rows[idx].Cells[5].Value = rdr.IsDBNull(5) ? GlobalProperties.NULLFIELD : rdr.GetString(5).Trim();
                        listTaiKhoan[listTaiKhoan.Count - 1].MaTK = rdr.IsDBNull(6) ? GlobalProperties.NULLFIELD : rdr.GetString(6).Trim();
                        listTaiKhoan[listTaiKhoan.Count - 1].Password = rdr.IsDBNull(5) ? GlobalProperties.NULLFIELD : rdr.GetString(5).Trim();
                    }

                }
            }
        }

        private void materialRaisedButton5_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < listTaiKhoan.Count; i++)
            {
                dataGridView_taikhoan_p8.Rows[i].Cells[5].Value = listTaiKhoan[i].Password;
            }
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listTaiKhoan.Count; i++)
            {
                string query = $"UPDATE TAIKHOAN SET PASS = '{dataGridView_taikhoan_p8.Rows[i].Cells[5].Value.ToString()}' WHERE MATK = '{listTaiKhoan[i].MaTK}'";
                listTaiKhoan[i].Password = dataGridView_taikhoan_p8.Rows[i].Cells[5].Value.ToString();
                SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);
                int rowCount = cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Đã lưu");
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đang phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đang phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }




        //----------Dùng chung các tab-------------
        string GetKeyTable(string query)
        {
            string key = "";
            bool f = false;
            //Tạo mới.
            do
            {
                key = GlobalFunction.RandomString(10);
                string sql = query + $"'{key}'";
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
            return key;
        }
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
                        string _siSo = rdr.IsDBNull(3) ? GlobalProperties.NULLFIELD : rdr.GetInt32(3).ToString();

                        listLop.Add(new Lop(_maLop, maKhoi, _maGVCN, _tenLop, _siSo));
                    }
                }
            }
        }
        void TaoTableDiemMon2HK(string maNamHoc, string maHS)
        {
            string query;
            SqlCommand cmd;
            for (int k = 1; k <= 2; k++)
            {
                string maHK = "HK" + k.ToString();
                for (int j = 0; j < 13; j++)
                {
                    string maMonHoc = GlobalProperties.listMaMH[j];
                    bool coDiemMon = false;
                    query = $"SELECT COUNT(*) FROM DIEMMON WHERE MAMONHOC = '{maMonHoc}' AND NAMHOC = '{maNamHoc}' AND MAHK = '{maHK}' AND MAHOCSINH = '{maHS}'";
                    cmd = new SqlCommand(query, GlobalProperties.conn);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            rdr.Read();
                            int count = rdr.GetInt32(0);
                            if (count > 0)
                                coDiemMon = true;
                        }
                    }

                    if (!coDiemMon)
                    {
                        //Tao DIEMMON moi
                        string key = GetKeyTable("SELECT COUNT(*) FROM DIEMMON WHERE MADIEMMON = ");
                        try
                        {
                            // Câu lệnh Insert.
                            query = $"INSERT INTO DIEMMON(MADIEMMON, MAMONHOC, MAHK, NAMHOC, MAHOCSINH) VALUES('{key}', '{maMonHoc}', '{maHK}', '{maNamHoc}', '{maHS}')";

                            cmd = new SqlCommand(query, GlobalProperties.conn);

                            int rowCount = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ee)
                        {
                            DialogResult dialogResult = MessageBox.Show("Lỗi trong quá trình thêm. Hiển thị lỗi?", "Thông báo", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                MessageBox.Show("Error: " + ee);
                            }
                        }
                    }
                }
            }
        }

        class diemTongKet
        {
            public string xepLoai;
            public double diemTrungBinh;
            public string hanhKiem;
        }

        diemTongKet TinhDiemTongKetHocKy(List<double> diem, int _hanhKiem)
        {
            Dictionary<string, int> hocLuc = new Dictionary<string, int>();
            hocLuc["Giỏi"] = 0;
            hocLuc["Khá"] = 1;
            hocLuc["Trung Bình"] = 2;
            hocLuc["Yếu"] = 3;

            Dictionary<string, int> hanhKiem = new Dictionary<string, int>();
            hanhKiem["Tốt"] = 0;
            hanhKiem["Khá"] = 1;
            hanhKiem["Trung bình"] = 2;
            hanhKiem["yếu"] = 3;
            diemTongKet hs = new diemTongKet();

            double toan = diem[0];
            double van = diem[1];
            double anhVan = diem[8];
            double theDuc = diem[11];

            diem.RemoveAt(11);

            //code here;
            int soMonHoc = diem.Count();
            double sum = 0;
            for (int i = 0; i < soMonHoc; i++)
            {
                sum += diem[i];
            }
            hs.diemTrungBinh = sum / soMonHoc;

            if (hs.diemTrungBinh >= 8)
            {
                if (diem[0] >= 8 || diem[1] >= 8 || diem[8] >= 8)
                {
                    for (int i = 0; i < soMonHoc; i++)
                    {
                        if (diem[i] >= 6.5)
                        {
                            hs.xepLoai = "Giỏi";
                        }
                        else if (diem[i] < 6.5)
                        {
                            hs.xepLoai = "Khá";
                            break;
                        }
                    }
                }
                else
                {
                    hs.xepLoai = "Khá";
                }
            }
            else if (hs.diemTrungBinh >= 6.5)
            {
                if (diem[0] >= 6.5 || diem[1] >= 6.5 || diem[8] >= 6.5)
                {
                    for (int i = 0; i < soMonHoc; i++)
                    {
                        if (diem[i] >= 5)
                        {
                            hs.xepLoai = "Khá";
                        }
                        else if (diem[i] < 5)
                        {
                            hs.xepLoai = "Trung bình";
                            break;
                        }
                    }
                }
                else
                {
                    hs.xepLoai = "Khá";
                }
            }
            else if (hs.diemTrungBinh >= 5)
            {
                if (diem[0] >= 5 || diem[1] >= 5 || diem[8] >= 5)
                {
                    for (int i = 0; i < soMonHoc; i++)
                    {
                        if (diem[i] >= 3.5)
                        {
                            hs.xepLoai = "Trung bình";
                        }
                        else if (diem[i] < 3.5)
                        {
                            hs.xepLoai = "Yếu";
                            break;
                        }
                    }
                }
                else
                {
                    hs.xepLoai = "Trung bình";
                }
            }
            else
            {
                hs.xepLoai = "Yếu";
            }

            if (hs.xepLoai == "Giỏi")
            {
                if (theDuc < 5)
                {
                    hs.xepLoai = "Khá";
                }
            }
            else if (hs.xepLoai == "Khá")
            {
                if (theDuc < 5)
                {
                    hs.xepLoai = "Trung bình";
                }
            }
            else if (hs.xepLoai == "Trung bình")
            {
                if (theDuc < 5)
                {
                    hs.xepLoai = "Yếu";
                }
            }
            else
            {
                hs.xepLoai = "Yếu";
            }

            for (int i = 0; i < soMonHoc; i++)
            {
                if (hs.diemTrungBinh >= 8)
                {
                    if (diem[i] >= 3.5 && diem[i] < 5)
                    {
                        hs.xepLoai = "Khá";
                    }
                    else if (diem[i] < 3.5)
                    {
                        hs.xepLoai = "Trung bình";
                    }
                }
                else if (hs.diemTrungBinh < 8 && hs.diemTrungBinh >= 6.5)
                {
                    if (diem[i] >= 3.5 && diem[i] < 5)
                    {
                        hs.xepLoai = "Trung Bình";
                    }
                    else if (diem[i] < 3.5)
                    {
                        hs.xepLoai = "Yếu";
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (hs.xepLoai == hocLuc.ElementAt(i).Key)
                {
                    int tmp = hocLuc.ElementAt(i).Value;

                    tmp += _hanhKiem;
                    if (tmp == 0)
                    {
                        hs.xepLoai = "Giỏi";
                    }
                    else if (tmp == 1)
                    {
                        hs.xepLoai = "Khá";
                    }
                    else if (tmp == 2)
                    {
                        hs.xepLoai = "Trung bình";
                    }
                    else
                    {
                        hs.xepLoai = "Yếu";
                    }
                    break;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (_hanhKiem == hanhKiem.ElementAt(i).Value)
                {
                    hs.hanhKiem = hanhKiem.ElementAt(i).Key;
                }
            }

            //hanh kiem: 0: kem, 1: trung binh, 2: kha, 3: tot;
            //Listdiem.count = 13
            //diem[11]: diem the duc
            //listTenMH = { "0-Toán học", "1-Ngữ văn", "Vật lí", "Hóa học", "Sinh học", "Tin học", "Lịch sử", "Địa lí", "8-Ngoại ngữ", "GDCD", "Công nghệ", "Thể dục", "GDQP" };
            return hs;
        }

        diemTongKet TinhDiemTongKetCaNam(List<double> diem1, int _hanhKiem1, List<double> diem2, int _hanhKiem2)
        {
            Dictionary<string, int> hocLuc = new Dictionary<string, int>();
            hocLuc["Giỏi"] = 0;
            hocLuc["Khá"] = 1;
            hocLuc["Trung Bình"] = 2;
            hocLuc["Yếu"] = 3;

            Dictionary<string, int> hanhKiem = new Dictionary<string, int>();
            hanhKiem["Tốt"] = 0;
            hanhKiem["Khá"] = 1;
            hanhKiem["Trung bình"] = 2;
            hanhKiem["yếu"] = 3;
            diemTongKet hs = new diemTongKet();
            List<double> diem = new List<double>();

            int soMonHoc = diem1.Count();

            for (int i = 0; i < soMonHoc; i++)
            {
                diem.Add((diem1[i] + diem2[i] * 2) / 3);
            }

            double theDuc = diem[11];
            diem.RemoveAt(11);
            soMonHoc = diem.Count();

            diemTongKet hk1 = TinhDiemTongKetHocKy(diem1, _hanhKiem1);
            diemTongKet hk2 = TinhDiemTongKetHocKy(diem2, _hanhKiem2);

            hs.diemTrungBinh = (hk1.diemTrungBinh + hk2.diemTrungBinh * 2) / 3;

            int _hanhKiemTB = (_hanhKiem1 + _hanhKiem2 * 2) / 3;
            for (int i = 0; i < 4; i++)
            {
                if (_hanhKiemTB == hanhKiem.ElementAt(i).Value)
                {
                    hs.hanhKiem = hanhKiem.ElementAt(i).Key;
                }
            }

            if (hs.diemTrungBinh >= 8)
            {
                if (diem[0] >= 8 || diem[1] >= 8 || diem[8] >= 8)
                {
                    for (int i = 0; i < soMonHoc; i++)
                    {
                        if (diem[i] >= 6.5)
                        {
                            hs.xepLoai = "Giỏi";
                        }
                        else if (diem[i] < 6.5)
                        {
                            hs.xepLoai = "Khá";
                            break;
                        }
                    }
                }
                else
                {
                    hs.xepLoai = "Khá";
                }
            }
            else if (hs.diemTrungBinh >= 6.5)
            {
                if (diem[0] >= 6.5 || diem[1] >= 6.5 || diem[8] >= 6.5)
                {
                    for (int i = 0; i < soMonHoc; i++)
                    {
                        if (diem[i] >= 5)
                        {
                            hs.xepLoai = "Khá";
                        }
                        else if (diem[i] < 5)
                        {
                            hs.xepLoai = "Trung bình";
                            break;
                        }
                    }
                }
                else
                {
                    hs.xepLoai = "Khá";
                }
            }
            else if (hs.diemTrungBinh >= 5)
            {
                if (diem[0] >= 5 || diem[1] >= 5 || diem[8] >= 5)
                {
                    for (int i = 0; i < soMonHoc; i++)
                    {
                        if (diem[i] >= 3.5)
                        {
                            hs.xepLoai = "Trung bình";
                        }
                        else if (diem[i] < 3.5)
                        {
                            hs.xepLoai = "Yếu";
                            break;
                        }
                    }
                }
                else
                {
                    hs.xepLoai = "Trung bình";
                }
            }
            else
            {
                hs.xepLoai = "Yếu";
            }

            if (hs.xepLoai == "Giỏi")
            {
                if (theDuc < 5)
                {
                    hs.xepLoai = "Khá";
                }
            }
            else if (hs.xepLoai == "Khá")
            {
                if (theDuc < 5)
                {
                    hs.xepLoai = "Trung bình";
                }
            }
            else if (hs.xepLoai == "Trung bình")
            {
                if (theDuc < 5)
                {
                    hs.xepLoai = "Yếu";
                }
            }
            else
            {
                hs.xepLoai = "Yếu";
            }

            for (int i = 0; i < soMonHoc; i++)
            {
                if (hs.diemTrungBinh >= 8)
                {
                    if (diem[i] >= 3.5 && diem[i] < 5)
                    {
                        hs.xepLoai = "Khá";
                    }
                    else if (diem[i] < 3.5)
                    {
                        hs.xepLoai = "Trung bình";
                    }
                }
                else if (hs.diemTrungBinh < 8 && hs.diemTrungBinh >= 6.5)
                {
                    if (diem[i] >= 3.5 && diem[i] < 5)
                    {
                        hs.xepLoai = "Trung Bình";
                    }
                    else if (diem[i] < 3.5)
                    {
                        hs.xepLoai = "Yếu";
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (hs.xepLoai == hocLuc.ElementAt(i).Key)
                {
                    int tmp = hocLuc.ElementAt(i).Value;

                    tmp += _hanhKiemTB;
                    if (tmp == 0)
                    {
                        hs.xepLoai = "Giỏi";
                    }
                    else if (tmp == 1)
                    {
                        hs.xepLoai = "Khá";
                    }
                    else if (tmp == 2)
                    {
                        hs.xepLoai = "Trung bình";
                    }
                    else
                    {
                        hs.xepLoai = "Yếu";
                    }
                    break;
                }
            }

            //hanh kiem: 0: kem, 1: trung binh, 2: kha, 3: tot;
            //Listdiem.count = 13
            //diem[11]: diem the duc
            //listTenMH = { "Toán học", "Ngữ văn", "Vật lí", "Hóa học", "Sinh học", "Tin học", "Lịch sử", "Địa lí", "Ngoại ngữ", "GDCD", "Công nghệ", "Thể dục", "GDQP" };
            return hs;
        }
    }
}
