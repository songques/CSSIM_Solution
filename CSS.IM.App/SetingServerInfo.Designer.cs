namespace CSS.IM.App
{
    partial class SetingServerInfo
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
            this.btn_save = new CSS.IM.UI.Control.BasicButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_ServerIP = new CSS.IM.UI.Control.BasicTextBox();
            this.txt_ServerPort = new CSS.IM.UI.Control.BasicTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Transparent;
            this.btn_save.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_save.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_save.Location = new System.Drawing.Point(80, 122);
            this.btn_save.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(69, 21);
            this.btn_save.TabIndex = 2;
            this.btn_save.Texts = "保存";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(16, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(16, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "端口";
            // 
            // txt_ServerIP
            // 
            this.txt_ServerIP.BackColor = System.Drawing.Color.Transparent;
            this.txt_ServerIP.IsPass = false;
            this.txt_ServerIP.Location = new System.Drawing.Point(50, 43);
            this.txt_ServerIP.Multi = false;
            this.txt_ServerIP.Name = "txt_ServerIP";
            this.txt_ServerIP.ReadOn = false;
            this.txt_ServerIP.Size = new System.Drawing.Size(173, 23);
            this.txt_ServerIP.TabIndex = 0;
            this.txt_ServerIP.Texts = "";
            // 
            // txt_ServerPort
            // 
            this.txt_ServerPort.BackColor = System.Drawing.Color.Transparent;
            this.txt_ServerPort.IsPass = false;
            this.txt_ServerPort.Location = new System.Drawing.Point(50, 80);
            this.txt_ServerPort.Multi = false;
            this.txt_ServerPort.Name = "txt_ServerPort";
            this.txt_ServerPort.ReadOn = false;
            this.txt_ServerPort.Size = new System.Drawing.Size(173, 23);
            this.txt_ServerPort.TabIndex = 1;
            this.txt_ServerPort.Texts = "";
            // 
            // SetingServerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 150);
            this.Controls.Add(this.txt_ServerPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.txt_ServerIP);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetingServerInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录服务器";
            this.Load += new System.EventHandler(this.SetingServerInfo_Load);
            this.Controls.SetChildIndex(this.txt_ServerIP, 0);
            this.Controls.SetChildIndex(this.btn_save, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            this.Controls.SetChildIndex(this.txt_ServerPort, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Control.BasicButton btn_save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private UI.Control.BasicTextBox txt_ServerIP;
        private UI.Control.BasicTextBox txt_ServerPort;
    }
}