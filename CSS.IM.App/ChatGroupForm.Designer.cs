using CSS.IM.UI.Control;
namespace CSS.IM.App
{
    partial class ChatGroupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatGroupForm));
            this.btn_close = new CSS.IM.UI.Control.BasicButton();
            this.btn_send = new CSS.IM.UI.Control.BasicButton();
            this.fontBtn = new System.Windows.Forms.PictureBox();
            this.faceBtn = new System.Windows.Forms.PictureBox();
            this.zhenBtn = new System.Windows.Forms.PictureBox();
            this.picBtn = new System.Windows.Forms.PictureBox();
            this.screenBtn = new System.Windows.Forms.PictureBox();
            this.friendHead = new System.Windows.Forms.PictureBox();
            this.toolBarBg = new System.Windows.Forms.Panel();
            this.btnSet = new System.Windows.Forms.PictureBox();
            this.butFontColor = new System.Windows.Forms.PictureBox();
            this.btn_videoOpen = new System.Windows.Forms.PictureBox();
            this.btn_filesend = new System.Windows.Forms.PictureBox();
            this.btn_filelist = new System.Windows.Forms.PictureBox();
            this.panel_msg = new CSS.IM.UI.Control.PanelQQText();
            this.RTBRecord = new CSS.IM.Library.ExtRichTextBox.MyExtRichTextBox();
            this.rtfSend = new CSS.IM.Library.ExtRichTextBox.MyExtRichTextBox();
            this.panle_friend = new CSS.IM.UI.Control.PanelQQText();
            this.friend_list = new CSS.IM.UI.Control.ChatGroupListView();
            this.btn_send_key = new CSS.IM.UI.Control.BasicButtonArrows();
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
            ((System.ComponentModel.ISupportInitialize)(this.btnSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butFontColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_videoOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_filesend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_filelist)).BeginInit();
            this.panel_msg.SuspendLayout();
            this.panle_friend.SuspendLayout();
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
            this.nikeName.Size = new System.Drawing.Size(401, 19);
            this.nikeName.TabIndex = 4;
            this.nikeName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btn_close
            // 
            this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_close.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_close.Location = new System.Drawing.Point(212, 461);
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
            this.btn_send.Location = new System.Drawing.Point(304, 461);
            this.btn_send.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(69, 21);
            this.btn_send.TabIndex = 6;
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
            this.screenBtn.Location = new System.Drawing.Point(135, 1);
            this.screenBtn.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.screenBtn.Name = "screenBtn";
            this.screenBtn.Size = new System.Drawing.Size(20, 20);
            this.screenBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.screenBtn.TabIndex = 54;
            this.screenBtn.TabStop = false;
            this.screenBtn.Visible = false;
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
            this.toolBarBg.Controls.Add(this.btnSet);
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
            // btnSet
            // 
            this.btnSet.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSet.BackColor = System.Drawing.Color.Transparent;
            this.btnSet.Image = ((System.Drawing.Image)(resources.GetObject("btnSet.Image")));
            this.btnSet.Location = new System.Drawing.Point(86, 1);
            this.btnSet.Margin = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(20, 20);
            this.btnSet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnSet.TabIndex = 56;
            this.btnSet.TabStop = false;
            this.btnSet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnSet_MouseClick);
            this.btnSet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolBtn_MouseDown);
            this.btnSet.MouseEnter += new System.EventHandler(this.ToolBtn_MouseEnter);
            this.btnSet.MouseLeave += new System.EventHandler(this.ToolBtn_MouseLeave);
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
            this.btn_videoOpen.Visible = false;
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
            this.btn_filesend.Visible = false;
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
            this.btn_filelist.Visible = false;
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
            // 
            // rtfSend
            // 
            this.rtfSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            // panle_friend
            // 
            this.panle_friend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panle_friend.BackColor = System.Drawing.SystemColors.Control;
            this.panle_friend.Controls.Add(this.friend_list);
            this.panle_friend.Location = new System.Drawing.Point(400, 72);
            this.panle_friend.Name = "panle_friend";
            this.panle_friend.Size = new System.Drawing.Size(169, 410);
            this.panle_friend.TabIndex = 1;
            // 
            // friend_list
            // 
            this.friend_list.BackColor = System.Drawing.Color.Transparent;
            this.friend_list.FriendKey = ((System.Collections.Generic.Dictionary<string, CSS.IM.XMPP.Jid>)(resources.GetObject("friend_list.FriendKey")));
            this.friend_list.Location = new System.Drawing.Point(6, 8);
            this.friend_list.Name = "friend_list";
            this.friend_list.OldSelectFriend = null;
            this.friend_list.SelectedFriend = null;
            this.friend_list.Size = new System.Drawing.Size(157, 87);
            this.friend_list.TabIndex = 0;
            this.friend_list.XmppConn = null;
            this.friend_list.friend_qcm_MouseClickEvent += new CSS.IM.UI.Control.ChatGroupListView.friend_qcm_MouseClick_Delegate(this.friend_list_friend_qcm_MouseClickEvent);
            // 
            // btn_send_key
            // 
            this.btn_send_key.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send_key.BackColor = System.Drawing.Color.Transparent;
            this.btn_send_key.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_send_key.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_send_key.Location = new System.Drawing.Point(373, 461);
            this.btn_send_key.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_send_key.Name = "btn_send_key";
            this.btn_send_key.Size = new System.Drawing.Size(21, 21);
            this.btn_send_key.TabIndex = 2;
            this.btn_send_key.Texts = "Button";
            this.btn_send_key.Click += new System.EventHandler(this.btn_send_key_Click);
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
            // ChatGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(573, 490);
            this.Controls.Add(this.btn_send_key);
            this.Controls.Add(this.panel_msg);
            this.Controls.Add(this.btn_filelist);
            this.Controls.Add(this.btn_filesend);
            this.Controls.Add(this.btn_videoOpen);
            this.Controls.Add(this.friendHead);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.panle_friend);
            this.Controls.Add(this.btn_send);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatGroupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatGroupForm";
            this.Load += new System.EventHandler(this.ChatGroupForm_Load);
            this.Controls.SetChildIndex(this.btn_send, 0);
            this.Controls.SetChildIndex(this.panle_friend, 0);
            this.Controls.SetChildIndex(this.btn_close, 0);
            this.Controls.SetChildIndex(this.friendHead, 0);
            this.Controls.SetChildIndex(this.btn_videoOpen, 0);
            this.Controls.SetChildIndex(this.btn_filesend, 0);
            this.Controls.SetChildIndex(this.btn_filelist, 0);
            this.Controls.SetChildIndex(this.panel_msg, 0);
            this.Controls.SetChildIndex(this.nikeName, 0);
            this.Controls.SetChildIndex(this.description, 0);
            this.Controls.SetChildIndex(this.btn_send_key, 0);
            ((System.ComponentModel.ISupportInitialize)(this.fontBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zhenBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.friendHead)).EndInit();
            this.toolBarBg.ResumeLayout(false);
            this.toolBarBg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butFontColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_videoOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_filesend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_filelist)).EndInit();
            this.panel_msg.ResumeLayout(false);
            this.panle_friend.ResumeLayout(false);
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
        private System.Windows.Forms.PictureBox btn_videoOpen;
        private System.Windows.Forms.PictureBox btn_filesend;
        private System.Windows.Forms.PictureBox btn_filelist;
        private PanelQQText panel_msg;
        private System.Windows.Forms.PictureBox butFontColor;
        private PanelQQText panle_friend;
        private UI.Control.ChatGroupListView friend_list;
        private System.Windows.Forms.PictureBox btnSet;
        private UI.Control.BasicButtonArrows btn_send_key;
        private UI.Control.QQContextMenu QQcm_send_key;
        private System.Windows.Forms.ToolStripMenuItem QQtlm_key_enter;
        private System.Windows.Forms.ToolStripMenuItem QQtlm_key_ctrl_enter;
    }
}