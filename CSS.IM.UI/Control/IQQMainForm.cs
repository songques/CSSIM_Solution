using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using CSS.IM.UI.Util;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace CSS.IM.UI.Control
{
    public partial class IQQMainForm : System.Windows.Forms.Form
    {

        public delegate void CloseEventDelegate();
        public event CloseEventDelegate CloseEvent;

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point Point);
        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        #region 参数
        private System.Drawing.Graphics g = null;
        private Bitmap Bmp = null;
        private Bitmap HeadBmp = null;
        private ImageAttributes imageAttr;
        private Bitmap closeBmp = null;
        private Bitmap minBmp = null;
        private Bitmap maxBmp = null;
        private Brush titleColor = Brushes.Black;

        private int xwidth;
        private bool isOneLoad = true;
        private bool miniMode = false;
        private Size oldSize;
        private List<string> shadlist = null;
        private Font f = null;

        private string _NikeName = "";
        public string NikeName
        {
            get { return _NikeName; }
            set { _NikeName = value; this.Invalidate(); }
        }

        #endregion

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (f != null)
            {
                f.Dispose();
                f = null;
            }

            if (titleColor != null)
            {
                titleColor.Dispose();
                titleColor = null;
            }

            if (HeadBmp != null)
            {
                HeadBmp.Dispose();
                HeadBmp = null;

            }

            if (maxBmp != null)
            {
                maxBmp.Dispose();
                maxBmp = null;
            }

            if (minBmp != null)
            {
                minBmp.Dispose();
                minBmp = null;
            }

            if (closeBmp != null)
            {
                closeBmp.Dispose();
                closeBmp = null;
            }

            if (Bmp != null)
            {
                Bmp.Dispose();
                Bmp = null;
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

            base.Dispose(disposing);

            System.GC.Collect();
        }


        public IQQMainForm()
        {

            if (Environment.OSVersion.Version.Major >= 6)
            {
                this.Font = new Font("微软雅黑", 9F, FontStyle.Regular);
            }
            else
            {
                this.Font = new Font("宋体", 9F, FontStyle.Regular);
            }
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            f = new Font("Tahoma", 9F, FontStyle.Bold);
            InitializeComponent();
            InitControl();
        }

        private void InitControl()
        {
            MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            closeBmp = ResClass.GetImgRes("btn_close_normal");
            minBmp = ResClass.GetImgRes("btn_mini_normal");
            maxBmp = ResClass.GetImgRes("btn_max_normal");
            if (this.WindowState == FormWindowState.Maximized)
            {
                maxBmp = ResClass.GetImgRes("btn_restore_normal");
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.FormBorderStyle != FormBorderStyle.None)
                TaskMenu.InitSYSMENU(this);
            else
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

            shadlist = new List<string>(14);
            shadlist.Add("pic_defaultcolor_normal");
            //if (Directory.Exists(Application.StartupPath + "\\skins"))
            //{
            //    string[] sd = Directory.GetDirectories(Application.StartupPath + "\\skins");
            //    for (int i = 0; i < sd.Length; i++)
            //    {
            //        string[] sf = Directory.GetFiles(sd[i], "main*");
            //        if (sf.Length > 0)
            //        {
            //            shadlist.Add(sd[i]);
            //        }
            //    } 
            //}
            //LoadShadList();
            //LoadColorList();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            titleColor = Brushes.Black;
            this.Invalidate(new Rectangle(26, 7, xwidth - 26, 31));
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            titleColor = Brushes.Black;
            this.Invalidate(new Rectangle(26, 7, xwidth - 26, 31));
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate(new Rectangle(26, 7, xwidth - 26, 31));
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            //if (miniMode && WindowState == FormWindowState.Normal)
            //miniTimer.Enabled = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            //if (this.Top == 0 && WindowState == FormWindowState.Normal)
            //{
            //    Point p = Control.MousePosition;
            //    if (p.X < this.Left || p.X > this.Left + this.Width || p.Y > this.Top + this.Height)
            //        miniTimer.Enabled = true;
            //}
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //搜索框

            g = e.Graphics;
            Bmp = ResClass.GetImgRes("main_png_bkg");
            g.DrawImage(Bmp, new Rectangle(0, 0, 5, 121), 5, 5, 5, 121, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(5, 0, this.Width - 10, 121), 10, 5, Bmp.Width - 20, 121, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, 0, 5, 121), Bmp.Width - 10, 5, 5, 121, GraphicsUnit.Pixel);

            g.DrawImage(Bmp, new Rectangle(0, 121, 2, this.Height - 156), 5, 126, 2, Bmp.Height - 192, GraphicsUnit.Pixel);
            //g.FillRectangle(Brushes.White, 2, 121, this.Width - 4, this.Height - 182);
            //g.DrawImage(Bmp, new Rectangle(2, 121, this.Width - 4, this.Height - 182), 7, 126, Bmp.Width - 14, Bmp.Height - 192, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 2, 121, 2, this.Height - 156), Bmp.Width - 7, 126, 2, Bmp.Height - 192, GraphicsUnit.Pixel);

            g.DrawImage(Bmp, new Rectangle(0, this.Height - 35, 5, 35), 5, Bmp.Height - 40, 5, 35, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(5, this.Height - 35, this.Width - 10, 35), 10, Bmp.Height - 40, Bmp.Width - 20, 35, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, this.Height - 35, 5, 35), Bmp.Width - 10, Bmp.Height - 40, 5, 35, GraphicsUnit.Pixel);


            if (HeadBmp == null)
            {
                HeadBmp = ResClass.GetImgRes("ChatFrame_Window_windowBkg");
                imageAttr = new ImageAttributes();
                imageAttr.SetColorKey(Bmp.GetPixel(1, 1), Bmp.GetPixel(1, 1));
            }


            g.DrawImage(HeadBmp, new Rectangle(0, 0, 50, 100), 0, 0, 50, 100, GraphicsUnit.Pixel, imageAttr);
            g.DrawImage(HeadBmp, new Rectangle(50, 0, this.Width - 120, 100), 50, 0, HeadBmp.Width - 120, 100, GraphicsUnit.Pixel);
            g.DrawImage(HeadBmp, new Rectangle(this.Width - 80, 0, 80, 100), HeadBmp.Width - 80, 0, 80, 100, GraphicsUnit.Pixel, imageAttr);


            g.DrawString(this.Text, f, titleColor, 10, 10);
            g.DrawString(NikeName, new Font(Font.FontFamily, 10F, FontStyle.Bold), titleColor, 90, 34);



            /*Bmp = ResClass.GetImgRes("main_search_bkg");
            g.DrawImage(Bmp, new Rectangle(2, 99, 9, Bmp.Height), 0, 0, 9, Bmp.Height, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(11, 99, this.Width - 22, Bmp.Height), 9, 0, Bmp.Width - 18, Bmp.Height, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 11, 99, 9, Bmp.Height), Bmp.Width - 9, 0, 9, Bmp.Height, GraphicsUnit.Pixel);*/
        }

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
            }
            else
            {
                xwidth = ButtonClose.Left;
            }
        }

        public AnchorStyles anchors;
        const int OFFSET = 6;
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TOPMOST = 8;
                base.CreateParams.Parent = GetDesktopWindow();
                base.CreateParams.ExStyle |= WS_EX_TOPMOST;
                return base.CreateParams;
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_MOVING = 534;
            switch (m.Msg)
            {
                case WM_MOVING:
                    int left = Marshal.ReadInt32(m.LParam, 0);
                    int top = Marshal.ReadInt32(m.LParam, 4);
                    int right = Marshal.ReadInt32(m.LParam, 8);
                    int bottom = Marshal.ReadInt32(m.LParam, 12);
                    left = Math.Min(Math.Max(0, left), Screen.PrimaryScreen.Bounds.Width - Width);
                    top = Math.Min(Math.Max(0, top), Screen.PrimaryScreen.Bounds.Height - Height);
                    right = Math.Min(Math.Max(Width, right), Screen.PrimaryScreen.Bounds.Width);
                    bottom = Math.Min(Math.Max(Height, bottom), Screen.PrimaryScreen.Bounds.Height);
                    Marshal.WriteInt32(m.LParam, 0, left);
                    Marshal.WriteInt32(m.LParam, 4, top);
                    Marshal.WriteInt32(m.LParam, 8, right);
                    Marshal.WriteInt32(m.LParam, 12, bottom);
                    anchors = AnchorStyles.None;
                    if (left <= OFFSET)
                    {
                        anchors |= AnchorStyles.Left;
                    }
                    if (top <= OFFSET)
                    {
                        anchors |= AnchorStyles.Top;
                    }
                    if (bottom >= Screen.PrimaryScreen.Bounds.Height - OFFSET)
                    {
                        anchors |= AnchorStyles.Bottom;
                    }
                    if (right >= Screen.PrimaryScreen.Bounds.Width - OFFSET)
                    {
                        anchors |= AnchorStyles.Right;
                    }
                    timer_autoHide.Enabled = anchors != AnchorStyles.None;

                    break;
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
                case Win32.WM_NCLBUTTONDOWN:
                    //description.Focus();
                    //skinPanel_Leave(null, null);
                    base.WndProc(ref m);
                    break;
                case Win32.WM_LBUTTONDOWN:
                    //description.Focus();
                    //skinPanel_Leave(null, null);
                    base.WndProc(ref m);
                    break;
                case Win32.WM_SYSCOMMAND:
                    if (m.WParam.ToInt32() == Win32.SC_MAXIMIZE || m.WParam.ToInt32() == Win32.SC_MAXIMIZE + 2)
                    {
                        if (!isOneLoad)
                        {
                            oldSize = this.Size;
                        }
                    }
                    else if (m.WParam == (IntPtr)Win32.SC_RESTORE || m.WParam.ToInt32() == Win32.SC_RESTORE + 2)
                    {
                        if (!isOneLoad)
                        {
                            this.Size = oldSize;
                        }
                    }
                    else if (m.WParam == (IntPtr)Win32.SC_MINIMIZE || m.WParam.ToInt32() == Win32.SC_MINIMIZE + 2)
                    {
                        if (oldSize.Width == 0)
                            oldSize = this.Size;
                    }
                    else if (m.WParam == (IntPtr)Win32.SC_CLOSE || m.WParam.ToInt32() == Win32.SC_CLOSE + 2)
                    {
                        Application.Exit();
                    }
                    base.WndProc(ref m);
                    break;
                case Win32.WM_NCHITTEST:
                    if (!miniMode)
                    {
                        Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                        vPoint = PointToClient(vPoint);
                        if (Top != 0)
                        {
                            if (vPoint.X <= 5)
                            {
                                if (vPoint.Y <= 5)
                                    m.Result = (IntPtr)Win32.HTTOPLEFT;
                                else if (vPoint.Y >= Height - 5)
                                    m.Result = (IntPtr)Win32.HTBOTTOMLEFT;
                                else m.Result = (IntPtr)Win32.HTLEFT;
                            }
                            else if (vPoint.X >= Width - 5)
                            {
                                if (vPoint.Y <= 5)
                                    m.Result = (IntPtr)Win32.HTTOPRIGHT;
                                else if (vPoint.Y >= Height - 5)
                                    m.Result = (IntPtr)Win32.HTBOTTOMRIGHT;
                                else m.Result = (IntPtr)Win32.HTRIGHT;
                            }
                            else if (vPoint.Y <= 5)
                            {
                                m.Result = (IntPtr)Win32.HTTOP;
                            }
                            else if (vPoint.Y >= Height - 5)
                                m.Result = (IntPtr)Win32.HTBOTTOM;
                        }
                        if (((vPoint.Y < 121 && vPoint.Y > 5) || (vPoint.Y > Height - 61 && vPoint.Y < Height - 5)) && (vPoint.X > 5 && vPoint.X < Width - 5))
                            m.Result = (IntPtr)Win32.HTCAPTION;
                    }
                    OnMouseEnter(null);
                    break;
                default:
                    try
                    {
                        base.WndProc(ref m);
                    }
                    catch (Exception ex)
                    {
                        //base.WndProc(ref m);
                        System.Diagnostics.Trace.WriteLine(ex.Message);
                    }

                    break;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeControl();
            if (!isOneLoad)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    int Rgn = Win32.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 5, 5);
                    Win32.SetWindowRgn(this.Handle, Rgn, true);
                }
                else if (this.WindowState == FormWindowState.Maximized)
                {
                    int Rgn = Win32.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 0, 0);
                    Win32.SetWindowRgn(this.Handle, Rgn, true);
                }
            }
            Size size = Screen.PrimaryScreen.WorkingArea.Size;
            MaximizedBounds = new Rectangle(0, 0, size.Width, size.Height - 1);
            //this.Invalidate();
            GC.Collect();
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            oldSize = this.Size;
        }

        #region 最小化按键
        private void ButtonMin_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Hide();
                if (!this.ShowInTaskbar)
                {
                    this.Hide();
                }
                else
                {
                    Win32.PostMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MINIMIZE, 0);
                }
            }
        }

        private void ButtonMin_MouseDown(object sender, MouseEventArgs e)
        {
            minBmp = ResClass.GetImgRes("btn_mini_down");
            ButtonMin.Invalidate();
        }

        private void ButtonMin_MouseEnter(object sender, EventArgs e)
        {
            minBmp = ResClass.GetImgRes("btn_mini_highlight");
            toolTip1.SetToolTip(ButtonMin, "最小化");
            ButtonMin.Invalidate();
        }

        private void ButtonMin_MouseLeave(object sender, EventArgs e)
        {
            minBmp = ResClass.GetImgRes("btn_mini_normal");
            ButtonMin.Invalidate();
        }

        private void ButtonMin_MouseUp(object sender, MouseEventArgs e)
        {
            minBmp = ResClass.GetImgRes("btn_mini_down");
            ButtonMin.Invalidate();
        }

        private void ButtonMin_Paint(object sender, PaintEventArgs e)
        {
            if (this.MinimizeBox)
            {
                g = e.Graphics;
                g.DrawImage(minBmp, new Rectangle(0, 0, minBmp.Width, minBmp.Height), 0, 0, minBmp.Width, minBmp.Height, GraphicsUnit.Pixel);
            }
        }
        #endregion

        #region 最大化按键
        private void ButtonMax_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Normal)
                    Win32.PostMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MAXIMIZE, 0);
                else
                    Win32.PostMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);
                this.Invalidate();
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
            }
        }
        #endregion

        #region 关闭按键
        private void ButtonClose_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!this.ShowInTaskbar)
                {
                    this.Hide();
                }
                else
                {
                    Win32.PostMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MINIMIZE, 0);
                }
            }
            if (CloseEvent != null)
            {
                CloseEvent();
            }
        }

        private void ButtonClose_MouseDown(object sender, MouseEventArgs e)
        {
            closeBmp = ResClass.GetImgRes("btn_close_down");
            ButtonClose.Invalidate();
        }

        private void ButtonClose_MouseEnter(object sender, EventArgs e)
        {
            closeBmp = ResClass.GetImgRes("btn_close_highlight");
            toolTip1.SetToolTip(ButtonClose, "关闭");
            ButtonClose.Invalidate();
        }

        private void ButtonClose_MouseLeave(object sender, EventArgs e)
        {
            closeBmp = ResClass.GetImgRes("btn_close_normal");
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

        private void ButtonClose_Paint(object sender, PaintEventArgs e)
        {
            if (this.ControlBox)
            {
                g = e.Graphics;
                g.DrawImage(closeBmp, new Rectangle(0, 0, closeBmp.Width, closeBmp.Height), 0, 0, closeBmp.Width, closeBmp.Height, GraphicsUnit.Pixel);
            }
        }
        #endregion

        /// <summary>
        /// 自动隐藏事件激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_autoHide_Tick(object sender, EventArgs e)
        {
            //if (GetTrayIconRectangle().Contains(MousePosition))
            //{
            //    return;
            //}

            IntPtr vHandle = WindowFromPoint(System.Windows.Forms.Control.MousePosition);
            while (vHandle != IntPtr.Zero && vHandle != Handle)
            {
                vHandle = GetParent(vHandle);
            }
            if (vHandle == Handle)
            {
                if ((anchors & AnchorStyles.Left) == AnchorStyles.Left)
                {
                    Left = 0;
                    if (TopMost)
                        TopMost = false;
                }
                if ((anchors & AnchorStyles.Top) == AnchorStyles.Top)
                {
                    Top = 0;
                    if (TopMost)
                        TopMost = false;
                }
                if ((anchors & AnchorStyles.Right) == AnchorStyles.Right)
                {
                    Left = Screen.PrimaryScreen.Bounds.Width - Width;
                    if (TopMost)
                        TopMost = false;
                }
                if ((anchors & AnchorStyles.Bottom) == AnchorStyles.Bottom)
                {
                    Top = Screen.PrimaryScreen.Bounds.Height - Height;
                    if (TopMost)
                        TopMost = false;
                }
            }
            else
            {
                if ((anchors & AnchorStyles.Left) == AnchorStyles.Left)
                {
                    Left = -Width + OFFSET;
                    if (!TopMost)
                        TopMost = true;

                }
                if ((anchors & AnchorStyles.Top) == AnchorStyles.Top)
                {
                    Top = -Height + OFFSET;
                    if (!TopMost)
                        TopMost = true;
                }
                if ((anchors & AnchorStyles.Right) == AnchorStyles.Right)
                {
                    Left = Screen.PrimaryScreen.Bounds.Width - OFFSET;
                    if (!TopMost)
                        TopMost = true;
                }
                //if ((anchors & AnchorStyles.Bottom) == AnchorStyles.Bottom)
                //{
                //    Top = Screen.PrimaryScreen.Bounds.Height - OFFSET;
                //}
            }
        }

    }
}
