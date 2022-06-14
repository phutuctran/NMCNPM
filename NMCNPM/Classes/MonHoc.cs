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
    class MonHoc
    {
        private string maMH;
        private string tenMH;

        public MonHoc() { }
        public MonHoc(string maMH, string tenMH)
        {
            this.maMH = maMH;
            this.tenMH = tenMH;
        }

        public string MaMH { get => maMH; set => maMH = value; }
        public string TenMH { get => tenMH; set => tenMH = value; }

        public override string ToString()
        {
            return maMH + " " + tenMH;
        }

        public static List<MonHoc> readCSVFile(string fileName)
        {
            List<MonHoc> mhList = new List<MonHoc>();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int linesLength = lines.Length;
            for (int i = 0; i < linesLength; i++)
            {
                string[] columns = lines[i].Split(',');
                MonHoc mh = new MonHoc(columns[0], columns[1]);
                mhList.Add(mh);
            }
            return mhList;
        }

        public static void InsertToDB(List<MonHoc> list)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            try
            {
                string sql = "INSERT into MONHOC VALUES (@maMH, @tenMH)";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                for (int i = 0; i < list.Count(); i++)
                {
                    cmd.Parameters.Add("@maMH", SqlDbType.Char).Value = list[i].maMH;
                    cmd.Parameters.Add("@tenMH", SqlDbType.NVarChar).Value = list[i].tenMH;
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
