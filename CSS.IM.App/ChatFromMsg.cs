using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI;
using CSS.IM.UI.Control;
using CSS.IM.App.Settings;
using CSS.IM.XMPP;
using CSS.IM.XMPP.protocol.iq.vcard;
using CSS.IM.XMPP.protocol.client;
using CSS.IM.XMPP.Collections;
using CSS.IM.Library.Class;
using System.Net;
using CSS.IM.UI.Form;
using CSS.IM.Library.Controls;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.UI.Util;
using System.Drawing.Imaging;
using CSS.IM.Library.Controls.UdpSendFile;
using CSS.IM.UI.Control.Graphics.FileTransfersControl;
using System.Threading;
using CSS.IM.Library.Net;
using System.IO;

namespace CSS.IM.App
{
    public partial class ChatFromMsg : IChatForm
    {

        private EmotionDropdown emotionDropdown;//表情管理器

        List<CSS.IM.XMPP.protocol.client.Message> main_msg = new List<XMPP.protocol.client.Message>();//用于保存从好友列表打开后传过来的消息队列
        private bool IsPlayMsg = true;//是否播放消息声音

        public bool OnLine { get; set; }//联系人状态

        CSS.IM.Library.gifCollections PicQueue = new CSS.IM.Library.gifCollections();//用于保存图片的发送队列

        CSS.IM.Library.Class.UserInfo Opposite = new CSS.IM.Library.Class.UserInfo();//好友的个人信息

        private int ListenBasicUDPPort { get; set; }//本窗口监听的UDP端口
        private int ListenVideoUDPPort { get; set; }//保存本地视频监听的监听端口 

        private int RemotVideoUDPPort { get; set; }//保存远程视频的监听端口
        private System.Net.IPAddress RemotVideoUDPIP { get; set; }//保存远程的视频IP

        private System.Net.IPAddress RemotBaseUDPIP { get; set; }//保存远程的IP地址
        private int RemotBaseUDPPort { get; set; }//保存远程端口

        private string _NickName="";//用户名
        public string NickName
        {
            get { return _NickName; }
            set { _NickName = value; }
        }

        public Jid TO_Jid { get; set; }//保存远程会话的jid

        private static XmppClientConnection XmppConn;//保存会话管理

        /// <summary>
        /// 图片传输组件集合
        /// </summary>
        private CSS.IM.Library.Controls.p2pFileTransmitCollectionsEX imageP2Ps = new CSS.IM.Library.Controls.p2pFileTransmitCollectionsEX();

        private CSS.IM.App.Controls.AVForm avForm = null;//发送的视频对话

        private CSS.IM.App.Controls.AVForm ravForm = null;//接收的视频对话

        private Red5Msg red5MsgSend = null;//基于red5的视频功能发送

        private Red5Msg red5MsgReceive = null;//基于red5的视频功能接收

        /// <summary>
        /// 发送文件组件
        /// </summary>
        private UdpSendFile udpSendFile;


        /// <summary>
        /// 接收文件
        /// </summary>
        private UdpReceiveFile udpReceiveFile;
        private Color _baseColor = Color.DarkGoldenrod;
        private Color _borderColor = Color.FromArgb(64, 64, 0);
        private Color _progressBarBarColor = Color.Gold;
        private Color _progressBarBorderColor = Color.Olive;
        private Color _progressBarTextColor = Color.Olive;

        /// <summary>
        /// 外部手稿截图功能
        /// </summary>
        /// <param name="image"></param>
        public delegate void ScreenImageDelegate(Image image);
        public event ScreenImageDelegate ScreenImageEvent;

        /// <summary>
        /// 动态显示提示信息
        /// </summary>
        /// <param name="str"></param>
        private delegate void appendTextDelegate(string str);

        /// <summary>
        /// 图片传输完成代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private delegate void imageTransmittedDelegate(object sender, CSS.IM.Library.Controls.fileTransmitEvnetArgs e);

        /// <summary>
        /// 异步处理计算文件 MD5值
        /// </summary>
        /// <param name="f">文件信息</param>
        private delegate void sendFileDelegate(System.IO.FileInfo f);

        /// <summary>
        /// 接收到对方的视频请求，然后进行本地的初使化
        /// </summary>
        /// <param name="msg"></param>
        private delegate void AcceptVideoInitDelegate(CSS.IM.XMPP.protocol.client.Message msg);
        AcceptVideoInitDelegate AccepVideotInit = null;


        /// <summary>
        /// 对方意视频后并初使化完在，我自己进行视频初使化，并监听
        /// </summary>
        /// <param name="msg"></param>
        private delegate void ReturnAcceptVideoInitDelegate(CSS.IM.XMPP.protocol.client.Message msg);
        ReturnAcceptVideoInitDelegate ReturnAcceptVideoInit = null;

        /// <summary>
        /// 同意视频请求后，打开视频功能
        /// </summary>
        /// <param name="msg"></param>
        private delegate void AcceptVideoOpenDelegate(CSS.IM.XMPP.protocol.client.Message msg);
        AcceptVideoOpenDelegate AcceptVideoOpen = null;


        /// <summary>
        /// 收到发送文件请求后进行功能提示
        /// </summary>
        /// <param name="e"></param>
        private delegate void FileSendRequestDelegate(CSS.IM.Library.Class.DataArrivalEventArgs e);
        FileSendRequestDelegate FileSendRequestEvent;

        /// <summary>
        /// 发送文件准备前的工作
        /// </summary>
        /// <param name="filepath"></param>
        private delegate void FileSendInitDelegate(String filepath);
        FileSendInitDelegate FileSendInitEvent;

        /// <summary>
        /// 接收离线文件
        /// </summary>
        /// <param name="filepath"></param>
        private delegate void GetFtpFileDelegate(CSS.IM.XMPP.protocol.client.Message msg);
        GetFtpFileDelegate GetFtpFileEvent;


        /// <summary>
        /// red5的视频请求
        /// </summary>
        /// <param name="filepath"></param>
        private delegate void Red5AccpetDelegate(CSS.IM.XMPP.protocol.client.Message msg);
        Red5AccpetDelegate Red5AccpetEvent;


        /// <summary>
        /// red5的视频请求拒绝
        /// </summary>
        /// <param name="filepath"></param>
        private delegate void Red5RefuseDelegate(CSS.IM.XMPP.protocol.client.Message msg);
        Red5RefuseDelegate Red5RefuseEvent;


        public ChatFromMsg(Jid jid, XmppClientConnection Conn,string nickName)
        {
            InitializeComponent();
            RemotBaseUDPIP = IPAddress.Parse("127.0.0.1");

            AccepVideotInit = new AcceptVideoInitDelegate(AcceptVideoInitMethod);
            ReturnAcceptVideoInit = new ReturnAcceptVideoInitDelegate(ReturnAcceptVideoInitMethod);
            AcceptVideoOpen = new AcceptVideoOpenDelegate(AcceptVideoOpenMethod);
            FileSendRequestEvent = new FileSendRequestDelegate(FileSendRequestMethod);
            FileSendInitEvent = new FileSendInitDelegate(FileSendInitMethod);
            GetFtpFileEvent = new GetFtpFileDelegate(GetFtpFileAMethod);
            Red5AccpetEvent = new Red5AccpetDelegate(Red5AccpetMethod);
            Red5RefuseEvent = new Red5RefuseDelegate(Red5RefuseMethod);

            TO_Jid = jid;
            XmppConn = Conn;
            this._NickName = nickName;
            this.Text = "正在与[" + (nikeName.Text != "" ? nikeName.Text : _NickName) + "]对话";

            VcardIq viq = new VcardIq(IqType.get, new Jid(jid.Bare));
            XmppConn.IqGrabber.SendIq(viq, new IqCB(VcardResult), null, true);

            Util.ChatForms.Add(TO_Jid.Bare, this);
            nikeName.Text = _NickName;

            rtfSend.AllowDrop = true;
            rtfSend.DragDrop += new DragEventHandler(rtfSend_DragDrop);
            rtfSend.DragEnter += new DragEventHandler(rtfSend_DragEnter);

            XmppConn.MessageGrabber.Add(jid, new BareJidComparer(), new MessageCB(MessageCallback), null);

            //用于外部更新截图
            ScreenImageEvent += new ScreenImageDelegate(ChatFromMsg_ScreenImageEvent);
            Util.TO_Jid =TO_Jid;//公布对外的to_jid用于外部插入截图


            //初使化发送文件
            udpSendFile = new UdpSendFile(2);
            //sendFile.Log += new TraFransfersFileLogEventHandler(SendFileLog);
            udpSendFile.FileSendBuffer += new FileSendBufferEventHandler(FileSendBuffer);
            udpSendFile.FileSendAccept += new FileSendEventHandler(FileSendAccept);
            udpSendFile.FileSendRefuse += new FileSendEventHandler(FileSendRefuse);
            udpSendFile.FileSendCancel += new FileSendEventHandler(FileSendCancel);
            udpSendFile.FileSendComplete += new FileSendEventHandler(FileSendComplete);
            udpSendFile.Start();

            //初使化接收文件
            udpReceiveFile = new UdpReceiveFile(2);
            udpReceiveFile.RequestSendFile += new RequestSendFileEventHandler(RequestSendFile);
            udpReceiveFile.FileReceiveBuffer += new FileReceiveBufferEventHandler(FileReceiveBuffer);
            udpReceiveFile.FileReceiveComplete += new FileReceiveEventHandler(FileReceiveComplete);
            udpReceiveFile.FileReceiveCancel += new FileReceiveEventHandler(FileReceiveCancel);
            udpReceiveFile.Start();
        }


        /// <summary>
        /// 外部调用更新事件
        /// </summary>
        /// <param name="image"></param>
        public void AddScreenImageEvent(Image image)
        {
            this.ScreenImageEvent(image);
        }

        /// <summary>
        /// 外部更新了截图
        /// </summary>
        /// <param name="image"></param>
        private void ChatFromMsg_ScreenImageEvent(Image image)
        {
            if (image != null)
            {
                System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(Util.sendImage);
                if (!dInfo.Exists)
                    dInfo.Create();

                string fileName = Util.sendImage + "temp.gif";

                image.Save(fileName,ImageFormat.Jpeg);

                string md5 = Hasher.GetMD5Hash(fileName);
                string Md5fileName = Util.sendImage + md5 + ".gif";

                if (!System.IO.File.Exists(Md5fileName))
                {
                    System.IO.File.Delete(fileName);
                    image.Save(Md5fileName, ImageFormat.Jpeg);
                }
                try
                {
                    this.rtfSend.addGifControl(md5, image);
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// 表单加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatFromMsg_Load(object sender, EventArgs e)
        {
            this.rtfSend.Font = CSS.IM.UI.Util.Path.SFong;
            this.rtfSend.ForeColor = CSS.IM.UI.Util.Path.SColor;

            panel_function.Visible = false;
            this.Width = this.Width - panel_function.Width;

            panel_msg.Width = panel_msg.Width + panel_function.Width+2;

            this.DoubleBuffered = true;
            LB_sockUDP.Listen(0);
            ListenBasicUDPPort = LB_sockUDP.ListenPort;

            foreach (CSS.IM.XMPP.protocol.client.Message mg in main_msg)
            {
                IsPlayMsg = false;
                IncomingMessage(mg);
                IsPlayMsg = true;
            }
            main_msg.Clear();

            try
            {
                rtfSend.ImeMode = ImeMode.OnHalf;
            }catch (Exception){}
            rtfSend.Focus();
            rtfSend.Select(this.rtfSend.TextLength, 0);
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
                        case 100:    //按下的是Shift+S
                            //此处填写快捷键响应代码         
                            break;
                        case 101:    //按下的是Ctrl+B
                            //此处填写快捷键响应代码
                            break;
                        case 102:    //按下的是Ctrl+Alt+S
                            btn_screen_Click(null, null);

                            

                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// 设置在消息盒子中保存的消息
        /// </summary>
        /// <param name="msg"></param>
        public void FristMessage(List<CSS.IM.XMPP.protocol.client.Message> msg)
        {
            main_msg = msg;
        }


        #region 消息接收

        private void MessageCallback(object sender, CSS.IM.XMPP.protocol.client.Message msg, object data)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new MessageCB(MessageCallback), new object[] { sender, msg, data });
                    return;
                }
                IncomingMessage(msg);
            }
            catch (Exception)
            {

            }

        }

        public void IncomingMessage(CSS.IM.XMPP.protocol.client.Message msg)
        {
            try
            {
                if (msg.Type == MessageType.error)
                {
                    MsgBox.Show(this, "CSS&IM", "离线消息没有发送成功！", MessageBoxButtons.OK);
                    btn_close_Click(null, null);
                    //btn_close_Click(null, null);
                    //if (msg.To.Bare == _connection.MyJID.Bare && msg.From.Bare == to_Jid.Bare)
                    //{
                    //    MsgBox.Show(this, "CSS&IM", "在服务器中没有找到该用户，无法发送消息！", MessageBoxButtons.OK);
                    //    btn_close_Click(null, null);
                    //}
                }

                int m_type = msg.GetTagInt("m_type");

                switch (m_type)
                {
                    case 0://正常消息
                        //RTBRecord.AppendTextAsRtf(msg.From.User + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "\n", new Font(this.Font, FontStyle.Underline | FontStyle.Bold), CSS.IM.Library.ExtRichTextBox.RtfColor.Red, CSS.IM.Library.ExtRichTextBox.RtfColor.White);
                        CSS.IM.UI.Util.Win32.FlashWindow(this.Handle, true);//闪烁 

                        #region 显示我自己发送的消息
                        CSS.IM.XMPP.protocol.client.Message top_msg = new XMPP.protocol.client.Message();
                        top_msg.SetTag("FName", this.Font.Name);//获得字体名称
                        top_msg.SetTag("FSize", this.Font.Size);//字体大小
                        top_msg.SetTag("FBold", true);//是否粗体
                        top_msg.SetTag("FItalic", this.Font.Italic);//是否斜体
                        top_msg.SetTag("FStrikeout", this.Font.Strikeout);//是否删除线
                        top_msg.SetTag("FUnderline", true);//是否下划线

                        Color top_cl = Color.Red;//获取颜色
                        byte[] top_cby = BitConverter.GetBytes(top_cl.ToArgb());
                        top_msg.SetTag("CA", top_cby[0]);
                        top_msg.SetTag("CR", top_cby[1]);
                        top_msg.SetTag("CG", top_cby[2]);
                        top_msg.SetTag("CB", top_cby[3]);
                        top_msg.Body = (_NickName != "" ? _NickName : msg.From.User) + "(" + msg.From.User + "):" + DateTime.Now.ToString("HH:mm:ss");
                        top_msg.From = TO_Jid;
                        top_msg.To = TO_Jid;
                        RTBRecord_Show(top_msg, true);
                        #endregion

                        RTBRecord_Show(msg, false);

                        if (IsPlayMsg)
                        {
                            if (CSS.IM.UI.Util.Path.MsgSwitch)
                                CSS.IM.UI.Util.SoundPlayEx.MsgPlay(CSS.IM.UI.Util.Path.MsgPath);
                        }
                        break;
                    case 1://收到对方的请求要过行视频功能服务，初始化本地的视频
                        this.Invoke(AccepVideotInit, new object[] { msg });
                        break;
                    case 2://我发送视频请求后，对方告诉我视频初使化完成，进行自己本地的视频初使化
                        this.Invoke(ReturnAcceptVideoInit, new object[] { msg });
                        break;
                    case 3://对方给我发送视频请求，我初使化本地视频服务，告诉对方，对方也初使化视频服务了，我打开视频功能
                        this.Invoke(AcceptVideoOpen, new object[] { msg });
                        break;
                    case 4://去对方获取截图的功能
                        RemotBaseUDPPort = msg.GetTagInt("BPort");
                        RemotBaseUDPIP = IPAddress.Parse(msg.GetTag("BIP"));
                        bool isSend = msg.GetTagBool("isSend");

                        if (isSend)
                        {
                            CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
                            fmsg.SetTag("m_type", 4);
                            fmsg.Type = MessageType.chat;
                            fmsg.To = TO_Jid;
                            fmsg.SetTag("BPort", ListenBasicUDPPort);
                            fmsg.SetTag("BIP", Program.LocalHostIP.ToString());
                            fmsg.SetTag("isSend", false);
                            XmppConn.Send(fmsg);
                        }

                        sendSelfImage();
                        break;
                    case 5://视频释放
                        if (RemotVideoUDPPort != -1)
                        {
                            //avForm.isBtn_hangup = true;
                            if (avForm != null && !avForm.IsDisposed)
                            {
                                avForm.isBtn_hangup = true;
                                avForm.AVClose();
                            }
                            if (ravForm != null && !ravForm.IsDisposed)
                            {
                                ravForm.isBtn_hangup = true;
                                ravForm.AVClose();
                            }
                        }
                        break;
                    case 6://传文件
                        RemotBaseUDPPort = msg.GetTagInt("BPort");
                        RemotBaseUDPIP = IPAddress.Parse(msg.GetTag("BIP"));

                        CSS.IM.XMPP.protocol.client.Message lmsg = new CSS.IM.XMPP.protocol.client.Message();
                        lmsg.SetTag("m_type", 7);//告诉对方要发送文件啦
                        lmsg.Type = MessageType.chat;
                        lmsg.To = TO_Jid;
                        lmsg.SetTag("BPort", udpReceiveFile.Port);
                        lmsg.SetTag("BIP", Program.LocalHostIP.ToString());
                        lmsg.SetTag("isSend", false);
                        lmsg.SetTag("File", msg.GetTag("File").ToString());
                        XmppConn.Send(lmsg);
                        break;
                    case 7:
                        RemotBaseUDPPort = msg.GetTagInt("BPort");
                        RemotBaseUDPIP = IPAddress.Parse(msg.GetTag("BIP"));
                        this.Invoke(FileSendInitEvent, msg.GetTag("File").ToString());
                        break;
                    case 8://ftp离线文件
                        this.Invoke(GetFtpFileEvent, new object[] { msg});
                        break;
                    case 9://对方拒绝接收主线文件
                        this.AppendSystemRtf("对方"+msg.Body);
                        break;
                    case 10://对方接收离线文件
                        //DownloadImage
                        try
                        {
                            FTPClient ftpClient = new FTPClient(Util.ServerAddress, Util.ftpPath, Util.ftpUser, Util.ftpPswd, Util.ftpPort);
                            ftpClient.FtpPath=msg.GetTag("Path");
                            DownloadImage downloadImage = new DownloadImage();
                            downloadImage.ftpClient = ftpClient;
                            downloadImage.remoteImageName = msg.GetTag("FileName");
                            downloadImage.parent = this;
                            Thread getFileThread = new Thread(downloadImage.Download);
                            getFileThread.Start();
                        }
                        catch (Exception)
                        {
                            
                        }
                        break;
                    case 11://接收到red的视频请求
                        this.Invoke(Red5AccpetEvent, new object[] { msg });
                        break;
                    case 12://接收到red的视频请求
                        this.Invoke(Red5RefuseEvent, new object[] { msg });
                        break;
                    case 13:
                        //red5MsgSend_FormClosing
                        if (red5MsgReceive != null || !red5MsgReceive.IsDisposed)
                        {
                            red5MsgReceive.Close();
                            red5MsgReceive.Dispose();
                            red5MsgReceive = null;
                        }
                        this.AppendSystemRtf("对方" + msg.From.User + "关闭了视频通话");
                        break;
                    case 14:
                        //red5MsgReceive_FormClosing
                        if (red5MsgSend != null || !red5MsgSend.IsDisposed)
                        {
                            red5MsgSend.Close();
                            red5MsgSend.Dispose();
                            red5MsgSend = null;
                        }
                        this.AppendSystemRtf("对方" + msg.From.User + "关闭了视频通话");
                        break;

                    

                }
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region 代理事件的实现

        /// <summary>
        /// 同意视频信息后，进行初使化本地视频服务
        /// </summary>
        /// <param name="msg"></param>
        void AcceptVideoInitMethod(CSS.IM.XMPP.protocol.client.Message msg)
        {
            UserInfo user1;
            UserInfo user2;

            if (ravForm != null && !ravForm.IsDisposed)
            {
                try
                {
                    ravForm.Dispose();
                    ravForm = null;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                ravForm = new CSS.IM.App.Controls.AVForm(TO_Jid);
                ravForm.AVCloseEvent += new CSS.IM.App.Controls.AVForm.AVCloseDelegate(avForm_AVCloseEvent);
            }

            //保存本地视频的监听端口
            ListenVideoUDPPort = ravForm.aVcommunicationEx1.selfUDPPort;

            RemotVideoUDPPort = msg.GetTagInt("UDPPort");
            RemotVideoUDPIP = IPAddress.Parse(msg.GetTag("UDPIP"));

            user1 = new UserInfo();

            user1.LocalPort = RemotVideoUDPPort;
            user1.LocalIP = RemotVideoUDPIP;

            ravForm.SetRemoteAddress(user1, RemotVideoUDPPort);


            user2 = new UserInfo();
            user2.LocalIP = Program.LocalHostIP;
            user2.LocalPort = ListenVideoUDPPort;
            ravForm.SetLocalAddress(user2);
            ravForm.AgreeEvent += new CSS.IM.App.Controls.AVForm.AgreeDelegate(ravForm_AgreeEvent);//接收视频会话事件
            ravForm.btn_hangup.Visible = false;//挂断不可用
            ravForm.btn_agree.Visible = true;//接受可用
            ravForm.btn_refuse.Visible = true;//拒绝可用
            try
            {
                ravForm.Show();
            }
            catch (Exception)
            {


            }

            //if (ravForm.callSoundPlayer != null)
            //    ravForm.callSoundPlayer.Stop();

        }

        /// <summary>
        /// 关闭视频时
        /// </summary>
        void avForm_AVCloseEvent()
        {
            if (avForm != null && !avForm.IsDisposed)
                avForm.AVClose();
            if (ravForm != null && !ravForm.IsDisposed)
                ravForm.AVClose();
            CSS.IM.XMPP.protocol.client.Message msg = new CSS.IM.XMPP.protocol.client.Message();
            msg.Type = MessageType.chat;
            msg.To = TO_Jid;
            msg.SetTag("m_type", 5);
            XmppConn.Send(msg);


        }

        /// <summary>
        /// 接收视频会话事件
        /// </summary>
        void ravForm_AgreeEvent()
        {
            CSS.IM.XMPP.protocol.client.Message res_msg = new CSS.IM.XMPP.protocol.client.Message();
            res_msg.Type = MessageType.chat;
            res_msg.To = TO_Jid;
            res_msg.SetTag("m_type", 2);
            res_msg.SetTag("UDPPort", ListenVideoUDPPort);
            res_msg.SetTag("UDPIP", Program.LocalHostIP.ToString());
            if (XmppConn != null)
            {
                XmppConn.Send(res_msg);
            }
        }

        /// <summary>
        /// 对方同意视频，并初使化视频后，本地也进行视频初使化
        /// </summary>
        /// <param name="msg"></param>
        void ReturnAcceptVideoInitMethod(XMPP.protocol.client.Message msg)
        {

            UserInfo user1;
            UserInfo user2;
            CSS.IM.XMPP.protocol.client.Message res_msg;
            RemotVideoUDPPort = msg.GetTagInt("UDPPort");
            RemotVideoUDPIP = IPAddress.Parse(msg.GetTag("UDPIP"));

            user1 = new UserInfo();
            user1.LocalIP = RemotVideoUDPIP;
            user1.LocalPort = RemotVideoUDPPort;

            avForm.SetRemoteAddress(user1, RemotVideoUDPPort);

            user2 = new UserInfo();
            user2.LocalIP = Program.LocalHostIP;
            user2.LocalPort = ListenBasicUDPPort;

            avForm.SetLocalAddress(user2);

            avForm.iniAV(CSS.IM.Library.AV.VideoSizeModel.W320_H240);

            res_msg = new CSS.IM.XMPP.protocol.client.Message();
            res_msg.Type = MessageType.chat;
            res_msg.To = TO_Jid;
            res_msg.SetTag("m_type", 3);
            XmppConn.Send(res_msg);//告诉对我准备好了

            if (avForm.callSoundPlayer != null)
                avForm.callSoundPlayer.Stop();
        }

        /// <summary>
        /// 我发送视频后，对方同意后进行初使化视频
        /// </summary>
        /// <param name="msg"></param>
        void AcceptVideoOpenMethod(XMPP.protocol.client.Message msg)
        {
            //if (ravForm.callSoundPlayer != null)
            //    ravForm.callSoundPlayer.Stop();

            ravForm.iniAV(CSS.IM.Library.AV.VideoSizeModel.W320_H240);

        }

        /// <summary>
        /// 用户发送文件请求
        /// </summary>
        /// <param name="e"></param>
        private void FileSendRequestMethod(DataArrivalEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(FileSendRequestEvent, e);
            }
            string[] fileInfo = TextEncoder.bytesToText(e.msg.Content).Split('|');
            if (fileInfo.Length < 4) return;//抛掉非法数据 
            //string fileInfo = FileName + "|" + FileLen.ToString() + "|" + fileExtension + "|" + FileMD5Value;//初次请求发送文件时要先发送“控件参数”到对方，请求对方创建“文件发送控件”并建立连接

           // FileTransfers(false, fileInfo[0], fileInfo[0], Convert.ToInt32(fileInfo[1]), fileInfo[2], fileInfo[3]);
        }

        #region ftp文件转发
        ///// <summary>
        ///// 发送文件
        ///// </summary>
        ///// <param name="path"></param>
        //public void FileSendInitMethod(String path)
        //{
        //    if (InvokeRequired)
        //    {
        //        this.Invoke(FileSendInitEvent, path);
        //    }
        //    try
        //    {
        //        Thread.Sleep(500);

        //        //用于好友不在线时的离线文件发送重复检测
        //        foreach (FileTransfersItem item in fileTansfersContainer.Controls)
        //        {
        //            if (item.Tag.GetType().Name == "SendFileManager")
        //            {
        //                SendFileManager sendSfm = item.Tag as SendFileManager;
        //                if (item.Style == FileTransfersItemStyle.Cancel || item.Style == FileTransfersItemStyle.Receive)
        //                {
        //                    if (sendSfm.FileName == path)
        //                    {
        //                        MsgBox.Show(this, "CSS&IM", "文件正在发送，不要能重复发送！", MessageBoxButtons.OK);
        //                        return;
        //                    }
        //                }
        //            }
        //        }

        //        SendFileManager sendFileManager;
        //        if (OnLine)
        //        {
        //            sendFileManager = new SendFileManager(path);
        //        }
        //        else
        //        {
        //            sendFileManager = new SendFileManager(path, true);
        //        }

        //        if (udpSendFile.CanSend(sendFileManager)&&(!Util.SendFileManagerList.ContainsKey(sendFileManager.MD5)))
        //        {

        //            FileTransfersItem item = fileTansfersContainer.AddItem(
        //                sendFileManager.MD5,
        //                "发送文件",
        //                sendFileManager.Name,
        //                Icon.ExtractAssociatedIcon(path).ToBitmap(),
        //                sendFileManager.Length,
        //                FileTransfersItemStyle.Send);
        //            item.CancelButtonClick += new EventHandler(ItemCancelButtonClick1);
        //            item.OffLineFilesButtonClick += new EventHandler(item_OffLineFilesButtonClick);
        //            item.Tag = sendFileManager;
        //            sendFileManager.Tag = item;

        //            if (OnLine)
        //            {
        //                udpSendFile.RemoteEP = new IPEndPoint(RemotBaseUDPIP, RemotBaseUDPPort);
        //                udpSendFile.SendFile(sendFileManager, item.Image);
        //            }
        //        }
        //        else
        //        {
        //            MsgBox.Show(this, "CSS&IM", "文件正在发送，不要能重复发送！", MessageBoxButtons.OK);
        //        }

        //        if (!panel_function.Visible)
        //        {
        //            this.WindowState = FormWindowState.Normal;
        //            this.Activate();
        //            panel_function.Visible = true;
        //            this.Width = this.Width + panel_function.Width;
        //            panel_msg.Width = panel_msg.Width - panel_function.Width - 2;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        #endregion

        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="path"></param>
        public void FileSendInitMethod(String path)
        {
            if (InvokeRequired)
            {
                this.Invoke(FileSendInitEvent, path);
            }
            try
            {
                Thread.Sleep(500);
                //用于好友不在线时的离线文件发送重复检测
                foreach (FileTransfersItem item in fileTansfersContainer.Controls)
                {
                    if (item.Tag.GetType().Name == "SendFileManager")
                    {
                        SendFileManager sendSfm = item.Tag as SendFileManager;
                        if (item.Style == FileTransfersItemStyle.Cancel || item.Style == FileTransfersItemStyle.Receive)
                        {
                            if (sendSfm.FileName == path)
                            {
                                MsgBox.Show(this, "CSS&IM", "文件正在发送，不要能重复发送！", MessageBoxButtons.OK);
                                return;
                            }
                        }
                    }
                }

                SendFileManager sendFileManager = new SendFileManager(path, true);
                if (udpSendFile.CanSend(sendFileManager) && (!Util.SendFileManagerList.ContainsKey(sendFileManager.MD5)))
                {

                    FileTransfersItem item = fileTansfersContainer.AddItem(
                        sendFileManager.MD5,
                        "发送文件",
                        sendFileManager.Name,
                        Icon.ExtractAssociatedIcon(path).ToBitmap(),
                        sendFileManager.Length,
                        FileTransfersItemStyle.Send);
                    item.CancelButtonClick += new EventHandler(ItemCancelButtonClick1);
                    item.OffLineFilesButtonClick += new EventHandler(item_OffLineFilesButtonClick);
                    item.Tag = sendFileManager;
                    sendFileManager.Tag = item;
                    //item.FtpSendFile();
                    udpSendFile.RemoteEP = new IPEndPoint(RemotBaseUDPIP, RemotBaseUDPPort);
                    //udpSendFile.SendFile(sendFileManager, item.Image);
                    udpSendFile.SendFile(sendFileManager, item.Image);
                }
                else
                {
                    MsgBox.Show(this, "CSS&IM", "文件正在发送，不要能重复发送！", MessageBoxButtons.OK);
                }

                if (!panel_function.Visible)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.Activate();
                    panel_function.Visible = true;
                    this.Width = this.Width + panel_function.Width;
                    panel_msg.Width = panel_msg.Width - panel_function.Width - 2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 在对话记录文本框中添加内容(多线程)
        /// </summary>
        /// <param name="str">要添加的内容</param>
        private void AppendSystemRtf(string str)
        {
            appendTextDelegate d = new appendTextDelegate(appendText);
            this.BeginInvoke(d, str);
        }

        /// <summary>
        /// 实现显示提示信息的代理事件
        /// </summary>
        /// <param name="str"></param>
        private void appendText(string str)
        {
            CSS.IM.Library.ExtRichTextBox.MyExtRichTextBox rich = new CSS.IM.Library.ExtRichTextBox.MyExtRichTextBox();

            rich.AppendText("\n");
            //rich.InsertImage(FormAccess.imageInformation);
            rich.AppendText(" " + str);
            rich.ForeColor = Color.Brown;

            this.RTBRecord.AppendRtf(rich.Rtf);
            rich.Clear();
            rich.Dispose();

            RTBRecord.Select(RTBRecord.TextLength, 0);
            RTBRecord.ScrollToCaret();
            //this.RTBRecord.AppendText("\n");

        }


        /// <summary>
        /// 接收离线文件
        /// </summary>
        /// <param name="msg"></param>
        private void GetFtpFileAMethod(XMPP.protocol.client.Message msg)
        {
            FTPClient ftpClient = new FTPClient(Util.ServerAddress, Util.ftpPath, Util.ftpUser, Util.ftpPswd, Util.ftpPort);
            ftpClient.FtpPath = msg.GetTag("Path");
            ftpClient.GetFileErrorEvent += new FTPClient.GetFileErrorDelegate(ftpClient_GetFileErrorEvent);
            ftpClient.GetFileProgressEvent += new FTPClient.GetFileProgressDelegate(ftpClient_GetFileProgressEvent);
            ftpClient.GetFileSucceedEvent += new FTPClient.GetFileSucceedDelegate(ftpClient_GetFileSucceedEvent);
            ftpClient.ChDir("/" + ftpClient.FtpPath + "/");
            long fileSize = long.Parse(msg.GetTag("Length"));

            FileTransfersItem item = fileTansfersContainer.AddItem(
                        msg.GetTag("MD5"),
                        "接收文件",
                        msg.GetTag("FileName"),
                        Properties.Resources.ReceiveIco,
                        fileSize,
                        FileTransfersItemStyle.FtpGet);

            item.BaseColor = _baseColor;
            item.BorderColor = _borderColor;
            item.ProgressBarBarColor = _progressBarBarColor;
            item.ProgressBarBorderColor = _progressBarBorderColor;
            item.ProgressBarTextColor = _progressBarTextColor;

            RequestSendFileEventArgs requestSendFileEventArgs = new RequestSendFileEventArgs();
            requestSendFileEventArgs.ftpClient = ftpClient;

            ftpClient.fileTransfersItem = item;
            item.Tag = requestSendFileEventArgs;

            item.SaveButtonClick += new EventHandler(ItemSaveButtonClick);
            item.SaveToButtonClick += new EventHandler(ItemSaveToButtonClick);
            item.RefuseButtonClick += new EventHandler(ItemRefuseButtonClick);

            Util.ReceiveFileManagerList.Add(msg.GetTag("MD5"), requestSendFileEventArgs);

            this.Invoke(new RequestSendFilePanel(RequestSendFilePanelMethod));
            this.AppendSystemRtf(string.Format("请求接收离线文件 {0}。", msg.GetTag("FileName")));
        }

        /// <summary>
        /// red5视频请求
        /// </summary>
        /// <param name="msg"></param>
        private void Red5AccpetMethod(XMPP.protocol.client.Message msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(Red5AccpetEvent, new object[]{msg});
            }


            if (red5MsgReceive != null)
                if (!red5MsgReceive.IsDisposed)
                    return;
            

            DialogResult result=MsgBox.Show(this, "CSS&IM", "好友" + NickName + "向您发送视频请求，是否接受！", MessageBoxButtons.YesNo);

            if (result==DialogResult.Yes)
            {
                string serUrl = @"http://"+Program.ServerIP+":7070/redfire/video/redfire_2way.html?me={0}&you={1}&key={2}";
                red5MsgReceive = new Red5Msg();
                red5MsgReceive.FriendName = "正在与[" + msg.GetTag("me").ToString() + "]视频通话中";
                red5MsgReceive.FormClosing += new FormClosingEventHandler(red5MsgReceive_FormClosing);
                red5MsgReceive.Accept(string.Format(serUrl, msg.GetTag("you").ToString(), msg.GetTag("me").ToString(), msg.GetTag("key").ToString()));
                red5MsgReceive.Show();
                this.AppendSystemRtf("开始与" + msg.GetTag("me").ToString() + "进行视频通话");
            }
            else
            {
                CSS.IM.XMPP.protocol.client.Message emsg = new CSS.IM.XMPP.protocol.client.Message();
                emsg.Type = MessageType.chat;
                emsg.To = TO_Jid;
                emsg.SetTag("m_type", 12);
                emsg.SetTag("me", msg.GetTag("me").ToString());
                emsg.SetTag("you", msg.GetTag("you").ToString());
                emsg.SetTag("key", msg.GetTag("key").ToString());
                XmppConn.Send(emsg);
                this.AppendSystemRtf("拒绝了" + msg.GetTag("me").ToString() + "视频通话");
            }
        }


        /// <summary>
        /// 拒绝视频请求
        /// </summary>
        /// <param name="msg"></param>
        private void Red5RefuseMethod(XMPP.protocol.client.Message msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(Red5RefuseEvent, new object[] { msg });
            }

            if (red5MsgSend != null || red5MsgSend.IsDisposed)
             {
                 red5MsgSend.Close();
                 red5MsgSend.Dispose();
                 red5MsgSend = null;
             }

            this.AppendSystemRtf("对方" + msg.From.User + "拒绝了你的视频请求");
        }

        #endregion

        #region old文件发送模块

        /// <summary>
        /// 发送文件事件
        /// </summary>
        /// <param name="f"></param>
        private void sendFile(System.IO.FileInfo f)
        {
            CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
            if (OnLine)//如果在线
            {
                try
                {
                    fmsg.SetTag("m_type", 6);//告诉对方要发送文件啦
                    fmsg.Type = MessageType.chat;
                    fmsg.To = TO_Jid;
                    fmsg.SetTag("BPort", udpSendFile.Port);
                    fmsg.SetTag("BIP", Program.LocalHostIP.ToString());
                    fmsg.SetTag("isSend", true);
                    fmsg.SetTag("File", f.FullName.ToString());
                    XmppConn.Send(fmsg);
                    AppendSystemRtf("等待好友查看接收命令.");
                }
                catch (Exception)
                {

                }
            }
            else//如果
            {
                this.Invoke(FileSendInitEvent, f.FullName.ToString());
            }
            //this.Invoke(FileSendInitEvent, f.FullName.ToString());
        }

        /// <summary>
        /// 当收发文件列表控件出现变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private object fileTansfersContainer_ControlRemovedLock = new object();
        private void fileTansfersContainer_ControlRemoved(object sender, ControlEventArgs e)
        {
            lock (fileTansfersContainer_ControlRemovedLock)
            {
                if (fileTansfersContainer.Controls.Count == 0)
                {
                    if (panel_function.Visible)
                    {
                        this.WindowState = FormWindowState.Normal;
                        this.Activate();
                        panel_function.Visible = false;
                        panel_msg.Width = panel_msg.Width + 222;
                        this.Width = this.Width - 220;
                    }
                }
            }
        }

        #endregion

        #region 发送文件
        private void FileSendBuffer(object sender, FileSendBufferEventArgs e)
        {
            FileTransfersItem item = e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    item.TotalTransfersSize += e.Size;
                }));
            }
        }

        private void FileSendAccept(object sender, FileSendEventArgs e)
        {
            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;
            item.Style = FileTransfersItemStyle.Cancel;
            if (item != null)
            {
                //BeginInvoke(new MethodInvoker(delegate()
                //{
                item.Start();
                //}));
            }
            this.AppendSystemRtf(string.Format("对方同意接收文件 {0}。", e.SendFileManager.Name));
        }

        private void FileSendRefuse(object sender, FileSendEventArgs e)
        {
            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    fileTansfersContainer.RemoveItem(item);
                    item.Dispose();
                }));
            }
            this.AppendSystemRtf(string.Format("对方拒绝接收文件 {0} 。", e.SendFileManager.Name));
        }

        private void FileSendCancel(object sender, FileSendEventArgs e)
        {

            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;

            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    fileTansfersContainer.RemoveItem(item);
                    item.Dispose();
                }));
            }
            this.AppendSystemRtf(string.Format("对方取消接收文件 {0} 。", e.SendFileManager.Name)); 
        }

        private void FileSendComplete(object sender, FileSendEventArgs e)
        {
            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    fileTansfersContainer.RemoveItem(item);
                    item.Dispose();
                }));
            }
            this.AppendSystemRtf(string.Format("文件 {0} 发送完成。", e.SendFileManager.Name));
        }

        /// <summary>
        /// 取消文件发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemCancelButtonClick1(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            SendFileManager sendFileManager=item.Tag as SendFileManager;

            if (sendFileManager.ftpClient == null)
            {
                udpSendFile.CancelSend(sendFileManager.MD5);
                fileTansfersContainer.RemoveItem(item);
                this.AppendSystemRtf(string.Format("取消发送文件 {0} 。", sendFileManager.Name));
            }
            else
            {
                sendFileManager.ftpClient.DisConnect(true);
            }
        }

        private void item_OffLineFilesButtonClick(object sender, EventArgs e)
        {

            FileTransfersItem item = sender as FileTransfersItem;
            SendFileManager sendFileManager = item.Tag as SendFileManager;
            this.AppendSystemRtf(string.Format("文件 {0} 取消发送成功。", sendFileManager.Name));
            if (OnLine)
            {
                udpSendFile.CancelSend(sendFileManager.MD5);    
            }

            Util.SendFileManagerList.Add(sendFileManager.MD5, sendFileManager);
            FTPClient ftpClient = new FTPClient(Util.ServerAddress,Util.ftpPath, Util.ftpUser, Util.ftpPswd, Util.ftpPort);
            ftpClient.sendFileManager = sendFileManager;
            sendFileManager.ftpClient = ftpClient;
            item.Tag = sendFileManager;
            item.Style = FileTransfersItemStyle.Cancel;
            this.AppendSystemRtf(string.Format("文件 {0} 更改发送离线文件。",sendFileManager.Name));
            UploadFile ftpPush = new UploadFile(this);
            Thread pushThread = new Thread(ftpPush.PushFile);
            pushThread.Start(ftpClient);
        }

        private class UploadFile 
        {
            ChatFromMsg parent;

            public UploadFile(ChatFromMsg parent)
            {
                this.parent = parent;
            }

            public void PushFile(object ftpObject)
            {
                FTPClient ftpClient = ftpObject as FTPClient;
                try
                {
                    //parent.AppendSystemRtf("服务器地址："+ftpClient.RemoteHost+"  端口:"+ftpClient.RemotePort+" 用户名和密码："+ftpClient.RemoteUser+"  "+ftpClient.RemotePass);
                    ftpClient.Connect();
                    parent.AppendSystemRtf(string.Format("连接附件服务器成功"));
                }
                catch (Exception)
                {
                    parent.ftpClient_PushFileErrorEvent(ftpClient, true);
                    parent.AppendSystemRtf(string.Format("连接附件服务器错误，请联系管理员。"));
                    return;
                }

            aa:
                parent.AppendSystemRtf(string.Format("准备打开用户目录"));
                string isRoot = "";
                string[] dir = ftpClient.Dir("/");
                foreach (string item in dir)
                {
                    string[] items = item.Split(' ');
                    string nitem = items[items.Length - 1].Replace('\r', ' ').Trim();
                    if (nitem == XmppConn.MyJID.User)
                    {
                        isRoot = nitem;
                        break;
                    }
                }

                if (isRoot == "")
                {
                    parent.AppendSystemRtf(string.Format("开始创建用户目录"));
                    ftpClient.MkDir(XmppConn.MyJID.User);
                    parent.AppendSystemRtf(string.Format("创建用户目录成功"));
                    goto aa;
                }
                ftpClient.ChDir("/" + XmppConn.MyJID.User + "/");

                ftpClient.PushProgressEvent += new FTPClient.PushProgressDelegate(parent.ftpClient_PushProgressEvent);
                ftpClient.PushFileSucceedEvent += new FTPClient.PushFileSucceedDelegate(parent.ftpClient_PushFileSucceedEvent);
                ftpClient.PushFileErrorEvent += new FTPClient.PushFileErrorDelegate(parent.ftpClient_PushFileErrorEvent);
                try
                {
                    ftpClient.Put(ftpClient.sendFileManager.FileName, true);
                }
                catch (Exception)
                {
                    ftpClient.DisConnect(false);
                }
            }
        }

        private void ftpClient_PushFileErrorEvent(FTPClient sender, bool args)
        {

            if (InvokeRequired)
            {
                this.Invoke(new FTPClient.PushFileErrorDelegate(ftpClient_PushFileErrorEvent), new object[] { sender, args });
            }

            Util.SendFileManagerList.Remove(sender.sendFileManager.MD5);
            FileTransfersItem item =sender.sendFileManager.Tag as FileTransfersItem;
            fileTansfersContainer.RemoveItem(item);
            this.AppendSystemRtf(string.Format("离线文件 {0} 取消成功。", sender.sendFileManager.Name));
        }

        private void ftpClient_PushFileSucceedEvent(FTPClient sender, string args)
        {
            FileTransfersItem item =sender.sendFileManager.Tag as FileTransfersItem;
            Util.SendFileManagerList.Remove(sender.sendFileManager.MD5);
            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
                    fmsg.Type = MessageType.chat;
                    fmsg.To = TO_Jid;
                    fmsg.SetTag("m_type", 8);
                    fmsg.SetTag("MD5", args);
                    fmsg.SetTag("FileName", System.IO.Path.GetFileName(sender.sendFileManager.FileName));
                    fmsg.SetTag("Length", sender.sendFileManager.Length);
                    fmsg.SetTag("Path", XmppConn.MyJID.User);
                    fmsg.From = XmppConn.MyJID;
                    fmsg.Body = "离线文件" + System.IO.Path.GetFileName(sender.sendFileManager.FileName);
                    XmppConn.Send(fmsg);

                    fileTansfersContainer.RemoveItem(item);
                    item.Dispose();
                }));
            }
            this.AppendSystemRtf(string.Format("离线文件 {0} 发送完成。", sender.sendFileManager.Name));
        }

        public void ftpClient_PushProgressEvent(FTPClient sender, int args)
        {
            FileTransfersItem item = sender.sendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    item.TotalTransfersSize += args;
                }));
            }
        }

        #endregion

        #region 接收文件
       
        private void FileReceiveCancel(object sender, FileReceiveEventArgs e)
        {
            string md5 = string.Empty;
            if (e.ReceiveFileManager != null)
            {
                md5 = e.ReceiveFileManager.MD5;
            }
            else
            {
                md5 = e.Tag.ToString();
            }

            FileTransfersItem item = fileTansfersContainer.Search(md5);

            BeginInvoke(new MethodInvoker(delegate()
            {
                fileTansfersContainer.RemoveItem(item);
            }));

            if (item!=null)
            {
                this.AppendSystemRtf(string.Format("对方取消发送文件文件 {0} 。", item.FileName));    
            }
        }

        private void FileReceiveComplete(object sender, FileReceiveEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate()
            {
                fileTansfersContainer.RemoveItem(e.ReceiveFileManager.MD5);
            }));
            //e.ReceiveFileManager.FileName
            //this.AppendSystemRtf(string.Format("文件 {0} 接收完成，MD5 校验: {1}。", e.ReceiveFileManager.Name, e.ReceiveFileManager.Success));
            //this.AppendSystemRtf(string.Format("文件 {0} 接收完成。", e.ReceiveFileManager.Name));
            this.AppendSystemRtf(string.Format(@"文件<file:\\{0}>接收完成。", e.ReceiveFileManager.FileName));
        }

        private void FileReceiveBuffer(object sender, FileReceiveBufferEventArgs e)
        {
            FileTransfersItem item = fileTansfersContainer.Search(
                e.ReceiveFileManager.MD5);
            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    item.TotalTransfersSize += e.Size;
                }));
            }
        }

        private delegate void RequestSendFilePanel();
        private void RequestSendFile(object sender, RequestSendFileEventArgs e)
        {
            TraFransfersFileStart traFransfersFileStart = e.TraFransfersFileStart;
            BeginInvoke(new MethodInvoker(delegate()
            {
                bool isExist = false;
                foreach (FileTransfersItem citem in fileTansfersContainer.Controls)
                {
                    if(citem.Style==FileTransfersItemStyle.ReadyReceive)
                    {
                        if (citem.Name == traFransfersFileStart.MD5)
                        {
                            isExist = true;
                        }
                    }
                }

                if (isExist)
                    return;

                FileTransfersItem item = fileTansfersContainer.AddItem(
                    traFransfersFileStart.MD5,
                    "接收文件",
                    traFransfersFileStart.FileName,
                    traFransfersFileStart.Image,
                    traFransfersFileStart.Length,
                    FileTransfersItemStyle.ReadyReceive);

                item.BaseColor = _baseColor;
                item.BorderColor = _borderColor;
                item.ProgressBarBarColor = _progressBarBarColor;
                item.ProgressBarBorderColor = _progressBarBorderColor;
                item.ProgressBarTextColor = _progressBarTextColor;

                item.Tag = e;
                item.SaveButtonClick += new EventHandler(ItemSaveButtonClick);
                item.SaveToButtonClick += new EventHandler(ItemSaveToButtonClick);
                item.RefuseButtonClick += new EventHandler(ItemRefuseButtonClick);
                this.Invoke( new RequestSendFilePanel(RequestSendFilePanelMethod));
                this.AppendSystemRtf(string.Format("请求接收文件 {0}。", traFransfersFileStart.FileName));
            }));
            

        }
        private void RequestSendFilePanelMethod()
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new RequestSendFilePanel(RequestSendFilePanelMethod));
            }

            if (panel_function.Visible==false)
            {
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                panel_function.Visible = true;
                this.Width = this.Width + 220;
                panel_msg.Width = panel_msg.Width -222;
            }
        }

        private void ItemRefuseButtonClick(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            RequestSendFileEventArgs rse = item.Tag as RequestSendFileEventArgs;
            if (item.Style == FileTransfersItemStyle.ReadyReceive)
            {
                rse.Cancel = true;
                fileTansfersContainer.RemoveItem(item);
                item.Dispose();

                this.AppendSystemRtf(string.Format("拒绝接收文件 {0}。", rse.TraFransfersFileStart.FileName));
                udpReceiveFile.AcceptReceive(rse);
            }
            else
            {
                rse.ftpClient.DisConnect(true);
                Util.ReceiveFileManagerList.Remove(item.Name);
                fileTansfersContainer.RemoveItem(item);
                this.AppendSystemRtf(string.Format("接收离线文件 {0} 取消成功。", item.FileName));

                CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
                fmsg.Type = MessageType.chat;
                fmsg.To = TO_Jid;
                fmsg.SetTag("m_type", 9);
                fmsg.SetTag("FileName", item.FileName);
                fmsg.From = XmppConn.MyJID;
                fmsg.Body = "离线文件" + item.FileName + "拒绝接受。";
                XmppConn.Send(fmsg);
            }
        }

        private void ItemSaveToButtonClick(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            RequestSendFileEventArgs rse = item.Tag as RequestSendFileEventArgs;
            if (item.Style == FileTransfersItemStyle.ReadyReceive)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    rse.Path = fbd.SelectedPath + "\\";
                    this.AppendSystemRtf(string.Format("同意接收文件 {0}。", rse.TraFransfersFileStart.FileName));
                    ControlTag tag = new ControlTag(
                         rse.TraFransfersFileStart.MD5,
                         rse.TraFransfersFileStart.FileName,
                         rse.RemoteIP);
                    item.Tag = tag;
                    item.Style = FileTransfersItemStyle.Cancel;
                    item.CancelButtonClick += new EventHandler(ItemCancelButtonClick2);
                    item.Start();
                    udpReceiveFile.AcceptReceive(rse);
                }
            }
            else
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    rse.Path = fbd.SelectedPath + "\\";
                    this.AppendSystemRtf(string.Format("同意接收文件 {0}。", rse.ftpClient.fileTransfersItem.FileName));
                    ControlTag tag = new ControlTag(
                        rse.ftpClient.fileTransfersItem.Name,
                        rse.ftpClient.fileTransfersItem.FileName,
                        null);
                    tag.ftpClient = rse.ftpClient;
                    item.Tag = tag;
                    item.Style = FileTransfersItemStyle.Cancel;
                    DownloadFile downloadFile = new DownloadFile(this, rse.ftpClient);
                    downloadFile.receivePath = fbd.SelectedPath + "\\";
                    Thread pushThread = new Thread(downloadFile.GetFile);
                    pushThread.Start(rse.ftpClient.fileTransfersItem.Name);

                    CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
                    fmsg.Type = MessageType.chat;
                    fmsg.To = TO_Jid;
                    fmsg.SetTag("m_type", 9);
                    fmsg.SetTag("FileName", rse.ftpClient.fileTransfersItem.FileName);
                    fmsg.From = XmppConn.MyJID;
                    fmsg.Body = "离线文件" + rse.ftpClient.fileTransfersItem.FileName + "开始接收。";
                    XmppConn.Send(fmsg);
                }
            }
        }

        private void ItemSaveButtonClick(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            RequestSendFileEventArgs rse = item.Tag as RequestSendFileEventArgs;
            if (item.Style == FileTransfersItemStyle.ReadyReceive)
            {
                rse.Path = Util.receiveImage;

                this.AppendSystemRtf(string.Format("同意接收文件 {0}。", rse.TraFransfersFileStart.FileName));
                ControlTag tag = new ControlTag(
                    rse.TraFransfersFileStart.MD5,
                    rse.TraFransfersFileStart.FileName,
                    rse.RemoteIP);
                item.Tag = tag;
                item.Style = FileTransfersItemStyle.Cancel;
                item.CancelButtonClick += new EventHandler(ItemCancelButtonClick2);
                item.Start();
                udpReceiveFile.AcceptReceive(rse);
            }
            else if (item.Style == FileTransfersItemStyle.FtpGet)
            {

                ControlTag tag = new ControlTag(
                    rse.ftpClient.fileTransfersItem.Name,
                    rse.ftpClient.fileTransfersItem.FileName,
                    null);
                tag.ftpClient = rse.ftpClient;
                item.Tag = tag;
                item.Style = FileTransfersItemStyle.Cancel;
                item.CancelButtonClick += new EventHandler(ItemCancelButtonClick2);

                DownloadFile downloadFile = new DownloadFile(this,rse.ftpClient);
                Thread pushThread = new Thread(downloadFile.GetFile);
                pushThread.Start(rse.ftpClient.fileTransfersItem.Name);

                CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
                fmsg.Type = MessageType.chat;
                fmsg.To = TO_Jid;
                fmsg.SetTag("m_type", 9);
                fmsg.SetTag("FileName", rse.ftpClient.fileTransfersItem.FileName);
                fmsg.From = XmppConn.MyJID;
                fmsg.Body = "离线文件" + rse.ftpClient.fileTransfersItem.FileName + "开始接收。";
                XmppConn.Send(fmsg);
            }
        }

        private void ItemCancelButtonClick2(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            if (item.Style == FileTransfersItemStyle.Cancel)
            {
                ControlTag tag = item.Tag as ControlTag;

                if (tag.ftpClient == null)
                {
                    
                    udpReceiveFile.CancelReceive(tag.MD5, tag.RemoteIP);
                    fileTansfersContainer.RemoveItem(item);
                    item.Dispose();
                    this.AppendSystemRtf(string.Format("取消接收文件 {0}。", tag.FileName));
                }
                else
                {
                    tag.ftpClient.DisConnect(true);

                    CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
                    fmsg.Type = MessageType.chat;
                    fmsg.To = TO_Jid;
                    fmsg.SetTag("m_type", 9);
                    fmsg.SetTag("FileName", item.FileName);
                    fmsg.From = XmppConn.MyJID;
                    fmsg.Body = "离线文件" + item.FileName + "拒绝接受。";
                    XmppConn.Send(fmsg);
                }
            }
        }

        private class DownloadFile
        {
            ChatFromMsg parent;
            FTPClient ftpClient;
            public string receivePath = Util.receiveImage;

            public DownloadFile(ChatFromMsg parent, FTPClient ftpClient)
            {
                this.parent = parent;
                this.ftpClient = ftpClient;
            }

            public void GetFile(object name)
            {
                ftpClient.GetFile(name.ToString(), receivePath, ftpClient.fileTransfersItem.FileName);
            }
        }

        private void ftpClient_GetFileSucceedEvent(FTPClient sender, string args)
        {
            try
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    fileTansfersContainer.RemoveItem(sender.fileTransfersItem.Name);
                }));
                Util.ReceiveFileManagerList.Remove(sender.fileTransfersItem.Name);
                this.AppendSystemRtf(string.Format(@"文件<file:\\{0}>接收完成。", sender.fileTransfersItem.receivePath + sender.fileTransfersItem.FileName));

                CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
                fmsg.Type = MessageType.chat;
                fmsg.To = TO_Jid;
                fmsg.SetTag("m_type", 9);
                fmsg.SetTag("FileName", sender.fileTransfersItem.FileName);
                fmsg.From = XmppConn.MyJID;
                fmsg.Body = "离线文件" + sender.fileTransfersItem.FileName + "接收成功。";
                XmppConn.Send(fmsg);
            }
            catch (Exception)
            {
                

            }
        }

        private void ftpClient_GetFileProgressEvent(FTPClient sender, int args)
        {
            FileTransfersItem item = fileTansfersContainer.Search(sender.fileTransfersItem.Name);
            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    item.TotalTransfersSize += args;
                }));
            }
        }

        private void ftpClient_GetFileErrorEvent(FTPClient sender, bool args)
        {
            if (InvokeRequired)
            {
                try
                {
                    this.Invoke(new FTPClient.GetFileErrorDelegate(ftpClient_GetFileErrorEvent), new object[] { sender, args });
                }
                catch (Exception)
                {
                    return;
                }
                
            }
            Util.ReceiveFileManagerList.Remove(sender.fileTransfersItem.Name);
            FileTransfersItem item = sender.fileTransfersItem;
            fileTansfersContainer.RemoveItem(item);
            this.AppendSystemRtf(string.Format("接收离线文件 {0} 取消成功。", sender.fileTransfersItem.FileName));
        }

        #endregion

        #region 收发发送截图

        private void sendSelfImage()//发送图片文件
        {
            CSS.IM.Library.gifCollections tempGifs = PicQueue.Clone() as CSS.IM.Library.gifCollections;
            try
            {
                foreach (CSS.IM.Library.MyPicture pic in tempGifs)
                    if (pic.IsSent)
                    {
                        System.IO.FileInfo f = new System.IO.FileInfo(Util.sendImage + pic.ImageMD5 + ".gif");

                        //this.ImageTransfers(true, f.FullName, pic.ImageMD5, (int)f.Length, f.Extension, pic.ImageMD5);

                        FTPClient ftpClient = new FTPClient(Util.ServerAddress, Util.ftpPath, Util.ftpUser, Util.ftpPswd, Util.ftpPort);
                        ftpClient.FtpPath = XmppConn.MyJID.User;
                        UploadImage uploadImage = new UploadImage();
                        uploadImage.ftpClient = ftpClient;
                        uploadImage.ImageName = pic.ImageMD5;
                        uploadImage.Name = f.FullName;
                        uploadImage.parent = this;

                        Thread pushThread = new Thread(uploadImage.Upload);
                        pushThread.Start();

                        PicQueue.Romove(pic);//将richTextBox中的自定义图片清除掉，以便下次继续发送消息时出现再次发送的情况
                        //System.Threading.Thread.Sleep(500);
                    }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("发送图片时错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 发送或接收图片文件
        /// </summary>
        /// <param name="IsSend">true是发送者,false是接收者</param>
        /// <param name="fullFileName">要发送的文件的绝对路径，如果接收则传空字符串</param>
        /// <param name="FileName">文件名</param>
        /// <param name="FileLen">文件的长度</param>
        /// <param name="fileExtension">文件扩展名</param>
        /// <param name="FileMD5Value">文件的MD5值</param>
        public void ImageTransfers(bool IsSend, string fullFileName, string FileName, int FileLen, string fileExtension, string FileMD5Value)
        {

            if (this.imageP2Ps.find(FileMD5Value) == null)//如果图片不在传输队列，则创建新文件传输组件进行传输
            {
                CSS.IM.Library.Controls.p2pFileTransmitEX imageTransmit = new CSS.IM.Library.Controls.p2pFileTransmitEX();
                this.imageP2Ps.add(imageTransmit);//添加文件传输组件到传输队列

                imageTransmit.fileTransmitGetUDPPort += new CSS.IM.Library.Controls.p2pFileTransmitEX.GetUDPPortEventHandler(imageTransmit_fileTransmitGetUDPPort);
                imageTransmit.fileTransmitted += new CSS.IM.Library.Controls.p2pFileTransmitEX.fileTransmittedEventHandler(imageTransmit_fileTransmitted);//图片传输结束
                imageTransmit.fileTransmitOutTime += new p2pFileTransmitEX.fileTransmitOutTimeEventHandler(imageTransmit_fileTransmitOutTime);
                //imageTransmit.fileTransmitConnected += new p2pFileTransmit.ConnectedEventHandler(imageTransmit_fileTransmitConnected);
                //imageTransmit.fileTransmitCancel += new p2pFileTransmit.fileTransmitCancelEventHandler(imageTransmit_fileTransmitCancel);//图片传输取消或异常中断
                //imageTransmit.getFileProxyID += new p2pFileTransmit.getFileProxyIDEventHandler(imageTransmit_getFileProxyID);
                //imageTransmit.fileTransmitting += new p2pFileTransmit.fileTransmittingEventHandler(imageTransmit_fileTransmitting);
                if (IsSend)
                {
                    string fileInfo = FileName + "|" + FileLen.ToString() + "|" + fileExtension + "|" + FileMD5Value;//初次请求发送文件时要先发送“控件参数”到对方，请求对方创建“文件发送控件”并建立连接
                    CSS.IM.Library.Class.Msg msg = new CSS.IM.Library.Class.Msg((byte)ProtocolClient.ImageTransmitRequest, TextEncoder.textToBytes(fileInfo));
                    this.LB_sockUDP.Send(RemotBaseUDPIP, RemotBaseUDPPort, msg.getBytes());
                    imageTransmit.ServerIp = RemotBaseUDPIP;
                }
                else
                {
                    System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(Util.receiveImage);
                    if (!dInfo.Exists)
                        dInfo.Create();
                    imageTransmit.ServerIp = RemotBaseUDPIP;
                    imageTransmit.startIncept(Util.receiveImage + FileMD5Value + fileExtension);//自动接收图片
                }
                imageTransmit.SetParameter(IsSend, fullFileName, FileName, FileLen, fileExtension, FileMD5Value);//设置文件传输组件相关参数
            }
        }

        /// <summary>
        /// 传输图片超时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageTransmit_fileTransmitOutTime(object sender, fileTransmitEvnetArgs e)
        {
            System.Diagnostics.Debug.WriteLine("传输文件超时"+e.errorMessage);
        }

        /// <summary>
        /// 图片传输前的获取对方的端口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Port"></param>
        /// <param name="udpHandshakeInfoClass"></param>
        private void imageTransmit_fileTransmitGetUDPPort(object sender, int Port, bool udpHandshakeInfoClass)
        {
            CSS.IM.Library.Class.Msg msg = new CSS.IM.Library.Class.Msg((byte)ProtocolClient.ImageTransmitGetUDPPort);
            msg.Content = TextEncoder.textToBytes((sender as CSS.IM.Library.Controls.p2pFileTransmitEX).FileMD5Value + "|" + Port.ToString() + "|" + udpHandshakeInfoClass.ToString());//将获得的本地UDP端口号传输给对方
            LB_sockUDP.Send(RemotBaseUDPIP, RemotBaseUDPPort, msg.getBytes());
        }

        /// <summary>
        /// 图片传输完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageTransmit_fileTransmitted(object sender, CSS.IM.Library.Controls.fileTransmitEvnetArgs e)
        {
            imageTransmittedDelegate d = new imageTransmittedDelegate(imageTransmitted);
            this.BeginInvoke(d, sender, e);
        }

        /// <summary>
        /// 图片传输完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageTransmitted(object sender, CSS.IM.Library.Controls.fileTransmitEvnetArgs e)
        {
            //Calculate.WirteLog(e.isSend.ToString() + "图片完成传输(" + e.currTransmittedLen.ToString() + "/" + e.fileLen.ToString() + ")");

            if (!e.isSend)//如果是图片接收者，则将传输完成的图片显示出来
            {
                CSS.IM.Library.MyPicture pic;
                if (e.isSend)
                {
                    pic = this.RTBRecord.findPic(e.FileMD5Value);
                }
                else
                {
                    pic = this.RTBRecord.findPic(e.FileMD5Value + "_r");
                }
                if (pic != null)
                {
                    pic.Image = System.Drawing.Image.FromFile(e.fullFileName+".gif");//显示图片
                    pic.Invalidate();
                    this.RTBRecord.Invalidate();
                    RTBRecord.Select(RTBRecord.TextLength + pic.Image.Height, 0);
                    RTBRecord.ScrollToCaret();
                }
            }

            if (sender != null)
            {
                CSS.IM.Library.Controls.p2pFileTransmitEX p2pImage = sender as CSS.IM.Library.Controls.p2pFileTransmitEX;
                this.imageP2Ps.Romove(p2pImage);//在传输队列中删除文件传输组件
                if (p2pImage != null)
                    p2pImage.Dispose();
            }


        }

        private class UploadImage
        {
            public string Name { set; get; }
            public string ImageName { set; get; }
            public FTPClient ftpClient { set; get; }
            public ChatFromMsg parent { set; get; }

            public void Upload()
            {
                try
                {
                    ftpClient.Connect();
                }
                catch (Exception)
                {
                    parent.AppendSystemRtf(string.Format("连接附件服务器错误，请联系管理员。"));
                    return;
                }

            aa:
                string isRoot = "";
                string[] dir = ftpClient.Dir("/");
                foreach (string item in dir)
                {
                    string[] items = item.Split(' ');
                    string nitem = items[items.Length - 1].Replace('\r', ' ').Trim();
                    if (nitem == ftpClient.FtpPath)
                    {
                        isRoot = nitem;
                        break;
                    }
                }

                if (isRoot == "")
                {
                    ftpClient.MkDir(ftpClient.FtpPath);
                    goto aa;
                }
                ftpClient.ChDir("/" + ftpClient.FtpPath + "/");

                ftpClient.PushProgressEvent += new FTPClient.PushProgressDelegate(parent.ftpClient_ImagePushProgressEvent);
                ftpClient.PushFileSucceedEvent += new FTPClient.PushFileSucceedDelegate(parent.ftpClient_ImagePushFileSucceedEvent);
                ftpClient.PushFileErrorEvent += new FTPClient.PushFileErrorDelegate(parent.ftpClient_ImagePushFileErrorEvent);
                try
                {
                    ftpClient.Put(Name, ImageName, true);
                }
                catch (Exception)
                {
                    ftpClient.DisConnect(false);
                }
            }
        }

        private void ftpClient_ImagePushProgressEvent(FTPClient sender, int args)
        {
            //FileTransfersItem item = sender.sendFileManager.Tag as FileTransfersItem;
            //if (item != null)
            //{
            //    BeginInvoke(new MethodInvoker(delegate()
            //    {
            //        item.TotalTransfersSize += args;
            //    }));
            //}
        }

        private void ftpClient_ImagePushFileSucceedEvent(FTPClient sender, string args)
        {
            CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
            fmsg.Type = MessageType.chat;
            fmsg.To = TO_Jid;
            fmsg.SetTag("m_type", 10);
            fmsg.SetTag("FileName", args);
            fmsg.SetTag("Path", sender.FtpPath);
            fmsg.From = XmppConn.MyJID;
            fmsg.Body = "发送屏幕截图";
            XmppConn.Send(fmsg);

            System.Diagnostics.Debug.WriteLine("图片上传完成:"+args);
        }

        private void ftpClient_ImagePushFileErrorEvent(FTPClient sender, bool args)
        {

            if (InvokeRequired)
            {
                this.Invoke(new FTPClient.PushFileErrorDelegate(ftpClient_ImagePushFileErrorEvent), new object[] { sender, args });
            }

            //Util.SendFileManagerList.Remove(sender.sendFileManager.MD5);
            //FileTransfersItem item = sender.sendFileManager.Tag as FileTransfersItem;
            //fileTansfersContainer.RemoveItem(item);
            //this.AppendSystemRtf(string.Format("离线文件 {0} 取消成功。", sender.sendFileManager.Name));
        }

        private class DownloadImage
        {
            public string remoteImageName { set; get; }
            public FTPClient ftpClient { set; get; }
            public ChatFromMsg parent { set; get; }

            public void Download()
            {
                ftpClient.GetFileProgressEvent += new FTPClient.GetFileProgressDelegate(parent.ftpClient_ImageGetFileProgressEvent);
                ftpClient.GetFileSucceedEvent += new FTPClient.GetFileSucceedDelegate(parent.ftpClient_ImageGetFileSucceedEvent);
                ftpClient.GetFileErrorEvent += new FTPClient.GetFileErrorDelegate(parent.ftpClient_ImageGetFileErrorEvent);
                ftpClient.ChDir("/" + ftpClient.FtpPath + "/");

                ftpClient.GetFile(remoteImageName, Util.receiveImage);
            }
        }

        private void ftpClient_ImageGetFileProgressEvent(FTPClient sender, int args)
        {
            //FileTransfersItem item = sender.sendFileManager.Tag as FileTransfersItem;
            //if (item != null)
            //{
            //    BeginInvoke(new MethodInvoker(delegate()
            //    {
            //        item.TotalTransfersSize += args;
            //    }));
            //}
        }

        private void ftpClient_ImageGetFileSucceedEvent(FTPClient sender, string args)
        {
            CSS.IM.Library.Controls.fileTransmitEvnetArgs e = new fileTransmitEvnetArgs(false, Util.receiveImage + args, args, null, 0, 0, args);
            imageTransmittedDelegate d = new imageTransmittedDelegate(imageTransmitted);
            this.BeginInvoke(d, null, e);
        }

        private void ftpClient_ImageGetFileErrorEvent(FTPClient sender, bool args)
        {

            if (InvokeRequired)
            {
                this.Invoke(new FTPClient.PushFileErrorDelegate(ftpClient_ImageGetFileErrorEvent), new object[] { sender, args });
            }

            //Util.SendFileManagerList.Remove(sender.sendFileManager.MD5);
            //FileTransfersItem item = sender.sendFileManager.Tag as FileTransfersItem;
            //fileTansfersContainer.RemoveItem(item);
            //this.AppendSystemRtf(string.Format("离线文件 {0} 取消成功。", sender.sendFileManager.Name));
        }

        #endregion


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {

            try
            {
                Util.ChatForms.Remove(TO_Jid.Bare);
            }
            catch (Exception)
            {

            }

            try
            {
                XmppConn.MessageGrabber.Remove(TO_Jid);
            }
            catch (Exception)
            {

            }

            try
            {
                udpReceiveFile.Dispose();
                udpReceiveFile = null;
            }
            catch (Exception)
            {
                
            }

            try
            {
                udpSendFile.Dispose();
                udpSendFile = null;
            }
            catch (Exception)
            {

            }
            

            RTBRecord.Dispose();
            RTBRecord = null;

            rtfSend.Dispose();
            rtfSend = null;

            if (QQcm_send_key != null)
            {
                QQcm_send_key.Dispose();
                QQcm_send_key = null;
            }

            try
            {
                if (avForm != null)
                {
                    avForm.AVClose();
                    avForm.Dispose();
                    avForm = null;
                }

            }
            catch (Exception)
            {

            }

            try
            {
                if (ravForm != null)
                {
                    ravForm.AVClose();
                    ravForm.Dispose();
                    ravForm = null;
                }
            }
            catch (Exception)
            {

            }

            try
            {
                LB_sockUDP.CloseSock();
                LB_sockUDP.Dispose();
                LB_sockUDP = null;
            }
            catch (Exception)
            {

            }


            if (emotionDropdown != null)
            {
                emotionDropdown.Dispose();
            }

            if (main_msg != null)
            {
                main_msg = null;
            }

            if (PicQueue != null)
            {
                PicQueue.Dispose();
                PicQueue.Clear();
                PicQueue = null;
            }

            if (Opposite != null)
            {
                Opposite = null;
            }

            if (imageP2Ps != null)
            {
                imageP2Ps.Dispose();
                imageP2Ps.Clear();
                imageP2Ps = null;
            }

            if (Close_Check!=null)
            {
                Close_Check.Enabled = false;
                Close_Check.Dispose();
                Close_Check = null;
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

            System.GC.Collect();
        }

        /// <summary>
        /// 确定选择表情事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmotionContainer_ItemClick(object sender, UI.Face.EmotionItemMouseClickEventArgs e)
        {
            rtfSend.addGifControl(e.Item.Tag.ToString(), e.Item.Image);
        }

        /// <summary>
        /// 窗体关闭的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatFromMsg_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            if (avForm != null)
            {
                if (avForm.IsDisposed == false)
                {
                    result = MsgBox.Show(this, "CSS&IM", "当前正在视频是否结束聊天？", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        avForm.isBtn_hangup = true;
                        avForm.AVClose();
                        avForm_AVCloseEvent();
                        e.Cancel = false;

                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            if (ravForm != null)
            {
                if (ravForm.IsDisposed == false)
                {
                    result = MsgBox.Show(this, "CSS&IM", "当前正在视频是否结束聊天？", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        ravForm.isBtn_hangup = true;
                        ravForm.AVClose();
                        avForm_AVCloseEvent();
                        e.Cancel = false;

                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            if (red5MsgSend != null)
            {
                if (red5MsgSend.IsDisposed == false)
                {
                    red5MsgSend.Close();
                    red5MsgSend.Dispose();
                    red5MsgSend = null;
                }
            }

            if (red5MsgReceive != null)
            {
                if (red5MsgReceive.IsDisposed == false)
                {
                    red5MsgReceive.Close();
                    red5MsgReceive.Dispose();
                    red5MsgReceive = null;
                }
            }

            if (fileTansfersContainer.Controls.Count> 0)
            {

                result = MsgBox.Show(this, "CSS&IM", "当前有正在发送的文件是否取消发送？", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {

                    List<FileTransfersItem> FTIList = new List<FileTransfersItem>();
                    foreach (Control citem in fileTansfersContainer.Controls)
                    {
                        FileTransfersItem item = citem as FileTransfersItem;
                        FTIList.Add(item);       
                    }

                    foreach (FileTransfersItem item in FTIList)
                    {
                        if (item.Style==FileTransfersItemStyle.Send)
                        {
                            SendFileManager sendFileManager = item.Tag as SendFileManager;
                            udpSendFile.CancelSend(sendFileManager.MD5);
                            fileTansfersContainer.RemoveItem(item);
                            item.Dispose();
                        }
                        else if (item.Style == FileTransfersItemStyle.Cancel)
                        {
                            SendFileManager sendFileManager = item.Tag as SendFileManager;
                            sendFileManager.ftpClient.DisConnect(true);
                        }
                        else if (item.Style == FileTransfersItemStyle.ReadyReceive)
                        {
                            RequestSendFileEventArgs rse = item.Tag as RequestSendFileEventArgs;
                            if (rse != null)
                            {
                                rse.Cancel = true;
                                fileTansfersContainer.RemoveItem(item);
                                item.Dispose();
                                udpReceiveFile.AcceptReceive(rse);
                            }
                        }
                        else if (item.Style == FileTransfersItemStyle.Receive)
                        {
                            ControlTag tag = item.Tag as ControlTag;
                            if (tag != null)
                            {
                                udpReceiveFile.CancelReceive(tag.MD5, tag.RemoteIP);
                                fileTansfersContainer.RemoveItem(item);
                                item.Dispose();
                            }
                        }
                        Thread.Sleep(500);
                    }


                    if (panel_function.Visible)
                    {
                        panel_function.Visible = false;
                        this.Width = this.Width - panel_function.Width;
                        panel_msg.Width = panel_msg.Width + panel_function.Width + 2;
                        
                    }
                    e.Cancel = false;

                }
                else
                {
                    e.Cancel = true;
                    return;
                }
               
            }

            if(!Close_Check.Enabled)
            {
                Close_Check.Enabled=true;
            }
            else
            {
                this.Dispose();
            }
            //= Close_Check.Enabled == false ? true : false;
        }

        /// <summary>
        /// 选择字体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_font_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.FontDialog fd = new FontDialog())
            {
                fd.Font = this.rtfSend.Font;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    CSS.IM.UI.Util.Path.SFong = fd.Font;

                this.rtfSend.Font = CSS.IM.UI.Util.Path.SFong;

                Document doc = new Document();
                doc.LoadFile(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
                Settings.Settings config = new Settings.Settings();

                CSS.IM.App.Settings.SFont font = doc.RootElement.SelectSingleElement(typeof(Settings.SFont), false) as Settings.SFont;

                if (font==null)
                {
                    font = new SFont();
                }

                font.Name = rtfSend.Font.Name;
                font.Size = rtfSend.Font.Size;
                font.Bold = rtfSend.Font.Bold;
                font.Italic = rtfSend.Font.Italic;
                font.Strikeout = rtfSend.Font.Strikeout;
                config.Font = font;
                config.Color = doc.RootElement.SelectSingleElement(typeof(Settings.SColor), false) as Settings.SColor;
                config.Paths = doc.RootElement.SelectSingleElement(typeof(Settings.Paths), false) as Settings.Paths;

                doc.Clear();
                doc.ChildNodes.Add(config);
                doc.Save(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
            }
        }

        /// <summary>
        /// 选择字体颜色样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_FontColor_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.ColorDialog cd = new ColorDialog())
            {
                cd.Color = this.rtfSend.ForeColor;
                if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    CSS.IM.UI.Util.Path.SColor = cd.Color;

                this.rtfSend.ForeColor = CSS.IM.UI.Util.Path.SColor;

                Document doc = new Document();
                doc.LoadFile(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
                Settings.Settings config = new Settings.Settings();

                CSS.IM.App.Settings.SColor color = doc.RootElement.SelectSingleElement(typeof(Settings.SColor), false) as Settings.SColor;

                if (color==null)
                {
                    color = new SColor();
                }

                Color top_cl = rtfSend.ForeColor;
                byte[] top_cby = BitConverter.GetBytes(top_cl.ToArgb());
                color.CA = top_cby[0];
                color.CR = top_cby[1];
                color.CG = top_cby[2];
                color.CB = top_cby[3];

                config.Color = color;
                config.Font = doc.RootElement.SelectSingleElement(typeof(Settings.SFont), false) as Settings.SFont;
                config.Paths = doc.RootElement.SelectSingleElement(typeof(Settings.Paths), false) as Settings.Paths;

                doc.Clear();
                doc.ChildNodes.Add(config);
                doc.Save(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
            }
        }

        /// <summary>
        /// 选择表情事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_face_Click(object sender, EventArgs e)
        {
            if (emotionDropdown==null)
            {
                emotionDropdown = new EmotionDropdown();
                emotionDropdown.EmotionContainer.ItemClick+=new UI.Face.EmotionItemMouseEventHandler(EmotionContainer_ItemClick);
            }
            emotionDropdown.Show(btn_face);
        }

        /// <summary>
        /// 发送截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_screen_Click(object sender, EventArgs e)
        {
            if (Util.ScreenImage)
                return;

            Util.ScreenImage = true;
            using (CaptureImageTool capture = new CaptureImageTool())
            {
                capture.ShowDialog();
                if (capture.Image != null)
                {
                    System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(Util.sendImage);
                    if (!dInfo.Exists)
                        dInfo.Create();

                    string fileName = Util.sendImage + "temp.gif";

                    capture.Image.Save(fileName,ImageFormat.Jpeg);
                    string md5 = Hasher.GetMD5Hash(fileName);
                    string Md5fileName = Util.sendImage + md5 + ".gif";

                    if (!System.IO.File.Exists(Md5fileName))
                    {
                        System.IO.File.Delete(fileName);
                        capture.Image.Save(Md5fileName, ImageFormat.Jpeg);
                    }
                    try
                    {
                        this.rtfSend.addGifControl(md5, capture.Image);
                    }
                    catch (Exception) { };
                }
                capture.Dispose();
                Util.ScreenImage = false;
            }
        }

        /// <summary>
        /// 拖曳文件到rtfSend边源的时候 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtfSend_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// rtfSend拖曳文件结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtfSend_DragDrop(object sender, DragEventArgs e)
        {
            Array arr = (Array)e.Data.GetData(DataFormats.FileDrop);
            foreach (var item in arr)
            {
                System.IO.FileInfo f = new System.IO.FileInfo(item.ToString());
                if (f.Exists)
                {
                    sendFileDelegate Dg = new sendFileDelegate(sendFile);//异步处理计算文件 MD5值
                    this.Invoke(Dg, f);
                }
            }
        }

        /// <summary>
        /// 更新消息显示
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isSend"></param>
        public void RTBRecord_Show(CSS.IM.XMPP.protocol.client.Message msg, bool isSend)
        {

            string sqlstr = "insert into ChatMessageLog (Belong,Jid,[MessageLog],[DateNow])values ({0},{1},{2},{3})";
            sqlstr = String.Format(sqlstr,
                "'" + XmppConn.MyJID.Bare.ToString() + "'",
                "'" + (isSend == true ? msg.To.Bare.ToString() : msg.From.Bare.ToString()) + "'",
                "'" + msg.ToString() + "'",
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            CSS.IM.Library.Data.OleDb.ExSQL(sqlstr);

            System.Drawing.FontStyle fontStyle = new System.Drawing.FontStyle();
            System.Drawing.Font ft = null;

            #region 获取字体
            try
            {
                if (msg.GetTagBool("FBold"))
                {
                    fontStyle = System.Drawing.FontStyle.Bold;
                }
                if (msg.GetTagBool("FItalic"))
                {
                    fontStyle = fontStyle | System.Drawing.FontStyle.Italic;
                }
                if (msg.GetTagBool("FStrikeout"))
                {
                    fontStyle = fontStyle | System.Drawing.FontStyle.Strikeout;
                }
                if (msg.GetTagBool("FUnderline"))
                {
                    fontStyle = fontStyle | System.Drawing.FontStyle.Underline;
                }
                ft = new System.Drawing.Font(msg.GetTag("FName"), float.Parse(msg.GetTag("FSize")), fontStyle);
            }
            catch (Exception)
            {
                ft = RTBRecord.Font;
            }
            #endregion

            #region 获取颜色
            Color cl = RTBRecord.ForeColor;
            try
            {
                byte[] cby = new byte[4];
                cby[0] = Byte.Parse(msg.GetTag("CA"));
                cby[1] = Byte.Parse(msg.GetTag("CR"));
                cby[2] = Byte.Parse(msg.GetTag("CG"));
                cby[3] = Byte.Parse(msg.GetTag("CB"));
                cl = Color.FromArgb(BitConverter.ToInt32(cby, 0));

            }
            catch
            {
                cl = RTBRecord.ForeColor;
            }
            #endregion

            int iniPos = this.RTBRecord.TextLength;//获得当前记录richBox中最后的位置
            String msgtext = msg.Body;
            RTBRecord.Select(RTBRecord.TextLength, 0);
            RTBRecord.ScrollToCaret();

            string face = "";
            try
            {
                if (msg.GetTag("face")!=null)
                {
                    face = msg.GetTag("face").ToString();
                }
                else
                {
                    face = "";
                }
            }
            catch (Exception)
            {
                
            }


            if (face != "")//如果消息中有图片，则添加图片
            {
                string[] imagePos = face.Split('|');
                int addPos = 0;//
                int currPos = 0;//当前正要添加的文本位置
                int textPos = 0;
                for (int i = 0; i < imagePos.Length - 1; i++)
                {
                    string[] imageContent = imagePos[i].Split(',');//获得图片所在的位置、图片名称 
                    currPos = Convert.ToInt32(imageContent[0]);//获得图片所在的位置

                    this.RTBRecord.AppendText(msgtext.Substring(textPos, currPos - addPos));
                    this.RTBRecord.SelectionStart = this.RTBRecord.TextLength;

                    textPos += currPos - addPos;
                    addPos += currPos - addPos;

                    Image image = null;

                    if (emotionDropdown==null)
                    {
                        emotionDropdown = new EmotionDropdown();
                        emotionDropdown.EmotionContainer.ItemClick += new UI.Face.EmotionItemMouseEventHandler(EmotionContainer_ItemClick);
                    }

                    if (emotionDropdown.faces.ContainsKey(imageContent[1]))
                    {
                        if (this.RTBRecord.findPic(imageContent[1]) == null)
                            image = CSS.IM.UI.Util.ResClass.GetImgRes("_" + int.Parse(imageContent[1].ToString()).ToString());
                        else
                            image = this.RTBRecord.findPic(imageContent[1]).Image;

                        this.RTBRecord.addGifControl(imageContent[1], image);
                    }
                    else
                    {
                        try
                        {
                            if (isSend)
                            {
                                image = this.rtfSend.findPic(imageContent[1]).Image;
                                this.RTBRecord.addGifControl(imageContent[1], image);

                            }
                            else
                            {
                                image = CSS.IM.UI.Util.ResClass.GetImgRes("wite");
                                this.RTBRecord.addGifControl(imageContent[1] + "_r", image);
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    addPos++;
                }
                this.RTBRecord.AppendText(msgtext.Substring(textPos, msgtext.Length - textPos) + "  \n");
            }
            else
            {
                this.RTBRecord.AppendText(msgtext + "\n");
            }

            this.RTBRecord.Select(iniPos, this.RTBRecord.TextLength - iniPos);
            this.RTBRecord.SelectionFont = ft;
            this.RTBRecord.SelectionColor = cl;
            this.RTBRecord.Select(this.RTBRecord.TextLength, 0);
            this.RTBRecord.ScrollToCaret();

        }

        /// <summary>
        /// 接收到UDP数据功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LB_sockUDP_DataArrival(object sender, Library.Net.SockEventArgs e)
        {
            CSS.IM.Library.Class.Msg msg = new CSS.IM.Library.Class.Msg(e.Data);
            switch (msg.InfoClass)
            {
                case (byte)ProtocolClient.ImageTransmitRequest://图片文件发送请求
                    onUserImageSendRequest(new DataArrivalEventArgs(msg));
                    break;
                case (byte)ProtocolClient.ImageTransmitGetUDPPort://获取对方文件传输UDP本地端口  
                    string[] IdInfo = TextEncoder.bytesToText(msg.Content).Split('|');
                    setImageTransmitGetUdpPort(IdInfo[0], IdInfo[1], IdInfo[2]);
                    break;
                case (byte)ProtocolClient.FileTransmitRequest://文件发送请求
                    this.BeginInvoke(FileSendRequestEvent, new object[] { new DataArrivalEventArgs(msg) });
                    break;
                case (byte)ProtocolClient.FileTransmitGetUDPPort://获取对方文件传输UDP本地端口  
                    //this.onUserFileTransmitGetUDPPort(new DataArrivalEventArgs(msg));
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolClient.FileTransmitCancel://获得服务器返回的文件传输套接字广域网UDP端口
                    {

                        ////Debug.WriteLine("取消文件传输");
                        ////Debug.WriteLine(TextEncoder.bytesToText(msg.Content));
                        ////filelistfrom.tabControl1.TabPages
                        //for (int i = 0; i < filelistfrom.tabControl1.TabPages.Count; i++)
                        //{
                        //    TabPage tabpage = filelistfrom.tabControl1.TabPages[i] as TabPage;
                        //    CSS.IM.App.Controls.p2pFile p2pfile = tabpage.Controls[0] as CSS.IM.App.Controls.p2pFile;
                        //    if (TextEncoder.bytesToText(msg.Content) == p2pfile.Name)
                        //    {
                        //        p2pfile.CancelTransmit(true);
                        //    }
                        //}
                    }
                    break;
                default:
                    break;
            }
        }

        #region Udp接收数据后的响应事件

        /// <summary>
        /// 设置对方文件传输UDP本地端口
        /// </summary>
        /// <param name="FileMD5Value">文件MD5值</param>
        /// <param name="Port">对方文件传输UDP本地端口</param>
        public void setImageTransmitGetUdpPort(string FileMD5Value, string Port, string udpHandshakeInfoClass)
        {
            CSS.IM.Library.Controls.p2pFileTransmitEX p2pf = this.imageP2Ps.find(FileMD5Value);
            if (p2pf == null) return;
            p2pf.setFileTransmitGetUdpLocalPort(RemotBaseUDPIP, Convert.ToInt32(Port), Convert.ToBoolean(udpHandshakeInfoClass));
        }

        /// <summary>
        /// 图片文件发送请求
        /// </summary>
        /// <param name="e"></param>
        private void onUserImageSendRequest(DataArrivalEventArgs e)//接收图片
        {
            string[] fileInfo = TextEncoder.bytesToText(e.msg.Content).Split('|');
            if (fileInfo.Length < 4) return;//抛掉非法数据 
            //FormSendMsg f = FormAccess.newSendMsgForm(e.msg.SendID);
            //if (f == null) return;
            //string fileInfo = FileName + "|" + FileLen.ToString() + "|" + fileExtension + "|" + FileMD5Value;//初次请求发送文件时要先发送“控件参数”到对方，请求对方创建“文件发送控件”并建立连接
            ImageTransfers(false, fileInfo[0], fileInfo[0], Convert.ToInt32(fileInfo[1]), fileInfo[2], fileInfo[3]);
        }


        #endregion

        /// <summary>
        /// 关闭窗体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_send_Click(object sender, EventArgs e)
        {
            if (XmppConn == null)
            {
                MsgBox.Show(this, "CSS&IM", "网络连接异常，需要重新打开聊天会话！", MessageBoxButtons.OK);
                this.Close();
            }

            if (rtfSend.Text.Length > 1000)
            {
                MsgBox.Show(this, "CSS&IM", "发送内容长度不能大于1000!", MessageBoxButtons.OK);
                return;
            }

            if (rtfSend.GetImageInfo().Length == 0)
            {

                if (!(rtfSend.Text.Trim().Length > 0))
                {
                    return;
                }
            }

            try
            {
                #region 显示我自己发送的消息
                CSS.IM.XMPP.protocol.client.Message top_msg = new XMPP.protocol.client.Message();
                top_msg.SetTag("FName", this.Font.Name);//获得字体名称
                top_msg.SetTag("FSize", this.Font.Size);//字体大小
                top_msg.SetTag("FBold", true);//是否粗体
                top_msg.SetTag("FItalic", this.Font.Italic);//是否斜体
                top_msg.SetTag("FStrikeout", this.Font.Strikeout);//是否删除线
                top_msg.SetTag("FUnderline", true);//是否下划线

                Color top_cl = Color.FromArgb(33, 119, 207);//获取颜色
                byte[] top_cby = BitConverter.GetBytes(top_cl.ToArgb());
                top_msg.SetTag("CA", top_cby[0]);
                top_msg.SetTag("CR", top_cby[1]);
                top_msg.SetTag("CG", top_cby[2]);
                top_msg.SetTag("CB", top_cby[3]);

                //(nikeName.Text != "" ? nikeName.Text : XmppConn.Username)
                //(Util.vcard.Nickname != "" ? Util.vcard.Nickname : XmppConn.Username)

                top_msg.Body = (Util.vcard.Nickname != "" ? Util.vcard.Nickname : XmppConn.Username) + "(" + XmppConn.Username + "):" + DateTime.Now.ToString("HH:mm:ss");
                top_msg.From = TO_Jid;
                top_msg.To = TO_Jid;
                RTBRecord_Show(top_msg, true);
                #endregion

                #region 发送的消息
                CSS.IM.XMPP.protocol.client.Message msg = new CSS.IM.XMPP.protocol.client.Message();

                Font ft = rtfSend.Font;//获取字体
                msg.SetTag("FName", ft.Name);//获得字体名称
                msg.SetTag("FSize", ft.Size);//字体大小
                msg.SetTag("FBold", ft.Bold);//是否粗体
                msg.SetTag("FItalic", ft.Italic);//是否斜体
                msg.SetTag("FStrikeout", ft.Strikeout);//是否删除线
                msg.SetTag("FUnderline", ft.Underline);//是否下划线

                Color cl = rtfSend.ForeColor;//获取颜色
                byte[] cby = BitConverter.GetBytes(cl.ToArgb());
                msg.SetTag("CA", cby[0]);
                msg.SetTag("CR", cby[1]);
                msg.SetTag("CG", cby[2]);
                msg.SetTag("CB", cby[3]);
                msg.SetTag("m_type", 0);
                msg.Type = MessageType.chat;
                msg.To = TO_Jid;
                msg.SetTag("face", rtfSend.GetImageInfo().ToString());
                msg.Body = rtfSend.Text;
                msg.From = XmppConn.MyJID;
                RTBRecord_Show(msg, true);

                //保存要发送的图片;
                CSS.IM.Library.gifCollections tempGifs = this.rtfSend.GetExistGifs();
                foreach (CSS.IM.Library.MyPicture pic in tempGifs)
                {
                    PicQueue.add(pic);
                    this.rtfSend.gifs.Romove(pic);
                }

                rtfSend.CloseImage();

                rtfSend.ClearUndo();
                rtfSend.Clear();
                rtfSend.Text = String.Empty;

                try
                {
                    rtfSend.ImeMode = ImeMode.OnHalf;
                }
                catch (Exception)
                {

                }
                XmppConn.Send(msg);

                //发送图片协议
                //CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
                //fmsg.SetTag("m_type", 4);
                //fmsg.Type = MessageType.chat;
                //fmsg.To = TO_Jid;
                //fmsg.SetTag("BPort", ListenBasicUDPPort);
                //fmsg.SetTag("BIP", Program.LocalHostIP.ToString());
                //fmsg.SetTag("isSend", true);
                //fmsg.From = XmppConn.MyJID;
                //XmppConn.Send(fmsg);
                //ftp发送图片
                sendSelfImage();
                #endregion

            }
            catch
            {

            }
            rtfSend.Focus();

            System.GC.Collect();
        }

        #region 发送消息按键

        /// <summary>
        /// 用于生命剪贴板的数据锁
        /// </summary>
        object clipboard_object = new object();
        private void rtfSend_KeyDown(object sender, KeyEventArgs e)
        {
            switch (CSS.IM.UI.Util.Path.SendKeyType)
            {
                case 1:
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (!e.Control)
                        {
                            btn_send_Click(null, null);
                            rtfSend.ClearUndo();
                            rtfSend.Clear();
                        }
                        base.OnKeyDown(e);
                        e.Handled = true;

                    }
                    break;
                case 2:
                    if (e.Control && e.KeyCode == Keys.Enter)
                    {
                        btn_send_Click(null, null);
                        rtfSend.ClearUndo();
                        rtfSend.Clear();
                        base.OnKeyDown(e);
                        e.Handled = true;
                    }
                    break;
                default:
                    break;
            }

            #region 禁用ctrl + v 功能 与 + c
            //禁用 ctrl + v 功能 与 + c 功能 用于自己重新开发新的功能，匹配ref控件
            if (e.Control && (e.KeyCode == Keys.V))
            {
                base.OnKeyDown(e);
                e.Handled = true;

                System.Windows.Forms.IDataObject data = Clipboard.GetDataObject();

                lock (clipboard_object)
                {
                    //如果剪贴板有图片
                    if (data.GetDataPresent(typeof(Bitmap)))
                    {
                        Bitmap map = (Bitmap)data.GetData(typeof(Bitmap));//将图片数据存到位图中
                        //this.pictureBox1.Image = map;//显示
                        //map.Save(@"C:\a.bmp");//保存图片

                        System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(Util.sendImage);
                        if (!dInfo.Exists)
                            dInfo.Create();

                        string fileName = Util.sendImage + "temp.gif";

                        map.Save(fileName);
                        //string md5 = Hasher.GetMD5Hash(fileName);
                        string md5 = Guid.NewGuid().ToString();
                        string Md5fileName = Util.sendImage + md5 + ".gif";

                        if (!System.IO.File.Exists(Md5fileName))
                        {
                            System.IO.File.Delete(fileName);
                            map.Save(Md5fileName);
                        }
                        try
                        {
                            this.rtfSend.addGifControl(md5, map);
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else if (data.GetDataPresent(typeof(string)))
                    {
                        string map = (string)data.GetData(typeof(string));//将图片数据存到位图中
                        this.rtfSend.SelectedText = map;
                    }
                }
            }
            #endregion


        }


        /// <summary>
        /// 设置发送消息快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_send_key_Click(object sender, EventArgs e)
        {

            switch (CSS.IM.UI.Util.Path.SendKeyType)
            {
                case 1:
                    QQtlm_key_enter.Checked = true;
                    QQtlm_key_ctrl_enter.Checked = false;
                    break;
                case 2:
                    QQtlm_key_enter.Checked = false;
                    QQtlm_key_ctrl_enter.Checked = true;
                    break;
                default:
                    break;
            }

            QQcm_send_key.Show(this, btn_send_key.Location);
        }

        /// <summary>
        /// 设置为enter发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QQtlm_key_enter_Click(object sender, EventArgs e)
        {
            try
            {
                if (QQtlm_key_enter.Checked)
                {
                    //QQtlm_key_enter.Checked = false;
                    //QQtlm_key_ctrl_enter.Checked = true;


                    //SetSendKeyType(2);
                }
                else
                {
                    QQtlm_key_enter.Checked = true;
                    QQtlm_key_ctrl_enter.Checked = false;

                    SetSendKeyType(1);
                }
            }
            catch (Exception)
            {
                
            }
            
        }

        /// <summary>
        /// 设置为ctrl+enter发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QQtlm_key_ctrl_enter_Click(object sender, EventArgs e)
        {
            try
            {
                if (QQtlm_key_ctrl_enter.Checked)
                {
                    //QQtlm_key_ctrl_enter.Checked = false;
                    //QQtlm_key_enter.Checked = true;

                    //SetSendKeyType(1);
                }
                else
                {
                    QQtlm_key_ctrl_enter.Checked = true;
                    QQtlm_key_enter.Checked = false;
                    SetSendKeyType(2);
                }
            }
            catch (Exception)
            {
                
            }
            
        }

        /// <summary>
        /// 设置发送消息的快捷键
        /// </summary>
        /// <param name="value"></param>
        public void SetSendKeyType(int value)
        {
            CSS.IM.XMPP.Xml.Dom.Document doc_setting = new CSS.IM.XMPP.Xml.Dom.Document();

            Settings.Settings config = new Settings.Settings();
            doc_setting.LoadFile(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
            Settings.Paths path = doc_setting.RootElement.SelectSingleElement(typeof(Settings.Paths), false) as Settings.Paths;
            path.SendKeyType = value;
            CSS.IM.UI.Util.Path.SendKeyType = value;
            config.Paths = path;
            config.Font = doc_setting.RootElement.SelectSingleElement(typeof(Settings.SFont)) as Settings.SFont;
            config.Color = doc_setting.RootElement.SelectSingleElement(typeof(Settings.SColor)) as Settings.SColor;

            doc_setting.Clear();
            doc_setting.ChildNodes.Add(config);
            doc_setting.Save(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
        }

        #endregion

        /// <summary>
        /// 关闭的时候检测文件传输是否完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Check_Tick(object sender, EventArgs e)
        {

        
        if (fileTansfersContainer.Controls.Count == 0 && (avForm == null || avForm.IsDisposed == true) && (ravForm == null || ravForm.IsDisposed == true))
        {
            System.Threading.Thread.Sleep(500);

            foreach (Control citem in fileTansfersContainer.Controls)
            {
                FileTransfersItem item = citem as FileTransfersItem;

                SendFileManager sendFileManager = item.Tag as SendFileManager;
                if (item.Style == FileTransfersItemStyle.Send)
                {
                    udpSendFile.CancelSend(sendFileManager.MD5);
                    fileTansfersContainer.RemoveItem(item);
                    item.Dispose();
                }

                if (item.Style == FileTransfersItemStyle.Cancel)
                {
                    sendFileManager.ftpClient.DisConnect(true);

                }
            }

            foreach (Control citem in fileTansfersContainer.Controls)
            {
                FileTransfersItem item = citem as FileTransfersItem;
                if (item.Style == FileTransfersItemStyle.ReadyReceive)
                {
                    ControlTag tag = item.Tag as ControlTag;
                    if (tag != null)
                    {
                        udpReceiveFile.CancelReceive(tag.MD5, tag.RemoteIP);
                        fileTansfersContainer.RemoveItem(item);
                        item.Dispose();
                    }
                }
            }

            foreach (Control citem in fileTansfersContainer.Controls)
            {
                FileTransfersItem item = citem as FileTransfersItem;
                if (item.Style == FileTransfersItemStyle.Receive)
                {
                    RequestSendFileEventArgs rse = item.Tag as RequestSendFileEventArgs;
                    if (rse != null)
                    {
                        rse.Cancel = true;
                        fileTansfersContainer.RemoveItem(item);
                        item.Dispose();
                    }
                }
            }

            if (panel_function.Visible)
            {
                panel_function.Visible = false;
                this.Width = this.Width - panel_function.Width;
                panel_msg.Width = panel_msg.Width + panel_function.Width + 2;

            }

            Close_Check.Enabled = false;
            this.Close();
        }

                    
        }

        /// <summary>
        /// 由工具栏发送文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_filesend_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            fd.Filter = "所有文件|*.*";
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < fd.FileNames.Length; i++)
                {
                    System.IO.FileInfo f = new System.IO.FileInfo(fd.FileNames[i]);
                    sendFileDelegate Dg = new sendFileDelegate(sendFile);//异步处理计算文件 MD5值
                    this.Invoke(Dg, f);
                }
            }
        }

        /// <summary>
        /// 视频发送请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_videoOpen_Click(object sender, EventArgs e)
        {
            if (!OnLine)
                return;

            string keyValue=Guid.NewGuid().ToString().Replace("-","");

            try
            {
                //if (avForm != null)
                //    if (!avForm.IsDisposed)
                //        return;

                //if (ravForm != null)
                //    if (!ravForm.IsDisposed)
                //        return;

                //avForm = new CSS.IM.App.Controls.AVForm(TO_Jid);
                //avForm.AVCloseEvent += new CSS.IM.App.Controls.AVForm.AVCloseDelegate(avForm_AVCloseEvent);
                //ListenVideoUDPPort = avForm.aVcommunicationEx1.selfUDPPort;

                //avForm.Show();

                if (red5MsgSend != null)
                    if (!red5MsgSend.IsDisposed)
                        return;

                if (red5MsgReceive != null)
                    if (!red5MsgReceive.IsDisposed)
                        return;


                string serUrl = @"http://" + Program.ServerIP + ":7070/redfire/video/redfire_2way.html?me={0}&you={1}&key={2}";
                red5MsgSend = new Red5Msg();
                red5MsgSend.FriendName = "正在与[" + TO_Jid.User + "]视频通话中";
                red5MsgSend.FormClosing += new FormClosingEventHandler(red5MsgSend_FormClosing);
                red5MsgSend.Cell(string.Format(serUrl, XmppConn.MyJID.User, TO_Jid.User, keyValue));
                red5MsgSend.Show();


                CSS.IM.XMPP.protocol.client.Message msg = new CSS.IM.XMPP.protocol.client.Message();
                msg.Type = MessageType.chat;
                msg.To = TO_Jid;
                msg.SetTag("m_type", 11);
                msg.SetTag("me", XmppConn.MyJID.User);
                msg.SetTag("you", TO_Jid.User);
                msg.SetTag("key", keyValue);
                XmppConn.Send(msg);

                this.AppendSystemRtf("向" + TO_Jid.User+"发送了视频请求，等待回应");
                 
            }
            catch (Exception)
            {

            }

            //CSS.IM.XMPP.protocol.client.Message msg = new CSS.IM.XMPP.protocol.client.Message();
            //msg.Type = MessageType.chat;
            //msg.To = TO_Jid;
            //msg.SetTag("m_type", 1);
            //msg.SetTag("UDPPort", ListenVideoUDPPort);
            //msg.SetTag("UDPIP", Program.LocalHostIP.ToString());
            //XmppConn.Send(msg);

            ////callSoundPlayer.PlayLooping();
            ////SoundPlayEx.MsgPlay("call");
        }

        /// <summary>
        /// 视频请求窗体关闭的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void red5MsgSend_FormClosing(object sender, FormClosingEventArgs e)
        {
            CSS.IM.XMPP.protocol.client.Message msg = new CSS.IM.XMPP.protocol.client.Message();
            msg.Type = MessageType.chat;
            msg.To = TO_Jid;
            msg.SetTag("m_type", 13);
            XmppConn.Send(msg);
        }

        /// <summary>
        /// 接收视频窗体关闭的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void red5MsgReceive_FormClosing(object sender, FormClosingEventArgs e)
        {
            CSS.IM.XMPP.protocol.client.Message msg = new CSS.IM.XMPP.protocol.client.Message();
            msg.Type = MessageType.chat;
            msg.To = TO_Jid;
            msg.SetTag("m_type", 14);
            XmppConn.Send(msg);
        }


        /// <summary>
        /// 在顶头工具栏更新功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_more_Click(object sender, EventArgs e)
        {
            MsgBox.Show(this, "CSS&IM", "更多功敬请期待！", MessageBoxButtons.OK);
        }

        /// <summary>
        /// 显示消息区单击连接超级连接的功能实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RTBRecord_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            //MessageBox.Show(e.LinkText);
            //file:\\C:\Users\Administrator\Desktop\system.wav
            string url = e.LinkText;
            string falg = url.Substring(0, 4);
            if (falg == "file")
            {
                url = url.Substring(7, url.Length - 7);
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "explorer"; //打开资源管理器
                proc.StartInfo.Arguments = @"/select," + url;
                proc.Start();
            }
            else
            {
                System.Diagnostics.Process.Start(url);
            }

        }

        /// <summary>
        /// 更改好友状态 是否在线
        /// </summary>
        /// <param name="value"></param>
        public void UpdateFriendOnline(bool value)
        {
            OnLine = value;

            if (!OnLine)
            {
                if (avForm!=null)
                {
                    if (!avForm.IsDisposed)
                    {
                        avForm.isBtn_hangup = true;
                        avForm.AVClose();
                        avForm = null;
                    }
                }

                if (ravForm != null)
                {
                    if (!ravForm.IsDisposed)
                    {
                        ravForm.isBtn_hangup = true;
                        ravForm.AVClose();
                        ravForm = null;
                    }
                }


                if (fileTansfersContainer.Controls.Count > 0)
                {

                    List<FileTransfersItem> FTIList = new List<FileTransfersItem>();
                    foreach (Control citem in fileTansfersContainer.Controls)
                    {
                        FileTransfersItem item = citem as FileTransfersItem;
                        FTIList.Add(item);
                    }

                    foreach (FileTransfersItem item in FTIList)
                    {
                        if (item.Style == FileTransfersItemStyle.Send)
                        {
                            SendFileManager sendFileManager = item.Tag as SendFileManager;
                            udpSendFile.CancelSend(sendFileManager.MD5);
                            fileTansfersContainer.RemoveItem(item);
                            item.Dispose();
                        }
                        else if (item.Style == FileTransfersItemStyle.ReadyReceive)
                        {
                            RequestSendFileEventArgs rse = item.Tag as RequestSendFileEventArgs;
                            if (rse != null)
                            {
                                rse.Cancel = true;
                                fileTansfersContainer.RemoveItem(item);
                                item.Dispose();
                                udpReceiveFile.AcceptReceive(rse);
                            }
                        }
                        else if (item.Style == FileTransfersItemStyle.Receive)
                        {
                            ControlTag tag = item.Tag as ControlTag;
                            if (tag != null)
                            {
                                udpReceiveFile.CancelReceive(tag.MD5, tag.RemoteIP);
                                fileTansfersContainer.RemoveItem(item);
                                item.Dispose();
                            }
                        }
                        Thread.Sleep(500);
                    }


                    if (panel_function.Visible)
                    {
                        panel_function.Visible = false;
                        this.Width = this.Width - panel_function.Width;
                        panel_msg.Width = panel_msg.Width + panel_function.Width + 2;

                    }

                }

                
            }

            VcardIq viq = new VcardIq(IqType.get, TO_Jid);
            XmppConn.IqGrabber.SendIq(viq, new IqCB(VcardResult), null, true);
        }

        /// <summary>
        /// 获取对方的名片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="iq"></param>
        /// <param name="data"></param>
        private void VcardResult(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new IqCB(VcardResult), new object[] { sender, iq, data });
                return;
            }

            try
            {
                if (iq.Type == IqType.result)
                {
                    Vcard vcard = iq.Vcard;
                    if (vcard != null)
                    {
                        if (vcard.Nickname.Trim().Length>0)
                        {
                            this._NickName=vcard.Nickname;
                        }
                        else
                        {
                            this._NickName=TO_Jid.User;;
                        }
                        nikeName.Text = this._NickName;

                        this.Text = "正在与[" + (this._NickName != "" ? this._NickName : _NickName) + "]对话";
                        
                        //txt_name.Text = vcard.Fullname;
                        //txt_nickname.Text = vcard.Nickname;
                        //txt_birthday.Text = vcard.Birthday.ToString();
                        //txt_desc.Text = vcard.Description;
                        Photo photo = vcard.Photo;
                        if (photo != null)
                            friendHead.Image = vcard.Photo.Image;
                        else
                            friendHead.Image = CSS.IM.UI.Util.ResClass.GetHead("big194");

                        if (!OnLine)
                        {
                            friendHead.Image = Util.MarkTopHead(friendHead.Image);
                        }

                        if (photo!=null)
                            if (photo.Image != null)
                            {
                                photo.Image.Dispose();
                            }
                        vcard = null;
                    }
                }
            }
            catch (Exception)
            {
                
            }
            

            System.GC.Collect();
        }

        /// <summary>
        /// 监听键盘事件输入，用于更新当前的窗体
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            System.Diagnostics.Debug.WriteLine(keyData.ToString());

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 窗体被激活的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatFromMsg_Activated(object sender, EventArgs e)
        {
            Util.TO_Jid = TO_Jid;
        }
    }
}
