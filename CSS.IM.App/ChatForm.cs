using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CSS.IM.UI;
using CSS.IM.App.Settings;
using CSS.IM.XMPP;
using CSS.IM.XMPP.Collections;
using CSS.IM.XMPP.protocol.client;
using CSS.IM.UI.Util;
using CSS.IM.UI.Control;
using CSS.IM.XMPP.protocol.iq.vcard;
using System.Drawing.Imaging;
using System.IO;
using CSS.IM.UI.Form;
using CSS.IM.Library;
using CSS.IM.Library.Class;
using CSS.IM.Library.Controls;
using System.Net;
using CSS.IM.Library.AV;
using CSS.IM.App.Properties;
using CSS.IM.App.Controls;
using CSS.IM.Library.ExtRichTextBox;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CSS.IM.Library.Net;
using CSS.IM.XMPP.Xml.Dom;
using System.Threading;

namespace CSS.IM.App
{
    public partial class ChatForm : IChatForm
    {
        List<CSS.IM.XMPP.protocol.client.Message> main_msg = new List<XMPP.protocol.client.Message>();//用于保存从好友列表打开后传过来的消息队列
        private bool IsPlayMsg = true;//是否播放消息声音

        public bool OnLine { get;set;}//联系人状态

        FileListForm filelistfrom = new FileListForm();

        CSS.IM.Library.gifCollections PicQueue = new gifCollections();//用于保存图片的发送队列

        UserInfo Opposite = new UserInfo();

        public int BaseLocalUDPPort { get; set; }//本窗口监听的UDP端口
        public string LocalVideoUDPPort { get; set; }//保存本地视频监听的监听端口 

        public int VideoRemotUDPPort { get; set; }//保存远程视频的监听端口
        public System.Net.IPAddress VideoRemotUDPIP { get; set; }//保存远程的视频IP

        public System.Net.IPAddress BaseRemotUDPIP { get; set; }//保存远程的IP地址
        public int BaseRemotUDPPort { get; set; }//保存远程端口


        public Jid to_Jid { get; set; }//保存远程会话的jid
        string packetId;

        private EmotionDropdown _emotion;//表情管理
        private static XmppClientConnection _connection;

        private string _nickname;
        private Bitmap Bmp = null;
        private Graphics g = null;
        private string DownBtn = "";

        private AVForm avForm = null;//发送的视频对话

        private AVForm ravForm = null;//接收的视频对话


        /// <summary>
        /// 接收到对方的视频请求，然后进行本地的初使化
        /// </summary>
        /// <param name="msg"></param>
        private delegate void AcceptInitDelegate(CSS.IM.XMPP.protocol.client.Message msg);
        AcceptInitDelegate AcceptInit = null;
        /// <summary>
        /// 对方意视频后并初使化完在，我自己进行视频初使化，并监听
        /// </summary>
        /// <param name="msg"></param>
        private delegate void ReturnAcceptInitDelegate(CSS.IM.XMPP.protocol.client.Message msg);
        ReturnAcceptInitDelegate ReturnAcceptInit = null;

        /// <summary>
        /// 同意视频请求后，打开视频功能
        /// </summary>
        /// <param name="msg"></param>
        private delegate void AcceptOpenDelegate(CSS.IM.XMPP.protocol.client.Message msg);
        AcceptOpenDelegate AcceptOpen = null;


        /// <summary>
        /// 收到发送文件请求后进行功能提示
        /// </summary>
        /// <param name="e"></param>
        private delegate void FileSendRequestDelegate(DataArrivalEventArgs e);
        FileSendRequestDelegate FileSendRequestEvent;

        /// <summary>
        /// 发送文件准备前的工作
        /// </summary>
        /// <param name="filepath"></param>
        private delegate void FileSendInitDelegate(String filepath);
        FileSendInitDelegate FileSendInitEvent;



        #region 初使化
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {

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
                sockUDP1.CloseSock();
                sockUDP1.Dispose();
                sockUDP1 = null;
            }
            catch (Exception)
            {

            }
            
            try
            {
                _emotion.Dispose();
                _emotion = null;
            }
            catch (Exception)
            {
                
            }

            try
            {
                Util.ChatForms.Remove(to_Jid.Bare.ToLower());
                _connection.MessageGrabber.Remove(to_Jid);
            }
            catch (Exception)
            {

            }

            g.Dispose();
            g = null;
            Bmp.Dispose();
            Bmp = null;

            _connection = null;

            System.GC.Collect();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public ChatForm(Jid jid, XmppClientConnection con, string nickname)
        {
            InitializeComponent();

            _emotion = new EmotionDropdown();
            AcceptInit = new AcceptInitDelegate(AcceptInitMethod);
            ReturnAcceptInit = new ReturnAcceptInitDelegate(ReturnAcceptMethod);
            AcceptOpen = new AcceptOpenDelegate(AcceptOpenMethod);
            FileSendRequestEvent = new FileSendRequestDelegate(onUserFileSendRequest);
            FileSendInitEvent = new FileSendInitDelegate(FileSendInit);

            to_Jid = jid;
            _connection = con;
            _nickname = jid.User;
            this.nikeName.Text = nickname;

            VcardIq viq = new VcardIq(IqType.get, new Jid(jid.Bare));
            packetId = viq.Id;
            _connection.IqGrabber.SendIq(viq, new IqCB(VcardResult), null, true);

            Util.ChatForms.Add(to_Jid.Bare.ToLower(), this);
            nikeName.Text = _nickname;

            con.MessageGrabber.Add(jid, new BareJidComparer(), new MessageCB(MessageCallback), null);
            _emotion.EmotionContainer.ItemClick += new UI.Face.EmotionItemMouseEventHandler(EmotionContainer_ItemClick);

            friendHead.BackgroundImage = ResClass.GetImgRes("Padding4Normal");
            friendHead.Image = ResClass.GetHead("big194");


            rtfSend.AllowDrop = true;

            rtfSend.DragDrop += new DragEventHandler(rtfSend_DragDrop);
            rtfSend.DragEnter += new DragEventHandler(rtfSend_DragEnter);

        }

        /// <summary>
        /// 拖曳文件到rtfSend边源的时候 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rtfSend_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect=DragDropEffects.Copy;
        }

        /// <summary>
        /// rtfSend拖曳文件结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rtfSend_DragDrop(object sender, DragEventArgs e)
        {
            if (!OnLine)
                return;

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

        void EmotionContainer_ItemClick(object sender, UI.Face.EmotionItemMouseClickEventArgs e)
        {
            rtfSend.addGifControl(e.Item.Tag.ToString(), e.Item.Image);
        }

        #endregion

        #region GDI

        private void friendHead_MouseEnter(object sender, EventArgs e)
        {
            friendHead.BackgroundImage = ResClass.GetImgRes("Padding4Select");

        }

        private void userImg_MouseLeave(object sender, EventArgs e)
        {
            friendHead.BackgroundImage = ResClass.GetImgRes("Padding4Normal");
        }

        #region 中间的小按键

        private void ToolBtn_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            g = p.CreateGraphics();
            Bmp = ResClass.GetImgRes("all_iconbutton_pushedBackground");
            g.DrawImage(Bmp, new Rectangle(0, 0, 20, 20), 0, 0, 20, 20, GraphicsUnit.Pixel);
            g.DrawImage(p.Image, new Rectangle(0, 0, 20, 20), 0, 0, 20, 20, GraphicsUnit.Pixel);
            g.Dispose();
        }

        private void ToolBtn_MouseEnter(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (!DownBtn.Equals(p.Name))
            {
                g = p.CreateGraphics();
                Bmp = ResClass.GetImgRes("All_iconbutton_highlightBackground");
                g.DrawImage(Bmp, new Rectangle(0, 0, 20, 20), 0, 0, 20, 20, GraphicsUnit.Pixel);
                g.DrawImage(p.Image, new Rectangle(0, 0, 20, 20), 0, 0, 20, 20, GraphicsUnit.Pixel);
                g.Dispose();
            }
        }

        private void ToolBtn_MouseLeave(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (!DownBtn.Equals(p.Name))
            {
                p.Invalidate();
            }
        }
        #endregion

        #region 上面的大按键

        private void TB_ToolBtn_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            g = p.CreateGraphics();
            Bmp = ResClass.GetImgRes("all_iconbutton_pushedBackground");
            g.DrawImage(Bmp, new Rectangle(0, 0, 35, 35), 0, 0, 20, 20, GraphicsUnit.Pixel);
            g.DrawImage(p.Image, new Rectangle(0, 0, 35, 35), 0, 0, 35, 35, GraphicsUnit.Pixel);
            g.Dispose();
        }

        private void TB_ToolBtn_MouseEnter(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (!DownBtn.Equals(p.Name))
            {
                g = p.CreateGraphics();
                Bmp = ResClass.GetImgRes("ChatFrame_splitter_button_highlightBackground");
                g.DrawImage(Bmp, new Rectangle(0, 0, 35, 35), 0, 0, 20, 20, GraphicsUnit.Pixel);
                g.DrawImage(p.Image, new Rectangle(0, 0, 35, 35), 0, 0, 35, 35, GraphicsUnit.Pixel);
                g.Dispose();
            }
        }

        private void TB_ToolBtn_MouseLeave(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (!DownBtn.Equals(p.Name))
            {
                p.Invalidate();
            }
        }
        #endregion

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void screenBtn_Click(object sender, MouseEventArgs e)
        {
            FormCapture cf = new FormCapture();
            cf.ShowDialog();
            if (cf.Image != null)
            {
                System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(Util.sendImage);
                if (!dInfo.Exists)
                    dInfo.Create();

                string fileName = Util.sendImage + "temp.gif";

                cf.Image.Save(fileName);
                string md5 = Hasher.GetMD5Hash(fileName);
                string Md5fileName = Util.sendImage + md5 + ".gif";

                if (!System.IO.File.Exists(Md5fileName))
                {
                    System.IO.File.Delete(fileName);
                    cf.Image.Save(Md5fileName);
                }
                try
                {
                    this.rtfSend.addGifControl(md5, cf.Image);
                }
                catch (Exception)
                {

                }

            }

            cf.Dispose();
        }

        private void faceBtn_MouseClick(object sender, MouseEventArgs e)
        {
            _emotion.Show(fontBtn);
        }

        private void fontBtn_MouseClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.FontDialog fd = new FontDialog();

            fd.Font = this.rtfSend.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.rtfSend.Font = fd.Font;
        }

        private void butFontColor_Click(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.ColorDialog cd = new ColorDialog();
            cd.Color = this.rtfSend.ForeColor;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.rtfSend.ForeColor = cd.Color;
        }

        private void toolBarBg_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Bmp = ResClass.GetImgRes("ChatFrame_QuickbarFrame_background");
            g.DrawImage(Bmp, new Rectangle(0, 0, 2, 22), 0, 0, 2, 22, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(2, 0, this.Width, 22), 2, 0, Bmp.Width - 4, 22, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 2, 0, 2, 22), Bmp.Width - 2, 0, 2, 22, GraphicsUnit.Pixel);

            //g.DrawImage(Bmp, new Rectangle(0, 5, 5, this.EditMsgBg.Height - 15), 0, 5, 5, Bmp.Height - 20, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(5, 5, this.EditMsgBg.Width - 10, this.EditMsgBg.Height - 15), 5, 5, 5, Bmp.Height - 20, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(this.EditMsgBg.Width - 5, 5, 5, this.EditMsgBg.Height - 15), Bmp.Width - 5, 5, 5, Bmp.Height - 20, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(0, this.EditMsgBg.Height - 10, 5, 10), 0, Bmp.Height - 10, 5, 10, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(5, this.EditMsgBg.Height - 10, this.EditMsgBg.Width - 10, 10), 5, Bmp.Height - 10, Bmp.Width - 10, 10, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(this.EditMsgBg.Width - 5, this.EditMsgBg.Height - 10, 5, 10), Bmp.Width - 5, Bmp.Height - 10, 5, 10, GraphicsUnit.Pixel);
        }
        #endregion

        #region 接收消息
        /// <summary>
        /// 我发送视频后，对方同意后进行初使化视频
        /// </summary>
        /// <param name="msg"></param>
        void AcceptOpenMethod(XMPP.protocol.client.Message msg)
        {
            ravForm.iniAV(VideoSizeModel.W320_H240);

        }


        /// <summary>
        /// 对方同意视频，并初使化视频后，本地也进行视频初使化
        /// </summary>
        /// <param name="msg"></param>
        void ReturnAcceptMethod(XMPP.protocol.client.Message msg)
        {
            UserInfo user1;
            UserInfo user2;
            CSS.IM.XMPP.protocol.client.Message res_msg;
            VideoRemotUDPPort = msg.GetTagInt("UDPPort");
            VideoRemotUDPIP = IPAddress.Parse(msg.GetTag("UDPIP"));

            user1 = new UserInfo();
            user1.LocalIP = VideoRemotUDPIP;
            user1.LocalPort = VideoRemotUDPPort;

            avForm.SetRemoteAddress(user1, VideoRemotUDPPort);

            user2 = new UserInfo();
            user2.LocalIP = Program.LocalHostIP;
            user2.LocalPort = int.Parse(LocalVideoUDPPort);

            avForm.SetLocalAddress(user2);

            avForm.iniAV(VideoSizeModel.W320_H240);

            res_msg = new CSS.IM.XMPP.protocol.client.Message();
            res_msg.Type = MessageType.chat;
            res_msg.To = to_Jid;
            res_msg.SetTag("m_type", 3);
            res_msg.Body = "falg";
            _connection.Send(res_msg);//告诉对我准备好了

        }

        /// <summary>
        /// 同意视频信息后，进行初使化本地视频服务
        /// </summary>
        /// <param name="msg"></param>
        void AcceptInitMethod(CSS.IM.XMPP.protocol.client.Message msg)
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
                ravForm = new AVForm(to_Jid);
                ravForm.AVCloseEvent += new AVForm.AVCloseDelegate(avForm_AVCloseEvent);
            }
            LocalVideoUDPPort = ravForm.aVcommunicationEx1.selfUDPPort.ToString();

            VideoRemotUDPPort = msg.GetTagInt("UDPPort");
            VideoRemotUDPIP = IPAddress.Parse(msg.GetTag("UDPIP"));
            user1 = new UserInfo();
            user1.LocalIP = VideoRemotUDPIP;
            user1.LocalPort = VideoRemotUDPPort;
            ravForm.SetRemoteAddress(user1, VideoRemotUDPPort);


            user2 = new UserInfo();
            user2.LocalIP = Program.LocalHostIP;
            user2.LocalPort = int.Parse(LocalVideoUDPPort);
            ravForm.SetLocalAddress(user2);
            ravForm.AgreeEvent += new AVForm.AgreeDelegate(ravForm_AgreeEvent);//接收视频会话事件
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

        }

        /// <summary>
        /// 接收视频会话事件
        /// </summary>
        void ravForm_AgreeEvent()
        {
            CSS.IM.XMPP.protocol.client.Message res_msg = new CSS.IM.XMPP.protocol.client.Message();
            res_msg.Type = MessageType.chat;
            res_msg.To = to_Jid;
            res_msg.SetTag("m_type", 2);
            res_msg.SetTag("UDPPort", LocalVideoUDPPort);
            res_msg.SetTag("UDPIP", Program.LocalHostIP.ToString());
            res_msg.Body = "falg";
            if (_connection != null)
            {
                _connection.Send(res_msg);
            }
        }


        private void MessageCallback(object sender, CSS.IM.XMPP.protocol.client.Message msg, object data)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new MessageCB(MessageCallback), new object[] { sender, msg, data });
                    return;
                }

                if (msg.Body != null)
                {
                    IncomingMessage(msg);
                }
            }
            catch (Exception)
            {

            }

        }

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
                if (vcard != null)
                {

                    //txt_name.Text = vcard.Fullname;
                    //txt_nickname.Text = vcard.Nickname;
                    //txt_birthday.Text = vcard.Birthday.ToString();
                    //txt_desc.Text = vcard.Description;
                    Photo photo = vcard.Photo;
                    if (photo != null)
                        friendHead.Image = vcard.Photo.Image;
                    else
                        friendHead.Image = ResClass.GetHead("big194");

                    if (!OnLine)
                    {
                        friendHead.Image = Util.MarkTopHead(friendHead.Image);
                    }
                }
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
                        Win32.FlashWindow(this.Handle, true);//闪烁 
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
                        top_msg.Body = msg.From.User + ":" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        top_msg.From = to_Jid;
                        top_msg.To = to_Jid;
                        RTBRecord_Show(top_msg, true);
                        #endregion
                        RTBRecord_Show(msg, false);
                        if (IsPlayMsg)
                        {
                            if (CSS.IM.UI.Util.Path.MsgSwitch)
                                SoundPlayEx.MsgPlay(CSS.IM.UI.Util.Path.MsgPath);

                        }
                        break;
                    case 1://收到对方的请求要过行视频功能服务，初始化本地的视频
                        this.Invoke(AcceptInit, new object[] { msg });
                        break;
                    case 2://我发送视频请求后，对方告诉我视频初使化完成，进行自己本地的视频初使化
                        this.Invoke(ReturnAcceptInit, new object[] { msg });
                        break;
                    case 3://对方给我发送视频请求，我初使化本地视频服务，告诉对方，对方也初使化视频服务了，我打开视频功能
                        this.Invoke(AcceptOpen, new object[] { msg });
                        break;
                    case 4:
                        BaseRemotUDPPort = msg.GetTagInt("BPort");
                        BaseRemotUDPIP = IPAddress.Parse(msg.GetTag("BIP"));
                        bool isSend = msg.GetTagBool("isSend");
                        if (isSend)
                        {
                            CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
                            fmsg.SetTag("m_type", 4);
                            fmsg.Type = MessageType.chat;
                            fmsg.To = to_Jid;
                            fmsg.SetTag("BPort", BaseLocalUDPPort);
                            fmsg.SetTag("BIP", Program.LocalHostIP.ToString());
                            fmsg.SetTag("isSend", false);
                            fmsg.Body = "falg";
                            _connection.Send(fmsg);
                        }
                        sendSelfImage();
                        break;
                    case 5://视频释放
                        if (VideoRemotUDPPort != -1)
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
                        BaseRemotUDPPort = msg.GetTagInt("BPort");
                        BaseRemotUDPIP = IPAddress.Parse(msg.GetTag("BIP"));

                        CSS.IM.XMPP.protocol.client.Message lmsg = new CSS.IM.XMPP.protocol.client.Message();
                        lmsg.SetTag("m_type", 7);//告诉对方要发送文件啦
                        lmsg.Type = MessageType.chat;
                        lmsg.To = to_Jid;
                        lmsg.SetTag("BPort", BaseLocalUDPPort);
                        lmsg.SetTag("BIP", Program.LocalHostIP.ToString());
                        lmsg.SetTag("isSend", false);
                        lmsg.SetTag("File", msg.GetTag("File").ToString());
                        lmsg.Body = "falg";
                        _connection.Send(lmsg);

                        break;
                    case 7:
                        BaseRemotUDPPort = msg.GetTagInt("BPort");
                        BaseRemotUDPIP = IPAddress.Parse(msg.GetTag("BIP"));
                        this.Invoke(FileSendInitEvent, msg.GetTag("File").ToString());
                        break;
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion


        /// <summary>
        /// 发送消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_send_Click(object sender, EventArgs e)
        {
            if (_connection == null)
            {
                MsgBox.Show(this, "CSS&IM", "网络连接异常，需要重新打开聊天会话！", MessageBoxButtons.OK);
                this.Close();
            }

            if (rtfSend.Text.Length>1000)
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
                //RTBRecord.AppendTextAsRtf(_connection.Username + ":" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "\n", new Font(this.Font, FontStyle.Underline | FontStyle.Bold), CSS.IM.Library.ExtRichTextBox.RtfColor.Lime, CSS.IM.Library.ExtRichTextBox.RtfColor.White);

                #region 显示我自己发送的消息
                CSS.IM.XMPP.protocol.client.Message top_msg = new XMPP.protocol.client.Message();
                top_msg.SetTag("FName", this.Font.Name);//获得字体名称
                top_msg.SetTag("FSize", this.Font.Size);//字体大小
                top_msg.SetTag("FBold", true);//是否粗体
                top_msg.SetTag("FItalic", this.Font.Italic);//是否斜体
                top_msg.SetTag("FStrikeout", this.Font.Strikeout);//是否删除线
                top_msg.SetTag("FUnderline", true);//是否下划线

                Color top_cl = Color.Lime;//获取颜色
                byte[] top_cby = BitConverter.GetBytes(top_cl.ToArgb());
                top_msg.SetTag("CA", top_cby[0]);
                top_msg.SetTag("CR", top_cby[1]);
                top_msg.SetTag("CG", top_cby[2]);
                top_msg.SetTag("CB", top_cby[3]);
                top_msg.Body = _connection.Username + ":" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                top_msg.From = to_Jid;
                top_msg.To = to_Jid;
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
                msg.To = to_Jid;
                msg.SetTag("face", rtfSend.GetImageInfo().ToString());
                msg.Body = rtfSend.Text;
                msg.From = _connection.MyJID;
                RTBRecord_Show(msg, true);
                //保存要发送的图片;
                CSS.IM.Library.gifCollections tempGifs = this.rtfSend.GetExistGifs();
                foreach (MyPicture pic in tempGifs)
                {
                    //pic.Tag = Guid.NewGuid().ToString();
                    PicQueue.add(pic);
                    this.rtfSend.gifs.Romove(pic);
                }
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

                _connection.Send(msg);

                CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
                fmsg.SetTag("m_type", 4);
                fmsg.Type = MessageType.chat;
                fmsg.To = to_Jid;
                fmsg.SetTag("BPort", BaseLocalUDPPort);
                fmsg.SetTag("BIP", Program.LocalHostIP.ToString());
                fmsg.SetTag("isSend", true);
                fmsg.Body = "falg";
                fmsg.From = _connection.MyJID;
                _connection.Send(fmsg);
                #endregion
            }
            catch
            {

            }
            rtfSend.Focus();
        }

        #region 接收与发送截图

        private void sendSelfImage()//发送图片文件
        {
            CSS.IM.Library.gifCollections tempGifs = PicQueue.Clone() as CSS.IM.Library.gifCollections;

            try
            {
                foreach (CSS.IM.Library.MyPicture pic in tempGifs)
                    if (pic.IsSent)
                    {
                        System.IO.FileInfo f = new System.IO.FileInfo(Util.sendImage + pic.ImageMD5 + ".gif");
                        this.ImageTransfers(true, f.FullName, pic.ImageMD5, (int)f.Length, f.Extension, pic.ImageMD5);
                        PicQueue.Romove(pic);//将richTextBox中的自定义图片清除掉，以便下次继续发送消息时出现再次发送的情况
                        System.Threading.Thread.Sleep(500);
                    }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("发送图片时错误：" + ex.Message);
            }
        }


        /// <summary>
        /// 图片传输完成代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private delegate void imageTransmittedDelegate(object sender, fileTransmitEvnetArgs e);
        private void imageTransmitted(object sender, fileTransmitEvnetArgs e)
        {
            //Calculate.WirteLog(e.isSend.ToString() + "图片完成传输(" + e.currTransmittedLen.ToString() + "/" + e.fileLen.ToString() + ")");

            if (!e.isSend)//如果是图片接收者，则将传输完成的图片显示出来
            {
                MyPicture pic = this.RTBRecord.findPic(e.FileMD5Value);
                if (pic != null)
                {
                    pic.Image = System.Drawing.Image.FromFile(e.fullFileName);//显示图片
                    pic.Invalidate();
                    this.RTBRecord.Invalidate();
                }
            }

            p2pFileTransmitEX p2pImage = sender as p2pFileTransmitEX;
            this.imageP2Ps.Romove(p2pImage);//在传输队列中删除文件传输组件
            if (p2pImage != null)
                p2pImage.Dispose();
        }

        /// <summary>
        /// 图片传输完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageTransmit_fileTransmitted(object sender, fileTransmitEvnetArgs e)
        {
            imageTransmittedDelegate d = new imageTransmittedDelegate(imageTransmitted);
            this.BeginInvoke(d, sender, e);
        }

        void imageTransmit_fileTransmitGetUDPPort(object sender, int Port, bool udpHandshakeInfoClass)
        {
            Msg msg = new Msg((byte)ProtocolClient.ImageTransmitGetUDPPort);
            msg.Content = TextEncoder.textToBytes((sender as p2pFileTransmitEX).FileMD5Value + "|" + Port.ToString() + "|" + udpHandshakeInfoClass.ToString());//将获得的本地UDP端口号传输给对方
            sockUDP1.Send(BaseRemotUDPIP, BaseRemotUDPPort, msg.getBytes());
        }

        /// <summary>
        /// 图片传输组件集合
        /// </summary>
        private p2pFileTransmitCollectionsEX imageP2Ps = new p2pFileTransmitCollectionsEX();

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
                p2pFileTransmitEX imageTransmit = new p2pFileTransmitEX();
                this.imageP2Ps.add(imageTransmit);//添加文件传输组件到传输队列

                imageTransmit.fileTransmitGetUDPPort += new p2pFileTransmitEX.GetUDPPortEventHandler(imageTransmit_fileTransmitGetUDPPort);
                imageTransmit.fileTransmitted += new p2pFileTransmitEX.fileTransmittedEventHandler(imageTransmit_fileTransmitted);//图片传输结束
                //imageTransmit.fileTransmitConnected += new p2pFileTransmit.ConnectedEventHandler(imageTransmit_fileTransmitConnected);
                //imageTransmit.fileTransmitCancel += new p2pFileTransmit.fileTransmitCancelEventHandler(imageTransmit_fileTransmitCancel);//图片传输取消或异常中断
                //imageTransmit.getFileProxyID += new p2pFileTransmit.getFileProxyIDEventHandler(imageTransmit_getFileProxyID);
                //imageTransmit.fileTransmitting += new p2pFileTransmit.fileTransmittingEventHandler(imageTransmit_fileTransmitting);
                if (IsSend)
                {
                    string fileInfo = FileName + "|" + FileLen.ToString() + "|" + fileExtension + "|" + FileMD5Value;//初次请求发送文件时要先发送“控件参数”到对方，请求对方创建“文件发送控件”并建立连接
                    Msg msg = new Msg((byte)ProtocolClient.ImageTransmitRequest, TextEncoder.textToBytes(fileInfo));
                    this.sockUDP1.Send(BaseRemotUDPIP, BaseRemotUDPPort, msg.getBytes());
                    imageTransmit.ServerIp = BaseRemotUDPIP;
                }
                else
                {
                    System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(Util.receiveImage);
                    if (!dInfo.Exists)
                        dInfo.Create();
                    imageTransmit.ServerIp = BaseRemotUDPIP;
                    imageTransmit.startIncept(Util.receiveImage + FileMD5Value + fileExtension);//自动接收图片
                }
                imageTransmit.SetParameter(IsSend, fullFileName, FileName, FileLen, fileExtension, FileMD5Value);//设置文件传输组件相关参数
            }
        }


        private void onUserImageSendRequest(DataArrivalEventArgs e)//接收图片
        {
            string[] fileInfo = TextEncoder.bytesToText(e.msg.Content).Split('|');
            if (fileInfo.Length < 4) return;//抛掉非法数据 
            //FormSendMsg f = FormAccess.newSendMsgForm(e.msg.SendID);
            //if (f == null) return;
            //string fileInfo = FileName + "|" + FileLen.ToString() + "|" + fileExtension + "|" + FileMD5Value;//初次请求发送文件时要先发送“控件参数”到对方，请求对方创建“文件发送控件”并建立连接
            ImageTransfers(false, fileInfo[0], fileInfo[0], Convert.ToInt32(fileInfo[1]), fileInfo[2], fileInfo[3]);
        }

        /// <summary>
        /// 设置对方文件传输UDP本地端口
        /// </summary>
        /// <param name="FileMD5Value">文件MD5值</param>
        /// <param name="Port">对方文件传输UDP本地端口</param>
        public void setImageTransmitGetUdpPort(string FileMD5Value, string Port, string udpHandshakeInfoClass)
        {
            p2pFileTransmitEX p2pf = this.imageP2Ps.find(FileMD5Value);
            if (p2pf == null) return;
            p2pf.setFileTransmitGetUdpLocalPort(BaseRemotUDPIP, Convert.ToInt32(Port), Convert.ToBoolean(udpHandshakeInfoClass));
        }

        #endregion

        #region 用户发送文件请求

        /// <summary>
        /// 用户发送文件请求
        /// </summary>
        /// <param name="e"></param>
        private void onUserFileSendRequest(DataArrivalEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(FileSendRequestEvent, e);
            }
            string[] fileInfo = TextEncoder.bytesToText(e.msg.Content).Split('|');
            if (fileInfo.Length < 4) return;//抛掉非法数据 
            //string fileInfo = FileName + "|" + FileLen.ToString() + "|" + fileExtension + "|" + FileMD5Value;//初次请求发送文件时要先发送“控件参数”到对方，请求对方创建“文件发送控件”并建立连接

            FileTransfers(false, fileInfo[0], fileInfo[0], Convert.ToInt32(fileInfo[1]), fileInfo[2], fileInfo[3]);
        }
        #endregion

        #region 获取对方文件传输UDP端口
        /// <summary>
        /// 获取对方文件传输UDP端口
        /// </summary>
        /// <param name="e"></param>
        private void onUserFileTransmitGetUDPPort(DataArrivalEventArgs e)
        {
            string[] IdInfo = TextEncoder.bytesToText(e.msg.Content).Split('|');
            if (IdInfo.Length < 2) return;

            setFileTransmitGetUdpPort(IdInfo[0], IdInfo[1], IdInfo[2]);
        }

        /// <summary>
        /// 设置对方文件传输UDP本地端口
        /// </summary>
        /// <param name="FileMD5Value">文件MD5值</param>
        /// <param name="Port">对方文件传输UDP端口</param>
        public void setFileTransmitGetUdpPort(string FileMD5Value, string Port, string udpHandshakeInfoClass)
        {
            try
            {
                foreach (TabPage tabItem in filelistfrom.tabControl1.TabPages)
                {
                    p2pFile p2pf = tabItem.Tag as p2pFile;
                    if (p2pf.FileMD5Value == FileMD5Value)
                    {
                        p2pf.p2pFileTransmit1.setFileTransmitGetUdpLocalPort(Convert.ToInt32(Port), Convert.ToBoolean(udpHandshakeInfoClass));//对方取消文件传输
                    }
                }
            }
            catch { }
        }

        #endregion

        private void sockUDP1_DataArrival(object sender, Library.Net.SockEventArgs e)
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
                    this.onUserFileTransmitGetUDPPort(new DataArrivalEventArgs(msg));
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolClient.FileTransmitCancel://获得服务器返回的文件传输套接字广域网UDP端口
                    {
                        
                        //Debug.WriteLine("取消文件传输");
                        //Debug.WriteLine(TextEncoder.bytesToText(msg.Content));
                        //filelistfrom.tabControl1.TabPages
                        for (int i = 0; i < filelistfrom.tabControl1.TabPages.Count; i++)
                        {
                            TabPage tabpage = filelistfrom.tabControl1.TabPages[i] as TabPage;
                            p2pFile p2pfile=tabpage.Controls[0] as p2pFile;
                            if (TextEncoder.bytesToText(msg.Content)==p2pfile.Name)
                            {
                                p2pfile.CancelTransmit(true);  
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        #region 窗体事件
        private void ChatForm_Load(object sender, EventArgs e)
        {
            panel_function.Visible = false;
            this.Width = this.Width - panel_function.Width;

            panel_msg.Width = panel_msg.Width + panel_function.Width;

            this.DoubleBuffered = true;
            sockUDP1.Listen(0);
            BaseLocalUDPPort = sockUDP1.ListenPort;

            this.Text = "正在与[" + _nickname + "]对话";


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
            }
            catch (Exception)
            {

            }


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
            rtfSend.Focus();
            rtfSend.Select(this.rtfSend.TextLength, 0);

        }

        /// <summary>
        /// 发送视频通话事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_videoOpen_MouseClick(object sender, MouseEventArgs e)
        {
            if (!OnLine)
                return;

            try
            {
                if (avForm != null)
                    if (!avForm.IsDisposed)
                        return;
                
                if (ravForm != null)
                    if (!ravForm.IsDisposed)
                        return;
                   
                   

                avForm = new AVForm(to_Jid);
                avForm.AVCloseEvent += new AVForm.AVCloseDelegate(avForm_AVCloseEvent);
                LocalVideoUDPPort = avForm.aVcommunicationEx1.selfUDPPort.ToString();
            
                avForm.Show();
            }
            catch (Exception)
            {

            }



            CSS.IM.XMPP.protocol.client.Message msg = new CSS.IM.XMPP.protocol.client.Message();
            msg.Type = MessageType.chat;
            msg.To = to_Jid;
            msg.SetTag("m_type", 1);
            msg.SetTag("UDPPort", LocalVideoUDPPort);
            msg.SetTag("UDPIP", Program.LocalHostIP.ToString());
            msg.Body = "falg";
            _connection.Send(msg);
            //callSoundPlayer.PlayLooping();
            //SoundPlayEx.MsgPlay("call");
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
            msg.To = to_Jid;
            msg.SetTag("m_type", 5);
            msg.Body = "falg";
            _connection.Send(msg);
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
                "'" + _connection.MyJID.Bare.ToString() + "'",
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
                    if (_emotion.faces.ContainsKey(imageContent[1]))
                    {
                        if (this.RTBRecord.findPic(imageContent[1]) == null)
                            image = ResClass.GetImgRes("_" + int.Parse(imageContent[1].ToString()).ToString());
                        else
                            image = this.RTBRecord.findPic(imageContent[1]).Image;
                    }
                    else
                    {
                        String img_str = msg.GetTag(imageContent[1]);
                        try
                        {
                            if (isSend)
                            {
                                image = this.rtfSend.findPic(imageContent[1]).Image;
                            }
                            else
                            {
                                image = ResClass.GetImgRes("wite");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    this.RTBRecord.addGifControl(imageContent[1], image);
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

        #endregion



        /// <summary>
        /// 在对话记录文本框中添加内容(多线程)
        /// </summary>
        /// <param name="str">要添加的内容</param>
        private void AppendSystemRtf(string str)
        {
            appendTextDelegate d = new appendTextDelegate(appendText);
            this.BeginInvoke(d, str);
        }

        #region 将对话记录滚动到最后一行
        /// <summary>
        /// 将对话记录滚动到最后一行
        /// </summary>
        private void RTBfocus()
        {
            this.RTBRecord.Select(this.RTBRecord.TextLength, 0);
        }
        #endregion

        private delegate void appendTextDelegate(string str);
        private void appendText(string str)
        {
            MyExtRichTextBox rich = new MyExtRichTextBox();

            rich.AppendText("\n");
            //rich.InsertImage(FormAccess.imageInformation);
            rich.AppendText(" " + str);
            rich.ForeColor = Color.Brown;

            this.RTBRecord.AppendRtf(rich.Rtf);
            rich.Clear();
            rich.Dispose();
            this.RTBRecord.AppendText(" ");
        }

        private void btn_filesend_MouseClick(object sender, MouseEventArgs e)
        {

            if (!OnLine)
                return;

            // currUserInfo = FormAccess.Users.find(this.Tag.ToString());
            //if (currUserInfo == null) return;

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

        #region 文件传输
        /// <summary>
        /// 异步处理计算文件 MD5值
        /// </summary>
        /// <param name="f">文件信息</param>
        private delegate void sendFileDelegate(System.IO.FileInfo f);
        private void sendFile(System.IO.FileInfo f)
        {
            CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
            try
            {
                fmsg.SetTag("m_type", 6);//告诉对方要发送文件啦
                fmsg.Type = MessageType.chat;
                fmsg.To = to_Jid;
                fmsg.SetTag("BPort", BaseLocalUDPPort);
                fmsg.SetTag("BIP", Program.LocalHostIP.ToString());
                fmsg.SetTag("isSend", true);
                fmsg.SetTag("File", f.FullName.ToString());
                fmsg.Body = "falg";
                _connection.Send(fmsg);
                AppendSystemRtf("等待好友查看接收命令.");
            }
            catch (Exception)
            {
                
            }
        }

        public void FileSendInit(String path)
        {
            if (InvokeRequired)
            {
                this.Invoke(FileSendInitEvent, path);
            }


            FileInfo f = new FileInfo(path);
            //this.FileTransfers(true, f.FullName, f.Name, (int)f.Length, f.Extension, Hasher.GetMD5Hash(f.FullName));

            this.FileTransfers(true, f.FullName, f.Name, (int)f.Length, f.Extension, Guid.NewGuid().ToString());

            //Thread th=new Thread(new ParameterizedThreadStart(sendFileTransfers));
            //th.Start(path);
        }

        //public void sendFileTransfers(object pathob)
        //{
        //    FileInfo f = new FileInfo(pathob.ToString());
        //    this.FileTransfers(true, f.FullName, f.Name, (int)f.Length, f.Extension, Hasher.GetMD5Hash(f.FullName));
        //}

        

        /// <summary>
        /// 发送或接收文件
        /// </summary>
        /// <param name="IsSend">true是发送者,false是接收者</param>
        /// <param name="fullFileName">要发送的文件的绝对路径，如果接收则传空字符串</param>
        /// <param name="FileName">文件名</param>
        /// <param name="FileLen">文件的长度</param>
        /// <param name="fileExtension">文件扩展名</param>
        /// <param name="FileMD5Value">文件的MD5值</param>
        public void FileTransfers(bool IsSend, string fullFileName, string FileName, int FileLen, string fileExtension, string FileMD5Value)
        {

            if (IsSend && IsTransfers(FileMD5Value))
            {
                this.AppendSystemRtf(FileName + " 已在传输队列中，请不要重复发送！");
                return;//如果要发送的文件已经在传输队列，则退出
            }

            p2pFile fSend = new p2pFile();//创建一个P2P文件传输组件用于传送文件
            fSend.Name = FileMD5Value;

            TabPage tab = new TabPage(FileName);
            tab.Controls.Add(fSend);
            tab.Tag = fSend;
            filelistfrom.tabControl1.TabPages.Add(tab);

            if (IsSend)
            {
                string fileInfo = FileName + "|" + FileLen.ToString() + "|" + fileExtension + "|" + FileMD5Value;//初次请求发送文件时要先发送“控件参数”到对方，请求对方创建“文件发送控件”并建立连接
                Msg msg = new Msg((byte)ProtocolClient.FileTransmitRequest, TextEncoder.textToBytes(fileInfo));

                this.sockUDP1.Send(BaseRemotUDPIP, BaseRemotUDPPort, msg.getBytes());
                AppendSystemRtf("请等待对方接收文件 " + FileName + ") 。请等待回应或取消传输。");
                //this.infoMessage1.showMsg("请等待对方接收文件 " + FileName + ") 。请等待回应或取消传输。");
            }
            else
            {
                AppendSystemRtf("对方要传送文件 " + FileName + "(" + Calculate.GetSizeStr(FileLen) + ")" + " 给您，请接收或取消传输。");
            }

            UserInfo self = new UserInfo();//将自己的在线基本信息告诉文件传输组件，使传输组件中的套接字确定文件数据包的网络传输协议 
            self.LocalIP = Program.LocalHostIP;
            self.IP = Program.LocalHostIP;

            UserInfo Opposite = new UserInfo();//将对方的在线基本信息告诉文件传输组件，使传输组件中的套接字确定文件数据包的网络传输协议
            Opposite.LocalIP = BaseRemotUDPIP;
            Opposite.IP = BaseRemotUDPIP;

            fSend.SetParameter(IsSend, fullFileName, FileName, FileLen, fileExtension, FileMD5Value, self, Opposite);//设置文件传输组件相关参数
            fSend.fileTransmitBefore += new p2pFile.fileTransmitBeforeEventHandler(fSend_fileTransmitBefore);//文件开始传输事件
            fSend.fileTransmitCancel += new p2pFile.fileTransmitCancelEventHandler(fSend_fileTransmitCancel);//文件传输取消事件
            fSend.fileTransmitError += new p2pFile.fileTransmitErrorEventHandler(fSend_fileTransmitError);//文件传输错误事件 
            fSend.fileTransmitted += new p2pFile.fileTransmittedEventHandler(fSend_fileTransmitted);//文件传输完成事件 
            fSend.getFileProxyID += new p2pFile.getFileProxyIDEventHandler(fSend_getFileProxyID);//文件传输获取中转服务ID
            fSend.fileTransmitGetUDPPort += new p2pFile.GetUDPPortEventHandler(fSend_fileTransmitGetUdpLocalPort);//获得文件传输套接字UDP侦听端口
            fSend.fileTransmitConnected += new p2pFile.ConnectedEventHandler(fSend_fileTransmitConnected);//当文件传输与对方建立传输通道事件
            fSend.Dock = System.Windows.Forms.DockStyle.Fill;

            this.filelistfrom.tabControl1.SelectedTab = tab;
            try
            {
                filelistfrom.TopMost = true;
                filelistfrom.Show();
            }
            catch (Exception)
            {

            }

            filelistfrom.Activate();
        }

        /// <summary>
        /// 文件传输组件事件：发送与接收双方的文件传输组件中的UDP或TCP套接字之间已经联接 
        /// </summary>
        /// <param name="sender">文件传输组件对像</param>
        /// <param name="netCommuctionClass">联接方式</param>
        private void fSend_fileTransmitConnected(object sender, NetCommunicationClass netCommuctionClass)
        {
            if (netCommuctionClass == NetCommunicationClass.LanUDP)
                //this.infoMessage1.showMsg("已与对方建立了UDP联接.");
                 AppendSystemRtf("已与对方建立了UDP联接.");
            //else if (netCommuctionClass == NetCommunicationClass.WanNoProxyUDP)
                //this.infoMessage1.showMsg("已与对方建立了UDP联接.");
                //AppendSystemRtf("已与对方建立了代理UDP联接.");
            else if (netCommuctionClass == NetCommunicationClass.TCP)
                //this.infoMessage1.showMsg("已与对方建立了TCP中转联接.");
                AppendSystemRtf("已与对方建立了TCP中转联接.");
            else if (netCommuctionClass == NetCommunicationClass.WanProxyUDP)
                //this.infoMessage1.showMsg("已与对方建立了UDP中转联接.");
                AppendSystemRtf("已与对方建立了UDP中转联接.");
            else if (netCommuctionClass == NetCommunicationClass.None)
            {
                AppendSystemRtf("无法与对方建立文件传输通道，取消文件传输！");
            }
        }

        /// <summary>
        /// 文件传输组件事件：创建了新的UDP套接字并已在随机端口上侦听
        /// </summary>
        /// <param name="sender">文件传输组件对像</param>
        /// <param name="Port">侦听端口</param>
        /// <param name="udpHandshakeInfoClass">与对方握手方式是局域网还是广域网，True为广域网，false为局域网</param>
        private void fSend_fileTransmitGetUdpLocalPort(object sender, int Port, bool udpHandshakeInfoClass)
        {
            Msg msg = new Msg((byte)ProtocolClient.FileTransmitGetUDPPort);
            msg.Content = TextEncoder.textToBytes((sender as p2pFile).FileMD5Value + "|" + Port.ToString() + "|" + udpHandshakeInfoClass.ToString());//将获得的本地UDP端口号传输给对方
            sockUDP1.Send(BaseRemotUDPIP, BaseRemotUDPPort, msg.getBytes());
            //FormAccess.sendMsgToOneUser(msg, this.currUserInfo);
            AppendSystemRtf("侦听UDP端口：" + Port.ToString());
            //this.infoMessage1.showMsg("侦听UDP端口：" + Port.ToString());
        }



        /// <summary>
        /// 文件传输组件事件：无法进行UDP P2P通信传输，采用TCP中转并从代理服务器上获得中转ID服务编号 
        /// </summary>
        /// <param name="sender">文件传输组件对像</param>
        /// <param name="proxyID">采用TCP中转并从代理服务器上获得中转的ID服务编号 </param>
        private void fSend_getFileProxyID(object sender, int proxyID)
        {
            Msg msg = new Msg((byte)ProtocolClient.FileTransmitGetOppositeID);
            msg.Content = TextEncoder.textToBytes((sender as p2pFile).FileMD5Value + "|" + proxyID.ToString());//将获得的服务器代理ID号传输给对方
            sockUDP1.Send(BaseRemotUDPIP, BaseRemotUDPPort, msg.getBytes());
            //AppendSystemRtf("获得文件传输中转服务ID："+ proxyID.ToString());
        }

        /// <summary>
        /// 文件传输组件事件：文件传输结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fSend_fileTransmitted(object sender, fileTransmitEvnetArgs e)
        {
            p2pFile FileSend = (sender as p2pFile);

            delTabItem(FileSend);//删除文件传输Tab控件

            if (e.isSend)
                //this.infoMessage1.showMsg("文件 " + e.fileName + " 已经传输完成.");
                AppendSystemRtf("文件 " + e.fileName + " 已经传输完成.");

            else
            {
                //this.infoMessage1.showMsg("文件 " + e.fileName + " 已经传输完成.");
                AppendSystemRtf("文件 " + e.fileName + " 已经传输完成.");
                AppendSystemRtf("文件 " + e.fileName + " 已经传输完成,保存路径<file:\\\\" + e.fullFileName + ">");
            }
        }

        /// <summary>
        /// 文件传输组件事件：文件传输结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fSend_fileTransmitOutTime(object sender, fileTransmitEvnetArgs e)
        {
            AppendSystemRtf(" 文件传输超时！");
        }

        /// <summary>
        /// 文件传输组件事件：文件传输错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fSend_fileTransmitError(object sender, fileTransmitEvnetArgs e)
        {

        }

        /// <summary>
        /// 文件传输组件事件：取消文件传输
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fSend_fileTransmitCancel(object sender, fileTransmitEvnetArgs e)
        {
            p2pFile FileSend = (sender as p2pFile);
            delTabItem(FileSend);//删除文件传输Tab控件

            if (e.isSend)
                //this.infoMessage1.showMsg("您已经取消了文件 " + e.fileName + " 的传输.");
                 AppendSystemRtf("您已经取消了文件 " + e.fileName + " 的传输.");
            else
                AppendSystemRtf("对方已经取消了文件 " + e.fileName + " 的传输！");

            Msg msg = new Msg((byte)ProtocolClient.FileTransmitCancel, TextEncoder.textToBytes(e.FileMD5Value));
            sockUDP1.Send(BaseRemotUDPIP, BaseRemotUDPPort, msg.getBytes());
        }

        /// <summary>
        /// 文件传输组件事件：文件传输数据包开始发送前 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fSend_fileTransmitBefore(object sender, fileTransmitEvnetArgs e)
        {
            if (!e.isSend)
                //this.infoMessage1.showMsg("您已经接收了文件 " + e.fileName + " 的传输，文件正在传输中...");
                AppendSystemRtf("您已经接收了文件 " + e.fileName + " 的传输，文件正在传输中...");
            else
                //this.infoMessage1.showMsg("对方已经接收了文件 " + e.fileName + " 的传输，文件正在传输中...");
            AppendSystemRtf("对方已经接收了文件 " + e.fileName + " 的传输，文件正在传输中...");
        }


        /// <summary>
        /// 删除文件传输控件,清除相关控件资源
        /// </summary>
        /// <param name="p2pFile"></param>
        private void delTabItem(p2pFile p2pFile)
        {
            try
            {
                foreach (TabPage tabItem in filelistfrom.tabControl1.TabPages)
                {
                    if ((tabItem.Tag as p2pFile).FileMD5Value == p2pFile.FileMD5Value)
                    {
                        p2pFile.Dispose();
                        this.filelistfrom.tabControl1.TabPages.Remove(tabItem);
                        tabItem.Dispose();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 判断文件是否在传输队列
        /// </summary>
        /// <param name="FileMD5Value">文件的MD5值</param>
        /// <returns></returns>
        private bool IsTransfers(string FileMD5Value)
        {
            try
            {
                foreach (TabPage tabItem in filelistfrom.tabControl1.TabPages)
                    if ((tabItem.Tag as p2pFileTransmit).FileMD5Value == FileMD5Value)
                        return true;
            }
            catch { }
            return false;
        }
        #endregion

        private void btn_filelist_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                filelistfrom.Show();
                filelistfrom.Activate();
            }
            catch (Exception)
            {

            }

        }

        public void FristMessage(List<CSS.IM.XMPP.protocol.client.Message> msg)
        {
            main_msg = msg;
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
                    if (e.KeyCode== Keys.Enter)
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
                    if (e.Control&&e.KeyCode== Keys.Enter)
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
                        this.rtfSend.SelectedText = map ;
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
            QQcm_send_key.Show(this, btn_send_key.Location);
        }

        /// <summary>
        /// 设置为enter发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QQtlm_key_enter_Click(object sender, EventArgs e)
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

        /// <summary>
        /// 设置为ctrl+enter发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QQtlm_key_ctrl_enter_Click(object sender, EventArgs e)
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

        /// <summary>
        /// 设置发送消息的快捷键
        /// </summary>
        /// <param name="value"></param>
        public void SetSendKeyType(int value)
        {
            Document doc_setting = new Document();

            Settings.Settings config = new Settings.Settings();
            doc_setting.LoadFile(CSS.IM.UI.Util.Path.ConfigFilename);
            Settings.Paths path = doc_setting.RootElement.SelectSingleElement(typeof(Settings.Paths), false) as Settings.Paths;
            path.SendKeyType = value;
            CSS.IM.UI.Util.Path.SendKeyType = value;
            doc_setting.Clear();

            config.Paths = path;
            doc_setting.ChildNodes.Add(config);
            doc_setting.Save(CSS.IM.UI.Util.Path.ConfigFilename);
        }

        #endregion

        /// <summary>
        /// 远程协助功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_remotedisktop_MouseClick(object sender, MouseEventArgs e)
        {
            //if (!RemoteAssist)
            //{
            //    RemoteAssist = true;

            //    lab_remote_context.Text = "您邀请使用远程协助。请等待回应……";
            //    panel_function.Visible = true;
            //    this.Width = this.Width + panel_function.Width;
            //    panel_msg.Width = panel_msg.Width - panel_function.Width;

            //    panel_remote.Location = new Point(7, 14);
            //    panel_remote.Visible = true;

            //    panel_accept_button.Location = new Point(6, 260);
            //    panel_accept_button.Visible = true;//操作发送远程协助的按键
            //    panel_receive_button.Visible = false;//接收远程协助的按键


            //    if (!RemoteAssist_Socket.Listened)
            //    {
            //        RemoteAssist_Socket.Listen(0);//开启远程协助胡端口
            //    }
                
            //    CSS.IM.XMPP.protocol.client.Message fmsg = new CSS.IM.XMPP.protocol.client.Message();
            //    fmsg.SetTag("m_type", 8);
            //    fmsg.Type = MessageType.chat;
            //    fmsg.To = to_Jid;
            //    fmsg.SetTag("BPort", RemoteAssist_Socket.ListenPort);
            //    fmsg.SetTag("BIP", Program.LocalHostIP.ToString());
            //    fmsg.Body = "falg";
            //    _connection.Send(fmsg);
            //}
        }

        private void RTBRecord_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            //MessageBox.Show(e.LinkText);
            //file:\\C:\Users\Administrator\Desktop\system.wav
            string url = e.LinkText;
            string falg = url.Substring(0, 4);
            if (falg=="http")
            {
                Process.Start(url);
            }
            else
            {
                url = url.Substring(7, url.Length - 7);
                Process proc = new Process();
                proc.StartInfo.FileName = "explorer"; //打开资源管理器
                proc.StartInfo.Arguments = @"/select," + url;
                proc.Start();
            }
            
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            DialogResult result;
            if (avForm!=null)
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

            if (filelistfrom.tabControl1.TabPages.Count>0)
            {
                result = MsgBox.Show(this, "CSS&IM", "当前正在传输文件是否结束聊天？", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    for (int i = 0; i < filelistfrom.tabControl1.TabPages.Count; i++)
                    {
                        TabPage tabpage = filelistfrom.tabControl1.TabPages[i] as TabPage;
                        p2pFile p2pfile = tabpage.Controls[0] as p2pFile;
                        p2pfile.CancelTransmit(true);
                    }
                    e.Cancel = true;
                   
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }

            Close_Check.Enabled = Close_Check.Enabled == false ? true : false;
        }

        private void Close_Check_Tick(object sender, EventArgs e)
        {
            if (filelistfrom.tabControl1.TabPages.Count == 0 && (avForm == null || avForm.IsDisposed == true) && (ravForm == null || ravForm.IsDisposed == true))
            {
                Thread.Sleep(1000);
                filelistfrom.Close();
                Close_Check.Enabled = false;
                this.Close();
            }
        }

        private void RTBRecord_DoubleClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 更改好友状态 是否在线
        /// </summary>
        /// <param name="value"></param>
        public void UpdateFriendOnline(bool value)
        {
            OnLine = value;

            VcardIq viq = new VcardIq(IqType.get, to_Jid);
            _connection.IqGrabber.SendIq(viq, new IqCB(VcardResult), null, true);
        }
    }
}

