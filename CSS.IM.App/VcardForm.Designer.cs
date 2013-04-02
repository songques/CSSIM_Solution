namespace CSS.IM.App
{
    partial class VcardForm
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
            this.pic_top = new System.Windows.Forms.PictureBox();
            this.lab_name = new System.Windows.Forms.Label();
            this.lab_nickName = new System.Windows.Forms.Label();
            this.lab_birthday = new System.Windows.Forms.Label();
            this.lab_desc = new System.Windows.Forms.Label();
            this.txt_name = new CSS.IM.UI.Control.QQtextBox();
            this.txt_nickname = new CSS.IM.UI.Control.QQtextBox();
            this.txt_birthday = new CSS.IM.UI.Control.QQtextBox();
            this.txt_desc = new CSS.IM.UI.Control.QQtextBox();
            this.txt_close = new CSS.IM.UI.Control.BasicButton();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_top)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(376, 0);
            // 
            // pic_top
            // 
            this.pic_top.BackColor = System.Drawing.Color.Transparent;
            this.pic_top.Location = new System.Drawing.Point(12, 40);
            this.pic_top.Name = "pic_top";
            this.pic_top.Size = new System.Drawing.Size(140, 127);
            this.pic_top.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_top.TabIndex = 24;
            this.pic_top.TabStop = false;
            // 
            // lab_name
            // 
            this.lab_name.AutoSize = true;
            this.lab_name.BackColor = System.Drawing.Color.Transparent;
            this.lab_name.Location = new System.Drawing.Point(156, 43);
            this.lab_name.Name = "lab_name";
            this.lab_name.Size = new System.Drawing.Size(29, 12);
            this.lab_name.TabIndex = 25;
            this.lab_name.Text = "姓名";
            // 
            // lab_nickName
            // 
            this.lab_nickName.AutoSize = true;
            this.lab_nickName.BackColor = System.Drawing.Color.Transparent;
            this.lab_nickName.Location = new System.Drawing.Point(156, 64);
            this.lab_nickName.Name = "lab_nickName";
            this.lab_nickName.Size = new System.Drawing.Size(29, 12);
            this.lab_nickName.TabIndex = 26;
            this.lab_nickName.Text = "昵称";
            // 
            // lab_birthday
            // 
            this.lab_birthday.AutoSize = true;
            this.lab_birthday.BackColor = System.Drawing.Color.Transparent;
            this.lab_birthday.Location = new System.Drawing.Point(156, 85);
            this.lab_birthday.Name = "lab_birthday";
            this.lab_birthday.Size = new System.Drawing.Size(29, 12);
            this.lab_birthday.TabIndex = 29;
            this.lab_birthday.Text = "生日";
            // 
            // lab_desc
            // 
            this.lab_desc.AutoSize = true;
            this.lab_desc.BackColor = System.Drawing.Color.Transparent;
            this.lab_desc.Location = new System.Drawing.Point(156, 105);
            this.lab_desc.Name = "lab_desc";
            this.lab_desc.Size = new System.Drawing.Size(29, 12);
            this.lab_desc.TabIndex = 31;
            this.lab_desc.Text = "描述";
            // 
            // txt_name
            // 
            this.txt_name.BackColor = System.Drawing.Color.White;
            this.txt_name.Icon = null;
            this.txt_name.Location = new System.Drawing.Point(188, 39);
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            this.txt_name.Size = new System.Drawing.Size(213, 21);
            this.txt_name.TabIndex = 37;
            // 
            // txt_nickname
            // 
            this.txt_nickname.BackColor = System.Drawing.Color.White;
            this.txt_nickname.Icon = null;
            this.txt_nickname.Location = new System.Drawing.Point(188, 60);
            this.txt_nickname.Name = "txt_nickname";
            this.txt_nickname.ReadOnly = true;
            this.txt_nickname.Size = new System.Drawing.Size(213, 21);
            this.txt_nickname.TabIndex = 38;
            // 
            // txt_birthday
            // 
            this.txt_birthday.BackColor = System.Drawing.Color.White;
            this.txt_birthday.Icon = null;
            this.txt_birthday.Location = new System.Drawing.Point(188, 81);
            this.txt_birthday.Name = "txt_birthday";
            this.txt_birthday.ReadOnly = true;
            this.txt_birthday.Size = new System.Drawing.Size(213, 21);
            this.txt_birthday.TabIndex = 39;
            // 
            // txt_desc
            // 
            this.txt_desc.BackColor = System.Drawing.Color.White;
            this.txt_desc.Icon = null;
            this.txt_desc.Location = new System.Drawing.Point(188, 102);
            this.txt_desc.Multiline = true;
            this.txt_desc.Name = "txt_desc";
            this.txt_desc.ReadOnly = true;
            this.txt_desc.Size = new System.Drawing.Size(213, 64);
            this.txt_desc.TabIndex = 40;
            // 
            // txt_close
            // 
            this.txt_close.BackColor = System.Drawing.Color.Transparent;
            this.txt_close.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txt_close.ForeColor = System.Drawing.Color.DarkBlue;
            this.txt_close.Location = new System.Drawing.Point(171, 180);
            this.txt_close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_close.Name = "txt_close";
            this.txt_close.Size = new System.Drawing.Size(69, 21);
            this.txt_close.TabIndex = 41;
            this.txt_close.Texts = "关闭";
            this.txt_close.Click += new System.EventHandler(this.txt_close_Click);
            // 
            // VcardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 208);
            this.Controls.Add(this.txt_close);
            this.Controls.Add(this.txt_desc);
            this.Controls.Add(this.txt_birthday);
            this.Controls.Add(this.txt_nickname);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.pic_top);
            this.Controls.Add(this.lab_name);
            this.Controls.Add(this.lab_birthday);
            this.Controls.Add(this.lab_desc);
            this.Controls.Add(this.lab_nickName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VcardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "名片";
            this.Load += new System.EventHandler(this.VcardForm_Load);
            this.Controls.SetChildIndex(this.lab_nickName, 0);
            this.Controls.SetChildIndex(this.lab_desc, 0);
            this.Controls.SetChildIndex(this.lab_birthday, 0);
            this.Controls.SetChildIndex(this.lab_name, 0);
            this.Controls.SetChildIndex(this.pic_top, 0);
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            this.Controls.SetChildIndex(this.txt_name, 0);
            this.Controls.SetChildIndex(this.txt_nickname, 0);
            this.Controls.SetChildIndex(this.txt_birthday, 0);
            this.Controls.SetChildIndex(this.txt_desc, 0);
            this.Controls.SetChildIndex(this.txt_close, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_top)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_top;
        private System.Windows.Forms.Label lab_name;
        private System.Windows.Forms.Label lab_nickName;
        private System.Windows.Forms.Label lab_birthday;
        private System.Windows.Forms.Label lab_desc;
        private UI.Control.QQtextBox txt_name;
        private UI.Control.QQtextBox txt_nickname;
        private UI.Control.QQtextBox txt_birthday;
        private UI.Control.QQtextBox txt_desc;
        private UI.Control.BasicButton txt_close;
    }
}