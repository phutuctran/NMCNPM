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
    class DiemMon
    {
        private string maDiemMon;
        private string maMH;
        private string namHoc;
        private string maHK;
        private string maHS;
        private float diemTB;

        public DiemMon(string maDiemMon, string maMH, string namHoc, string maHK, string maHS, float diemTB)
        {
            this.maDiemMon = maDiemMon;
            this.maMH = maMH;
            this.namHoc = namHoc;
            this.maHK = maHK;
            this.maHS = maHS;
            this.diemTB = diemTB;
        }

        public string MaDiemMon { get => maDiemMon; set => maDiemMon = value; }
        public string MaMH { get => maMH; set => maMH = value; }
        public string NamHoc { get => namHoc; set => namHoc = value; }
        public string MaHK { get => maHK; set => maHK = value; }
        public string MaHS { get => maHS; set => maHS = value; }
        public float DiemTB { get => diemTB; set => diemTB = value; }

        public static List<DiemMon> ReadCSVFile(string fileName)
        {
            List<DiemMon> list = new List<DiemMon>();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int linesLength = lines.Length;
            for (int i = 0; i < linesLength; i++)
            {
                string[] columns = lines[i].Split(',');
                DiemMon obj = new DiemMon(columns[0], columns[1], columns[2], columns[3], columns[4], float.Parse(columns[5]));
                list.Add(obj);
            }
            return list;
        }

        public static void InsertToDB(List<DiemMon> list)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            try
            {
                string sql = "INSERT into DIEMMON VALUES (@maDiemMon, @maMH, @namHoc, @maHK, @maHS, @diemTB)";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                for (int i = 0; i < list.Count(); i++)
                {
                    cmd.Parameters.Add("@maDiemMon", SqlDbType.Char).Value = list[i].maDiemMon;
                    cmd.Parameters.Add("@maMH", SqlDbType.Char).Value = list[i].maMH;
                    cmd.Parameters.Add("@namHoc", SqlDbType.Char).Value = list[i].namHoc;
                    cmd.Parameters.Add("@maHK", SqlDbType.Char).Value = list[i].maHK;
                    cmd.Parameters.Add("@maHS", SqlDbType.Char).Value = list[i].maHS;
                    cmd.Parameters.Add("@diemTB", SqlDbType.Float).Value = list[i].diemTB;
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
