namespace WForm1
{
    partial class SendFileForm
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.fileTansfersContainer1 = new CSS.IM.UI.Control.Graphics.FileTransfersControl.FileTansfersContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_open = new System.Windows.Forms.Button();
            this.btn_selectFile = new System.Windows.Forms.Button();
            this.txt_sendLocalPort = new System.Windows.Forms.TextBox();
            this.txt_tbRemoteIP = new System.Windows.Forms.TextBox();
            this.txt_tbRemotePort = new System.Windows.Forms.TextBox();
            this.txt_receiveLocalPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fileTansfersContainer2 = new CSS.IM.UI.Control.Graphics.FileTransfersControl.FileTansfersContainer();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(207, 304);
            this.listBox1.TabIndex = 4;
            // 
            // fileTansfersContainer1
            // 
            this.fileTansfersContainer1.AutoScroll = true;
            this.fileTansfersContainer1.Location = new System.Drawing.Point(210, 146);
            this.fileTansfersContainer1.Name = "fileTansfersContainer1";
            this.fileTansfersContainer1.Size = new System.Drawing.Size(217, 81);
            this.fileTansfersContainer1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 343);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "本机发送端口";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 401);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "远程端口";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 372);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "远程IP";
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(15, 427);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(75, 23);
            this.btn_open.TabIndex = 9;
            this.btn_open.Text = "开启";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // btn_selectFile
            // 
            this.btn_selectFile.Location = new System.Drawing.Point(99, 427);
            this.btn_selectFile.Name = "btn_selectFile";
            this.btn_selectFile.Size = new System.Drawing.Size(75, 23);
            this.btn_selectFile.TabIndex = 10;
            this.btn_selectFile.Text = "发送文件";
            this.btn_selectFile.UseVisualStyleBackColor = true;
            this.btn_selectFile.Click += new System.EventHandler(this.btn_selectFile_Click);
            // 
            // txt_sendLocalPort
            // 
            this.txt_sendLocalPort.Location = new System.Drawing.Point(89, 339);
            this.txt_sendLocalPort.Name = "txt_sendLocalPort";
            this.txt_sendLocalPort.Size = new System.Drawing.Size(100, 21);
            this.txt_sendLocalPort.TabIndex = 11;
            this.txt_sendLocalPort.Text = "10002";
            // 
            // txt_tbRemoteIP
            // 
            this.txt_tbRemoteIP.Location = new System.Drawing.Point(89, 368);
            this.txt_tbRemoteIP.Name = "txt_tbRemoteIP";
            this.txt_tbRemoteIP.Size = new System.Drawing.Size(100, 21);
            this.txt_tbRemoteIP.TabIndex = 12;
            this.txt_tbRemoteIP.Text = "192.168.0.22";
            // 
            // txt_tbRemotePort
            // 
            this.txt_tbRemotePort.Location = new System.Drawing.Point(89, 397);
            this.txt_tbRemotePort.Name = "txt_tbRemotePort";
            this.txt_tbRemotePort.Size = new System.Drawing.Size(100, 21);
            this.txt_tbRemotePort.TabIndex = 13;
            this.txt_tbRemotePort.Text = "10003";
            // 
            // txt_receiveLocalPort
            // 
            this.txt_receiveLocalPort.Location = new System.Drawing.Point(89, 310);
            this.txt_receiveLocalPort.Name = "txt_receiveLocalPort";
            this.txt_receiveLocalPort.Size = new System.Drawing.Size(100, 21);
            this.txt_receiveLocalPort.TabIndex = 15;
            this.txt_receiveLocalPort.Text = "10003";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 314);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "本机发送接收";
            // 
            // fileTansfersContainer2
            // 
            this.fileTansfersContainer2.AutoScroll = true;
            this.fileTansfersContainer2.Location = new System.Drawing.Point(430, 0);
            this.fileTansfersContainer2.Name = "fileTansfersContainer2";
            this.fileTansfersContainer2.Size = new System.Drawing.Size(209, 465);
            this.fileTansfersContainer2.TabIndex = 16;
            // 
            // SendFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 466);
            this.Controls.Add(this.fileTansfersContainer2);
            this.Controls.Add(this.txt_receiveLocalPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_tbRemotePort);
            this.Controls.Add(this.txt_tbRemoteIP);
            this.Controls.Add(this.txt_sendLocalPort);
            this.Controls.Add(this.btn_selectFile);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileTansfersContainer1);
            this.Controls.Add(this.listBox1);
            this.Name = "SendFileForm";
            this.Text = "SendFileForm";
            this.Load += new System.EventHandler(this.SendFileForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private CSS.IM.UI.Control.Graphics.FileTransfersControl.FileTansfersContainer fileTansfersContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Button btn_selectFile;
        private System.Windows.Forms.TextBox txt_sendLocalPort;
        private System.Windows.Forms.TextBox txt_tbRemoteIP;
        private System.Windows.Forms.TextBox txt_tbRemotePort;
        private System.Windows.Forms.TextBox txt_receiveLocalPort;
        private System.Windows.Forms.Label label4;
        private CSS.IM.UI.Control.Graphics.FileTransfersControl.FileTansfersContainer fileTansfersContainer2;
    }
}