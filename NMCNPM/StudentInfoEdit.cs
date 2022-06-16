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
using StudentManagementSystem.Classes;
using CSharpVitamins;
using System.IO;
using System.Drawing.Imaging;
using StudentManagementSystem.Properties;

namespace StudentManagementSystem
{
    public partial class StudentInfoEdit : MaterialForm
    {

        Dictionary<string, string> check_HK_NH = new Dictionary<string, string>();
        byte[] imageByte;
        int soMon = 13;
        string MaHS;
        List<(string, string)> ttBangDiem = new List<(string, string)>(); //thông tin học kì, năm học cho từng bảng điểm
        List<DiemThanhPhan> diem = new List<DiemThanhPhan>();//Lưu chi tiết bảng điểm
        HocSinh studentInfo = new HocSinh();
        string namHoc = "";
        string hocKi = "";
        double diemTrBHK;
        public StudentInfoEdit(string _MaHS) //_MaHS phải luôn tồn tại
        {
            InitializeComponent();
            circularPictureBox1.Image = Resources.UIT_Logo_NonBackground;
            MaHS = _MaHS;
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            GetDataStudent();
            ShowData();
            //datepicker_hs.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            //datepicker_hs.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //datepicker_hs.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            //Datepicker_cha.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            //Datepicker_cha.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //Datepicker_cha.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            //Datepicker_me.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            //Datepicker_me.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //Datepicker_me.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            //MessageBox.Show(Datepicker_cha.Text);

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            //this.PB_Avatar.Dispose();
        }

        //Xem thông tin bảng điểm
        private void btn_xem_Click(object sender, EventArgs e)
        {
            diemTrBHK = 0;
            diem = new List<DiemThanhPhan>();
            int idx = CB_ttHK_NH.SelectedIndex;
            if (idx < 0)
            {
                MessageBox.Show("Hãy chọn thông tin cần xem", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            namHoc = ttBangDiem[idx].Item2;
            hocKi = ttBangDiem[idx].Item1;
            dataGridView_Diem.Rows.Clear();
            GetandShowBangDiem(hocKi, namHoc);
        }

        private void bt_ResetPag1_Click(object sender, EventArgs e)
        {
            ShowDataPage1();
        }

        private void bt_SavePage1_Click(object sender, EventArgs e)
        {

            string sql = @"UPDATE HOCSINH 
                            SET HotenHS = @hoten, diachi = @diachi, noisinh = @noisinh, sodt = @sodt,
                                                email = @email, gioitinh = @gioitinh, dantoc = @dantoc, tongiao = @tongiao, tencha = @tencha, tenme = @tenme,
                                                nghenghiepcha = @nghecha, nghenghiepme = @ngheme, Ghichu = @ghichu
                            WHERE MAHS = @MAHS";

            string sqlUpdateNgaySinh = @"UPDATE HOCSINH 
                            SET ngaysinh = @ngaysinh
                            WHERE MAHS = @MAHS"; 
            string sqlUpdateNgaySinhCha = @"UPDATE HOCSINH 
                            SET ngaysinhcha = @sinhcha
                            WHERE MAHS = @MAHS"; 
            string sqlUpdateNgaySinhMe = @"UPDATE HOCSINH 
                            SET ngaysinhme = @sinhme
                            WHERE MAHS = @MAHS";

            string sqlUpadteANHHS = @"UPDATE HOCSINH 
                            SET ANHHS = @anhhs
                            WHERE MAHS = @MAHS";

            try
            {

                SqlCommand cmd = new SqlCommand(sql, GlobalProperties.conn);

                cmd.Parameters.Add("@MAHS", SqlDbType.Char).Value = MaHS.ToString();
                cmd.Parameters.Add("@hoten", SqlDbType.NVarChar).Value = TB_HoTen.Text;
                cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = TB_DiaChi.Text;
                cmd.Parameters.Add("@noisinh", SqlDbType.NVarChar).Value = TB_NoiSinh.Text;
                cmd.Parameters.Add("@sodt", SqlDbType.VarChar).Value = TB_Sodt.Text;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = TB_Email.Text;
                cmd.Parameters.Add("@gioitinh", SqlDbType.NVarChar).Value = CB_Sex.Text;
                cmd.Parameters.Add("@dantoc", SqlDbType.NVarChar).Value = TB_DanToc.Text;
                cmd.Parameters.Add("@tongiao", SqlDbType.NVarChar).Value = TB_TonGiao.Text;
                cmd.Parameters.Add("@tencha", SqlDbType.NVarChar).Value = TB_TenCha.Text;
                cmd.Parameters.Add("@tenme", SqlDbType.NVarChar).Value = TB_TenMe.Text;
                cmd.Parameters.Add("@nghecha", SqlDbType.NVarChar).Value = TB_NgheCha.Text;
                cmd.Parameters.Add("@ngheme", SqlDbType.NVarChar).Value = TB_NgheMe.Text;
                cmd.Parameters.Add("@ghichu", SqlDbType.NVarChar).Value = TB_GhiChu.Text;
                

                int rowCount = cmd.ExecuteNonQuery();
                
            }
            catch(Exception w)
            {
                MessageBox.Show(w.ToString());
                return;
            }
            //MessageBox.Show(Datepicker_cha.Text);
        //   //Update Ngày sinh
            if (!String.IsNullOrWhiteSpace(datepicker_hs.Text.Trim()))
            {
                try
                {
                    //MessageBox.Show(datepicker_hs);
                    SqlCommand cmd = new SqlCommand(sqlUpdateNgaySinh, GlobalProperties.conn);

                    cmd.Parameters.Add("@ngaysinh", SqlDbType.SmallDateTime).Value = datepicker_hs.Text;
                    cmd.Parameters.Add("@MAHS", SqlDbType.Char).Value = MaHS.ToString();

                    int rowCount = cmd.ExecuteNonQuery();
                }
                catch (Exception w)
                {
                    MessageBox.Show(w.ToString());
                    return;
                }
            }

            //MessageBox.Show(Datepicker_cha.Text);
            //UpDATE NGÀY SINH MẸ
            if (!String.IsNullOrWhiteSpace(Datepicker_me.Text.Trim()))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(sqlUpdateNgaySinhMe, GlobalProperties.conn);


                    cmd.Parameters.Add("@sinhme", SqlDbType.SmallDateTime).Value = Datepicker_me.Text;
                    cmd.Parameters.Add("@MAHS", SqlDbType.Char).Value = MaHS.ToString();

                    int rowCount = cmd.ExecuteNonQuery();
                }
                catch (Exception w)
                {
                    MessageBox.Show(w.ToString());
                    return;
                }
            }

            //update Ngày sinh cha
            if (!String.IsNullOrWhiteSpace(Datepicker_cha.Text.Trim()))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(sqlUpdateNgaySinhCha, GlobalProperties.conn);

                    cmd.Parameters.Add("@sinhcha", SqlDbType.SmallDateTime).Value = Datepicker_cha.Text;
                    cmd.Parameters.Add("@MAHS", SqlDbType.Char).Value = MaHS.ToString();

                    int rowCount = cmd.ExecuteNonQuery();
                }
                catch (Exception w)
                {
                    MessageBox.Show(w.ToString());
                    return;
                }
            }

            //Upadte ảnh
            if (imageByte != null)
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(sqlUpadteANHHS, GlobalProperties.conn);

                    cmd.Parameters.Add("@anhhs", SqlDbType.VarBinary).Value = imageByte;
                    cmd.Parameters.Add("@MAHS", SqlDbType.Char).Value = MaHS.ToString();

                    int rowCount = cmd.ExecuteNonQuery();
                }
                catch (Exception w)
                {
                    MessageBox.Show(w.ToString());
                    return;
                }
            }
            CB_ttHK_NH.Items.Clear();
            GetDataStudent();
            ShowDataPage1();
            MessageBox.Show("Đã lưu!", "Thông báo");

        }

        //Hiển thị chi tiết thông tin bảng điểm
        private void btn_hoantacpag2_Click(object sender, EventArgs e)
        {
            ShowDataPage2();
        }

        private void btn_Savepage2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < soMon; i++)
            {
                for (int j = 2; j <= 8; j++)
                {

                    string diem = dataGridView_Diem.Rows[i].Cells[j].Value == null? string.Empty : dataGridView_Diem.Rows[i].Cells[j].Value.ToString();
                    //MessageBox.Show(diem);
                    if (string.IsNullOrEmpty(diem.Trim()))
                    {
                        continue;
                    }
                    double diemthuc = GlobalFunction.CheckDiem(diem.Trim());
                    if (diemthuc == -1)
                    {
                        MessageBox.Show("Điểm nhập không hợp lệ", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            btn_TinhDTB.PerformClick();
            //string sqlDiemTB = "";
            
            //Cập nhật các cột điểm, chưa có điểm TRB
            for (int i = 0; i < soMon; i++)
            {
                for (int j = 2; j <= 7; j++)
                {

                    string _diem = dataGridView_Diem.Rows[i].Cells[j].Value == null? string.Empty : dataGridView_Diem.Rows[i].Cells[j].Value.ToString();
                    //MessageBox.Show(diem);
                    double diemthuc = GlobalFunction.CheckDiem(_diem.Trim());
                    string maDiem = GetMaDiem(i, j);
                    string maLoaiKT = GetMaLoaiKT(j);
                    if (string.IsNullOrEmpty(_diem.Trim()))
                    {
                        if (checkDiemTonTai(i, j))
                        {
                            //Xóa khỏi db;
                            DeleteChiTietDiem(maDiem, maLoaiKT);
                        }
                        continue;
                    }
                    if (maDiem != "")
                    {
                        if(UpdateDiem(maDiem, maLoaiKT, diemthuc))
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
                        string keyMaMonHoc = GetMaMonHoc(i);
                        //Tạo table DIEMMON
                        if (!diem[i].HaveTableDiemMon)
                        {
                            string keyMaDiemMon = GetMaDiemMonMoi();
                            if (InsertTableDiemMon(keyMaDiemMon, keyMaMonHoc, hocKi, MaHS))
                            {
                                diem[i].HaveTableDiemMon = true;
                                diem[i].MaDiemMon = keyMaDiemMon;
                                diem[i].MaMH = keyMaMonHoc;
                            }
                            else
                            {
                                ResetUpdateDiem(i, j);
                                return;
                            }
                            
                        }
                        //Thêm CHITIETDIEM
                        if (InsertChiTietDiem(diem[i].MaDiemMon, maLoaiKT, diemthuc))
                        {

                        }
                        else
                        {
                            ResetUpdateDiem(i, j);
                            return;
                        }
                    }
                }
            }

            //Cập nhật điểm trung bình

            for (int i = 0; i < soMon; i++)
            {
                string _diem = dataGridView_Diem.Rows[i].Cells[8].Value == null ? string.Empty : dataGridView_Diem.Rows[i].Cells[8].Value.ToString();
                //MessageBox.Show(diem);
                double diemthuc = GlobalFunction.CheckDiem(_diem.Trim());
                string maDiem = GetMaDiem(i, 8);
                if (string.IsNullOrEmpty(_diem.Trim()))
                {
                    if (checkDiemTonTai(i, 8))
                    {
                        if (UpdateDiemTrB(maDiem, -1))
                        {

                        }
                        else
                        {
                            ResetUpdateDiem(i, 8);
                            return;
                        }
                    }
                    continue;
                }
                if (maDiem != "")
                {
                    if (UpdateDiemTrB(maDiem, diemthuc))
                    {

                    }
                    else
                    {
                        ResetUpdateDiem(i, 8);
                        return;
                    }
                }
            }    
            

            dataGridView_Diem.Rows.Clear();
            GetandShowBangDiem(hocKi, namHoc);
            MessageBox.Show("Đã lưu", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btn_TinhDTB_Click(object sender, EventArgs e)
        {
            int[] heSo = { 1, 1, 1, 1, 2, 3 };
            double TrbHK = 0;
            int soMonHienTai = 0;
            for (int i = 0; i < soMon; i++)
            { 
                int tongHeSo = 0;
                double tongDiem = 0;
                int tongCotDiem = 0;
                for (int j = 2; j <= 7; j++)
                {
                    string diem = dataGridView_Diem.Rows[i].Cells[j].Value == null ? string.Empty : dataGridView_Diem.Rows[i].Cells[j].Value.ToString();
                    //MessageBox.Show(diem);
                    if (string.IsNullOrEmpty(diem.Trim()))
                    {
                        continue;
                    }
                    double diemthuc = GlobalFunction.CheckDiem(diem.Trim());
                    if (diemthuc == -1)
                    {
                        MessageBox.Show($"Điểm nhập không hợp lệ ở môn {GlobalProperties.listTenMH[i]}", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    tongCotDiem = (j >= 2 && j <= 5) ? 1 : tongCotDiem;
                    tongCotDiem += (j > 5) ? heSo[j - 2] : 0;
                    tongDiem += diemthuc * heSo[j - 2];
                    tongHeSo += heSo[j - 2];         
                }
                if (tongCotDiem == 6)
                {
                    double tmp = Math.Round(tongDiem / tongHeSo, 1);
                    dataGridView_Diem.Rows[i].Cells[8].Value = tmp.ToString();
                    if (i != 11)
                    {
                        TrbHK += tmp;
                        soMonHienTai++;
                    }
                }
                else
                {
                    dataGridView_Diem.Rows[i].Cells[8].Value = "";
                }
            }
            diemTrBHK = Math.Round(TrbHK / soMonHienTai, 1);
            dataGridView_Diem.Rows[soMon].Cells[8].Value = diemTrBHK != -1 ? diemTrBHK.ToString() : "";
            /////////////////////
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

        bool InsertTableDiemMon(string keyMaDiemMon, string keyMaMonHoc, string _hocKi, string _MaHS)
        {
            string sqlTaoTableDiemMon = @"INSERT INTO DIEMMON(MADIEMMON, MAMONHOC, MAHK, NAMHOC, MAHOCSINH)
	                                    VALUES(@madiemmon, @mamonhoc, @mahk, @manamhoc, @mahs)";
            try
            {
                SqlCommand cmd = new SqlCommand(sqlTaoTableDiemMon, GlobalProperties.conn);

                cmd.Parameters.Add("@madiemmon", SqlDbType.Char).Value = keyMaDiemMon.ToString();
                cmd.Parameters.Add("@mamonhoc", SqlDbType.Char).Value = keyMaMonHoc.ToString();
                cmd.Parameters.Add("@manamhoc", SqlDbType.Char).Value = namHoc;
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

        void GetandShowBangDiem(string HK, string NamHoc)
        {
            diem = new List<DiemThanhPhan>();
            dataGridView_Diem.Rows.Clear();
            string query = @"SELECT MN.MAMH, MN.TENMH, CTD.MADIEMMON, CTD.DIEM, LKT.TENLOAIKT, DM.TRUNGBINH
                            FROM CHITIETDIEM AS CTD
                            INNER JOIN DIEMMON AS DM ON CTD.MADIEMMON = DM.MADIEMMON 
                            LEFT JOIN LOAIKIEMTRA AS LKT ON LKT.MALOAIKT = CTD.MALOAIKT
                            LEFT JOIN HOCSINH AS HS ON HS.MAHS = DM.MAHOCSINH
                            LEFT JOIN MONHOC AS MN ON MN.MAMH = DM.MAMONHOC " +
                $"WHERE DM.MAHOCSINH = '{MaHS}' AND DM.MAHK = '{HK}' AND DM.NAMHOC = '{NamHoc}'";

            for (int i = 0; i < 13; i++)
            {
                diem.Add(new DiemThanhPhan(GlobalProperties.listMaMH[i], GlobalProperties.listTenMH[i]));
            }
            string maDiemMon;
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);
            string maMH;
            string tenMH = "";
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
                        double diemTB = rdr.IsDBNull(5) ? -1 : rdr.GetDouble(5);
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

                            if (f != -1)
                            {
                                diem[f].MaMH = maMH;
                                diem[f].HaveTableDiemMon = true;
                                diem[f].MaDiemMon = maDiemMon;
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

                                diem[f].DDGTRB = new DTP(diemTB, maDiemMon);
                            }

                        }
                    }
                }
                ShowDataPage2();
            }
        }

        void ResetUpdateDiem(int i, int j)
        {

        }

        string GetMaMonHoc(int i)
        {
            return GlobalProperties.listMaMH[i];
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
        
        string GetMaDiem(int x, int y) //Lấy mã điểm tại môn thứ i và cột thứ j
        {
            DiemThanhPhan d = diem[x];
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

        string GetMaLoaiKT(int y)//Mã loại Kiểm tra của cột x
        {
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

        void ShowDataPage1()
        {
            TB_MaHS.Text = studentInfo.MaHS;
            TB_HoTen.Text = studentInfo.HoTen;
            datepicker_hs.Text = studentInfo.NgaySinh;
            TB_DiaChi.Text = studentInfo.DiaChi;
            CB_Sex.Text = studentInfo.GioiTinh;
            CB_NienKhoa.Text = studentInfo.NienKhoa;
            TB_DanToc.Text = studentInfo.DanToc;
            TB_TonGiao.Text = studentInfo.TonGiao;
            TB_TenCha.Text = studentInfo.TenCha;
            TB_NgheCha.Text = studentInfo.NgheNghiepCha;
            Datepicker_cha.Text = studentInfo.NgaySinhCha;
            TB_TenMe.Text = studentInfo.TenMe;
            TB_NgheMe.Text = studentInfo.NgheNghiepMe;
            Datepicker_me.Text = studentInfo.NgaySinhMe;
            TB_GhiChu.Text = studentInfo.GhiChu;
            CB_Lop.Text = studentInfo.MaLop;
            TB_NoiSinh.Text = studentInfo.NoiSinh;
            TB_Sodt.Text = studentInfo.Sdt;
            TB_Email.Text = studentInfo.Email;
        }

        void ShowDataPage2()
        {
            
            int stt = 0;
            dataGridView_Diem.AllowUserToAddRows = true;
            dataGridView_Diem.AllowUserToDeleteRows = true;
            dataGridView_Diem.Rows.Clear();
            for (int i = 0; i < soMon; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView_Diem.Rows[0].Clone();

                row.Cells[0].Value = (++stt).ToString();//Số thứ tự
                row.Cells[1].Value = diem[i].TenMH;
                row.Cells[2].Value = diem[i].DDGTX1.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGTX1.diem.ToString();
                row.Cells[3].Value = diem[i].DDGTX2.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGTX2.diem.ToString();
                row.Cells[4].Value = diem[i].DDGTX3.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGTX3.diem.ToString();
                row.Cells[5].Value = diem[i].DDGTX4.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGTX4.diem.ToString();
                row.Cells[6].Value = diem[i].DDGGK.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGGK.diem.ToString();
                row.Cells[7].Value = diem[i].DDGCK.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGCK.diem.ToString();
                row.Cells[8].Value = diem[i].DDGTRB.diem == -1 ? GlobalProperties.NULLFIELD : diem[i].DDGTRB.diem.ToString();
                dataGridView_Diem.Rows.Add(row);
            }
            // Show diem TrB
            DataGridViewRow _row = (DataGridViewRow)dataGridView_Diem.Rows[0].Clone();

            _row.Cells[7].Value = "TrBHK:";
            _row.Cells[8].Value = diemTrBHK != -1 ? diemTrBHK.ToString() : "";
            dataGridView_Diem.Rows.Add(_row);
            dataGridView_Diem.Rows[soMon].ReadOnly = true;

            dataGridView_Diem.AllowUserToAddRows = false;
            dataGridView_Diem.AllowUserToDeleteRows = false;
        }

        void ShowData()
        {
            //MessageBox.Show(studentInfo.HoTen);
            // Page 1: student info 
            ShowDataPage1();
            //------------------------------
        }

        void GetDataStudent()
        {
            //Thông tin học sinh
            string query = @"SELECT HS.MAHS, HS.HotenHS, HS.NgaySinh, HS.diachi, HS.gioitinh, HS.nienkhoa, HS.dantoc,
                            HS.tongiao, HS.tencha, HS.nghenghiepcha, HS.ngaysinhcha, HS.tenme, HS.nghenghiepme, 
                            HS.ngaysinhme, HS.ghichu, L.TENLOP, HS.noisinh, HS.sodt, HS.email
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
                        studentInfo.MaHS = rdr.IsDBNull(0) ? GlobalProperties.NULLFIELD : rdr.GetString(0).Trim();
                        studentInfo.HoTen = rdr.IsDBNull(1) ? GlobalProperties.NULLFIELD : rdr.GetString(1).Trim();
                        studentInfo.NgaySinh = rdr.IsDBNull(2) ? GlobalProperties.NULLFIELD : rdr.GetDateTime(2).ToString("dd/MM/yyyy");
                        studentInfo.DiaChi = rdr.IsDBNull(3) ? GlobalProperties.NULLFIELD : rdr.GetString(3);
                        studentInfo.GioiTinh = rdr.IsDBNull(4) ? GlobalProperties.NULLFIELD : rdr.GetString(4);
                        studentInfo.NienKhoa = rdr.IsDBNull(5) ? GlobalProperties.NULLFIELD : rdr.GetString(5);
                        studentInfo.DanToc = rdr.IsDBNull(6) ? GlobalProperties.NULLFIELD : rdr.GetString(6);
                        studentInfo.TonGiao = rdr.IsDBNull(7) ? GlobalProperties.NULLFIELD : rdr.GetString(7);
                        studentInfo.TenCha = rdr.IsDBNull(8) ? GlobalProperties.NULLFIELD : rdr.GetString(8);
                        studentInfo.NgheNghiepCha = rdr.IsDBNull(9) ? GlobalProperties.NULLFIELD : rdr.GetString(9);
                        studentInfo.NgaySinhCha = rdr.IsDBNull(10) ? GlobalProperties.NULLFIELD : rdr.GetDateTime(10).ToString("dd/MM/yyyy");
                        studentInfo.TenMe = rdr.IsDBNull(11) ? GlobalProperties.NULLFIELD : rdr.GetString(11);
                        studentInfo.NgheNghiepMe = rdr.IsDBNull(12) ? GlobalProperties.NULLFIELD : rdr.GetString(12);
                        studentInfo.NgaySinhMe = rdr.IsDBNull(13) ? GlobalProperties.NULLFIELD : rdr.GetDateTime(13).ToString("dd/MM/yyyy");
                        studentInfo.GhiChu = rdr.IsDBNull(14) ? GlobalProperties.NULLFIELD : rdr.GetString(14);
                        studentInfo.MaLop = rdr.IsDBNull(15) ? GlobalProperties.NULLFIELD : rdr.GetString(15).Trim();
                        studentInfo.NoiSinh = rdr.IsDBNull(16) ? GlobalProperties.NULLFIELD : rdr.GetString(16).Trim();
                        studentInfo.Sdt = rdr.IsDBNull(17) ? GlobalProperties.NULLFIELD : rdr.GetString(17).Trim();
                        studentInfo.Email = rdr.IsDBNull(18) ? GlobalProperties.NULLFIELD : rdr.GetString(18).Trim();
                        //rdr.GetBytes

                    }

                }
            }

            //get hình ảnh

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
                            Image img = circularPictureBox1.Image;
                            circularPictureBox1.Image = GlobalFunction.ToImage(byteBLOBData);
                            if (img != null)
                            {
                                img.Dispose();
                            }
                        }

                    }
                }
            }
            catch { }

            check_HK_NH.Clear();
            ttBangDiem.Clear();
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
                        if (check_HK_NH.ContainsKey(namHoc) && check_HK_NH[namHoc] == hk)
                        {
                            continue;
                        }
                        check_HK_NH[namHoc] = hk;
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

        bool checkDiemTonTai(int x, int y) //Check có tồn tại điểm môn x, cột y không?
        {
            DiemThanhPhan _diem = diem[x];
            return (y == 2 && _diem.DDGTX1.diem != -1)
                || (y == 3 && _diem.DDGTX2.diem != -1)
                || (y == 4 && _diem.DDGTX3.diem != -1)
                || (y == 5 && _diem.DDGTX4.diem != -1)
                || (y == 6 && _diem.DDGGK.diem != -1)
                || (y == 7 && _diem.DDGCK.diem != -1)
                || (y == 8 && _diem.DDGTRB.diem != -1);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "PNG, JPG|*.png;*.jpg";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                Image img = circularPictureBox1.Image;
                circularPictureBox1.Image = new Bitmap(selectedFileName);
                imageByte = ReadFile(selectedFileName);
                if (img != null)
                {
                    img.Dispose();
                }
            }
        }
        byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes 
            //to read from file.
            //In this case we want to read entire file. 
            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);

            return data;
        }
    }
}
