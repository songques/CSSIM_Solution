namespace CSS.IM.App
{
    partial class MessageBoxForm
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
            this.lllab = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(240, 0);
            // 
            // lllab
            // 
            this.lllab.BackColor = System.Drawing.Color.Transparent;
            this.lllab.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lllab.LinkColor = System.Drawing.Color.Black;
            this.lllab.Location = new System.Drawing.Point(11, 48);
            this.lllab.Name = "lllab";
            this.lllab.Size = new System.Drawing.Size(254, 132);
            this.lllab.TabIndex = 25;
            this.lllab.TabStop = true;
            this.lllab.Text = "lllab";
            this.lllab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lllab.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lllab_LinkClicked);
            // 
            // MessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 208);
            this.Controls.Add(this.lllab);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxForm";
            this.ShowInTaskbar = false;
            this.Text = "MessageBoxForm";
            this.Load += new System.EventHandler(this.MessageBoxForm_Load);
            this.Controls.SetChildIndex(this.lllab, 0);
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lllab;





    }
}