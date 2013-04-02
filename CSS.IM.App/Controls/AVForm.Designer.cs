namespace CSS.IM.App.Controls
{
    partial class AVForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AVForm));
            this.btn_agree = new CSS.IM.UI.Control.BasicButton();
            this.btn_refuse = new CSS.IM.UI.Control.BasicButton();
            this.btn_hangup = new CSS.IM.UI.Control.BasicButton();
            this.pic_remote = new System.Windows.Forms.PictureBox();
            this.pic_local = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.aVcommunicationEx1 = new CSS.IM.Library.AV.Controls.AVcommunicationEx(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_remote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_local)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(291, 0);
            // 
            // btn_agree
            // 
            this.btn_agree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_agree.BackColor = System.Drawing.Color.Transparent;
            this.btn_agree.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_agree.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_agree.Location = new System.Drawing.Point(22, 401);
            this.btn_agree.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_agree.Name = "btn_agree";
            this.btn_agree.Size = new System.Drawing.Size(75, 23);
            this.btn_agree.TabIndex = 24;
            this.btn_agree.Texts = "同意";
            this.btn_agree.Visible = false;
            this.btn_agree.Click += new System.EventHandler(this.btn_agree_Click);
            // 
            // btn_refuse
            // 
            this.btn_refuse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_refuse.BackColor = System.Drawing.Color.Transparent;
            this.btn_refuse.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_refuse.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_refuse.Location = new System.Drawing.Point(228, 401);
            this.btn_refuse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_refuse.Name = "btn_refuse";
            this.btn_refuse.Size = new System.Drawing.Size(75, 23);
            this.btn_refuse.TabIndex = 25;
            this.btn_refuse.Texts = "拒绝";
            this.btn_refuse.Visible = false;
            this.btn_refuse.Click += new System.EventHandler(this.btn_refuse_Click);
            // 
            // btn_hangup
            // 
            this.btn_hangup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_hangup.BackColor = System.Drawing.Color.Transparent;
            this.btn_hangup.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_hangup.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_hangup.Location = new System.Drawing.Point(125, 401);
            this.btn_hangup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_hangup.Name = "btn_hangup";
            this.btn_hangup.Size = new System.Drawing.Size(75, 23);
            this.btn_hangup.TabIndex = 26;
            this.btn_hangup.Texts = "挂断";
            this.btn_hangup.Click += new System.EventHandler(this.btn_hangup_Click);
            // 
            // pic_remote
            // 
            this.pic_remote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_remote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pic_remote.Image = ((System.Drawing.Image)(resources.GetObject("pic_remote.Image")));
            this.pic_remote.Location = new System.Drawing.Point(6, 36);
            this.pic_remote.Name = "pic_remote";
            this.pic_remote.Size = new System.Drawing.Size(320, 240);
            this.pic_remote.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_remote.TabIndex = 27;
            this.pic_remote.TabStop = false;
            // 
            // pic_local
            // 
            this.pic_local.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pic_local.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pic_local.Image = ((System.Drawing.Image)(resources.GetObject("pic_local.Image")));
            this.pic_local.Location = new System.Drawing.Point(187, 282);
            this.pic_local.Name = "pic_local";
            this.pic_local.Size = new System.Drawing.Size(138, 105);
            this.pic_local.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_local.TabIndex = 28;
            this.pic_local.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(82, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "音量";
            // 
            // trackBar2
            // 
            this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBar2.BackColor = System.Drawing.Color.White;
            this.trackBar2.Location = new System.Drawing.Point(21, 317);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(145, 42);
            this.trackBar2.TabIndex = 29;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(112, 130);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 42);
            this.trackBar1.TabIndex = 31;
            this.trackBar1.Visible = false;
            // 
            // aVcommunicationEx1
            // 
            this.aVcommunicationEx1.selfUDPPort = -1;
            this.aVcommunicationEx1.AudioData += new CSS.IM.Library.AV.AVcommunication.AVEventHandler(this.aVcommunicationEx1_AudioData);
            this.aVcommunicationEx1.VideoData += new CSS.IM.Library.AV.AVcommunication.AVEventHandler(this.aVcommunicationEx1_VideoData);
            // 
            // AVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 431);
            this.Controls.Add(this.pic_local);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.pic_remote);
            this.Controls.Add(this.btn_agree);
            this.Controls.Add(this.btn_refuse);
            this.Controls.Add(this.btn_hangup);
            this.Controls.Add(this.trackBar1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AVForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "与视频通话中";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AVForm_FormClosing);
            this.Controls.SetChildIndex(this.trackBar1, 0);
            this.Controls.SetChildIndex(this.btn_hangup, 0);
            this.Controls.SetChildIndex(this.btn_refuse, 0);
            this.Controls.SetChildIndex(this.btn_agree, 0);
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            this.Controls.SetChildIndex(this.pic_remote, 0);
            this.Controls.SetChildIndex(this.trackBar2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pic_local, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_remote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_local)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_remote;
        private System.Windows.Forms.PictureBox pic_local;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar1;
        public Library.AV.Controls.AVcommunicationEx aVcommunicationEx1;
        public CSS.IM.UI.Control.BasicButton btn_agree;
        public CSS.IM.UI.Control.BasicButton btn_refuse;
        public CSS.IM.UI.Control.BasicButton btn_hangup;
    }
}