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
    public partial class ChatHistoryFriend : UserControl
    {

        public enum Dri { L, T, R };

        Dri dri = Dri.T;
        bool isLR = false;

        private System.Drawing.Graphics g;
        private Bitmap bgImg;
        private Bitmap headImg;
        private Bitmap headImgbak;
        private Bitmap headBorder;

        public String TextName{ get;set;}

        public Jid MJID { get;set;}

        public int x = 8;
        public int y = 8;

        private Bitmap HeadBmp = ResClass.GetImgRes("big1");
        private Friend _friendInfo;

        public Friend FriendInfo
        {
            get { return _friendInfo; }
            set
            {
                _friendInfo = value;
                ChangeInfo(_friendInfo, false);
                this.Invalidate();
            }
        }
        private Font f;
        public Timer timer1;
        private IContainer components;


        public delegate void OpenChatEventHandler(Friend sender);
        public event OpenChatEventHandler OpenChat;

        private bool _IsSelect = false;

        public XmppClientConnection XmppConn { set; get; }

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

            if (HeadBmp != null)
            {
                HeadBmp.Dispose();
                HeadBmp = null;
            }

            if (timer1 != null)
            {
                timer1.Enabled=false;
                timer1.Dispose();
                timer1 = null;
            }

            if (f != null)
            {
                f.Dispose();
                f = null;
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

        public ChatHistoryFriend(XmppClientConnection conn)
        {
            InitializeComponent();
            XmppConn = conn;
            headImg = ResClass.GetHead("big194");

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

        public void UpdateImage()
        {
            VcardIq viq = new VcardIq(IqType.get,MJID);
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
                        headImg = new Bitmap(photo.BitImage, new Size(40, 40));
                    }
                    else
                    {
                        headImg = ResClass.GetHead("big194");
                    }
                }
                else
                {
                    headImg = ResClass.GetHead("big194");
                }
                headImgbak = new Bitmap(headImg);
            }
            ChangeInfo(_friendInfo, false);
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
            //if (!FriendInfo.IsSysHead)
            //    headBorder = ResClass.GetImgRes("Padding4Normal");
            //else
            //    headBorder = ResClass.GetImgRes("Padding4Press");

            if (headBorder != null)
            {
                g.DrawImage(headBorder, new Rectangle(x-4, y-4, 48, 48), 0, 0, 48, 48, GraphicsUnit.Pixel);
            }

            
            g.DrawImage(headImg, new Rectangle(x, y, 40, 40), 0, 0, 40, 40, GraphicsUnit.Pixel);
            g.DrawString(MJID.User, f, new SolidBrush(Color.FromArgb(240, 0, 0, 0)), 60, 7);
            //g.DrawString(FriendInfo.Description, f, new SolidBrush(Color.FromArgb(100, 0, 0, 0)), 60, 28);
            System.GC.Collect();

        }

        private void timer1_Tick(object sender, EventArgs e)
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
            //if (e.Button == MouseButtons.Left)
            //{
            //    if (!IsSelected)
            //    {
            //        IsSelected = true;
            //        bgImg = ResClass.GetImgRes("friendTitleOpenbg");
            //        headBorder = ResClass.GetImgRes("Padding4Press");
            //        this.Invalidate();
            //        //if (Selecting != null)
            //            //Selecting(this.FriendInfo);
            //    }
            //}
            //else
            //{
            //    IsSelected = true;
            //    //if (Selecting != null)
            //        //Selecting(this.FriendInfo);
            //    if (ShowContextMenu != null)
            //        ShowContextMenu(this, e);

            //}
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
                    OpenChat(FriendInfo);
                    timer1.Enabled = false;
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

        public void SelectCurrent(bool flag)
        {
            if (flag)
            {
                OnMouseClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            }
            else
            {
                OnMouseLeave(null);
            }
            System.GC.Collect();
        }

        public void Call(int Jsec)
        {
            if (Jsec % 2 == 0)
            {
                x = 8;
                y = 6;
            }
            else
            {
                x = 7;
                y = 5;
            }
            this.Invalidate(new Rectangle(7, 5, 41, 41));
            System.GC.Collect();
        }

        internal void Selected()
        {
            OnMouseClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            System.GC.Collect();
        }

        public bool IsSelected
        {
            get
            {
                return _IsSelect;
            }
            set
            {
                _IsSelect = value;
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FriendControl
            // 
            this.Name = "FriendControl";
            this.ResumeLayout(false);
            System.GC.Collect();

        }

        #region 在线或不在线制作黑白头像
        private void ChangeInfo(Friend friend, bool isX)
        {
            #region 制作黑白色的头像
            Bitmap OldHeadImg = null;
            if (isX)
            {
                OldHeadImg = ResClass.GetHead(friend.HeadIMG);
            }
            else
            {
                try
                {
                    OldHeadImg = new Bitmap(headImgbak);
                }
                catch (Exception)
                {
                    OldHeadImg = ResClass.GetHead(friend.HeadIMG);
                }

            }

            try
            {
                int Height = OldHeadImg.Height;
                int Width = OldHeadImg.Width;
                if (!FriendInfo.IsOnline)
                {
                    headImg = new Bitmap(Width, Height);
                    Color pixel;
                    for (int x = 0; x < Width; x++)
                        for (int y = 0; y < Height; y++)
                        {
                            pixel = OldHeadImg.GetPixel(x, y);
                            int r, g, b, Result = 0;
                            r = pixel.R;
                            g = pixel.G;
                            b = pixel.B;
                            //实例程序以加权平均值法产生黑白图像
                            int iType = 2;
                            switch (iType)
                            {
                                case 0://平均值法
                                    Result = ((r + g + b) / 3);
                                    break;
                                case 1://最大值法
                                    Result = r > g ? r : g;
                                    Result = Result > b ? Result : b;
                                    break;
                                case 2://加权平均值法
                                    Result = ((int)(0.7 * r) + (int)(0.2 * g) + (int)(0.1 * b));
                                    break;
                            }
                            headImg.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                        }

                }
                else
                {
                    headImg = new Bitmap(headImgbak);
                }
            }
            catch (Exception) { }
            #endregion

            Bitmap state = null;
            Bitmap headMark = null;
            System.Drawing.Graphics grap = null;
            switch (friend.State)
            {
                case 0:
                    if (!FriendInfo.IsOnline)
                        break;
                    headImg = new Bitmap(headImgbak);
                    break;
                case 1:
                    break;
                case 2:
                    if (!FriendInfo.IsOnline)
                        break;
                    state = new Bitmap(ResClass.GetImgRes("away"), new Size(11, 11));
                    headMark = headImg;
                    grap = System.Drawing.Graphics.FromImage(headMark);
                    grap.DrawImage(state, 28, 28);
                    headImg = headMark;
                    break;
                case 3:
                    break;
                case 4:
                    if (!FriendInfo.IsOnline)
                        break;
                    state = new Bitmap(ResClass.GetImgRes("busy"), new Size(11, 11));
                    headMark = headImg;
                    grap = System.Drawing.Graphics.FromImage(headMark);
                    grap.DrawImage(state, 28, 28);
                    headImg = headMark;
                    break;
                default:
                    if (!FriendInfo.IsOnline)
                        break;
                    headImg = new Bitmap(headImgbak);
                    break;
            }
            System.GC.Collect();
        }
        #endregion


    }
}
