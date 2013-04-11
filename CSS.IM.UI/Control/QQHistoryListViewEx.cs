using System;
using System.Collections.Generic;
using System.Text;
using CSS.IM.UI.Entity;
using CSS.IM.XMPP;
using System.Windows.Forms;
using System.Drawing;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    public class QQHistoryListViewEx : System.Windows.Forms.UserControl
    {
        public bool isMenuShow { set; get; }

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

        private QQContextMenu friend_qcm;

        public delegate void friend_qcm_MouseClick_Delegate(object sender, Friend item, String type);//用于好友右单击事件
        public event friend_qcm_MouseClick_Delegate friend_qcm_MouseClickEvent;

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

        private CSS.IM.XMPP.XmppClientConnection XmppConn;
        [System.ComponentModel.Description("XMPP连接"), System.ComponentModel.Category("Appearance")]
        public CSS.IM.XMPP.XmppClientConnection XmppConnection
        {
            get { return XmppConn; }
            set { XmppConn = value; }
        }


        public delegate void delegate_openChat(Friend sender);//打开聊天窗口
        public event delegate_openChat OpenChatEvent;

        private Dictionary<string, Friend> m_Rosters =null;
        public Dictionary<string, Friend> Rosters
        {
            get
            {
                if (m_Rosters == null)
                {
                    m_Rosters = new Dictionary<string, Friend>();
                }
                return m_Rosters;
            }
            set { m_Rosters = value; }
        }

        public QQHistoryListViewEx()
        {
            System.GC.Collect();
        }

        public void AddFriend(Friend firend_src)
        {

            if (!Rosters.ContainsKey(firend_src.Ritem.Jid.Bare))
            {
                FriendControl friend = new FriendControl(XmppConn,firend_src.Ritem.Jid);
                friend.isTreeSearch = firend_src.isTreeSearch;
                friend.FCType = FCType;;
                friend.Location = new Point(1, (ItemHeight + 1) * this.Controls.Count);
                friend.Name = firend_src.Ritem.Jid.Bare;
                friend.Size = new Size(this.Width - 2, ItemHeight);
                friend.FriendInfo = firend_src;
                friend.NickName = firend_src.NikeName;
                friend.OpenChat += new CSS.IM.UI.Control.FriendControl.OpenChatEventHandler(friend_OpenChat);
                if (isMenuShow)
                    friend.ShowContextMenu += new FriendControl.ShowContextMenuEventHandler(friend_ShowContextMenu);
                friend.Selecting += new FriendControl.SelectedEventHandler(friend_Selecting);
                friend.UpdateImage();//更新头像信息
                Controls.Add(friend);
                Rosters.Add(firend_src.Ritem.Jid.Bare, firend_src);
                this.Height += (ItemHeight + 1);
            }

            System.GC.Collect();
        }

        private void InitFriendMenu()
        {

            QQToolStripMenuItem qtsm_send = new QQToolStripMenuItem();
            qtsm_send.Text = "发送消息";
            qtsm_send.MouseDown += new MouseEventHandler(qtsm_send_MouseDown);

            QQToolStripMenuItem qtsm_info = new QQToolStripMenuItem();
            qtsm_info.Text = "查看资料";
            qtsm_info.MouseDown += new MouseEventHandler(qtsm_info_MouseDown);

            QQToolStripMenuItem qtsm_move = new QQToolStripMenuItem();
            qtsm_move.MouseDown += new MouseEventHandler(qtsm_Addfriend_MouseDown);
            qtsm_move.Text = "添加好友";

            friend_qcm = new QQContextMenu();
            friend_qcm.Items.AddRange(new ToolStripItem[] { qtsm_send, qtsm_info, qtsm_move });
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

        void qtsm_send_MouseDown(object sender, MouseEventArgs e)
        {
            if (friend_qcm_MouseClickEvent != null)
                friend_qcm_MouseClickEvent(sender, SelectFriend, "char");
            System.GC.Collect();
        }

        void qtsm_info_MouseDown(object sender, MouseEventArgs e)
        {
            if (friend_qcm_MouseClickEvent != null)
                friend_qcm_MouseClickEvent(sender, SelectFriend, "info");
            System.GC.Collect();
        }

        void qtsm_Addfriend_MouseDown(object sender, MouseEventArgs e)
        {
            if (friend_qcm_MouseClickEvent != null)
                friend_qcm_MouseClickEvent(sender, SelectFriend, "add");
            System.GC.Collect();
        }

        public void friend_Selecting(Friend sender)
        {
            SelectFriend = sender;
            System.GC.Collect();
        }

        private void friend_OpenChat(Entity.Friend sender)
        {
            if (OpenChatEvent != null)
            {
                OpenChatEvent(sender);
            }
            System.GC.Collect();
        }

        public void RemoveFriend(Jid jid)
        {
            int index = Controls.IndexOfKey(jid.Bare);
            if (index > -1)
            {
                Controls.RemoveAt(index);
                this.Height -= (ItemHeight + 1);
                Rosters.Remove(jid.Bare);
            }

            UpdateControls();
            System.GC.Collect();
        }

        public void RemoveFriendAll()
        {
            Controls.Clear();
            Rosters.Clear();
            this.Height = 1;
            UpdateControls();
            System.GC.Collect();
        }

        public void UpdateFriendInfoOnline(Jid jid, bool online)
        {
            int index = Controls.IndexOfKey(jid.Bare);
            if (index > -1)
            {
                FriendControl frienditem = Controls[index] as FriendControl;
                frienditem.FriendInfo.IsOnline = online;
                frienditem.UpdateImage();
                frienditem.Invalidate();
            }
            System.GC.Collect();
        }

        public void UpdateControls()
        {
            List<Friend> CL = new List<Friend>();
            int Count = Controls.Count;
            for (int i = 0; i < Count; i++)
            {
                Friend jid = (Controls[i] as FriendControl).FriendInfo;
                CL.Add(jid);
            }

            Controls.Clear();
            Rosters.Clear();

            for (int i = 0; i < CL.Count; i++)
            {
                AddFriend(CL[i]);
            }
            System.GC.Collect();
        }

        private void UpdateFriendControl()
        {
            if (OldSelectFriend != null)
            {
                FriendControl fc = Controls[OldSelectFriend.Ritem.Jid.Bare] as FriendControl;
                if (fc!=null)
                {
                    fc.IsSelected = false;    
                }
            }
            System.GC.Collect();
        }
    }
}
