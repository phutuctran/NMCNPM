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
    class ChiTietDiem
    {
        private string maDM;
        private string maLKT;
        private float diem;

        public ChiTietDiem() { }
        public ChiTietDiem(string maDM, string maLKT, float diem)
        {
            this.maDM = maDM;
            this.maLKT = maLKT;
            this.diem = diem;
        }

        public string MaDM { get => maDM; set => maDM = value; }
        public string MaLKT { get => maLKT; set => maLKT = value; }
        public float Diem { get => diem; set => diem = value; }

        public static List<ChiTietDiem> ReadCSVFile(string fileName)
        {
            List<ChiTietDiem> ctdList = new List<ChiTietDiem>();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int linesLength = lines.Length;
            for (int i = 0; i < linesLength; i++)
            {
                string[] columns = lines[i].Split(',');
                ChiTietDiem ctd = new ChiTietDiem(columns[0], columns[1], float.Parse(columns[2]));
                ctdList.Add(ctd);
            }
            return ctdList;
        }

        public static void InsertToDB(List<ChiTietDiem> list)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            try
            {
                string sql = "INSERT into CHITIETDIEM(MADIEMMON, MALOATKT, DIEM) VALUES (@maDM, @maLKT, @diem)";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                for (int i = 0; i < list.Count(); i++)
                {
                    cmd.Parameters.Add("@maDM", SqlDbType.Char).Value = list[i].maDM;
                    cmd.Parameters.Add("@maLKT", SqlDbType.Char).Value = list[i].maLKT;
                    cmd.Parameters.Add("@diem", SqlDbType.Float).Value = list[i].diem;
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
