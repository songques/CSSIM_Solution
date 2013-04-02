using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI;
using CSS.IM.App.Settings;
using CSS.IM.XMPP;
using CSS.IM.XMPP.Collections;
using CSS.IM.XMPP.protocol.client;
using CSS.IM.UI.Util;
using CSS.IM.UI.Control;
using CSS.IM.XMPP.protocol.iq.vcard;
using System.Threading;
using System.Drawing.Imaging;
using System.IO;
using CSS.IM.UI.Form;
using CSS.IM.Library;
using CSS.IM.Library.Class;
using CSS.IM.Library.Controls;
using System.Net;
using CSS.IM.XMPP.protocol.extensions.si;
using CSS.IM.XMPP.protocol.extensions.filetransfer;
using CSS.IM.XMPP.protocol.extensions.featureneg;
using CSS.IM.XMPP.protocol.x.data;
using CSS.IM.XMPP.protocol.extensions.bytestreams;
using CSS.IM.Library.AV;
using CSS.IM.App.Properties;
using CSS.IM.Library.AV.Controls;
using CSS.IM.App.Controls;
using CSS.IM.Library.ExtRichTextBox;
using System.Diagnostics;
using CSS.IM.XMPP.protocol.x.muc;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.XMPP.protocol.Base;

namespace CSS.IM.App
{
    public partial class ChatGroupForm : IChatForm
    {

        List<CSS.IM.XMPP.protocol.client.Message> main_msg = new List<XMPP.protocol.client.Message>();//用于保存从好友列表打开后传过来的消息队列

        //private bool IsPlayMsg = true;//是否播放消息声音

        CSS.IM.Library.gifCollections PicQueue = new gifCollections();//用于保存图片的发送队列

        public Jid to_Jid { get; set; }//保存远程会话的jid

        private EmotionDropdown _emotion;//表情管理
        private static XmppClientConnection _connection;

        private string _nickname;
        private Bitmap Bmp = null;
        private Graphics g = null;
        private string DownBtn = "";


        /// <summary>
        /// 异步响应PresenceChatGroupCell
        /// </summary>
        /// <param name="pres"></param>
        private delegate void PresenceChatGroupDelegate(Presence pres);

        #region 初使化
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                _connection.MessageGrabber.Remove(to_Jid);
                _connection.PresenceGrabber.Remove(to_Jid);

                MucManager mucManager = new MucManager(_connection);
                mucManager.LeaveRoom(to_Jid, _connection.MyJID.User);
                Util.GroupChatForms.Remove(to_Jid.Bare.ToLower());

            }
            catch (Exception)
            {

            }
            _connection = null;

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public ChatGroupForm(Jid jid, XmppClientConnection con, string nickname)
        {
            InitializeComponent();
            friend_list.OpenChatEvent += new ChatGroupListView.delegate_openChat(friend_list_OpenChatEvent);
            _emotion = new EmotionDropdown();

            to_Jid = jid;
            _connection = con;
            _nickname = jid.User;
            this.nikeName.Text = nickname;

            Util.GroupChatForms.Add(to_Jid.Bare.ToLower(), this);

            nikeName.Text = "当前所在会议室[" + _nickname + "]";

            _emotion.EmotionContainer.ItemClick += new UI.Face.EmotionItemMouseEventHandler(EmotionContainer_ItemClick);

            friendHead.BackgroundImage = ResClass.GetImgRes("Padding4Normal");
            friendHead.Image = ResClass.GetHead("big199");

        }

        void friend_list_OpenChatEvent(UI.Entity.Friend friend)
        {
            if (!Util.ChatForms.ContainsKey(friend.Ritem.Jid.Bare.ToString()))
            {
                ChatFromMsg chat = new ChatFromMsg(friend.Ritem.Jid, _connection);
                try
                {
                    chat.Show();
                }
                catch (Exception)
                {

                }

            }
        }

        void EmotionContainer_ItemClick(object sender, UI.Face.EmotionItemMouseClickEventArgs e)
        {
            rtfSend.addGifControl(e.Item.Tag.ToString(), e.Item.Image);
        }

        #endregion

        #region GDI
        public Image SetImage(Image image, ImageFormat format)
        {

            if (image != null)
            {
                Image temp = new Bitmap(image.Width, image.Height);
                Graphics g = Graphics.FromImage(temp);
                g.DrawImage(image, new Rectangle(0, 0, temp.Width, temp.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return temp;
            }
            else
            {
                return null;
            }
        }

        public byte[] SetImage(Image image)
        {
            Image temp = new Bitmap(image.Width, image.Height);
            //get graphics
            Graphics g = Graphics.FromImage(temp);

            g.DrawImage(image, new Rectangle(0, 0, temp.Width, temp.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            g.Dispose();

            MemoryStream ms = new MemoryStream();
            temp.Save(ms, ImageFormat.Gif);
            byte[] buf = ms.GetBuffer();
            return buf;
        }


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
            //CaptureImage capture = new CaptureImage();
            //if (capture.ShowDialog() == DialogResult.OK)
            //{
            //    pictureBox1.Image = capture.Image;
            //}

            using (CaptureImage capture = new CaptureImage())
            {
                if (capture.ShowDialog() == DialogResult.OK)
                {
                    capture.ShowDialog();
                    if (capture.Image != null)
                    {
                        System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(Util.sendImage);
                        if (!dInfo.Exists)
                            dInfo.Create();

                        string fileName = Util.sendImage + "temp.gif";

                        capture.Image.Save(fileName);
                        string md5 = Hasher.GetMD5Hash(fileName);
                        string Md5fileName = Util.sendImage + md5 + ".gif";

                        if (!System.IO.File.Exists(Md5fileName))
                        {
                            System.IO.File.Delete(fileName);
                            capture.Image.Save(Md5fileName);
                        }
                        try
                        {
                            this.rtfSend.addGifControl(md5, capture.Image);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                capture.Dispose();
            } 
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
            try
            {
                g = e.Graphics;
                Bmp = ResClass.GetImgRes("ChatFrame_QuickbarFrame_background");
                g.DrawImage(Bmp, new Rectangle(0, 0, 2, 22), 0, 0, 2, 22, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(2, 0, this.Width, 22), 2, 0, Bmp.Width - 4, 22, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width - 2, 0, 2, 22), Bmp.Width - 2, 0, 2, 22, GraphicsUnit.Pixel);
            }
            catch (Exception)
            {

            }

        }

        
        private void btnSet_MouseClick(object sender, MouseEventArgs e)
        {
            //chatGroupRoomSetForm.Show();
            //MucManager mucManagr = new MucManager(_connection);
            //mucManagr.RequestConfigurationForm(new Jid(to_Jid.Bare), new IqCB(RequestConfigurationForm), null);
            //<iq id="QBB6p-45" to="aabbcc_room@conference.songques-pc" type="get"><query xmlns="http://jabber.org/protocol/muc#owner"></query></iq>
            IQ iq = new IQ();
            iq.Namespace = null;
            iq.Id = CSS.IM.XMPP.Id.GetNextId();
            iq.To = new Jid(to_Jid.User, to_Jid.Server, _connection.MyJID.User);
            iq.Type = IqType.get;
            Query query = new Query();
            query.Namespace = CSS.IM.XMPP.Uri.MUC_OWNER;
            iq.AddChild(query);
            _connection.IqGrabber.SendIq(iq, new IqCB(RequestConfigurationForm), null, true);
        }
        #endregion

        ChatGroupRoomSetForm chatGrouSetFomr = null;
        private void RequestConfigurationForm(object sender, IQ iq, object data)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new IqCB(RequestConfigurationForm), new object[] { sender, iq, data });
            }
            Data fields = iq.Query.FirstChild as Data;
            if (fields != null)
            {
                if (chatGrouSetFomr == null || chatGrouSetFomr.IsDisposed)
                {
                    chatGrouSetFomr = new ChatGroupRoomSetForm();
                }
                chatGrouSetFomr.XMPPConn = _connection;
                chatGrouSetFomr.to_jid = new Jid(to_Jid.Bare);
                chatGrouSetFomr.fields = fields;
                try
                {
                    chatGrouSetFomr.Show();
                }
                catch (Exception)
                {

                }

            }
        }

        private void MessageCallback(object sender, CSS.IM.XMPP.protocol.client.Message msg, object data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageCB(MessageCallback), new object[] { sender, msg, data });
                return;
            }

            if (msg.Body != null)
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

                if (_connection.MyJID.User == msg.From.Resource)
                {
                    cl = Color.Lime;//获取颜色
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
                top_msg.Body = msg.From.Resource + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                RTBRecord_Show(top_msg, false);
                //RTBRecord.AppendTextAsRtf(msg.From.User + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "\n", new Font(this.Font, FontStyle.Underline | FontStyle.Bold), CSS.IM.Library.ExtRichTextBox.RtfColor.Red, CSS.IM.Library.ExtRichTextBox.RtfColor.White);
                RTBRecord_Show(msg, false);

                if (CSS.IM.UI.Util.Path.MsgSwitch)
                    SoundPlayEx.MsgPlay(CSS.IM.UI.Util.Path.MsgPath);



                //new Font(this.Font, FontStyle.Underline | FontStyle.Bold), 
                //CSS.IM.Library.ExtRichTextBox.RtfColor.Red, 
                //CSS.IM.Library.ExtRichTextBox.RtfColor.White
            }
            catch (Exception)
            {
                
            }
            


        }

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

        private void PresenceChatGroupMethod(Presence pres)
        {
 
            if (pres.Type == PresenceType.error)
            {
                MsgBox.Show(this, "CSS&IM", "密码错误，" + pres.Error.Code);
                //this.Enabled = true;
                this.Close();
            }
            if (pres.Type == PresenceType.available)
            {
                this.Enabled = true;
                friend_list.AddFriend(pres.MucUser.Item.Jid, _connection);
                ShowCellText(pres.MucUser.Item.Jid.User + "加入聊天室", Color.Blue);
            }
            if (pres.Type == PresenceType.unavailable)
            {
                friend_list.RemoveFroend(pres.MucUser.Item.Jid);
                ShowCellText(pres.MucUser.Item.Jid.User + "退出聊天室", Color.Blue);
            }
            
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {

                if (_connection == null)
                {
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

                //RTBRecord.AppendTextAsRtf(_connection.Username + ":" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "\n", new Font(this.Font, FontStyle.Underline | FontStyle.Bold), CSS.IM.Library.ExtRichTextBox.RtfColor.Lime, CSS.IM.Library.ExtRichTextBox.RtfColor.White);
                //CSS.IM.XMPP.protocol.client.Message top_msg = new XMPP.protocol.client.Message();
                //top_msg.SetTag("FName", this.Font.Name);//获得字体名称
                //top_msg.SetTag("FSize", this.Font.Size);//字体大小
                //top_msg.SetTag("FBold", true);//是否粗体
                //top_msg.SetTag("FItalic", this.Font.Italic);//是否斜体
                //top_msg.SetTag("FStrikeout", this.Font.Strikeout);//是否删除线
                //top_msg.SetTag("FUnderline", true);//是否下划线


                //Color tcl = Color.Lime;//获取颜色
                //byte[] tcby = BitConverter.GetBytes(tcl.ToArgb());
                //top_msg.SetTag("CA", tcby[0]);
                //top_msg.SetTag("CR", tcby[1]);
                //top_msg.SetTag("CG", tcby[2]);
                //top_msg.SetTag("CB", tcby[3]);
                //top_msg.Body = _connection.Username + ":" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second ;
                //RTBRecord_Show(top_msg, true);

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
                msg.To = new Jid(to_Jid.User, to_Jid.Server, null);
                msg.SetTag("face", rtfSend.GetImageInfo().ToString());
                msg.Body = rtfSend.Text;

                //保存要发送的图片;
                CSS.IM.Library.gifCollections tempGifs = this.rtfSend.GetExistGifs();
                foreach (MyPicture pic in tempGifs)
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
                
                _connection.Send(msg);
                //RTBRecord_Show(msg, true);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        private void ShowCellText(String text, Color color)
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

        #region 窗体事件

        private void ChatGroupForm_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.Text = "当前会议室-" + to_Jid.ToString();

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

            //rtfSend.Focus();
            //rtfSend.Select(this.rtfSend.TextLength, 0);
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


      
        public void Initial(String pswd)
        {
            Jid jid = to_Jid;
            jid.Resource = _connection.MyJID.User;

            this.Show();
            this.Enabled = false;

            _connection.PresenceGrabber.Add(jid, new PresenceCB(PresenceChatGroupCell), null);
            _connection.MessageGrabber.Add(jid, new BareJidComparer(), new MessageCB(MessageCallback), null);
            Presence spres = new Presence();
            spres.To = jid;
            Muc x = new Muc();
            if (pswd != null)
                x.Password = pswd;
            History hist = new History();
            hist.MaxCharacters = 100;
            x.History = hist;
            spres.AddChild(x);
            _connection.Send(spres);

        }

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
                    this.rtfSend.SelectedText = map ;
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

        private void friend_list_friend_qcm_MouseClickEvent(object sender, Jid item, string type)
        {
            switch (type)
            {
                case "资料":
                    VcardForm vcardForm = new VcardForm(item, _connection);
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

        private void RTBRecord_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            //MessageBox.Show(e.LinkText);
            //file:\\C:\Users\Administrator\Desktop\system.wav
            string url = e.LinkText;
            string falg = url.Substring(0, 4);
            if (falg == "http")
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
       
    }
}

