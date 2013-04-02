using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Text;
using System.Threading;
using CSS.IM.UI.Entity;
using CSS.IM.UI.Form;
using CSS.IM.UI.Util;
using CSS.IM.UI.Roster;
using CSS.IM.XMPP;

namespace CSS.IM.UI.Control
{
    public partial class QQListView : UserControl
    {
        private int GroupCount = 0;
        private XmppClientConnection _connection;

        private Friend _selectFriend = null;
        private GroupControl _clickGroup = null;
        private bool _bgColorMode = true;
        private Friend _oldSelectFriend = null;
        private Thread skinThread = null;

        private QQContextMenu qcm, qcm1, qcm2 = null;
        private ArrayList groupList = null;

        public ArrayList GroupList
        {
            get { return groupList; }
        }

        private Dictionary<string, Friend> m_Roster = new Dictionary<string, Friend>();
        private Dictionary<string, Group> m_Group = new Dictionary<string, Group>();

        private int groupID = 50;
        private int FriendID = 50;

        public QQListView(XmppClientConnection con)
        {
            _connection = con;
            this.AutoScroll = true;
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.White;
            this.Name = "ListView";
            this.Size = new Size(235, 350);

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            groupList = new ArrayList(5);

            InitGroupMenu();
            InitPanelMenu();
            InitFriendMenu();
        }


        private void InitFriendMenu()
        {
            QQToolStripMenuItem item1 = new QQToolStripMenuItem();
            item1.Text = "发送消息";
            item1.Click += new EventHandler(item1_Click);
            QQToolStripMenuItem item2 = new QQToolStripMenuItem();
            item2.Text = "查看资料";
            item2.Click+=new EventHandler(item2_Click);
            QQToolStripMenuItem item3 = new QQToolStripMenuItem();
            item3.Text = "移动到";
            item3.Click+=new EventHandler(item3_Click);
            //QQToolStripMenuItem item4 = new QQToolStripMenuItem();
            //item4.Text = "我的好友";
            QQToolStripMenuItem item5 = new QQToolStripMenuItem();
            item5.Text = "加入到陌生人";
            item5.Click+=new EventHandler(item5_Click);
            QQToolStripMenuItem item6 = new QQToolStripMenuItem();
            item6.Text = "移到黑名单";
            item6.Click+=new EventHandler(item6_Click);
            qcm2 = new QQContextMenu();
            qcm2.Items.AddRange(new ToolStripItem[] { item1, item2, item3, item5, item6 });
        }

        private void item1_Click(object sender, EventArgs e)
        {
            
        }

        private void item2_Click(object sender, EventArgs e)
        {

        }

        private void item3_Click(object sender, EventArgs e)
        {

        }

        private void item4_Click(object sender, EventArgs e)
        {

        }

        private void item5_Click(object sender, EventArgs e)
        {

        }

        private void item6_Click(object sender, EventArgs e)
        {

        }

        private void InitPanelMenu()
        {
            QQToolStripMenuItem pm1 = new QQToolStripMenuItem();
            pm1.Text = "显示陌生人";
            //pm1.Click += new EventHandler(qm1_Click);
            QQToolStripMenuItem pm2 = new QQToolStripMenuItem();
            pm2.Text = "显示黑名单";
            //pm2.Click += new EventHandler(qm2_Click);
            QQToolStripSeparator qs = new QQToolStripSeparator();
            QQToolStripMenuItem pm3 = new QQToolStripMenuItem();
            pm3.Text = "添加组";
            pm3.Click += new EventHandler(gm_4_Click);
            qcm1 = new QQContextMenu();
            qcm1.Items.AddRange(new ToolStripItem[] { pm1, pm2, qs, pm3 });
            this.ContextMenuStrip = qcm1;
        }

        private void InitGroupMenu()
        {
            QQToolStripMenuItem qm1 = new QQToolStripMenuItem();
            qm1.Text = "展开/收缩";
            qm1.Click += new EventHandler(gm_1_Click);
            QQToolStripMenuItem qm4 = new QQToolStripMenuItem();
            qm4.Text = "添加组";
            qm4.Click += new EventHandler(gm_4_Click);
            QQToolStripSeparator qs = new QQToolStripSeparator();
            QQToolStripMenuItem qm2 = new QQToolStripMenuItem();
            qm2.Text = "重命名";
            qm2.Click += new EventHandler(gm_2_Click);
            QQToolStripMenuItem qm3 = new QQToolStripMenuItem();
            qm3.Text = "删除组";
            qm3.Click += new EventHandler(gm_3_Click);
            qcm = new QQContextMenu();
            qcm.Items.AddRange(new ToolStripItem[] { qm1, qm4, qs, qm2, qm3 });
        }

        private void gm_1_Click(object sender, EventArgs e)
        {
            if (ClickGroup.IsExpand)
                CollapseGroup(ClickGroup.GroupInfo.Id);
            else
                ExpandGroup(ClickGroup.GroupInfo.Id);
        }

        private void gm_2_Click(object sender, EventArgs e)
        {
            BasicTextBox tb = new BasicTextBox();
            tb.BorderStyle = BorderStyle.None;
            tb.Texts = ClickGroup.GroupInfo.Title;
            tb.Location = new Point(16, ClickGroup.Top + 1);
            tb.Size = new Size(Width - 20,22);
            if (sender == null)
            {
                tb.Leave += new EventHandler(tb_Leave_n);
            }
            else
            {
                tb.Leave += new EventHandler(tb_Leave);
            }
            tb.KeyDown+=new KeyEventHandler(tb_KeyDown);
            Controls.Add(tb);
            tb.BringToFront();
            tb.Focus();
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ClickGroup.Focus();
        }

        private void tb_Leave(object sender, EventArgs e)
        {
            BasicTextBox tb = sender as BasicTextBox;
            ClickGroup.GroupInfo.Title = tb.Texts;
            tb.Dispose();
            Controls.Remove(tb);
        }

        private void tb_Leave_n(object sender, EventArgs e)
        {
            BasicTextBox tb = sender as BasicTextBox;
            ClickGroup.GroupInfo.Title = tb.Texts;
            tb.Dispose();
            Controls.Remove(tb);
            UpdateLayout(3, 0);
        }

        private void gm_3_Click(object sender, EventArgs e)
        {
            if (MsgBox.Show(this, "提示", "删除组也将删除该组下面的所有好友？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
            {
                DeleteGroup(ClickGroup.GroupInfo.Id);
            }
        }

        private void gm_4_Click(object sender, EventArgs e)
        {
            int id = AddGroup();
            ClickGroup = Controls[StringFinal.GroupName + id] as GroupControl;
            gm_2_Click(null,null);
        }

        public bool BgColorMode
        {
            get
            {
                return _bgColorMode;
            }
            set 
            { 
                _bgColorMode = value;
                if (_bgColorMode)
                {
                    this.BackColor = Color.White;
                }
                else
                {
                    this.BackColor = Color.Transparent;
                }
                skinThread = new Thread(new ParameterizedThreadStart(ChangeSkin));
                skinThread.Start(this.BackColor);
            }
        }

        private GroupControl ClickGroup 
        {
            get
            {
                return _clickGroup;
            }
            set
            {
                _clickGroup = value;
            }
        }

        public Friend OldSelectFriend
        {
            get
            {
                return _oldSelectFriend;
            }
            set
            {
                _oldSelectFriend = value;
            }
        }

        public Friend SelectedFriend
        {
            get 
            {
                return _selectFriend;
            }
            set 
            {
                if (_selectFriend!=null)
                    OldSelectFriend = _selectFriend;
                _selectFriend = value;
                UpdateFriendControl();
            }
        }

        public  int addFriend(Friend item)
        {
            FriendID += 1;
            int groupid = item.GroupID;
            item.Uin = FriendID;
            
            int panel_index=Controls.IndexOfKey(StringFinal.UserPanelName + groupid);
            int group_index = Controls.IndexOfKey(StringFinal.GroupName + groupid);

            Panel panel_user = Controls[panel_index] as Panel;
            GroupControl gruop_user = Controls[group_index] as GroupControl;

            gruop_user.GroupInfo.Count++;//好友总数
            //gruop_user.GroupInfo.OnlineCount++;//在线好友
            m_Roster.Add(item.NikeName, item);
            FriendControl friend = new FriendControl();
            friend.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            friend.BackColor = this.BackColor;
            friend.Location = new Point(1, 56 * panel_user.Controls.Count);
            friend.Name = "friend_" + item.Uin;
            friend.Size = new Size(panel_user.Width - 2, 55);
            friend.FriendInfo = item;
            friend.Selecting += new FriendControl.SelectedEventHandler(friend_Selecting);
            friend.ShowContextMenu += new FriendControl.ShowContextMenuEventHandler(friend_ShowContextMenu);
            friend.MouseDoubleClick += new MouseEventHandler(friend_MouseDoubleClick);
            
            panel_user.Controls.Add(friend);
            System.Diagnostics.Debug.WriteLine("count:"+panel_user.Controls.Count);
            //panel_user.Controls.Add(item);
            panel_user.Height += 56;


            //if (item.State != 0 && item.State != 5)
            //{
            //    cGroup.GroupInfo.OnlineCount++;
            //}
            //friendInfo.GroupID = groups[i].Id;
            

            return 0;
        }

        public void SelectFriend(int qq)
        {
            (Controls["friend_" + qq] as FriendControl).Selected();
        }

        public void GetFriendInfo(int qq) 
        {

        }

        public void DeleteFriend(int qq) 
        {

        }

        public void InitFriendList(List<Group> groups)
        {
            GroupCount = groups.Count;
            for (int i = 0; i < groups.Count; i++)
            {
                GroupControl cGroup = new GroupControl();
                this.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
                this.BackColor = this.BackColor;
                cGroup.Location = new Point(1, 24 * i + i + 1);
                cGroup.Name = StringFinal.GroupName + groups[i].Id;
                cGroup.Size = new Size(Width, 24);
                cGroup.GroupInfo = groups[i];
                cGroup.ExpandChanged += new GroupControl.ExpandChangeEventHandler(cGroup_ExpandChanged);
                cGroup.ShowContextMenu += new GroupControl.ShowContextMenuEventHandler(cGroup_ShowContextMenu);
                //AddGroup(groups[i].Title);

                //this.Controls.Add(cGroup);//添加组便签

                int index=groupList.Add(groups[i]);
                m_Group.Add(groups[i].Title, groups[i]);

                Panel panel = new Panel();
                panel.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
                panel.BackColor = this.BackColor;
                panel.Location = new Point(0, 0);
                panel.Name = "userPanel_" + groups[i].Id;
                panel.Size = new Size(this.Width, groups[i].Count * 56);
                panel.Visible = false;
                if (groups[i].Count > 0)
                {
                    for (int j = 0; j < groups[i].List.Count; j++)
                    {
                        Friend friendInfo = (Friend)groups[i].List[j];
                        if (friendInfo.State != 0 && friendInfo.State != 5)
                        {
                            //cGroup.GroupInfo.OnlineCount++;
                        }
                        friendInfo.GroupID = groups[i].Id;
                        FriendControl friend = new FriendControl();
                        friend.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
                        friend.BackColor = this.BackColor;
                        friend.Location = new Point(1, 56 * j);
                        friend.Name = "friend_" + friendInfo.Uin;
                        friend.Size = new Size(panel.Width - 2, 55);
                        friend.FriendInfo = friendInfo;
                        friend.Selecting += new FriendControl.SelectedEventHandler(friend_Selecting);
                        friend.ShowContextMenu += new FriendControl.ShowContextMenuEventHandler(friend_ShowContextMenu);
                        friend.MouseDoubleClick += new MouseEventHandler(friend_MouseDoubleClick);
                        panel.Controls.Add(friend);
                    }
                }
                this.Controls.Add(panel);//添加联系人标签
            }
            GC.Collect();
        }

        private void friend_ShowContextMenu(FriendControl sender, MouseEventArgs e)
        {
            if (qcm2 == null)
            {
                InitFriendMenu();
            }
            qcm2.Show(sender, e.Location);
        }

        private void friend_Selecting(Friend sender)
        {
            SelectedFriend = sender;
        }

        public  int AddGroup() 
        {
            //UpdateLayout(2, 0);

            groupID += 1;
            Group group = new Group();
            group.OnlineCount = 0;
            group.Count = 0;
            group.Title = "";
            group.Id = groupID;
            group.List = new ArrayList(0);
            group.DisplayOrder = 0;

            groupList.Add(group);

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
            panel.Name = "userPanel_" + group.Id;
            panel.Size = new Size(this.Width, group.Count * 56);
            panel.Visible = false;
            this.Controls.Add(panel);
            return groupID;
        }

        public  int AddGroup(string groupName)
        {
            groupID += 1;
            Group group = new Group();
            group.OnlineCount = 0;
            group.Count = 0;
            group.Title = groupName;
            group.Id = groupID;
            
            groupList.Add(group);
            m_Group.Add(group.Title, group);

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
            panel.Size = new Size(Width, group.Count * 56);
            panel.Visible = false;
            this.Controls.Add(panel);
            //UpdateLayout(2, 0);
            return groupID;

        }

        public int AddGroup(string groupName,int groupId)
        {
            Group group = new Group();
            group.OnlineCount = 0;
            group.Count = 0;
            group.Title = groupName;
            group.Id = groupId;

            groupList.Add(group);
            m_Group.Add(group.Title, group);

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
            panel.Size = new Size(Width, group.Count * 56);
            panel.Visible = false;
            this.Controls.Add(panel);
            //UpdateLayout(2, 0);
            return groupId;

        }

        public void ExpandGroup(int groupID) 
        {
            GroupControl gg = Controls[StringFinal.GroupName + groupID] as GroupControl;
            if (!gg.IsExpand)
            {
                gg.IsExpand = true;
                ShowFriendsList(groupID);
            }
        }

        public void CollapseGroup(int groupID)
        {
            GroupControl gg = Controls[StringFinal.GroupName + groupID] as GroupControl;
            if (gg.IsExpand)
            {
                gg.IsExpand = false;
                HideFriendsList(groupID);
            }
        }

        public void DeleteGroup(int gid)
        {
            Controls[StringFinal.GroupName + gid].Visible = false;
            Controls["userPanel_" + gid].Visible = false;
            Controls[StringFinal.GroupName + gid].Top -= 25;
            GroupCount--;
            UpdateLayout(0, gid);

           groupList.Remove((Controls[StringFinal.GroupName + gid] as GroupControl).GroupInfo);          

            Controls[StringFinal.GroupName + gid].Dispose();
            Controls["userPanel_" + gid].Dispose();
            Controls.RemoveByKey(StringFinal.GroupName + gid);
            Controls.RemoveByKey("userPanel_" + gid);
            if (SelectedFriend != null)
            {
                if (SelectedFriend.GroupID == gid)
                    OldSelectFriend = SelectedFriend = null;
            }
        }

        private void cGroup_ShowContextMenu(GroupControl sender, MouseEventArgs e)
        {
            ClickGroup = sender;
            if (qcm == null)
            {
                InitGroupMenu();
            }
            qcm.Show(sender, e.Location);
        }

        private void cGroup_ExpandChanged(Group sender, bool Currentstate)
        {
            if (Currentstate)
                ShowFriendsList(sender.Id);
            else
                HideFriendsList(sender.Id);
        }

        private void ChangeSkin(object obj) 
        {
            Color color = (Color)obj;
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i].Name.IndexOf("userPanel") != -1) 
                {
                    Controls[i].BackColor = color;
                    for (int j = 0; j < Controls[i].Controls.Count; j++)
                    {
                        Controls[i].Controls[j].BackColor = color;
                    }
                }
            }
        }

        private void UpdateFriendControl()
        {
            if (OldSelectFriend != null) 
            {
                Panel p = (Panel)Controls["userPanel_" + OldSelectFriend.GroupID];
                if (p != null)
                {
                    FriendControl fc = p.Controls["friend_" + OldSelectFriend.Uin] as FriendControl;
                    fc.IsSelected = false;
                }
            }
        }

        private void OrderByNum(ArrayList arrayList)
        {
            string t1 = arrayList[0].ToString();
            //for(int i=0;i<arrayList.Count;i++){
            //string[] info = arrayList[i].ToString().Split(";".ToCharArray());
            //   if(info[0].Equals(Program.userInfo.qq.ToString())){
            //       arrayList[0] = arrayList[i];
            //       arrayList[i] = t1;
            //       break;
            //   }
            //   continue;
            //}
        }

        private void friend_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //mainForm.showChatDlg(((GFriendControl)sender).Info);
        }

        private void ShowFriendsList(int gid)
        {
            if (gid >= 0)
            {
                this.Controls["userPanel_" + gid].Top = this.Controls[StringFinal.GroupName + gid].Top + 23;
                this.Controls["userPanel_" + gid].Show();
                UpdateLayout(1, gid);
            }
        }

        private void HideFriendsList(int gid)
        {
            if (gid >= 0)
            {
                this.Controls["userPanel_" + gid].Hide();
                UpdateLayout(1, gid);
                if (SelectedFriend != null)
                {
                    if (SelectedFriend.GroupID == gid)
                        OldSelectFriend = SelectedFriend = null;
                }
                GC.Collect();
            }
        }

        public void UpdateLayout(int action, int gid) {
            if (action == 0) 
            {
                int cid = Controls.IndexOfKey(StringFinal.GroupName + gid);
                for (int i = cid + 2; i < Controls.Count; i+=2)
                {
                    if (this.Controls[i - 1].Visible)
                    {
                        this.Controls[i].Top = this.Controls[i - 2].Top + this.Controls[i - 1].Height;
                        this.Controls[i + 1].Top = this.Controls[i].Top + 25;
                    }
                    else
                    {
                        this.Controls[i].Top = this.Controls[i - 2].Top + 25;
                        this.Controls[i + 1].Top = this.Controls[i].Top + 25;
                    }
                }
            }
            else if(action == 1)
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
            else if (action == 2) 
            {
                for (int i = 0; i < Controls.Count; i += 2)
                {
                    Controls[i].Top += 25;
                    Controls[i + 1].Top += 25;
                }
            }
            else if (action == 3)
            {

                for (int i = 0; i < groupList.Count; i++)
                {
                    if (i == 0)
                    {
                        Controls[i].Top = 1;
                        Controls[i + 1].Top = Controls[i].Top + 25;
                    }
                    else
                    {
                        
                        Group gg = groupList[i - 1] as Group;
                        Group ggg = groupList[i] as Group;

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
            //else if (action == 4)
            //{

            //    for (int i = 0; i < groupList.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            Controls[i].Top = 1;
            //            Controls[i + 1].Top = Controls[i].Top + 25;
            //        }
            //        else
            //        {
            //            Group gg = groupList[i - 1] as Group;
            //            Group ggg = groupList[i] as Group;

            //            if (Controls["userPanel_" + gg.Id].Visible)
            //            {
            //                Controls[StringFinal.GroupName + ggg.Id].Top = Controls[StringFinal.GroupName + gg.Id].Top + Controls["userPanel_" + gg.Id].Height + 25;
            //                Controls["userPanel_" + ggg.Id].Top = Controls[StringFinal.GroupName + ggg.Id].Top + 25;
            //            }
            //            else
            //            {
            //                Controls[StringFinal.GroupName + ggg.Id].Top = Controls[StringFinal.GroupName + gg.Id].Top + 25;
            //                Controls["userPanel_" + ggg.Id].Top = Controls[StringFinal.GroupName + ggg.Id].Top + 25;
            //            }
            //        }
            //    }
            //}

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GC.Collect();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg) 
            {
                default:
                    break;
            }
            base.WndProc(ref m);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (ClickGroup!=null)
                ClickGroup.Focus();
        }
    }
}
