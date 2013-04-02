namespace CSS.IM.App
{
    partial class FindFriendForm
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
            this.cmb_findtype = new CSS.IM.UI.Control.BasicComboBox();
            this.txt_userName = new CSS.IM.UI.Control.BasicTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listView1 = new CSS.IM.UI.Control.ListViewEx.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_find = new CSS.IM.UI.Control.BasicButton();
            this.btn_close = new CSS.IM.UI.Control.BasicButton();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(404, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(29, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "查找类型";
            // 
            // cmb_findtype
            // 
            this.cmb_findtype.BackColor = System.Drawing.Color.White;
            this.cmb_findtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_findtype.Items = new string[] {
        "全部",
        "名称"};
            this.cmb_findtype.Location = new System.Drawing.Point(88, 47);
            this.cmb_findtype.Name = "cmb_findtype";
            this.cmb_findtype.SelectIndex = 0;
            this.cmb_findtype.SelectItem = null;
            this.cmb_findtype.SelectText = null;
            this.cmb_findtype.Size = new System.Drawing.Size(134, 22);
            this.cmb_findtype.TabIndex = 6;
            this.cmb_findtype.Texts = null;
            // 
            // txt_userName
            // 
            this.txt_userName.BackColor = System.Drawing.Color.Transparent;
            this.txt_userName.IsPass = false;
            this.txt_userName.Location = new System.Drawing.Point(263, 46);
            this.txt_userName.Multi = false;
            this.txt_userName.Name = "txt_userName";
            this.txt_userName.ReadOn = false;
            this.txt_userName.Size = new System.Drawing.Size(150, 23);
            this.txt_userName.TabIndex = 5;
            this.txt_userName.Texts = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(232, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "条件";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(8, 76);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(425, 376);
            this.listView1.TabIndex = 24;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "JID";
            this.columnHeader1.Width = 141;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "用户名";
            this.columnHeader2.Width = 74;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "姓名";
            this.columnHeader3.Width = 81;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "邮件";
            this.columnHeader4.Width = 123;
            // 
            // btn_find
            // 
            this.btn_find.BackColor = System.Drawing.Color.Transparent;
            this.btn_find.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_find.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_find.Location = new System.Drawing.Point(80, 464);
            this.btn_find.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_find.Name = "btn_find";
            this.btn_find.Size = new System.Drawing.Size(69, 21);
            this.btn_find.TabIndex = 25;
            this.btn_find.Texts = "查找";
            this.btn_find.Click += new System.EventHandler(this.btn_find_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_close.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_close.Location = new System.Drawing.Point(287, 464);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(69, 21);
            this.btn_close.TabIndex = 26;
            this.btn_close.Texts = "关闭";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // FindFriendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 492);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_find);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.cmb_findtype);
            this.Controls.Add(this.txt_userName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindFriendForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找好友";
            this.Load += new System.EventHandler(this.FindFriendForm_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            this.Controls.SetChildIndex(this.txt_userName, 0);
            this.Controls.SetChildIndex(this.cmb_findtype, 0);
            this.Controls.SetChildIndex(this.listView1, 0);
            this.Controls.SetChildIndex(this.btn_find, 0);
            this.Controls.SetChildIndex(this.btn_close, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private UI.Control.BasicTextBox txt_userName;
        private UI.Control.BasicComboBox cmb_findtype;
        private CSS.IM.UI.Control.ListViewEx.ListViewEx listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private UI.Control.BasicButton btn_find;
        private UI.Control.BasicButton btn_close;
    }
}