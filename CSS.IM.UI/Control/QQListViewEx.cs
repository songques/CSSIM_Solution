using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Entity;
using CSS.IM.UI.Util;
using System.Diagnostics;
using System.Collections;
using System.Threading;
using CSS.IM.XMPP;
using CSS.IM.UI.Form;
using CSS.IM.XMPP.protocol.client;

namespace CSS.IM.UI.Control
{
    public partial class QQListViewEx : UserControl
    {
        private int groupIndex = 50;//分组自动增长ID
        private int friendIndex = 50;//好友自动增长ID

        private Friend _SelectFriend;
        public Friend SelectFriend
        {
            get
            {
                return _SelectFriend;
            }
            set
            {
                if (_SelectFriend != null)
                    OldSelectFriend = _SelectFriend;
                _SelectFriend = value;
                UpdateFriendControl();
            }
        }

        public Friend OldSelectFriend { get; set; }
        private GroupControl ClickGroup { set; get; }

        private int ItemHeight = 55;
        /// <summary>
        /// 头像大小
        /// </summary>
        FriendContainerType _FCType = FriendContainerType.Big;
        public FriendContainerType FCType
        {
            get { return _FCType; }
            set
            {
                _FCType = value;
                if (value == FriendContainerType.Big)
                {
                    ItemHeight = 55;
                }
                else
                {
                    ItemHeight = 35;
                }

            }
        }

        private XmppClientConnection XmppConn;
        [Description("XMPP连接"), Category("Appearance")]
        public XmppClientConnection XmppConnection
        {
            get{return XmppConn;}
            set{XmppConn = value;}
        }

        public  delegate void friend_qcm_MouseClick_Delegate(object sender,Friend item,String type);//用于好友右单击事件
        public event friend_qcm_MouseClick_Delegate friend_qcm_MouseClickEvent;

        public delegate void delegate_openChat(Friend sender);//打开聊天窗口
        public event delegate_openChat OpenChatEvent;


        private QQContextMenu friend_qcm, control_qcm, group_qcm = null;

        private Dictionary<string, Friend> m_Rosters = null;
        public Dictionary<string, Friend> Rosters
        {
            get
            {
                if (m_Rosters==null)
                {
                    m_Rosters = new Dictionary<string, Friend>();
                }
                return m_Rosters; 
            }
            set { m_Rosters = value; }
        }

        private Dictionary<string, Group> m_Groups = null;
        public Dictionary<string, Group> Groups
        {
            get
            {
                if (m_Groups == null)
                {
                    m_Groups = new Dictionary<string, Group>();
                }
                return m_Groups; 
            }
            set { m_Groups = value; }
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {

            if (SelectFriend != null)
            {
                SelectFriend = null;
            }

            if (OldSelectFriend != null)
            {
                OldSelectFriend = null;
            }

            if (friend_qcm != null)
            {
                friend_qcm.Dispose();
                friend_qcm = null;
            }

            if (control_qcm != null)
            {
                control_qcm.Dispose();
                control_qcm = null;
            }

            if (group_qcm!= null)
            {
                group_qcm.Dispose();
                group_qcm = null;
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);

            System.GC.Collect();
        }

        public QQListViewEx()
        {
            this.AutoScroll = true;
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.White;
            this.Name = "ListView";

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            InitGroupMenu();
            InitControlMenu();
            InitFriendMenu();
        }

        #region 单项右键菜单
        private void InitFriendMenu()
        {

            QQToolStripMenuItem qtsm_send = new QQToolStripMenuItem();
            qtsm_send.Text = "发送消息";
            qtsm_send.MouseDown += new MouseEventHandler(qtsm_send_MouseDown);

            QQToolStripMenuItem qtsm_info = new QQToolStripMenuItem();
            qtsm_info.Text = "查看资料";
            qtsm_info.MouseDown += new MouseEventHandler(qtsm_info_MouseDown);
            
            QQToolStripMenuItem qtsm_move = new QQToolStripMenuItem();
            qtsm_move.MouseDown += new MouseEventHandler(qtsm_move_MouseDown);
            qtsm_move.Text = "移动到";

            //QQToolStripMenuItem qtsm_remark = new QQToolStripMenuItem();
            //qtsm_remark.MouseDown += new MouseEventHandler(item4_MouseDown);
            //qtsm_remark.Text = "备注";

            QQToolStripMenuItem qtsm_delete = new QQToolStripMenuItem();
            qtsm_delete.Text = "删除联系人";
            qtsm_delete.MouseDown += new MouseEventHandler(qtsm_delete_MouseDown);

            friend_qcm = new QQContextMenu();
            friend_qcm.Items.AddRange(new ToolStripItem[] {qtsm_send, qtsm_info, qtsm_move, qtsm_delete });
        }

        void qtsm_send_MouseDown(object sender, MouseEventArgs e)
        {
            if (friend_qcm_MouseClickEvent != null)
                friend_qcm_MouseClickEvent(sender, SelectFriend, "chat");
            System.GC.Collect();
        }

        void qtsm_info_MouseDown(object sender, MouseEventArgs e)
        {
            if (friend_qcm_MouseClickEvent != null)
                friend_qcm_MouseClickEvent(sender, SelectFriend, "vcar");
            System.GC.Collect();
        }

        void qtsm_move_MouseDown(object sender, MouseEventArgs e)
        {
            if (friend_qcm_MouseClickEvent != null)
                friend_qcm_MouseClickEvent(sender, SelectFriend, "move");
            System.GC.Collect();
        }

        void item4_MouseDown(object sender, MouseEventArgs e)
        {
            if (friend_qcm_MouseClickEvent != null)
                friend_qcm_MouseClickEvent(sender, SelectFriend, "remark");
            System.GC.Collect();
        }

        void qtsm_delete_MouseDown(object sender, MouseEventArgs e)
        {
            if (friend_qcm_MouseClickEvent != null)
                friend_qcm_MouseClickEvent(sender, SelectFriend, "dele");
            System.GC.Collect();
        }
        #endregion

        #region 空白右键菜单
        private void InitControlMenu()
        {
            //QQToolStripMenuItem pm1 = new QQToolStripMenuItem();
            //pm1.Text = "显示陌生人";
            //QQToolStripMenuItem pm2 = new QQToolStripMenuItem();
            //pm2.Text = "显示黑名单";

            QQToolStripMenuItem qtsm_Bigitem = new QQToolStripMenuItem();
            qtsm_Bigitem.Name = "qtsm_Bigitem";
            qtsm_Bigitem.Text = "大头像";
            qtsm_Bigitem.MouseDown += new MouseEventHandler(qtsm_Bigitem_MouseDown);
            
            QQToolStripMenuItem qtsm_Smallitem = new QQToolStripMenuItem();
            qtsm_Smallitem.Name="qtsm_Smallitem";
            qtsm_Smallitem.Text = "小头像";
           
            qtsm_Smallitem.MouseDown += new MouseEventHandler(qtsm_Smallitem_MouseDown);

            QQToolStripSeparator atss1 = new QQToolStripSeparator();
            QQToolStripMenuItem qtsm_addgroup = new QQToolStripMenuItem();
            qtsm_addgroup.Text = "添加组";
            qtsm_addgroup.MouseDown += new MouseEventHandler(qtsm_addgroup_MouseDown);
            control_qcm = new QQContextMenu();
            control_qcm.Items.AddRange(new ToolStripItem[] { qtsm_Bigitem, qtsm_Smallitem,atss1, qtsm_addgroup });
            this.ContextMenuStrip = control_qcm;
            control_qcm.Paint += new PaintEventHandler(control_qcm_Paint);
            System.GC.Collect();

        }

        private void control_qcm_Paint(object sender, PaintEventArgs e)
        {
            if (FCType == FriendContainerType.Big)
            {
                ((QQToolStripMenuItem)control_qcm.Items["qtsm_Bigitem"]).Checked = true;
                ((QQToolStripMenuItem)control_qcm.Items["qtsm_Smallitem"]).Checked = false;
            }
            else
            {
                ((QQToolStripMenuItem)control_qcm.Items["qtsm_Bigitem"]).Checked = false;
                ((QQToolStripMenuItem)control_qcm.Items["qtsm_Smallitem"]).Checked = true;
            }
        }

        private void qtsm_addgroup_MouseDown(object sender, MouseEventArgs e)
        {
            String name = "";
            AddGroup(name);
            ClickGroup = Controls[StringFinal.GroupName + Groups[name].Id] as GroupControl;
            qtsm_rechristen_MouseDown(null, null);
            System.GC.Collect();

        }

        private void qtsm_Bigitem_MouseDown(object sender, MouseEventArgs e)
        {
            FCType = FriendContainerType.Big;

            foreach (String key in Groups.Keys)
            {
                Panel groupPanel = Controls["userPanel_" + Groups[key].Id.ToString()] as Panel;
                UpdateLayout(groupPanel);
            }
            ((QQToolStripMenuItem)control_qcm.Items["qtsm_Bigitem"]).Checked = true;
            ((QQToolStripMenuItem)control_qcm.Items["qtsm_Smallitem"]).Checked = false;
            UpdateLayout(3, 0);
            friend_qcm_MouseClickEvent(sender, SelectFriend, "HeadBig");
        }

        void qtsm_Smallitem_MouseDown(object sender, MouseEventArgs e)
        {
            FCType = FriendContainerType.Small;
            foreach (String key in Groups.Keys)
            {
                Panel groupPanel = Controls["userPanel_" + Groups[key].Id.ToString()] as Panel;
                UpdateLayout(groupPanel);
            }
            ((QQToolStripMenuItem)control_qcm.Items["qtsm_Bigitem"]).Checked = false;
            ((QQToolStripMenuItem)control_qcm.Items["qtsm_Smallitem"]).Checked = true;
            UpdateLayout(3, 0);
            friend_qcm_MouseClickEvent(sender, SelectFriend, "HeadSmall");
        }
        
        #endregion

        #region 空白列表右键菜单

        private void InitGroupMenu()
        {
            QQToolStripMenuItem qtsm_open_close = new QQToolStripMenuItem();
            qtsm_open_close.Text = "展开/收缩";
            qtsm_open_close.MouseDown += new MouseEventHandler(qtsm_open_close_MouseDown);

            QQToolStripSeparator atss1 = new QQToolStripSeparator();

            QQToolStripMenuItem qtsm_rechristen = new QQToolStripMenuItem();
            qtsm_rechristen.MouseDown += new MouseEventHandler(qtsm_rechristen_MouseDown);
            qtsm_rechristen.Text = "重命名";

            QQToolStripMenuItem qtsm_deleteGroup = new QQToolStripMenuItem();
            qtsm_deleteGroup.MouseDown += new MouseEventHandler(qtsm_deleteGroup_MouseDown);
            qtsm_deleteGroup.Text = "删除组";

            group_qcm = new QQContextMenu();
            group_qcm.Items.AddRange(new ToolStripItem[] { qtsm_open_close, atss1, qtsm_rechristen, qtsm_deleteGroup });
            System.GC.Collect();
        }

        public void ExpandGroup(int groupID)
        {
            GroupControl gg = Controls["group_" + groupID] as GroupControl;
            if (!gg.IsExpand)
            {
                gg.IsExpand = true;
                ShowFriendsList(groupID);
            }
            System.GC.Collect();
        }

        public void CollapseGroup(int groupID)
        {
            GroupControl gg = Controls["group_" + groupID] as GroupControl;
            if (gg.IsExpand)
            {
                gg.IsExpand = false;
                HideFriendsList(groupID);
            }
            System.GC.Collect();
        }

        void qtsm_open_close_MouseDown(object sender, MouseEventArgs e)
        {
            if (ClickGroup.IsExpand)
                CollapseGroup(ClickGroup.GroupInfo.Id);
            else
                ExpandGroup(ClickGroup.GroupInfo.Id);
            System.GC.Collect();
        }

        void qtsm_rechristen_MouseDown(object sender, MouseEventArgs e)
        {
            BasicTextBox tb = new BasicTextBox();
            tb.BorderStyle = BorderStyle.None;
            tb.Texts = ClickGroup.GroupInfo.Title;

            Group gruop = Groups[ClickGroup.GroupInfo.Title];

            if (Controls["userPanel_" + gruop.Id].Controls.Count > 0)
            {
                return;
            }

            tb.Location = new Point(16, ClickGroup.Top + 1);
            tb.Size = new Size(Width - 20, 22);
            if (sender == null)
            {
                tb.Leave += new EventHandler(tb_Leave_n);
            }
            else
            {
                tb.Leave += new EventHandler(tb_Leave);
            }
            tb.KeyDown += new KeyEventHandler(tb_KeyDown);

            Controls.Add(tb);
            tb.BringToFront();
            tb.Focus();
            System.GC.Collect();
        }

        void qtsm_deleteGroup_MouseDown(object sender, MouseEventArgs e)
        {

            String panelName = StringFinal.UserPanelName + ClickGroup.GroupInfo.Id;
            System.Windows.Forms.Control c = this.Controls[this.Controls.IndexOfKey(panelName)];
            if (c.Controls.Count==0)
            {
                RemoveGroup(ClickGroup.GroupInfo.Title);    
            }
            else
            {
                MsgBox.Show("CSS&IM", "当前分组中有好友，请先转移好友后在删除分组！");
            }
            System.GC.Collect();            
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ClickGroup.Focus();
            System.GC.Collect();
        }

        private void tb_Leave(object sender, EventArgs e)
        {
            BasicTextBox tb = sender as BasicTextBox;

            if (!Groups.ContainsKey(tb.Texts))
            {

                Group gruop = Groups[ClickGroup.GroupInfo.Title];
                Groups.Remove(ClickGroup.GroupInfo.Title);
                ClickGroup.GroupInfo.Title = tb.Texts;
                gruop.Title = ClickGroup.GroupInfo.Title;
                Groups.Add(gruop.Title, gruop);
                ClickGroup.GroupInfo = gruop;
                tb.Dispose();
            }
            //tb.Texts = "未命名组";
            tb.Dispose();
            System.GC.Collect();
        }

        private void tb_Leave_n(object sender, EventArgs e)
        {
            BasicTextBox tb = sender as BasicTextBox;

            if (!Groups.ContainsKey(tb.Texts))
            {
                ClickGroup.GroupInfo.Title = tb.Texts;
                Group gruop = Groups[""];
                gruop.Title = tb.Texts;
                Groups.Remove("");
                Groups.Add(gruop.Title, gruop);
                ClickGroup.GroupInfo = gruop;
                tb.Dispose();
            }
            else if (tb.Texts == "")
            {
                try
                {

                    string groupname = "未命名组";
                    string newgroupname = groupname;
                    int groupnameIndex = 0;

                    while (true)
                    {
                        if (Groups.ContainsKey(newgroupname))
                        {
                            groupnameIndex++;
                            newgroupname = groupname + groupnameIndex;
                        }
                        else
                        {
                            break;
                        }
                    }


                    tb.Texts = newgroupname;
                    ClickGroup.GroupInfo.Title = tb.Texts;
                    Group gruop = Groups[""];
                    gruop.Title = tb.Texts;
                    Groups.Remove("");
                    Groups.Add(gruop.Title, gruop);
                    ClickGroup.GroupInfo = gruop;
                    tb.Dispose();
                }
                catch (Exception)
                {

                }
            }
            tb.Focus();
            System.GC.Collect();
        }

        #endregion

        private void friend_OpenChat(Friend sender)
        {
            if (OpenChatEvent != null)
                OpenChatEvent(sender);
            System.GC.Collect();
        }

        private void friend_ShowContextMenu(FriendControl sender, MouseEventArgs e)
        {
            if (friend_qcm == null)
            {
                InitFriendMenu();
            }
            friend_qcm.Show(sender, e.Location);
            System.GC.Collect();
        }

        private void cGroup_ShowContextMenu(GroupControl sender, MouseEventArgs e)
        {
            ClickGroup = sender;
            if (group_qcm == null)
            {
                InitGroupMenu();
            }
            group_qcm.Show(sender, e.Location);
            System.GC.Collect();
        }

        private void cGroup_ExpandChanged(Group sender, bool Currentstate)
        {
            if (Currentstate)
                ShowFriendsList(sender.Id);
            else
                HideFriendsList(sender.Id);
            System.GC.Collect();
        }

        public void AddGroup(String groupName)
        {
            if (Groups.ContainsKey(groupName))
                throw new Exception("group is exits");

            groupIndex += 1;

            Group group = new Group();
            group.OnlineCount = 0;
            group.Count = 0;
            group.Title = groupName;
            group.Id = groupIndex;
            Groups.Add(groupName, group);

            GroupControl cGroup = new GroupControl();
            this.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            this.BackColor = this.BackColor;
            cGroup.Location = new Point(2, 1);
            cGroup.Name = StringFinal.GroupName + group.Id;
            cGroup.Size = new Size(Width, 24);
            cGroup.GroupInfo = group;
            cGroup.ExpandChanged += new GroupControl.ExpandChangeEventHandler(cGroup_ExpandChanged);
            cGroup.ShowContextMenu += new GroupControl.ShowContextMenuEventHandler(cGroup_ShowContextMenu);
            this.Controls.Add(cGroup);


            Panel panel = new Panel();
            panel.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            panel.BackColor = this.BackColor;
            panel.Location = new Point(0, 0);
            panel.Name = StringFinal.UserPanelName + group.Id;
            panel.Size = new Size(this.Width, group.Count * 56);
            panel.Visible = false;
            this.Controls.Add(panel);

            UpdateLayout(3,group.Id);
            System.GC.Collect();
        }

        public void RemoveGroup(String groupName)
        {
            Group item=null;
            string controlsName=null;
            string panelName = null;
            try
            {
                item = Groups[groupName];
            }
            catch (Exception)
            {
                throw new Exception("group is exits");
            }
            controlsName=StringFinal.GroupName+item.Id;
            panelName = StringFinal.UserPanelName + item.Id;
            this.Controls.RemoveAt(this.Controls.IndexOfKey(controlsName));
            this.Controls.RemoveAt(this.Controls.IndexOfKey(panelName));
            this.Groups.Remove(groupName);
            UpdateLayout(3, 0);
            System.GC.Collect();

        }

        private object AddFriendLock = new object();
        public void AddFriend(Friend item)
        {
            lock (AddFriendLock)
            {
                int panel_index = Controls.IndexOfKey(StringFinal.UserPanelName + item.GroupID);
                Group group_item = Groups[item.GroupName];
                int group_index = Controls.IndexOfKey(StringFinal.GroupName + item.GroupID);
                friendIndex += 1;
                item.Uin = friendIndex;
                Panel panel_user = Controls[panel_index] as Panel;
                GroupControl gruop_user = Controls[group_index] as GroupControl;
                if (!Rosters.ContainsKey(item.Ritem.Jid.Bare))
                {
                    group_item.Count += 1;
                }
                if (item.IsOnline)
                {
                    group_item.OnlineCount += 1;
                }
                gruop_user.GroupInfo = group_item;//好友总数
                Rosters.Add(item.Ritem.Jid.Bare, item);
                FriendControl friend = new FriendControl(XmppConn, item.Ritem.Jid);
                friend.FCType = FriendContainerType.Small;
                friend.Location = new Point(1, ItemHeight + 1 * panel_user.Controls.Count);
                friend.Name = StringFinal.FriendName + item.Uin;
                friend.Size = new Size(panel_user.Width - 2, ItemHeight);
                friend.FriendInfo = item;
                friend.Selecting += new FriendControl.SelectedEventHandler(friend_Selecting);
                friend.ShowContextMenu += new FriendControl.ShowContextMenuEventHandler(friend_ShowContextMenu);
                friend.OpenChat += new FriendControl.OpenChatEventHandler(friend_OpenChat);
                friend.UpdateImage();//更新头像信息
                panel_user.Controls.Add(friend);
                panel_user.Height += (ItemHeight + 1);
                UpdateLayout(3, 0);
                //UpdateLayout(1, group_item.Id);
            }
        }

        private object RemoveFriendLock = new object();
        public void RemoveFriend(Friend item)
        {
            lock (RemoveFriendLock)
            {
                Friend friend = Rosters[item.Ritem.Jid.Bare];
                Group group_item = Groups[friend.GroupName];

                int group_index = Controls.IndexOfKey(StringFinal.GroupName + friend.GroupID);
                int panel_index = Controls.IndexOfKey(StringFinal.UserPanelName + friend.GroupID);

                group_item.Count -= 1;

                if (friend.IsOnline)
                {
                    group_item.OnlineCount -= 1;
                }

                Panel panel_user = Controls[panel_index] as Panel;
                GroupControl gruop_user = Controls[group_index] as GroupControl;

                gruop_user.GroupInfo = group_item;//好友总数
                panel_user.Controls.RemoveAt(panel_user.Controls.IndexOfKey(StringFinal.FriendName + friend.Uin));
                Rosters.Remove(item.Ritem.Jid.Bare);

                UpdateLayout(panel_user);
                UpdateLayout(3, 0);
                System.GC.Collect();
            }
        }

        public void RemoveFriend(string Bare)
        {
            lock (RemoveFriendLock)
            {
                try
                {
                    Friend friend = Rosters[Bare];
                    Group group_item = Groups[friend.GroupName];

                    int group_index = Controls.IndexOfKey(StringFinal.GroupName + friend.GroupID);
                    int panel_index = Controls.IndexOfKey(StringFinal.UserPanelName + friend.GroupID);

                    group_item.Count -= 1;
                    if (friend.IsOnline)
                    {
                        group_item.OnlineCount -= 1;
                    }

                    Panel panel_user = Controls[panel_index] as Panel;
                    GroupControl gruop_user = Controls[group_index] as GroupControl;
                    gruop_user.GroupInfo = group_item;//好友总数

                    panel_user.Controls.RemoveAt(panel_user.Controls.IndexOfKey(StringFinal.FriendName + friend.Uin));
                    Rosters.Remove(Bare);

                    UpdateLayout(panel_user);
                    UpdateLayout(3, 0);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("RemoveFriend(string Bare)错误:"+ex.Message);
                }
                System.GC.Collect();
            }
        }

        public void RefreshFriend(string Bare, XMPP.protocol.client.PresenceType presenceType, ShowType Show)
        {

            Friend friend = Rosters[Bare];
            //dnd 忙碌 busy 4  away 离开 away 2
            //int state = 1;//不知道是什么
            switch (Show)
            {
                case ShowType.NONE:
                    bool isonline = friend.IsOnline;
                    friend.State = 0;
                    friend.IsOnline = isonline;
                    break;
                case ShowType.dnd:
                    //state = 4;
                    friend.State = 4;
                    break;
                case ShowType.away:
                    //state = 2;
                    friend.State = 2;
                    break;
            }
            //friend.State = state;
            Group group_item = Groups[friend.GroupName];
            int group_index = Controls.IndexOfKey(StringFinal.GroupName + friend.GroupID);
            GroupControl gruop_user = Controls[group_index] as GroupControl;

            int panel_index = Controls.IndexOfKey(StringFinal.UserPanelName + friend.GroupID);
            Panel panel_user = Controls[panel_index] as Panel;

            if (presenceType == XMPP.protocol.client.PresenceType.unavailable)
            {
                if (friend.IsOnline)
                {
                    if (gruop_user.GroupInfo.OnlineCount > 0)
                    {
                        group_item.OnlineCount -= 1;
                        gruop_user.GroupInfo = group_item;
                    }
                }
                friend.IsOnline = false;
                if(Path.InputAlertSwitch)
                    SoundPlayEx.MsgPlay(Path.InputAlertPath);
            }
            else
            {
                if (friend.IsOnline==false)
                {
                    group_item.OnlineCount += 1;
                    gruop_user.GroupInfo = group_item;    
                }
                friend.IsOnline = true;
                if(Path.GlobalSwitch)
                    SoundPlayEx.MsgPlay(Path.GlobalPath);
            }


            int friend_index = panel_user.Controls.IndexOfKey(StringFinal.FriendName + friend.Uin);
            FriendControl friend_user = panel_user.Controls[friend_index] as FriendControl;
            friend_user.FriendInfo = friend;
            friend_user.UpdateImage();//更新名片

            UpdateLayout(panel_user);
            System.GC.Collect();
                 
        }

        public void UpdateFriendFlicker(string bare)
        {
            try
            {
                Friend friend = Rosters[bare];
                Group group_item = Groups[friend.GroupName];


                int group_index = Controls.IndexOfKey(StringFinal.GroupName + friend.GroupID);
                int panel_index = Controls.IndexOfKey(StringFinal.UserPanelName + friend.GroupID);
       
                Panel panel_user = Controls[panel_index] as Panel;
                FriendControl firendCs = panel_user.Controls[panel_user.Controls.IndexOfKey(StringFinal.FriendName + friend.Uin)] as FriendControl;

                firendCs.timer_flicker.Enabled = false;
                firendCs.x = 8;
                firendCs.y = 8;
                firendCs.Invalidate();

                
            }
            catch (Exception)
            {


            }
            System.GC.Collect();
        }

        private void friend_Selecting(Friend sender)
        {
            SelectFriend = sender;
             System.GC.Collect();
        }

        private void HideFriendsList(int gid)
        {

            if (gid >= 0)
            {
                GroupControl group = this.Controls[StringFinal.GroupName + gid] as GroupControl;
                if (group.GroupInfo.Count > 0)//如果分组没有好友就是显示panel是为了不让列表出来布局混乱
                {
                    this.Controls["userPanel_" + gid].Hide();

                    UpdateLayout(1, gid);
                    if (SelectFriend != null)
                    {
                        if (SelectFriend.GroupID == gid)
                            OldSelectFriend = SelectFriend = null;
                    }
                }
                GC.Collect();
            }
            System.GC.Collect();
        }

        private void ShowFriendsList(int gid)
        {
            if (gid >= 0)
            {
                GroupControl group=this.Controls[StringFinal.GroupName + gid] as GroupControl;
                if (group.GroupInfo.Count > 0)//如果分组没有好友就是显示panel是为了不让列表出来布局混乱
                {
                    this.Controls["userPanel_" + gid].Top = this.Controls[StringFinal.GroupName + gid].Top + 23;
                    this.Controls["userPanel_" + gid].Show();

                    UpdateLayout(1, gid);
                }
                GC.Collect();
            }
            System.GC.Collect();
        }

        public void UpdateFriendControl()
        {
            if (OldSelectFriend != null)
            {
                Panel p = (Panel)Controls[StringFinal.UserPanelName + OldSelectFriend.GroupID];
                if (p != null)
                {
                    try
                    {
                        FriendControl fc = p.Controls[StringFinal.FriendName + OldSelectFriend.Uin] as FriendControl;
                        fc.IsSelected = false;
                    }
                    catch (Exception)
                    {
                        
                    }
                   
                }
            }
            System.GC.Collect();
        }

        public void RefreshGroup()
        {
            foreach (String key in Groups.Keys)
            {
                Panel groupPanel = Controls["userPanel_" + Groups[key].Id.ToString()] as Panel;
                UpdateLayout(groupPanel);
            }
            UpdateLayout(3, 0);
        }

        public void UpdateLayout(int action, int gid)
        {
            if (action == 0)
            {
                int cid = Controls.IndexOfKey(StringFinal.GroupName + gid);

                for (int i = cid + 2; i < Controls.Count; i += 2)
                {
                    if (this.Controls[i - 1].Visible)
                    {
                        this.Controls[i].Top = Controls[i - 2].Top + Controls[i - 1].Height;
                        this.Controls[i + 1].Top = Controls[i].Top + 25;
                    }
                    else
                    {
                        this.Controls[i].Top = Controls[i - 2].Top + 25;
                        this.Controls[i + 1].Top = Controls[i].Top + 25;
                    }
                }
            }
            else if (action == 1)
            {
                int cid = Controls.IndexOfKey(StringFinal.GroupName + gid);
                for (int i = cid + 2; i < Controls.Count; i += 2)
                {
                    if (Controls[i - 1].Visible)
                    {
                        Controls[i].Top = Controls[i - 2].Top + Controls[i - 1].Height + 25;
                        Controls[i + 1].Top = Controls[i].Top + 25;
                    }
                    else
                    {
                        Controls[i].Top = Controls[i - 2].Top + 25;
                        Controls[i + 1].Top = Controls[i].Top + 25;
                    }
                }
            }
            else if (action == 3)
            {
                ArrayList keyList = new ArrayList();
                foreach (String item in Groups.Keys)
                {
                    keyList.Add(item.ToString());
                }

                for (int i = 0; i < keyList.Count; i++)
                {
                    if (i == 0)
                    {
                        //Controls[i].Location = new Point(1, 2);
                        //Controls[i + 1].Top = Controls[i].Top + 25;

                    }
                    else
                    {
                        Group gg = Groups[keyList[i - 1].ToString()] as Group;
                        Group ggg = Groups[keyList[i].ToString()] as Group;

                        if (Controls["userPanel_" + gg.Id].Visible)
                        {
                            Controls[StringFinal.GroupName + ggg.Id].Top = Controls[StringFinal.GroupName + gg.Id].Top + Controls["userPanel_" + gg.Id].Height + 25;
                            Controls["userPanel_" + ggg.Id].Top = Controls[StringFinal.GroupName + ggg.Id].Top + 25;
                        }
                        else
                        {
                            Controls[StringFinal.GroupName + ggg.Id].Top = Controls[StringFinal.GroupName + gg.Id].Top + 25;
                            Controls["userPanel_" + ggg.Id].Top = Controls[StringFinal.GroupName + ggg.Id].Top + 25;
                        }
                    }
                }
            }
            System.GC.Collect();
        }

        bool isRefSort = false;
        public void UpdateLayout(Panel panel_user)
        {
            List<FriendControl> listFriend1 = new List<FriendControl>();
            FriendControl item = null;

            for (int i = 0; i < panel_user.Controls.Count; i++)
            {
                //panel_user.Controls[i].Location = new Point(1, 56 * i);
                item = (FriendControl)panel_user.Controls[i];
                item.FCType = this.FCType;
                item.Height = ItemHeight + 1;
                listFriend1.Add(item);
            }

            if (panel_user.Controls.Count > 0)
            {
                Reverser<FriendControl> reverser1 = new Reverser<FriendControl>(item.GetType(), "OnLine", ReverserInfo.Direction.DESC);
                listFriend1.Sort(reverser1);

                if (isRefSort)
                {
                    reverser1 = new Reverser<FriendControl>(item.GetType(), "OnLine", ReverserInfo.Direction.DESC);
                    listFriend1.Sort(reverser1);
                }
            }
            isRefSort = true;

            panel_user.Controls.Clear();

            for (int i = 0; i < listFriend1.Count; i++)
            {
                listFriend1[i].Location = new Point(1, (ItemHeight+1) * i);
                panel_user.Controls.Add(listFriend1[i]);
            }

            panel_user.Height = (ItemHeight + 1) * panel_user.Controls.Count;
            System.GC.Collect();
        }

        public void LoadRosterEnd()
        {
            foreach (String key in Groups.Keys)
            {
                Panel groupPanel = Controls["userPanel_" + Groups[key].Id.ToString()] as Panel;
                UpdateLayout(groupPanel);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (ClickGroup != null)
                ClickGroup.Focus();
            System.GC.Collect();
        }

        /// <summary>
        /// 记好友的头像闪烁
        /// </summary>
        /// <param name="formJid"></param>
        public void flickerFriend(Jid formJid)
        {
            Friend friend = Rosters[formJid.Bare];
            Group group_item = Groups[friend.GroupName];

            int group_index = Controls.IndexOfKey(StringFinal.GroupName + friend.GroupID);
            int panel_index = Controls.IndexOfKey(StringFinal.UserPanelName + friend.GroupID);


            Panel panel_user = Controls[panel_index] as Panel;
            GroupControl gruop_user = Controls[group_index] as GroupControl;

            gruop_user.GroupInfo = group_item;//好友总数
            FriendControl friend_item=panel_user.Controls[panel_user.Controls.IndexOfKey(StringFinal.FriendName + friend.Uin)] as FriendControl;
            friend_item.timer_flicker.Enabled = true;
            System.GC.Collect();
        }

        /// <summary>
        /// 返回昵称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetFriendNickName(string bare)
        {
            try
            {
                Friend friend = Rosters[bare];
                Group group_item = Groups[friend.GroupName];


                int group_index = Controls.IndexOfKey(StringFinal.GroupName + friend.GroupID);
                int panel_index = Controls.IndexOfKey(StringFinal.UserPanelName + friend.GroupID);

                Panel panel_user = Controls[panel_index] as Panel;
                GroupControl gruop_user = Controls[group_index] as GroupControl;
                gruop_user.GroupInfo = group_item;//好友总数

                FriendControl friendControl = panel_user.Controls[panel_user.Controls.IndexOfKey(StringFinal.FriendName + friend.Uin)] as FriendControl;
                return friendControl.NickName;
            }
            catch (Exception)
            {
                return bare;

            }
            finally
            {
                System.GC.Collect();
            }
        }
    }
}
