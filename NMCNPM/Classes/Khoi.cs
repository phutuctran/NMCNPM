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
    class Khoi
    {
        private string maKhoi;
        private string tenKhoi;

        public Khoi() { }
        public Khoi(string maKhoi, string tenKhoi)
        {
            this.maKhoi = maKhoi;
            this.tenKhoi = tenKhoi;
        }

        public string MaKhoi { get => maKhoi; set => maKhoi = value; }
        public string TenKhoi { get => tenKhoi; set => tenKhoi = value; }

        public override string ToString()
        {
            return maKhoi + " " + tenKhoi;
        }

        public static List<Khoi> ReadCSVFile(string fileName)
        {
            List<Khoi> kList = new List<Khoi>();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int linesLength = lines.Length;
            for (int i = 0; i < linesLength; i++)
            {
                string[] columns = lines[i].Split(',');
                Khoi k = new Khoi(columns[0], columns[1]);
                kList.Add(k);
            }
            return kList;
        }

        public static void InsertToDB(List<Khoi> list)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            try
            {
                string sql = "INSERT into KHOI VALUES (@maKhoi, @tenKhoi)";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                for (int i = 0; i < list.Count(); i++)
                {
                    cmd.Parameters.Add("@maKhoi", SqlDbType.Char).Value = list[i].maKhoi;
                    cmd.Parameters.Add("@tenKhoi", SqlDbType.NChar).Value = list[i].tenKhoi;
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
