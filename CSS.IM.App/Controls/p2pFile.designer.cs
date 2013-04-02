using CSS.IM.Library.Controls;
namespace CSS.IM.App.Controls
{
    partial class p2pFile
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                this.p2pFileTransmit1.Dispose();
            }
            catch { }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(p2pFile));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labProcess = new System.Windows.Forms.Label();
            this.PBar1 = new CSS.IM.Library.Controls.XpProgressBar();
            this.labelState = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.linkLabelCancel = new System.Windows.Forms.LinkLabel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.labFileName = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.labOr = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.linkSaveAs = new System.Windows.Forms.LinkLabel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.picWait = new System.Windows.Forms.PictureBox();
            this.p2pFileTransmit1 = new CSS.IM.Library.Controls.p2pFileTransmit(this.components);
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWait)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(255, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(8, 50);
            this.panel3.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(8, 50);
            this.panel2.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.labProcess);
            this.panel4.Controls.Add(this.PBar1);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 24);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(263, 50);
            this.panel4.TabIndex = 8;
            // 
            // labProcess
            // 
            this.labProcess.BackColor = System.Drawing.Color.Transparent;
            this.labProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labProcess.ForeColor = System.Drawing.Color.DimGray;
            this.labProcess.Location = new System.Drawing.Point(8, 12);
            this.labProcess.Name = "labProcess";
            this.labProcess.Size = new System.Drawing.Size(247, 38);
            this.labProcess.TabIndex = 6;
            this.labProcess.Text = "(0/0)";
            this.labProcess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PBar1
            // 
            this.PBar1.ColorBackGround = System.Drawing.Color.White;
            this.PBar1.ColorBarBorder = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(240)))), ((int)(((byte)(170)))));
            this.PBar1.ColorBarCenter = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(10)))));
            this.PBar1.ColorText = System.Drawing.Color.Blue;
            this.PBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.PBar1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PBar1.Location = new System.Drawing.Point(8, 0);
            this.PBar1.Name = "PBar1";
            this.PBar1.Position = 0;
            this.PBar1.PositionMax = 100;
            this.PBar1.PositionMin = 0;
            this.PBar1.Size = new System.Drawing.Size(247, 12);
            this.PBar1.TabIndex = 5;
            // 
            // labelState
            // 
            this.labelState.BackColor = System.Drawing.Color.Transparent;
            this.labelState.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelState.ForeColor = System.Drawing.Color.Purple;
            this.labelState.Location = new System.Drawing.Point(0, 0);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(263, 24);
            this.labelState.TabIndex = 0;
            this.labelState.Text = "等待接收...";
            this.labelState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel9
            // 
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(8, 57);
            this.panel9.TabIndex = 28;
            // 
            // linkLabelCancel
            // 
            this.linkLabelCancel.AutoSize = true;
            this.linkLabelCancel.ForeColor = System.Drawing.Color.Purple;
            this.linkLabelCancel.LinkColor = System.Drawing.Color.DarkOliveGreen;
            this.linkLabelCancel.Location = new System.Drawing.Point(154, 25);
            this.linkLabelCancel.Name = "linkLabelCancel";
            this.linkLabelCancel.Size = new System.Drawing.Size(29, 12);
            this.linkLabelCancel.TabIndex = 38;
            this.linkLabelCancel.TabStop = true;
            this.linkLabelCancel.Text = "取消";
            this.linkLabelCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.linkLabelCancel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCancel_LinkClicked);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.labFileName);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 131);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(263, 71);
            this.panel6.TabIndex = 10;
            // 
            // labFileName
            // 
            this.labFileName.BackColor = System.Drawing.Color.Transparent;
            this.labFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labFileName.ForeColor = System.Drawing.Color.Purple;
            this.labFileName.Location = new System.Drawing.Point(10, 0);
            this.labFileName.Name = "labFileName";
            this.labFileName.Size = new System.Drawing.Size(253, 71);
            this.labFileName.TabIndex = 19;
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(10, 71);
            this.panel7.TabIndex = 16;
            // 
            // labOr
            // 
            this.labOr.AutoSize = true;
            this.labOr.ForeColor = System.Drawing.Color.Purple;
            this.labOr.Location = new System.Drawing.Point(137, 25);
            this.labOr.Name = "labOr";
            this.labOr.Size = new System.Drawing.Size(17, 12);
            this.labOr.TabIndex = 37;
            this.labOr.Text = "或";
            this.labOr.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.labelState);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 202);
            this.panel1.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.linkLabelCancel);
            this.panel5.Controls.Add(this.labOr);
            this.panel5.Controls.Add(this.linkSaveAs);
            this.panel5.Controls.Add(this.panel10);
            this.panel5.Controls.Add(this.picWait);
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 74);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(263, 57);
            this.panel5.TabIndex = 9;
            // 
            // linkSaveAs
            // 
            this.linkSaveAs.AutoSize = true;
            this.linkSaveAs.ForeColor = System.Drawing.Color.Purple;
            this.linkSaveAs.LinkColor = System.Drawing.Color.DarkOliveGreen;
            this.linkSaveAs.Location = new System.Drawing.Point(84, 25);
            this.linkSaveAs.Name = "linkSaveAs";
            this.linkSaveAs.Size = new System.Drawing.Size(53, 12);
            this.linkSaveAs.TabIndex = 36;
            this.linkSaveAs.TabStop = true;
            this.linkSaveAs.Text = "接收文件";
            this.linkSaveAs.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.linkSaveAs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSaveAs_LinkClicked);
            // 
            // panel10
            // 
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(8, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(255, 10);
            this.panel10.TabIndex = 30;
            // 
            // picWait
            // 
            this.picWait.ErrorImage = ((System.Drawing.Image)(resources.GetObject("picWait.ErrorImage")));
            this.picWait.Image = ((System.Drawing.Image)(resources.GetObject("picWait.Image")));
            this.picWait.InitialImage = ((System.Drawing.Image)(resources.GetObject("picWait.InitialImage")));
            this.picWait.Location = new System.Drawing.Point(47, 15);
            this.picWait.Name = "picWait";
            this.picWait.Size = new System.Drawing.Size(31, 36);
            this.picWait.TabIndex = 29;
            this.picWait.TabStop = false;
            // 
            // p2pFileTransmit1
            // 
            this.p2pFileTransmit1.maxReadWriteFileBlock = 10485760;
            this.p2pFileTransmit1.OutTime = ((byte)(0));
            this.p2pFileTransmit1.fileTransmitted += new CSS.IM.Library.Controls.p2pFileTransmit.fileTransmittedEventHandler(this.p2pFileTransmit1_fileTransmitted);
            this.p2pFileTransmit1.fileTransmitCancel += new CSS.IM.Library.Controls.p2pFileTransmit.fileTransmitCancelEventHandler(this.p2pFileTransmit1_fileTransmitCancel);
            this.p2pFileTransmit1.fileTransmitBefore += new CSS.IM.Library.Controls.p2pFileTransmit.fileTransmitBeforeEventHandler(this.p2pFileTransmit1_fileTransmitBefore);
            this.p2pFileTransmit1.fileTransmitOutTime += new CSS.IM.Library.Controls.p2pFileTransmit.fileTransmitOutTimeEventHandler(this.p2pFileTransmit1_fileTransmitOutTime);
            this.p2pFileTransmit1.fileTransmitting += new CSS.IM.Library.Controls.p2pFileTransmit.fileTransmittingEventHandler(this.p2pFileTransmit1_fileTransmitting);
            this.p2pFileTransmit1.getFileProxyID += new CSS.IM.Library.Controls.p2pFileTransmit.getFileProxyIDEventHandler(this.p2pFileTransmit1_getFileProxyID);
            this.p2pFileTransmit1.fileTransmitConnected += new CSS.IM.Library.Controls.p2pFileTransmit.ConnectedEventHandler(this.p2pFileTransmit1_fileTransmitConnected);
            this.p2pFileTransmit1.fileTransmitGetUDPPort += new CSS.IM.Library.Controls.p2pFileTransmit.GetUDPPortEventHandler(this.p2pFileTransmit1_fileTransmitGetUDPPort);
            // 
            // p2pFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Name = "p2pFile";
            this.Size = new System.Drawing.Size(263, 202);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private XpProgressBar PBar1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labProcess;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.LinkLabel linkLabelCancel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label labFileName;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label labOr;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.LinkLabel linkSaveAs;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.PictureBox picWait;
        public  p2pFileTransmit p2pFileTransmit1;
    }
}
