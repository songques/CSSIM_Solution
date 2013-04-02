namespace CSS.IM.App
{
    partial class ChatFromMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatFromMsg));
            this.friendHead = new CSS.IM.UI.Control.HeadPortrait();
            this.btn_remotedisktop = new CSS.IM.UI.Control.BarImageButton();
            this.btn_addfriend = new CSS.IM.UI.Control.BarImageButton();
            this.btn_more = new CSS.IM.UI.Control.BarImageButton();
            this.btn_filesend = new CSS.IM.UI.Control.BarImageButton();
            this.btn_videoOpen = new CSS.IM.UI.Control.BarImageButton();
            this.panel_msg = new CSS.IM.UI.Control.PanelQQText();
            this.toolBarBg = new CSS.IM.UI.Control.BarPanel();
            this.btn_FontColor = new CSS.IM.UI.Control.BarImageButton();
            this.btn_font = new CSS.IM.UI.Control.BarImageButton();
            this.btn_screen = new CSS.IM.UI.Control.BarImageButton();
            this.btn_pic = new CSS.IM.UI.Control.BarImageButton();
            this.btn_zhen = new CSS.IM.UI.Control.BarImageButton();
            this.btn_face = new CSS.IM.UI.Control.BarImageButton();
            this.RTBRecord = new CSS.IM.Library.ExtRichTextBox.MyExtRichTextBox();
            this.rtfSend = new CSS.IM.Library.ExtRichTextBox.MyExtRichTextBox();
            this.btn_send_key = new CSS.IM.UI.Control.BasicButtonArrows();
            this.btn_close = new CSS.IM.UI.Control.BasicButton();
            this.btn_send = new CSS.IM.UI.Control.BasicButton();
            this.Close_Check = new System.Windows.Forms.Timer(this.components);
            this.panel_function = new CSS.IM.UI.Control.PanelQQText();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fileTansfersContainer = new CSS.IM.UI.Control.Graphics.FileTransfersControl.FileTansfersContainer();
            this.QQcm_send_key = new CSS.IM.UI.Control.QQContextMenu();
            this.QQtlm_key_enter = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.QQtlm_key_ctrl_enter = new CSS.IM.UI.Control.QQToolStripMenuItem();
            this.LB_sockUDP = new CSS.IM.Library.Net.SockUDP(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.friendHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_remotedisktop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_addfriend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_more)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_filesend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_videoOpen)).BeginInit();
            this.panel_msg.SuspendLayout();
            this.toolBarBg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_FontColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_font)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_screen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_zhen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_face)).BeginInit();
            this.panel_function.SuspendLayout();
            this.panel1.SuspendLayout();
            this.QQcm_send_key.SuspendLayout();
            this.SuspendLayout();
            // 
            // nikeName
            // 
            this.nikeName.Location = new System.Drawing.Point(78, 11);
            this.nikeName.Size = new System.Drawing.Size(224, 19);
            // 
            // friendHead
            // 
            this.friendHead.BackColor = System.Drawing.Color.Transparent;
            this.friendHead.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("friendHead.BackgroundImage")));
            this.friendHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.friendHead.Cursor = System.Windows.Forms.Cursors.Hand;
            this.friendHead.Image = ((System.Drawing.Image)(resources.GetObject("friendHead.Image")));
            this.friendHead.Location = new System.Drawing.Point(13, 12);
            this.friendHead.Name = "friendHead";
            this.friendHead.Padding = new System.Windows.Forms.Padding(6);
            this.friendHead.SelectTab = false;
            this.friendHead.Size = new System.Drawing.Size(57, 57);
            this.friendHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.friendHead.TabIndex = 58;
            this.friendHead.TabStop = false;
            // 
            // btn_remotedisktop
            // 
            this.btn_remotedisktop.BackColor = System.Drawing.Color.Transparent;
            this.btn_remotedisktop.Image = ((System.Drawing.Image)(resources.GetObject("btn_remotedisktop.Image")));
            this.btn_remotedisktop.Location = new System.Drawing.Point(201, 33);
            this.btn_remotedisktop.Name = "btn_remotedisktop";
            this.btn_remotedisktop.Size = new System.Drawing.Size(35, 35);
            this.btn_remotedisktop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_remotedisktop.TabIndex = 73;
            this.btn_remotedisktop.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_remotedisktop, "远程协助");
            this.btn_remotedisktop.Visible = false;
            // 
            // btn_addfriend
            // 
            this.btn_addfriend.BackColor = System.Drawing.Color.Transparent;
            this.btn_addfriend.Image = ((System.Drawing.Image)(resources.GetObject("btn_addfriend.Image")));
            this.btn_addfriend.Location = new System.Drawing.Point(242, 33);
            this.btn_addfriend.Name = "btn_addfriend";
            this.btn_addfriend.Size = new System.Drawing.Size(35, 35);
            this.btn_addfriend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_addfriend.TabIndex = 72;
            this.btn_addfriend.TabStop = false;
            this.btn_addfriend.Visible = false;
            // 
            // btn_more
            // 
            this.btn_more.BackColor = System.Drawing.Color.Transparent;
            this.btn_more.Image = ((System.Drawing.Image)(resources.GetObject("btn_more.Image")));
            this.btn_more.Location = new System.Drawing.Point(160, 33);
            this.btn_more.Name = "btn_more";
            this.btn_more.Size = new System.Drawing.Size(35, 35);
            this.btn_more.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_more.TabIndex = 71;
            this.btn_more.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_more, "更多功能");
            this.btn_more.Click += new System.EventHandler(this.btn_more_Click);
            // 
            // btn_filesend
            // 
            this.btn_filesend.BackColor = System.Drawing.Color.Transparent;
            this.btn_filesend.Image = ((System.Drawing.Image)(resources.GetObject("btn_filesend.Image")));
            this.btn_filesend.Location = new System.Drawing.Point(119, 33);
            this.btn_filesend.Name = "btn_filesend";
            this.btn_filesend.Size = new System.Drawing.Size(35, 35);
            this.btn_filesend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_filesend.TabIndex = 70;
            this.btn_filesend.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_filesend, "文件传输");
            this.btn_filesend.Click += new System.EventHandler(this.btn_filesend_Click);
            // 
            // btn_videoOpen
            // 
            this.btn_videoOpen.BackColor = System.Drawing.Color.Transparent;
            this.btn_videoOpen.Image = ((System.Drawing.Image)(resources.GetObject("btn_videoOpen.Image")));
            this.btn_videoOpen.Location = new System.Drawing.Point(78, 33);
            this.btn_videoOpen.Name = "btn_videoOpen";
            this.btn_videoOpen.Size = new System.Drawing.Size(35, 35);
            this.btn_videoOpen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_videoOpen.TabIndex = 69;
            this.btn_videoOpen.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_videoOpen, "视频通话");
            this.btn_videoOpen.Click += new System.EventHandler(this.btn_videoOpen_Click);
            // 
            // panel_msg
            // 
            this.panel_msg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_msg.BackColor = System.Drawing.Color.Transparent;
            this.panel_msg.Controls.Add(this.toolBarBg);
            this.panel_msg.Controls.Add(this.RTBRecord);
            this.panel_msg.Controls.Add(this.rtfSend);
            this.panel_msg.Location = new System.Drawing.Point(5, 72);
            this.panel_msg.Margin = new System.Windows.Forms.Padding(0);
            this.panel_msg.Name = "panel_msg";
            this.panel_msg.Size = new System.Drawing.Size(391, 385);
            this.panel_msg.TabIndex = 74;
            // 
            // toolBarBg
            // 
            this.toolBarBg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolBarBg.Controls.Add(this.btn_FontColor);
            this.toolBarBg.Controls.Add(this.btn_font);
            this.toolBarBg.Controls.Add(this.btn_screen);
            this.toolBarBg.Controls.Add(this.btn_pic);
            this.toolBarBg.Controls.Add(this.btn_zhen);
            this.toolBarBg.Controls.Add(this.btn_face);
            this.toolBarBg.Location = new System.Drawing.Point(0, 265);
            this.toolBarBg.Name = "toolBarBg";
            this.toolBarBg.Size = new System.Drawing.Size(388, 22);
            this.toolBarBg.TabIndex = 2;
            // 
            // btn_FontColor
            // 
            this.btn_FontColor.BackColor = System.Drawing.Color.Transparent;
            this.btn_FontColor.Image = ((System.Drawing.Image)(resources.GetObject("btn_FontColor.Image")));
            this.btn_FontColor.Location = new System.Drawing.Point(32, 1);
            this.btn_FontColor.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.btn_FontColor.Name = "btn_FontColor";
            this.btn_FontColor.Size = new System.Drawing.Size(20, 20);
            this.btn_FontColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_FontColor.TabIndex = 55;
            this.btn_FontColor.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_FontColor, "字体颜色");
            this.btn_FontColor.Click += new System.EventHandler(this.btn_FontColor_Click);
            // 
            // btn_font
            // 
            this.btn_font.BackColor = System.Drawing.Color.Transparent;
            this.btn_font.Image = ((System.Drawing.Image)(resources.GetObject("btn_font.Image")));
            this.btn_font.Location = new System.Drawing.Point(5, 1);
            this.btn_font.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.btn_font.Name = "btn_font";
            this.btn_font.Size = new System.Drawing.Size(20, 20);
            this.btn_font.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_font.TabIndex = 50;
            this.btn_font.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_font, "字体");
            this.btn_font.Click += new System.EventHandler(this.btn_font_Click);
            // 
            // btn_screen
            // 
            this.btn_screen.BackColor = System.Drawing.Color.Transparent;
            this.btn_screen.Image = ((System.Drawing.Image)(resources.GetObject("btn_screen.Image")));
            this.btn_screen.Location = new System.Drawing.Point(86, 1);
            this.btn_screen.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.btn_screen.Name = "btn_screen";
            this.btn_screen.Size = new System.Drawing.Size(20, 20);
            this.btn_screen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_screen.TabIndex = 54;
            this.btn_screen.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_screen, "截屏");
            this.btn_screen.Click += new System.EventHandler(this.btn_screen_Click);
            // 
            // btn_pic
            // 
            this.btn_pic.BackColor = System.Drawing.Color.Transparent;
            this.btn_pic.Image = ((System.Drawing.Image)(resources.GetObject("btn_pic.Image")));
            this.btn_pic.Location = new System.Drawing.Point(168, 1);
            this.btn_pic.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.btn_pic.Name = "btn_pic";
            this.btn_pic.Size = new System.Drawing.Size(20, 20);
            this.btn_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_pic.TabIndex = 53;
            this.btn_pic.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_pic, "发送图片");
            this.btn_pic.Visible = false;
            // 
            // btn_zhen
            // 
            this.btn_zhen.BackColor = System.Drawing.Color.Transparent;
            this.btn_zhen.Image = ((System.Drawing.Image)(resources.GetObject("btn_zhen.Image")));
            this.btn_zhen.Location = new System.Drawing.Point(142, 2);
            this.btn_zhen.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.btn_zhen.Name = "btn_zhen";
            this.btn_zhen.Size = new System.Drawing.Size(20, 20);
            this.btn_zhen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_zhen.TabIndex = 52;
            this.btn_zhen.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_zhen, "震动");
            this.btn_zhen.Visible = false;
            // 
            // btn_face
            // 
            this.btn_face.BackColor = System.Drawing.Color.Transparent;
            this.btn_face.Image = ((System.Drawing.Image)(resources.GetObject("btn_face.Image")));
            this.btn_face.Location = new System.Drawing.Point(59, 1);
            this.btn_face.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.btn_face.Name = "btn_face";
            this.btn_face.Size = new System.Drawing.Size(20, 20);
            this.btn_face.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_face.TabIndex = 51;
            this.btn_face.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_face, "表情");
            this.btn_face.Click += new System.EventHandler(this.btn_face_Click);
            // 
            // RTBRecord
            // 
            this.RTBRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTBRecord.BackColor = System.Drawing.Color.White;
            this.RTBRecord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTBRecord.HiglightColor = CSS.IM.Library.ExtRichTextBox.RtfColor.White;
            this.RTBRecord.Location = new System.Drawing.Point(4, 7);
            this.RTBRecord.Name = "RTBRecord";
            this.RTBRecord.ReadOnly = true;
            this.RTBRecord.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RTBRecord.Size = new System.Drawing.Size(380, 256);
            this.RTBRecord.TabIndex = 1;
            this.RTBRecord.Text = "";
            this.RTBRecord.TextColor = CSS.IM.Library.ExtRichTextBox.RtfColor.Black;
            this.RTBRecord.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.RTBRecord_LinkClicked);
            // 
            // rtfSend
            // 
            this.rtfSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfSend.BackColor = System.Drawing.Color.White;
            this.rtfSend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtfSend.HiglightColor = CSS.IM.Library.ExtRichTextBox.RtfColor.White;
            this.rtfSend.Location = new System.Drawing.Point(4, 290);
            this.rtfSend.MaxLength = 1000;
            this.rtfSend.Name = "rtfSend";
            this.rtfSend.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtfSend.Size = new System.Drawing.Size(380, 92);
            this.rtfSend.TabIndex = 0;
            this.rtfSend.Text = "";
            this.rtfSend.TextColor = CSS.IM.Library.ExtRichTextBox.RtfColor.Black;
            this.rtfSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtfSend_KeyDown);
            // 
            // btn_send_key
            // 
            this.btn_send_key.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send_key.BackColor = System.Drawing.Color.Transparent;
            this.btn_send_key.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_send_key.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_send_key.Location = new System.Drawing.Point(590, 462);
            this.btn_send_key.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_send_key.Name = "btn_send_key";
            this.btn_send_key.Size = new System.Drawing.Size(21, 21);
            this.btn_send_key.TabIndex = 76;
            this.btn_send_key.Texts = "Button";
            this.btn_send_key.Click += new System.EventHandler(this.btn_send_key_Click);
            // 
            // btn_close
            // 
            this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_close.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_close.Location = new System.Drawing.Point(431, 462);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(69, 21);
            this.btn_close.TabIndex = 77;
            this.btn_close.Texts = "关闭";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_send
            // 
            this.btn_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send.BackColor = System.Drawing.Color.Transparent;
            this.btn_send.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_send.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_send.Location = new System.Drawing.Point(521, 462);
            this.btn_send.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(69, 21);
            this.btn_send.TabIndex = 75;
            this.btn_send.Texts = "发送";
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // Close_Check
            // 
            this.Close_Check.Tick += new System.EventHandler(this.Close_Check_Tick);
            // 
            // panel_function
            // 
            this.panel_function.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_function.BackColor = System.Drawing.Color.White;
            this.panel_function.Controls.Add(this.panel1);
            this.panel_function.Location = new System.Drawing.Point(399, 72);
            this.panel_function.Name = "panel_function";
            this.panel_function.Padding = new System.Windows.Forms.Padding(3);
            this.panel_function.Size = new System.Drawing.Size(220, 384);
            this.panel_function.TabIndex = 78;
            this.panel_function.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.fileTansfersContainer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 378);
            this.panel1.TabIndex = 0;
            // 
            // fileTansfersContainer
            // 
            this.fileTansfersContainer.AutoScroll = true;
            this.fileTansfersContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTansfersContainer.Location = new System.Drawing.Point(0, 0);
            this.fileTansfersContainer.Name = "fileTansfersContainer";
            this.fileTansfersContainer.Size = new System.Drawing.Size(214, 378);
            this.fileTansfersContainer.TabIndex = 6;
            this.fileTansfersContainer.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.fileTansfersContainer_ControlRemoved);
            // 
            // QQcm_send_key
            // 
            this.QQcm_send_key.BackColor = System.Drawing.Color.White;
            this.QQcm_send_key.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.QQtlm_key_enter,
            this.QQtlm_key_ctrl_enter});
            this.QQcm_send_key.Name = "QQcm_send_key";
            this.QQcm_send_key.Size = new System.Drawing.Size(164, 48);
            // 
            // QQtlm_key_enter
            // 
            this.QQtlm_key_enter.Name = "QQtlm_key_enter";
            this.QQtlm_key_enter.Size = new System.Drawing.Size(163, 22);
            this.QQtlm_key_enter.Text = "Enter 发送";
            this.QQtlm_key_enter.Click += new System.EventHandler(this.QQtlm_key_enter_Click);
            // 
            // QQtlm_key_ctrl_enter
            // 
            this.QQtlm_key_ctrl_enter.Name = "QQtlm_key_ctrl_enter";
            this.QQtlm_key_ctrl_enter.Size = new System.Drawing.Size(163, 22);
            this.QQtlm_key_ctrl_enter.Text = "Ctrl+Enter 发送";
            this.QQtlm_key_ctrl_enter.Click += new System.EventHandler(this.QQtlm_key_ctrl_enter_Click);
            // 
            // LB_sockUDP
            // 
            this.LB_sockUDP.Description = null;
            this.LB_sockUDP.IsAsync = false;
            this.LB_sockUDP.DataArrival += new CSS.IM.Library.Net.SockUDP.DataArrivalEventHandler(this.LB_sockUDP_DataArrival);
            // 
            // ChatFromMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(623, 490);
            this.Controls.Add(this.panel_function);
            this.Controls.Add(this.btn_send_key);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.panel_msg);
            this.Controls.Add(this.btn_remotedisktop);
            this.Controls.Add(this.btn_addfriend);
            this.Controls.Add(this.btn_more);
            this.Controls.Add(this.btn_filesend);
            this.Controls.Add(this.btn_videoOpen);
            this.Controls.Add(this.friendHead);
            this.MinimumSize = new System.Drawing.Size(405, 490);
            this.Name = "ChatFromMsg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatFromMsg";
            this.Activated += new System.EventHandler(this.ChatFromMsg_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatFromMsg_FormClosing);
            this.Load += new System.EventHandler(this.ChatFromMsg_Load);
            this.Controls.SetChildIndex(this.nikeName, 0);
            this.Controls.SetChildIndex(this.description, 0);
            this.Controls.SetChildIndex(this.friendHead, 0);
            this.Controls.SetChildIndex(this.btn_videoOpen, 0);
            this.Controls.SetChildIndex(this.btn_filesend, 0);
            this.Controls.SetChildIndex(this.btn_more, 0);
            this.Controls.SetChildIndex(this.btn_addfriend, 0);
            this.Controls.SetChildIndex(this.btn_remotedisktop, 0);
            this.Controls.SetChildIndex(this.panel_msg, 0);
            this.Controls.SetChildIndex(this.btn_send, 0);
            this.Controls.SetChildIndex(this.btn_close, 0);
            this.Controls.SetChildIndex(this.btn_send_key, 0);
            this.Controls.SetChildIndex(this.panel_function, 0);
            ((System.ComponentModel.ISupportInitialize)(this.friendHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_remotedisktop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_addfriend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_more)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_filesend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_videoOpen)).EndInit();
            this.panel_msg.ResumeLayout(false);
            this.toolBarBg.ResumeLayout(false);
            this.toolBarBg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_FontColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_font)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_screen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_zhen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_face)).EndInit();
            this.panel_function.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.QQcm_send_key.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CSS.IM.UI.Control.HeadPortrait friendHead;
        private CSS.IM.UI.Control.BarImageButton btn_remotedisktop;
        private CSS.IM.UI.Control.BarImageButton btn_addfriend;
        private CSS.IM.UI.Control.BarImageButton btn_more;
        private CSS.IM.UI.Control.BarImageButton btn_filesend;
        private CSS.IM.UI.Control.BarImageButton btn_videoOpen;
        private UI.Control.PanelQQText panel_msg;
        private CSS.IM.UI.Control.BarPanel toolBarBg;
        private CSS.IM.UI.Control.BarImageButton btn_FontColor;
        private CSS.IM.UI.Control.BarImageButton btn_font;
        private CSS.IM.UI.Control.BarImageButton btn_screen;
        private CSS.IM.UI.Control.BarImageButton btn_pic;
        private CSS.IM.UI.Control.BarImageButton btn_zhen;
        private CSS.IM.UI.Control.BarImageButton btn_face;
        private Library.ExtRichTextBox.MyExtRichTextBox RTBRecord;
        private Library.ExtRichTextBox.MyExtRichTextBox rtfSend;
        private Library.Net.SockUDP LB_sockUDP;
        private UI.Control.BasicButtonArrows btn_send_key;
        private UI.Control.BasicButton btn_close;
        private UI.Control.BasicButton btn_send;
        private System.Windows.Forms.Timer Close_Check;
        private UI.Control.PanelQQText panel_function;
        private UI.Control.QQContextMenu QQcm_send_key;
        private UI.Control.QQToolStripMenuItem QQtlm_key_enter;
        private UI.Control.QQToolStripMenuItem QQtlm_key_ctrl_enter;
        private System.Windows.Forms.Panel panel1;
        private UI.Control.Graphics.FileTransfersControl.FileTansfersContainer fileTansfersContainer;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}