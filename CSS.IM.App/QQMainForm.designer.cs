using CSS.IM.UI.Control;
using System.Windows.Forms;
namespace CSS.IM.App
{
    partial class QQMainForm
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

            //try
            //{
            //    foreach (System.Diagnostics.Process item in System.Diagnostics.Process.GetProcesses())
            //    {
            //        if (item.ProcessName == "CSSIM.vshost")
            //        {
            //            item.Kill();
            //        }
            //        if (item.ProcessName == "CSSIM")
            //        {
            //            item.Kill();
            //        }
                    
            //    }
            //}
            //catch (System.Exception)
            //{
                
               
            //}

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QQMainForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.miniTimer = new System.Windows.Forms.Timer(this.components);
            this.groupListView = new System.Windows.Forms.Panel();
            this.pal_chatGroupRef = new System.Windows.Forms.Panel();
            this.pal_tree = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.treeContextMenu1 = new CSS.IM.UI.Control.QQContextMenu();
            this.tsmi添加好友 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi发送消息 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi查看信息 = new System.Windows.Forms.ToolStripMenuItem();
            this.treenode_imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.skinPanel = new System.Windows.Forms.Panel();
            this.shadPanel = new System.Windows.Forms.Panel();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.select_color = new System.Windows.Forms.PictureBox();
            this.select_shad = new System.Windows.Forms.PictureBox();
            this.iContextMenu1 = new CSS.IM.UI.Control.QQContextMenu();
            this.我在线上ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.忙碌ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.隐身ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.离开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.离线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.nContextMenu1 = new CSS.IM.UI.Control.QQContextMenu();
            this.我在线上ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.忙碌ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.离开ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.关闭声音ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.更换用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer_notifyIco = new System.Windows.Forms.Timer(this.components);
            this.mail_btn = new System.Windows.Forms.PictureBox();
            this.qzone16_btn = new System.Windows.Forms.PictureBox();
            this.userImg = new System.Windows.Forms.PictureBox();
            this.lt_btn = new System.Windows.Forms.PictureBox();
            this.nt_btn = new System.Windows.Forms.PictureBox();
            this.gp_btn = new System.Windows.Forms.PictureBox();
            this.fd_btn = new System.Windows.Forms.PictureBox();
            this.color_Btn = new System.Windows.Forms.PictureBox();
            this.find_Btn = new System.Windows.Forms.PictureBox();
            this.ButtonClose = new System.Windows.Forms.PictureBox();
            this.message_Btn = new System.Windows.Forms.PictureBox();
            this.tools_Btn = new System.Windows.Forms.PictureBox();
            this.ButtonMax = new System.Windows.Forms.PictureBox();
            this.ButtonMin = new System.Windows.Forms.PictureBox();
            this.menu_Btn = new System.Windows.Forms.PictureBox();
            this.stateButton1 = new CSS.IM.UI.Control.StateButton();
            this.description = new CSS.IM.UI.Control.BasicLable();
            this.groupListView.SuspendLayout();
            this.pal_tree.SuspendLayout();
            this.treeContextMenu1.SuspendLayout();
            this.skinPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.select_color)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.select_shad)).BeginInit();
            this.iContextMenu1.SuspendLayout();
            this.nContextMenu1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mail_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qzone16_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lt_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nt_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gp_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fd_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.color_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.find_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.message_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tools_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menu_Btn)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 4000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // miniTimer
            // 
            this.miniTimer.Interval = 1;
            this.miniTimer.Tick += new System.EventHandler(this.miniTimer_Tick);
            // 
            // groupListView
            // 
            this.groupListView.BackColor = System.Drawing.Color.White;
            this.groupListView.Controls.Add(this.pal_chatGroupRef);
            this.groupListView.Location = new System.Drawing.Point(2, 127);
            this.groupListView.Name = "groupListView";
            this.groupListView.Size = new System.Drawing.Size(222, 303);
            this.groupListView.TabIndex = 47;
            this.groupListView.Visible = false;
            // 
            // pal_chatGroupRef
            // 
            this.pal_chatGroupRef.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pal_chatGroupRef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pal_chatGroupRef.Location = new System.Drawing.Point(0, 0);
            this.pal_chatGroupRef.Name = "pal_chatGroupRef";
            this.pal_chatGroupRef.Size = new System.Drawing.Size(222, 303);
            this.pal_chatGroupRef.TabIndex = 1;
            // 
            // pal_tree
            // 
            this.pal_tree.BackColor = System.Drawing.Color.White;
            this.pal_tree.Controls.Add(this.treeView1);
            this.pal_tree.Location = new System.Drawing.Point(2, 127);
            this.pal_tree.Name = "pal_tree";
            this.pal_tree.Size = new System.Drawing.Size(221, 303);
            this.pal_tree.TabIndex = 51;
            this.pal_tree.Visible = false;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.ContextMenuStrip = this.treeContextMenu1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.treenode_imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(221, 303);
            this.treeView1.StateImageList = this.treenode_imageList1;
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // treeContextMenu1
            // 
            this.treeContextMenu1.BackColor = System.Drawing.Color.White;
            this.treeContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi添加好友,
            this.tsmi发送消息,
            this.tsmi查看信息});
            this.treeContextMenu1.Name = "nContextMenu1";
            this.treeContextMenu1.Size = new System.Drawing.Size(119, 70);
            this.treeContextMenu1.Paint += new System.Windows.Forms.PaintEventHandler(this.treeContextMenu1_Paint);
            // 
            // tsmi添加好友
            // 
            this.tsmi添加好友.Name = "tsmi添加好友";
            this.tsmi添加好友.Size = new System.Drawing.Size(118, 22);
            this.tsmi添加好友.Text = "添加好友";
            this.tsmi添加好友.Click += new System.EventHandler(this.tsmi添加好友_Click);
            // 
            // tsmi发送消息
            // 
            this.tsmi发送消息.Name = "tsmi发送消息";
            this.tsmi发送消息.Size = new System.Drawing.Size(118, 22);
            this.tsmi发送消息.Text = "发送消息";
            this.tsmi发送消息.Click += new System.EventHandler(this.tsmi发送消息_Click);
            // 
            // tsmi查看信息
            // 
            this.tsmi查看信息.Name = "tsmi查看信息";
            this.tsmi查看信息.Size = new System.Drawing.Size(118, 22);
            this.tsmi查看信息.Text = "查看信息";
            this.tsmi查看信息.Click += new System.EventHandler(this.tsmi查看信息_Click);
            // 
            // treenode_imageList1
            // 
            this.treenode_imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treenode_imageList1.ImageStream")));
            this.treenode_imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.treenode_imageList1.Images.SetKeyName(0, "root.gif");
            this.treenode_imageList1.Images.SetKeyName(1, "util.png");
            this.treenode_imageList1.Images.SetKeyName(2, "user.png");
            // 
            // skinPanel
            // 
            this.skinPanel.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel.Controls.Add(this.shadPanel);
            this.skinPanel.Controls.Add(this.colorPanel);
            this.skinPanel.Controls.Add(this.select_color);
            this.skinPanel.Controls.Add(this.select_shad);
            this.skinPanel.Location = new System.Drawing.Point(2, 99);
            this.skinPanel.Name = "skinPanel";
            this.skinPanel.Size = new System.Drawing.Size(0, 140);
            this.skinPanel.TabIndex = 41;
            this.skinPanel.Leave += new System.EventHandler(this.skinPanel_Leave);
            // 
            // shadPanel
            // 
            this.shadPanel.Location = new System.Drawing.Point(8, 38);
            this.shadPanel.Name = "shadPanel";
            this.shadPanel.Size = new System.Drawing.Size(218, 85);
            this.shadPanel.TabIndex = 4;
            // 
            // colorPanel
            // 
            this.colorPanel.Location = new System.Drawing.Point(7, 37);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(222, 98);
            this.colorPanel.TabIndex = 3;
            this.colorPanel.Visible = false;
            // 
            // select_color
            // 
            this.select_color.Location = new System.Drawing.Point(41, 5);
            this.select_color.Name = "select_color";
            this.select_color.Size = new System.Drawing.Size(30, 30);
            this.select_color.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.select_color.TabIndex = 2;
            this.select_color.TabStop = false;
            this.select_color.Click += new System.EventHandler(this.select_color_Click);
            // 
            // select_shad
            // 
            this.select_shad.Location = new System.Drawing.Point(5, 5);
            this.select_shad.Name = "select_shad";
            this.select_shad.Size = new System.Drawing.Size(30, 30);
            this.select_shad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.select_shad.TabIndex = 1;
            this.select_shad.TabStop = false;
            this.select_shad.Click += new System.EventHandler(this.select_shad_Click);
            // 
            // iContextMenu1
            // 
            this.iContextMenu1.BackColor = System.Drawing.Color.White;
            this.iContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.我在线上ToolStripMenuItem,
            this.忙碌ToolStripMenuItem,
            this.隐身ToolStripMenuItem,
            this.离开ToolStripMenuItem,
            this.离线ToolStripMenuItem});
            this.iContextMenu1.Name = "iContextMenu1";
            this.iContextMenu1.Size = new System.Drawing.Size(119, 114);
            // 
            // 我在线上ToolStripMenuItem
            // 
            this.我在线上ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("我在线上ToolStripMenuItem.Image")));
            this.我在线上ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.我在线上ToolStripMenuItem.Name = "我在线上ToolStripMenuItem";
            this.我在线上ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.我在线上ToolStripMenuItem.Text = "我在线上";
            this.我在线上ToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.我在线上ToolStripMenuItem_MouseDown);
            // 
            // 忙碌ToolStripMenuItem
            // 
            this.忙碌ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("忙碌ToolStripMenuItem.Image")));
            this.忙碌ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.忙碌ToolStripMenuItem.Name = "忙碌ToolStripMenuItem";
            this.忙碌ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.忙碌ToolStripMenuItem.Text = "忙碌";
            this.忙碌ToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.忙碌ToolStripMenuItem_MouseDown);
            // 
            // 隐身ToolStripMenuItem
            // 
            this.隐身ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("隐身ToolStripMenuItem.Image")));
            this.隐身ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.隐身ToolStripMenuItem.Name = "隐身ToolStripMenuItem";
            this.隐身ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.隐身ToolStripMenuItem.Text = "隐身";
            this.隐身ToolStripMenuItem.Visible = false;
            this.隐身ToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.隐身ToolStripMenuItem_MouseDown);
            // 
            // 离开ToolStripMenuItem
            // 
            this.离开ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("离开ToolStripMenuItem.Image")));
            this.离开ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.离开ToolStripMenuItem.Name = "离开ToolStripMenuItem";
            this.离开ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.离开ToolStripMenuItem.Text = "离开";
            this.离开ToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.离开ToolStripMenuItem_MouseDown);
            // 
            // 离线ToolStripMenuItem
            // 
            this.离线ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("离线ToolStripMenuItem.Image")));
            this.离线ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.离线ToolStripMenuItem.Name = "离线ToolStripMenuItem";
            this.离线ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.离线ToolStripMenuItem.Text = "离线";
            this.离线ToolStripMenuItem.Visible = false;
            this.离线ToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.离线ToolStripMenuItem_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.nContextMenu1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            this.notifyIcon1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseMove);
            // 
            // nContextMenu1
            // 
            this.nContextMenu1.BackColor = System.Drawing.Color.White;
            this.nContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.我在线上ToolStripMenuItem1,
            this.忙碌ToolStripMenuItem1,
            this.离开ToolStripMenuItem1,
            this.toolStripSeparator1,
            this.关闭声音ToolStripMenuItem,
            this.sToolStripMenuItem,
            this.更换用户ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.nContextMenu1.Name = "nContextMenu1";
            this.nContextMenu1.Size = new System.Drawing.Size(143, 148);
            // 
            // 我在线上ToolStripMenuItem1
            // 
            this.我在线上ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("我在线上ToolStripMenuItem1.Image")));
            this.我在线上ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.我在线上ToolStripMenuItem1.Name = "我在线上ToolStripMenuItem1";
            this.我在线上ToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.我在线上ToolStripMenuItem1.Text = "我在线上";
            this.我在线上ToolStripMenuItem1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.我在线上ToolStripMenuItem_MouseDown);
            // 
            // 忙碌ToolStripMenuItem1
            // 
            this.忙碌ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("忙碌ToolStripMenuItem1.Image")));
            this.忙碌ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.忙碌ToolStripMenuItem1.Name = "忙碌ToolStripMenuItem1";
            this.忙碌ToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.忙碌ToolStripMenuItem1.Text = "忙碌";
            this.忙碌ToolStripMenuItem1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.忙碌ToolStripMenuItem_MouseDown);
            // 
            // 离开ToolStripMenuItem1
            // 
            this.离开ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("离开ToolStripMenuItem1.Image")));
            this.离开ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.离开ToolStripMenuItem1.Name = "离开ToolStripMenuItem1";
            this.离开ToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.离开ToolStripMenuItem1.Text = "离开";
            this.离开ToolStripMenuItem1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.离开ToolStripMenuItem_MouseDown);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
            // 
            // 关闭声音ToolStripMenuItem
            // 
            this.关闭声音ToolStripMenuItem.Name = "关闭声音ToolStripMenuItem";
            this.关闭声音ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.关闭声音ToolStripMenuItem.Text = "关闭所有声音";
            this.关闭声音ToolStripMenuItem.Click += new System.EventHandler(this.关闭声音ToolStripMenuItem_Click);
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(139, 6);
            // 
            // 更换用户ToolStripMenuItem
            // 
            this.更换用户ToolStripMenuItem.Name = "更换用户ToolStripMenuItem";
            this.更换用户ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.更换用户ToolStripMenuItem.Text = "打开主面板";
            this.更换用户ToolStripMenuItem.Click += new System.EventHandler(this.更换用户ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // timer_notifyIco
            // 
            this.timer_notifyIco.Interval = 500;
            this.timer_notifyIco.Tick += new System.EventHandler(this.timer_notifyIco_Tick);
            // 
            // mail_btn
            // 
            this.mail_btn.BackColor = System.Drawing.Color.Transparent;
            this.mail_btn.Location = new System.Drawing.Point(30, 77);
            this.mail_btn.Name = "mail_btn";
            this.mail_btn.Size = new System.Drawing.Size(22, 22);
            this.mail_btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.mail_btn.TabIndex = 36;
            this.mail_btn.TabStop = false;
            this.mail_btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mail_btn_MouseClick);
            this.mail_btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.mail_btn.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.mail_btn.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.mail_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // qzone16_btn
            // 
            this.qzone16_btn.BackColor = System.Drawing.Color.Transparent;
            this.qzone16_btn.Location = new System.Drawing.Point(6, 77);
            this.qzone16_btn.Name = "qzone16_btn";
            this.qzone16_btn.Size = new System.Drawing.Size(22, 22);
            this.qzone16_btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.qzone16_btn.TabIndex = 35;
            this.qzone16_btn.TabStop = false;
            this.qzone16_btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_MouseClick);
            this.qzone16_btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.qzone16_btn.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.qzone16_btn.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.qzone16_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // userImg
            // 
            this.userImg.BackColor = System.Drawing.Color.Transparent;
            this.userImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.userImg.Location = new System.Drawing.Point(5, 30);
            this.userImg.Name = "userImg";
            this.userImg.Padding = new System.Windows.Forms.Padding(5);
            this.userImg.Size = new System.Drawing.Size(48, 48);
            this.userImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userImg.TabIndex = 51;
            this.userImg.TabStop = false;
            this.userImg.Click += new System.EventHandler(this.userImg_Click);
            this.userImg.MouseEnter += new System.EventHandler(this.userImg_MouseEnter);
            this.userImg.MouseLeave += new System.EventHandler(this.userImg_MouseLeave);
            // 
            // lt_btn
            // 
            this.lt_btn.BackColor = System.Drawing.Color.Transparent;
            this.lt_btn.Location = new System.Drawing.Point(179, 100);
            this.lt_btn.Name = "lt_btn";
            this.lt_btn.Size = new System.Drawing.Size(59, 27);
            this.lt_btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.lt_btn.TabIndex = 46;
            this.lt_btn.TabStop = false;
            this.lt_btn.Click += new System.EventHandler(this.tab_Click);
            this.lt_btn.MouseEnter += new System.EventHandler(this.tab_MouseEnter);
            this.lt_btn.MouseLeave += new System.EventHandler(this.tab_MouseLeave);
            // 
            // nt_btn
            // 
            this.nt_btn.BackColor = System.Drawing.Color.Transparent;
            this.nt_btn.Location = new System.Drawing.Point(120, 100);
            this.nt_btn.Name = "nt_btn";
            this.nt_btn.Size = new System.Drawing.Size(59, 27);
            this.nt_btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.nt_btn.TabIndex = 45;
            this.nt_btn.TabStop = false;
            this.nt_btn.Click += new System.EventHandler(this.tab_Click);
            this.nt_btn.MouseEnter += new System.EventHandler(this.tab_MouseEnter);
            this.nt_btn.MouseLeave += new System.EventHandler(this.tab_MouseLeave);
            // 
            // gp_btn
            // 
            this.gp_btn.BackColor = System.Drawing.Color.Transparent;
            this.gp_btn.Location = new System.Drawing.Point(61, 100);
            this.gp_btn.Name = "gp_btn";
            this.gp_btn.Size = new System.Drawing.Size(59, 27);
            this.gp_btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.gp_btn.TabIndex = 44;
            this.gp_btn.TabStop = false;
            this.gp_btn.Click += new System.EventHandler(this.tab_Click);
            this.gp_btn.MouseEnter += new System.EventHandler(this.tab_MouseEnter);
            this.gp_btn.MouseLeave += new System.EventHandler(this.tab_MouseLeave);
            // 
            // fd_btn
            // 
            this.fd_btn.BackColor = System.Drawing.Color.Transparent;
            this.fd_btn.Location = new System.Drawing.Point(2, 100);
            this.fd_btn.Name = "fd_btn";
            this.fd_btn.Size = new System.Drawing.Size(59, 27);
            this.fd_btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.fd_btn.TabIndex = 43;
            this.fd_btn.TabStop = false;
            this.fd_btn.Click += new System.EventHandler(this.tab_Click);
            this.fd_btn.MouseEnter += new System.EventHandler(this.tab_MouseEnter);
            this.fd_btn.MouseLeave += new System.EventHandler(this.tab_MouseLeave);
            // 
            // color_Btn
            // 
            this.color_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.color_Btn.BackColor = System.Drawing.Color.Transparent;
            this.color_Btn.Location = new System.Drawing.Point(213, 77);
            this.color_Btn.Name = "color_Btn";
            this.color_Btn.Size = new System.Drawing.Size(22, 22);
            this.color_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.color_Btn.TabIndex = 34;
            this.color_Btn.TabStop = false;
            this.color_Btn.Visible = false;
            this.color_Btn.Click += new System.EventHandler(this.color_Btn_Click);
            this.color_Btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.color_Btn_MouseDown);
            this.color_Btn.MouseEnter += new System.EventHandler(this.color_Btn_MouseEnter);
            this.color_Btn.MouseLeave += new System.EventHandler(this.color_Btn_MouseLeave);
            this.color_Btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.color_Btn_MouseUp);
            // 
            // find_Btn
            // 
            this.find_Btn.BackColor = System.Drawing.Color.Transparent;
            this.find_Btn.Location = new System.Drawing.Point(51, 433);
            this.find_Btn.Name = "find_Btn";
            this.find_Btn.Size = new System.Drawing.Size(22, 22);
            this.find_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.find_Btn.TabIndex = 39;
            this.find_Btn.TabStop = false;
            this.find_Btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.find_Btn_MouseClick);
            this.find_Btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.find_Btn.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.find_Btn.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.find_Btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.BackColor = System.Drawing.Color.Transparent;
            this.ButtonClose.Location = new System.Drawing.Point(202, 0);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(38, 18);
            this.ButtonClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ButtonClose.TabIndex = 21;
            this.ButtonClose.TabStop = false;
            this.ButtonClose.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonClose_Paint);
            this.ButtonClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonClose_MouseClick);
            this.ButtonClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonClose_MouseDown);
            this.ButtonClose.MouseEnter += new System.EventHandler(this.ButtonClose_MouseEnter);
            this.ButtonClose.MouseLeave += new System.EventHandler(this.ButtonClose_MouseLeave);
            this.ButtonClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonClose_MouseUp);
            // 
            // message_Btn
            // 
            this.message_Btn.BackColor = System.Drawing.Color.Transparent;
            this.message_Btn.Location = new System.Drawing.Point(26, 433);
            this.message_Btn.Name = "message_Btn";
            this.message_Btn.Size = new System.Drawing.Size(22, 22);
            this.message_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.message_Btn.TabIndex = 38;
            this.message_Btn.TabStop = false;
            this.message_Btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.message_Btn_MouseClick);
            this.message_Btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.message_Btn.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.message_Btn.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.message_Btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // tools_Btn
            // 
            this.tools_Btn.BackColor = System.Drawing.Color.Transparent;
            this.tools_Btn.Location = new System.Drawing.Point(1, 433);
            this.tools_Btn.Name = "tools_Btn";
            this.tools_Btn.Size = new System.Drawing.Size(22, 22);
            this.tools_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tools_Btn.TabIndex = 37;
            this.tools_Btn.TabStop = false;
            this.tools_Btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tools_Btn_MouseClick);
            this.tools_Btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.tools_Btn.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.tools_Btn.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.tools_Btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // ButtonMax
            // 
            this.ButtonMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonMax.BackColor = System.Drawing.Color.Transparent;
            this.ButtonMax.Location = new System.Drawing.Point(177, 0);
            this.ButtonMax.Name = "ButtonMax";
            this.ButtonMax.Size = new System.Drawing.Size(25, 18);
            this.ButtonMax.TabIndex = 23;
            this.ButtonMax.TabStop = false;
            this.ButtonMax.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonMax_Paint);
            this.ButtonMax.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonMax_MouseClick);
            this.ButtonMax.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonMax_MouseDown);
            this.ButtonMax.MouseEnter += new System.EventHandler(this.ButtonMax_MouseEnter);
            this.ButtonMax.MouseLeave += new System.EventHandler(this.ButtonMax_MouseLeave);
            this.ButtonMax.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMax_MouseUp);
            // 
            // ButtonMin
            // 
            this.ButtonMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonMin.BackColor = System.Drawing.Color.Transparent;
            this.ButtonMin.Location = new System.Drawing.Point(152, 0);
            this.ButtonMin.Name = "ButtonMin";
            this.ButtonMin.Size = new System.Drawing.Size(25, 18);
            this.ButtonMin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ButtonMin.TabIndex = 20;
            this.ButtonMin.TabStop = false;
            this.ButtonMin.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonMin_Paint);
            this.ButtonMin.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonMin_MouseClick);
            this.ButtonMin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonMin_MouseDown);
            this.ButtonMin.MouseEnter += new System.EventHandler(this.ButtonMin_MouseEnter);
            this.ButtonMin.MouseLeave += new System.EventHandler(this.ButtonMin_MouseLeave);
            this.ButtonMin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMin_MouseUp);
            // 
            // menu_Btn
            // 
            this.menu_Btn.BackColor = System.Drawing.Color.Transparent;
            this.menu_Btn.Location = new System.Drawing.Point(3, 433);
            this.menu_Btn.Name = "menu_Btn";
            this.menu_Btn.Size = new System.Drawing.Size(40, 40);
            this.menu_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.menu_Btn.TabIndex = 40;
            this.menu_Btn.TabStop = false;
            this.menu_Btn.Visible = false;
            this.menu_Btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.menu_Btn_MouseClick);
            // 
            // stateButton1
            // 
            this.stateButton1.BackColor = System.Drawing.Color.Transparent;
            this.stateButton1.Location = new System.Drawing.Point(54, 36);
            this.stateButton1.Name = "stateButton1";
            this.stateButton1.Size = new System.Drawing.Size(32, 20);
            this.stateButton1.State = 1;
            this.stateButton1.TabIndex = 52;
            this.stateButton1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.stateButton1_MouseClick);
            // 
            // description
            // 
            this.description.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.description.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.description.BackColor = System.Drawing.Color.Transparent;
            this.description.Location = new System.Drawing.Point(60, 56);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(175, 18);
            this.description.TabIndex = 32;
            this.description.MouseClick += new System.Windows.Forms.MouseEventHandler(this.description_MouseClick);
            // 
            // QQMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(205)))));
            this.ClientSize = new System.Drawing.Size(240, 500);
            this.Controls.Add(this.pal_tree);
            this.Controls.Add(this.groupListView);
            this.Controls.Add(this.stateButton1);
            this.Controls.Add(this.skinPanel);
            this.Controls.Add(this.mail_btn);
            this.Controls.Add(this.qzone16_btn);
            this.Controls.Add(this.userImg);
            this.Controls.Add(this.lt_btn);
            this.Controls.Add(this.nt_btn);
            this.Controls.Add(this.gp_btn);
            this.Controls.Add(this.fd_btn);
            this.Controls.Add(this.color_Btn);
            this.Controls.Add(this.find_Btn);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.message_Btn);
            this.Controls.Add(this.tools_Btn);
            this.Controls.Add(this.description);
            this.Controls.Add(this.ButtonMax);
            this.Controls.Add(this.ButtonMin);
            this.Controls.Add(this.menu_Btn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(240, 400);
            this.Name = "QQMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "`";
            this.Load += new System.EventHandler(this.QQMainForm_Load);
            this.groupListView.ResumeLayout(false);
            this.pal_tree.ResumeLayout(false);
            this.treeContextMenu1.ResumeLayout(false);
            this.skinPanel.ResumeLayout(false);
            this.skinPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.select_color)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.select_shad)).EndInit();
            this.iContextMenu1.ResumeLayout(false);
            this.nContextMenu1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mail_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qzone16_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lt_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nt_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gp_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fd_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.color_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.find_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.message_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tools_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menu_Btn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer miniTimer;
        private System.Windows.Forms.PictureBox ButtonMin;
        private System.Windows.Forms.PictureBox ButtonMax;
        private System.Windows.Forms.PictureBox ButtonClose;
        private System.Windows.Forms.Panel skinPanel;
        private System.Windows.Forms.PictureBox select_color;
        private System.Windows.Forms.PictureBox select_shad;
        private QQListViewEx friendListView;
        private System.Windows.Forms.PictureBox qzone16_btn;
        private System.Windows.Forms.PictureBox mail_btn;
        private System.Windows.Forms.PictureBox message_Btn;
        private System.Windows.Forms.PictureBox tools_Btn;
        private System.Windows.Forms.PictureBox find_Btn;
        private System.Windows.Forms.PictureBox menu_Btn;
        private System.Windows.Forms.PictureBox color_Btn;
        private PictureBox fd_btn;
        private PictureBox gp_btn;
        private PictureBox lt_btn;
        private PictureBox nt_btn;
        private System.Windows.Forms.Panel groupListView;
        private System.Windows.Forms.PictureBox userImg;
        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.Panel shadPanel;
        private QQContextMenu iContextMenu1;
        private System.Windows.Forms.ToolStripMenuItem 离开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 忙碌ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 隐身ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 离线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 我在线上ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pal_chatGroupRef;
        private System.Windows.Forms.Panel pal_tree;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private QQContextMenu nContextMenu1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更换用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭声音ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 离开ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 忙碌ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 我在线上ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator sToolStripMenuItem;
        private System.Windows.Forms.Timer timer_notifyIco;
        private QQContextMenu treeContextMenu1;
        private System.Windows.Forms.ToolStripMenuItem tsmi添加好友;
        private System.Windows.Forms.ToolStripMenuItem tsmi发送消息;
        private System.Windows.Forms.ToolStripMenuItem tsmi查看信息;
        private System.Windows.Forms.ImageList treenode_imageList1;
        private ChatHistoryFriendListView chat_history_listview;
        private StateButton stateButton1;
        private BasicLable description;

    }
}