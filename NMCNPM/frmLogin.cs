using StudentManagementSystem.DatabaseCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            TB_password._TextBox.PasswordChar = '\u25CF';
            updateConnectStatus();
            if (!GlobalProperties.isConnectDatabase)
            {
                LB_connectDB_Click(LB_connectDB, EventArgs.Empty);
            }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LB_forgotPass_Click(object sender, EventArgs e)
        {
           
        }

        private void label6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đang phát triển, vui lòng liên hệ Quản trị viên!!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LB_connectDB_Click(object sender, EventArgs e)
        {
            using (Form form = new frmConnectDB())
            {
                form.ShowDialog();
                updateConnectStatus();
                GC.Collect();
            }
        }

        private void BT_Login_Click(object sender, EventArgs e)
        {
            //Các trường hợp chưa nhập đủ
            if (string.IsNullOrEmpty(TB_username.text) && string.IsNullOrEmpty(TB_password.text))
            {
                LB_LoginStatus.Visible = true;
                LB_LoginStatus.Text = "Nhập thông tin đăng nhập";
            }
            else if (string.IsNullOrEmpty(TB_username.text))
            {
                LB_LoginStatus.Visible = true;
                LB_LoginStatus.Text = "Nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(TB_password.text))
            {
                LB_LoginStatus.Visible = true;
                LB_LoginStatus.Text = "Nhập mật khẩu";
            }
            else //Đã đầy đủ, bắt đầu kiểm tra
            {
                string query = @"SELECT TK.USERNAME, TK.PASS
                                FROM TAIKHOAN AS TK
                                WHERE TK.USERNAME = " + $"'{TB_username.text.Trim()}'";
                if (GlobalProperties.isConnectDatabase == false)
                {
                    MessageBox.Show("chưa kết nối với Cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SqlCommand cmd = new SqlCommand(query, GlobalProperties.conn);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        GlobalProperties.curUsername = rdr.GetString(0).Trim();
                        GlobalProperties.curUserPassword = rdr.GetString(1).Trim();
                    }
                }
                if (GlobalProperties.curUserPassword == TB_password.text.Trim())
                {
                    int type = CheckUserType(GlobalProperties.curUsername);
                    this.Hide();
                    if (type == 1)
                    {
                        using (Form frm = new frmMain())
                        {
                            frm.ShowDialog();
                            GC.Collect();
                        }
                    }
                    else if (type == 2)
                    {
                        MessageBox.Show("Giáo vụ, đang phát triển");
                    }
                    else if (type == 3)
                    {
                        MessageBox.Show("Giáo viên, đang phát triển");
                    }
                    else if (type == 4)
                    {
                        string maHS = "";
                        query = @"SELECT HS.MAHS
                                FROM TAIKHOAN AS TK
                                INNER JOIN HOCSINH AS HS ON HS.MATK = TK.MATK
                                WHERE TK.USERNAME = " + $"'{GlobalProperties.curUsername.Trim()}'";

                        cmd = new SqlCommand(query, GlobalProperties.conn);
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                rdr.Read();
                                maHS = rdr.GetString(0);
                            }
                        }
                        //MessageBox.Show(maHS);
                        if (!string.IsNullOrEmpty(maHS))
                        {/////////////////////////////////////////
                            using (Form frm = new StudentInfoEdit(maHS))
                            {
                                frm.ShowDialog();
                                GC.Collect();
                            }
                         /////////////////////////////////////////
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không nhận diện được loại tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    TB_password.text = "";
                    LB_LoginStatus.Visible = false;
                    this.Show();
                }
                else
                {
                    LB_LoginStatus.Visible = true;
                    LB_LoginStatus.Text = "Sai mật khẩu!";
                    TB_password.text = "";
                }
                
            }
        }

        private int CheckUserType(string username) //return 1: admin, 2: GVu, 3: GVien, 4: HS
        {
            string type = username.Substring(0, 3);
            if (type == "GVU") //Giao vu
            {
                return 2;
            }
            else
            {
                type = username.Substring(0, 2);
                if (type == "ad")
                {
                    return 1;
                }
                else if (type == "HS")
                {
                    return 4;
                }
                else if (type == "GV")
                {
                    return 3;
                }
                else
                {
                    return 0;
                }
            }
        }

        private void updateConnectStatus()
        {
            if (GlobalProperties.isConnectDatabase)
            {
                LB_trangThaiKetNoi.Text = "Đã kết nối CSDL";
            }
            else
            {
                LB_trangThaiKetNoi.Text = "Chưa kết nối CSDL";
            }
        }

        private void TB_username_OnTextChange(object sender, EventArgs e)
        {
            if (TB_username.text == "")
            {
                TB_username.text = null;
            }
        }

        private void TB_password_OnTextChange(object sender, EventArgs e)
        {
            if (TB_password.text == "")
            {
                TB_password.text = null;
            }
        }
    }
}
