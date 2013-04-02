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
    public partial class MiniFriendControl : UserControl
    {
        private XmppClientConnection XmppConn;
        private Jid MJid;

        public int IndexID { set;get;}
        private System.Drawing.Graphics g;
        private Bitmap bgImg;
        private Bitmap headImg;
        private Bitmap headBorder;

        public string NameRmark { set; get; }//备注名称

        private string NickName;//当前显示的名称

        private int x = 3;
        private int y = 3;

        private Bitmap HeadBmp = ResClass.GetImgRes("big1");
        private Friend _friendInfo;
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

        public delegate void SelectedEventHandler(Friend sender);
        public event SelectedEventHandler Selecting;

        public delegate void OpenChatEventHandler(Friend friend, Jid sender, String CName);
        public event OpenChatEventHandler OpenChat;

       
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

            if (HeadBmp != null)
            {
                HeadBmp.Dispose();
                HeadBmp = null;
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

        public MiniFriendControl(XmppClientConnection conn,Jid jid)
        {
            XmppConn = conn;
            MJid = jid;
            NickName = jid.User;

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

            try
            {
                VcardIq viq = new VcardIq(IqType.get, new Jid(jid.Bare));
                XmppConn.IqGrabber.SendIq(viq, new IqCB(VcardResult), null, true);
            }
            catch (Exception)
            {
                
            }
        }

        public Friend FriendInfo
        {
            get
            {
                return _friendInfo;
            }
            set
            {
                _friendInfo = value;
                this.Invalidate();
            }
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
                        headImg = ResClass.GetHead(FriendInfo.HeadIMG);
                    }
                    NickName=vcard.Nickname.Trim()==""?MJid.User:vcard.Nickname.Trim();
    
                }
                else
                {
                    headImg = ResClass.GetHead(FriendInfo.HeadIMG);
                }
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
            if (!FriendInfo.IsSysHead)
            {
                headBorder=new Bitmap(ResClass.GetImgRes("Padding4Normal"), new Size(28, 28));
            }
            else
            {
                headBorder = new Bitmap(ResClass.GetImgRes("Padding4Press"), new Size(28, 28));
            }

            if (headBorder != null)
            {
                g.DrawImage(headBorder, new Rectangle(x-4, y-4, 27, 27), 0, 0, 27, 27, GraphicsUnit.Pixel);
            }
            if (headImg==null)
            {
                headImg = new Bitmap(ResClass.GetImgRes(FriendInfo.HeadIMG), new Size(20, 20));
            }

            if (NameRmark == null)
                g.DrawString(NickName, f, new SolidBrush(Color.FromArgb(240, 0, 0, 0)), 28, 6);
            else
                g.DrawString(NameRmark, f, new SolidBrush(Color.FromArgb(240, 0, 0, 0)), 28, 6);

            g.DrawImage(headImg, new Rectangle(x, y, 20, 20), 0, 0, headImg.Width, headImg.Height, GraphicsUnit.Pixel);
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
                //if (!IsSelected)
                //{
                //    IsSelected = true;
                //    bgImg = ResClass.GetImgRes("friendTitleOpenbg");
                //    headBorder = ResClass.GetImgRes("Padding4Press");
                //    this.Invalidate();
                //    if (Selecting != null)
                //        Selecting(this.FriendInfo);
                //}
            }
            else
            {
                IsSelected = true;
                if (Selecting != null)
                    Selecting(this.FriendInfo);
                //if (ShowContextMenu != null)
                //    ShowContextMenu(this, e);

            }
            System.GC.Collect();
        }

        public new void DoubleClick()
        {
            MouseEventArgs args = new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 0);
            OnMouseDoubleClick(args);
            System.GC.Collect();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (e.Button == MouseButtons.Left)
            {
                if (!IsSelected)
                {
                    //bgImg = ResClass.GetImgRes("friendTitleOpenbg");
                    //headBorder = ResClass.GetImgRes("Padding4Press");
                    this.Invalidate();
                }
                if (OpenChat != null)
                {
                    OpenChat(this.FriendInfo,this.MJid,this.Name);
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
       
        
    }
}
