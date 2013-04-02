namespace CSS.IM.App
{
    partial class ChatGroupRoomSetForm
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
            this.btn_submit = new CSS.IM.UI.Control.BasicButton();
            this.btn_cancel = new CSS.IM.UI.Control.BasicButton();
            this.panel1 = new CSS.IM.UI.Control.SPanle();
            this.btn_add_roomowners = new CSS.IM.UI.Control.BasicButton();
            this.btn_add_roomadmins = new CSS.IM.UI.Control.BasicButton();
            this.cbb_roomconfig_whois = new CSS.IM.UI.Control.BasicComboBox();
            this.list_roomconfig_roomowners = new System.Windows.Forms.ListView();
            this.list_roomconfig_roomadmins = new System.Windows.Forms.ListView();
            this.lab08 = new System.Windows.Forms.Label();
            this.lab07 = new System.Windows.Forms.Label();
            this.chb_roomconfig_roomsecret = new CSS.IM.UI.Control.BasicTextBox();
            this.chb_roomconfig_canchangenick = new CSS.IM.UI.Control.BasicCheckBox();
            this.chb_roomconfig_registration = new CSS.IM.UI.Control.BasicCheckBox();
            this.chb_roomconfig_reservednick = new CSS.IM.UI.Control.BasicCheckBox();
            this.chb_roomconfig_enablelogging = new CSS.IM.UI.Control.BasicCheckBox();
            this.chb_roomconfig_membersonly = new CSS.IM.UI.Control.BasicCheckBox();
            this.chb_roomconfig_passwordprotectedroom = new CSS.IM.UI.Control.BasicCheckBox();
            this.chb_roomconfig_allowinvites = new CSS.IM.UI.Control.BasicCheckBox();
            this.chb_roomconfig_persistentroom = new CSS.IM.UI.Control.BasicCheckBox();
            this.chb_roomconfig_moderatedroom = new CSS.IM.UI.Control.BasicCheckBox();
            this.chb_roomconfig_publicroom = new CSS.IM.UI.Control.BasicCheckBox();
            this.txt_roomconfig_roomdesc = new CSS.IM.UI.Control.BasicTextBox();
            this.cbb_roomconfig_maxusers = new CSS.IM.UI.Control.BasicComboBox();
            this.chb_roomconfig_changesubject = new CSS.IM.UI.Control.BasicCheckBox();
            this.txt_roomconfig_roomname = new CSS.IM.UI.Control.BasicTextBox();
            this.lab06 = new System.Windows.Forms.Label();
            this.lab05 = new System.Windows.Forms.Label();
            this.lab04 = new System.Windows.Forms.Label();
            this.lab03 = new System.Windows.Forms.Label();
            this.lab02 = new System.Windows.Forms.Label();
            this.lab01 = new System.Windows.Forms.Label();
            this.group_roomconfig_presencebroadcast = new CSS.IM.UI.Control.SPanle();
            this.chb_visitor = new CSS.IM.UI.Control.BasicCheckBox();
            this.chb_participant = new CSS.IM.UI.Control.BasicCheckBox();
            this.chb_moderator = new CSS.IM.UI.Control.BasicCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).BeginInit();
            this.panel1.SuspendLayout();
            this.group_roomconfig_presencebroadcast.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(415, 0);
            // 
            // btn_submit
            // 
            this.btn_submit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_submit.BackColor = System.Drawing.Color.Transparent;
            this.btn_submit.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_submit.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_submit.Location = new System.Drawing.Point(102, 435);
            this.btn_submit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(69, 21);
            this.btn_submit.TabIndex = 24;
            this.btn_submit.Texts = "更新";
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_cancel.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_cancel.Location = new System.Drawing.Point(260, 436);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(69, 21);
            this.btn_cancel.TabIndex = 25;
            this.btn_cancel.Texts = "取消";
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btn_add_roomowners);
            this.panel1.Controls.Add(this.btn_add_roomadmins);
            this.panel1.Controls.Add(this.cbb_roomconfig_whois);
            this.panel1.Controls.Add(this.list_roomconfig_roomowners);
            this.panel1.Controls.Add(this.list_roomconfig_roomadmins);
            this.panel1.Controls.Add(this.lab08);
            this.panel1.Controls.Add(this.lab07);
            this.panel1.Controls.Add(this.chb_roomconfig_roomsecret);
            this.panel1.Controls.Add(this.chb_roomconfig_canchangenick);
            this.panel1.Controls.Add(this.chb_roomconfig_registration);
            this.panel1.Controls.Add(this.chb_roomconfig_reservednick);
            this.panel1.Controls.Add(this.chb_roomconfig_enablelogging);
            this.panel1.Controls.Add(this.chb_roomconfig_membersonly);
            this.panel1.Controls.Add(this.chb_roomconfig_passwordprotectedroom);
            this.panel1.Controls.Add(this.chb_roomconfig_allowinvites);
            this.panel1.Controls.Add(this.chb_roomconfig_persistentroom);
            this.panel1.Controls.Add(this.chb_roomconfig_moderatedroom);
            this.panel1.Controls.Add(this.chb_roomconfig_publicroom);
            this.panel1.Controls.Add(this.txt_roomconfig_roomdesc);
            this.panel1.Controls.Add(this.cbb_roomconfig_maxusers);
            this.panel1.Controls.Add(this.chb_roomconfig_changesubject);
            this.panel1.Controls.Add(this.txt_roomconfig_roomname);
            this.panel1.Controls.Add(this.lab06);
            this.panel1.Controls.Add(this.lab05);
            this.panel1.Controls.Add(this.lab04);
            this.panel1.Controls.Add(this.lab03);
            this.panel1.Controls.Add(this.lab02);
            this.panel1.Controls.Add(this.lab01);
            this.panel1.Controls.Add(this.group_roomconfig_presencebroadcast);
            this.panel1.Location = new System.Drawing.Point(12, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 385);
            this.panel1.TabIndex = 26;
            this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            // 
            // btn_add_roomowners
            // 
            this.btn_add_roomowners.BackColor = System.Drawing.Color.Transparent;
            this.btn_add_roomowners.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_add_roomowners.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_add_roomowners.Location = new System.Drawing.Point(112, 562);
            this.btn_add_roomowners.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_add_roomowners.Name = "btn_add_roomowners";
            this.btn_add_roomowners.Size = new System.Drawing.Size(69, 21);
            this.btn_add_roomowners.TabIndex = 73;
            this.btn_add_roomowners.Texts = "添加";
            this.btn_add_roomowners.Click += new System.EventHandler(this.btn_add_roomowners_Click);
            // 
            // btn_add_roomadmins
            // 
            this.btn_add_roomadmins.BackColor = System.Drawing.Color.Transparent;
            this.btn_add_roomadmins.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_add_roomadmins.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_add_roomadmins.Location = new System.Drawing.Point(112, 480);
            this.btn_add_roomadmins.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_add_roomadmins.Name = "btn_add_roomadmins";
            this.btn_add_roomadmins.Size = new System.Drawing.Size(69, 21);
            this.btn_add_roomadmins.TabIndex = 72;
            this.btn_add_roomadmins.Texts = "添加";
            this.btn_add_roomadmins.Click += new System.EventHandler(this.btn_add_roomadmins_Click);
            // 
            // cbb_roomconfig_whois
            // 
            this.cbb_roomconfig_whois.BackColor = System.Drawing.Color.White;
            this.cbb_roomconfig_whois.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_roomconfig_whois.Items = null;
            this.cbb_roomconfig_whois.Location = new System.Drawing.Point(190, 347);
            this.cbb_roomconfig_whois.Name = "cbb_roomconfig_whois";
            this.cbb_roomconfig_whois.SelectIndex = 0;
            this.cbb_roomconfig_whois.SelectItem = null;
            this.cbb_roomconfig_whois.SelectText = null;
            this.cbb_roomconfig_whois.Size = new System.Drawing.Size(200, 22);
            this.cbb_roomconfig_whois.TabIndex = 71;
            this.cbb_roomconfig_whois.Texts = null;
            // 
            // list_roomconfig_roomowners
            // 
            this.list_roomconfig_roomowners.Location = new System.Drawing.Point(190, 533);
            this.list_roomconfig_roomowners.Name = "list_roomconfig_roomowners";
            this.list_roomconfig_roomowners.Size = new System.Drawing.Size(200, 80);
            this.list_roomconfig_roomowners.TabIndex = 70;
            this.list_roomconfig_roomowners.UseCompatibleStateImageBehavior = false;
            this.list_roomconfig_roomowners.View = System.Windows.Forms.View.List;
            this.list_roomconfig_roomowners.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.list_roomconfig_roomowners_MouseDoubleClick);
            // 
            // list_roomconfig_roomadmins
            // 
            this.list_roomconfig_roomadmins.FullRowSelect = true;
            this.list_roomconfig_roomadmins.GridLines = true;
            this.list_roomconfig_roomadmins.Location = new System.Drawing.Point(190, 450);
            this.list_roomconfig_roomadmins.Name = "list_roomconfig_roomadmins";
            this.list_roomconfig_roomadmins.Size = new System.Drawing.Size(200, 80);
            this.list_roomconfig_roomadmins.TabIndex = 69;
            this.list_roomconfig_roomadmins.UseCompatibleStateImageBehavior = false;
            this.list_roomconfig_roomadmins.View = System.Windows.Forms.View.List;
            this.list_roomconfig_roomadmins.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.list_roomconfig_roomadmins_MouseDoubleClick);
            // 
            // lab08
            // 
            this.lab08.AutoSize = true;
            this.lab08.BackColor = System.Drawing.Color.Transparent;
            this.lab08.Location = new System.Drawing.Point(116, 536);
            this.lab08.Name = "lab08";
            this.lab08.Size = new System.Drawing.Size(65, 12);
            this.lab08.TabIndex = 68;
            this.lab08.Text = "房间拥有者";
            // 
            // lab07
            // 
            this.lab07.AutoSize = true;
            this.lab07.BackColor = System.Drawing.Color.Transparent;
            this.lab07.Location = new System.Drawing.Point(116, 451);
            this.lab07.Name = "lab07";
            this.lab07.Size = new System.Drawing.Size(65, 12);
            this.lab07.TabIndex = 67;
            this.lab07.Text = "房间管理员";
            // 
            // chb_roomconfig_roomsecret
            // 
            this.chb_roomconfig_roomsecret.BackColor = System.Drawing.Color.Transparent;
            this.chb_roomconfig_roomsecret.IsPass = true;
            this.chb_roomconfig_roomsecret.Location = new System.Drawing.Point(190, 324);
            this.chb_roomconfig_roomsecret.MaxLength = 32767;
            this.chb_roomconfig_roomsecret.Multi = false;
            this.chb_roomconfig_roomsecret.Name = "chb_roomconfig_roomsecret";
            this.chb_roomconfig_roomsecret.ReadOn = false;
            this.chb_roomconfig_roomsecret.Size = new System.Drawing.Size(200, 23);
            this.chb_roomconfig_roomsecret.TabIndex = 65;
            this.chb_roomconfig_roomsecret.Texts = "";
            // 
            // chb_roomconfig_canchangenick
            // 
            this.chb_roomconfig_canchangenick.BackColor = System.Drawing.Color.Transparent;
            this.chb_roomconfig_canchangenick.Checked = false;
            this.chb_roomconfig_canchangenick.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_roomconfig_canchangenick.Location = new System.Drawing.Point(190, 410);
            this.chb_roomconfig_canchangenick.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_roomconfig_canchangenick.Name = "chb_roomconfig_canchangenick";
            this.chb_roomconfig_canchangenick.Size = new System.Drawing.Size(134, 20);
            this.chb_roomconfig_canchangenick.TabIndex = 64;
            this.chb_roomconfig_canchangenick.Texts = "允许使用者修改昵称";
            // 
            // chb_roomconfig_registration
            // 
            this.chb_roomconfig_registration.BackColor = System.Drawing.Color.Transparent;
            this.chb_roomconfig_registration.Checked = false;
            this.chb_roomconfig_registration.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_roomconfig_registration.Location = new System.Drawing.Point(190, 430);
            this.chb_roomconfig_registration.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_roomconfig_registration.Name = "chb_roomconfig_registration";
            this.chb_roomconfig_registration.Size = new System.Drawing.Size(127, 20);
            this.chb_roomconfig_registration.TabIndex = 63;
            this.chb_roomconfig_registration.Texts = "允许用户注册房间";
            // 
            // chb_roomconfig_reservednick
            // 
            this.chb_roomconfig_reservednick.BackColor = System.Drawing.Color.Transparent;
            this.chb_roomconfig_reservednick.Checked = false;
            this.chb_roomconfig_reservednick.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_roomconfig_reservednick.Location = new System.Drawing.Point(190, 390);
            this.chb_roomconfig_reservednick.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_roomconfig_reservednick.Name = "chb_roomconfig_reservednick";
            this.chb_roomconfig_reservednick.Size = new System.Drawing.Size(153, 20);
            this.chb_roomconfig_reservednick.TabIndex = 62;
            this.chb_roomconfig_reservednick.Texts = "公允许注册的昵称登录";
            // 
            // chb_roomconfig_enablelogging
            // 
            this.chb_roomconfig_enablelogging.BackColor = System.Drawing.Color.Transparent;
            this.chb_roomconfig_enablelogging.Checked = false;
            this.chb_roomconfig_enablelogging.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_roomconfig_enablelogging.Location = new System.Drawing.Point(190, 370);
            this.chb_roomconfig_enablelogging.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_roomconfig_enablelogging.Name = "chb_roomconfig_enablelogging";
            this.chb_roomconfig_enablelogging.Size = new System.Drawing.Size(95, 20);
            this.chb_roomconfig_enablelogging.TabIndex = 61;
            this.chb_roomconfig_enablelogging.Texts = "登录房间对话";
            // 
            // chb_roomconfig_membersonly
            // 
            this.chb_roomconfig_membersonly.AutoScroll = true;
            this.chb_roomconfig_membersonly.BackColor = System.Drawing.Color.Transparent;
            this.chb_roomconfig_membersonly.Checked = false;
            this.chb_roomconfig_membersonly.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_roomconfig_membersonly.Location = new System.Drawing.Point(190, 264);
            this.chb_roomconfig_membersonly.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_roomconfig_membersonly.Name = "chb_roomconfig_membersonly";
            this.chb_roomconfig_membersonly.Size = new System.Drawing.Size(127, 20);
            this.chb_roomconfig_membersonly.TabIndex = 59;
            this.chb_roomconfig_membersonly.Texts = "房间公对成员开放";
            // 
            // chb_roomconfig_passwordprotectedroom
            // 
            this.chb_roomconfig_passwordprotectedroom.BackColor = System.Drawing.Color.Transparent;
            this.chb_roomconfig_passwordprotectedroom.Checked = false;
            this.chb_roomconfig_passwordprotectedroom.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_roomconfig_passwordprotectedroom.Location = new System.Drawing.Point(190, 304);
            this.chb_roomconfig_passwordprotectedroom.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_roomconfig_passwordprotectedroom.Name = "chb_roomconfig_passwordprotectedroom";
            this.chb_roomconfig_passwordprotectedroom.Size = new System.Drawing.Size(153, 20);
            this.chb_roomconfig_passwordprotectedroom.TabIndex = 58;
            this.chb_roomconfig_passwordprotectedroom.Texts = "需要密码才能进入房间";
            // 
            // chb_roomconfig_allowinvites
            // 
            this.chb_roomconfig_allowinvites.BackColor = System.Drawing.Color.Transparent;
            this.chb_roomconfig_allowinvites.Checked = false;
            this.chb_roomconfig_allowinvites.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_roomconfig_allowinvites.Location = new System.Drawing.Point(190, 284);
            this.chb_roomconfig_allowinvites.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_roomconfig_allowinvites.Name = "chb_roomconfig_allowinvites";
            this.chb_roomconfig_allowinvites.Size = new System.Drawing.Size(153, 20);
            this.chb_roomconfig_allowinvites.TabIndex = 57;
            this.chb_roomconfig_allowinvites.Texts = "允许占有者邀请其他人";
            // 
            // chb_roomconfig_persistentroom
            // 
            this.chb_roomconfig_persistentroom.BackColor = System.Drawing.Color.Transparent;
            this.chb_roomconfig_persistentroom.Checked = false;
            this.chb_roomconfig_persistentroom.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_roomconfig_persistentroom.Location = new System.Drawing.Point(190, 224);
            this.chb_roomconfig_persistentroom.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_roomconfig_persistentroom.Name = "chb_roomconfig_persistentroom";
            this.chb_roomconfig_persistentroom.Size = new System.Drawing.Size(95, 20);
            this.chb_roomconfig_persistentroom.TabIndex = 56;
            this.chb_roomconfig_persistentroom.Texts = "房间是持久的";
            // 
            // chb_roomconfig_moderatedroom
            // 
            this.chb_roomconfig_moderatedroom.BackColor = System.Drawing.Color.Transparent;
            this.chb_roomconfig_moderatedroom.Checked = false;
            this.chb_roomconfig_moderatedroom.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_roomconfig_moderatedroom.Location = new System.Drawing.Point(190, 244);
            this.chb_roomconfig_moderatedroom.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_roomconfig_moderatedroom.Name = "chb_roomconfig_moderatedroom";
            this.chb_roomconfig_moderatedroom.Size = new System.Drawing.Size(95, 20);
            this.chb_roomconfig_moderatedroom.TabIndex = 55;
            this.chb_roomconfig_moderatedroom.Texts = "房间是适度的";
            // 
            // chb_roomconfig_publicroom
            // 
            this.chb_roomconfig_publicroom.BackColor = System.Drawing.Color.Transparent;
            this.chb_roomconfig_publicroom.Checked = false;
            this.chb_roomconfig_publicroom.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_roomconfig_publicroom.Location = new System.Drawing.Point(190, 204);
            this.chb_roomconfig_publicroom.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_roomconfig_publicroom.Name = "chb_roomconfig_publicroom";
            this.chb_roomconfig_publicroom.Size = new System.Drawing.Size(127, 20);
            this.chb_roomconfig_publicroom.TabIndex = 54;
            this.chb_roomconfig_publicroom.Texts = "列出目录中的房间";
            // 
            // txt_roomconfig_roomdesc
            // 
            this.txt_roomconfig_roomdesc.BackColor = System.Drawing.Color.Transparent;
            this.txt_roomconfig_roomdesc.IsPass = false;
            this.txt_roomconfig_roomdesc.Location = new System.Drawing.Point(190, 37);
            this.txt_roomconfig_roomdesc.MaxLength = 32767;
            this.txt_roomconfig_roomdesc.Multi = false;
            this.txt_roomconfig_roomdesc.Name = "txt_roomconfig_roomdesc";
            this.txt_roomconfig_roomdesc.ReadOn = false;
            this.txt_roomconfig_roomdesc.Size = new System.Drawing.Size(200, 23);
            this.txt_roomconfig_roomdesc.TabIndex = 53;
            this.txt_roomconfig_roomdesc.Texts = "";
            // 
            // cbb_roomconfig_maxusers
            // 
            this.cbb_roomconfig_maxusers.BackColor = System.Drawing.Color.White;
            this.cbb_roomconfig_maxusers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_roomconfig_maxusers.Items = null;
            this.cbb_roomconfig_maxusers.Location = new System.Drawing.Point(190, 80);
            this.cbb_roomconfig_maxusers.Name = "cbb_roomconfig_maxusers";
            this.cbb_roomconfig_maxusers.SelectIndex = 0;
            this.cbb_roomconfig_maxusers.SelectItem = null;
            this.cbb_roomconfig_maxusers.SelectText = null;
            this.cbb_roomconfig_maxusers.Size = new System.Drawing.Size(200, 22);
            this.cbb_roomconfig_maxusers.TabIndex = 50;
            this.cbb_roomconfig_maxusers.Texts = null;
            // 
            // chb_roomconfig_changesubject
            // 
            this.chb_roomconfig_changesubject.BackColor = System.Drawing.Color.Transparent;
            this.chb_roomconfig_changesubject.Checked = false;
            this.chb_roomconfig_changesubject.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_roomconfig_changesubject.Location = new System.Drawing.Point(190, 60);
            this.chb_roomconfig_changesubject.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_roomconfig_changesubject.Name = "chb_roomconfig_changesubject";
            this.chb_roomconfig_changesubject.Size = new System.Drawing.Size(146, 20);
            this.chb_roomconfig_changesubject.TabIndex = 49;
            this.chb_roomconfig_changesubject.Texts = "允许占有者改更主题";
            // 
            // txt_roomconfig_roomname
            // 
            this.txt_roomconfig_roomname.BackColor = System.Drawing.Color.Transparent;
            this.txt_roomconfig_roomname.IsPass = false;
            this.txt_roomconfig_roomname.Location = new System.Drawing.Point(190, 14);
            this.txt_roomconfig_roomname.MaxLength = 32767;
            this.txt_roomconfig_roomname.Multi = false;
            this.txt_roomconfig_roomname.Name = "txt_roomconfig_roomname";
            this.txt_roomconfig_roomname.ReadOn = false;
            this.txt_roomconfig_roomname.Size = new System.Drawing.Size(200, 23);
            this.txt_roomconfig_roomname.TabIndex = 52;
            this.txt_roomconfig_roomname.Texts = "";
            // 
            // lab06
            // 
            this.lab06.AutoSize = true;
            this.lab06.BackColor = System.Drawing.Color.Transparent;
            this.lab06.Location = new System.Drawing.Point(14, 353);
            this.lab06.Name = "lab06";
            this.lab06.Size = new System.Drawing.Size(167, 12);
            this.lab06.TabIndex = 48;
            this.lab06.Text = "能够发现占有者真实的JID角色";
            // 
            // lab05
            // 
            this.lab05.AutoSize = true;
            this.lab05.BackColor = System.Drawing.Color.Transparent;
            this.lab05.Location = new System.Drawing.Point(152, 330);
            this.lab05.Name = "lab05";
            this.lab05.Size = new System.Drawing.Size(29, 12);
            this.lab05.TabIndex = 47;
            this.lab05.Text = "密码";
            // 
            // lab04
            // 
            this.lab04.AutoSize = true;
            this.lab04.BackColor = System.Drawing.Color.Transparent;
            this.lab04.Location = new System.Drawing.Point(14, 109);
            this.lab04.Name = "lab04";
            this.lab04.Size = new System.Drawing.Size(167, 12);
            this.lab04.TabIndex = 46;
            this.lab04.Text = "其Presence是Broadcase的角色";
            // 
            // lab03
            // 
            this.lab03.AutoSize = true;
            this.lab03.BackColor = System.Drawing.Color.Transparent;
            this.lab03.Location = new System.Drawing.Point(80, 85);
            this.lab03.Name = "lab03";
            this.lab03.Size = new System.Drawing.Size(101, 12);
            this.lab03.TabIndex = 45;
            this.lab03.Text = "最大房间占有人数";
            // 
            // lab02
            // 
            this.lab02.AutoSize = true;
            this.lab02.BackColor = System.Drawing.Color.Transparent;
            this.lab02.Location = new System.Drawing.Point(152, 44);
            this.lab02.Name = "lab02";
            this.lab02.Size = new System.Drawing.Size(29, 12);
            this.lab02.TabIndex = 44;
            this.lab02.Text = "描述";
            // 
            // lab01
            // 
            this.lab01.AutoSize = true;
            this.lab01.BackColor = System.Drawing.Color.Transparent;
            this.lab01.Location = new System.Drawing.Point(128, 18);
            this.lab01.Name = "lab01";
            this.lab01.Size = new System.Drawing.Size(53, 12);
            this.lab01.TabIndex = 43;
            this.lab01.Text = "房间名称";
            // 
            // group_roomconfig_presencebroadcast
            // 
            this.group_roomconfig_presencebroadcast.BackColor = System.Drawing.Color.Transparent;
            this.group_roomconfig_presencebroadcast.Controls.Add(this.chb_visitor);
            this.group_roomconfig_presencebroadcast.Controls.Add(this.chb_participant);
            this.group_roomconfig_presencebroadcast.Controls.Add(this.chb_moderator);
            this.group_roomconfig_presencebroadcast.Location = new System.Drawing.Point(190, 106);
            this.group_roomconfig_presencebroadcast.Name = "group_roomconfig_presencebroadcast";
            this.group_roomconfig_presencebroadcast.Size = new System.Drawing.Size(200, 91);
            this.group_roomconfig_presencebroadcast.TabIndex = 51;
            this.group_roomconfig_presencebroadcast.TabStop = false;
            // 
            // chb_visitor
            // 
            this.chb_visitor.BackColor = System.Drawing.Color.Transparent;
            this.chb_visitor.Checked = false;
            this.chb_visitor.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_visitor.Location = new System.Drawing.Point(20, 66);
            this.chb_visitor.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_visitor.Name = "chb_visitor";
            this.chb_visitor.Size = new System.Drawing.Size(95, 20);
            this.chb_visitor.TabIndex = 35;
            this.chb_visitor.Tag = "visitor";
            this.chb_visitor.Texts = "访客";
            this.chb_visitor.Visible = false;
            // 
            // chb_participant
            // 
            this.chb_participant.BackColor = System.Drawing.Color.Transparent;
            this.chb_participant.Checked = false;
            this.chb_participant.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_participant.Location = new System.Drawing.Point(20, 40);
            this.chb_participant.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_participant.Name = "chb_participant";
            this.chb_participant.Size = new System.Drawing.Size(95, 20);
            this.chb_participant.TabIndex = 34;
            this.chb_participant.Tag = "participant";
            this.chb_participant.Texts = "参与者";
            this.chb_participant.Visible = false;
            // 
            // chb_moderator
            // 
            this.chb_moderator.BackColor = System.Drawing.Color.Transparent;
            this.chb_moderator.Checked = false;
            this.chb_moderator.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            this.chb_moderator.Location = new System.Drawing.Point(20, 14);
            this.chb_moderator.MinimumSize = new System.Drawing.Size(15, 15);
            this.chb_moderator.Name = "chb_moderator";
            this.chb_moderator.Size = new System.Drawing.Size(95, 20);
            this.chb_moderator.TabIndex = 33;
            this.chb_moderator.Tag = "moderator";
            this.chb_moderator.Texts = "主持者";
            this.chb_moderator.Visible = false;
            // 
            // ChatGroupRoomSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 463);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChatGroupRoomSetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "聊天室设置";
            this.Load += new System.EventHandler(this.ChatGroupRoomSetForm_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btn_cancel, 0);
            this.Controls.SetChildIndex(this.btn_submit, 0);
            this.Controls.SetChildIndex(this.ButtonClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonClose)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.group_roomconfig_presencebroadcast.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Control.BasicButton btn_submit;
        private UI.Control.BasicButton btn_cancel;
        private CSS.IM.UI.Control.SPanle panel1;
        private UI.Control.BasicCheckBox chb_roomconfig_membersonly;
        private UI.Control.BasicCheckBox chb_roomconfig_passwordprotectedroom;
        private UI.Control.BasicCheckBox chb_roomconfig_allowinvites;
        private UI.Control.BasicCheckBox chb_roomconfig_persistentroom;
        private UI.Control.BasicCheckBox chb_roomconfig_moderatedroom;
        private UI.Control.BasicCheckBox chb_roomconfig_publicroom;
        private UI.Control.BasicTextBox txt_roomconfig_roomdesc;
        private CSS.IM.UI.Control.SPanle group_roomconfig_presencebroadcast;
        private UI.Control.BasicCheckBox chb_visitor;
        private UI.Control.BasicCheckBox chb_participant;
        private UI.Control.BasicCheckBox chb_moderator;
        private UI.Control.BasicComboBox cbb_roomconfig_maxusers;
        private UI.Control.BasicCheckBox chb_roomconfig_changesubject;
        private UI.Control.BasicTextBox txt_roomconfig_roomname;
        private System.Windows.Forms.Label lab06;
        private System.Windows.Forms.Label lab05;
        private System.Windows.Forms.Label lab04;
        private System.Windows.Forms.Label lab03;
        private System.Windows.Forms.Label lab02;
        private System.Windows.Forms.Label lab01;
        private UI.Control.BasicCheckBox chb_roomconfig_canchangenick;
        private UI.Control.BasicCheckBox chb_roomconfig_registration;
        private UI.Control.BasicCheckBox chb_roomconfig_reservednick;
        private UI.Control.BasicCheckBox chb_roomconfig_enablelogging;
        private System.Windows.Forms.Label lab08;
        private System.Windows.Forms.Label lab07;
        private UI.Control.BasicTextBox chb_roomconfig_roomsecret;
        private System.Windows.Forms.ListView list_roomconfig_roomowners;
        private System.Windows.Forms.ListView list_roomconfig_roomadmins;
        private UI.Control.BasicComboBox cbb_roomconfig_whois;
        private UI.Control.BasicButton btn_add_roomowners;
        private UI.Control.BasicButton btn_add_roomadmins;
    }
}