using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.XMPP;
using CSS.IM.XMPP.protocol.client;
using System.Diagnostics;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.XMPP.protocol.iq.disco;
using CSS.IM.XMPP.protocol.iq.rooms;
using CSS.IM.XMPP.protocol.x.data;
using CSS.IM.XMPP.protocol.Base;
using CSS.IM.XMPP.protocol.x.muc;

namespace CSS.IM.App
{
    public partial class ChatGroupRoomsForm : BasicFormNC
    {
        public Jid MJid { set; get; }
        public XmppClientConnection XmppCon;

        /// <summary>
        /// 打开聊天窗体事件
        /// </summary>
        /// <param name="jid"></param>
        /// <param name="pswd"></param>
        public delegate void OpenChatGroupWindowsDelegate(Jid jid, String pswd);
        public event OpenChatGroupWindowsDelegate OpenChatGroupWindowsEvent;

        /// <summary>
        /// 主线程添加列表数据
        /// </summary>
        /// <param name="items"></param>
        public delegate void ListItemsAddDelegate(String[] items,IQ iq);
        ListItemsAddDelegate listItemsAddDelegate = null;


        public ChatGroupRoomsForm(Jid args,XmppClientConnection con)
        {
            this.XmppCon = con;
            MJid = args;
            InitializeComponent();
            this.Text = this.Text + MJid.Bare.ToString();
            listItemsAddDelegate = new ListItemsAddDelegate(listItemsAddMedhod);
        }

        public void listItemsAddMedhod(String[] items, IQ iq)
        {
            foreach (ListViewItem ritem in list_rooms.Items)
            {
                if (ritem.Tag.ToString() == iq.ToString())
                    return;
            }
            ListViewItem iitem = new ListViewItem(items);
            iitem.Tag = iq;
            list_rooms.Items.Add(iitem); 
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChatGroupRoomsForm_Load(object sender, EventArgs e)
        {
            IQ groupIQ = new IQ(IqType.get);
            groupIQ.Id = CSS.IM.XMPP.Id.GetNextId();
            groupIQ.Namespace = null;
            groupIQ.To = MJid;
            CSS.IM.XMPP.protocol.Base.Query query = new CSS.IM.XMPP.protocol.Base.Query();
            query.Namespace = CSS.IM.XMPP.Uri.DISCO_ITEMS;
            groupIQ.AddChild(query);
            XmppCon.IqGrabber.SendIq(groupIQ, new IqCB(GroupCallMethod), null,true);
        }

        public void GroupCallMethod(object sender, IQ iq, object data)
        {

            DiscoItems items = iq.Query as DiscoItems;
            DiscoItem[] itms=items.GetDiscoItems();
            foreach (DiscoItem item in itms)
            {

                //Debug.WriteLine(item.Name);
                //Debug.WriteLine(item.Jid);
                IQ itemIQ = new IQ(IqType.get);
                itemIQ.Namespace = null;
                itemIQ.Id = CSS.IM.XMPP.Id.GetNextId();
                itemIQ.To = item.Jid;
                CSS.IM.XMPP.protocol.Base.Query query = new CSS.IM.XMPP.protocol.Base.Query();
                query.Namespace = CSS.IM.XMPP.Uri.DISCO_INFO;
                itemIQ.AddChild(query);
                XmppCon.IqGrabber.SendIq(itemIQ, new IqCB(ItemCallMethod), null, true);
            }
        }

        public void ItemCallMethod(object sender, IQ iq, object data)
        {
            int popleCount = 0;

            if (InvokeRequired)
            {
                this.BeginInvoke(new IqCB(ItemCallMethod), new object[] { sender,iq,data});
            }
            Debug.WriteLine(iq.ToString());
            ElementList nl1 = iq.Query.SelectElements(typeof(Data));

            foreach (Element e1 in nl1)
            {
                
                Data dt = e1 as Data;
                ElementList nl2=dt.SelectElements(typeof(Field));

                foreach (Element e2 in nl2)
                {
                    Field field=e2 as Field;
                    if ("占有者人数" == field.Label)
                    {
                        popleCount = int.Parse(field.GetValue());
                    }
                }
            }

            string name = "";
            if (iq.Query.SelectElements("identity").Count>0)
            {
                try
                {
                    name=iq.Query.SelectElements("identity").Item(0).GetAttribute("name");
                }
                catch (Exception)
                {
                    name = iq.From.User;
                }
            }
           

            this.Invoke(listItemsAddDelegate, new object[] { new String[] { "", name, iq.From.Bare, popleCount.ToString() }, iq });
                
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            list_rooms.Items.Clear();
            ChatGroupRoomsForm_Load(null, null);
        }

        ChatGroupRoomCrateForm crateForm = null;
        private void btn_crate_Click(object sender, EventArgs e)
        {
            if (crateForm==null||crateForm.IsDisposed)
            {
                crateForm = new ChatGroupRoomCrateForm(MJid, XmppCon);
                //聊天室创建完成事件
                crateForm.CreateRoomOverEvent += new ChatGroupRoomCrateForm.CreateRoomOverDelegate(crateForm_CreateRoomOverEvent);
            }
            try
            {
                crateForm.Show();
            }
            catch (Exception)
            {

            }
            
        }

        private void crateForm_CreateRoomOverEvent(Jid jid, string pswd)
        {
            if (InvokeRequired)
            {
                this.Invoke(new ChatGroupRoomCrateForm.CreateRoomOverDelegate(crateForm_CreateRoomOverEvent), new object[] {jid,pswd });
            }

            if (OpenChatGroupWindowsEvent!=null)
            {
                OpenChatGroupWindowsEvent(jid, pswd);
            }
            else
            {
                MsgBox.Show(this, "CSS&IM", "聊天室创建完成！", MessageBoxButtons.OK);
            }

            try
            {
                if (crateForm != null && !crateForm.IsDisposed)
                    crateForm.Close();
            }
            catch (Exception)
            {
                
            }
            
        }

        private void list_rooms_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IQ iq = list_rooms.Items[list_rooms.SelectedIndices[0]].Tag as IQ;
            IQ useChatIQ = new IQ();
            useChatIQ.Namespace = null;
            useChatIQ.Id = CSS.IM.XMPP.Id.GetNextId();
            useChatIQ.To = iq.From;
            useChatIQ.Type = IqType.get;

            Query useQuery = new Query();
            useQuery.Namespace = CSS.IM.XMPP.Uri.DISCO_INFO;
            useChatIQ.AddChild(useQuery);
            XmppCon.IqGrabber.SendIq(useChatIQ, new IqCB(UseChatGetInfo), null,true);
        }

        ChatsGroupPasswordForm pswd_form = new ChatsGroupPasswordForm();
        private void UseChatGetInfo(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {
                this.Invoke(new IqCB(UseChatGetInfo),new object[]{sender,iq,data});
            }
            pswd_form.iq = iq;
            pswd_form.ChatsPasswordSetEvent += new ChatsGroupPasswordForm.ChatsPasswordSetDelegate(pswd_form_ChatsPasswordSetEvent);
            bool isPassword = true;
            for (int i = 0; i < iq.Query.SelectElements("feature").Count; i++)
            {
                if (iq.Query.SelectElements("feature").Item(i).Attribute("var") == "muc_unsecured")
                {
                    isPassword = false;
                }
            }
            if (isPassword)
            {

                if (pswd_form.IsDisposed)
                {
                    pswd_form = new ChatsGroupPasswordForm();
                }
                try
                {
                    pswd_form.Show();
                }
                catch (Exception)
                {

                }
                

            }
            else
            {

                //<presence id="OUw29-39" to="1111@conference.songques-pc/songques">
                //    <x xmlns="http://jabber.org/protocol/muc"></x>
                //    <x xmlns="vcard-temp:x:update"><photo>50bc87cccff6f129652471a2b1196c97203f5abb</photo></x>
                //    <x xmlns="jabber:x:avatar"><hash>50bc87cccff6f129652471a2b1196c97203f5abb</hash></x>
                //</presence>

                if (OpenChatGroupWindowsEvent!=null)
                {
                    OpenChatGroupWindowsEvent(iq.From, "");
                }
            }
        }

        void pswd_form_ChatsPasswordSetEvent(IQ iq,string pswd)
        {
            if (OpenChatGroupWindowsEvent != null)
            {
                OpenChatGroupWindowsEvent(iq.From, pswd);
            }
            
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (list_rooms.SelectedIndices == null)
                return;
            if (list_rooms.SelectedIndices.Count<=0)
                return;
            IQ iq = list_rooms.Items[list_rooms.SelectedIndices[0]].Tag as IQ;
            IQ useChatIQ = new IQ();
            useChatIQ.Namespace = null;
            useChatIQ.Id = CSS.IM.XMPP.Id.GetNextId();
            useChatIQ.To = iq.From;
            useChatIQ.Type = IqType.get;

            Query useQuery = new Query();
            useQuery.Namespace = CSS.IM.XMPP.Uri.DISCO_INFO;
            useChatIQ.AddChild(useQuery);
            XmppCon.IqGrabber.SendIq(useChatIQ, new IqCB(UseChatGetInfo), null, true);
        }
    }
}
