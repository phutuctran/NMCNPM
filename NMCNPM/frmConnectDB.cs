using StudentManagementSystem.DatabaseCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class frmConnectDB : Form
    {
        string password_XacThuc = "1234";
        public frmConnectDB()
        {
            InitializeComponent();
            panel1.BringToFront();
            TB_pass._TextBox.PasswordChar = '\u25CF';
            TB_pass._TextBox.MaxLength = 10;
            if (GlobalProperties.isConnectDatabase)
            {
                LB_trangThaiKetNoi.Text = "Trạng thái: Đã kết nối CSDL";
            }
            else
            {
                LB_trangThaiKetNoi.Text = "Trạng thái: Chưa kết nối CSDL";
            }
            using (StreamReader readtext = new StreamReader(GlobalProperties.serverBDNamepath))
            {
                password_XacThuc = readtext.ReadLine();
                MessageBox.Show(password_XacThuc);
            }
        }

        private void BT_xacThuc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TB_pass.text))
            {
                //LB_trangThaiXacThuc.Visible = true;
                LB_trangThaiXacThuc.Text = "Vui lòng nhập mật khẩu!";
            }
            else
            {
                if (TB_pass.text.Trim() == password_XacThuc)
                {
                    panel1.Visible = false;
                }
                else
                {
                    //LB_trangThaiXacThuc.Visible = true;
                    LB_trangThaiXacThuc.Text = "Mật khẩu sai!";
                }
            }

        }

        private void BT_Close1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BT_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BT_Connect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TB_severName.text) || string.IsNullOrEmpty(TB_DBName.text))
            {
                LB_trangThaiDB.Text = "Nhập đầy đủ thông tin!";
            }
            else
            {
                string textfile = password_XacThuc + Environment.NewLine + TB_severName.text + Environment.NewLine + TB_DBName.text;
                try
                {
                    LB_trangThaiKetNoi.Text = "Trạng thái: Đang nhận và mở kết nối...";
                    GlobalProperties.conn = DBUtils.GetDBConnection(TB_severName.text, TB_DBName.text);
                    GlobalProperties.conn.Open();
                    LB_trangThaiKetNoi.Text = "Trạng thái: Kết nối thành công...";
                    File.WriteAllText(GlobalProperties.serverBDNamepath, textfile);
                    GlobalProperties.isConnectDatabase = true;
                    string sql = "SET DATEFORMAT dmy";
                    SqlCommand cmd = new SqlCommand(sql, GlobalProperties.conn);
                    //this.result
                    //this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    LB_trangThaiKetNoi.Text = "Trạng thái: Không thể kết nối...";
                    GlobalProperties.isConnectDatabase = false;
                    //this.DialogResult = DialogResult.No;
                }
            }
        }
    }
}
