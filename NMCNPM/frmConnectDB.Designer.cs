
namespace StudentManagementSystem
{
    partial class frmConnectDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnectDB));
            this.label4 = new System.Windows.Forms.Label();
            this.BT_Close = new MaterialSkin.Controls.MaterialRaisedButton();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BT_Connect = new MaterialSkin.Controls.MaterialRaisedButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.BT_xacThuc = new MaterialSkin.Controls.MaterialRaisedButton();
            this.label2 = new System.Windows.Forms.Label();
            this.LB_trangThaiXacThuc = new System.Windows.Forms.Label();
            this.BT_Close1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.TB_DBName = new Bunifu.Framework.UI.BunifuTextbox();
            this.TB_severName = new Bunifu.Framework.UI.BunifuTextbox();
            this.TB_pass = new Bunifu.Framework.UI.BunifuTextbox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LB_trangThaiDB = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LB_trangThaiKetNoi = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Roboto", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tên Server:";
            // 
            // BT_Close
            // 
            this.BT_Close.AutoSize = true;
            this.BT_Close.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BT_Close.Depth = 0;
            this.BT_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_Close.Icon = null;
            this.BT_Close.Location = new System.Drawing.Point(127, 258);
            this.BT_Close.MouseState = MaterialSkin.MouseState.HOVER;
            this.BT_Close.Name = "BT_Close";
            this.BT_Close.Primary = true;
            this.BT_Close.Size = new System.Drawing.Size(59, 36);
            this.BT_Close.TabIndex = 10;
            this.BT_Close.Text = "Đóng";
            this.BT_Close.UseVisualStyleBackColor = true;
            this.BT_Close.Click += new System.EventHandler(this.BT_Close_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Roboto", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(3, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Tên CSDL:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TB_DBName, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.TB_severName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(30, 73);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(268, 163);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // BT_Connect
            // 
            this.BT_Connect.AutoSize = true;
            this.BT_Connect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BT_Connect.Depth = 0;
            this.BT_Connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_Connect.Icon = null;
            this.BT_Connect.Location = new System.Drawing.Point(33, 258);
            this.BT_Connect.MouseState = MaterialSkin.MouseState.HOVER;
            this.BT_Connect.Name = "BT_Connect";
            this.BT_Connect.Primary = true;
            this.BT_Connect.Size = new System.Drawing.Size(72, 36);
            this.BT_Connect.TabIndex = 13;
            this.BT_Connect.Text = "Kết nối      ";
            this.BT_Connect.UseVisualStyleBackColor = true;
            this.BT_Connect.Click += new System.EventHandler(this.BT_Connect_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BT_Close1);
            this.panel1.Controls.Add(this.LB_trangThaiXacThuc);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.BT_xacThuc);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TB_pass);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(355, 363);
            this.panel1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(44, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Mật khẩu xác thực:";
            // 
            // BT_xacThuc
            // 
            this.BT_xacThuc.AutoSize = true;
            this.BT_xacThuc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BT_xacThuc.Depth = 0;
            this.BT_xacThuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_xacThuc.Icon = null;
            this.BT_xacThuc.Location = new System.Drawing.Point(46, 242);
            this.BT_xacThuc.MouseState = MaterialSkin.MouseState.HOVER;
            this.BT_xacThuc.Name = "BT_xacThuc";
            this.BT_xacThuc.Primary = true;
            this.BT_xacThuc.Size = new System.Drawing.Size(88, 36);
            this.BT_xacThuc.TabIndex = 14;
            this.BT_xacThuc.Text = "Xác thực";
            this.BT_xacThuc.UseVisualStyleBackColor = true;
            this.BT_xacThuc.Click += new System.EventHandler(this.BT_xacThuc_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(65, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 24);
            this.label2.TabIndex = 15;
            this.label2.Text = "KẾT NỐI CƠ SỞ DỮ LIỆU";
            // 
            // LB_trangThaiXacThuc
            // 
            this.LB_trangThaiXacThuc.AutoSize = true;
            this.LB_trangThaiXacThuc.Font = new System.Drawing.Font("Roboto", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_trangThaiXacThuc.ForeColor = System.Drawing.Color.Firebrick;
            this.LB_trangThaiXacThuc.Location = new System.Drawing.Point(44, 202);
            this.LB_trangThaiXacThuc.Name = "LB_trangThaiXacThuc";
            this.LB_trangThaiXacThuc.Size = new System.Drawing.Size(144, 20);
            this.LB_trangThaiXacThuc.TabIndex = 16;
            this.LB_trangThaiXacThuc.Text = "Hãy nhập mật khẩu";
            // 
            // BT_Close1
            // 
            this.BT_Close1.AutoSize = true;
            this.BT_Close1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BT_Close1.Depth = 0;
            this.BT_Close1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_Close1.Icon = null;
            this.BT_Close1.Location = new System.Drawing.Point(147, 242);
            this.BT_Close1.MouseState = MaterialSkin.MouseState.HOVER;
            this.BT_Close1.Name = "BT_Close1";
            this.BT_Close1.Primary = true;
            this.BT_Close1.Size = new System.Drawing.Size(59, 36);
            this.BT_Close1.TabIndex = 17;
            this.BT_Close1.Text = "Đóng";
            this.BT_Close1.UseVisualStyleBackColor = true;
            this.BT_Close1.Click += new System.EventHandler(this.BT_Close1_Click);
            // 
            // TB_DBName
            // 
            this.TB_DBName.BackColor = System.Drawing.SystemColors.Control;
            this.TB_DBName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TB_DBName.BackgroundImage")));
            this.TB_DBName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TB_DBName.ForeColor = System.Drawing.Color.Gray;
            this.TB_DBName.Icon = ((System.Drawing.Image)(resources.GetObject("TB_DBName.Icon")));
            this.TB_DBName.Location = new System.Drawing.Point(3, 103);
            this.TB_DBName.Name = "TB_DBName";
            this.TB_DBName.Size = new System.Drawing.Size(250, 45);
            this.TB_DBName.TabIndex = 6;
            this.TB_DBName.text = "";
            // 
            // TB_severName
            // 
            this.TB_severName.BackColor = System.Drawing.SystemColors.Control;
            this.TB_severName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TB_severName.BackgroundImage")));
            this.TB_severName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TB_severName.ForeColor = System.Drawing.Color.Gray;
            this.TB_severName.Icon = ((System.Drawing.Image)(resources.GetObject("TB_severName.Icon")));
            this.TB_severName.Location = new System.Drawing.Point(3, 23);
            this.TB_severName.Name = "TB_severName";
            this.TB_severName.Size = new System.Drawing.Size(250, 45);
            this.TB_severName.TabIndex = 5;
            this.TB_severName.text = "";
            // 
            // TB_pass
            // 
            this.TB_pass.BackColor = System.Drawing.SystemColors.Control;
            this.TB_pass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TB_pass.BackgroundImage")));
            this.TB_pass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TB_pass.ForeColor = System.Drawing.Color.Gray;
            this.TB_pass.Icon = ((System.Drawing.Image)(resources.GetObject("TB_pass.Icon")));
            this.TB_pass.Location = new System.Drawing.Point(48, 145);
            this.TB_pass.Name = "TB_pass";
            this.TB_pass.Size = new System.Drawing.Size(250, 45);
            this.TB_pass.TabIndex = 7;
            this.TB_pass.text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LB_trangThaiKetNoi);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.LB_trangThaiDB);
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Controls.Add(this.BT_Connect);
            this.panel2.Controls.Add(this.BT_Close);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(355, 363);
            this.panel2.TabIndex = 15;
            // 
            // LB_trangThaiDB
            // 
            this.LB_trangThaiDB.AutoSize = true;
            this.LB_trangThaiDB.Font = new System.Drawing.Font("Roboto", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_trangThaiDB.ForeColor = System.Drawing.Color.Firebrick;
            this.LB_trangThaiDB.Location = new System.Drawing.Point(33, 224);
            this.LB_trangThaiDB.Name = "LB_trangThaiDB";
            this.LB_trangThaiDB.Size = new System.Drawing.Size(77, 20);
            this.LB_trangThaiDB.TabIndex = 12;
            this.LB_trangThaiDB.Text = "Thông tin";
            this.LB_trangThaiDB.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Roboto", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(65, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(210, 24);
            this.label6.TabIndex = 16;
            this.label6.Text = "KẾT NỐI CƠ SỞ DỮ LIỆU";
            // 
            // LB_trangThaiKetNoi
            // 
            this.LB_trangThaiKetNoi.AutoSize = true;
            this.LB_trangThaiKetNoi.Font = new System.Drawing.Font("Roboto", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_trangThaiKetNoi.ForeColor = System.Drawing.Color.DimGray;
            this.LB_trangThaiKetNoi.Location = new System.Drawing.Point(29, 313);
            this.LB_trangThaiKetNoi.Name = "LB_trangThaiKetNoi";
            this.LB_trangThaiKetNoi.Size = new System.Drawing.Size(177, 20);
            this.LB_trangThaiKetNoi.TabIndex = 17;
            this.LB_trangThaiKetNoi.Text = "Trạng thái: Chưa kết nối";
            // 
            // frmConnectDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 363);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConnectDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmConnectDB";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private Bunifu.Framework.UI.BunifuTextbox TB_DBName;
        private Bunifu.Framework.UI.BunifuTextbox TB_severName;
        private MaterialSkin.Controls.MaterialRaisedButton BT_Close;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MaterialSkin.Controls.MaterialRaisedButton BT_Connect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private MaterialSkin.Controls.MaterialRaisedButton BT_xacThuc;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuTextbox TB_pass;
        private System.Windows.Forms.Label LB_trangThaiXacThuc;
        private MaterialSkin.Controls.MaterialRaisedButton BT_Close1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LB_trangThaiKetNoi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LB_trangThaiDB;
    }
}