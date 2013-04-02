using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CSS.IM.XMPP;
using CSS.IM.UI.Entity;

namespace CSS.IM.UI.Control
{
    public partial class ChatHistoryFriendListView : UserControl
    {
        public Dictionary<String, Friend> FriendKey{get;set;}

        public XmppClientConnection XmppConn{set; get;}

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

        public delegate void delegate_openChat(Friend sender);//打开聊天窗口
        public event delegate_openChat OpenChatEvent;

        public ChatHistoryFriendListView()
        {
            InitializeComponent();
            System.GC.Collect();
        }

        public void AddFriend(Friend firend)
        {
            if (FriendKey==null)
            {
                FriendKey = new Dictionary<string, Friend>();
            }

            if (!FriendKey.ContainsKey(firend.Ritem.Jid.ToString()))
            {
                FriendControl friend = new FriendControl();
                friend.XmppConnection = XmppConn;

                friend.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
                friend.BackColor = this.BackColor;
                friend.Location = new Point(1, ItemHeight + 1 * this.Controls.Count);
                friend.Name = firend.Ritem.Jid.Bare;
                friend.Size = new Size(this.Width - 2, ItemHeight);
                friend.MJID = firend.Ritem.Jid;
                friend.FriendInfo = firend;
                friend.OpenChat += new CSS.IM.UI.Control.FriendControl.OpenChatEventHandler(friend_OpenChat);
                friend.UpdateImage();//更新头像信息
                Controls.Add(friend);
                FriendKey.Add(firend.Ritem.Jid.ToString(), firend);
                this.Height += (ItemHeight + 1);
            }

            System.GC.Collect();
        }

        private void friend_OpenChat(Entity.Friend sender)
        {
            if (OpenChatEvent!=null)
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
                FriendKey.Remove(jid.ToString());
            }

            UpdateControls();
            System.GC.Collect();
        }

        public void RemoveFriendAll()
        {
            if (FriendKey==null)
            {
                FriendKey = new Dictionary<string, Friend>();
            }
            Controls.Clear();
            FriendKey.Clear();
            this.Height = 1;
            UpdateControls();
            System.GC.Collect();
        }

        public void UpdateFriendInfoOnline(Jid jid,bool online)
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
            FriendKey.Clear();

            for (int i = 0; i < CL.Count; i++)
            {
                AddFriend(CL[i]);
            }
            System.GC.Collect();
        }
    }
}
