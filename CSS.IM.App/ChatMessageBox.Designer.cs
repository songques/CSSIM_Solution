namespace CSS.IM.App
{
    partial class ChatMessageBox
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
            this.components = new System.ComponentModel.Container();
            this.panel_userList = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(192, 0);
            // 
            // panel_userList
            // 
            this.panel_userList.AutoScroll = true;
            this.panel_userList.BackColor = System.Drawing.Color.Transparent;
            this.panel_userList.Location = new System.Drawing.Point(10, 38);
            this.panel_userList.Name = "panel_userList";
            this.panel_userList.Size = new System.Drawing.Size(210, 80);
            this.panel_userList.TabIndex = 24;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ChatMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 125);
            this.ControlBox = false;
            this.Controls.Add(this.panel_userList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChatMessageBox";
            this.ShowInTaskbar = false;
            this.Text = "消息盒子";
            this.MouseEnter += new System.EventHandler(this.ChatMessageBox_MouseEnter);
            this.Controls.SetChildIndex(this.panel_userList, 0);
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_userList;
        private System.Windows.Forms.Timer timer1;
    }
}