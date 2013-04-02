using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CSS.IM.UI.Control
{
    public class QQGroupChatListViewEx:Panel
    {
        /// <summary>
        /// 打开聊天会议室事件
        /// </summary>
        public delegate void ChatGroupOpenDelegate(CSS.IM.XMPP.Jid jid);
        public event ChatGroupOpenDelegate ChatGroupOpenEvent;

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
                UpdateControl();
            }
        }

        public void AddGroupChat(CSS.IM.XMPP.Jid jid)
        {
            ChatGroupControl chatgroup = new ChatGroupControl(jid);
            chatgroup.FCType = FCType;
            //chatgroup.TextName = jid.Bare;
            chatgroup.Location = new Point(1, (ItemHeight+1) * this.Controls.Count);
            chatgroup.Size = new Size(this.Width - 2, ItemHeight);
            chatgroup.ChatGroupOpenEvent += new ChatGroupControl.ChatGroupOpenDelegate(chatgroup_ChatGroupOpenEvent);

            Controls.Add(chatgroup);
        }

        private void chatgroup_ChatGroupOpenEvent(XMPP.Jid jid)
        {
            if (ChatGroupOpenEvent != null)
                ChatGroupOpenEvent(jid);
        }

        public void UpdateControl()
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                ChatGroupControl item = Controls[i] as ChatGroupControl;
                item.FCType = FCType;
                item.Location = new Point(1, (ItemHeight + 1) * i);
                item.Size = new Size(this.Width - 2, ItemHeight);
            }
        }
    }
}
