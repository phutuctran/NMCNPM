
using Bunifu.Framework.UI;

namespace StudentManagementSystem
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LB_LoginStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_password = new Bunifu.Framework.UI.BunifuTextbox();
            this.TB_username = new Bunifu.Framework.UI.BunifuTextbox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BT_Login = new MaterialSkin.Controls.MaterialRaisedButton();
            this.LB_connectDB = new System.Windows.Forms.Label();
            this.LB_trangThaiKetNoi = new System.Windows.Forms.Label();
            this.BT_Exit = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BT_Exit)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 450);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(42, 271);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "QUẢN LÍ HỌC SINH";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 14.25F);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(88, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "HỆ THỐNG";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::StudentManagementSystem.Properties.Resources.schoollogo_nonbackground;
            this.pictureBox1.Location = new System.Drawing.Point(75, 70);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(129, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.LB_LoginStatus, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TB_password, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.TB_username, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.BT_Login, 0, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(330, 103);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(268, 261);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // LB_LoginStatus
            // 
            this.LB_LoginStatus.AutoSize = true;
            this.LB_LoginStatus.Font = new System.Drawing.Font("Roboto", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_LoginStatus.ForeColor = System.Drawing.Color.Firebrick;
            this.LB_LoginStatus.Location = new System.Drawing.Point(3, 155);
            this.LB_LoginStatus.Name = "LB_LoginStatus";
            this.LB_LoginStatus.Size = new System.Drawing.Size(80, 19);
            this.LB_LoginStatus.TabIndex = 13;
            this.LB_LoginStatus.Text = "Trạng thái";
            this.LB_LoginStatus.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Roboto", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tên đăng nhập:";
            // 
            // TB_password
            // 
            this.TB_password.BackColor = System.Drawing.SystemColors.Control;
            this.TB_password.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TB_password.BackgroundImage")));
            this.TB_password.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TB_password.ForeColor = System.Drawing.Color.Gray;
            this.TB_password.Icon = ((System.Drawing.Image)(resources.GetObject("TB_password.Icon")));
            this.TB_password.Location = new System.Drawing.Point(4, 104);
            this.TB_password.Margin = new System.Windows.Forms.Padding(4);
            this.TB_password.Name = "TB_password";
            this.TB_password.Size = new System.Drawing.Size(250, 45);
            this.TB_password.TabIndex = 6;
            this.TB_password.text = "";
            this.TB_password.OnTextChange += new System.EventHandler(this.TB_password_OnTextChange);
            // 
            // TB_username
            // 
            this.TB_username.BackColor = System.Drawing.SystemColors.Control;
            this.TB_username.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TB_username.BackgroundImage")));
            this.TB_username.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TB_username.ForeColor = System.Drawing.Color.Gray;
            this.TB_username.Icon = ((System.Drawing.Image)(resources.GetObject("TB_username.Icon")));
            this.TB_username.Location = new System.Drawing.Point(4, 24);
            this.TB_username.Margin = new System.Windows.Forms.Padding(4);
            this.TB_username.Name = "TB_username";
            this.TB_username.Size = new System.Drawing.Size(250, 45);
            this.TB_username.TabIndex = 5;
            this.TB_username.text = "";
            this.TB_username.OnTextChange += new System.EventHandler(this.TB_username_OnTextChange);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Roboto", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(3, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "Mật khẩu:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Roboto", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(3, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 19);
            this.label6.TabIndex = 12;
            this.label6.Text = "Quên mật khẩu?";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // BT_Login
            // 
            this.BT_Login.AutoSize = true;
            this.BT_Login.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BT_Login.Depth = 0;
            this.BT_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_Login.Icon = null;
            this.BT_Login.Location = new System.Drawing.Point(3, 178);
            this.BT_Login.MouseState = MaterialSkin.MouseState.HOVER;
            this.BT_Login.Name = "BT_Login";
            this.BT_Login.Primary = true;
            this.BT_Login.Size = new System.Drawing.Size(99, 36);
            this.BT_Login.TabIndex = 10;
            this.BT_Login.Text = "Đăng nhập";
            this.BT_Login.UseVisualStyleBackColor = true;
            this.BT_Login.Click += new System.EventHandler(this.BT_Login_Click);
            // 
            // LB_connectDB
            // 
            this.LB_connectDB.AutoSize = true;
            this.LB_connectDB.Font = new System.Drawing.Font("Roboto", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_connectDB.ForeColor = System.Drawing.Color.DimGray;
            this.LB_connectDB.Location = new System.Drawing.Point(525, 417);
            this.LB_connectDB.Name = "LB_connectDB";
            this.LB_connectDB.Size = new System.Drawing.Size(100, 19);
            this.LB_connectDB.TabIndex = 10;
            this.LB_connectDB.Text = "Kết nối CSDL";
            this.LB_connectDB.Click += new System.EventHandler(this.LB_connectDB_Click);
            // 
            // LB_trangThaiKetNoi
            // 
            this.LB_trangThaiKetNoi.AutoSize = true;
            this.LB_trangThaiKetNoi.Font = new System.Drawing.Font("Roboto", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_trangThaiKetNoi.ForeColor = System.Drawing.Color.DimGray;
            this.LB_trangThaiKetNoi.Location = new System.Drawing.Point(286, 417);
            this.LB_trangThaiKetNoi.Name = "LB_trangThaiKetNoi";
            this.LB_trangThaiKetNoi.Size = new System.Drawing.Size(218, 19);
            this.LB_trangThaiKetNoi.TabIndex = 11;
            this.LB_trangThaiKetNoi.Text = "Trạng thái: Chưa kết nối CSDL";
            // 
            // BT_Exit
            // 
            this.BT_Exit.Image = global::StudentManagementSystem.Properties.Resources.exit2_nonbackground;
            this.BT_Exit.Location = new System.Drawing.Point(615, 12);
            this.BT_Exit.Name = "BT_Exit";
            this.BT_Exit.Size = new System.Drawing.Size(23, 24);
            this.BT_Exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BT_Exit.TabIndex = 1;
            this.BT_Exit.TabStop = false;
            this.BT_Exit.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.Controls.Add(this.LB_trangThaiKetNoi);
            this.Controls.Add(this.LB_connectDB);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.BT_Exit);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.Text = "Login";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BT_Exit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox BT_Exit;
        private Bunifu.Framework.UI.BunifuTextbox TB_username;
        private BunifuTextbox TB_password;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MaterialSkin.Controls.MaterialRaisedButton BT_Login;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LB_connectDB;
        private System.Windows.Forms.Label LB_trangThaiKetNoi;
        private System.Windows.Forms.Label LB_LoginStatus;
    }
}