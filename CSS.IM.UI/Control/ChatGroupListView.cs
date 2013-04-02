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
    public partial class ChatGroupListView : UserControl
    {

        public delegate void delegate_openChat(Friend sender,string nickName);//打开聊天窗口
        public event delegate_openChat OpenChatEvent;
        private QQContextMenu group_friendcm;

        public delegate void friend_qcm_MouseClick_Delegate(object sender, Jid item, String type);//用于好友右单击事件
        public event friend_qcm_MouseClick_Delegate friend_qcm_MouseClickEvent;

        private Dictionary<string, Jid> _FriendKey;
        public Dictionary<string, Jid> FriendKey
        {

            get 
            {
                if (_FriendKey == null)
                    _FriendKey = new Dictionary<string, Jid>();

                return _FriendKey; 
            }
            set { _FriendKey = value; }
        }

        private Jid _selectFriend;//当前选择的好友
        public Jid SelectedFriend
        {
            get{return _selectFriend;}
            set
            {
                if (_selectFriend != null)
                    OldSelectFriend = _selectFriend;
                _selectFriend = value;
                UpdateFriendControl();
            }
        }


        public Jid OldSelectFriend { set; get; }//上一个好友

        public XmppClientConnection XmppConn{set;get;}

      
        public ChatGroupListView()
        {
            InitializeComponent();
            System.GC.Collect();
        }

        private void UpdateFriendControl()
        {
            if (OldSelectFriend != null)
            {
                GroupFriendControl gfc = (GroupFriendControl)Controls[OldSelectFriend.Bare];
                if (gfc != null)
                {
                    gfc.FCType = FriendContainerType.Small;
                    gfc.IsSelected = false;
                }
            }
            System.GC.Collect();
        }

        public void AddFriend(Jid jid, XmppClientConnection XmppConn)
        {
            if (!FriendKey.ContainsKey(jid.Bare))
            {
                GroupFriendControl friend = new GroupFriendControl(XmppConn,jid);
                friend.FCType = FriendContainerType.Small;
                friend.Location = new Point(1, 36 * this.Controls.Count);
                friend.Size = new Size(this.Width - 2, 35);
                friend.OpenChat += new GroupFriendControl.OpenChatEventHandler(friend_OpenChat);
                friend.ShowContextMenu += new GroupFriendControl.ShowContextMenuEventHandler(friend_ShowContextMenu);
                friend.Selecting += new GroupFriendControl.SelectedEventHandler(friend_Selecting);
                Controls.Add(friend);
                this.Height += 36;
                FriendKey.Add(jid.Bare, jid);
            }
            System.GC.Collect();
        }

        void friend_Selecting(Jid sender)
        {
            SelectedFriend = sender;
            System.GC.Collect();
        }

        private void InitFriendMenu()
        {
            QQToolStripMenuItem item2 = new QQToolStripMenuItem();
            item2.Text = "查看资料";
            item2.MouseDown += new MouseEventHandler(item2_MouseDown);

            group_friendcm = new QQContextMenu();
            group_friendcm.Items.AddRange(new ToolStripItem[] { item2 });
            System.GC.Collect();
        }

        void item2_MouseDown(object sender, MouseEventArgs e)
        {
            if (friend_qcm_MouseClickEvent != null)
            {
                friend_qcm_MouseClickEvent(sender, SelectedFriend, "资料");
            }
            System.GC.Collect();
        }

        public void friend_ShowContextMenu(GroupFriendControl sender, MouseEventArgs e)
        {
            if (group_friendcm == null)
            {
                InitFriendMenu();
            }
            group_friendcm.Show(sender, e.Location);
            System.GC.Collect();
        }

        public void friend_OpenChat(Entity.Friend sender,string nickName)
        {
            if (OpenChatEvent != null)
            {
                OpenChatEvent(sender,nickName);
            }
            System.GC.Collect();
        }

        public void RemoveFroend(Jid jid)
        {
            int index = Controls.IndexOfKey(jid.Bare);
            if (index > -1)
            {
                Controls.RemoveAt(index);
                this.Height -= 36;
                FriendKey.Remove(jid.Bare);
            }

            UpdateControls();
            System.GC.Collect();
        }

        public void UpdateControls()
        {
            try
            {
                List<Jid> CL = new List<Jid>();
                int Count = Controls.Count;
                for (int i = 0; i < Count; i++)
                {
                    Jid jid = Controls[i].Tag as Jid;
                    CL.Add(jid);
                }

                Controls.Clear();
                FriendKey.Clear();

                for (int i = 0; i < CL.Count; i++)
                {
                    AddFriend(CL[i], XmppConn);
                }
            }
            catch (Exception)
            {


            }
            System.GC.Collect();

        }

        public void RefreshFroend(Jid jid)
        {
            int index = Controls.IndexOfKey(jid.Bare);
            if (index != -1)
            {
                GroupFriendControl item = Controls[index] as GroupFriendControl;
                item.UpdateImage();
            }
        }
    }

    
}
