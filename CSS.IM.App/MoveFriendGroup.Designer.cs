namespace CSS.IM.App
{
    partial class MoveFriendGroup
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_move = new CSS.IM.UI.Control.BasicButton();
            this.btn_close = new CSS.IM.UI.Control.BasicButton();
            this.basicComboBox1 = new CSS.IM.UI.Control.BasicComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(224, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(19, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "移动组";
            // 
            // btn_move
            // 
            this.btn_move.BackColor = System.Drawing.Color.Transparent;
            this.btn_move.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_move.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_move.Location = new System.Drawing.Point(35, 97);
            this.btn_move.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_move.Name = "btn_move";
            this.btn_move.Size = new System.Drawing.Size(69, 21);
            this.btn_move.TabIndex = 25;
            this.btn_move.Texts = "确定";
            this.btn_move.Click += new System.EventHandler(this.btn_move_Click);
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
            this.btn_close.TabIndex = 26;
            this.btn_close.Texts = "取消";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // basicComboBox1
            // 
            this.basicComboBox1.BackColor = System.Drawing.Color.White;
            this.basicComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.basicComboBox1.Items = null;
            this.basicComboBox1.Location = new System.Drawing.Point(66, 50);
            this.basicComboBox1.Name = "basicComboBox1";
            this.basicComboBox1.SelectIndex = 0;
            this.basicComboBox1.SelectItem = null;
            this.basicComboBox1.SelectText = null;
            this.basicComboBox1.Size = new System.Drawing.Size(171, 22);
            this.basicComboBox1.TabIndex = 27;
            this.basicComboBox1.Texts = null;
            // 
            // MoveFriendGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 126);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_move);
            this.Controls.Add(this.basicComboBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveFriendGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "移动好友组";
            this.Load += new System.EventHandler(this.MoveFriendGroup_Load);
            this.Controls.SetChildIndex(this.basicComboBox1, 0);
            this.Controls.SetChildIndex(this.btn_move, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btn_close, 0);
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private UI.Control.BasicButton btn_move;
        private UI.Control.BasicButton btn_close;
        public UI.Control.BasicComboBox basicComboBox1;
    }
}