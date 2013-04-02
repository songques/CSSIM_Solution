using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI;
using CSS.IM.XMPP;
using CSS.IM.UI.Control;
using CSS.IM.XMPP.protocol.client;
using CSS.IM.XMPP.protocol.x.muc;
using CSS.IM.App.Settings;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.UI.Util;

namespace CSS.IM.App
{
    public partial class ChatGroupFormMsg : IChatForm
    {
        List<CSS.IM.XMPP.protocol.client.Message> main_msg = new List<XMPP.protocol.client.Message>();//用于保存从好友列表打开后传过来的消息队列

        CSS.IM.Library.gifCollections PicQueue = new CSS.IM.Library.gifCollections();//用于保存图片的发送队列

        public Jid TO_Jid { get; set; }//保存远程会话的jid

        private EmotionDropdown emotionDropdown;//表情管理
        private static XmppClientConnection XmppConn;

        private string _NickName;

        private ChatGroupRoomSetForm chatGroupRoomSetForm;//聊天设置功能

        /// <summary>
        /// 异步响应PresenceChatGroupCell
        /// </summary>
        /// <param name="pres"></param>
        private delegate void PresenceChatGroupDelegate(Presence pres);

        #region 代理实现方法
        /// <summary>
        /// 聊天室的验证信息和通知回调事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pres"></param>
        /// <param name="data"></param>
        public void PresenceChatGroupCell(object sender, Presence pres, object data)
        {
            try
            {
                //if (InvokeRequired)
                //{
                //    this.BeginInvoke(new PresenceCB(PresenceChatGroupCell), new object[] { sender, pres, data });
                //}

                this.BeginInvoke(new PresenceChatGroupDelegate(PresenceChatGroupMethod), new object[] { pres });
            }
            catch (Exception)
            {

            }


        }

        /// <summary>
        /// 用于PresenceChatGroupCell回调调用
        /// </summary>
        /// <param name="pres"></param>
        private void PresenceChatGroupMethod(Presence pres)
        {

            if (pres.Type == PresenceType.error)
            {
                CSS.IM.UI.Form.MsgBox.Show(this, "CSS&IM", "密码错误，" + pres.Error.Code);
                //this.Enabled = true;
                this.Close();
            }
            if (pres.Type == PresenceType.available)
            {
                this.Enabled = true;
                friend_list.AddFriend(pres.MucUser.Item.Jid, XmppConn);
                RTBRecord_Show(pres.MucUser.Item.Jid.User + "加入聊天室", Color.Blue);
            }
            if (pres.Type == PresenceType.unavailable)
            {
                friend_list.RemoveFroend(pres.MucUser.Item.Jid);
                RTBRecord_Show(pres.MucUser.Item.Jid.User + "退出聊天室", Color.Blue);
            }

        }
        #endregion

        #region 接收消息处理
        private void MessageCallback(object sender, CSS.IM.XMPP.protocol.client.Message msg, object data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageCB(MessageCallback), new object[] { sender, msg, data });
                return;
            }
            IncomingMessage(msg);
        }

        public void IncomingMessage(CSS.IM.XMPP.protocol.client.Message msg)
        {
            try
            {
                CSS.IM.XMPP.protocol.client.Message top_msg = new XMPP.protocol.client.Message();
                top_msg.SetTag("FName", this.Font.Name);//获得字体名称
                top_msg.SetTag("FSize", this.Font.Size);//字体大小
                top_msg.SetTag("FBold", true);//是否粗体
                top_msg.SetTag("FItalic", this.Font.Italic);//是否斜体
                top_msg.SetTag("FStrikeout", this.Font.Strikeout);//是否删除线
                top_msg.SetTag("FUnderline", true);//是否下划线

                Color cl;

                if (XmppConn.MyJID.User == msg.From.Resource)
                {
                    cl = Color.FromArgb(33, 119, 207);//获取颜色
                }
                else
                {
                    cl = Color.Red;//获取颜色
                }
                byte[] cby = BitConverter.GetBytes(cl.ToArgb());
                top_msg.SetTag("CA", cby[0]);
                top_msg.SetTag("CR", cby[1]);
                top_msg.SetTag("CG", cby[2]);
                top_msg.SetTag("CB", cby[3]);

                string nickname = msg.GetTag("NickName").ToString();
                if (nickname == null)
                {
                    nickname = msg.From.Resource;
                }
                if (nickname.Trim().Length == 0)
                {
                    nickname = msg.From.Resource;
                }
                nickname=nickname + "(" + msg.From.Resource + ")";
                top_msg.Body = nickname + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                RTBRecord_Show(top_msg, false);
                //RTBRecord.AppendTextAsRtf(msg.From.User + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "\n", new Font(this.Font, FontStyle.Underline | FontStyle.Bold), CSS.IM.Library.ExtRichTextBox.RtfColor.Red, CSS.IM.Library.ExtRichTextBox.RtfColor.White);
                RTBRecord_Show(msg, false);

                if (CSS.IM.UI.Util.Path.MsgSwitch)
                    CSS.IM.UI.Util.SoundPlayEx.MsgPlay(CSS.IM.UI.Util.Path.MsgPath);



                //new Font(this.Font, FontStyle.Underline | FontStyle.Bold), 
                //CSS.IM.Library.ExtRichTextBox.RtfColor.Red, 
                //CSS.IM.Library.ExtRichTextBox.RtfColor.White
            }
            catch (Exception)
            {

            }
        }
        #endregion

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {

            Util.GroupPresenceEvent -= new Util.PresenceHandler(Util_GroupPresenceEvent);

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
                XmppConn.MessageGrabber.Remove(TO_Jid);
                XmppConn.PresenceGrabber.Remove(TO_Jid);

            }
            catch (Exception)
            {

            }

            try
            {
                MucManager mucManager = new MucManager(XmppConn);
                mucManager.LeaveRoom(TO_Jid, XmppConn.MyJID.User);
            }
            catch (Exception)
            {

            }

            try
            {
                Util.GroupChatForms.Remove(TO_Jid.Bare.ToLower());
            }
            catch (Exception)
            {

            }

            if (emotionDropdown != null)
            {
                emotionDropdown.Dispose();
            }

            if (chatGroupRoomSetForm != null)
            {
                chatGroupRoomSetForm.Dispose();
            }

            if (PicQueue != null)
            {
                PicQueue.Dispose();
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

            System.GC.Collect();
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jid"></param>
        /// <param name="con"></param>
        /// <param name="nickname"></param>
        public ChatGroupFormMsg(Jid jid, XmppClientConnection con, string nickname)
        {
            InitializeComponent();
            friend_list.OpenChatEvent += new ChatGroupListView.delegate_openChat(friend_list_OpenChatEvent);

            TO_Jid = jid;
            XmppConn = con;
            _NickName = jid.User;
            this.nikeName.Text = nickname;

            Util.GroupChatForms.Add(TO_Jid.Bare.ToLower(), this);

            nikeName.Text = "当前所在会议室[" + _NickName + "]";

        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatGroupFormMsg_Load(object sender, EventArgs e)
        {

            this.rtfSend.Font = CSS.IM.UI.Util.Path.SFong;
            this.rtfSend.ForeColor = CSS.IM.UI.Util.Path.SColor;

            this.Text = "当前会议室-" + TO_Jid.ToString();

            foreach (CSS.IM.XMPP.protocol.client.Message mg in main_msg)
            {
                //IsPlayMsg = false;
                IncomingMessage(mg);
                //IsPlayMsg = true;
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
            //加入好友头像更新事件
            Util.GroupPresenceEvent += new Util.PresenceHandler(Util_GroupPresenceEvent);
        }

        /// <summary>
        /// 通过好友来更新列表 头像
        /// </summary>
        /// <param name="sender"></param>
        public void Util_GroupPresenceEvent(object sender)
        {
            friend_list.RefreshFroend((Jid)sender);
        }

        /// <summary>
        /// 通验证后要进行聊天室初始化
        /// </summary>
        /// <param name="pswd"></param>
        public void Initial(String pswd)
        {
            friendHead.Image = CSS.IM.UI.Util.ResClass.GetImgRes("big199");

            Jid jid = TO_Jid;
            jid.Resource = XmppConn.MyJID.User;

            this.Show();
            this.Enabled = false;

            XmppConn.PresenceGrabber.Add(jid, new PresenceCB(PresenceChatGroupCell), null);
            XmppConn.MessageGrabber.Add(jid, new CSS.IM.XMPP.Collections.BareJidComparer(), new MessageCB(MessageCallback), null);
            Presence spres = new Presence();
            spres.To = jid;
            Muc x = new Muc();
            if (pswd != null)
                x.Password = pswd;
            History hist = new History();
            hist.MaxCharacters = 100;
            x.History = hist;
            spres.AddChild(x);
            XmppConn.Send(spres);

        }

        /// <summary>
        /// 通过分组聊天列表，打开点对点聊天
        /// </summary>
        /// <param name="friend"></param>
        private void friend_list_OpenChatEvent(UI.Entity.Friend friend, string nickName)
        {
            if (!Util.ChatForms.ContainsKey(friend.Ritem.Jid.Bare))
            {
                ChatFromMsg chat = new ChatFromMsg(friend.Ritem.Jid, XmppConn, nickName);
                chat.UpdateFriendOnline(true);
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
                    ChatFromMsg chatform = Util.ChatForms[friend.Ritem.Jid.Bare] as ChatFromMsg;
                    chatform.WindowState = FormWindowState.Normal;
                    chatform.Activate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 头像选择功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EmotionContainer_ItemClick(object sender, UI.Face.EmotionItemMouseClickEventArgs e)
        {
            rtfSend.addGifControl(e.Item.Tag.ToString(), e.Item.Image);
        }

        /// <summary>
        /// 选择设置发送快捷按键
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
            Document doc_setting = new Document();

            Settings.Settings config = new Settings.Settings();
            doc_setting.LoadFile(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
            Settings.Paths path = doc_setting.RootElement.SelectSingleElement(typeof(Settings.Paths), false) as Settings.Paths;
            path.SendKeyType = value;
            CSS.IM.UI.Util.Path.SendKeyType = value;
            doc_setting.Clear();

            config.Paths = path;
            doc_setting.ChildNodes.Add(config);
            doc_setting.Save(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
        }

        /// <summary>
        /// 更新消息显示
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isSend"></param>
        public void RTBRecord_Show(CSS.IM.XMPP.protocol.client.Message msg, bool isSend)
        {

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
                face = msg.GetTag("face").ToString();
            }
            catch (Exception)
            {
                face = "";
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

                    if (emotionDropdown == null)
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
                                image = CSS.IM.UI.Util.ResClass.GetImgRes("wite");
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

        /// <summary>
        /// 用于显示进入聊天室或退出聊天室的信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public void RTBRecord_Show(String text, Color color)
        {
            CSS.IM.XMPP.protocol.client.Message top_msg = new XMPP.protocol.client.Message();
            top_msg.SetTag("FName", this.Font.Name);//获得字体名称
            top_msg.SetTag("FSize", this.Font.Size);//字体大小
            top_msg.SetTag("FBold", true);//是否粗体
            top_msg.SetTag("FItalic", this.Font.Italic);//是否斜体
            top_msg.SetTag("FStrikeout", this.Font.Strikeout);//是否删除线
            top_msg.SetTag("FUnderline", true);//是否下划线

            Color cl = color;

            byte[] cby = BitConverter.GetBytes(cl.ToArgb());
            top_msg.SetTag("CA", cby[0]);
            top_msg.SetTag("CR", cby[1]);
            top_msg.SetTag("CG", cby[2]);
            top_msg.SetTag("CB", cby[3]);
            top_msg.Body = text;
            RTBRecord_Show(top_msg, false);
        }

        /// <summary>
        /// 显示表情选取窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_face_Click(object sender, EventArgs e)
        {
            if (emotionDropdown == null)
            {
                emotionDropdown = new EmotionDropdown();
                emotionDropdown.EmotionContainer.ItemClick += new UI.Face.EmotionItemMouseEventHandler(EmotionContainer_ItemClick);
            }
            emotionDropdown.Show(btn_face);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {

                if (XmppConn == null)
                {
                    this.Close();
                }


                if (rtfSend.Text.Length > 1000)
                {
                    CSS.IM.UI.Form.MsgBox.Show(this, "CSS&IM", "发送内容长度不能大于1000!", MessageBoxButtons.OK);
                    return;
                }

                if (rtfSend.GetImageInfo().Length == 0)
                {

                    if (!(rtfSend.Text.Trim().Length > 0))
                    {
                        return;
                    }
                }

                CSS.IM.XMPP.protocol.client.Message msg = new CSS.IM.XMPP.protocol.client.Message();
                msg.Namespace = null;
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
                msg.Type = MessageType.groupchat;
                msg.To = new Jid(TO_Jid.User, TO_Jid.Server, null);
                msg.SetTag("face", rtfSend.GetImageInfo().ToString());
                msg.Body = rtfSend.Text;

                //保存要发送的图片;
                CSS.IM.Library.gifCollections tempGifs = this.rtfSend.GetExistGifs();
                foreach (CSS.IM.Library.MyPicture pic in tempGifs)
                {
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

                //发送自己的昵称
                if (Util.vcard.Nickname == null)
                {
                    msg.SetTag("NickName", XmppConn.MyJID.User);
                }
                else
                {
                    msg.SetTag("NickName", Util.vcard.Nickname.Trim().Length > 0 ? Util.vcard.Nickname : XmppConn.MyJID.User);
                }

                XmppConn.Send(msg);
            }
            catch
            {

            }

            System.GC.Collect();
        }

        /// <summary>
        /// 关闭窗体功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查看聊天室中好友的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item"></param>
        /// <param name="type"></param>
        private void friend_list_friend_qcm_MouseClickEvent(object sender, Jid item, string type)
        {
            switch (type)
            {
                case "资料":
                    VcardInfoForm vcardForm = new VcardInfoForm(item, XmppConn);
                    try
                    {
                        vcardForm.Show();
                    }
                    catch (Exception)
                    {

                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 聊天室设置功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_set_Click(object sender, EventArgs e)
        {
            IQ iq = new IQ();
            iq.Namespace = null;
            iq.Id = CSS.IM.XMPP.Id.GetNextId();
            iq.To = new Jid(TO_Jid.User, TO_Jid.Server, XmppConn.MyJID.User);
            iq.Type = IqType.get;
            CSS.IM.XMPP.protocol.Base.Query query = new CSS.IM.XMPP.protocol.Base.Query();
            query.Namespace = CSS.IM.XMPP.Uri.MUC_OWNER;
            iq.AddChild(query);
            XmppConn.IqGrabber.SendIq(iq, new IqCB(RequestConfigurationForm), null, true);
        }

        /// <summary>
        /// 聊天室设置回调事件窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="iq"></param>
        /// <param name="data"></param>
        private void RequestConfigurationForm(object sender, IQ iq, object data)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new IqCB(RequestConfigurationForm), new object[] { sender, iq, data });
            }
            CSS.IM.XMPP.protocol.x.data.Data fields = iq.Query.FirstChild as CSS.IM.XMPP.protocol.x.data.Data;
            if (fields != null)
            {
                if (chatGroupRoomSetForm == null || chatGroupRoomSetForm.IsDisposed)
                {
                    chatGroupRoomSetForm = new ChatGroupRoomSetForm();
                }
                chatGroupRoomSetForm.XMPPConn = XmppConn;
                chatGroupRoomSetForm.to_jid = new Jid(TO_Jid.Bare);
                chatGroupRoomSetForm.fields = fields;
                try
                {
                    chatGroupRoomSetForm.Show();
                }
                catch (Exception)
                {

                }

            }
        }


        /// <summary>
        /// 发送文本区的按键监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                if (data.GetDataPresent(typeof(string)))
                {
                    string map = (string)data.GetData(typeof(string));//将图片数据存到位图中
                    this.rtfSend.SelectedText = map;
                }

                //lock (clipboard_object)
                //{
                //    //如果剪贴板有图片
                //    if (data.GetDataPresent(typeof(Bitmap)))
                //    {
                //        Bitmap map = (Bitmap)data.GetData(typeof(Bitmap));//将图片数据存到位图中
                //        //this.pictureBox1.Image = map;//显示
                //        //map.Save(@"C:\a.bmp");//保存图片

                //        System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(Util.sendImage);
                //        if (!dInfo.Exists)
                //            dInfo.Create();

                //        string fileName = Util.sendImage + "temp.gif";

                //        map.Save(fileName);
                //        //string md5 = Hasher.GetMD5Hash(fileName);
                //        string md5 = Guid.NewGuid().ToString();
                //        string Md5fileName = Util.sendImage + md5 + ".gif";

                //        if (!System.IO.File.Exists(Md5fileName))
                //        {
                //            System.IO.File.Delete(fileName);
                //            map.Save(Md5fileName);
                //        }
                //        try
                //        {
                //            this.rtfSend.addGifControl(md5, map);
                //        }
                //        catch (Exception)
                //        {

                //        }
                //    }
                //}
            }
            #endregion
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
        /// 字体颜色设置
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

                if (color == null)
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
        /// 字体设置
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

                if (font == null)
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

    }
}
