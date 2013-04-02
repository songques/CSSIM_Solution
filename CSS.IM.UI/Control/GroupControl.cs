using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Entity;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    public class GroupControl : UserControl
    {
        private System.Drawing.Graphics g;
        private Bitmap Bmp;
        private Group _groupInfo;

        public delegate void ExpandChangeEventHandler(Group sender, bool Currentstate);
        public event ExpandChangeEventHandler ExpandChanged;

        public delegate void ShowContextMenuEventHandler(GroupControl sender, MouseEventArgs e);
        public event ShowContextMenuEventHandler ShowContextMenu;
        
        public Group GroupInfo
        {
            get
            {
                return _groupInfo;
            }
            set
            {
                _groupInfo = value;
                this.Invalidate();
            }
        }

        private Font f;
        private Bitmap bgImg;
        private bool _IsExpand;

        protected override void Dispose(bool disposing)
        {
            if (bgImg != null)
            {
                bgImg.Dispose();
                bgImg = null;
            }

            if (_groupInfo != null)
            {
                _groupInfo = null;
            }


            if (f != null)
            {
                f.Dispose();
                f = null;
            }

            if (bgImg != null)
            {
                bgImg.Dispose();
                bgImg = null;
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

        public GroupControl()
        {
            this.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            this.BackColor = Color.Transparent;
            this.Height = 22;
            this.TabStop = true;
            Bmp = ResClass.GetImgRes("MainPanel_FolderNode_collapseTexture");
            if (IsExpand)
            {
                Bmp = ResClass.GetImgRes("MainPanel_FolderNode_expandTexture");
            }
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
            base.OnPaint(e);
            g = e.Graphics;
            if (bgImg != null)
            {
                g.DrawImage(bgImg, new Rectangle(1, 0, this.Width-4, 1), 1, 0, bgImg.Width - 4, 1, GraphicsUnit.Pixel);
                g.DrawImage(bgImg, new Rectangle(0, 1, 1, this.Height - 2), 0, 1, 1, bgImg.Height - 2, GraphicsUnit.Pixel);
                g.DrawImage(bgImg, new Rectangle(1, 1, this.Width - 3, this.Height - 2), 1, 1, bgImg.Width - 3, bgImg.Height - 2, GraphicsUnit.Pixel);
                g.DrawImage(bgImg, new Rectangle(this.Width - 1, 1, 1, this.Height - 2), bgImg.Width - 1, 1, 1, bgImg.Height - 2, GraphicsUnit.Pixel);
                g.DrawImage(bgImg, new Rectangle(1, this.Height - 1, this.Width - 4, 1), 1, bgImg.Height - 1, bgImg.Width - 4, 1, GraphicsUnit.Pixel);
            }
            g.DrawImage(Bmp, new Rectangle(3, 5, 12, 12), 0, 0, 12, 12, GraphicsUnit.Pixel);
            g.DrawString(GroupInfo.Title + " [" + GroupInfo.OnlineCount + "/" + GroupInfo.Count + "]", f,Brushes.Black, 20, 3);
            System.GC.Collect();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Bmp = ResClass.GetImgRes("MainPanel_FolderNode_collapseTextureHighlight");
            if (IsExpand)
            {
                Bmp = ResClass.GetImgRes("MainPanel_FolderNode_expandTextureHighlight");
            }
            else 
            {
                bgImg = ResClass.GetImgRes("main_group_normal");
            }
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Bmp = ResClass.GetImgRes("MainPanel_FolderNode_collapseTexture");
            if (IsExpand)
            {
                Bmp = ResClass.GetImgRes("MainPanel_FolderNode_expandTexture");
            }
            bgImg = null;
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left)
            {
                if (!IsExpand)
                    IsExpand = true;
                else 
                    IsExpand = false;
                if (ExpandChanged != null)
                    ExpandChanged(GroupInfo, IsExpand);
                this.Invalidate();
            }
            else
            {
                if (ShowContextMenu != null)
                    ShowContextMenu(this, e);
            }
            System.GC.Collect();
        }

        public bool IsExpand
        {
            get
            {
                return _IsExpand;
            }
            set
            {
                _IsExpand = value;
                if (_IsExpand)
                {
                    Bmp = ResClass.GetImgRes("MainPanel_FolderNode_expandTextureHighlight");
                }
                else
                {
                    Bmp = ResClass.GetImgRes("MainPanel_FolderNode_collapseTextureHighlight");
                }
                if(Path.FolderSwitch)
                    SoundPlayEx.MsgPlay(Path.FolderPath);
                bgImg = null;
                this.Invalidate();
            }
        }
    }
}
