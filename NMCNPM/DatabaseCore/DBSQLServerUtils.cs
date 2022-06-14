using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



namespace StudentManagementSystem.DatabaseCore
{
    class DBSQLServerUtils
    {
        public static SqlConnection GetDBConnection(string datasource, string database, string username, string password)
        {
            //no password
            string connString = "Data Source = " + datasource + @"; Initial Catalog = " + database + @"; Integrated Security = True";
            
            //Data Source=.\SQLEXPRESS;Initial Catalog=THPT;Integrated Security=True
            SqlConnection connect = new SqlConnection(connString);
            return connect;
        }
    }
}
