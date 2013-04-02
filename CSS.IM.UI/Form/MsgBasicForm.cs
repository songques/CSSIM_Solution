using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Form
{
    public partial class MsgBasicForm : System.Windows.Forms.Form
    {
        #region 参数
        private Graphics g = null;
        private Bitmap Bmp = null;
        private Bitmap closeBmp = null;
        private Bitmap minBmp = null;
        private Bitmap maxBmp = null;
        private Brush titleColor = Brushes.Black;

        private int xwidth;
        private int strX = 30;
        private Icon icon = null;
        private bool isOneLoad=true;
        private bool _allowResize = true;
        private bool _allowMove = true;
        #endregion

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //if (icon != null)
            //{
            //    icon.Dispose();
            //    icon = null;
            //}

            //if (titleColor != null)
            //{
            //    titleColor.Dispose();
            //    titleColor = null;
            //}

            //if (maxBmp != null)
            //{
            //    maxBmp.Dispose();
            //    maxBmp = null;
            //}

            //if (minBmp != null)
            //{
            //    minBmp.Dispose();
            //    minBmp = null;
            //}

            //if (closeBmp != null)
            //{
            //    closeBmp.Dispose();
            //    closeBmp = null;
            //}

            //if (g != null)
            //{
            //    g.Dispose();
            //    g = null;
            //}


            //if (Bmp != null)
            //{
            //    Bmp.Dispose();
            //    Bmp = null;
            //}

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);

            //System.GC.Collect();
        }

        #region 初始化

        public MsgBasicForm()
        {
            InitializeComponent();
            InitControl();
        }

        private void InitControl() 
        {
            icon = new Icon(this.Icon, 16, 16);
            MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            closeBmp = ResClass.GetImgRes("btn_close_normal");
            minBmp = ResClass.GetImgRes("btn_mini_normal");
            maxBmp = ResClass.GetImgRes("btn_max_normal");
            if (this.WindowState == FormWindowState.Maximized)
            {
                maxBmp = ResClass.GetImgRes("btn_restore_normal");
            }
        }

        #endregion

        #region 系统事件

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            TaskMenu.ShowSYSMENU(this);
            if (this.ControlBox)
            {
                this.ButtonClose.Visible = true;
                if (!this.MinimizeBox)
                    this.ButtonMin.Visible = false;
                else
                    this.ButtonMin.Visible = true;
                if (!this.MaximizeBox)
                    this.ButtonMax.Visible = false;
                else
                    this.ButtonMax.Visible = true;
            }
            else
            {
                this.ButtonClose.Visible = false;
                this.ButtonMin.Visible = false;
                this.ButtonMax.Visible = false;
            }
            ResizeControl();
            int Rgn = Win32.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 5, 5);
            Win32.SetWindowRgn(this.Handle, Rgn, true);
            isOneLoad = false;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            titleColor = Brushes.Black;
            this.Invalidate(new Rectangle(strX, 7, xwidth - 26, 31));
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            titleColor = Brushes.WhiteSmoke;
            this.Invalidate(new Rectangle(strX, 7, xwidth - 26, 31));
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate(new Rectangle(strX, 7, xwidth - 26, 31));
        }

        protected override void OnPaint(PaintEventArgs e)              
        {
            base.OnPaint(e);
            g = e.Graphics;
            Bmp = ResClass.GetImgRes("login_png_bkg");
            g.DrawImage(Bmp, new Rectangle(0, 0, 5, 31), 0, 0, 5, 31, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(5, 0, this.Width - 10, 31), 5, 0, Bmp.Width - 10, 31, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, 0, 5, 31), Bmp.Width - 5, 0, 5, 31, GraphicsUnit.Pixel);

            g.DrawImage(Bmp, new Rectangle(0, 31, 2, 25), 0, 31, 2, 25, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(2, 31, this.Width - 4, 25), 2, 31, 5, 25, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 2, 31, 2, 25), Bmp.Width - 2, 31, 2, 25, GraphicsUnit.Pixel);

            

            g.DrawImage(Bmp, new Rectangle(0, 56, 5, Height - 2), 0, Bmp.Height - 230, 5, 130, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(5, 56, this.Width - 10, Height - 2), 5, Bmp.Height - 230, Bmp.Width - 10, 130, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, 56, 5, Height - 2), Bmp.Width - 5, Bmp.Height - 230, 5, 130, GraphicsUnit.Pixel);

            g.DrawImage(Bmp, new Rectangle(0, this.Height -2, 5, 2), 0, Bmp.Height - 2, 5, 2, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(5, this.Height - 2, this.Width - 10, 2), 5, Bmp.Height - 2, Bmp.Width - 10, 2, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, this.Height - 2, 5, 2), Bmp.Width - 5, Bmp.Height - 2, 5, 2, GraphicsUnit.Pixel);

            if (this.ShowIcon)
            {
                g.DrawIcon(icon, new Rectangle(8, 7, 16, 16));
            }
            else 
            {
                strX = 10;
            }
            //Bmp = ResClass.GetResObj("MainPanel_TitleBackgroundBluelight_background");
            Font f = null;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                f = new Font("微软雅黑", 11F, FontStyle.Regular);
                g.DrawString(this.Text, f, titleColor, strX, 4);
                //g.DrawImage(Bmp, new Rectangle(strX, 4, (int)SkinUtil.GetStrWidth(Text, f), 20), 0, 0, Bmp.Width, Bmp.Height, GraphicsUnit.Pixel);
            }
            else 
            {
                f = new Font("宋体", 11F, FontStyle.Regular);
                g.DrawString(this.Text, f, titleColor, strX, 8);
            }
            f.Dispose();
            Bmp.Dispose();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.WM_NCPAINT:
                    break;
                case Win32.WM_NCACTIVATE:
                    if (m.WParam == (IntPtr)0)
                    {
                        m.Result = (IntPtr)1;
                    }
                    if (m.WParam == (IntPtr)2097152)
                    {
                        m.Result = (IntPtr)1;
                    }
                    break;
                case Win32.WM_NCCALCSIZE:
                    break;
                case Win32.WM_NCHITTEST:
                    Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (AllowResize && MaximizeBox)
                    {
                        if (vPoint.X <= 3)
                        {
                            if (vPoint.Y <= 3)
                                m.Result = (IntPtr)Win32.HTTOPLEFT;
                            else if (vPoint.Y >= Height - 3)
                                m.Result = (IntPtr)Win32.HTBOTTOMLEFT;
                            else m.Result = (IntPtr)Win32.HTLEFT;
                        }
                        else if (vPoint.X >= Width - 3)
                        {
                            if (vPoint.Y <= 3)
                                m.Result = (IntPtr)Win32.HTTOPRIGHT;
                            else if (vPoint.Y >= Height - 3)
                                m.Result = (IntPtr)Win32.HTBOTTOMRIGHT;
                            else m.Result = (IntPtr)Win32.HTRIGHT;
                        }
                        else if (vPoint.Y <= 3)
                        {
                            m.Result = (IntPtr)Win32.HTTOP;
                        }
                        else if (vPoint.Y >= Height - 3)
                            m.Result = (IntPtr)Win32.HTBOTTOM;
                    }
                    if (AllowMove)
                    {
                        if (vPoint.Y < Height - 3 && vPoint.Y > 2 && (vPoint.X > 3 && vPoint.X < Width - 3))
                            m.Result = (IntPtr)Win32.HTCAPTION;
                    }
                    else
                    {
                        if (vPoint.Y < 31 && vPoint.Y > 2 && (vPoint.X > 3 && vPoint.X < Width - 3))
                            m.Result = (IntPtr)Win32.HTCAPTION;
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
            
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!isOneLoad)
            {
                if (this.WindowState != FormWindowState.Maximized)
                {
                    int Rgn = Win32.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 5, 5);
                    Win32.SetWindowRgn(this.Handle, Rgn, true);
                }
                else
                {
                    int Rgn = Win32.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 0, 0);
                    Win32.SetWindowRgn(this.Handle, Rgn, true);
                }
            }
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            ResizeControl();
            this.Invalidate();
        }

        public new void Show(IWin32Window owner) 
        {
            base.Show(owner);
            this.BackColor = ((System.Windows.Forms.Form)owner).BackColor;
            this.BackgroundImage = ((System.Windows.Forms.Form)owner).BackgroundImage;
        }

        #endregion

        #region 其他操作

        private void ResizeControl()
        {
            if (this.ControlBox)
            {
                this.ButtonClose.Left = this.Width - ButtonClose.Width;
            }

            if (this.MinimizeBox)
            {
                if (this.MaximizeBox)
                {
                    this.ButtonMax.Left = ButtonClose.Left - ButtonMax.Width;
                    this.ButtonMin.Left = ButtonMax.Left - ButtonMin.Width;
                    xwidth = ButtonMin.Left;
                }
                else
                {
                    this.ButtonMin.Left = ButtonClose.Left - ButtonMin.Width;
                    xwidth = ButtonMin.Left;
                }
                //colour.Left = ButtonMin.Left - 25;
            }
            else
            {
                //colour.Left = ButtonClose.Left - 25;
                xwidth = ButtonClose.Left;
            }
            //colour.Visible = ShowColorButton;
            //color_Btn.Left = Width - color_Btn.Width - 3;
        }

        #endregion

        #region 辅助函数
        private Bitmap GetImg(string ImgName)
        {
            Image srcImg = ResClass.GetImgRes(ImgName);
            return GetImg(ImgName, srcImg.Width, srcImg.Height, 0, 0, srcImg.Width, srcImg.Height);
        }

        private Bitmap GetImg(string ImgName, int i)
        {
            Image srcImg = ResClass.GetImgRes(ImgName);
            return GetImg(ImgName, srcImg.Width / 4, srcImg.Height, srcImg.Width / 4 * i, 0, srcImg.Width / 4, srcImg.Height);
        }

        private Bitmap GetImg(string ImgName, int width, int height, int sleft, int stop, int sWidth, int sHeight)
        {
            Image srcImg = ResClass.GetImgRes(ImgName);

            Bitmap newImg = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(newImg);

            graphics.DrawImage(srcImg, new Rectangle(0, 0, width, height), (sleft < 0 ? srcImg.Width + sleft : sleft), (stop < 0 ? srcImg.Height + stop : stop), (sWidth < 0 ? srcImg.Width + sWidth : sWidth), (sHeight < 0 ? srcImg.Height + sHeight : sHeight), GraphicsUnit.Pixel);
            graphics.Dispose();
            srcImg.Dispose();
            return newImg;
        }
        #endregion

        #region 控制按钮实现

        private void ButtonClose_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.Close();
        }

        private void ButtonClose_MouseDown(object sender, MouseEventArgs e)
        {
            closeBmp = ResClass.GetImgRes("btn_close_down");
            ButtonClose.Invalidate();
        }

        private void ButtonClose_MouseUp(object sender, MouseEventArgs e)
        {
            if (!ButtonClose.IsDisposed)
            {
                closeBmp = ResClass.GetImgRes("btn_close_normal");
                ButtonClose.Invalidate();
            }
        }

        private void ButtonClose_MouseLeave(object sender, EventArgs e)
        {
            closeBmp = ResClass.GetImgRes("btn_close_normal");
            ButtonClose.Invalidate();
        }

        private void ButtonClose_MouseEnter(object sender, EventArgs e)
        {
            closeBmp = ResClass.GetImgRes("btn_close_highlight");
            toolTip1.SetToolTip(ButtonClose, "关闭");
            ButtonClose.Invalidate();
        }

        private void ButtonClose_Paint(object sender, PaintEventArgs e)
        {
            if (this.ControlBox)
            {
                g = e.Graphics;
                g.DrawImage(closeBmp, new Rectangle(0, 0, closeBmp.Width, closeBmp.Height), 0, 0, closeBmp.Width, closeBmp.Height, GraphicsUnit.Pixel);
            }
        }

        private void ButtonMin_MouseEnter(object sender, EventArgs e)
        {
            minBmp = ResClass.GetImgRes("btn_mini_highlight");
            toolTip1.SetToolTip(ButtonMin, "最小化");
            ButtonMin.Invalidate();
        }

        private void ButtonMin_MouseDown(object sender, MouseEventArgs e)
        {
            minBmp = ResClass.GetImgRes("btn_mini_down");
            ButtonMin.Invalidate();
        }

        private void ButtonMin_MouseUp(object sender, MouseEventArgs e)
        {
            minBmp = ResClass.GetImgRes("btn_close_normal");
            ButtonMin.Invalidate();
        }

        private void ButtonMin_MouseLeave(object sender, EventArgs e)
        {
            minBmp = ResClass.GetImgRes("btn_mini_normal");
            ButtonMin.Invalidate();
        }

        private void ButtonMin_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!this.ShowInTaskbar)
                {
                    this.Hide();
                }
                else
                {
                    this.WindowState = FormWindowState.Minimized;
                }
            }
        }

        private void ButtonMin_Paint(object sender, PaintEventArgs e)
        {
            if (this.MinimizeBox)
            {
                g = e.Graphics;
                g.DrawImage(minBmp, new Rectangle(0, 0, minBmp.Width, minBmp.Height), 0, 0, minBmp.Width, minBmp.Height, GraphicsUnit.Pixel);
            }
        }

        private void ButtonMax_MouseDown(object sender, MouseEventArgs e)
        {
            g = ButtonMax.CreateGraphics();
            maxBmp = ResClass.GetImgRes("btn_max_down");
            if (this.WindowState == FormWindowState.Maximized)
            {
                maxBmp = ResClass.GetImgRes("btn_restore_down");
            }
            g.DrawImage(maxBmp, new Rectangle(0, 0, maxBmp.Width, maxBmp.Height), 0, 0, maxBmp.Width, maxBmp.Height, GraphicsUnit.Pixel);
        }

        private void ButtonMax_MouseEnter(object sender, EventArgs e)
        {
            g = ButtonMax.CreateGraphics();
            maxBmp = ResClass.GetImgRes("btn_max_highlight");
            toolTip1.SetToolTip(ButtonMax, "最大化");
            if (this.WindowState == FormWindowState.Maximized)
            {
                maxBmp = ResClass.GetImgRes("btn_restore_highlight");
                toolTip1.SetToolTip(ButtonMax, "还原");
            }
            g.DrawImage(maxBmp, new Rectangle(0, 0, maxBmp.Width, maxBmp.Height), 0, 0, maxBmp.Width, maxBmp.Height, GraphicsUnit.Pixel);
        }

        private void ButtonMax_MouseLeave(object sender, EventArgs e)
        {
            maxBmp = ResClass.GetImgRes("btn_max_normal");
            if (this.WindowState == FormWindowState.Maximized)
            {
                maxBmp = ResClass.GetImgRes("btn_restore_normal");
            }
            ButtonMax.Invalidate();
        }

        private void ButtonMax_MouseUp(object sender, MouseEventArgs e)
        {
            maxBmp = ResClass.GetImgRes("btn_max_normal");
            if (this.WindowState == FormWindowState.Maximized)
            {
                maxBmp = ResClass.GetImgRes("btn_restore_normal");
            }
            ButtonMax.Invalidate();
        }

        private void ButtonMax_Paint(object sender, PaintEventArgs e)
        {
            if (this.MaximizeBox)
            {
                g = e.Graphics;
                maxBmp = ResClass.GetImgRes("btn_max_normal");
                if (this.WindowState == FormWindowState.Maximized)
                {
                    maxBmp = ResClass.GetImgRes("btn_restore_normal");
                }
                g.DrawImage(maxBmp, new Rectangle(0, 0, maxBmp.Width, maxBmp.Height), 0, 0, maxBmp.Width, maxBmp.Height, GraphicsUnit.Pixel);
                maxBmp.Dispose();
            }
        }

        private void ButtonMax_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Normal)
                    this.WindowState = FormWindowState.Maximized;
                else
                    this.WindowState = FormWindowState.Normal;
                this.Invalidate();
            }
        }

        #endregion

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox p = sender as PictureBox;
                g = p.CreateGraphics();
                g.Clear(this.BackColor);
                Bmp = ResClass.GetImgRes("allbtn_down");
                g.DrawImage(Bmp, new Rectangle(0, 0, 22, 22), 0, 0, 22, 22, GraphicsUnit.Pixel);
                g.DrawImage(p.Image, new Rectangle(2, 3, 18, 18), 0, 0, 18, 18, GraphicsUnit.Pixel);
                Bmp.Dispose();
                g.Dispose();
            }
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ((PictureBox)sender).Invalidate();
            }
        }

        [Description("允许用户拖动边框调整大小"), Category("Appearance")]
        public bool AllowResize 
        {
            get 
            {
                return _allowResize;
            } 
            set
            {
                _allowResize = value;
            } 
        }

        [Description("允许用户拖动任意处移动窗口"), Category("Appearance")]
        public bool AllowMove
        {
            get
            {
                return _allowMove;
            }
            set
            {
                _allowMove = value;
            }
        }
    }
}
