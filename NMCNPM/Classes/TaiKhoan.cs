using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentManagementSystem.DatabaseCore;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace StudentManagementSystem.Classes
{
    class TaiKhoan
    {
        private string maTK;
        private string username;
        private string password;

        public TaiKhoan() { }
        public TaiKhoan(string maTK, string username, string password)
        {
            this.maTK = maTK;
            this.username = username;
            this.password = password;
        }

        public string MaTK { get => maTK; set => maTK = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

        public override string ToString()
        {
            return maTK + " " + username + " " + password;
        }

        public static List<TaiKhoan> ReadCSVFile(string fileName)
        {
            List<TaiKhoan> tkList = new List<TaiKhoan>();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int linesLength = lines.Length;
            for (int i = 0; i < linesLength; i++)
            {
                string[] columns = lines[i].Split(',');
                TaiKhoan tk = new TaiKhoan(columns[0], columns[1], columns[2]);
                tkList.Add(tk);
            }
            return tkList;
        }


        public static void InsertToDB(List<TaiKhoan> list)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            try
            {
                string sql = "INSERT into TAIKHOAN VALUES (@maTK, @username, @password)";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                for (int i = 0; i < list.Count(); i++)
                {
                    cmd.Parameters.Add("@maTK", SqlDbType.Char).Value = list[i].maTK;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = list[i].username;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = list[i].password;
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
