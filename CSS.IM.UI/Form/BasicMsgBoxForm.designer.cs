using CSS.IM.UI.Control;
namespace CSS.IM.UI.Form
{
    partial class BasicMsgBoxForm
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
            timer_update.Enabled = false;
            timer_update.Dispose();

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
            this.messageText = new System.Windows.Forms.Label();
            this.iconPic = new System.Windows.Forms.PictureBox();
            this.no = new CSS.IM.UI.Control.BasicButton();
            this.canel = new CSS.IM.UI.Control.BasicButton();
            this.ok = new CSS.IM.UI.Control.BasicButton();
            this.Retry = new CSS.IM.UI.Control.BasicButton();
            this.timer_update = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPic)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(301, 0);
            // 
            // messageText
            // 
            this.messageText.BackColor = System.Drawing.Color.Transparent;
            this.messageText.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.messageText.ForeColor = System.Drawing.Color.Black;
            this.messageText.Location = new System.Drawing.Point(59, 45);
            this.messageText.Name = "messageText";
            this.messageText.Size = new System.Drawing.Size(252, 48);
            this.messageText.TabIndex = 0;
            this.messageText.Text = " ";
            // 
            // iconPic
            // 
            this.iconPic.BackColor = System.Drawing.Color.Transparent;
            this.iconPic.Location = new System.Drawing.Point(13, 45);
            this.iconPic.Name = "iconPic";
            this.iconPic.Size = new System.Drawing.Size(40, 40);
            this.iconPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.iconPic.TabIndex = 2;
            this.iconPic.TabStop = false;
            // 
            // no
            // 
            this.no.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.no.BackColor = System.Drawing.Color.Transparent;
            this.no.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.no.ForeColor = System.Drawing.Color.DarkBlue;
            this.no.Location = new System.Drawing.Point(262, 123);
            this.no.Name = "no";
            this.no.Size = new System.Drawing.Size(69, 21);
            this.no.TabIndex = 5;
            this.no.Texts = "取消";
            this.no.Visible = false;
            this.no.MouseClick += new System.Windows.Forms.MouseEventHandler(this.no_MouseClick);
            // 
            // canel
            // 
            this.canel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.canel.BackColor = System.Drawing.Color.Transparent;
            this.canel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.canel.ForeColor = System.Drawing.Color.DarkBlue;
            this.canel.Location = new System.Drawing.Point(187, 123);
            this.canel.Name = "canel";
            this.canel.Size = new System.Drawing.Size(69, 21);
            this.canel.TabIndex = 4;
            this.canel.Texts = "取消";
            this.canel.Visible = false;
            this.canel.Click += new System.EventHandler(this.canel_Click);
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.BackColor = System.Drawing.Color.Transparent;
            this.ok.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ok.ForeColor = System.Drawing.Color.DarkBlue;
            this.ok.Location = new System.Drawing.Point(112, 123);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(69, 21);
            this.ok.TabIndex = 3;
            this.ok.Texts = "确定";
            this.ok.Click += new System.EventHandler(this.iButton1_Click);
            // 
            // Retry
            // 
            this.Retry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Retry.BackColor = System.Drawing.Color.Transparent;
            this.Retry.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Retry.ForeColor = System.Drawing.Color.DarkBlue;
            this.Retry.Location = new System.Drawing.Point(187, 123);
            this.Retry.Name = "Retry";
            this.Retry.Size = new System.Drawing.Size(69, 21);
            this.Retry.TabIndex = 24;
            this.Retry.Texts = "重新登录";
            this.Retry.Visible = false;
            this.Retry.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Retry_MouseClick);
            // 
            // timer_update
            // 
            this.timer_update.Tick += new System.EventHandler(this.timer_update_Tick);
            // 
            // BasicMsgBoxForm
            // 
            this.AllowResize = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 150);
            this.Controls.Add(this.Retry);
            this.Controls.Add(this.no);
            this.Controls.Add(this.canel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.iconPic);
            this.Controls.Add(this.messageText);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BasicMsgBoxForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " 提示";
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            this.Controls.SetChildIndex(this.messageText, 0);
            this.Controls.SetChildIndex(this.iconPic, 0);
            this.Controls.SetChildIndex(this.ok, 0);
            this.Controls.SetChildIndex(this.canel, 0);
            this.Controls.SetChildIndex(this.no, 0);
            this.Controls.SetChildIndex(this.Retry, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label messageText;
        private System.Windows.Forms.PictureBox iconPic;
        private BasicButton ok;
        private BasicButton canel;
        private BasicButton no;
        private BasicButton Retry;
        private System.Windows.Forms.Timer timer_update;
    }
}