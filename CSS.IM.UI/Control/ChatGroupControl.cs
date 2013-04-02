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
    public partial class ChatGroupControl : UserControl
    {
        private Jid MJid;

        private System.Drawing.Graphics g;
        private Bitmap bgImg;
        private Bitmap headImg = ResClass.GetImgRes("big199");
        private Bitmap headBorder;

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
                //if (FriendInfo != null)
                //    ChangeInfo(FriendInfo);

            }
        }

        private String NickName;
       
        private int x = 8;
        private int y = 8;

        private Font f;

        private bool _IsSelect = false;
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
        
        /// <summary>
        /// 打开聊天会议室事件
        /// </summary>
        public delegate void ChatGroupOpenDelegate(Jid jid);
        public event ChatGroupOpenDelegate ChatGroupOpenEvent;

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
            if (headBorder != null)
            {
                headBorder.Dispose();
                headBorder = null;
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

        public ChatGroupControl(Jid jid)
        {
            MJid = jid;
            NickName = MJid.Bare;
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
            else
                headBorder = ResClass.GetImgRes("Padding4Press");

            if (headBorder != null)
            {
                g.DrawImage(headBorder, new Rectangle(4, 4, HeadWidth + 8, HeadWidth+8), 0, 0, 48, 48, GraphicsUnit.Pixel);
            }

            g.DrawImage(headImg, new Rectangle(x, y, HeadWidth, HeadWidth), 0, 0, headImg.Width, headImg.Height, GraphicsUnit.Pixel);

            if (FCType == FriendContainerType.Big)
            {
                g.DrawString(NickName, f, new SolidBrush(Color.FromArgb(240, 0, 0, 0)), 60, 7);
            }
            else
            {
                g.DrawString(NickName, f, new SolidBrush(Color.FromArgb(240, 0, 0, 0)), 40, 7);
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
            //if (e.Button == MouseButtons.Left)
            //{
            //    if (!IsSelected)
            //    {
            //        IsSelected = true;
            //        bgImg = ResClass.GetImgRes("friendTitleOpenbg");
            //        headBorder = ResClass.GetImgRes("Padding4Press");
            //        this.Invalidate();
            //    }
            //}
            //else
            //{
            //    IsSelected = true;
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
                if (ChatGroupOpenEvent!=null)
                {
                    ChatGroupOpenEvent(MJid);
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
    }
}
