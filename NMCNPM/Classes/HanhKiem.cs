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
    class HanhKiem
    {
        private string maHK;
        private string maHS;
        private string xepLoaiHK1;
        private string xepLoaiHK2;
        private string xepLoaiCN;

        public HanhKiem() { }
        public HanhKiem(string maHK, string maHS, string xepLoaiHK1, string xepLoaiHK2, string xepLoaiCN)
        {
            this.maHK = maHK;
            this.maHS = maHS;
            this.xepLoaiHK1 = xepLoaiHK1;
            this.xepLoaiHK2 = xepLoaiHK2;
            this.xepLoaiCN = xepLoaiCN;
        }

        public string MaHK { get => maHK; set => maHK = value; }
        public string MaHS { get => maHS; set => maHS = value; }
        public string XepLoaiHK1 { get => xepLoaiHK1; set => xepLoaiHK1 = value; }
        public string XepLoaiHK2 { get => xepLoaiHK2; set => xepLoaiHK2 = value; }
        public string XepLoaiCN { get => xepLoaiCN; set => xepLoaiCN = value; }

        public static List<HanhKiem> ReadCSVFile(string fileName)
        {
            List<HanhKiem> hkList = new List<HanhKiem>();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int linesLength = lines.Length;
            for (int i = 0; i < linesLength; i++)
            {
                string[] columns = lines[i].Split(',');
                HanhKiem hk = new HanhKiem(columns[0], columns[1], columns[2], columns[3], columns[4]);
                hkList.Add(hk);
            }
            return hkList;
        }

        public static void InsertToDB(List<HanhKiem> list)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            try
            {
                string sql = "INSERT into HANHKIEM VALUES (@maHK, @maHS, @xepLoaiHK1, @xepLoaiHK2, @xepLoaiCN)";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                for (int i = 0; i < list.Count(); i++)
                {
                    cmd.Parameters.Add("@maHK", SqlDbType.Char).Value = list[i].maHK;
                    cmd.Parameters.Add("@maHS", SqlDbType.Char).Value = list[i].maHS;
                    cmd.Parameters.Add("@xepLoaiHK1", SqlDbType.NVarChar).Value = list[i].xepLoaiHK1;
                    cmd.Parameters.Add("@xepLoaiHK2", SqlDbType.NVarChar).Value = list[i].xepLoaiHK2;
                    cmd.Parameters.Add("@xepLoaiCN", SqlDbType.NVarChar).Value = list[i].xepLoaiCN;
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
