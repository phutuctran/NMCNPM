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
    public class Lop
    {
        private string maLop;
        private string maKhoi;
        private string maGVCN;
        private string tenLop;
        private string siSo;

        public Lop() { }

        public Lop(string maLop = "", string maKhoi = "", string maGVCN = "", string tenLop = "", string siSo = "")
        {
            this.maLop = maLop;
            this.maKhoi = maKhoi;
            this.maGVCN = maGVCN;
            this.tenLop = tenLop;
            this.siSo = siSo;
        }

        public string MaLop { get => maLop; set => maLop = value; }
        public string MaKhoi { get => maKhoi; set => maKhoi = value; }
        public string MaGVCN { get => maGVCN; set => maGVCN = value; }
        public string TenLop { get => tenLop; set => tenLop = value; }
        public string SiSo { get => siSo; set => siSo = value; }

        public override string ToString()
        {
            return maLop + " " + maKhoi + " " + maGVCN + " " + tenLop + " " + siSo;
        }

        public static List<Lop> ReadCSVFile(string fileName)
        {
            List<Lop> lopList = new List<Lop>();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int linesLength = lines.Length;
            for (int i = 0; i < linesLength; i++)
            {
                string[] columns = lines[i].Split(',');
                Lop lop = new Lop(columns[0], columns[1], columns[2], columns[3], columns[4]);
                lopList.Add(lop);
            }
            return lopList;
        }

        public static void InsertToDB(List<Lop> list)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            try
            {
                string sql = "INSERT into LOP VALUES (@maLop, @maKhoi, @maGVCN, @tenLop, @siSo)";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                for (int i = 0; i < list.Count(); i++)
                {
                    cmd.Parameters.Add("@maLop", SqlDbType.Char).Value = list[i].maLop;
                    cmd.Parameters.Add("@maKhoi", SqlDbType.Char).Value = list[i].maKhoi;
                    cmd.Parameters.Add("@maGVCN", SqlDbType.Char).Value = list[i].maGVCN;
                    cmd.Parameters.Add("@tenLop", SqlDbType.NVarChar).Value = list[i].tenLop;
                    cmd.Parameters.Add("@siSo", SqlDbType.Char).Value = list[i].siSo;
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
