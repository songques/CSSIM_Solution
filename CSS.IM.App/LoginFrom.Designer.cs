namespace CSS.IM.App
{
    partial class LoginFrom
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
            //if (QQtsm_ServerAddress != null)
            //{
            //    QQtsm_ServerAddress.Dispose();
            //    QQtsm_ServerAddress = null;
            //}

            //if (QQtsm_region != null)
            //{
            //    QQtsm_region.Dispose();
            //    QQtsm_region = null;
            //}

            if (QQcm_menu != null)
            {
                QQcm_menu.Dispose();
                QQcm_menu = null;
            }

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginFrom));
            this.btn_login = new CSS.IM.UI.Control.BasicButton();
            this.btn_setings = new CSS.IM.UI.Control.BasicButton();
            this.txt_name = new CSS.IM.UI.Control.BasicTextBox();
            this.txt_pswd = new CSS.IM.UI.Control.BasicTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chb_autu = new CSS.IM.UI.Control.BasicCheckBox();
            this.chb_save = new CSS.IM.UI.Control.BasicCheckBox();
            this.lab_isAutoLogin = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer_keyLogin = new System.Windows.Forms.Timer(this.components);
            this.QQcm_menu = new CSS.IM.UI.Control.QQContextMenu();
            this.QQtsm_ServerAddress = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.QQtsm_region = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.ax_bs = new Axbsioav2Lib.Axbsioav2();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.QQcm_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ax_bs)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(261, 0);
            this.ButtonClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonClose_MouseClick);
            // 
            // btn_login
            // 
            this.btn_login.BackColor = System.Drawing.Color.Transparent;
            this.btn_login.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_login.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_login.Location = new System.Drawing.Point(172, 224);
            this.btn_login.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_login.Name = "btn_login";
            this.btn_login.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_login.Size = new System.Drawing.Size(69, 21);
            this.btn_login.TabIndex = 4;
            this.btn_login.Texts = "登录";
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // btn_setings
            // 
            this.btn_setings.BackColor = System.Drawing.Color.Transparent;
            this.btn_setings.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_setings.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_setings.Location = new System.Drawing.Point(52, 224);
            this.btn_setings.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_setings.Name = "btn_setings";
            this.btn_setings.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_setings.Size = new System.Drawing.Size(69, 21);
            this.btn_setings.TabIndex = 5;
            this.btn_setings.Texts = "设置";
            this.btn_setings.Click += new System.EventHandler(this.btn_setings_Click);
            // 
            // txt_name
            // 
            this.txt_name.BackColor = System.Drawing.Color.Transparent;
            this.txt_name.IsFocused = false;
            this.txt_name.IsPass = false;
            this.txt_name.Location = new System.Drawing.Point(91, 124);
            this.txt_name.MaxLength = 20;
            this.txt_name.Multi = false;
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOn = false;
            this.txt_name.Size = new System.Drawing.Size(150, 23);
            this.txt_name.TabIndex = 0;
            this.txt_name.Texts = "";
            // 
            // txt_pswd
            // 
            this.txt_pswd.BackColor = System.Drawing.Color.Transparent;
            this.txt_pswd.IsFocused = false;
            this.txt_pswd.IsPass = true;
            this.txt_pswd.Location = new System.Drawing.Point(91, 163);
            this.txt_pswd.MaxLength = 20;
            this.txt_pswd.Multi = false;
            this.txt_pswd.Name = "txt_pswd";
            this.txt_pswd.ReadOn = false;
            this.txt_pswd.Size = new System.Drawing.Size(150, 23);
            this.txt_pswd.TabIndex = 1;
            this.txt_pswd.Texts = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(48, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(50, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "密  码";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(292, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // chb_autu
            // 
            this.chb_autu.BackColor = System.Drawing.Color.Transparent;
            this.chb_autu.Checked = false;
            this.chb_autu.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_autu.Location = new System.Drawing.Point(52, 195);
            this.chb_autu.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_autu.Name = "chb_autu";
            this.chb_autu.Size = new System.Drawing.Size(95, 20);
            this.chb_autu.TabIndex = 2;
            this.chb_autu.Texts = "自动登录";
            this.chb_autu.CheckedChanged += new CSS.IM.UI.Control.BasicCheckBox.CheckedChangedEventHandler(this.chb_autu_CheckedChanged);
            // 
            // chb_save
            // 
            this.chb_save.BackColor = System.Drawing.Color.Transparent;
            this.chb_save.Checked = false;
            this.chb_save.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_save.Location = new System.Drawing.Point(167, 195);
            this.chb_save.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_save.Name = "chb_save";
            this.chb_save.Size = new System.Drawing.Size(74, 20);
            this.chb_save.TabIndex = 3;
            this.chb_save.Texts = "记住密码";
            this.chb_save.CheckedChanged += new CSS.IM.UI.Control.BasicCheckBox.CheckedChangedEventHandler(this.chb_save_CheckedChanged);
            // 
            // lab_isAutoLogin
            // 
            this.lab_isAutoLogin.BackColor = System.Drawing.Color.Transparent;
            this.lab_isAutoLogin.ForeColor = System.Drawing.Color.Red;
            this.lab_isAutoLogin.Location = new System.Drawing.Point(49, 148);
            this.lab_isAutoLogin.Name = "lab_isAutoLogin";
            this.lab_isAutoLogin.Size = new System.Drawing.Size(210, 13);
            this.lab_isAutoLogin.TabIndex = 8;
            this.lab_isAutoLogin.Text = "30秒后开始重新登录";
            this.lab_isAutoLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lab_isAutoLogin.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer_keyLogin
            // 
            this.timer_keyLogin.Interval = 200;
            this.timer_keyLogin.Tick += new System.EventHandler(this.timer_keyLogin_Tick);
            // 
            // QQcm_menu
            // 
            this.QQcm_menu.BackColor = System.Drawing.Color.White;
            this.QQcm_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.QQtsm_ServerAddress,
            this.QQtsm_region});
            this.QQcm_menu.Name = "QQcm_menu";
            this.QQcm_menu.Size = new System.Drawing.Size(135, 48);
            // 
            // QQtsm_ServerAddress
            // 
            this.QQtsm_ServerAddress.Name = "QQtsm_ServerAddress";
            this.QQtsm_ServerAddress.Size = new System.Drawing.Size(134, 22);
            this.QQtsm_ServerAddress.Text = "服务器地址";
            this.QQtsm_ServerAddress.Click += new System.EventHandler(this.QQtsm_ServerAddress_Click);
            // 
            // QQtsm_region
            // 
            this.QQtsm_region.Name = "QQtsm_region";
            this.QQtsm_region.Size = new System.Drawing.Size(134, 22);
            this.QQtsm_region.Text = "注册用户";
            this.QQtsm_region.Click += new System.EventHandler(this.QQtsm_region_Click);
            // 
            // ax_bs
            // 
            this.ax_bs.Enabled = true;
            this.ax_bs.Location = new System.Drawing.Point(10, 40);
            this.ax_bs.Name = "ax_bs";
            this.ax_bs.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ax_bs.OcxState")));
            this.ax_bs.Size = new System.Drawing.Size(37, 28);
            this.ax_bs.TabIndex = 31;
            // 
            // LoginFrom
            // 
            this.AllowResize = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 253);
            this.Controls.Add(this.lab_isAutoLogin);
            this.Controls.Add(this.chb_save);
            this.Controls.Add(this.chb_autu);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txt_pswd);
            this.Controls.Add(this.btn_setings);
            this.Controls.Add(this.ax_bs);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSS&IM";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginFrom_FormClosed);
            this.Load += new System.EventHandler(this.LoginFrom_Load);
            this.Controls.SetChildIndex(this.ax_bs, 0);
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            this.Controls.SetChildIndex(this.btn_setings, 0);
            this.Controls.SetChildIndex(this.txt_pswd, 0);
            this.Controls.SetChildIndex(this.btn_login, 0);
            this.Controls.SetChildIndex(this.txt_name, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.chb_autu, 0);
            this.Controls.SetChildIndex(this.chb_save, 0);
            this.Controls.SetChildIndex(this.lab_isAutoLogin, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.QQcm_menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ax_bs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Control.BasicButton btn_login;
        private UI.Control.BasicButton btn_setings;
        private UI.Control.BasicTextBox txt_name;
        private UI.Control.BasicTextBox txt_pswd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private UI.Control.BasicCheckBox chb_autu;
        private UI.Control.BasicCheckBox chb_save;
        private System.Windows.Forms.Label lab_isAutoLogin;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer_keyLogin;
        private UI.Control.QQContextMenu QQcm_menu;
        private UI.Control.QQToolStripMenuItem QQtsm_ServerAddress;
        private UI.Control.QQToolStripMenuItem QQtsm_region;
        private Axbsioav2Lib.Axbsioav2 ax_bs;
    }
}