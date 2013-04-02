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

        public void AddFriend(Friend firend)
        {

            if (!Rosters.ContainsKey(firend.Ritem.Jid.Bare))
            {
                FriendControl friend = new FriendControl(XmppConn,firend.Ritem.Jid);
                friend.FCType = FCType;;
                friend.Location = new Point(1, (ItemHeight + 1) * this.Controls.Count);
                friend.Name = firend.Ritem.Jid.Bare;
                friend.Size = new Size(this.Width - 2, ItemHeight);
                friend.FriendInfo = firend;
                friend.OpenChat += new CSS.IM.UI.Control.FriendControl.OpenChatEventHandler(friend_OpenChat);
                friend.Selecting += new FriendControl.SelectedEventHandler(friend_Selecting);
                friend.UpdateImage();//更新头像信息
                Controls.Add(friend);
                Rosters.Add(firend.Ritem.Jid.Bare, firend);
                this.Height += (ItemHeight + 1);
            }

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
