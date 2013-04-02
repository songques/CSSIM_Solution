namespace CSS.IM.App
{
    partial class RemarkFriendGroup
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
            this.btn_close = new CSS.IM.UI.Control.BasicButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_save = new CSS.IM.UI.Control.BasicButton();
            this.txt_remark = new CSS.IM.UI.Control.BasicTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(224, 0);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_close.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_close.Location = new System.Drawing.Point(164, 97);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(69, 21);
            this.btn_close.TabIndex = 30;
            this.btn_close.Texts = "取消";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(19, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 28;
            this.label1.Text = "备注";
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Transparent;
            this.btn_save.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_save.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_save.Location = new System.Drawing.Point(35, 97);
            this.btn_save.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(69, 21);
            this.btn_save.TabIndex = 29;
            this.btn_save.Texts = "确定";
            // 
            // txt_remark
            // 
            this.txt_remark.BackColor = System.Drawing.Color.Transparent;
            this.txt_remark.IsPass = false;
            this.txt_remark.Location = new System.Drawing.Point(66, 50);
            this.txt_remark.MaxLength = 32767;
            this.txt_remark.Multi = false;
            this.txt_remark.Name = "txt_remark";
            this.txt_remark.ReadOn = false;
            this.txt_remark.Size = new System.Drawing.Size(171, 23);
            this.txt_remark.TabIndex = 32;
            this.txt_remark.Texts = "";
            // 
            // RemarkFriendGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 126);
            this.Controls.Add(this.txt_remark);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_save);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RemarkFriendGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "好友备注";
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            this.Controls.SetChildIndex(this.btn_save, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btn_close, 0);
            this.Controls.SetChildIndex(this.txt_remark, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Control.BasicButton btn_close;
        private System.Windows.Forms.Label label1;
        private UI.Control.BasicButton btn_save;
        private UI.Control.BasicTextBox txt_remark;
    }
}