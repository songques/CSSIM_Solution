using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.XMPP;
using CSS.IM.UI.Control;
using System.Diagnostics;
using CSS.IM.UI.Entity;
using CSS.IM.XMPP.protocol.iq.roster;

namespace CSS.IM.App
{
    public partial class ChatMessageBox : MsgBasicForm
    {
        private static MainForm mainForm;
        public static bool IsShow;


        public delegate void delegate_openChat(Friend friend, Jid sender, string CName);//打开聊天窗口
        public event delegate_openChat OpenChatEvent;

        public ChatMessageBox()
        {
            InitializeComponent();
        }

        private static ChatMessageBox Instance = null;

        public static ChatMessageBox GetInstance(MainForm args)
        {
            if (Instance == null)
                Instance = new ChatMessageBox();
            mainForm=args;
            return Instance;
        }

        private void ChatMessageBox_MouseEnter(object sender, EventArgs e)
        {
            mainForm.timer_MessageQueue.Enabled = false;
        }

        int oldPanle = 0;
        public void AddFriendMessage(Jid jid, XmppClientConnection con, string nickname)
        {
            int UserCount = panel_userList.Controls.Count;

            //如果已经有此联系人的消息就不添加
            if (panel_userList.Controls.ContainsKey("FI" + jid.Bare.ToString()))
                return;

            Friend friend = new Friend();
            friend.NikeName = jid.User;
            friend.Description = "";//心情
            friend.HeadIMG = "big194";
            friend.IsSysHead = true;

            MiniFriendControl firneItem = new MiniFriendControl(con, jid);
            firneItem.FriendInfo = friend;
            firneItem.Height = 28;
            firneItem.Width = 210;
            firneItem.Name = "FI" + jid.Bare;
            firneItem.Location = new Point(0, UserCount * 28);
            firneItem.OpenChat += new MiniFriendControl.OpenChatEventHandler(firneItem_OpenChat);
            panel_userList.Controls.Add(firneItem);
            if (panel_userList.Controls.Count>10)
            {
                panel_userList.AutoScroll = true;
            }
            else
            {
                panel_userList.AutoScroll = false;
                if (UserCount * 28 > panel_userList.Height)
                {
                    oldPanle = panel_userList.Height;
                    panel_userList.Height = UserCount * 28;
                    this.Height = this.Height - oldPanle + panel_userList.Height;

                }
            }
        }

        void firneItem_OpenChat(Friend friend,Jid sender, string CName)
        {
            RosterItem ritem = new RosterItem(sender);
            friend.Ritem = ritem;
            if (panel_userList.Controls.ContainsKey("FI" + friend.Ritem.Jid.Bare))
            {
                panel_userList.Controls.RemoveByKey(CName);
                UpdateControls();

                if (OpenChatEvent != null)
                {
                    OpenChatEvent(friend, sender, CName);
                }
            }
            Debug.WriteLine("打开:"+sender.ToString());
        }

        public void RemoveFriend()
        {
            panel_userList.Controls.Clear();
        }

        public void RemoveFriend(Jid jid)
        {
            if (panel_userList.Controls.ContainsKey("FI" + jid.ToString()))
            {
                panel_userList.Controls.RemoveAt(panel_userList.Controls.IndexOfKey("FI" + jid.ToString()));
                UpdateControls();
            }
        }

        public void UpdateControls()
        {
            
            List<MiniFriendControl> mcs = new List<MiniFriendControl>();

            foreach (Control item in  panel_userList.Controls)
            {
                MiniFriendControl mc = item as MiniFriendControl;
                mcs.Add(mc);
            }

            panel_userList.Controls.Clear();

            int UserCount = panel_userList.Controls.Count;
            foreach (MiniFriendControl item in mcs)
            {
                item.Location = new Point(0, UserCount * 28);

                if (UserCount > 10)
                {
                    panel_userList.AutoScroll = true;
                }
                else
                {
                    panel_userList.AutoScroll = false;
                    if (UserCount * 28 > panel_userList.Height)
                    {
                        oldPanle = panel_userList.Height;
                        panel_userList.Height = UserCount * 28;
                        this.Height = this.Height - oldPanle + panel_userList.Height;

                    }
                }
                panel_userList.Controls.Add(item);
            }
        }

        public int OpenChatAll()
        {
            List<MiniFriendControl> box = new List<MiniFriendControl>();

            for (int i = 0; i < panel_userList.Controls.Count; i++)
            {
                box.Add(panel_userList.Controls[i] as MiniFriendControl);
            }

            foreach (MiniFriendControl item in box)
            {
                item.DoubleClick();
            }
            return box.Count;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point screenPoint = MousePosition; //或者Control.MousePosition //屏幕坐标
            Point clientPoint = this.PointToClient(screenPoint);   //窗体坐标
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            if(!rect.Contains(clientPoint))
            {
                mainForm.timer_MessageQueue.Enabled = true;
            }
            else
            {
                mainForm.timer_MessageQueue.Enabled = false;
            }
        }

        public int FrienMessageCount()
        {
            return panel_userList.Controls.Count;
        }
    }
}
