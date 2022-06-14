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
    class LoaiKiemTra
    {
        private string maLoaiKT;
        private string tenLoaiKT;

        public LoaiKiemTra() { }
        public LoaiKiemTra(string maLoaiKT, string tenLoaiKT)
        {
            this.maLoaiKT = maLoaiKT;
            this.tenLoaiKT = tenLoaiKT;
        }
        public string MaLoaiKT1 { get => maLoaiKT; set => maLoaiKT = value; }
        public string TenLoaiKT1 { get => tenLoaiKT; set => tenLoaiKT = value; }

        public static List<LoaiKiemTra> ReadCSVFile(string fileName)
        {
            List<LoaiKiemTra> list = new List<LoaiKiemTra>();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int linesLength = lines.Length;
            for (int i = 0; i < linesLength; i++)
            {
                string[] columns = lines[i].Split(',');
                LoaiKiemTra lkt = new LoaiKiemTra(columns[0], columns[1]);
                list.Add(lkt);
            }
            return list;
        }

        public static void InsertToDB(List<LoaiKiemTra> list)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            try
            {
                string sql = "INSERT into LOAIKIEMTRA VALUES (@maLoaiKT, @tenLoaiKT)";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                for (int i = 0; i < list.Count(); i++)
                {
                    cmd.Parameters.Add("@maLoaiKT", SqlDbType.Char).Value = list[i].maLoaiKT;
                    cmd.Parameters.Add("@tenLoaiKT", SqlDbType.VarChar).Value = list[i].tenLoaiKT;
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
