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
    class GiangDay
    {
        private string maGV;
        private string maLop;

        public GiangDay() { }
        public GiangDay(string maGV, string maLop)
        {
            this.maGV = maGV;
            this.maLop = maLop;
        }

        public string MaGV { get => maGV; set => maGV = value; }
        public string MaLop { get => maLop; set => maLop = value; }

        public static List<GiangDay> ReadCSVFile(string fileName)
        {
            List<GiangDay> gdList = new List<GiangDay>();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int linesLength = lines.Length;
            for (int i = 0; i < linesLength; i++)
            {
                string[] columns = lines[i].Split(',');
                GiangDay gd = new GiangDay(columns[0], columns[1]);
                gdList.Add(gd);
            }
            return gdList;
        }

        public static void InsertToDB(List<GiangDay> list)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            try
            {
                string sql = "INSERT into GIANGDAY VALUES (@maGV, @maLop)";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                for (int i = 0; i < list.Count(); i++)
                {
                    cmd.Parameters.Add("@maGV", SqlDbType.Char).Value = list[i].maGV;
                    cmd.Parameters.Add("@maLop", SqlDbType.Char).Value = list[i].maLop;
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
