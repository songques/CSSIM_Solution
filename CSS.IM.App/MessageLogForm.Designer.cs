namespace CSS.IM.App
{
    partial class MessageLogForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("我的好友");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("通知消息", 2, 2);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageLogForm));
            this.groupBox1 = new CSS.IM.UI.Control.SPanle();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.qqContextMenu1 = new CSS.IM.UI.Control.QQContextMenu();
            this.删除记录ToolStripMenuItem = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new CSS.IM.UI.Control.SPanle();
            this.RTBRecord = new CSS.IM.Library.ExtRichTextBox.MyExtRichTextBox();
            this.btn_frist = new CSS.IM.UI.Control.BasicButton();
            this.btn_last = new CSS.IM.UI.Control.BasicButton();
            this.btn_pre = new CSS.IM.UI.Control.BasicButton();
            this.btn_next = new CSS.IM.UI.Control.BasicButton();
            this.lab_pageCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_pageIndex = new System.Windows.Forms.TextBox();
            this.pal_buttons = new CSS.IM.UI.Control.SPanle();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.qqContextMenu1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pal_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(762, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 428);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.ContextMenuStrip = this.qqContextMenu1;
            this.treeView1.FullRowSelect = true;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(8, 19);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "tn_friend";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Text = "我的好友";
            treeNode2.ImageIndex = 2;
            treeNode2.Name = "tn_message";
            treeNode2.SelectedImageIndex = 2;
            treeNode2.Text = "通知消息";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(196, 400);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // qqContextMenu1
            // 
            this.qqContextMenu1.BackColor = System.Drawing.Color.White;
            this.qqContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除记录ToolStripMenuItem});
            this.qqContextMenu1.Name = "qqContextMenu1";
            this.qqContextMenu1.Size = new System.Drawing.Size(125, 26);
            this.qqContextMenu1.Paint += new System.Windows.Forms.PaintEventHandler(this.qqContextMenu1_Paint);
            // 
            // 删除记录ToolStripMenuItem
            // 
            this.删除记录ToolStripMenuItem.Name = "删除记录ToolStripMenuItem";
            this.删除记录ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除记录ToolStripMenuItem.Text = "删除记录";
            this.删除记录ToolStripMenuItem.Click += new System.EventHandler(this.删除记录ToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "util.png");
            this.imageList1.Images.SetKeyName(1, "user.png");
            this.imageList1.Images.SetKeyName(2, "message.png");
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.RTBRecord);
            this.groupBox2.Location = new System.Drawing.Point(227, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(561, 428);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            // 
            // RTBRecord
            // 
            this.RTBRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.RTBRecord.BackColor = System.Drawing.Color.White;
            this.RTBRecord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTBRecord.HiglightColor = CSS.IM.Library.ExtRichTextBox.RtfColor.White;
            this.RTBRecord.Location = new System.Drawing.Point(7, 19);
            this.RTBRecord.Name = "RTBRecord";
            this.RTBRecord.ReadOnly = true;
            this.RTBRecord.Size = new System.Drawing.Size(545, 400);
            this.RTBRecord.TabIndex = 0;
            this.RTBRecord.Text = "";
            this.RTBRecord.TextColor = CSS.IM.Library.ExtRichTextBox.RtfColor.Black;
            this.RTBRecord.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.RTBRecord_LinkClicked);
            // 
            // btn_frist
            // 
            this.btn_frist.BackColor = System.Drawing.Color.Transparent;
            this.btn_frist.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_frist.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_frist.Location = new System.Drawing.Point(29, 3);
            this.btn_frist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_frist.Name = "btn_frist";
            this.btn_frist.Size = new System.Drawing.Size(69, 21);
            this.btn_frist.TabIndex = 26;
            this.btn_frist.Texts = "首页";
            this.btn_frist.Click += new System.EventHandler(this.btn_frist_Click);
            // 
            // btn_last
            // 
            this.btn_last.BackColor = System.Drawing.Color.Transparent;
            this.btn_last.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_last.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_last.Location = new System.Drawing.Point(358, 3);
            this.btn_last.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_last.Name = "btn_last";
            this.btn_last.Size = new System.Drawing.Size(69, 21);
            this.btn_last.TabIndex = 27;
            this.btn_last.Texts = "尾页";
            this.btn_last.Click += new System.EventHandler(this.btn_last_Click);
            // 
            // btn_pre
            // 
            this.btn_pre.BackColor = System.Drawing.Color.Transparent;
            this.btn_pre.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_pre.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_pre.Location = new System.Drawing.Point(107, 3);
            this.btn_pre.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_pre.Name = "btn_pre";
            this.btn_pre.Size = new System.Drawing.Size(69, 21);
            this.btn_pre.TabIndex = 28;
            this.btn_pre.Texts = "上一页";
            this.btn_pre.Click += new System.EventHandler(this.btn_pre_Click);
            // 
            // btn_next
            // 
            this.btn_next.BackColor = System.Drawing.Color.Transparent;
            this.btn_next.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_next.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_next.Location = new System.Drawing.Point(280, 3);
            this.btn_next.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(69, 21);
            this.btn_next.TabIndex = 29;
            this.btn_next.Texts = "下一页";
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // lab_pageCount
            // 
            this.lab_pageCount.AutoSize = true;
            this.lab_pageCount.BackColor = System.Drawing.Color.Transparent;
            this.lab_pageCount.Location = new System.Drawing.Point(229, 9);
            this.lab_pageCount.Name = "lab_pageCount";
            this.lab_pageCount.Size = new System.Drawing.Size(29, 12);
            this.lab_pageCount.TabIndex = 30;
            this.lab_pageCount.Text = "总页";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(218, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 31;
            this.label2.Text = "/";
            // 
            // txt_pageIndex
            // 
            this.txt_pageIndex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_pageIndex.Location = new System.Drawing.Point(188, 8);
            this.txt_pageIndex.Name = "txt_pageIndex";
            this.txt_pageIndex.Size = new System.Drawing.Size(28, 14);
            this.txt_pageIndex.TabIndex = 32;
            this.txt_pageIndex.Text = "1";
            this.txt_pageIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_pageIndex.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_pageIndex_KeyUp);
            // 
            // pal_buttons
            // 
            this.pal_buttons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pal_buttons.BackColor = System.Drawing.Color.Transparent;
            this.pal_buttons.Controls.Add(this.btn_frist);
            this.pal_buttons.Controls.Add(this.txt_pageIndex);
            this.pal_buttons.Controls.Add(this.btn_last);
            this.pal_buttons.Controls.Add(this.label2);
            this.pal_buttons.Controls.Add(this.btn_pre);
            this.pal_buttons.Controls.Add(this.btn_next);
            this.pal_buttons.Controls.Add(this.lab_pageCount);
            this.pal_buttons.Location = new System.Drawing.Point(259, 469);
            this.pal_buttons.Name = "pal_buttons";
            this.pal_buttons.Size = new System.Drawing.Size(458, 27);
            this.pal_buttons.TabIndex = 1;
            this.pal_buttons.Visible = false;
            // 
            // MessageLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pal_buttons);
            this.Controls.Add(this.groupBox1);
            this.Name = "MessageLogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "消息管理";
            this.Load += new System.EventHandler(this.MessageLogForm_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.pal_buttons, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.qqContextMenu1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.pal_buttons.ResumeLayout(false);
            this.pal_buttons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CSS.IM.UI.Control.SPanle groupBox1;
        private CSS.IM.UI.Control.SPanle groupBox2;
        private System.Windows.Forms.TreeView treeView1;
        private Library.ExtRichTextBox.MyExtRichTextBox RTBRecord;
        private System.Windows.Forms.ImageList imageList1;
        private UI.Control.QQContextMenu qqContextMenu1;
        private UI.Control.QQToolStripMenuItem 删除记录ToolStripMenuItem;
        private UI.Control.BasicButton btn_frist;
        private UI.Control.BasicButton btn_last;
        private UI.Control.BasicButton btn_pre;
        private UI.Control.BasicButton btn_next;
        private System.Windows.Forms.Label lab_pageCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_pageIndex;
        private CSS.IM.UI.Control.SPanle pal_buttons;
    }
}