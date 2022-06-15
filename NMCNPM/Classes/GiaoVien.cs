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
    class GiaoVien
    {
        private string maGV;
        private string maMH;
        private string maTK;
        private string tenGV;
        private string diaChi;
        private string namSinh;
        private string gioiTinh;
        private string sdt;
        private string email;

        public GiaoVien() { }

        public GiaoVien(string maGV, string maMH, string tenGV)
        {
            this.maGV = maGV;
            this.maMH = maMH;
            this.tenGV = tenGV;
        }

        public GiaoVien(string maGV, string maMH, string maTK, string tenGV, string diaChi, string namSinh, string gioiTinh, string sdt, string email)
        {
            this.maGV = maGV;
            this.maMH = maMH;
            this.maTK = maTK;
            this.tenGV = tenGV;
            this.diaChi = diaChi;
            this.namSinh = namSinh;
            this.gioiTinh = gioiTinh;
            this.sdt = sdt;
            this.email = email;
        }

        public string MaGV { get => maGV; set => maGV = value; }
        public string MaMH { get => maMH; set => maMH = value; }
        public string MaTK { get => maTK; set => maTK = value; }
        public string TenGV { get => tenGV; set => tenGV = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string NamSinh { get => namSinh; set => namSinh = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string Email { get => email; set => email = value; }

        public override string ToString()
        {
            return maGV + " " + maMH + " " + maTK + " " + tenGV + " " + diaChi + " " + namSinh + " " + gioiTinh + " " + sdt + " " + email;
        }

        public static List<GiaoVien> readCSVFile(string fileName)
        {
            List<GiaoVien> gvList = new List<GiaoVien>();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int linesLength = lines.Length;
            for (int i = 0; i < linesLength; i++)
            {
                string[] columns = lines[i].Split(',');
                GiaoVien gv = new GiaoVien(columns[0], columns[1], columns[2], columns[3], columns[4], columns[5], columns[6], columns[7], columns[8]);
                gvList.Add(gv);
            }
            return gvList;
        }

        public static void InsertToDB(List<GiaoVien> list)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            try
            {
                string sql = "INSERT into GIAOVIEN VALUES (@maGV, @maMH, @maTK, @tenGV, @diaChi, @namSinh, @gioiTinh, @sdt, @email)";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                for (int i = 0; i < list.Count(); i++)
                {
                    cmd.Parameters.Add("@maGV", SqlDbType.Char).Value = list[i].maGV;
                    cmd.Parameters.Add("@maMH", SqlDbType.Char).Value = list[i].maMH;
                    cmd.Parameters.Add("@maTK", SqlDbType.Char).Value = list[i].maTK;
                    cmd.Parameters.Add("@tenGV", SqlDbType.NVarChar).Value = list[i].tenGV;
                    cmd.Parameters.Add("@diaChi", SqlDbType.NVarChar).Value = list[i].diaChi;
                    cmd.Parameters.Add("@namSinh", SqlDbType.Char).Value = list[i].namSinh;
                    cmd.Parameters.Add("@gioiTinh", SqlDbType.NVarChar).Value = list[i].gioiTinh;
                    cmd.Parameters.Add("@sdt", SqlDbType.NVarChar).Value = list[i].sdt;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = list[i].email;
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
