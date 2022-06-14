using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using StudentManagementSystem.DatabaseCore;

namespace StudentManagementSystem.Classes
{
    class HocSinh
    {
        private string maHS;
        private string maLop;
        private string maTK;
        private string hoTen;
        private string ngaySinh;
        private string diaChi;
        private string noiSinh;
        private string sdt;
        private string email;
        private string gioiTinh;
        private string nienKhoa;
        private string danToc;
        private string tonGiao;
        private string tenCha;
        private string ngheNghiepCha;
        private string ngaySinhCha;
        private string tenMe;
        private string ngheNghiepMe;
        private string ngaySinhMe;
        private string ghiChu;

        public HocSinh() { }

        public HocSinh(string maHS, string hoTen)
        {
            this.maHS = maHS;
            this.hoTen = hoTen;

        }

        public HocSinh(string maHS, string maLop, string maTK, string hoTen, string ngaySinh, string diaChi, string noiSinh, string sdt,
            string email, string gioiTinh, string nienKhoa, string danToc, string tonGiao, string tenCha, string ngheNghiepCha, string ngaySinhCha,
            string tenMe, string ngheNghiepMe, string ngaySinhMe, string ghiChu)
        {
            this.maHS = maHS;
            this.maLop = maLop;
            this.maTK = maTK;
            this.hoTen = hoTen;
            this.ngaySinh = ngaySinh;
            this.diaChi = diaChi;
            this.noiSinh = noiSinh;
            this.sdt = sdt;
            this.email = email;
            this.gioiTinh = gioiTinh;
            this.nienKhoa = nienKhoa;
            this.danToc = danToc;
            this.tonGiao = tonGiao;
            this.tenCha = tenCha;
            this.ngheNghiepCha = ngheNghiepCha;
            this.ngaySinhCha = ngaySinhCha;
            this.tenMe = tenMe;
            this.ngheNghiepMe = ngheNghiepMe;
            this.ngaySinhMe = ngaySinhMe;
            this.ghiChu = ghiChu;
        }

        public string MaHS { get => maHS; set => maHS = value; }
        public string MaLop { get => maLop; set => maLop = value; }
        public string MaTK { get => maTK; set => maTK = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string NoiSinh { get => noiSinh; set => noiSinh = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string Email { get => email; set => email = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string NienKhoa { get => nienKhoa; set => nienKhoa = value; }
        public string DanToc { get => danToc; set => danToc = value; }
        public string TonGiao { get => tonGiao; set => tonGiao = value; }
        public string TenCha { get => tenCha; set => tenCha = value; }
        public string NgheNghiepCha { get => ngheNghiepCha; set => ngheNghiepCha = value; }
        public string NgaySinhCha { get => ngaySinhCha; set => ngaySinhCha = value; }
        public string TenMe { get => tenMe; set => tenMe = value; }
        public string NgheNghiepMe { get => ngheNghiepMe; set => ngheNghiepMe = value; }
        public string NgaySinhMe { get => ngaySinhMe; set => ngaySinhMe = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }

        public override string ToString()
        {
            return maHS + " " + maLop + " " + maTK + " " + hoTen + " " + ngaySinh + " " + diaChi + " " + noiSinh + " " + sdt + " " + email + " " + gioiTinh
                + " " + nienKhoa + " " + danToc + " " + tonGiao + " " + tenCha + " " + ngheNghiepCha + " " + ngaySinhCha + " " + tenMe + " " + ngheNghiepMe +
                " " + ngaySinhMe + " " + ghiChu;
        }

        public static List<HocSinh> readCSVFile(string fileName)
        {
            List<HocSinh> hsList = new List<HocSinh>();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int linesLength = lines.Length;
            for (int i = 0; i < linesLength; i++)
            {
                string[] columns = lines[i].Split(',');
                HocSinh hs = new HocSinh(columns[0], columns[1], columns[2], columns[3], columns[4], columns[5], columns[6], columns[7], columns[8], columns[9], 
                    columns[10], columns[11], columns[12], columns[13], columns[14], columns[15], columns[16], columns[17], columns[18], columns[19]);
                hsList.Add(hs);
            }
            return hsList;
        }

        public static void InsertToDB(List<HocSinh> list)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            try
            {
                string sql = "INSERT into HOCSINH VALUES (@maHS, @maLop, @maTK, @hoTen, @ngaySinh, @diaChi, @noiSinh, @sdt, @email, @gioiTinh" +
                    "@nienKhoa, @danToc, @tonGiao, @tenCha, @ngheNghiepCha, @ngaySinhCha, @tenMe, @ngheNghiepMe, @ngaySinhMe, @ghiChu)";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                for (int i = 0; i < list.Count(); i++)
                {
                    cmd.Parameters.Add("@maHS", SqlDbType.Char).Value = list[i].maHS;
                    cmd.Parameters.Add("@maLop", SqlDbType.Char).Value = list[i].maLop;
                    cmd.Parameters.Add("@maTK", SqlDbType.Char).Value = list[i].maTK;
                    cmd.Parameters.Add("@hoTen", SqlDbType.NVarChar).Value = list[i].hoTen;
                    cmd.Parameters.Add("@ngaySinh", SqlDbType.SmallDateTime).Value = list[i].ngaySinh;
                    cmd.Parameters.Add("@diaChi", SqlDbType.NVarChar).Value = list[i].diaChi;
                    cmd.Parameters.Add("@noiSinh", SqlDbType.NVarChar).Value = list[i].noiSinh;
                    cmd.Parameters.Add("@sdt", SqlDbType.VarChar).Value = list[i].sdt;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = list[i].email;
                    cmd.Parameters.Add("@gioiTinh", SqlDbType.NVarChar).Value = list[i].gioiTinh;
                    cmd.Parameters.Add("@nienKhoa", SqlDbType.VarChar).Value = list[i].nienKhoa;
                    cmd.Parameters.Add("@danToc", SqlDbType.NVarChar).Value = list[i].danToc;
                    cmd.Parameters.Add("@tonGiao", SqlDbType.NVarChar).Value = list[i].tonGiao;
                    cmd.Parameters.Add("@tenCha", SqlDbType.NVarChar).Value = list[i].tenCha;
                    cmd.Parameters.Add("@ngheNghiepCha", SqlDbType.NVarChar).Value = list[i].ngheNghiepCha;
                    cmd.Parameters.Add("@ngaySinhCha", SqlDbType.SmallDateTime).Value = list[i].ngaySinhCha;
                    cmd.Parameters.Add("@tenMe", SqlDbType.NVarChar).Value = list[i].tenMe;
                    cmd.Parameters.Add("@ngheNghiepMe", SqlDbType.NVarChar).Value = list[i].ngheNghiepMe;
                    cmd.Parameters.Add("@ngaySinhMe", SqlDbType.SmallDateTime).Value = list[i].ngaySinhMe;
                    cmd.Parameters.Add("@ghiChu", SqlDbType.NVarChar).Value = list[i].ghiChu;
                }
                int rowCount = cmd.ExecuteNonQuery();
                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            Console.Read();
        }
    }
}
