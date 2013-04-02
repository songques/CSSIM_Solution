namespace CSS.IM.App
{
    partial class Red5Msg
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
            this.web_red5 = new System.Windows.Forms.WebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(590, 0);
            // 
            // web_red5
            // 
            this.web_red5.AllowWebBrowserDrop = false;
            this.web_red5.IsWebBrowserContextMenuEnabled = false;
            this.web_red5.Location = new System.Drawing.Point(-14, 32);
            this.web_red5.MinimumSize = new System.Drawing.Size(20, 20);
            this.web_red5.Name = "web_red5";
            this.web_red5.ScrollBarsEnabled = false;
            this.web_red5.Size = new System.Drawing.Size(640, 480);
            this.web_red5.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 800);
            this.label1.TabIndex = 25;
            // 
            // Red5Msg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 515);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.web_red5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Red5Msg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视频聊天";
            this.Controls.SetChildIndex(this.web_red5, 0);
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser web_red5;
        private System.Windows.Forms.Label label1;
    }
}