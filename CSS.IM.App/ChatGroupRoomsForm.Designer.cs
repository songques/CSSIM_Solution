namespace CSS.IM.App
{
    partial class ChatGroupRoomsForm
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
            this.groupBox1 = new CSS.IM.UI.Control.SPanle();
            this.lab_ask = new System.Windows.Forms.Label();
            this.lab_caption = new System.Windows.Forms.Label();
            this.groupBox2 = new CSS.IM.UI.Control.SPanle();
            this.list_rooms = new CSS.IM.UI.Control.ListViewEx.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_refresh = new CSS.IM.UI.Control.BasicButton();
            this.btn_crate = new CSS.IM.UI.Control.BasicButton();
            this.btn_collect = new CSS.IM.UI.Control.BasicButton();
            this.btn_add = new CSS.IM.UI.Control.BasicButton();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(452, 0);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_close.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_close.Location = new System.Drawing.Point(213, 358);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(69, 21);
            this.btn_close.TabIndex = 24;
            this.btn_close.Texts = "关闭";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lab_ask);
            this.groupBox1.Controls.Add(this.lab_caption);
            this.groupBox1.Location = new System.Drawing.Point(10, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 66);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // lab_ask
            // 
            this.lab_ask.AutoSize = true;
            this.lab_ask.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ask.Location = new System.Drawing.Point(15, 41);
            this.lab_ask.Name = "lab_ask";
            this.lab_ask.Size = new System.Drawing.Size(188, 17);
            this.lab_ask.TabIndex = 1;
            this.lab_ask.Text = "添加聊天室到收藏列表或直接加入";
            // 
            // lab_caption
            // 
            this.lab_caption.AutoSize = true;
            this.lab_caption.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_caption.Location = new System.Drawing.Point(15, 18);
            this.lab_caption.Name = "lab_caption";
            this.lab_caption.Size = new System.Drawing.Size(92, 17);
            this.lab_caption.TabIndex = 0;
            this.lab_caption.Text = "连接或书签房间";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.list_rooms);
            this.groupBox2.Controls.Add(this.btn_refresh);
            this.groupBox2.Controls.Add(this.btn_crate);
            this.groupBox2.Controls.Add(this.btn_collect);
            this.groupBox2.Controls.Add(this.btn_add);
            this.groupBox2.Location = new System.Drawing.Point(10, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 234);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // list_rooms
            // 
            this.list_rooms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.list_rooms.FullRowSelect = true;
            this.list_rooms.GridLines = true;
            this.list_rooms.Location = new System.Drawing.Point(6, 41);
            this.list_rooms.Name = "list_rooms";
            this.list_rooms.Size = new System.Drawing.Size(458, 192);
            this.list_rooms.TabIndex = 4;
            this.list_rooms.UseCompatibleStateImageBehavior = false;
            this.list_rooms.View = System.Windows.Forms.View.Details;
            this.list_rooms.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.list_rooms_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "书签";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "名称";
            this.columnHeader2.Width = 119;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "地址";
            this.columnHeader3.Width = 199;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "占有者";
            this.columnHeader4.Width = 69;
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackColor = System.Drawing.Color.Transparent;
            this.btn_refresh.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_refresh.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_refresh.Location = new System.Drawing.Point(365, 14);
            this.btn_refresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(69, 21);
            this.btn_refresh.TabIndex = 3;
            this.btn_refresh.Texts = "刷新";
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btn_crate
            // 
            this.btn_crate.BackColor = System.Drawing.Color.Transparent;
            this.btn_crate.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_crate.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_crate.Location = new System.Drawing.Point(196, 14);
            this.btn_crate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_crate.Name = "btn_crate";
            this.btn_crate.Size = new System.Drawing.Size(69, 21);
            this.btn_crate.TabIndex = 2;
            this.btn_crate.Texts = "创建";
            this.btn_crate.Click += new System.EventHandler(this.btn_crate_Click);
            // 
            // btn_collect
            // 
            this.btn_collect.BackColor = System.Drawing.Color.Transparent;
            this.btn_collect.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_collect.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_collect.Location = new System.Drawing.Point(145, 14);
            this.btn_collect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_collect.Name = "btn_collect";
            this.btn_collect.Size = new System.Drawing.Size(69, 21);
            this.btn_collect.TabIndex = 1;
            this.btn_collect.Texts = "书签";
            this.btn_collect.Visible = false;
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.Transparent;
            this.btn_add.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_add.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_add.Location = new System.Drawing.Point(35, 14);
            this.btn_add.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(69, 21);
            this.btn_add.TabIndex = 0;
            this.btn_add.Texts = "加入";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // ChatGroupRoomsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 385);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChatGroupRoomsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "浏览会议室-";
            this.Load += new System.EventHandler(this.ChatGroupRoomsForm_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btn_close, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Control.BasicButton btn_close;
        private CSS.IM.UI.Control.SPanle groupBox1;
        private System.Windows.Forms.Label lab_ask;
        private System.Windows.Forms.Label lab_caption;
        private CSS.IM.UI.Control.SPanle groupBox2;
        private UI.Control.BasicButton btn_refresh;
        private UI.Control.BasicButton btn_crate;
        private UI.Control.BasicButton btn_collect;
        private UI.Control.BasicButton btn_add;
        private CSS.IM.UI.Control.ListViewEx.ListViewEx list_rooms;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}