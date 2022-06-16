using StudentManagementSystem.DatabaseCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (StreamReader readtext = new StreamReader(GlobalProperties.serverBDNamepath))
            {
                string severName = readtext.ReadLine();
                severName = readtext.ReadLine();
                string databaseName = readtext.ReadLine();
                try
                {
                    GlobalProperties.conn = DBUtils.GetDBConnection(severName, databaseName);
                    GlobalProperties.conn.Open();
                    GlobalProperties.isConnectDatabase = true;
                    string query = "SET DATEFORMAT dmy";
                    SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);
                    int rowCount = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    GlobalProperties.isConnectDatabase = false;
                }
            }
            Application.Run(new Login());
            //Application.Run(new StudentInfoEdit("HS80901207"));
        }
    }
}
