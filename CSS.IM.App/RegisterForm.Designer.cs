namespace CSS.IM.App
{
    partial class RegisterForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_user = new CSS.IM.UI.Control.BasicTextBox();
            this.txt_pswd02 = new CSS.IM.UI.Control.BasicTextBox();
            this.txt_pswd01 = new CSS.IM.UI.Control.BasicTextBox();
            this.btn_regedit = new CSS.IM.UI.Control.BasicButton();
            this.btn_close = new CSS.IM.UI.Control.BasicButton();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(210, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(27, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(39, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "密码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(15, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "确认密码";
            // 
            // txt_user
            // 
            this.txt_user.BackColor = System.Drawing.Color.Transparent;
            this.txt_user.IsPass = false;
            this.txt_user.Location = new System.Drawing.Point(79, 46);
            this.txt_user.Multi = false;
            this.txt_user.Name = "txt_user";
            this.txt_user.ReadOn = false;
            this.txt_user.Size = new System.Drawing.Size(150, 23);
            this.txt_user.TabIndex = 0;
            this.txt_user.Texts = "";
            // 
            // txt_pswd02
            // 
            this.txt_pswd02.BackColor = System.Drawing.Color.Transparent;
            this.txt_pswd02.IsPass = true;
            this.txt_pswd02.Location = new System.Drawing.Point(79, 116);
            this.txt_pswd02.Multi = false;
            this.txt_pswd02.Name = "txt_pswd02";
            this.txt_pswd02.ReadOn = false;
            this.txt_pswd02.Size = new System.Drawing.Size(150, 23);
            this.txt_pswd02.TabIndex = 2;
            this.txt_pswd02.Texts = "";
            // 
            // txt_pswd01
            // 
            this.txt_pswd01.BackColor = System.Drawing.Color.Transparent;
            this.txt_pswd01.IsPass = true;
            this.txt_pswd01.Location = new System.Drawing.Point(79, 81);
            this.txt_pswd01.Multi = false;
            this.txt_pswd01.Name = "txt_pswd01";
            this.txt_pswd01.ReadOn = false;
            this.txt_pswd01.Size = new System.Drawing.Size(150, 23);
            this.txt_pswd01.TabIndex = 1;
            this.txt_pswd01.Texts = "";
            // 
            // btn_regedit
            // 
            this.btn_regedit.BackColor = System.Drawing.Color.Transparent;
            this.btn_regedit.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_regedit.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_regedit.Location = new System.Drawing.Point(41, 162);
            this.btn_regedit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_regedit.Name = "btn_regedit";
            this.btn_regedit.Size = new System.Drawing.Size(69, 21);
            this.btn_regedit.TabIndex = 3;
            this.btn_regedit.Texts = "注册";
            this.btn_regedit.Click += new System.EventHandler(this.btn_regedit_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_close.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_close.Location = new System.Drawing.Point(141, 162);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(69, 21);
            this.btn_close.TabIndex = 4;
            this.btn_close.Texts = "关闭";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 189);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_regedit);
            this.Controls.Add(this.txt_pswd01);
            this.Controls.Add(this.txt_pswd02);
            this.Controls.Add(this.txt_user);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注册用户";
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txt_user, 0);
            this.Controls.SetChildIndex(this.txt_pswd02, 0);
            this.Controls.SetChildIndex(this.txt_pswd01, 0);
            this.Controls.SetChildIndex(this.btn_regedit, 0);
            this.Controls.SetChildIndex(this.btn_close, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private UI.Control.BasicTextBox txt_user;
        private UI.Control.BasicTextBox txt_pswd02;
        private UI.Control.BasicTextBox txt_pswd01;
        private UI.Control.BasicButton btn_regedit;
        private UI.Control.BasicButton btn_close;

    }
}