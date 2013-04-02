using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Text;
using CSS.IM.UI.Util;
using CSS.IM.UI.Entity;
using CSS.IM.XMPP;
using CSS.IM.XMPP.protocol.iq.vcard;
using CSS.IM.XMPP.protocol.client;
using System.Diagnostics;

namespace CSS.IM.UI.Control
{
    public partial class FriendControl : UserControl
    {

        private XmppClientConnection XmppConn;
        private Jid MJid;

        private string NameRmark=null;//备注名称
        public string NickName{get;set;}//显示的名称
        public Boolean OnLine { get; set; }//用户是否在线
        
        public Timer timer_flicker;//用于新闪烁的线程控制
        public enum Dri { L, T, R };//用于头像闪烁方向
        Dri dri = Dri.T;//用于头像闪烁默认方向
        bool isLR = false;//用于头像闪烁

        public int x = 8;
        public int y = 8;

        /// <summary>
        /// 大头像为 40 小头像为 20
        /// </summary>
        private int HeadWidth = 40;
        private int HeadHeight = 40;

        /// <summary>
        /// 头像大小
        /// </summary>
        FriendContainerType _FCType = FriendContainerType.Big;
        public FriendContainerType FCType
        {
            get { return _FCType; }
            set { 
                    _FCType = value;
                    if (value == FriendContainerType.Big)
                    {
                        HeadWidth = 40;
                        HeadHeight = 40;
                    }
                    else
                    {
                        HeadWidth = 20;
                        HeadHeight = 20;
                    }
                    if (FriendInfo!=null)
                        ChangeInfo(FriendInfo);
                    
                }
        }

        private System.Drawing.Graphics g ;
        private Bitmap bgImg ;
        private Bitmap headImg ;
        private Bitmap headImgbak ;
        private Bitmap headBorder ;
        private Font f;

        private Friend _friendInfo;
        public Friend FriendInfo
        {
            get{return _friendInfo;}
            set{ OnLine = value.IsOnline;
                _friendInfo = value;
                ChangeInfo(_friendInfo);
                this.Invalidate();}

        }

        public bool IsSelected
        {
            get { return IsSelect; }
            set
            {
                IsSelect = value;
                if (value)
                {
                    bgImg = ResClass.GetImgRes("friendTitleOpenbg");
                    headBorder = ResClass.GetImgRes("Padding4Press");
                }
                else
                {
                    bgImg = null;
                    headBorder = ResClass.GetImgRes("Padding4Normal");
                }
                this.Invalidate();
            }
        }

        private bool IsSelect = false;

        private IContainer components;

        public delegate void SelectedEventHandler(Friend sender);
        public event SelectedEventHandler Selecting;

        public delegate void OpenChatEventHandler(Friend sender);
        public event OpenChatEventHandler OpenChat;

        public delegate void ShowContextMenuEventHandler(FriendControl sender, MouseEventArgs e);
        public event ShowContextMenuEventHandler ShowContextMenu;

        protected override void Dispose(bool disposing)
        {
            if (bgImg != null)
            {
                bgImg.Dispose();
                bgImg = null;
            }

            if (headImg != null)
            {
                headImg.Dispose();
                headImg = null;
            }
            if (headImgbak != null)
            {
                headImgbak.Dispose();
                headImgbak = null;
            }

            if (headBorder != null)
            {
                headBorder.Dispose();
                headBorder = null;
            }

            if (_friendInfo != null)
            {
                _friendInfo = null;
            }

            if (f != null)
            {
                f.Dispose();
                f = null;
            }

            if (timer_flicker != null)
            {
                timer_flicker.Enabled = false;
                timer_flicker.Dispose();
                timer_flicker = null;
            }

            if (g != null)
            {
                g.Dispose();
                g = null;
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            try
            {
                base.Dispose(disposing);
            }
            catch (System.Exception)
            {

            }
            System.GC.Collect();
        }

        public FriendControl(XmppClientConnection conn,Jid jid)
        {
            XmppConn = conn;
            MJid = jid;
            NickName = MJid.User;

            InitializeComponent();
            headImgbak = ResClass.GetImgRes("big194");
            bgImg = null;
            this.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            this.BackColor = Color.Transparent;

            if (Environment.OSVersion.Version.Major >= 6)
            {
                f = new Font("微软雅黑", 9F, FontStyle.Regular);
            }
            else
            {
                f = new Font("宋体", 9F, FontStyle.Regular);
            }

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            System.GC.Collect();

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer_flicker = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer_flicker.Interval = 500;
            this.timer_flicker.Tick += new System.EventHandler(this.timer_flicker_Tick);
            // 
            // FriendControl
            // 
            this.Name = "FriendControl";
            this.ResumeLayout(false);
            System.GC.Collect();

        }

        public void Selected()
        {
            OnMouseClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            System.GC.Collect();
        }

        public void UpdateImage()
        {
            VcardIq viq = new VcardIq(IqType.get, new Jid(FriendInfo.Ritem.Jid.Bare));
            XmppConn.IqGrabber.SendIq(viq, new IqCB(VcardResult), null, true);
            System.GC.Collect();
        }

        private void VcardResult(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new IqCB(VcardResult), new object[] { sender, iq, data });
                return;
            }
            if (iq.Type == IqType.result)
            {
                Vcard vcard = iq.Vcard;
                if (vcard != null)
                {
                    Photo photo = vcard.Photo;
                    if (photo != null)
                    {
                        headImgbak = new Bitmap(photo.BitImage, new Size(40, 40));
                    }
                    else
                    {
                        headImgbak = ResClass.GetHead(FriendInfo.HeadIMG);
                    }
                    NickName = vcard.Nickname.Trim() == "" ? MJid.User : vcard.Nickname.Trim();
                    NameRmark = NickName;//没办法的处理以后有血液的时候在说
                    FriendInfo.Description = vcard.Description;
                    FriendInfo.NikeName = NickName;
                }
                else
                {
                    headImgbak = ResClass.GetHead(FriendInfo.HeadIMG);
                }
                headImg = new Bitmap(headImgbak);
            }            
            ChangeInfo(_friendInfo);
            System.GC.Collect();
        }

        private void timer_flicker_Tick(object sender, EventArgs e)
        {
            switch (dri)
            {
                case Dri.L:
                    dri = Dri.T;
                    x = 3;
                    y = 12;
                    break;
                case Dri.T:
                    x = 8;
                    y = 8;
                    if (isLR)
                    {
                        isLR = false;
                        dri = Dri.L;
                    }
                    else
                    {
                        isLR = true;
                        dri = Dri.R;
                    }
                    break;
                case Dri.R:
                    dri = Dri.T;
                    x = 11;
                    y = 12;
                    break;
            }
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;
            if (bgImg != null)
            {
                g.DrawImage(bgImg, new Rectangle(0, 0, this.Width, 1), 0, 0, bgImg.Width, 1, GraphicsUnit.Pixel);
                g.DrawImage(bgImg, new Rectangle(0, 1, 1, this.Height - 2), 0, 1, 1, bgImg.Height - 2, GraphicsUnit.Pixel);
                g.DrawImage(bgImg, new Rectangle(1, 1, this.Width - 2, this.Height - 2), 1, 1, bgImg.Width - 2, bgImg.Height - 2, GraphicsUnit.Pixel);
                g.DrawImage(bgImg, new Rectangle(this.Width - 1, 1, 1, this.Height - 2), bgImg.Width - 1, 1, 1, bgImg.Height - 2, GraphicsUnit.Pixel);
                g.DrawImage(bgImg, new Rectangle(0, this.Height - 1, this.Width, 1), 0, bgImg.Height - 1, bgImg.Width, 1, GraphicsUnit.Pixel);
            }
            if (!FriendInfo.IsSysHead)
                headBorder = ResClass.GetImgRes("Padding4Normal");
            else
                headBorder = ResClass.GetImgRes("Padding4Press");

            if (headBorder != null)
            {
                g.DrawImage(headBorder, new Rectangle(x - 4, y - 4, HeadWidth + 8, HeadHeight+8), 0, 0, 48, 48, GraphicsUnit.Pixel);
            }


            g.DrawImage(headImg, new Rectangle(x, y, HeadWidth, HeadHeight), 0, 0, headImg.Width, headImg.Height, GraphicsUnit.Pixel);

            if (NameRmark == null)
                NameRmark = NickName;


            if (FCType==FriendContainerType.Big)
            {
                g.DrawString(NameRmark, f, new SolidBrush(Color.FromArgb(240, 0, 0, 0)), 60, 7); 
                g.DrawString(FriendInfo.Description != null ? FriendInfo.Description.Replace("\n", "") : "", f, new SolidBrush(Color.FromArgb(100, 0, 0, 0)), 60, 28);
            }
            else
            {
                g.DrawString(NameRmark, f, new SolidBrush(Color.FromArgb(240, 0, 0, 0)), 40, 7);
                SizeF sizeF = g.MeasureString(NameRmark, Font);
                g.DrawString(FriendInfo.Description != null ? FriendInfo.Description.Replace("\n", "") : "", f, new SolidBrush(Color.FromArgb(100, 0, 0, 0)), sizeF.Width + 40 + 5, 7);
            }

            System.GC.Collect();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!IsSelected)
            {
                bgImg = ResClass.GetImgRes("friendTitlebg");
                headBorder = ResClass.GetImgRes("Padding4Select");
                this.Invalidate();
            }
            System.GC.Collect();

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!IsSelected)
            {
                bgImg = null;
                headBorder = null;
                this.Invalidate();
            }
            System.GC.Collect();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!IsSelected)
                {
                    IsSelected = true;
                    bgImg = ResClass.GetImgRes("friendTitleOpenbg");
                    headBorder = ResClass.GetImgRes("Padding4Press");
                    this.Invalidate();
                    if (Selecting != null)
                        Selecting(this.FriendInfo);
                }
            }
            else
            {
                IsSelected = true;
                if (Selecting != null)
                    Selecting(this.FriendInfo);
                if (ShowContextMenu != null)
                    ShowContextMenu(this, e);

            }
            System.GC.Collect();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (e.Button == MouseButtons.Left)
            {
                if (!IsSelected)
                {
                    bgImg = ResClass.GetImgRes("friendTitleOpenbg");
                    headBorder = ResClass.GetImgRes("Padding4Press");
                    this.Invalidate();
                }
                if (OpenChat != null)
                {
                    OpenChat(this.FriendInfo);
                    timer_flicker.Enabled = false;
                    x = 8; y = 8;
                    this.Invalidate();
                }
            }
            System.GC.Collect();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            int Rgn = Win32.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 5, 5);
            Win32.SetWindowRgn(this.Handle, Rgn, true);
            this.Invalidate();
            System.GC.Collect();
        }

        /// <summary>
        /// 状态头像更改
        /// </summary>
        /// <param name="friend"></param>
        private void ChangeInfo(Friend friend)
        {
            Bitmap state = null;
            Bitmap headMark = null;
            System.Drawing.Graphics grap = null;

            if (!FriendInfo.IsOnline)
            {
                headImg = null;
                headImg = ResClass.MarkTopHead(headImgbak);//制作黑白头像;    
            }

            FriendInfo.State = friend.State;


            switch (friend.State)
            {
                case 0:
                    if (!FriendInfo.IsOnline)
                        break;
                    headImg = null;
                    headImg = new Bitmap(headImgbak);
                    break;
                case 1:
                    break;
                case 2:
                    if (!FriendInfo.IsOnline)
                        break;
                    state = new Bitmap(ResClass.GetImgRes("away"), new Size(11, 11));
                    headMark = new Bitmap(headImgbak,new Size(HeadWidth, HeadHeight));
                    grap = System.Drawing.Graphics.FromImage(headMark);
                    //grap.DrawImage(state, 28, 28);
                    grap.DrawImage(state, HeadWidth - 12, HeadHeight-12);

                    headImg = null;
                    headImg = new Bitmap(headMark, new Size(HeadWidth, HeadHeight));
                    break;
                case 3:
                    break;
                case 4:
                    if (!FriendInfo.IsOnline)
                        break;
                    state = new Bitmap(ResClass.GetImgRes("busy"), new Size(11, 11));
                    headMark = new Bitmap(headImgbak, new Size(HeadWidth, HeadHeight));
                    grap = System.Drawing.Graphics.FromImage(headMark);
                    //grap.DrawImage(state, 28, 28);
                    grap.DrawImage(state, HeadWidth - 12, HeadHeight-12);

                    headImg = null;
                    headImg = new Bitmap(headMark);
                    break;
                default:
                    if (!FriendInfo.IsOnline)
                        break;
                    headImg = null;
                    headImg = new Bitmap(headImgbak);
                    break;
            }

            if (state != null)
            {
                state.Dispose();
                state = null;
            }

            if (headMark != null)
            {
                headMark.Dispose();
                headMark = null;
            }

            if (grap != null)
            {
                grap.Dispose();
                grap = null;
            }

            System.GC.Collect();
        }

    }

    public enum FriendContainerType
    {
        Big,
        Small
    }
}
