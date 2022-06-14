using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StudentManagementSystem.DatabaseCore
{
    public static class CheckDataFuntions
    {
        public static bool HaveStudent(string maHS)
        {
            string query = @"SELECT * FROM HOCSINH
                            WHERE MAHS = " + $"'{maHS}'";
            SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    return true;
                }
            }
            return false;
        } 
    }
}
