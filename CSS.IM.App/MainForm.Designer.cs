namespace CSS.IM.App
{
    partial class MainForm
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
            if (btn_state_Menu!=null)
            {
                btn_state_Menu.Dispose();
                btn_state_Menu = null;
            }

            if (notifyIcon_MessageQueue_Menu != null)
            {
                notifyIcon_MessageQueue_Menu.Dispose();
                notifyIcon_MessageQueue_Menu = null;
            }

            if (treeView_nt_Menu != null)
            {
                treeView_nt_Menu.Dispose();
                treeView_nt_Menu = null;
            }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_state = new CSS.IM.UI.Control.StateButton();
            this.btn_mail = new CSS.IM.UI.Control.BarImageButton();
            this.btn_default_index = new CSS.IM.UI.Control.BarImageButton();
            this.userImg = new CSS.IM.UI.Control.HeadPortrait();
            this.description = new CSS.IM.UI.Control.BasicLable();
            this.btn_lt = new CSS.IM.UI.Control.TabImageButton();
            this.btn_nt = new CSS.IM.UI.Control.TabImageButton();
            this.btn_gp = new CSS.IM.UI.Control.TabImageButton();
            this.btn_fd = new CSS.IM.UI.Control.TabImageButton();
            this.btn_find = new CSS.IM.UI.Control.BarImageButton();
            this.btn_message = new CSS.IM.UI.Control.BarImageButton();
            this.btn_tools = new CSS.IM.UI.Control.BarImageButton();
            this.panel_fd = new System.Windows.Forms.Panel();
            this.listView_fd = new CSS.IM.UI.Control.QQListViewEx();
            this.panel_gp = new System.Windows.Forms.Panel();
            this.listView_gp = new CSS.IM.UI.Control.QQGroupChatListViewEx();
            this.panel_nt = new System.Windows.Forms.Panel();
            this.treeView_nt = new System.Windows.Forms.TreeView();
            this.treeView_nt_Menu = new CSS.IM.UI.Control.QQContextMenu();
            this.tsmi添加好友 = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.tsmi发送消息 = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.tsmi查看信息 = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.imageList_treeView_nt = new System.Windows.Forms.ImageList(this.components);
            this.panel_lt = new System.Windows.Forms.Panel();
            this.chatHistory_lt = new CSS.IM.UI.Control.QQHistoryListViewEx();
            this.btn_state_Menu = new CSS.IM.UI.Control.QQContextMenu();
            this.我在线上ToolStripMenuItem = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.忙碌ToolStripMenuItem = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.隐身ToolStripMenuItem = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.离开ToolStripMenuItem = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.离线ToolStripMenuItem = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.notifyIcon_MessageQueue = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIcon_MessageQueue_Menu = new CSS.IM.UI.Control.QQContextMenu();
            this.我在线上ToolStripMenuItem1 = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.忙碌ToolStripMenuItem1 = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.离开ToolStripMenuItem1 = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.toolStripSeparator1 = new CSS.IM.UI.Control.QQToolStripSeparator();
            this.关闭声音ToolStripMenuItem = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.sToolStripMenuItem = new CSS.IM.UI.Control.QQToolStripSeparator();
            this.打开主面板ToolStripMenuItem = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.退出ToolStripMenuItem = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.timer_MessageAlert = new System.Windows.Forms.Timer(this.components);
            this.btn_color = new CSS.IM.UI.Control.BarImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.btn_mail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_default_index)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_lt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_nt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_gp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_fd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_find)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_message)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_tools)).BeginInit();
            this.panel_fd.SuspendLayout();
            this.panel_gp.SuspendLayout();
            this.panel_nt.SuspendLayout();
            this.treeView_nt_Menu.SuspendLayout();
            this.panel_lt.SuspendLayout();
            this.btn_state_Menu.SuspendLayout();
            this.notifyIcon_MessageQueue_Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_color)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_state
            // 
            this.btn_state.BackColor = System.Drawing.Color.Transparent;
            this.btn_state.Location = new System.Drawing.Point(56, 32);
            this.btn_state.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_state.Name = "btn_state";
            this.btn_state.Size = new System.Drawing.Size(31, 20);
            this.btn_state.State = 1;
            this.btn_state.TabIndex = 0;
            this.btn_state.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_state_MouseClick);
            // 
            // btn_mail
            // 
            this.btn_mail.BackColor = System.Drawing.Color.Transparent;
            this.btn_mail.Image = ((System.Drawing.Image)(resources.GetObject("btn_mail.Image")));
            this.btn_mail.Location = new System.Drawing.Point(30, 77);
            this.btn_mail.Name = "btn_mail";
            this.btn_mail.Size = new System.Drawing.Size(22, 22);
            this.btn_mail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_mail.TabIndex = 55;
            this.btn_mail.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_mail, "邮箱");
            this.btn_mail.Visible = false;
            this.btn_mail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_mail_MouseClick);
            // 
            // btn_default_index
            // 
            this.btn_default_index.BackColor = System.Drawing.Color.Transparent;
            this.btn_default_index.Image = ((System.Drawing.Image)(resources.GetObject("btn_default_index.Image")));
            this.btn_default_index.Location = new System.Drawing.Point(6, 77);
            this.btn_default_index.Name = "btn_default_index";
            this.btn_default_index.Size = new System.Drawing.Size(22, 22);
            this.btn_default_index.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_default_index.TabIndex = 54;
            this.btn_default_index.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_default_index, "办公主页");
            this.btn_default_index.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_default_index_MouseClick);
            // 
            // userImg
            // 
            this.userImg.BackColor = System.Drawing.Color.Transparent;
            this.userImg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("userImg.BackgroundImage")));
            this.userImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.userImg.Image = ((System.Drawing.Image)(resources.GetObject("userImg.Image")));
            this.userImg.Location = new System.Drawing.Point(5, 30);
            this.userImg.Name = "userImg";
            this.userImg.Padding = new System.Windows.Forms.Padding(6);
            this.userImg.SelectTab = false;
            this.userImg.Size = new System.Drawing.Size(48, 48);
            this.userImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userImg.TabIndex = 56;
            this.userImg.TabStop = false;
            this.userImg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.userImg_MouseClick);
            // 
            // description
            // 
            this.description.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.description.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.description.BackColor = System.Drawing.Color.Transparent;
            this.description.Location = new System.Drawing.Point(59, 58);
            this.description.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(175, 20);
            this.description.TabIndex = 1;
            this.description.MouseClick += new System.Windows.Forms.MouseEventHandler(this.description_MouseClick);
            // 
            // btn_lt
            // 
            this.btn_lt.BackColor = System.Drawing.Color.Transparent;
            this.btn_lt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_lt.BackgroundImage")));
            this.btn_lt.Location = new System.Drawing.Point(179, 124);
            this.btn_lt.Name = "btn_lt";
            this.btn_lt.SelectTab = false;
            this.btn_lt.Size = new System.Drawing.Size(59, 27);
            this.btn_lt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_lt.TabIndex = 61;
            this.btn_lt.TabStop = false;
            this.btn_lt.Click += new System.EventHandler(this.btn_lt_Click);
            // 
            // btn_nt
            // 
            this.btn_nt.BackColor = System.Drawing.Color.Transparent;
            this.btn_nt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_nt.BackgroundImage")));
            this.btn_nt.Location = new System.Drawing.Point(120, 124);
            this.btn_nt.Name = "btn_nt";
            this.btn_nt.SelectTab = false;
            this.btn_nt.Size = new System.Drawing.Size(59, 27);
            this.btn_nt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_nt.TabIndex = 60;
            this.btn_nt.TabStop = false;
            this.btn_nt.Click += new System.EventHandler(this.btn_nt_Click);
            // 
            // btn_gp
            // 
            this.btn_gp.BackColor = System.Drawing.Color.Transparent;
            this.btn_gp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_gp.BackgroundImage")));
            this.btn_gp.Location = new System.Drawing.Point(61, 124);
            this.btn_gp.Name = "btn_gp";
            this.btn_gp.SelectTab = false;
            this.btn_gp.Size = new System.Drawing.Size(59, 27);
            this.btn_gp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_gp.TabIndex = 59;
            this.btn_gp.TabStop = false;
            this.btn_gp.Click += new System.EventHandler(this.btn_gp_Click);
            // 
            // btn_fd
            // 
            this.btn_fd.BackColor = System.Drawing.Color.Transparent;
            this.btn_fd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_fd.BackgroundImage")));
            this.btn_fd.Location = new System.Drawing.Point(2, 124);
            this.btn_fd.Name = "btn_fd";
            this.btn_fd.SelectTab = false;
            this.btn_fd.Size = new System.Drawing.Size(59, 27);
            this.btn_fd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_fd.TabIndex = 58;
            this.btn_fd.TabStop = false;
            this.btn_fd.Click += new System.EventHandler(this.btn_fd_Click);
            // 
            // btn_find
            // 
            this.btn_find.BackColor = System.Drawing.Color.Transparent;
            this.btn_find.Image = ((System.Drawing.Image)(resources.GetObject("btn_find.Image")));
            this.btn_find.Location = new System.Drawing.Point(56, 457);
            this.btn_find.Name = "btn_find";
            this.btn_find.Size = new System.Drawing.Size(22, 22);
            this.btn_find.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_find.TabIndex = 65;
            this.btn_find.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_find, "查找好友");
            this.btn_find.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_find_MouseClick);
            // 
            // btn_message
            // 
            this.btn_message.BackColor = System.Drawing.Color.Transparent;
            this.btn_message.Image = ((System.Drawing.Image)(resources.GetObject("btn_message.Image")));
            this.btn_message.Location = new System.Drawing.Point(31, 457);
            this.btn_message.Name = "btn_message";
            this.btn_message.Size = new System.Drawing.Size(22, 22);
            this.btn_message.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_message.TabIndex = 64;
            this.btn_message.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_message, "消息管理");
            this.btn_message.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_message_MouseClick);
            // 
            // btn_tools
            // 
            this.btn_tools.BackColor = System.Drawing.Color.Transparent;
            this.btn_tools.Image = ((System.Drawing.Image)(resources.GetObject("btn_tools.Image")));
            this.btn_tools.Location = new System.Drawing.Point(6, 457);
            this.btn_tools.Name = "btn_tools";
            this.btn_tools.Size = new System.Drawing.Size(22, 22);
            this.btn_tools.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_tools.TabIndex = 63;
            this.btn_tools.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_tools, "设置");
            this.btn_tools.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_tools_MouseClick);
            // 
            // panel_fd
            // 
            this.panel_fd.BackColor = System.Drawing.Color.White;
            this.panel_fd.Controls.Add(this.listView_fd);
            this.panel_fd.Location = new System.Drawing.Point(2, 151);
            this.panel_fd.Name = "panel_fd";
            this.panel_fd.Size = new System.Drawing.Size(25, 303);
            this.panel_fd.TabIndex = 66;
            this.panel_fd.Visible = false;
            // 
            // listView_fd
            // 
            this.listView_fd.AutoScroll = true;
            this.listView_fd.BackColor = System.Drawing.Color.White;
            this.listView_fd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_fd.FCType = CSS.IM.UI.Control.FriendContainerType.Big;
            this.listView_fd.ForeColor = System.Drawing.Color.White;
            this.listView_fd.Groups = ((System.Collections.Generic.Dictionary<string, CSS.IM.UI.Entity.Group>)(resources.GetObject("listView_fd.Groups")));
            this.listView_fd.Location = new System.Drawing.Point(0, 0);
            this.listView_fd.Name = "listView_fd";
            this.listView_fd.OldSelectFriend = null;
            this.listView_fd.Rosters = ((System.Collections.Generic.Dictionary<string, CSS.IM.UI.Entity.Friend>)(resources.GetObject("listView_fd.Rosters")));
            this.listView_fd.SelectFriend = null;
            this.listView_fd.Size = new System.Drawing.Size(25, 303);
            this.listView_fd.TabIndex = 0;
            this.listView_fd.XmppConnection = null;
            this.listView_fd.friend_qcm_MouseClickEvent += new CSS.IM.UI.Control.QQListViewEx.friend_qcm_MouseClick_Delegate(this.listView_fd_friend_qcm_MouseClickEvent);
            this.listView_fd.OpenChatEvent += new CSS.IM.UI.Control.QQListViewEx.delegate_openChat(this.listView_fd_OpenChatEvent);
            // 
            // panel_gp
            // 
            this.panel_gp.BackColor = System.Drawing.Color.White;
            this.panel_gp.Controls.Add(this.listView_gp);
            this.panel_gp.Location = new System.Drawing.Point(28, 151);
            this.panel_gp.Name = "panel_gp";
            this.panel_gp.Size = new System.Drawing.Size(25, 303);
            this.panel_gp.TabIndex = 67;
            this.panel_gp.Visible = false;
            // 
            // listView_gp
            // 
            this.listView_gp.AutoScroll = true;
            this.listView_gp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_gp.FCType = CSS.IM.UI.Control.FriendContainerType.Big;
            this.listView_gp.Location = new System.Drawing.Point(0, 0);
            this.listView_gp.Name = "listView_gp";
            this.listView_gp.Size = new System.Drawing.Size(25, 303);
            this.listView_gp.TabIndex = 0;
            this.listView_gp.ChatGroupOpenEvent += new CSS.IM.UI.Control.QQGroupChatListViewEx.ChatGroupOpenDelegate(this.listView_gp_Item_Open);
            // 
            // panel_nt
            // 
            this.panel_nt.BackColor = System.Drawing.Color.White;
            this.panel_nt.Controls.Add(this.treeView_nt);
            this.panel_nt.Location = new System.Drawing.Point(54, 151);
            this.panel_nt.Name = "panel_nt";
            this.panel_nt.Size = new System.Drawing.Size(25, 303);
            this.panel_nt.TabIndex = 67;
            this.panel_nt.Visible = false;
            // 
            // treeView_nt
            // 
            this.treeView_nt.BackColor = System.Drawing.Color.White;
            this.treeView_nt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView_nt.ContextMenuStrip = this.treeView_nt_Menu;
            this.treeView_nt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_nt.ImageIndex = 0;
            this.treeView_nt.ImageList = this.imageList_treeView_nt;
            this.treeView_nt.Location = new System.Drawing.Point(0, 0);
            this.treeView_nt.Name = "treeView_nt";
            this.treeView_nt.SelectedImageIndex = 0;
            this.treeView_nt.Size = new System.Drawing.Size(25, 303);
            this.treeView_nt.TabIndex = 1;
            this.treeView_nt.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_nt_NodeMouseDoubleClick);
            this.treeView_nt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_nt_MouseDown);
            // 
            // treeView_nt_Menu
            // 
            this.treeView_nt_Menu.BackColor = System.Drawing.Color.White;
            this.treeView_nt_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi添加好友,
            this.tsmi发送消息,
            this.tsmi查看信息});
            this.treeView_nt_Menu.Name = "nContextMenu1";
            this.treeView_nt_Menu.Size = new System.Drawing.Size(125, 70);
            this.treeView_nt_Menu.Paint += new System.Windows.Forms.PaintEventHandler(this.treeView_nt_Menu_Paint);
            // 
            // tsmi添加好友
            // 
            this.tsmi添加好友.Name = "tsmi添加好友";
            this.tsmi添加好友.Size = new System.Drawing.Size(124, 22);
            this.tsmi添加好友.Text = "添加好友";
            this.tsmi添加好友.Click += new System.EventHandler(this.tsmi添加好友_Click);
            // 
            // tsmi发送消息
            // 
            this.tsmi发送消息.Name = "tsmi发送消息";
            this.tsmi发送消息.Size = new System.Drawing.Size(124, 22);
            this.tsmi发送消息.Text = "发送消息";
            this.tsmi发送消息.Click += new System.EventHandler(this.tsmi发送消息_Click);
            // 
            // tsmi查看信息
            // 
            this.tsmi查看信息.Name = "tsmi查看信息";
            this.tsmi查看信息.Size = new System.Drawing.Size(124, 22);
            this.tsmi查看信息.Text = "查看信息";
            this.tsmi查看信息.Click += new System.EventHandler(this.tsmi查看信息_Click);
            // 
            // imageList_treeView_nt
            // 
            this.imageList_treeView_nt.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_treeView_nt.ImageStream")));
            this.imageList_treeView_nt.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_treeView_nt.Images.SetKeyName(0, "root.gif");
            this.imageList_treeView_nt.Images.SetKeyName(1, "util.png");
            this.imageList_treeView_nt.Images.SetKeyName(2, "user.png");
            // 
            // panel_lt
            // 
            this.panel_lt.BackColor = System.Drawing.Color.White;
            this.panel_lt.Controls.Add(this.chatHistory_lt);
            this.panel_lt.Location = new System.Drawing.Point(80, 151);
            this.panel_lt.Name = "panel_lt";
            this.panel_lt.Size = new System.Drawing.Size(25, 303);
            this.panel_lt.TabIndex = 67;
            this.panel_lt.Visible = false;
            // 
            // chatHistory_lt
            // 
            this.chatHistory_lt.AutoScroll = true;
            this.chatHistory_lt.BackColor = System.Drawing.Color.White;
            this.chatHistory_lt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatHistory_lt.FCType = CSS.IM.UI.Control.FriendContainerType.Big;
            this.chatHistory_lt.Location = new System.Drawing.Point(0, 0);
            this.chatHistory_lt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chatHistory_lt.Name = "chatHistory_lt";
            this.chatHistory_lt.OldSelectFriend = null;
            this.chatHistory_lt.Rosters = ((System.Collections.Generic.Dictionary<string, CSS.IM.UI.Entity.Friend>)(resources.GetObject("chatHistory_lt.Rosters")));
            this.chatHistory_lt.SelectFriend = null;
            this.chatHistory_lt.Size = new System.Drawing.Size(25, 303);
            this.chatHistory_lt.TabIndex = 0;
            this.chatHistory_lt.XmppConnection = null;
            this.chatHistory_lt.OpenChatEvent += new CSS.IM.UI.Control.QQHistoryListViewEx.delegate_openChat(this.listView_fd_OpenChatEvent);
            // 
            // btn_state_Menu
            // 
            this.btn_state_Menu.BackColor = System.Drawing.Color.White;
            this.btn_state_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.我在线上ToolStripMenuItem,
            this.忙碌ToolStripMenuItem,
            this.隐身ToolStripMenuItem,
            this.离开ToolStripMenuItem,
            this.离线ToolStripMenuItem});
            this.btn_state_Menu.Name = "iContextMenu1";
            this.btn_state_Menu.Size = new System.Drawing.Size(125, 114);
            // 
            // 我在线上ToolStripMenuItem
            // 
            this.我在线上ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("我在线上ToolStripMenuItem.Image")));
            this.我在线上ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.我在线上ToolStripMenuItem.Name = "我在线上ToolStripMenuItem";
            this.我在线上ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.我在线上ToolStripMenuItem.Text = "我在线上";
            this.我在线上ToolStripMenuItem.Click += new System.EventHandler(this.我在线上ToolStripMenuItem_Click);
            // 
            // 忙碌ToolStripMenuItem
            // 
            this.忙碌ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("忙碌ToolStripMenuItem.Image")));
            this.忙碌ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.忙碌ToolStripMenuItem.Name = "忙碌ToolStripMenuItem";
            this.忙碌ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.忙碌ToolStripMenuItem.Text = "忙碌";
            this.忙碌ToolStripMenuItem.Click += new System.EventHandler(this.忙碌ToolStripMenuItem_Click);
            // 
            // 隐身ToolStripMenuItem
            // 
            this.隐身ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("隐身ToolStripMenuItem.Image")));
            this.隐身ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.隐身ToolStripMenuItem.Name = "隐身ToolStripMenuItem";
            this.隐身ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.隐身ToolStripMenuItem.Text = "隐身";
            this.隐身ToolStripMenuItem.Visible = false;
            // 
            // 离开ToolStripMenuItem
            // 
            this.离开ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("离开ToolStripMenuItem.Image")));
            this.离开ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.离开ToolStripMenuItem.Name = "离开ToolStripMenuItem";
            this.离开ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.离开ToolStripMenuItem.Text = "离开";
            this.离开ToolStripMenuItem.Click += new System.EventHandler(this.离开ToolStripMenuItem_Click);
            // 
            // 离线ToolStripMenuItem
            // 
            this.离线ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("离线ToolStripMenuItem.Image")));
            this.离线ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.离线ToolStripMenuItem.Name = "离线ToolStripMenuItem";
            this.离线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.离线ToolStripMenuItem.Text = "离线";
            this.离线ToolStripMenuItem.Visible = false;
            // 
            // notifyIcon_MessageQueue
            // 
            this.notifyIcon_MessageQueue.ContextMenuStrip = this.notifyIcon_MessageQueue_Menu;
            this.notifyIcon_MessageQueue.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_MessageQueue.Icon")));
            this.notifyIcon_MessageQueue.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MessageQueue_MouseDoubleClick);
            this.notifyIcon_MessageQueue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MessageQueue_MouseMove);
            // 
            // notifyIcon_MessageQueue_Menu
            // 
            this.notifyIcon_MessageQueue_Menu.BackColor = System.Drawing.Color.White;
            this.notifyIcon_MessageQueue_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.我在线上ToolStripMenuItem1,
            this.忙碌ToolStripMenuItem1,
            this.离开ToolStripMenuItem1,
            this.toolStripSeparator1,
            this.关闭声音ToolStripMenuItem,
            this.sToolStripMenuItem,
            this.打开主面板ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.notifyIcon_MessageQueue_Menu.Name = "nContextMenu1";
            this.notifyIcon_MessageQueue_Menu.Size = new System.Drawing.Size(149, 148);
            // 
            // 我在线上ToolStripMenuItem1
            // 
            this.我在线上ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("我在线上ToolStripMenuItem1.Image")));
            this.我在线上ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.我在线上ToolStripMenuItem1.Name = "我在线上ToolStripMenuItem1";
            this.我在线上ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.我在线上ToolStripMenuItem1.Text = "我在线上";
            this.我在线上ToolStripMenuItem1.Click += new System.EventHandler(this.我在线上ToolStripMenuItem1_Click);
            // 
            // 忙碌ToolStripMenuItem1
            // 
            this.忙碌ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("忙碌ToolStripMenuItem1.Image")));
            this.忙碌ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.忙碌ToolStripMenuItem1.Name = "忙碌ToolStripMenuItem1";
            this.忙碌ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.忙碌ToolStripMenuItem1.Text = "忙碌";
            this.忙碌ToolStripMenuItem1.Click += new System.EventHandler(this.忙碌ToolStripMenuItem1_Click);
            // 
            // 离开ToolStripMenuItem1
            // 
            this.离开ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("离开ToolStripMenuItem1.Image")));
            this.离开ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.离开ToolStripMenuItem1.Name = "离开ToolStripMenuItem1";
            this.离开ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.离开ToolStripMenuItem1.Text = "离开";
            this.离开ToolStripMenuItem1.Click += new System.EventHandler(this.离开ToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // 关闭声音ToolStripMenuItem
            // 
            this.关闭声音ToolStripMenuItem.Name = "关闭声音ToolStripMenuItem";
            this.关闭声音ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.关闭声音ToolStripMenuItem.Text = "关闭所有声音";
            this.关闭声音ToolStripMenuItem.Click += new System.EventHandler(this.关闭声音ToolStripMenuItem_Click);
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(145, 6);
            // 
            // 打开主面板ToolStripMenuItem
            // 
            this.打开主面板ToolStripMenuItem.Name = "打开主面板ToolStripMenuItem";
            this.打开主面板ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.打开主面板ToolStripMenuItem.Text = "打开主面板";
            this.打开主面板ToolStripMenuItem.Click += new System.EventHandler(this.打开主面板ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // timer_MessageAlert
            // 
            this.timer_MessageAlert.Interval = 500;
            this.timer_MessageAlert.Tick += new System.EventHandler(this.timer_MessageAlert_Tick);
            // 
            // btn_color
            // 
            this.btn_color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_color.BackColor = System.Drawing.Color.Transparent;
            this.btn_color.Image = ((System.Drawing.Image)(resources.GetObject("btn_color.Image")));
            this.btn_color.Location = new System.Drawing.Point(213, 77);
            this.btn_color.Name = "btn_color";
            this.btn_color.Size = new System.Drawing.Size(22, 22);
            this.btn_color.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_color.TabIndex = 68;
            this.btn_color.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_color, "邮箱");
            this.btn_color.Visible = false;
            this.btn_color.Click += new System.EventHandler(this.btn_color_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 492);
            this.Controls.Add(this.btn_color);
            this.Controls.Add(this.panel_lt);
            this.Controls.Add(this.panel_nt);
            this.Controls.Add(this.panel_gp);
            this.Controls.Add(this.panel_fd);
            this.Controls.Add(this.btn_find);
            this.Controls.Add(this.btn_message);
            this.Controls.Add(this.btn_tools);
            this.Controls.Add(this.btn_lt);
            this.Controls.Add(this.btn_nt);
            this.Controls.Add(this.btn_gp);
            this.Controls.Add(this.btn_fd);
            this.Controls.Add(this.btn_state);
            this.Controls.Add(this.btn_mail);
            this.Controls.Add(this.btn_default_index);
            this.Controls.Add(this.userImg);
            this.Controls.Add(this.description);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "办公助手";
            this.CloseEvent += new CSS.IM.UI.Control.IQQMainForm.CloseEventDelegate(this.MainForm_CloseEvent);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Controls.SetChildIndex(this.description, 0);
            this.Controls.SetChildIndex(this.userImg, 0);
            this.Controls.SetChildIndex(this.btn_default_index, 0);
            this.Controls.SetChildIndex(this.btn_mail, 0);
            this.Controls.SetChildIndex(this.btn_state, 0);
            this.Controls.SetChildIndex(this.btn_fd, 0);
            this.Controls.SetChildIndex(this.btn_gp, 0);
            this.Controls.SetChildIndex(this.btn_nt, 0);
            this.Controls.SetChildIndex(this.btn_lt, 0);
            this.Controls.SetChildIndex(this.btn_tools, 0);
            this.Controls.SetChildIndex(this.btn_message, 0);
            this.Controls.SetChildIndex(this.btn_find, 0);
            this.Controls.SetChildIndex(this.panel_fd, 0);
            this.Controls.SetChildIndex(this.panel_gp, 0);
            this.Controls.SetChildIndex(this.panel_nt, 0);
            this.Controls.SetChildIndex(this.panel_lt, 0);
            this.Controls.SetChildIndex(this.btn_color, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btn_mail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_default_index)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_lt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_nt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_gp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_fd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_find)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_message)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_tools)).EndInit();
            this.panel_fd.ResumeLayout(false);
            this.panel_gp.ResumeLayout(false);
            this.panel_nt.ResumeLayout(false);
            this.treeView_nt_Menu.ResumeLayout(false);
            this.panel_lt.ResumeLayout(false);
            this.btn_state_Menu.ResumeLayout(false);
            this.notifyIcon_MessageQueue_Menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_color)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Control.StateButton btn_state;
        private CSS.IM.UI.Control.BarImageButton btn_mail;
        private CSS.IM.UI.Control.BarImageButton btn_default_index;
        private CSS.IM.UI.Control.HeadPortrait userImg;
        private UI.Control.BasicLable description;
        private CSS.IM.UI.Control.TabImageButton btn_lt;
        private CSS.IM.UI.Control.TabImageButton btn_nt;
        private CSS.IM.UI.Control.TabImageButton btn_gp;
        private CSS.IM.UI.Control.TabImageButton btn_fd;
        private CSS.IM.UI.Control.BarImageButton btn_find;
        private CSS.IM.UI.Control.BarImageButton btn_message;
        private CSS.IM.UI.Control.BarImageButton btn_tools;
        private System.Windows.Forms.Panel panel_fd;
        private System.Windows.Forms.Panel panel_gp;
        private System.Windows.Forms.Panel panel_nt;
        private System.Windows.Forms.Panel panel_lt;
        private System.Windows.Forms.NotifyIcon notifyIcon_MessageQueue;
        private UI.Control.QQListViewEx listView_fd;
        private System.Windows.Forms.TreeView treeView_nt;
        private UI.Control.QQHistoryListViewEx chatHistory_lt;
        private System.Windows.Forms.Timer timer_MessageAlert;
        private CSS.IM.UI.Control.QQGroupChatListViewEx listView_gp;
        private System.Windows.Forms.ImageList imageList_treeView_nt;

        private UI.Control.QQContextMenu btn_state_Menu;
        private UI.Control.QQToolStripMenuItem 我在线上ToolStripMenuItem;
        private UI.Control.QQToolStripMenuItem 忙碌ToolStripMenuItem;
        private UI.Control.QQToolStripMenuItem 隐身ToolStripMenuItem;
        private UI.Control.QQToolStripMenuItem 离开ToolStripMenuItem;
        private UI.Control.QQToolStripMenuItem 离线ToolStripMenuItem;

        private UI.Control.QQContextMenu notifyIcon_MessageQueue_Menu;
        private UI.Control.QQToolStripMenuItem 我在线上ToolStripMenuItem1;
        private UI.Control.QQToolStripMenuItem 忙碌ToolStripMenuItem1;
        private UI.Control.QQToolStripMenuItem 离开ToolStripMenuItem1;
        private UI.Control.QQToolStripSeparator toolStripSeparator1;
        private UI.Control.QQToolStripMenuItem 关闭声音ToolStripMenuItem;
        private UI.Control.QQToolStripSeparator sToolStripMenuItem;
        private UI.Control.QQToolStripMenuItem 打开主面板ToolStripMenuItem;
        private UI.Control.QQToolStripMenuItem 退出ToolStripMenuItem;
        private UI.Control.QQContextMenu treeView_nt_Menu;
        private UI.Control.QQToolStripMenuItem tsmi添加好友;
        private UI.Control.QQToolStripMenuItem tsmi发送消息;
        private UI.Control.QQToolStripMenuItem tsmi查看信息;
        private UI.Control.BarImageButton btn_color;

    }
}