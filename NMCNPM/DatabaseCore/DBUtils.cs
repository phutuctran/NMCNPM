using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StudentManagementSystem.DatabaseCore
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection(string severName = @".\SQLEXPRESS", string dbName = "THPT")
        {
            string datasource = severName;
            string database = dbName;
            string username = "";
            string password = "";
            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}
