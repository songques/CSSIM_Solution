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
    public partial class GroupFriendControl : UserControl
    {
        private XmppClientConnection XmppConn;
        private Jid MJid;

        private System.Drawing.Graphics g ;
        private Bitmap bgImg ;
        private Bitmap headImg ;
        private Bitmap headImgbak ;
        private Bitmap headBorder ;
        private string NameRmark = null;
        private string NickName;

        public int x = 8;
        public int y = 8;

        private Bitmap HeadBmp = ResClass.GetImgRes("big1");
        private Font f;

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
            set
            {
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
            }
        }

        public delegate void SelectedEventHandler(Jid sender);
        public event SelectedEventHandler Selecting;

        public delegate void OpenChatEventHandler(Friend sender, string NameRmark);
        public event OpenChatEventHandler OpenChat;

        public delegate void ShowContextMenuEventHandler(GroupFriendControl sender, MouseEventArgs e);
        public event ShowContextMenuEventHandler ShowContextMenu;
        private bool _IsSelect = false;

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

            try
            {
                base.Dispose(disposing);
            }
            catch (System.Exception)
            {

            }
            System.GC.Collect();
        }

        public GroupFriendControl(XmppClientConnection conn,Jid jid)
        {
            XmppConn = conn;
            MJid = jid;
            NickName = MJid.User;

            headImgbak = ResClass.GetImgRes("big194");
            headImg = ResClass.GetImgRes("big194");
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
            UpdateImage();
        }

        public void UpdateImage()
        {
            try
            {
                VcardIq viq = new VcardIq(IqType.get, new Jid(MJid.Bare));
                XmppConn.IqGrabber.SendIq(viq, new IqCB(VcardResult), null, true);
            }
            catch (Exception)
            {
                
            }
            System.GC.Collect();
        }

        private void VcardResult(object sender, IQ iq, object data)
        {
            try
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
                            headImg = new Bitmap(headImgbak);
                        }

                        NickName = vcard.Nickname.Trim()=="" ?MJid.User: vcard.Nickname.Trim();
                        NameRmark = NickName;
                        this.Invalidate();

                    }
                    this.Invalidate();
                    vcard = null;
                    System.GC.Collect();

                }
            }
            catch (Exception)
            {
                
            }
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
            headBorder = ResClass.GetImgRes("Padding4Normal");

            if (headBorder != null)
            {
                g.DrawImage(headBorder, new Rectangle(x - 4, y - 4, HeadWidth + 8, HeadHeight + 8), 0, 0, 48, 48, GraphicsUnit.Pixel);
            }
            g.DrawImage(headImg, new Rectangle(x, y, HeadWidth, HeadHeight), 0, 0, headImg.Width, headImg.Height, GraphicsUnit.Pixel);

            if (NameRmark == null)
                NameRmark = NickName;

            if (FCType == FriendContainerType.Big)
            {
                g.DrawString(NameRmark, f, new SolidBrush(Color.FromArgb(240, 0, 0, 0)), 60, 7);
            }
            else
            {
                g.DrawString(NameRmark, f, new SolidBrush(Color.FromArgb(240, 0, 0, 0)), 40, 7);
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
                        Selecting(MJid);
                }
            }
            else
            {
                IsSelected = true;
                if (Selecting != null)
                    Selecting(MJid);
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
                    Friend friendinfo = new Friend();
                    CSS.IM.XMPP.protocol.iq.roster.RosterItem Ritem = new XMPP.protocol.iq.roster.RosterItem();
                    Ritem.Jid = MJid;
                    friendinfo.Ritem = Ritem;
                    OpenChat(friendinfo, NickName);
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
    }
}
