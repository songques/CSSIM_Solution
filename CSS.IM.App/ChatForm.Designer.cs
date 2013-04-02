namespace CSS.IM.App
{
    partial class ChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.btn_close = new CSS.IM.UI.Control.BasicButton();
            this.btn_send = new CSS.IM.UI.Control.BasicButton();
            this.fontBtn = new System.Windows.Forms.PictureBox();
            this.faceBtn = new System.Windows.Forms.PictureBox();
            this.zhenBtn = new System.Windows.Forms.PictureBox();
            this.picBtn = new System.Windows.Forms.PictureBox();
            this.screenBtn = new System.Windows.Forms.PictureBox();
            this.friendHead = new System.Windows.Forms.PictureBox();
            this.toolBarBg = new System.Windows.Forms.Panel();
            this.butFontColor = new System.Windows.Forms.PictureBox();
            this.btn_videoOpen = new System.Windows.Forms.PictureBox();
            this.btn_filesend = new System.Windows.Forms.PictureBox();
            this.btn_filelist = new System.Windows.Forms.PictureBox();
            this.panel_msg = new CSS.IM.UI.Control.PanelQQText();
            this.RTBRecord = new CSS.IM.Library.ExtRichTextBox.MyExtRichTextBox();
            this.rtfSend = new CSS.IM.Library.ExtRichTextBox.MyExtRichTextBox();
            this.panel_accept_button = new System.Windows.Forms.Panel();
            this.btn_remote_shutdown = new CSS.IM.UI.Control.BasicButton();
            this.btn_remote_close = new CSS.IM.UI.Control.BasicButton();
            this.panel_receive_button = new System.Windows.Forms.Panel();
            this.btn_request_accept = new CSS.IM.UI.Control.BasicButton();
            this.btn_request_shutdown = new CSS.IM.UI.Control.BasicButton();
            this.panel_function = new CSS.IM.UI.Control.PanelQQText();
            this.panel_remote = new System.Windows.Forms.Panel();
            this.lab_remote_context = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_addfriend = new System.Windows.Forms.PictureBox();
            this.btn_remotedisktop = new System.Windows.Forms.PictureBox();
            this.Close_Check = new System.Windows.Forms.Timer(this.components);
            this.btn_send_key = new CSS.IM.UI.Control.BasicButtonArrows();
            this.sockUDP1 = new CSS.IM.Library.Net.SockUDP(this.components);
            this.QQcm_send_key = new CSS.IM.UI.Control.QQContextMenu();
            this.QQtlm_key_enter = new System.Windows.Forms.ToolStripMenuItem();
            this.QQtlm_key_ctrl_enter = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.fontBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zhenBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.friendHead)).BeginInit();
            this.toolBarBg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butFontColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_videoOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_filesend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_filelist)).BeginInit();
            this.panel_msg.SuspendLayout();
            this.panel_accept_button.SuspendLayout();
            this.panel_receive_button.SuspendLayout();
            this.panel_function.SuspendLayout();
            this.panel_remote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_addfriend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_remotedisktop)).BeginInit();
            this.QQcm_send_key.SuspendLayout();
            this.SuspendLayout();
            // 
            // description
            // 
            this.description.TabIndex = 5;
            // 
            // nikeName
            // 
            this.nikeName.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.nikeName.Location = new System.Drawing.Point(78, 11);
            this.nikeName.Size = new System.Drawing.Size(224, 19);
            this.nikeName.TabIndex = 4;
            this.nikeName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btn_close
            // 
            this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_close.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_close.Location = new System.Drawing.Point(406, 460);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(69, 21);
            this.btn_close.TabIndex = 3;
            this.btn_close.Texts = "关闭";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_send
            // 
            this.btn_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send.BackColor = System.Drawing.Color.Transparent;
            this.btn_send.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_send.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_send.Location = new System.Drawing.Point(496, 460);
            this.btn_send.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(69, 21);
            this.btn_send.TabIndex = 1;
            this.btn_send.Texts = "发送";
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // fontBtn
            // 
            this.fontBtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.fontBtn.BackColor = System.Drawing.Color.Transparent;
            this.fontBtn.Image = ((System.Drawing.Image)(resources.GetObject("fontBtn.Image")));
            this.fontBtn.Location = new System.Drawing.Point(5, 1);
            this.fontBtn.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.fontBtn.Name = "fontBtn";
            this.fontBtn.Size = new System.Drawing.Size(20, 20);
            this.fontBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.fontBtn.TabIndex = 50;
            this.fontBtn.TabStop = false;
            this.fontBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fontBtn_MouseClick);
            this.fontBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolBtn_MouseDown);
            this.fontBtn.MouseEnter += new System.EventHandler(this.ToolBtn_MouseEnter);
            this.fontBtn.MouseLeave += new System.EventHandler(this.ToolBtn_MouseLeave);
            // 
            // faceBtn
            // 
            this.faceBtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.faceBtn.BackColor = System.Drawing.Color.Transparent;
            this.faceBtn.Image = ((System.Drawing.Image)(resources.GetObject("faceBtn.Image")));
            this.faceBtn.Location = new System.Drawing.Point(59, 1);
            this.faceBtn.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.faceBtn.Name = "faceBtn";
            this.faceBtn.Size = new System.Drawing.Size(20, 20);
            this.faceBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.faceBtn.TabIndex = 51;
            this.faceBtn.TabStop = false;
            this.faceBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.faceBtn_MouseClick);
            this.faceBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolBtn_MouseDown);
            this.faceBtn.MouseEnter += new System.EventHandler(this.ToolBtn_MouseEnter);
            this.faceBtn.MouseLeave += new System.EventHandler(this.ToolBtn_MouseLeave);
            // 
            // zhenBtn
            // 
            this.zhenBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.zhenBtn.BackColor = System.Drawing.Color.Transparent;
            this.zhenBtn.Image = ((System.Drawing.Image)(resources.GetObject("zhenBtn.Image")));
            this.zhenBtn.Location = new System.Drawing.Point(142, 2);
            this.zhenBtn.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.zhenBtn.Name = "zhenBtn";
            this.zhenBtn.Size = new System.Drawing.Size(20, 20);
            this.zhenBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.zhenBtn.TabIndex = 52;
            this.zhenBtn.TabStop = false;
            this.zhenBtn.Visible = false;
            // 
            // picBtn
            // 
            this.picBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBtn.BackColor = System.Drawing.Color.Transparent;
            this.picBtn.Image = ((System.Drawing.Image)(resources.GetObject("picBtn.Image")));
            this.picBtn.Location = new System.Drawing.Point(168, 1);
            this.picBtn.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.picBtn.Name = "picBtn";
            this.picBtn.Size = new System.Drawing.Size(20, 20);
            this.picBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBtn.TabIndex = 53;
            this.picBtn.TabStop = false;
            this.picBtn.Visible = false;
            // 
            // screenBtn
            // 
            this.screenBtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.screenBtn.BackColor = System.Drawing.Color.Transparent;
            this.screenBtn.Image = ((System.Drawing.Image)(resources.GetObject("screenBtn.Image")));
            this.screenBtn.Location = new System.Drawing.Point(86, 1);
            this.screenBtn.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.screenBtn.Name = "screenBtn";
            this.screenBtn.Size = new System.Drawing.Size(20, 20);
            this.screenBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.screenBtn.TabIndex = 54;
            this.screenBtn.TabStop = false;
            this.screenBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.screenBtn_Click);
            this.screenBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolBtn_MouseDown);
            this.screenBtn.MouseEnter += new System.EventHandler(this.ToolBtn_MouseEnter);
            this.screenBtn.MouseLeave += new System.EventHandler(this.ToolBtn_MouseLeave);
            // 
            // friendHead
            // 
            this.friendHead.BackColor = System.Drawing.Color.Transparent;
            this.friendHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.friendHead.Cursor = System.Windows.Forms.Cursors.Hand;
            this.friendHead.Location = new System.Drawing.Point(8, 12);
            this.friendHead.Name = "friendHead";
            this.friendHead.Padding = new System.Windows.Forms.Padding(7);
            this.friendHead.Size = new System.Drawing.Size(64, 57);
            this.friendHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.friendHead.TabIndex = 57;
            this.friendHead.TabStop = false;
            this.friendHead.MouseEnter += new System.EventHandler(this.friendHead_MouseEnter);
            this.friendHead.MouseLeave += new System.EventHandler(this.ToolBtn_MouseLeave);
            // 
            // toolBarBg
            // 
            this.toolBarBg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.toolBarBg.Controls.Add(this.butFontColor);
            this.toolBarBg.Controls.Add(this.fontBtn);
            this.toolBarBg.Controls.Add(this.screenBtn);
            this.toolBarBg.Controls.Add(this.picBtn);
            this.toolBarBg.Controls.Add(this.zhenBtn);
            this.toolBarBg.Controls.Add(this.faceBtn);
            this.toolBarBg.Location = new System.Drawing.Point(0, 265);
            this.toolBarBg.Name = "toolBarBg";
            this.toolBarBg.Size = new System.Drawing.Size(391, 22);
            this.toolBarBg.TabIndex = 2;
            this.toolBarBg.Paint += new System.Windows.Forms.PaintEventHandler(this.toolBarBg_Paint);
            // 
            // butFontColor
            // 
            this.butFontColor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.butFontColor.BackColor = System.Drawing.Color.Transparent;
            this.butFontColor.Image = ((System.Drawing.Image)(resources.GetObject("butFontColor.Image")));
            this.butFontColor.Location = new System.Drawing.Point(32, 1);
            this.butFontColor.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.butFontColor.Name = "butFontColor";
            this.butFontColor.Size = new System.Drawing.Size(20, 20);
            this.butFontColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.butFontColor.TabIndex = 55;
            this.butFontColor.TabStop = false;
            this.butFontColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.butFontColor_Click);
            this.butFontColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolBtn_MouseDown);
            this.butFontColor.MouseEnter += new System.EventHandler(this.ToolBtn_MouseEnter);
            this.butFontColor.MouseLeave += new System.EventHandler(this.ToolBtn_MouseLeave);
            // 
            // btn_videoOpen
            // 
            this.btn_videoOpen.BackColor = System.Drawing.Color.Transparent;
            this.btn_videoOpen.Image = ((System.Drawing.Image)(resources.GetObject("btn_videoOpen.Image")));
            this.btn_videoOpen.Location = new System.Drawing.Point(78, 33);
            this.btn_videoOpen.Name = "btn_videoOpen";
            this.btn_videoOpen.Size = new System.Drawing.Size(35, 35);
            this.btn_videoOpen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_videoOpen.TabIndex = 64;
            this.btn_videoOpen.TabStop = false;
            this.btn_videoOpen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_videoOpen_MouseClick);
            this.btn_videoOpen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TB_ToolBtn_MouseDown);
            this.btn_videoOpen.MouseEnter += new System.EventHandler(this.TB_ToolBtn_MouseEnter);
            this.btn_videoOpen.MouseLeave += new System.EventHandler(this.TB_ToolBtn_MouseLeave);
            // 
            // btn_filesend
            // 
            this.btn_filesend.BackColor = System.Drawing.Color.Transparent;
            this.btn_filesend.Image = ((System.Drawing.Image)(resources.GetObject("btn_filesend.Image")));
            this.btn_filesend.Location = new System.Drawing.Point(119, 33);
            this.btn_filesend.Name = "btn_filesend";
            this.btn_filesend.Size = new System.Drawing.Size(35, 35);
            this.btn_filesend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_filesend.TabIndex = 65;
            this.btn_filesend.TabStop = false;
            this.btn_filesend.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_filesend_MouseClick);
            this.btn_filesend.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TB_ToolBtn_MouseDown);
            this.btn_filesend.MouseEnter += new System.EventHandler(this.TB_ToolBtn_MouseEnter);
            this.btn_filesend.MouseLeave += new System.EventHandler(this.TB_ToolBtn_MouseLeave);
            // 
            // btn_filelist
            // 
            this.btn_filelist.BackColor = System.Drawing.Color.Transparent;
            this.btn_filelist.Image = ((System.Drawing.Image)(resources.GetObject("btn_filelist.Image")));
            this.btn_filelist.Location = new System.Drawing.Point(160, 33);
            this.btn_filelist.Name = "btn_filelist";
            this.btn_filelist.Size = new System.Drawing.Size(35, 35);
            this.btn_filelist.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_filelist.TabIndex = 66;
            this.btn_filelist.TabStop = false;
            this.btn_filelist.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_filelist_MouseClick);
            this.btn_filelist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TB_ToolBtn_MouseDown);
            this.btn_filelist.MouseEnter += new System.EventHandler(this.TB_ToolBtn_MouseEnter);
            this.btn_filelist.MouseLeave += new System.EventHandler(this.TB_ToolBtn_MouseLeave);
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
            this.panel_msg.Size = new System.Drawing.Size(394, 385);
            this.panel_msg.TabIndex = 0;
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
            this.RTBRecord.Size = new System.Drawing.Size(386, 256);
            this.RTBRecord.TabIndex = 1;
            this.RTBRecord.Text = "";
            this.RTBRecord.TextColor = CSS.IM.Library.ExtRichTextBox.RtfColor.Black;
            this.RTBRecord.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.RTBRecord_LinkClicked);
            this.RTBRecord.DoubleClick += new System.EventHandler(this.RTBRecord_DoubleClick);
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
            this.rtfSend.Size = new System.Drawing.Size(386, 92);
            this.rtfSend.TabIndex = 0;
            this.rtfSend.Text = "";
            this.rtfSend.TextColor = CSS.IM.Library.ExtRichTextBox.RtfColor.Black;
            this.rtfSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtfSend_KeyDown);
            // 
            // panel_accept_button
            // 
            this.panel_accept_button.Controls.Add(this.btn_remote_shutdown);
            this.panel_accept_button.Controls.Add(this.btn_remote_close);
            this.panel_accept_button.Location = new System.Drawing.Point(6, 224);
            this.panel_accept_button.Name = "panel_accept_button";
            this.panel_accept_button.Size = new System.Drawing.Size(165, 30);
            this.panel_accept_button.TabIndex = 1;
            // 
            // btn_remote_shutdown
            // 
            this.btn_remote_shutdown.BackColor = System.Drawing.Color.Transparent;
            this.btn_remote_shutdown.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_remote_shutdown.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_remote_shutdown.Location = new System.Drawing.Point(90, 4);
            this.btn_remote_shutdown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_remote_shutdown.Name = "btn_remote_shutdown";
            this.btn_remote_shutdown.Size = new System.Drawing.Size(69, 21);
            this.btn_remote_shutdown.TabIndex = 1;
            this.btn_remote_shutdown.Texts = "取消";
            // 
            // btn_remote_close
            // 
            this.btn_remote_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_remote_close.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_remote_close.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_remote_close.Location = new System.Drawing.Point(8, 4);
            this.btn_remote_close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_remote_close.Name = "btn_remote_close";
            this.btn_remote_close.Size = new System.Drawing.Size(69, 21);
            this.btn_remote_close.TabIndex = 0;
            this.btn_remote_close.Texts = "挂断";
            this.btn_remote_close.Visible = false;
            // 
            // panel_receive_button
            // 
            this.panel_receive_button.Controls.Add(this.btn_request_accept);
            this.panel_receive_button.Controls.Add(this.btn_request_shutdown);
            this.panel_receive_button.Location = new System.Drawing.Point(6, 260);
            this.panel_receive_button.Name = "panel_receive_button";
            this.panel_receive_button.Size = new System.Drawing.Size(165, 30);
            this.panel_receive_button.TabIndex = 0;
            // 
            // btn_request_accept
            // 
            this.btn_request_accept.BackColor = System.Drawing.Color.Transparent;
            this.btn_request_accept.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_request_accept.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_request_accept.Location = new System.Drawing.Point(7, 5);
            this.btn_request_accept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_request_accept.Name = "btn_request_accept";
            this.btn_request_accept.Size = new System.Drawing.Size(69, 21);
            this.btn_request_accept.TabIndex = 1;
            this.btn_request_accept.Texts = "同意";
            // 
            // btn_request_shutdown
            // 
            this.btn_request_shutdown.BackColor = System.Drawing.Color.Transparent;
            this.btn_request_shutdown.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_request_shutdown.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_request_shutdown.Location = new System.Drawing.Point(89, 5);
            this.btn_request_shutdown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_request_shutdown.Name = "btn_request_shutdown";
            this.btn_request_shutdown.Size = new System.Drawing.Size(69, 21);
            this.btn_request_shutdown.TabIndex = 0;
            this.btn_request_shutdown.Texts = "拒绝";
            // 
            // panel_function
            // 
            this.panel_function.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_function.BackColor = System.Drawing.Color.White;
            this.panel_function.Controls.Add(this.panel_remote);
            this.panel_function.Location = new System.Drawing.Point(400, 72);
            this.panel_function.Name = "panel_function";
            this.panel_function.Size = new System.Drawing.Size(190, 384);
            this.panel_function.TabIndex = 6;
            this.panel_function.Visible = false;
            // 
            // panel_remote
            // 
            this.panel_remote.Controls.Add(this.panel_accept_button);
            this.panel_remote.Controls.Add(this.lab_remote_context);
            this.panel_remote.Controls.Add(this.panel_receive_button);
            this.panel_remote.Controls.Add(this.pictureBox1);
            this.panel_remote.Location = new System.Drawing.Point(7, 36);
            this.panel_remote.Name = "panel_remote";
            this.panel_remote.Size = new System.Drawing.Size(177, 321);
            this.panel_remote.TabIndex = 0;
            this.panel_remote.Visible = false;
            // 
            // lab_remote_context
            // 
            this.lab_remote_context.Location = new System.Drawing.Point(18, 183);
            this.lab_remote_context.Name = "lab_remote_context";
            this.lab_remote_context.Size = new System.Drawing.Size(142, 39);
            this.lab_remote_context.TabIndex = 2;
            this.lab_remote_context.Text = "您邀请使用远程协助。请等待回应……";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(20, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(140, 163);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btn_addfriend
            // 
            this.btn_addfriend.BackColor = System.Drawing.Color.Transparent;
            this.btn_addfriend.Image = ((System.Drawing.Image)(resources.GetObject("btn_addfriend.Image")));
            this.btn_addfriend.Location = new System.Drawing.Point(242, 33);
            this.btn_addfriend.Name = "btn_addfriend";
            this.btn_addfriend.Size = new System.Drawing.Size(35, 35);
            this.btn_addfriend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_addfriend.TabIndex = 67;
            this.btn_addfriend.TabStop = false;
            this.btn_addfriend.Visible = false;
            // 
            // btn_remotedisktop
            // 
            this.btn_remotedisktop.BackColor = System.Drawing.Color.Transparent;
            this.btn_remotedisktop.Image = ((System.Drawing.Image)(resources.GetObject("btn_remotedisktop.Image")));
            this.btn_remotedisktop.Location = new System.Drawing.Point(201, 33);
            this.btn_remotedisktop.Name = "btn_remotedisktop";
            this.btn_remotedisktop.Size = new System.Drawing.Size(35, 35);
            this.btn_remotedisktop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_remotedisktop.TabIndex = 68;
            this.btn_remotedisktop.TabStop = false;
            this.btn_remotedisktop.Visible = false;
            this.btn_remotedisktop.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_remotedisktop_MouseClick);
            this.btn_remotedisktop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TB_ToolBtn_MouseDown);
            this.btn_remotedisktop.MouseEnter += new System.EventHandler(this.TB_ToolBtn_MouseEnter);
            this.btn_remotedisktop.MouseLeave += new System.EventHandler(this.TB_ToolBtn_MouseLeave);
            // 
            // Close_Check
            // 
            this.Close_Check.Tick += new System.EventHandler(this.Close_Check_Tick);
            // 
            // btn_send_key
            // 
            this.btn_send_key.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send_key.BackColor = System.Drawing.Color.Transparent;
            this.btn_send_key.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_send_key.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_send_key.Location = new System.Drawing.Point(565, 460);
            this.btn_send_key.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_send_key.Name = "btn_send_key";
            this.btn_send_key.Size = new System.Drawing.Size(21, 21);
            this.btn_send_key.TabIndex = 2;
            this.btn_send_key.Texts = "Button";
            this.btn_send_key.Click += new System.EventHandler(this.btn_send_key_Click);
            // 
            // sockUDP1
            // 
            this.sockUDP1.Description = null;
            this.sockUDP1.IsAsync = false;
            this.sockUDP1.DataArrival += new CSS.IM.Library.Net.SockUDP.DataArrivalEventHandler(this.sockUDP1_DataArrival);
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
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(595, 490);
            this.Controls.Add(this.btn_remotedisktop);
            this.Controls.Add(this.btn_send_key);
            this.Controls.Add(this.panel_function);
            this.Controls.Add(this.btn_addfriend);
            this.Controls.Add(this.panel_msg);
            this.Controls.Add(this.btn_filelist);
            this.Controls.Add(this.btn_filesend);
            this.Controls.Add(this.btn_videoOpen);
            this.Controls.Add(this.friendHead);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_send);
            this.DoubleBuffered = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(405, 490);
            this.Name = "ChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.Controls.SetChildIndex(this.btn_send, 0);
            this.Controls.SetChildIndex(this.btn_close, 0);
            this.Controls.SetChildIndex(this.friendHead, 0);
            this.Controls.SetChildIndex(this.btn_videoOpen, 0);
            this.Controls.SetChildIndex(this.btn_filesend, 0);
            this.Controls.SetChildIndex(this.btn_filelist, 0);
            this.Controls.SetChildIndex(this.panel_msg, 0);
            this.Controls.SetChildIndex(this.btn_addfriend, 0);
            this.Controls.SetChildIndex(this.panel_function, 0);
            this.Controls.SetChildIndex(this.btn_send_key, 0);
            this.Controls.SetChildIndex(this.btn_remotedisktop, 0);
            this.Controls.SetChildIndex(this.nikeName, 0);
            this.Controls.SetChildIndex(this.description, 0);
            ((System.ComponentModel.ISupportInitialize)(this.fontBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zhenBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.friendHead)).EndInit();
            this.toolBarBg.ResumeLayout(false);
            this.toolBarBg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butFontColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_videoOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_filesend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_filelist)).EndInit();
            this.panel_msg.ResumeLayout(false);
            this.panel_accept_button.ResumeLayout(false);
            this.panel_receive_button.ResumeLayout(false);
            this.panel_function.ResumeLayout(false);
            this.panel_remote.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_addfriend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_remotedisktop)).EndInit();
            this.QQcm_send_key.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Control.BasicButton btn_close;
        private UI.Control.BasicButton btn_send;
        private System.Windows.Forms.PictureBox fontBtn;
        private System.Windows.Forms.PictureBox faceBtn;
        private System.Windows.Forms.PictureBox zhenBtn;
        private System.Windows.Forms.PictureBox picBtn;
        private System.Windows.Forms.PictureBox screenBtn;
        private System.Windows.Forms.PictureBox friendHead;
        private CSS.IM.Library.ExtRichTextBox.MyExtRichTextBox RTBRecord;
        private CSS.IM.Library.ExtRichTextBox.MyExtRichTextBox rtfSend;
        private System.Windows.Forms.Panel toolBarBg;
        private Library.Net.SockUDP sockUDP1;
        private System.Windows.Forms.PictureBox btn_videoOpen;
        private System.Windows.Forms.PictureBox btn_filesend;
        private System.Windows.Forms.PictureBox btn_filelist;
        private CSS.IM.UI.Control.PanelQQText panel_msg;
        private System.Windows.Forms.PictureBox butFontColor;
        private CSS.IM.UI.Control.PanelQQText panel_function;
        private System.Windows.Forms.PictureBox btn_addfriend;
        private UI.Control.BasicButton btn_remote_shutdown;
        private System.Windows.Forms.Label lab_remote_context;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel_remote;
        private UI.Control.BasicButton btn_request_shutdown;
        private UI.Control.BasicButton btn_request_accept;
        private UI.Control.BasicButton btn_remote_close;
        private System.Windows.Forms.Panel panel_accept_button;
        private System.Windows.Forms.Panel panel_receive_button;
        private System.Windows.Forms.PictureBox btn_remotedisktop;
        private System.Windows.Forms.Timer Close_Check;
        private UI.Control.BasicButtonArrows btn_send_key;
        private UI.Control.QQContextMenu QQcm_send_key;
        private System.Windows.Forms.ToolStripMenuItem QQtlm_key_enter;
        private System.Windows.Forms.ToolStripMenuItem QQtlm_key_ctrl_enter;
    }
}