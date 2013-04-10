using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CSS.IM.UI.Control;
using CSS.IM.UI.Util;
using CSS.IM.XMPP;
using CSS.IM.XMPP.protocol.client;
using CSS.IM.XMPP.protocol.iq.disco;
using CSS.IM.App.Settings;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.UI.Form;
using System.Threading;
using CSS.IM.XMPP.protocol.iq.roster;
using CSS.IM.UI.Entity;
using System.Net;
using CSS.IM.XMPP.protocol.iq.vcard;
using CSS.IM.XMPP.protocol.x.data;
using System.Diagnostics;
using System.Data.OleDb;
using CSS.IM.Library.Data;
using System.IO;
using System.Runtime.InteropServices;

namespace CSS.IM.App
{
    public partial class MainForm : IQQMainForm
    {

        private LoginWaiting waiting = null;
        private LoginFrom login = null;
        private User login_user=null;

        private String filename = "";//用于组织结构保存的文件名称
        private Element orgsOld =new Element();
        private TreeNode Selectnode = null;//用于保存选取后的treenode

        private Dictionary<String, String> userDic = new Dictionary<String, String>();

        public XmppClientConnection XmppCon = new XmppClientConnection();
        public DiscoManager discoManager;

        private Dictionary<String, List<CSS.IM.XMPP.protocol.client.Message>> msgBox = new Dictionary<string, List<XMPP.protocol.client.Message>>();

        private List<Point> point_MessageQueue = new List<Point>();//记录消息盒子事件捕捉的坐标点
        public System.Windows.Forms.Timer timer_MessageQueue = new System.Windows.Forms.Timer();//消息盒子事件捕捉

        private ChatGroupRoomsForm rooms;//聊天室打开窗体
        private SetInfoForm setInfoForm;//设置个人信息窗体
        private SetingForm setingForm;//设置功能
        private MessageLogForm messageLogForm;//聊天记记录
        private FindFriendForm findFriendForm;//查找好友
        private VcardInfoForm vcardForm;//查看名片
        private MoveFriendGroup moveFriendGroup;//好友级移动

        private bool Notify_Message_Alert = false;//消息在任务栏提醒

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pres"></param>
        private delegate void OnPresenceDelegate(object sender, Presence pres);
        /// <summary>
        /// 打开消息窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        private delegate void OnMessageDelegate(object sender, CSS.IM.XMPP.protocol.client.Message msg);

        /// <summary>
        /// 添加聊天室的代理
        /// </summary>
        /// <param name="jid"></param>
        private delegate void OnChatGroupAddDelegate(Jid jid);

        /// <summary>
        /// 注销事件
        /// </summary>
        private delegate void LogoutDelegate(bool ISAutoLogin, bool isLogin);

        /// <summary>
        /// Socket打开事件
        /// </summary>
        private delegate void XmppSocketOpenDelegate();

        /// <summary>
        /// 好友列表更新完成的事件
        /// </summary>
        private delegate void RosterEndDelegate();

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
        const int WM_COPYDATA = 0x004A;  

        public MainForm()
        {
            InitializeComponent();
            btn_default_index.Image = CSS.IM.UI.Properties.Resources.Wireless;
            btn_mail.Image = ResClass.GetImgRes("mail");
            btn_color.Image = ResClass.GetImgRes("colour");


            btn_fd.Image = ResClass.GetImgRes("MainPanel_ContactMainTabButton_texture");
            btn_gp.Image = ResClass.GetImgRes("MainPanel_GroupMainTabButton_texture1");
            btn_nt.Image = ResClass.GetImgRes("role_tabBtn_Normal");
            btn_lt.Image = ResClass.GetImgRes("MainPanel_RecentMainTabButton_texture1");
            btn_tools.Image = ResClass.GetImgRes("Tools");
            btn_message.Image = ResClass.GetImgRes("message");
            btn_find.Image = ResClass.GetImgRes("find");
            btn_searh_clear.Image = ResClass.GetImgRes("btn_clear");
            panel_fd.Location = panel_gp.Location = panel_nt.Location = panel_lt.Location = new Point(2, 150);
            panel_Search.Location = new Point(2, 125);
            listView_fd.Size= panel_fd.Size = panel_gp.Size = panel_nt.Size = panel_lt.Size = new Size(this.Width - 4, this.Height - 235 + 50);
            panel_Search.Size = new Size(this.Width - 4, this.Height - 235+77);
            //panel_fd.Size = panel_gp.Size = panel_nt.Size = panel_lt.Size = new Size(this.Width - 4, 100);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btn_fd_Click(null, null);
            Initial();

            listView_fd.FCType = FriendContainerType.Small;
            chatHistory_lt.FCType = listView_fd.FCType;
            listView_gp.FCType = listView_fd.FCType;
            qqHistoryListViewEx_panel_Search.FCType = listView_fd.FCType;

            #region 注册快捷按键
            string[] keys = CSS.IM.UI.Util.Path.GetOutMsgKeyTYpe.Split('+');
            if (keys.Length == 2)
            {
                HotKey.RegisterHotKey(Handle, 101, Util.GetFunctionKeyValue(keys[1]), Util.GetKeyValue(keys[0].Trim()));
            }
            else if (keys.Length == 3)
            {
                HotKey.RegisterHotKey(Handle, 101, Util.GetFunctionKeyValue(keys[1]) | Util.GetFunctionKeyValue(keys[2]), Util.GetKeyValue(keys[0]));
            }

            keys = null;
            keys = CSS.IM.UI.Util.Path.ScreenKeyTYpe.Split('+');
            if (keys.Length == 2)
            {
                HotKey.RegisterHotKey(Handle, 102, Util.GetFunctionKeyValue(keys[1]), Util.GetKeyValue(keys[0].Trim()));
            }
            else if (keys.Length == 3)
            {
                HotKey.RegisterHotKey(Handle, 102, Util.GetFunctionKeyValue(keys[1]) | Util.GetFunctionKeyValue(keys[2]), Util.GetKeyValue(keys[0]));
            }


            Util.MainHandle = Handle;
            #endregion;

            Util.HrefOpenFriendEvent += Util_HrefOpenFriendEvent;

        }

        void Util_HrefOpenFriendEvent(string sender)
        {
            MessageBox.Show(sender);
        }

        /// 
        /// 监视Windows消息
        /// 重载WndProc方法，用于实现热键响应
        /// 
        ///
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_HOTKEY = 0x0312;//如果m.Msg的值为0x0312那么表示用户按下了热键
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        //case 100:    //按下的是Shift+S
                        //    //此处填写快捷键响应代码         
                        //    break;
                        case 101:    //按下的是Ctrl+B
                            System.Diagnostics.Debug.WriteLine("获取消息功能！");
                            int count = ChatMessageBox.GetInstance(this).OpenChatAll();//打开所有聊天窗体
                            notifyIcon_MessageQueue.Icon = CSS.IM.UI.Properties.Resources.Iico;
                            timer_MessageQueue.Enabled = false;
                            ChatMessageBox.GetInstance(this).RemoveFriend();
                            if (count > 0)
                            {
                                return;
                            }
                            break;
                        case 102:    //按下的是Ctrl+Alt+S

                            if (Util.ScreenImage)
                                return;

                            Util.ScreenImage = true;
                            using (CSS.IM.Library.Controls.CaptureImageTool capture = new CSS.IM.Library.Controls.CaptureImageTool())
                            {
                                try
                                {
                                    capture.ShowDialog();
                                    if (Util.TO_Jid != null)
                                    {
                                        ChatFromMsg chatform = Util.ChatForms[Util.TO_Jid.Bare] as ChatFromMsg;
                                        if (chatform != null)
                                        {
                                            chatform.AddScreenImageEvent(capture.Image);
                                            chatform.WindowState = FormWindowState.Normal;
                                            chatform.Activate();
                                        }
                                    }
                                    capture.Dispose();
                                    Util.ScreenImage = false;
                                }
                                catch (Exception)
                                {

                                }
                            }
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            int tw = (Width - 4) / 4;

            if (btn_fd == null)//如果是在没有表单实例化完成的时候就退出事件
                return;

            btn_fd.Width = tw;
            btn_gp.Left = btn_fd.Left + btn_fd.Width;
            btn_gp.Width = tw;
            btn_nt.Left = btn_gp.Left + btn_gp.Width;
            btn_nt.Width = tw;
            btn_lt.Left = btn_nt.Left + btn_nt.Width;
            btn_lt.Width = Width - btn_lt.Left - 2;
            //search_Btn.Left = Width - search_Btn.Width - 3;
            panel_fd.Location = panel_gp.Location = panel_nt.Location = panel_lt.Location = new Point(2, 151);
            panel_Search.Location = new Point(2, 125);
            //panel_fd.Location = panel_gp.Location = panel_nt.Location = panel_lt.Location = new Point(2, 127);
            listView_fd.Size = panel_fd.Size = panel_gp.Size = panel_nt.Size = panel_lt.Size = new Size(this.Width - 4, this.Height - 235 + 50);
            panel_Search.Size = new Size(this.Width - 4, this.Height - 235 + 77);
            //panel_fd.Size = panel_gp.Size = panel_nt.Size = panel_lt.Size = new Size(this.Width - 4, this.Height - 211 + 50);
            //friendListView.Size = groupListView.Size = pal_tree.Size = lastListView.Size = new Size(this.Width - 4, this.Height - 211);//设置 好友列表区的大小
            //menu_Btn.Top = Height - menu_Btn.Height - 10;
            btn_tools.Top = Height - btn_tools.Height - 10;
            btn_message.Top = Height - btn_message.Height - 10;
            btn_find.Top = Height - btn_find.Height - 10;
        }

        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                    Type mytype = mystr.GetType();
                    mystr = (COPYDATASTRUCT)m.GetLParam(mytype);

                    //MessageBox.Show(mystr.lpData);
                    Jid openJid = new Jid(mystr.lpData, XmppCon.Server,null);
                    //MessageBox.Show(openJid.ToString());

                    Friend item=null;
                    if (listView_fd.Rosters.ContainsKey(openJid.Bare))
                    {
                        item = listView_fd.Rosters[openJid.Bare];
                        if (!Util.ChatForms.ContainsKey(item.Ritem.Jid.Bare))
                        {
                            try
                            {
                                string nickName = listView_fd.GetFriendNickName(openJid.Bare);
                                ChatFromMsg chat = new ChatFromMsg(openJid, XmppCon, nickName);
                                chat.UpdateFriendOnline(item.IsOnline);//设置好友在线状态
                                chat.Show();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            try
                            {
                                ChatFromMsg chatform = Util.ChatForms[item.Ritem.Jid.Bare] as ChatFromMsg;
                                chatform.WindowState = FormWindowState.Normal;
                                chatform.Activate();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        if (!Util.ChatForms.ContainsKey(openJid.Bare))
                        {
                            listView_fd.UpdateFriendFlicker(openJid.Bare);
                            string nickName = listView_fd.GetFriendNickName(openJid.Bare);
                            ChatFromMsg chat = new ChatFromMsg(openJid, XmppCon, nickName);

                            Friend friend;
                            if (listView_fd.Rosters.ContainsKey(openJid.Bare))
                            {
                                friend = listView_fd.Rosters[openJid.Bare];
                            }
                            else
                            {
                                friend = null;
                            }

                            if (friend != null)
                            {
                                chat.UpdateFriendOnline(listView_fd.Rosters[openJid.Bare].IsOnline);
                            }

                            if (msgBox.ContainsKey(openJid.Bare.ToString()))
                            {
                                chat.FristMessage(msgBox[openJid.Bare.ToString()]);
                                msgBox.Remove(openJid.Bare.ToString());
                            }
                            try
                            {
                                chat.Show();
                            }
                            catch (Exception)
                            {

                            }

                        }
                        else
                        {
                            try
                            {
                                ChatFromMsg chatform = Util.ChatForms[openJid.Bare.ToString()] as ChatFromMsg;
                                chatform.WindowState = FormWindowState.Normal;
                                chatform.Activate();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("打开窗体错误："+ex.Message);
                            }
                        }
                    }

                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }  

        private void MainForm_CloseEvent()
        {

        }

        private void btn_fd_Click(object sender, EventArgs e)
        {
            btn_fd.SelectTab = true;
            panel_fd.Visible = true;
            btn_gp.SelectTab = false;
            panel_gp.Visible = false;
            btn_nt.SelectTab = false;
            panel_nt.Visible = false;
            btn_lt.SelectTab = false;
            panel_lt.Visible = false;
            System.GC.Collect();
        }

        private void btn_gp_Click(object sender, EventArgs e)
        {
            btn_fd.SelectTab = false;
            panel_fd.Visible = false;
            btn_gp.SelectTab = true;
            panel_gp.Visible = true;
            btn_nt.SelectTab = false;
            panel_nt.Visible = false;
            btn_lt.SelectTab = false;
            panel_lt.Visible = false;
            System.GC.Collect();
        }

        private void btn_nt_Click(object sender, EventArgs e)
        {
            btn_fd.SelectTab = false;
            panel_fd.Visible = false;
            btn_gp.SelectTab = false;
            panel_gp.Visible = false;
            btn_nt.SelectTab = true;
            panel_nt.Visible = true;
            btn_lt.SelectTab = false;
            panel_lt.Visible = false;
            System.GC.Collect();


            //treeView_nt.Nodes.Clear();
            treeView_nt.BackColor = Color.White;
            treeView_nt.Width = panel_nt.Width - 2;
            treeView_nt.Height = panel_nt.Height - 2;
            treeView_nt.Scrollable = true;
            if (filename == null || filename.Trim().Length == 0)
                filename = "new";
            IQ tree_iq = new IQ(IqType.get);
            tree_iq.Id = CSS.IM.XMPP.Id.GetNextId();
            tree_iq.Namespace = null;
            CSS.IM.XMPP.protocol.Base.Query query = new CSS.IM.XMPP.protocol.Base.Query();
            query.Attributes.Add("filename", filename);
            query.Namespace = "xmlns:org:tree";
            tree_iq.AddChild(query);
            XmppCon.IqGrabber.SendIq(tree_iq, new IqCB(TreeResulit), null);

        }

        private void btn_lt_Click(object sender, EventArgs e)
        {
            btn_fd.SelectTab = false;
            panel_fd.Visible = false;
            btn_gp.SelectTab = false;
            panel_gp.Visible = false;
            btn_nt.SelectTab = false;
            panel_nt.Visible = false;
            btn_lt.SelectTab = true;
            panel_lt.Visible = true;


            OleDbDataReader dr = null;
            try
            {
                dr = OleDb.ExSQLReDr("select Jid from ChatMessageLog where Belong='" + XmppCon.MyJID.Bare.ToString() + "' group by Jid");
                chatHistory_lt.RemoveFriendAll();
                while (dr.Read())
                {

                    string jid = dr.GetString(0);

                    Friend flfriend = listView_fd.Rosters[new Jid(jid).Bare];

                    chatHistory_lt.XmppConnection = XmppCon;
                    chatHistory_lt.AddFriend(flfriend);
                }
            }
            catch (Exception)
            {
                dr = null;
            }
            System.GC.Collect();
        }

        private void btn_state_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                btn_state_Menu.Show(this, btn_state.Left, btn_state.Top + 20);
            }
            System.GC.Collect();
        }

        /// <summary>
        /// 基本初使化
        /// </summary>
        private void Initial()
        {
            Util.VcardChangeEvent += new Util.ObjectHandler(Util_VcardChangeEvent);//更新个个资料
            Util.SetInfoFormEvent += new Util.ObjectHandler(Util_SetInfoFormEvent);//打开个人资料
            Util.SetingFormEvent += new Util.ObjectHandler(Util_SetingFormEvent);//打开设置信息

            timer_MessageQueue.Interval = 250;
            timer_MessageQueue.Enabled = true;
            timer_MessageQueue.Tick += new EventHandler(timer_MessageQueue_Tick);

            btn_state.State = 1;
            this.Hide();
            this.ShowInTaskbar = false;

            XmppCon.SocketConnectionType = CSS.IM.XMPP.net.SocketConnectionType.Direct;//使用TCP/IP的方式进行连接
            //XmppCon.ClientSocket.ConnectTimeout = 1000;
            XmppCon.OnReadXml += new XmlHandler(XmppCon_OnReadXml);//发送数据的XML
            XmppCon.OnWriteXml += new XmlHandler(XmppCon_OnWriteXml);//接收数据的XML


            XmppCon.OnRosterStart += new ObjectHandler(XmppCon_OnRosterStart);//获取联系人列表
            XmppCon.OnRosterEnd += new ObjectHandler(XmppCon_OnRosterEnd);//获取列表结束
            XmppCon.OnRosterItem += new CSS.IM.XMPP.XmppClientConnection.RosterHandler(XmppCon_OnRosterItem);//得到列表项目

            //XmppCon.OnAgentStart += new ObjectHandler(XmppCon_OnAgentStart);//代理
            //XmppCon.OnAgentEnd += new ObjectHandler(XmppCon_OnAgentEnd);
            //XmppCon.OnAgentItem += new agsXMPP.XmppClientConnection.AgentHandler(XmppCon_OnAgentItem);

            XmppCon.OnLogin += new ObjectHandler(XmppCon_OnLogin);//登录
            XmppCon.OnClose += new ObjectHandler(XmppCon_OnClose);
            XmppCon.OnError += new ErrorHandler(XmppCon_OnError);//错误
            XmppCon.OnPresence += new PresenceHandler(XmppCon_OnPresence);//状态
            XmppCon.OnMessage += new MessageHandler(XmppCon_OnMessage);//消息
            XmppCon.OnIq += new IqHandler(XmppCon_OnIq);//ID

            //XmppCon.OnAuthError += new XmppElementHandler(XmppCon_OnAuthError);//登录错误
            XmppCon.OnSocketError += new ErrorHandler(XmppCon_OnSocketError);
            //XmppCon.OnStreamError += new XmppElementHandler(XmppCon_OnStreamError);

            XmppCon.OnReadSocketData += new CSS.IM.XMPP.net.BaseSocket.OnSocketDataHandler(ClientSocket_OnReceive);//查看socket接收数据
            XmppCon.OnWriteSocketData += new CSS.IM.XMPP.net.BaseSocket.OnSocketDataHandler(ClientSocket_OnSend);//查看socket发送数据
            //XmppCon.ClientSocket.OnValidateCertificate += new System.Net.Security.RemoteCertificateValidationCallback(ClientSocket_OnValidateCertificate);//SSL验证


            //XmppCon.OnXmppConnectionStateChanged += new XmppConnectionStateHandler(XmppCon_OnXmppConnectionStateChanged);
            //XmppCon.OnSaslStart += new SaslEventHandler(XmppCon_OnSaslStart);

            ////discoManager = new DiscoManager(XmppCon);

            XmppCon.OnAuthError += new XmppElementHandler(XmppCon_OnAuthError);
            discoManager = new DiscoManager(XmppCon);

            CSS.IM.XMPP.Factory.ElementFactory.AddElementType("UserRemark", null, typeof(Settings.TUserRemark));//用户备注表

            CSS.IM.XMPP.Factory.ElementFactory.AddElementType("Login", null, typeof(Settings.Login));//注册login类
            CSS.IM.XMPP.Factory.ElementFactory.AddElementType("Paths", null, typeof(Settings.Paths));//注册Paths类
            CSS.IM.XMPP.Factory.ElementFactory.AddElementType("ServerInfo", null, typeof(Settings.ServerInfo));//注册ServerInfo类
            //保存和管理字体功能
            CSS.IM.XMPP.Factory.ElementFactory.AddElementType("SFont", null, typeof(Settings.SFont));//注册SColor类
            CSS.IM.XMPP.Factory.ElementFactory.AddElementType("SColor", null, typeof(Settings.SColor));//注册SColor类

            CSS.IM.XMPP.Factory.ElementFactory.AddElementType("HistoryLogin", null, typeof(Settings.HistoryLogin));//注册HistoryLogin类，用于最后的登录记录功能


            //XmppCon.Server = "DUBIN-PC";
            //XmppCon.Server = "192.168.0.44";
            //XmppCon.Server = "192.168.0.102";

            //XmppCon.Server = System.Net.Dns.GetHostByAddress("6.136.8.14").HostName.ToString();
            //XmppCon.Server = "192.168.0.36";

            //XmppCon.Username = "ccbbaa";
            //XmppCon.Password = "1";
            //XmppCon.Resource = "CSS.IM.App";
            XmppCon.Resource = "Spark 2.6.3";
            XmppCon.Priority = 10;
            //XmppCon.UseSSL=false;
            XmppCon.AutoResolveConnectServer = true;
            XmppCon.UseCompression = false;
            //XmppCon.RegisterAccount = true;  //是否注册.
            XmppCon.EnableCapabilities = true;
            XmppCon.ClientVersion = Program.Vsion;
            XmppCon.Capabilities.Node = "http://www.css.com.cn/";

            //System.IO.MemoryStream memory = new MemoryStream();
            //this.Icon.Save(memory);

            //FileStream fileStream = new FileStream("C:\\a.jpg", FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
            //fileStream.Write(memory.ToArray(), 0, memory.ToArray().Length);
            //fileStream.Flush();
            //fileStream.Close();

            timer_autoHide.Enabled = false;
            timer_autoHide.Interval = 200;
            ShowInTaskbar = false;
            ChatMessageBox.GetInstance(this).OpenChatEvent += new ChatMessageBox.delegate_openChat(QQMainForm_OpenChatEvent);

            try
            {
                login = new LoginFrom(this, false);
                login.ISAutoLogin = true;
                login.Login_Event += new LoginFrom.LoginDelegate(login_Login_Event);
                login.Show();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 打开个人设置
        /// </summary>
        /// <param name="sender"></param>
        private void Util_SetingFormEvent(object sender)
        {
            btn_tools_MouseClick(null, null);
        }

        /// <summary>
        /// 打开个人资料
        /// </summary>
        /// <param name="sender"></param>
        private void Util_SetInfoFormEvent(object sender)
        {
            userImg_MouseClick(null, null);
        }

        /// <summary>
        /// 通过消息盒子打开消息窗体
        /// </summary>
        /// <param name="friend"></param>
        /// <param name="sender"></param>
        /// <param name="CName"></param>
        void QQMainForm_OpenChatEvent(Friend friend, Jid sender, string CName)
        {
            RosterItem ritem = new RosterItem(sender);
            friend.Ritem = ritem;
            //throw new NotImplementedException();
            //friendListView_OpenChatEvent(friend);
            if (!Util.ChatForms.ContainsKey(friend.Ritem.Jid.Bare))
            {
                listView_fd.UpdateFriendFlicker(friend.Ritem.Jid.Bare);

                string nickName = listView_fd.GetFriendNickName(friend.Ritem.Jid.Bare);
                ChatFromMsg chat = new ChatFromMsg(friend.Ritem.Jid, XmppCon, nickName);

                Friend flfriend = listView_fd.Rosters[friend.Ritem.Jid.Bare];
                if (flfriend != null)
                {
                    chat.UpdateFriendOnline(flfriend.IsOnline);
                }

                if (msgBox.ContainsKey(friend.Ritem.Jid.Bare.ToString()))
                {
                    chat.FristMessage(msgBox[friend.Ritem.Jid.Bare.ToString()]);
                    msgBox.Remove(friend.Ritem.Jid.Bare.ToString());
                }
                chat.Show();
            }
            else
            {
                try
                {
                    ChatFromMsg chatform = Util.ChatForms[friend.Ritem.Jid.Bare] as ChatFromMsg;
                    chatform.WindowState = FormWindowState.Normal;
                    chatform.Activate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (ChatMessageBox.GetInstance(this).FrienMessageCount() > 0)
                Debug.WriteLine("?");
            else
            {
                notifyIcon_MessageQueue.Icon = CSS.IM.UI.Properties.Resources.Iico;
                timer_MessageAlert.Enabled = false;
            }

        }

        /// <summary>
        /// 监听任务栏图标鼠标移动保存坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_MessageQueue_Tick(object sender, EventArgs e)
        {
            if (MessageQueuePointRectangle().Contains(MousePosition))
            {
                if (ChatMessageBox.GetInstance(this).FrienMessageCount() > 0)
                {
                    ChatMessageBox.GetInstance(this).Show();
                    ChatMessageBox.IsShow = true;
                }
            }
            else
            {
                if (!timer_autoHide.Enabled)
                    timer_autoHide.Enabled = true;
                ChatMessageBox.GetInstance(this).Hide();
                ChatMessageBox.IsShow = false;
            }
            timer_MessageQueue.Enabled = false;
        }

        /// <summary>
        /// 对比消息盒子事件
        /// </summary>
        /// <returns></returns>
        public Rectangle MessageQueuePointRectangle()
        {
            Point TopLeft = new Point(10000, 10000);
            Point BottomRight = new Point(-10000, -10000);
            foreach (Point curPoint in point_MessageQueue)
            {
                if (curPoint.X < TopLeft.X)
                {
                    TopLeft.X = curPoint.X;
                }

                if (curPoint.Y < TopLeft.Y)
                {
                    TopLeft.Y = curPoint.Y;
                }

                if (curPoint.X > BottomRight.X)
                {
                    BottomRight.X = curPoint.X;
                }

                if (curPoint.Y > BottomRight.Y)
                {
                    BottomRight.Y = curPoint.Y;
                }
            }

            return new Rectangle(TopLeft, new Size(BottomRight.X - TopLeft.X, BottomRight.Y - TopLeft.Y));
        }

        /// <summary>
        /// 名片改变事件
        /// </summary>
        /// <param name="sender"></param>
        private void Util_VcardChangeEvent(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(Util_VcardChangeEvent), new object[] { sender });
                return;
            }

            XmppCon.SendMyPresence();

            try
            {

                this.NikeName = Util.vcard.Nickname.Trim() == "" ? this.NikeName : Util.vcard.Nickname;
                description.Text = Util.vcard.Description;
                userImg.Image = Util.vcard.Photo.Image;
            }
            catch (Exception)
            {

            }


        }

        /// <summary>
        /// 任务图标双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MessageQueue_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (timer_MessageQueue.Enabled)
            {

                int count = ChatMessageBox.GetInstance(this).OpenChatAll();//打开所有聊天窗体

                notifyIcon_MessageQueue.Icon = CSS.IM.UI.Properties.Resources.Iico;
                timer_MessageQueue.Enabled = false;
                ChatMessageBox.GetInstance(this).RemoveFriend();
                if (count > 0)
                {
                    return;
                }
            }
            timer_autoHide.Enabled = false;

            this.TopMost = true;
            this.Show();
            this.Activate();
            this.TopMost = false;

            if ((anchors & AnchorStyles.Left) == AnchorStyles.Left)
            {
                Left = 0;
            }
            if ((anchors & AnchorStyles.Top) == AnchorStyles.Top)
            {
                Top = 0;
            }
            if ((anchors & AnchorStyles.Right) == AnchorStyles.Right)
            {
                Left = Screen.PrimaryScreen.Bounds.Width - Width;
            }
            if ((anchors & AnchorStyles.Bottom) == AnchorStyles.Bottom)
            {
                Top = Screen.PrimaryScreen.Bounds.Height - Height;
            }
        }

        /// <summary>
        /// 保存消息盒子坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MessageQueue_MouseMove(object sender, MouseEventArgs e)
        {
            // 显示提示窗体
            timer_MessageQueue.Enabled = true;

            Point MouseAt = new Point(MousePosition.X, MousePosition.Y);
            if (!point_MessageQueue.Contains(MouseAt))
            {
                point_MessageQueue.Add(MouseAt);
            }

            // 获得屏幕的宽
            Screen screen = Screen.PrimaryScreen;
            int screenWidth = screen.Bounds.Width;

            // 获得工作区域的高
            int workAreaHeight = Screen.PrimaryScreen.WorkingArea.Height;

            // 获得提示窗体的宽和高
            int toolTipWidth = ChatMessageBox.GetInstance(this).Width;
            int toolTipHeight = ChatMessageBox.GetInstance(this).Height;

            // 那么提示窗体的左上角坐标就是：屏幕的宽 - 提示窗体的宽， 工作区域的高 - 提示窗体的高

            if (!ChatMessageBox.IsShow)
            {
                ChatMessageBox.GetInstance(this).Location = new Point(MousePosition.X - toolTipWidth / 2, workAreaHeight - toolTipHeight);
            }


        }

        /// <summary>
        /// 重新登录功能
        /// </summary>
        /// <param name="ISAutoLogin">是否读取配置文件的自动登录</param>
        ///<param name="isLogin">是否显示错误登录功能</param>
        public void LogOut(bool ISAutoLogin, bool isLogin)
        {
            this.Hide();
            timer_MessageQueue.Enabled = false;
            timer_autoHide.Enabled = false;
            try
            {
                XmppCon.OnClose -= new ObjectHandler(XmppCon_OnClose);
                XmppCon.OnError -= new ErrorHandler(XmppCon_OnError);//错误
                XmppCon.Close();
            }
            catch (Exception)
            {

            }
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            listView_fd.Rosters.Clear();
            listView_fd.Groups.Clear();
            listView_fd.Controls.Clear();
            try
            {
                login = new LoginFrom(this, isLogin);
                login.ISAutoLogin = ISAutoLogin;//不从配置中读取设置
                login.Login_Event += new LoginFrom.LoginDelegate(login_Login_Event);
                login.Show();

                XmppCon.OnClose += new ObjectHandler(XmppCon_OnClose);
                XmppCon.OnError += new ErrorHandler(XmppCon_OnError);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 登录窗体登录返回事件
        /// </summary>
        /// <param name="user"></param>
        private void login_Login_Event(User user)
        {
            login_user = user;//保存登陆成功后的用户
            XmppCon.Username = user.UserName;
            XmppCon.Password = user.PassWord;
            waiting = new LoginWaiting();
            try
            {
                waiting.Show();
            }
            catch (Exception)
            {

            }

            if (Program.ServerIP == null || Program.Port == null)
            {
                MsgBox.Show(waiting, "CSS&IM", "服务器地址错误！", MessageBoxButtons.OK);
                waiting.Hide();
                LogOut(false, false);
                return;
            }

            XmppCon.Port = int.Parse(Program.Port);
            XmppCon.Server =Program.ServerIP;
            this.Hide();
            this.ShowInTaskbar = false;
            new Thread(new ThreadStart(OpenSocket)).Start();
        }

        /// <summary>
        /// 登录的线程
        /// </summary>
        public void OpenSocket()
        {
            try
            {
                XmppCon.Open();
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                this.Invoke(new XmppSocketOpenDelegate(XmppSocketOpenMedhot));
            }
            finally
            {
            }

        }

        /// <summary>
        /// 登陆线程的代理事件
        /// </summary>
        public void XmppSocketOpenMedhot()
        {
            if (InvokeRequired)
            {
                this.Invoke(new XmppSocketOpenDelegate(XmppSocketOpenMedhot));
            }

            if (XmppCon.XmppConnectionState != XmppConnectionState.Disconnected)
                XmppCon.Close();

            MsgBox.Show(waiting, "CSS&IM", "登录失败，服务器无法识别！", MessageBoxButtons.OK);

            waiting.Close();
            try
            {
                LogOut(false, false);
            }
            catch (Exception)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// 添加IM会议室事件
        /// </summary>
        /// <param name="jid"></param>
        public void listView_gp_Item_Add(Jid jid)
        {
            if (Util.Services.Finds.Contains(jid))
            {
                return;
            }
            listView_gp.Location = new Point(1, 3);
            listView_gp.BackColor = Color.White;
            listView_gp.Width = panel_fd.Width - 2;
            listView_gp.Height = panel_fd.Height - 4;
            if (InvokeRequired)
            {
                BeginInvoke(new OnChatGroupAddDelegate(listView_gp_Item_Add), new object[] { jid });
            }

            listView_gp.AddGroupChat(jid);

            //ChatGroupControl chatgroup = new ChatGroupControl();
            //chatgroup.FCType = listView_fd.FCType;
            //chatgroup.MJid = jid;
            //chatgroup.TextName = jid.ToString();
            //chatgroup.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            //chatgroup.BackColor = this.BackColor;
            //chatgroup.Location = new Point(1, 56 * listView_gp.Controls.Count);
            //chatgroup.Name = jid.ToString();
            //chatgroup.Size = new Size(listView_gp.Width - 2, 55);
            //chatgroup.BackColor = Color.White;
            //chatgroup.ChatGroupOpenEvent += new ChatGroupControl.ChatGroupOpenDelegate(listView_gp_Item_Open);

            ////friend.FriendInfo = item;
            ////friend.Selecting += new FriendControl.SelectedEventHandler(friend_Selecting);
            ////friend.ShowContextMenu += new FriendControl.ShowContextMenuEventHandler(friend_ShowContextMenu);
            ////friend.OpenChat += new FriendControl.OpenChatEventHandler(friend_OpenChat);
            ////friend.Conn = _connection;
            ////friend.UpdateImage();//更新头像信息
            //listView_gp.Controls.Add(chatgroup);
            ////pal_chatGroupRef.BackColor = Color.Aqua;
            /////pal_chatGroupRef.Height += 56;
            ////UpdateLayout(panel_user);
        }

        /// <summary>
        /// IM会议室打开事件
        /// </summary>
        /// <param name="jid"></param>
        private void listView_gp_Item_Open(Jid jid)
        {
            if (rooms == null || rooms.IsDisposed)
            {
                rooms = new ChatGroupRoomsForm(jid, XmppCon);
                //打开聊天室窗体事件
                rooms.OpenChatGroupWindowsEvent += new ChatGroupRoomsForm.OpenChatGroupWindowsDelegate(rooms_OpenChatGroupWindowsEvent);
            }
            try
            {
                rooms.Show();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 从房间列表的打开房间事件开发
        /// </summary>
        /// <param name="jid"></param>
        /// <param name="pswd"></param>
        private void rooms_OpenChatGroupWindowsEvent(Jid jid, string pswd)
        {

            if (InvokeRequired)
            {
                this.Invoke(new ChatGroupRoomsForm.OpenChatGroupWindowsDelegate(rooms_OpenChatGroupWindowsEvent), new object[] { jid, pswd });
            }

            if (!Util.GroupChatForms.ContainsKey(jid.Bare))
            {
                try
                {
                    ChatGroupFormMsg chat = new ChatGroupFormMsg(jid, XmppCon, XmppCon.MyJID.User);
                    chat.Initial(pswd);
                }
                catch (Exception)
                {


                }

            }
        }

        /// <summary>
        /// 消息在任务栏图片交替提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_MessageAlert_Tick(object sender, EventArgs e)
        {
            if (Notify_Message_Alert)
            {
                notifyIcon_MessageQueue.Icon = CSS.IM.UI.Properties.Resources.Imessage;
                Notify_Message_Alert = false;
            }
            else
            {
                notifyIcon_MessageQueue.Icon = CSS.IM.UI.Properties.Resources.Iico;
                Notify_Message_Alert = true;
            }
        }

        /// <summary>
        /// 查看联系人详细信息事件、删除、备注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item"></param>
        public void listView_fd_friend_qcm_MouseClickEvent(object sender, Friend item, String type)
        {

            CSS.IM.XMPP.protocol.iq.roster.RosterItem ritem = null;

            if (listView_fd.SelectFriend!=null)
            {
                ritem = listView_fd.SelectFriend.Ritem;
            }

            Document doc_setting = new Document();
            Settings.Settings config = new Settings.Settings();
            Settings.Paths path = null;

            switch (type)
            {
                case "vcar":
                    if (vcardForm == null || vcardForm.IsDisposed)
                    {
                        vcardForm = new VcardInfoForm(ritem.Jid, XmppCon);
                    }
                    vcardForm.Show();
                    vcardForm.Activate();

                    break;
                case "chat":
                    if (!Util.ChatForms.ContainsKey(item.Ritem.Jid.Bare))
                    {
                        try
                        {
                            string nickName = listView_fd.GetFriendNickName(item.Ritem.Jid.Bare);
                            ChatFromMsg chat = new ChatFromMsg(item.Ritem.Jid, XmppCon, nickName);
                            chat.UpdateFriendOnline(item.IsOnline);//设置好友在线状态
                            chat.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else
                    {
                        try
                        {
                            ChatFromMsg chatform = Util.ChatForms[item.Ritem.Jid.Bare] as ChatFromMsg;
                            chatform.WindowState = FormWindowState.Normal;
                            chatform.Activate();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    break;
                case "dele":
                    Jid jid = item.Ritem.Jid;
                    Friend friend = new Friend();
                    friend.NikeName = item.Ritem.Jid.User;
                    friend.Description = "";//心情
                    friend.HeadIMG = "big1";
                    friend.IsSysHead = true;
                    friend.State = 1;

                    if (item.Ritem.GetGroups().Count > 0)
                    {
                        CSS.IM.XMPP.protocol.Base.Group g = (CSS.IM.XMPP.protocol.Base.Group)item.Ritem.GetGroups().Item(0);
                        int groupID = 0;

                        foreach (var groups in listView_fd.Groups)
                        {
                            if (groups.Value.Title == g.Name)
                            {
                                groupID = groups.Value.Id;
                            }
                        }
                        //if (groupID == 0)
                        //{
                        //    friendListView.AddGroup(g.Name);
                        //    groupID = friendListView.Groups[g.Name].Id;
                        //    friendListView.UpdateLayout(3, groupID);

                        //}
                        friend.GroupID = groupID;
                        friend.GroupName = g.Name;
                    }
                    else
                    {
                        //离线联系人
                        Group group = listView_fd.Groups["我的联系人"];
                        friend.GroupID = group.Id;
                        friend.GroupName = group.Title;
                    }
                    friend.Ritem = item.Ritem;
                    friend.IsOnline = false;

                    DialogResult msgResult = MsgBox.Show(this, "CSS&IM", "确认要删除联系人么？", MessageBoxButtons.YesNo);

                    if (msgResult == DialogResult.Yes)
                    {
                        listView_fd.RemoveFriend(friend);


                        try
                        {
                            RosterIq riq = new RosterIq();
                            riq.Type = IqType.set;
                            XmppCon.RosterManager.RemoveRosterItem(jid);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    break;
                case "move":

                    if (moveFriendGroup == null || moveFriendGroup.IsDisposed)
                    {
                        moveFriendGroup = new MoveFriendGroup();
                    }
                    Dictionary<string, Group> group_args = listView_fd.Groups;
                    string[] strvalue = new string[group_args.Count];
                    int index = 0;
                    foreach (String keystr in group_args.Keys)
                    {
                        Group groupargs = group_args[keystr];
                        strvalue[index] = groupargs.Title;
                        index++;

                    }

                    moveFriendGroup.basicComboBox1.Items = strvalue;
                    moveFriendGroup.basicComboBox1.SelectIndex = 0;
                    DialogResult reslut = moveFriendGroup.ShowDialog();

                    String groupupdate = moveFriendGroup.basicComboBox1.SelectItem.ToString();

                    String Bare_move = listView_fd.SelectFriend.Ritem.Jid.Bare;
                    if (reslut == DialogResult.Yes)
                    {

                        foreach (String user_key in listView_fd.Rosters.Keys)
                        {
                            if (listView_fd.Rosters[user_key].Ritem.Jid.Bare == Bare_move)
                            {


                                Friend friend_old = listView_fd.Rosters[user_key];
                                listView_fd.RemoveFriend(listView_fd.Rosters[user_key].Ritem.Jid.Bare);

                                Group fgroup = listView_fd.Groups[groupupdate];
                                friend_old.GroupID = fgroup.Id;
                                friend_old.GroupName = fgroup.Title;

                                listView_fd.AddFriend(friend_old);
                                listView_fd.RefreshGroup();
                                break;

                            }
                        }

                        CSS.IM.XMPP.protocol.Base.Group group_move = new CSS.IM.XMPP.protocol.Base.Group(groupupdate);
                        CSS.IM.XMPP.protocol.Base.Item item_move = new CSS.IM.XMPP.protocol.Base.Item();
                        item_move.Namespace = null;
                        item_move.AddChild(group_move);
                        item_move.SetAttribute("jid", listView_fd.SelectFriend.Ritem.Jid);
                        item_move.SetAttribute("subscripton", "from");

                        CSS.IM.XMPP.protocol.Base.Query query_move = new CSS.IM.XMPP.protocol.Base.Query();
                        query_move.Namespace = CSS.IM.XMPP.Uri.IQ_ROSTER;
                        query_move.AddChild(item_move);

                        IQ iq = new IQ(IqType.set);
                        iq.GenerateId();
                        iq.Namespace = null;
                        iq.AddChild(query_move);

                        XmppCon.IqGrabber.SendIq(iq);


                    }
                    break;
                case "g_dele":

                    break;
                case "remark":

                    //RemarkFriendGroup _RemarkFriendGroup = new RemarkFriendGroup();
                    //Dictionary<string, Group> group_args = friendListView.Groups;
                    //string[] strvalue = new string[group_args.Count];
                    //int index = 0;
                    //foreach (String keystr in group_args.Keys)
                    //{
                    //    Group groupargs = group_args[keystr];
                    //    strvalue[index] = groupargs.Title;
                    //    index++;

                    //}

                    //_MoveFriendGroup.basicComboBox1.Items = strvalue;
                    //_MoveFriendGroup.basicComboBox1.SelectIndex = 0;
                    //DialogResult reslut = _MoveFriendGroup.ShowDialog();

                    //String groupupdate = _MoveFriendGroup.basicComboBox1.SelectItem.ToString();

                    //String name_move = friendListView.SelectedFriend.Ritem.Jid.User;
                    //if (reslut == DialogResult.Yes)
                    //{

                    //    foreach (String user_key in friendListView.Rosters.Keys)
                    //    {
                    //        if (friendListView.Rosters[user_key].Ritem.Jid.User == name_move)
                    //        {


                    //            Friend friend_old = friendListView.Rosters[user_key];
                    //            friendListView.RemoveFriend(friendListView.Rosters[user_key].Ritem.Jid.User);

                    //            Group fgroup = friendListView.Groups[groupupdate];
                    //            friend_old.GroupID = fgroup.Id;
                    //            friend_old.GroupName = fgroup.Title;

                    //            friendListView.AddFriend(friend_old);

                    //            friendListView.UpdateLayout(3, 0);
                    //            break;

                    //        }
                    //    }


                    //    CSS.IM.XMPP.protocol.Base.Group group_move = new CSS.IM.XMPP.protocol.Base.Group(groupupdate);
                    //    CSS.IM.XMPP.protocol.Base.Item item_move = new CSS.IM.XMPP.protocol.Base.Item();
                    //    item_move.Namespace = null;
                    //    item_move.AddChild(group_move);
                    //    item_move.SetAttribute("jid", friendListView.SelectedFriend.Ritem.Jid);
                    //    item_move.SetAttribute("subscripton", "from");

                    //    CSS.IM.XMPP.protocol.Base.Query query_move = new CSS.IM.XMPP.protocol.Base.Query();
                    //    query_move.Namespace = CSS.IM.XMPP.Uri.IQ_ROSTER;
                    //    query_move.AddChild(item_move);

                    //    IQ iq = new IQ(IqType.set);
                    //    iq.GenerateId();
                    //    iq.Namespace = null;
                    //    iq.AddChild(query_move);

                    //    XmppCon.IqGrabber.SendIq(iq);
                    break;
                case "HeadSmall":
                    //listView_fd.FCType = FriendContainerType.Small;
                    CSS.IM.UI.Util.Path.FriendContainerType = false;
                    doc_setting.LoadFile(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
                    path = doc_setting.RootElement.SelectSingleElement(typeof(Settings.Paths),false) as Settings.Paths;
                    path.FriendContainerType = CSS.IM.UI.Util.Path.FriendContainerType;
                    config.Paths = path;
                    config.Font = doc_setting.RootElement.SelectSingleElement(typeof(Settings.SFont), false) as Settings.SFont;
                    config.Color = doc_setting.RootElement.SelectSingleElement(typeof(Settings.SColor), false) as Settings.SColor;

                    doc_setting.Clear();
                    doc_setting.ChildNodes.Add(config);

                    try
                    {
                        doc_setting.Save(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
                    }
                    catch (Exception)
                    {
                        
                    }

                    listView_fd.FCType = CSS.IM.UI.Util.Path.FriendContainerType == true ? FriendContainerType.Big : FriendContainerType.Small;
                    chatHistory_lt.FCType = listView_fd.FCType;
                    listView_gp.FCType = listView_fd.FCType; 
                    break;
                case "HeadBig":
                    //listView_fd.FCType = FriendContainerType.Small;
                    CSS.IM.UI.Util.Path.FriendContainerType = true;
                    doc_setting.LoadFile(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
                    path = doc_setting.RootElement.SelectSingleElement(typeof(Settings.Paths),false) as Settings.Paths;
                    path.FriendContainerType = CSS.IM.UI.Util.Path.FriendContainerType;
                    config.Paths = path;
                    config.Font = doc_setting.RootElement.SelectSingleElement(typeof(Settings.SFont), false) as Settings.SFont;
                    config.Color = doc_setting.RootElement.SelectSingleElement(typeof(Settings.SColor), false) as Settings.SColor;

                    doc_setting.Clear();
                    doc_setting.ChildNodes.Add(config);
                    try
                    {
                        doc_setting.Save(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
                    }
                    catch (Exception)
                    {
                        
                    }
                    listView_fd.FCType = CSS.IM.UI.Util.Path.FriendContainerType == true ? FriendContainerType.Big : FriendContainerType.Small;
                    chatHistory_lt.FCType = listView_fd.FCType;
                    listView_gp.FCType = listView_fd.FCType; 
                    break;
                default:
                    break;
            }


        }

        /// <summary>
        /// 聊天窗口打开事件
        /// </summary>
        /// <param name="sender"></param>
        public void listView_fd_OpenChatEvent(Friend sender)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new QQListViewEx.delegate_openChat(listView_fd_OpenChatEvent), new object[] { sender });
                return;
            }

            System.GC.Collect();


            //for (int i = 0; i < 50; i++)
            //{
            //    ChatFromMsg chat = new ChatFromMsg(sender.Ritem.Jid, XmppCon);
            //    chat.Show();
            //}

            if (!Util.ChatForms.ContainsKey(sender.Ritem.Jid.Bare))
            {
                try
                {
                    string nickName = listView_fd.GetFriendNickName(sender.Ritem.Jid.Bare);
                    ChatFromMsg chat = new ChatFromMsg(sender.Ritem.Jid, XmppCon, nickName);
                    chat.UpdateFriendOnline(sender.IsOnline);//设置好友在线状态
                    ChatMessageBox.GetInstance(this).RemoveFriend(sender.Ritem.Jid);
                    if (msgBox.ContainsKey(sender.Ritem.Jid.ToString()))
                    {
                        chat.FristMessage(msgBox[sender.Ritem.Jid.ToString()]);
                        msgBox.Remove(sender.Ritem.Jid.ToString());
                    }
                    chat.Show();
                    if (ChatMessageBox.GetInstance(this).FrienMessageCount() > 0)
                        Debug.WriteLine("?");
                    else
                    {
                        notifyIcon_MessageQueue.Icon = CSS.IM.UI.Properties.Resources.Iico;
                        timer_MessageAlert.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                try
                {
                    ChatFromMsg chatform = Util.ChatForms[sender.Ritem.Jid.Bare] as ChatFromMsg;
                    chatform.WindowState = FormWindowState.Normal;
                    chatform.Activate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            System.GC.Collect();
        }

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userImg_MouseClick(object sender, MouseEventArgs e)
        {
            if (setInfoForm == null || setInfoForm.IsDisposed)
            {
                setInfoForm = new SetInfoForm(XmppCon);
                setInfoForm.update_top_image_event += new SetInfoForm.update_top_image(update_head_portrait);
            }
            setInfoForm.Show();
            setInfoForm.Activate();
        }

        /// <summary>
        /// 组织结构节点双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_nt_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Selectnode = treeView_nt.SelectedNode;

            if (treeView_nt.SelectedNode == null)
                return;
            if (treeView_nt.SelectedNode.Tag == null)
                return;
            if (treeView_nt.SelectedNode.Tag == treeView_nt.Nodes[0].Tag)
                return;
            if (treeView_nt.SelectedNode != null)
            {
                if (treeView_nt.SelectedNode.Tag != null)
                {
                    tsmi发送消息_Click(null, null);
                }
            }
        }

        /// <summary>
        /// 更新头像事件
        /// </summary>
        private void update_head_portrait()
        {
            VcardIq viq = new VcardIq(IqType.get, null, new Jid(XmppCon.MyJID.User));
            XmppCon.IqGrabber.SendIq(viq, new IqCB(VcardResult), null);
            if (setInfoForm != null)
            {
                setInfoForm.Dispose();
                setInfoForm = null;
            }
        }

        #region 用户状态更改事件
        private void 我在线上ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_state.State = 1;
            if (XmppCon != null && XmppCon.Authenticated)
            {
                XmppCon.Show = ShowType.NONE;
                XmppCon.SendMyPresence();
            }
        }

        private void 忙碌ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_state.State = 4;
            if (XmppCon != null && XmppCon.Authenticated)
            {
                XmppCon.Show = ShowType.dnd;
                XmppCon.SendMyPresence();
            }
        }

        private void 离开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_state.State = 2;
            if (XmppCon != null && XmppCon.Authenticated)
            {
                XmppCon.Show = ShowType.away;
                XmppCon.SendMyPresence();
            }
        }
        #endregion

        #region 修改个人备注事件
        private void description_MouseClick(object sender, MouseEventArgs e)
        {
            QQtextBox tb = new QQtextBox();
            tb.MaxLength = 100;
            tb.BorderStyle = BorderStyle.Fixed3D;
            tb.Text = description.Text;
            tb.Location = new Point(description.Left, description.Top - 2);
            tb.Size = new Size(description.Width, description.Height + 4);
            tb.Leave += new EventHandler(tb_Leave);
            tb.KeyDown += new KeyEventHandler(tb_KeyDown);
            tb.LostFocus += new EventHandler(tb_LostFocus);
            Controls.Add(tb);
            tb.BringToFront();
            tb.Focus();
        }

        private void tb_LostFocus(object sender, EventArgs e)
        {
            description.Focus();
        }


        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                description.Focus();
        }

        private void tb_Leave(object sender, EventArgs e)
        {
            bool change = true;

            QQtextBox tb = sender as QQtextBox;
            if (description.Text == tb.Text)
            {
                change = false;
            }
            else
            {
                change = true;
                description.Text = tb.Text;
            }
            tb.Dispose();
            Controls.Remove(tb);

            if (change)
            {
                Util.vcard.Description = description.Text;
                VcardIq viq = new VcardIq(IqType.set, null, new Jid(XmppCon.MyJID.User), Util.vcard);
                XmppCon.IqGrabber.SendIq(viq, new IqCB(SaveVcardResult), null);
            }
        }

        private void SaveVcardResult(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new IqCB(SaveVcardResult), new object[] { sender, iq, data });
                return;
            }

            if (iq.Type == IqType.result)
            {
                Util.VcardChangeEventMethod();//通知名片更新事件
                MsgBox.Show(this, "CSS&IM", "备注保存成功！", MessageBoxButtons.OK);
            }
            else
            {
                MsgBox.Show(this, "CSS&IM", "备注保存失败", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region 组合结构所用到的方法

        public void TreeResulit(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {
                this.Invoke(new IqCB(TreeResulit), new object[] { sender, iq, data });
            }

            try
            {

                Element orgs = iq.Query.FirstChild;
                if (orgs==null)
                {
                    return;
                }
                if (orgsOld.ToString()!=orgs.ToString())
                {
                    
                    orgsOld = orgs;
                    treeView_nt.Nodes.Clear();
                    TreeNode root_treenode = new TreeNode(orgs.GetAttribute("orgName").ToString(), 0, 0);
                    //root_treenode.Tag = iq.Query.GetAttribute("filename").ToString();
                    root_treenode.Tag = null;

                    treeView_nt.Nodes.Add(root_treenode);

                    //ElementList org_list = orgs.FirstChild
                    DescTreeGroup(orgs, root_treenode);

                    List<int> selectNodeIndex = new List<int>();
                    if (Selectnode != null)
                    {
                        for (TreeNode i = Selectnode; i.Parent != null; i = i.Parent)
                        {
                            selectNodeIndex.Insert(0, i.Index);
                        }

                        TreeNode selecNodeout = treeView_nt.Nodes[0];
                        selecNodeout.Expand();
                        for (int i = 0; i < selectNodeIndex.Count; i++)
                        {
                            selecNodeout = selecNodeout.Nodes[selectNodeIndex[i]];
                            selecNodeout.Expand();

                        }
                    }

               }
            }
            catch (Exception)
            {

            }

        }

        public void DescTreeGroup(Element root_item, TreeNode treeindex)
        {
            int elemntLength = root_item.ChildNodes.Count;
            for (int i = 0; i < elemntLength; i++)
            {
                Element item = root_item.ChildNodes.Item(i) as Element;
                if (item != null)
                {
                    if (item.TagName == "user")
                    {
                        string loginName = item.GetAttribute("loginName");
                        if (loginName != null)
                        {
                            string userName = item.GetAttribute("username");
                            TreeNode node = new TreeNode(userName, 2, 2);
                            node.Tag = loginName;
                            treeindex.Nodes.Add(node);

                            userDic.Add(loginName, userName);
                        }

                    }
                    else if (item.TagName == "org")
                    {
                        string userName = item.GetAttribute("orgName");
                        TreeNode node = new TreeNode(userName, 1, 1);
                        int ctreeindex = treeindex.Nodes.Add(node);
                        DescTreeGroup(item, node);
                    }

                }
            }



            //String loginName = "";
            //String userName = "";
            //for (int i = 0; i < OrgList.Count; i++)
            //{

            //    ElementList user_list = OrgList.Item(i).SelectElements("user");
            //    for (int j = 0; j < user_list.Count; j++)
            //    {
            //        if (user_list.Item(j).Attributes.Count > 0)
            //        {
            //            loginName = user_list.Item(j).GetAttribute("loginName");
            //            userName = user_list.Item(j).GetAttribute("username");
            //            TreeNode node = new TreeNode(userName, 2, 2);
            //            node.Tag = loginName;
            //            treeindex.Nodes.Add(node);

            //        }
            //    }

            //    ElementList org_list = OrgList.Item(i).SelectElements("org");
            //    for (int j = 0; j < org_list.Count; j++)
            //    {
            //        if (org_list.Item(j).Attributes.Count > 0)
            //        {
            //            userName = org_list.Item(j).GetAttribute("orgName");
            //            TreeNode node = new TreeNode(userName, 1, 1);
            //            int ctreeindex = treeindex.Nodes.Add(node);
            //            MarkOrgTOList(org_list, node);
            //        }
            //    }
            //}
        }

        #endregion

        #region XMPP事件

        private void VcardResult(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new IqCB(VcardResult), new object[] { sender, iq, data });
                return;
            }
            if (iq.Type == IqType.result)
            {
                Vcard vcard = iq.Vcard;

                Util.vcard = vcard;

                if (vcard != null)
                {
                    //Program.NikName = vcard.Nickname;
                    Photo photo = vcard.Photo;
                    if (photo != null)
                        userImg.Image = vcard.Photo.Image;
                }
                description.Text = vcard.Description;
                this.NikeName = vcard.Nickname.Trim() == "" ? this.NikeName : vcard.Nickname.Trim();
            }
        }

        public void DiscoServer()
        {
            discoManager.DiscoverItems(new Jid(XmppCon.Server), new IqCB(OnDiscoServerResult), null);
        }

        public void OnDiscoServerResult(object sender, IQ iq, object data)
        {
            if (iq.Type == IqType.result)
            {
                Element query = iq.Query;
                if (query != null && query.GetType() == typeof(DiscoItems))
                {
                    DiscoItems items = query as DiscoItems;
                    DiscoItem[] itms = items.GetDiscoItems();

                    foreach (DiscoItem itm in itms)
                    {
                        if (itm.Jid != null)
                            discoManager.DiscoverInformation(itm.Jid, new IqCB(OnDiscoInfoResult), itm);
                    }
                }
            }
        }

        public void OnDiscoInfoResult(object sender, IQ iq, object data)
        {
            // <iq from='proxy.cachet.myjabber.net' to='gnauck@jabber.org/Exodus' type='result' id='jcl_19'>
            //  <query xmlns='http://jabber.org/protocol/disco#info'>
            //      <identity category='proxy' name='SOCKS5 Bytestreams Service' type='bytestreams'/>
            //      <feature var='http://jabber.org/protocol/bytestreams'/>
            //      <feature var='http://jabber.org/protocol/disco#info'/>
            //  </query>
            // </iq>

            if (iq.Type == IqType.result)
            {
                if (iq.Query is DiscoInfo)
                {
                    DiscoInfo di = iq.Query as DiscoInfo;
                    Jid jid = iq.From;
                    if (di.HasFeature(CSS.IM.XMPP.Uri.IQ_SEARCH))
                    {
                        if (!Util.Services.Search.Contains(jid))
                        {

                            if (!Util.Services.Rooms.Contains(jid))
                            {
                                if ("conference" == di.SelectElements("identity").Item(0).GetAttribute("category"))
                                {
                                    Util.Services.Rooms.Add(jid);

                                }
                            }


                            if (!Util.Services.Finds.Contains(jid))
                            {
                                if ("directory" == di.SelectElements("identity").Item(0).GetAttribute("category"))
                                {
                                    Util.Services.Finds.Add(jid);
                                }
                            }

                            Util.Services.Search.Add(jid);
                            this.Invoke(new OnChatGroupAddDelegate(listView_gp_Item_Add), new object[] { jid });//添加聊天室事件
                        }

                    }
                    else if (di.HasFeature(CSS.IM.XMPP.Uri.BYTESTREAMS))
                    {
                        if (!Util.Services.Proxy.Contains(jid))
                            Util.Services.Proxy.Add(jid);
                    }

                }
            }

        }

        public void XmppCon_OnReadXml(object sender, string xml)
        {
            //Debug.WriteLine("XmppCon_OnReadXml:" + xml + "\n");
        }

        public void XmppCon_OnWriteXml(object sender, string xml)
        {
            //Debug.WriteLine("XmppCon_OnWriteXml:" + xml + "\n");
        }

        public void XmppCon_OnClose(object sender)
        {
            this.Invoke(new LogoutDelegate(LogOut), new object[] { false, true });
        }

        public void XmppCon_OnError(object sender, Exception ex)
        {
            this.Invoke(new LogoutDelegate(LogOut), new object[] { false, true });
        }

        public void XmppCon_OnRosterStart(object sender)
        {
            //friendListView.Update();
            CSS.IM.UI.Util.Path.MsgSwitch = false;
            CSS.IM.UI.Util.Path.SystemSwitch = false;
            CSS.IM.UI.Util.Path.CallSwitch = false;
            CSS.IM.UI.Util.Path.FolderSwitch = false;
            CSS.IM.UI.Util.Path.GlobalSwitch = false;
            CSS.IM.UI.Util.Path.InputAlertSwitch = false;
        }

        public void XmppCon_OnRosterItem(object sender, CSS.IM.XMPP.protocol.iq.roster.RosterItem item)
        {

            if (InvokeRequired)
            {
                BeginInvoke(new CSS.IM.XMPP.XmppClientConnection.RosterHandler(XmppCon_OnRosterItem), new object[] { this, item });
                return;
            }
            if (item.Jid.Bare == null)
            {
                return;
            }
            if (item.Subscription != SubscriptionType.remove)
            {


                Friend friend = new Friend();
                friend.NikeName = item.Jid.User;
                friend.Description = "";//心情
                friend.HeadIMG = "big194";
                friend.IsSysHead = true;
                friend.State = 1;

                if (item.GetGroups().Count > 0)
                {
                    CSS.IM.XMPP.protocol.Base.Group g = (CSS.IM.XMPP.protocol.Base.Group)item.GetGroups().Item(0);
                    int groupID = 0;

                    foreach (var groups in listView_fd.Groups)
                    {
                        if (groups.Value.Title == g.Name)
                        {
                            groupID = groups.Value.Id;
                        }
                    }
                    if (groupID == 0)
                    {
                        listView_fd.AddGroup(g.Name);
                        groupID = listView_fd.Groups[g.Name].Id;
                        listView_fd.UpdateLayout(3, groupID);

                    }
                    friend.GroupID = groupID;
                    friend.GroupName = g.Name;
                }
                else
                {
                    //离线联系人
                    Group group = listView_fd.Groups["我的联系人"];
                    friend.GroupID = group.Id;
                    friend.GroupName = group.Title;

                    try
                    {
                        if (listView_fd.Rosters.ContainsKey(item.Jid.Bare))
                            return;
                    }
                    catch (Exception)
                    {

                        //throw ex;
                    }

                }
                friend.Ritem = item;
                friend.IsOnline = false;

                try
                {
                    listView_fd.AddFriend(friend);
                }
                catch (Exception)
                {

                    //throw ex;
                }

            }
            else
            {
                listView_fd.RemoveFriend(item.Jid.Bare);
            }
        }

        public void XmppCon_OnRosterEnd(object sender)
        {
           if (InvokeRequired)
            {
                this.Invoke(new ObjectHandler(XmppCon_OnRosterEnd), new object[] { sender });
            }

            this.Invoke(new RosterEndDelegate(RosterEndMethod));
            CSS.IM.UI.Util.Path.MsgSwitch = true;
            CSS.IM.UI.Util.Path.SystemSwitch = true;
            CSS.IM.UI.Util.Path.CallSwitch = true;
            CSS.IM.UI.Util.Path.FolderSwitch = true;
            CSS.IM.UI.Util.Path.GlobalSwitch = true;
            CSS.IM.UI.Util.Path.InputAlertSwitch = true;
        }

        private void RosterEndMethod()
        {
            listView_fd.LoadRosterEnd();
        }

        public void XmppCon_OnPresence(object sender, Presence pres)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new OnPresenceDelegate(XmppCon_OnPresence), new object[] { sender, pres });
                return;
            }

            if (pres.Type == PresenceType.subscribe)
            {
                RequestFriendForm f = new RequestFriendForm(XmppCon, pres.From);
                f.TopMost = true;
                try
                {
                    f.Show();
                }
                catch (Exception)
                {

                }

            }
            else if (pres.Type == PresenceType.subscribed)
            {

            }
            else if (pres.Type == PresenceType.unsubscribe)
            {

            }
            else if (pres.Type == PresenceType.unsubscribed)
            {

            }
            else
            {
                try
                {
                    
                    listView_fd.RefreshFriend(pres.From.Bare, pres.Type, pres.Show);

                    if (pres.Type == XMPP.protocol.client.PresenceType.unavailable)
                    {
                        if (Util.ChatForms.ContainsKey(pres.From.Bare))
                        {
                            ((ChatFromMsg)Util.ChatForms[pres.From.Bare]).UpdateFriendOnline(false);
                        }

                        chatHistory_lt.UpdateFriendInfoOnline(pres.From, false);
                    }
                    else
                    {
                        if (Util.ChatForms.ContainsKey(pres.From.Bare))
                        {
                            ((ChatFromMsg)Util.ChatForms[pres.From.Bare]).UpdateFriendOnline(true);
                        }
                        chatHistory_lt.UpdateFriendInfoOnline(pres.From, true);
                    }

                    //Util.GroupPresenceEventMethod(pres.From);
                }
                catch (Exception)
                {

                }
            }
        }

        public void XmppCon_OnMessage(object sender, CSS.IM.XMPP.protocol.client.Message msg)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                Invoke(new OnMessageDelegate(XmppCon_OnMessage), new object[] { sender, msg });
                return;
            }

            // Dont handle GroupChat Messages here, they have their own callbacks in the
            // GroupChat Form
            if (msg.Type == MessageType.groupchat)
            {
                //Debug.WriteLine("创建聊天室发送了消息:"+msg.Body);
                return;
            }
            if (msg.Type == MessageType.error)
            {
                //Handle errors here
                // we dont handle them in this example
                return;
            }
            //if (msg.Type == MessageType.normal)
            //{
            //    filename = msg.Body;
            //    Debug.WriteLine(msg.ToString());
            //    return;
            //}

            // check for xData Message

            if (msg.HasTag(typeof(Data)))//如果是文件
            {
                Element e = msg.SelectSingleElement(typeof(Data));
                Data xdata = e as Data;
                if (xdata.Type == XDataFormType.form)
                {
                    //frmXData fXData = new frmXData(xdata);
                    //fXData.Text = "xData Form from " + msg.From.ToString();
                    //fXData.Show();
                }
            }
            else if (msg.HasTag(typeof(CSS.IM.XMPP.protocol.extensions.ibb.Data)))
            {
                // ignore IBB messages
                return;
            }
            else
            {
                //if (msg.Body != null)
                //{
                    if (msg.GetTag("subject") == "notify")//xu
                    {
                        filename = msg.Body;
                        return;
                    }
                    else if(msg.GetTag("subject") == "homepage")
                    {
                        Msg.MqMessage mqmsg = MessageBoxForm.MarkMessage_Mq(msg);
                        CSS.IM.UI.Util.Path.DefaultURL = mqmsg.Herf + "?url=" + mqmsg.Url + "&token=" + mqmsg.Token + "&loginname=" +XmppCon.Username +"&pwd=" + Base64.EncodeBase64(XmppCon.Password);
                        
                       return;
                    }
                    else if (msg.GetTag("subject") == "window")
                    {
                        if (CSS.IM.UI.Util.Path.ReveiveSystemNotification)//是否接收服务器消息
                        {
                            if (CSS.IM.UI.Util.Path.SystemSwitch)
                                SoundPlayEx.MsgPlay(CSS.IM.UI.Util.Path.SystemPath);

                            MessageBoxForm sBox = new MessageBoxForm("系统通知", msg,XmppCon.Username, XmppCon.Password);

                            try
                            {
                                sBox.TopMost = true;
                                sBox.Show();
                            }
                            catch (Exception)
                            {

                            }
                            string sqlstr = "insert into MessageLog (Belong,MessageType,MessageLog,[DateNow]) values ({0},{1},{2},{3})";
                            sqlstr = String.Format(sqlstr,
                                "'" + XmppCon.MyJID.Bare.ToString() + "'",
                                "'0'",
                                "'" + msg.ToString() + "'",
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                            CSS.IM.Library.Data.OleDb.ExSQL(sqlstr);
                        }
                        return;
                    }
                    if (msg.From.ToString() == msg.From.Server)
                    {
                        if (CSS.IM.UI.Util.Path.ReveiveSystemNotification)//是否接收服务器消息
                        {
                            if (CSS.IM.UI.Util.Path.SystemSwitch)
                                SoundPlayEx.MsgPlay(CSS.IM.UI.Util.Path.SystemPath);

                            MessageBoxForm sBox = new MessageBoxForm("系统通知", msg, XmppCon.Username,XmppCon.Password);
                            
                            try
                            {
                                sBox.TopMost = true;
                                sBox.Show();
                            }
                            catch (Exception)
                            {

                            }

                            string sqlstr = "insert into MessageLog (Belong,MessageType,MessageLog,[DateNow]) values ({0},{1},{2},{3})";
                            sqlstr = String.Format(sqlstr,
                                "'" + XmppCon.MyJID.Bare.ToString() + "'",
                                "'0'",
                                "'" + msg.ToString() + "'",
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                            CSS.IM.Library.Data.OleDb.ExSQL(sqlstr);
                        }
                    }
                    else
                    {

                        if (!Util.ChatForms.ContainsKey(msg.From.Bare))//查看聊天窗口是否已经打开了
                        {

                            //RosterNode rn = rosterControl.GetRosterItem(msg.From);
                            //string nick = msg.From.Bare;
                            //if (rn != null)
                            //    nick = rn.Text;
                            try
                            {
                                //string sqlstr = "insert into ChatMessageLog (Jid,[MessageLog],[DateNow])values ({0},{1},{2})";
                                //sqlstr = String.Format(sqlstr,
                                //    "'" + msg.From.Bare.ToString() + "'",
                                //    "'" + msg.ToString() + "'",
                                //     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                                //CSS.IM.Library.Data.OleDb.ExSQL(sqlstr);
                                /**
                                 * 0、普通消息
                                 * 8、ftp离线文件
                                 * 9、对方拒绝接收主线文件
                                 * 11、接收到red的视频请求
                                 * */
                                if (msg.GetTagInt("m_type") == 0 || msg.GetTagInt("m_type") == 8 || msg.GetTagInt("m_type") == 9 || msg.GetTagInt("m_type") == 11)//如果为0就是正常消息，就播放声音
                                {
                                    if (CSS.IM.UI.Util.Path.MsgSwitch)
                                        SoundPlayEx.MsgPlay(CSS.IM.UI.Util.Path.MsgPath);
                                }
                                else//代码进入到这里，说明，聊天的窗口已经关闭，接收到属于聊天窗口里面的业务不做处理
                                {
                                    //6发图片
                                    //1接收视频

                                    if (!(msg.GetTagInt("m_type") == 6 || msg.GetTagInt("m_type") == 1))
                                    {
                                        return;
                                    }
                                }

                                //MessageBox.Show("1");
                                Friend flfriend=null;
                                if (listView_fd.Rosters.Count > 0)
                                {
                                    try
                                    {
                                        flfriend = listView_fd.Rosters[msg.From.Bare];
                                    }
                                    catch (Exception)
                                    {
                                        flfriend = null;
                                    }
                                    
                                }
                                //MessageBox.Show("2");
                                if (CSS.IM.UI.Util.Path.ChatOpen)
                                {
                                    //friendListView.flickerFriend(new Jid(msg.From.Bare));
                                    //ChatMessageBox.GetInstance(this).AddFriendMessage(msg.From, XmppCon, msg.From.Bare);
                                    //timer_notifyIco.Enabled = true;

                                    //MessageBox.Show("3");
                                    string nickName = "";
                                    if (flfriend == null)
                                        nickName = msg.From.User;
                                    else
                                        nickName = listView_fd.GetFriendNickName(msg.From.Bare);

                                    //MessageBox.Show(nickName);

                                    ChatFromMsg chatForm = new ChatFromMsg(msg.From, XmppCon, nickName);
                                    try
                                    {
                                        chatForm.UpdateFriendOnline(flfriend==null?false:flfriend.IsOnline);//设置好友在线状态
                                        chatForm.Show();
                                        chatForm.Activate();
                                        chatForm.IncomingMessage(msg);
                                    }
                                    catch (Exception)
                                    {

                                    }

                                }
                                else
                                {
                                    if (msgBox.ContainsKey(msg.From.Bare.ToString()))
                                    {
                                        List<CSS.IM.XMPP.protocol.client.Message> msgs = msgBox[msg.From.Bare.ToString()];
                                        msgs.Add(msg);
                                        msgBox.Remove(msg.From.Bare.ToString());
                                        msgBox.Add(msg.From.Bare.ToString(), msgs);
                                        listView_fd.flickerFriend(new Jid(msg.From.Bare));

                                    }
                                    else
                                    {
                                        if (listView_fd.Rosters.ContainsKey(msg.From.Bare))
                                        {
                                            List<CSS.IM.XMPP.protocol.client.Message> msgs = new List<XMPP.protocol.client.Message>();
                                            msgs.Add(msg);
                                            msgBox.Add(msg.From.Bare.ToString(), msgs);
                                            listView_fd.flickerFriend(new Jid(msg.From.Bare));
                                            ChatMessageBox.GetInstance(this).AddFriendMessage(msg.From, XmppCon, msg.From.Bare);
                                            timer_MessageAlert.Enabled = true;
                                        }
                                        else
                                        {
                                            string nickName = listView_fd.GetFriendNickName(msg.From.Bare);
                                            ChatFromMsg chatForm = new ChatFromMsg(msg.From, XmppCon, nickName);
                                            try
                                            {
                                                chatForm.UpdateFriendOnline(flfriend.IsOnline==null?false:flfriend.IsOnline);//设置好友在线状态
                                                chatForm.Show();
                                                chatForm.Activate();
                                                chatForm.IncomingMessage(msg);
                                            }
                                            catch (Exception)
                                            {


                                            }

                                        }

                                        //Friend item=friendListView.Rosters[msg.From.Bare.ToString()];
                                    }
                                }
                                //ChatForm chatForm = new ChatForm(msg.From, XmppCon, msg.From.Bare);
                                //chatForm.Show();
                                //chatForm.IncomingMessage(msg);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("系统错误，打开消息窗体失败，原因："+ex.Message+"请联系软件开发商。");
                            }

                        }
                    }
                //}
            }
        }

        public void XmppCon_OnIq(object sender, IQ iq)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new IqHandler(XmppCon_OnIq), new object[] { sender, iq });
                return;
            }


            if (iq != null)
            {
                if (iq.HasTag(typeof(CSS.IM.XMPP.protocol.extensions.si.SI)))
                {
                    if (iq.Type == IqType.set)
                    {
                        CSS.IM.XMPP.protocol.extensions.si.SI si = iq.SelectSingleElement(typeof(CSS.IM.XMPP.protocol.extensions.si.SI)) as CSS.IM.XMPP.protocol.extensions.si.SI;

                        CSS.IM.XMPP.protocol.extensions.filetransfer.File file = si.File;
                        if (file != null)
                        {
                            if (!Util.ChatForms.ContainsKey(iq.From.Bare))//查看聊天窗口是否已经打开了
                            {
                                try
                                {
                                    Friend flfriend = listView_fd.Rosters[iq.From.Bare];
                                    string nickName = listView_fd.GetFriendNickName(iq.From.Bare);
                                    ChatFromMsg chatForm = new ChatFromMsg(iq.From, XmppCon, nickName);
                                    chatForm.UpdateFriendOnline(flfriend.IsOnline);//设置好友在线状态
                                    chatForm.Show();
                                    //chatForm.FileTransfer(iq);
                                    chatForm.Activate();
                                }
                                catch (Exception)
                                {

                                }
                            }
                            else
                            {

                            }

                        }
                    }
                }
                else
                {

                }
            }
        }

        public void XmppCon_OnSocketError(object sender, Exception ex)
        {

            if (InvokeRequired)
            {
                BeginInvoke(new ErrorHandler(XmppCon_OnSocketError), new object[] { sender, ex });
                return;
            }
            waiting.Close();
            string str = "Socket Error\r\n" + ex.Message + "\r\n" + ex.InnerException + "\n 请重检查网络并重新启动程序！";
            MsgBox.Show(this, "CSS&IM", str, MessageBoxButtons.OK);

            this.Invoke(new LogoutDelegate(LogOut), new object[] { false, true });
            //Application.Exit();
        }

        public void ClientSocket_OnReceive(object sender, byte[] data, int count)
        {
            //Debug.WriteLine(Encoding.UTF8.GetString(data));
        }

        public void ClientSocket_OnSend(object sender, byte[] data, int count)
        {
            //Debug.WriteLine(Encoding.UTF8.GetString(data));
        }

        /// <summary>
        /// 登录成功事件
        /// </summary>
        /// <param name="sender"></param>
        public void XmppCon_OnLogin(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new CSS.IM.XMPP.ObjectHandler(XmppCon_OnLogin), new object[] { sender });
                return;
            }

            Program.UserName = XmppCon.Username;
            Program.LocalHostIP = IPAddress.Parse(XmppCon.ClientSocket.LocalHostIP);//设置本地IP地址
            VcardIq viq = new VcardIq(IqType.get, null, new Jid(XmppCon.MyJID.User));
            XmppCon.IqGrabber.SendIq(viq, new IqCB(VcardResult), null);
            Program.UserName = XmppCon.MyJID.User;//保存登录的用户名

            notifyIcon_MessageQueue.Visible = true;
            waiting.Close();

            XmppCon.Show = ShowType.NONE;
            XmppCon.SendMyPresence();

            DiscoServer();//获取各种服务器

            this.NikeName = XmppCon.Username;
            this.ShowInTaskbar = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            listView_fd.XmppConnection = XmppCon;

            listView_fd.AddGroup("我的联系人");
            listView_fd.UpdateLayout(3, 0);

            this.TopMost = true;
            this.Show();
            this.Activate();
            this.TopMost = false;

            #region 设置最后一次登录的用户
            Document hl_doc = new Document();
            Settings.HistoryLogin doc_HLlgin;
            if (!System.IO.File.Exists(CSS.IM.UI.Util.Path.HistoryFilename))
            {
                doc_HLlgin = new Settings.HistoryLogin();
                doc_HLlgin.LoginName = Program.UserName;
            }
            else
            {
                hl_doc.LoadFile(CSS.IM.UI.Util.Path.HistoryFilename);
                doc_HLlgin = hl_doc.RootElement as Settings.HistoryLogin;
                doc_HLlgin.LoginName = Program.UserName;
                hl_doc.RemoveAllChildNodes();
               
            }
            hl_doc.AddChild(doc_HLlgin);
            FileInfo file = new FileInfo(CSS.IM.UI.Util.Path.HistoryFilename);
            if (!File.Exists(file.DirectoryName))
            {
                Directory.CreateDirectory(file.DirectoryName);
            }
            hl_doc.Save(CSS.IM.UI.Util.Path.HistoryFilename);
            #endregion

            #region 创建服务配置文件
            Document vy_doc = new Document();
            Settings.Verify vy_doc_settings = new Settings.Verify();
            Settings.Login vy_doc_login = null;
            Settings.ServerInfo vy_doc_serverInfo = null;

            if (System.IO.File.Exists(string.Format(CSS.IM.UI.Util.Path.SettingsFilename,Program.UserName)))
            {
                vy_doc.LoadFile(string.Format(CSS.IM.UI.Util.Path.SettingsFilename, Program.UserName));

                vy_doc_login = vy_doc.RootElement.SelectSingleElement(typeof(Settings.Login)) as Settings.Login;
                vy_doc_serverInfo = vy_doc.RootElement.SelectSingleElement(typeof(Settings.ServerInfo)) as Settings.ServerInfo;
                vy_doc.RemoveAllChildNodes();
            }
            else
            {
                vy_doc_login = new Settings.Login();
                vy_doc_serverInfo = new Settings.ServerInfo();
                vy_doc_login.InitIal = true;//设置开机自动启动，默认为启动
            }

            vy_doc_login.Auto = login_user.Auto;
            vy_doc_login.Save = login_user.Save;
            vy_doc_login.Jid = new Jid(login_user.UserName);
            vy_doc_login.Password = login_user.PassWord;

            vy_doc_serverInfo.ServerIP = Program.ServerIP;
            vy_doc_serverInfo.ServerPort = Program.Port;

            vy_doc_settings.ServerInfo = vy_doc_serverInfo;
            vy_doc_settings.Login = vy_doc_login;

            vy_doc.ChildNodes.Add(vy_doc_settings);
            vy_doc.Save(string.Format(CSS.IM.UI.Util.Path.SettingsFilename, Program.UserName));
            CSS.IM.UI.Util.Path.Initial = vy_doc_login.InitIal;//设置开机自动启动
            #endregion

            #region 创建个人配置文件
            if (!System.IO.File.Exists(string.Format(CSS.IM.UI.Util.Path.ConfigFilename,Program.UserName)))
            {
                Document doc = new Document();
                Settings.Settings config = new Settings.Settings();
                CSS.IM.App.Settings.Paths path = new Settings.Paths();
                path.MsgPath = CSS.IM.UI.Util.Path.MsgPath;
                path.SelectSingleElement("MsgPath").SetAttribute("Enable", true);
                path.SystemPath = CSS.IM.UI.Util.Path.SystemPath;
                path.SelectSingleElement("SystemPath").SetAttribute("Enable", true);
                path.CallPath = CSS.IM.UI.Util.Path.CallPath;
                path.SelectSingleElement("CallPath").SetAttribute("Enable", true);
                path.FolderPath = CSS.IM.UI.Util.Path.FolderPath;
                path.SelectSingleElement("FolderPath").SetAttribute("Enable", true);
                path.GlobalPath = CSS.IM.UI.Util.Path.GlobalPath;
                path.SelectSingleElement("GlobalPath").SetAttribute("Enable", true);
                path.InputAlertPath = CSS.IM.UI.Util.Path.InputAlertPath;
                path.SelectSingleElement("InputAlertPath").SetAttribute("Enable", true);
                path.ReveiveSystemNotification = true;
                path.ChatOpen = true;
                path.SendKeyType = CSS.IM.UI.Util.Path.SendKeyType;//创建消息发送快捷键类型 默认为enter发送 
                path.GetOutMsgKeyTYpe = "W+ Control+ Alt";//默认获取消息快捷按键
                path.ScreenKeyTYpe = "S+ Control+ Alt";//默认截图快捷按键
                path.FriendContainerType = true;//保存是大头像还是小头像 默认为小头像

                path.DefaultURL = "http://10.0.0.207:8080/bgtoa/eblueplugins/eblueim/forwardUrl.do?url=cmVkaXJlY3Q6L2luZGV4LmRv";//毛奇使用

                path.EmailURL = "http://10.0.0.207:8080/bgtoa/eblueplugins/eblueim/forwardUrl.do?url=cmVkaXJlY3Q6L2luZGV4LmRv";//毛奇使用

                config.Paths = path;

                CSS.IM.UI.Util.Path.DefaultURL = path.DefaultURL;

                BasicTextBox txt_temp = new BasicTextBox();

                CSS.IM.App.Settings.SFont font = new Settings.SFont();
                font.Name = txt_temp.Font.Name;
                font.Size = txt_temp.Font.Size;
                font.Bold = txt_temp.Font.Bold;
                font.Italic = txt_temp.Font.Italic;
                font.Strikeout = txt_temp.Font.Strikeout;
                config.Font = font;

                CSS.IM.App.Settings.SColor color = new Settings.SColor();
                Color top_cl = txt_temp.ForeColor;
                byte[] top_cby = BitConverter.GetBytes(top_cl.ToArgb());
                color.CA = top_cby[0];
                color.CR = top_cby[1];
                color.CG = top_cby[2];
                color.CB = top_cby[3];
                config.Color = color;

                doc.ChildNodes.Add(config);
                doc.Save(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));

                txt_temp.Dispose();
            }

            #endregion

            #region 加载树图
            if (filename == null || filename.Trim().Length == 0)
                filename = "new";
            IQ tree_iq = new IQ(IqType.get);
            tree_iq.Id = CSS.IM.XMPP.Id.GetNextId();
            tree_iq.Namespace = null;
            CSS.IM.XMPP.protocol.Base.Query query = new CSS.IM.XMPP.protocol.Base.Query();
            query.Attributes.Add("filename", filename);
            query.Namespace = "xmlns:org:tree";
            tree_iq.AddChild(query);
            XmppCon.IqGrabber.SendIq(tree_iq, new IqCB(TreeResulit), null);
            #endregion
        }

        /// <summary>
        ///  登录失败事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void XmppCon_OnAuthError(object sender, Element e)
        {

            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new XmppElementHandler(XmppCon_OnAuthError), new object[] { sender, e });
                return;
            }

            //if (XmppCon.XmppConnectionState != XmppConnectionState.Disconnected)
            //    XmppCon.Close();

            //Program.IsLogin = true;
            Program.UserName = XmppCon.Username;
            MsgBox.Show(waiting, "CSS&IM", "登录失败，用户名或密码错误！", MessageBoxButtons.OK);
            waiting.Close();
            LogOut(false, false);

        }

        #endregion

        private void btn_default_index_MouseClick(object sender, MouseEventArgs e)
        {
            //System.Diagnostics.Process.Start(@"http://www.6tianxia.com.cn");

            if (CSS.IM.UI.Util.Path.DefaultURL!="")
            {
                System.Diagnostics.Process.Start(CSS.IM.UI.Util.Path.DefaultURL + "&loginname="+XmppCon.Username+"&pwd="+Base64.EncodeBase64(XmppCon.Password));
            }
        }

        private void btn_mail_MouseClick(object sender, MouseEventArgs e)
        {
            //System.Diagnostics.Process.Start(@"http://mail.css.com.cn");

            if (CSS.IM.UI.Util.Path.EmailURL != "")
            {
                System.Diagnostics.Process.Start(CSS.IM.UI.Util.Path.EmailURL + "&loginname=" + XmppCon.Username + "&pwd=" + Base64.EncodeBase64(XmppCon.Password));
            }
        }

        private void btn_color_Click(object sender, EventArgs e)
        {

        }

        private void btn_tools_MouseClick(object sender, MouseEventArgs e)
        {
            if (setingForm == null || setingForm.IsDisposed)
            {
                setingForm = new SetingForm();
            }

            try
            {
                setingForm.Show();
                setingForm.Activate();
            }
            catch (Exception)
            {

            }
        }

        private void btn_message_MouseClick(object sender, MouseEventArgs e)
        {
            if (messageLogForm == null || messageLogForm.IsDisposed)
            {
                messageLogForm = new MessageLogForm();
                messageLogForm.XmppConn = XmppCon;
                messageLogForm.BackgroundImage = this.BackgroundImage;
                messageLogForm.Text = "消息管理器";
                messageLogForm.ShowIcon = false;
                messageLogForm.AllowMove = false;
                messageLogForm.ShowInTaskbar = false;
            }
            try
            {
                messageLogForm.Show();
                messageLogForm.Activate();
            }
            catch (Exception)
            {

            }
        }

        private void btn_find_MouseClick(object sender, MouseEventArgs e)
        {
            if (findFriendForm == null || findFriendForm.IsDisposed)
            {
                findFriendForm = new FindFriendForm(XmppCon);
            }

            try
            {
                findFriendForm.Show();
                findFriendForm.Activate();
            }
            catch (Exception)
            {

            }
        }

        private void 我在线上ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            我在线上ToolStripMenuItem_Click(null, null);
        }

        private void 忙碌ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            忙碌ToolStripMenuItem_Click(null, null);
        }

        private void 离开ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            离开ToolStripMenuItem_Click(null, null);
        }

        private void 关闭声音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document doc = new Document();
            doc.LoadFile(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
            Settings.Settings config = new Settings.Settings();
            Settings.Paths path = doc.RootElement.SelectSingleElement(typeof(Settings.Paths)) as Settings.Paths;

            if (关闭声音ToolStripMenuItem.Text == "关闭所有声音")
            {
                关闭声音ToolStripMenuItem.Checked = true;
                关闭声音ToolStripMenuItem.Text = "开启所有声音";
                path.SelectSingleElement("MsgPath").SetAttribute("Enable", false);
                CSS.IM.UI.Util.Path.MsgSwitch = false;
                path.SelectSingleElement("SystemPath").SetAttribute("Enable", false);
                CSS.IM.UI.Util.Path.SystemSwitch = false;
                path.SelectSingleElement("CallPath").SetAttribute("Enable", false);
                CSS.IM.UI.Util.Path.CallSwitch = false;
                path.SelectSingleElement("FolderPath").SetAttribute("Enable", false);
                CSS.IM.UI.Util.Path.FolderSwitch = false;
                path.SelectSingleElement("GlobalPath").SetAttribute("Enable", false);
                CSS.IM.UI.Util.Path.GlobalSwitch = false;
                path.SelectSingleElement("InputAlertPath").SetAttribute("Enable", false);
                CSS.IM.UI.Util.Path.InputAlertSwitch = false;
            }
            else
            {
                关闭声音ToolStripMenuItem.Checked = false;
                关闭声音ToolStripMenuItem.Text = "关闭所有声音";
                path.SelectSingleElement("MsgPath").SetAttribute("Enable", true);
                CSS.IM.UI.Util.Path.MsgSwitch = true;
                path.SelectSingleElement("SystemPath").SetAttribute("Enable", true);
                CSS.IM.UI.Util.Path.SystemSwitch = true;
                path.SelectSingleElement("CallPath").SetAttribute("Enable", true);
                CSS.IM.UI.Util.Path.CallSwitch = true;
                path.SelectSingleElement("FolderPath").SetAttribute("Enable", true);
                CSS.IM.UI.Util.Path.FolderSwitch = true;
                path.SelectSingleElement("GlobalPath").SetAttribute("Enable", true);
                CSS.IM.UI.Util.Path.GlobalSwitch = true;
                path.SelectSingleElement("InputAlertPath").SetAttribute("Enable", true);
                CSS.IM.UI.Util.Path.InputAlertSwitch = true;
            }
            config.Paths = path;
            doc.Clear();
            doc.ChildNodes.Add(config);
            doc.Save(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
        }

        private void 打开主面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();


            if ((anchors & AnchorStyles.Left) == AnchorStyles.Left)
            {
                Left = 0;
            }
            if ((anchors & AnchorStyles.Top) == AnchorStyles.Top)
            {
                Top = 0;
            }
            if ((anchors & AnchorStyles.Right) == AnchorStyles.Right)
            {
                Left = Screen.PrimaryScreen.Bounds.Width - Width;
            }
            if ((anchors & AnchorStyles.Bottom) == AnchorStyles.Bottom)
            {
                Top = Screen.PrimaryScreen.Bounds.Height - Height;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmi添加好友_Click(object sender, EventArgs e)
        {
            if (treeView_nt.SelectedNode == null)
                return;
            Jid j = new Jid(treeView_nt.SelectedNode.Tag.ToString(), XmppCon.Server, null);
            XmppCon.RosterManager.AddRosterItem(j);
            XmppCon.PresenceManager.Subscribe(j);
        }

        private void tsmi发送消息_Click(object sender, EventArgs e)
        {
            if (treeView_nt.SelectedNode == null)
                return;
            if (treeView_nt.SelectedNode.Tag == null)
                return;
            Jid j = new Jid(treeView_nt.SelectedNode.Tag.ToString(), XmppCon.Server, null);
            if (!Util.ChatForms.ContainsKey(j.Bare))
            {
                listView_fd.UpdateFriendFlicker(j.Bare);

                string nickName = listView_fd.GetFriendNickName(j.Bare);
                ChatFromMsg chat = new ChatFromMsg(j, XmppCon, nickName);

                Friend friend;
                if (listView_fd.Rosters.ContainsKey(j.Bare))
                {
                    friend = listView_fd.Rosters[j.Bare];
                }
                else
                {
                    friend = null;
                }

                if (friend != null)
                {
                    chat.UpdateFriendOnline(listView_fd.Rosters[j.Bare].IsOnline);
                }

                if (msgBox.ContainsKey(j.Bare.ToString()))
                {
                    chat.FristMessage(msgBox[j.Bare.ToString()]);
                    msgBox.Remove(j.Bare.ToString());
                }
                try
                {
                    chat.Show();
                }
                catch (Exception)
                {

                }

            }
        }

        private void tsmi查看信息_Click(object sender, EventArgs e)
        {

            if (treeView_nt.SelectedNode == null)
                return;
            VcardInfoForm vcardForm = new VcardInfoForm(new Jid(treeView_nt.SelectedNode.Tag.ToString(), XmppCon.Server, null), XmppCon);
            try
            {
                vcardForm.Show();
            }
            catch (Exception)
            {

            }
        }

        private void treeView_nt_Menu_Paint(object sender, PaintEventArgs e)
        {
            if (treeView_nt.SelectedNode != null)
            {
                if (treeView_nt.SelectedNode.Tag != null)
                {
                    treeView_nt_Menu.Enabled = true;
                }
                else
                {
                    treeView_nt_Menu.Enabled = false;
                }
            }
            else
            {
                treeView_nt_Menu.Enabled = false;
            }
        }

        private void treeView_nt_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode tn = treeView_nt.GetNodeAt(e.X, e.Y);
                if (tn != null)
                {
                    treeView_nt.SelectedNode = tn;
                    Selectnode = treeView_nt.SelectedNode;
                }

                if (treeView_nt.SelectedNode==null)
                    treeView_nt.ContextMenuStrip = null;
                else if (treeView_nt.SelectedNode.Tag != null)
                    treeView_nt.ContextMenuStrip = treeView_nt_Menu;
                else
                    treeView_nt.ContextMenuStrip = null;
            }
            else
            {
                treeView_nt.ContextMenuStrip = null;
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (txt_search.Text == "")
            {
                panel_Search.Visible = false;
                btn_searh_clear.Visible = false;
                qqHistoryListViewEx_panel_Search.RemoveFriendAll();
            }
            else
            {
                btn_searh_clear.Visible = true;
                panel_Search.Visible = true;

                foreach (KeyValuePair<String, String> i in userDic)
                {
                    if(i.Key.Contains(txt_search.Text) || i.Value.Contains(txt_search.Text))
                    {
                        Jid Toid = XmppCon.MyJID;
                        Toid.User = i.Key;
                        //Friend flfriend = listView_fd.Rosters[Toid.Bare];
                        Friend flfriend = new Friend();
                        flfriend.isTreeSearch = true;
                        flfriend.NikeName = i.Value;
                        flfriend.Ritem = new RosterItem(Toid);
                        qqHistoryListViewEx_panel_Search.XmppConnection = XmppCon;
                        qqHistoryListViewEx_panel_Search.AddFriend(flfriend);
                        qqHistoryListViewEx_panel_Search.OpenChatEvent += qqHistoryListViewEx_panel_Search_OpenChatEvent;
                    }
                }
 

                //Jid Toid = XmppCon.MyJID;
                //Toid.User = "songques";
                //Friend flfriend = listView_fd.Rosters[Toid.Bare];
                //qqHistoryListViewEx_panel_Search.XmppConnection = XmppCon;
                //qqHistoryListViewEx_panel_Search.AddFriend(flfriend);
            }

        }

        /// <summary>
        /// 要查找结果中打开聊天窗口
        /// </summary>
        /// <param name="sender"></param>
        public void qqHistoryListViewEx_panel_Search_OpenChatEvent(Friend sender)
        {
            Jid j = sender.Ritem.Jid;
            if (!Util.ChatForms.ContainsKey(j.Bare))
            {
                listView_fd.UpdateFriendFlicker(j.Bare);

                string nickName = sender.NikeName;
                ChatFromMsg chat = new ChatFromMsg(j, XmppCon, nickName);

                Friend friend;
                if (listView_fd.Rosters.ContainsKey(j.Bare))
                {
                    friend = listView_fd.Rosters[j.Bare];
                }
                else
                {
                    friend = null;
                }

                if (friend != null)
                {
                    chat.UpdateFriendOnline(listView_fd.Rosters[j.Bare].IsOnline);
                }

                if (msgBox.ContainsKey(j.Bare.ToString()))
                {
                    chat.FristMessage(msgBox[j.Bare.ToString()]);
                    msgBox.Remove(j.Bare.ToString());
                }
                try
                {
                    chat.Show();
                }
                catch (Exception)
                {

                }

            }
        }

        private void btn_searh_clear_Click(object sender, EventArgs e)
        {
            txt_search.Text = "";

        }

    }
}
