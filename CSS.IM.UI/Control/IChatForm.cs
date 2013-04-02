using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using CSS.IM.UI.Util;
using CSS.IM.UI.Form;

namespace CSS.IM.UI
{
    public partial class IChatForm : System.Windows.Forms.Form
    {
        private Graphics g;
        private Bitmap Bmp;
        private Bitmap closeBmp;
        private Bitmap minBmp;
        private Bitmap maxBmp;
        private ImageAttributes imageAttr;
        private int xwidth = 0;
        private bool _allowResize = true;
        private bool _allowMove = true;
        private bool isOneLoad = true;

        private Size oldSize;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {

            if(Bmp!=null)
            {
                Bmp.Dispose();
                Bmp = null;
            }
            if (closeBmp != null)
            {
                closeBmp.Dispose();
                closeBmp = null;
            }
            if (minBmp != null)
            {
                minBmp.Dispose();
                minBmp = null;
            }
            if (maxBmp != null)
            {
                maxBmp.Dispose();
                maxBmp = null;
            }

            if (g != null)
            {
                g.Dispose();
                g = null;
            }

            if (imageAttr != null)
            {
                imageAttr.Dispose();
                imageAttr = null;
            }

            if (description!=null)
            {
                description.Dispose();
                description = null;
            }

            if (nikeName!=null)
            {
                nikeName.Dispose();
                nikeName = null;

            }

            if (toolTip1!=null)
            {
                toolTip1.Dispose();
                toolTip1 = null;
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

        public IChatForm()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            closeBmp = ResClass.GetImgRes("btn_close_normal");
            minBmp = ResClass.GetImgRes("btn_mini_normal");
            maxBmp = ResClass.GetImgRes("btn_max_normal");
            if (this.WindowState == FormWindowState.Maximized)
            {
                maxBmp = ResClass.GetImgRes("btn_restore_normal");
            }
 
        }

        private void IChatForm_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                g = e.Graphics;
                Bmp = ResClass.GetImgRes("ChatFrame_Window_windowBkg");
                imageAttr = new ImageAttributes();
                imageAttr.SetColorKey(Bmp.GetPixel(1, 1), Bmp.GetPixel(1, 1));
                g.DrawImage(Bmp, new Rectangle(0, 0, 50, 100), 0, 0, 50, 100, GraphicsUnit.Pixel, imageAttr);
                g.DrawImage(Bmp, new Rectangle(50, 0, this.Width - 120, 100), 50, 0, Bmp.Width - 120, 100, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width - 80, 0, 80, 100), Bmp.Width - 80, 0, 80, 100, GraphicsUnit.Pixel, imageAttr);
                g.DrawImage(Bmp, new Rectangle(0, 100, 5, this.Height - 105), 0, 100, 5, Bmp.Height - 105, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(5, 100, this.Width - 10, this.Height - 105), 5, 100, 5, Bmp.Height - 105, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width - 5, 100, 5, this.Height - 105), Bmp.Width - 5, 100, 5, Bmp.Height - 105, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(0, this.Height - 5, 5, 5), 0, Bmp.Height - 5, 5, 5, GraphicsUnit.Pixel, imageAttr);
                g.DrawImage(Bmp, new Rectangle(5, this.Height - 5, this.Width - 10, 5), 5, Bmp.Height - 5, Bmp.Width - 10, 5, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width - 5, this.Height - 5, 5, 5), Bmp.Width - 5, Bmp.Height - 5, 5, 5, GraphicsUnit.Pixel, imageAttr);

                //Bmp = ResClass.GetImgRes("ChatFrame_ShowMsgFrame_background");
                //g.DrawImage(Bmp, new Rectangle(5, 85, 5, 15), 0, 0, 5, 20, GraphicsUnit.Pixel);
                //g.DrawImage(Bmp, new Rectangle(10, 85, this.Width - 15, 15), 5, 0, Bmp.Width - 10, 20, GraphicsUnit.Pixel);
                //g.DrawImage(Bmp, new Rectangle(this.Width - 10, 85, 5, 15), Bmp.Width - 5, 0, 5, 20, GraphicsUnit.Pixel);

                //g.DrawImage(Bmp, new Rectangle(5, 100, 5, this.Height - 255), 0, 15, 5, Bmp.Height - 17, GraphicsUnit.Pixel);
                //g.DrawImage(Bmp, new Rectangle(10, 100, this.Width - 15, this.Height - 255), 5, 15, 5, Bmp.Height - 17, GraphicsUnit.Pixel);
                //g.DrawImage(Bmp, new Rectangle(this.Width - 10, 100, 5, this.Height - 255), Bmp.Width - 5, 15, 5, Bmp.Height - 17, GraphicsUnit.Pixel);

                //g.DrawImage(Bmp, new Rectangle(5, this.Height - 156, 5, 1), 0, Bmp.Height - 1, 5, 1, GraphicsUnit.Pixel);
                //g.DrawImage(Bmp, new Rectangle(10, this.Height - 156, this.Width - 15, 1), 5, Bmp.Height - 1, Bmp.Width - 10, 1, GraphicsUnit.Pixel);
                //g.DrawImage(Bmp, new Rectangle(this.Width - 10, this.Height - 156, 5, 1), Bmp.Width - 5, Bmp.Height - 1, 5, 1, GraphicsUnit.Pixel);

                //Bmp = ResClass.GetImgRes("ChatFrame_EditMsgFrame_background");
                //g.DrawImage(Bmp, new Rectangle(5, 357, 5, 5), 0, 0, 5, 5, GraphicsUnit.Pixel);
                //g.DrawImage(Bmp, new Rectangle(10, 357, this.Width - 15, 5), 5, 0, Bmp.Width - 10, 5, GraphicsUnit.Pixel);
                //g.DrawImage(Bmp, new Rectangle(this.Width - 10, 357, 5, 5), Bmp.Width - 5, 0, 5, 5, GraphicsUnit.Pixel);

                //g.DrawImage(Bmp, new Rectangle(5, 362, 5, this.Height - 408), 0, 5, 5, Bmp.Height - 20, GraphicsUnit.Pixel);
                //g.DrawImage(Bmp, new Rectangle(10, 362, this.Width - 15, this.Height - 408), 5, 5, 5, Bmp.Height - 20, GraphicsUnit.Pixel);
                //g.DrawImage(Bmp, new Rectangle(this.Width - 10, 362, 5, this.Height - 408), Bmp.Width - 5, 5, 5, Bmp.Height - 20, GraphicsUnit.Pixel);

                //g.DrawImage(Bmp, new Rectangle(5, this.Height - 46, 5, 10), 0, Bmp.Height - 10, 5, 10, GraphicsUnit.Pixel);
                //g.DrawImage(Bmp, new Rectangle(10, this.Height - 46, this.Width - 15, 10), 5, Bmp.Height - 10, Bmp.Width - 10, 10, GraphicsUnit.Pixel);
                //g.DrawImage(Bmp, new Rectangle(this.Width - 10, this.Height - 46, 5, 10), Bmp.Width - 5, Bmp.Height - 10, 5, 10, GraphicsUnit.Pixel);
            }
            catch (Exception)
            {
                
               
            }
        }

        private void IChatForm_Resize(object sender, EventArgs e)
        {
            this.ButtonClose.Left = this.Width - ButtonClose.Width;
            this.ButtonMax.Left = this.Width - ButtonClose.Width - ButtonMax.Width;
            this.ButtonMin.Left = this.Width - ButtonClose.Width - ButtonMax.Width - ButtonMin.Width;
            this.Refresh();
        }


        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            oldSize = this.Size;
        }

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
            try
            {
                toolTip1.SetToolTip(ButtonClose, "关闭");
            }
            catch (Exception)
            {

            }
            
            ButtonClose.Invalidate();
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
                this.WindowState = FormWindowState.Minimized;
        }

        private void IChatForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks >= 2 && this.MaximizeBox)
                {
                    ButtonMax_MouseClick(null, new MouseEventArgs(MouseButtons.Left, 1, 2, 2, 0));
                }
                else
                {
                    Win32.ReleaseCapture();
                    Win32.SendMessage(Handle, 0x00A1, 2, 0);
                }
            }
        }

        private void ButtonMin_Paint(object sender, PaintEventArgs e)
        {
            if (this.MinimizeBox)
            {
                g = e.Graphics;
                g.DrawImage(minBmp, new Rectangle(0, 0, minBmp.Width, minBmp.Height), 0, 0, minBmp.Width, minBmp.Height, GraphicsUnit.Pixel, imageAttr);
            }
        }

        private void ButtonClose_Paint(object sender, PaintEventArgs e)
        {
            if (this.ControlBox)
            {
                g = e.Graphics;
                g.DrawImage(closeBmp, new Rectangle(0, 0, closeBmp.Width, closeBmp.Height), 0, 0, closeBmp.Width, closeBmp.Height, GraphicsUnit.Pixel, imageAttr);
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
            try
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
            catch (Exception)
            {
                

            }            
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

        private void ButtonMax_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void IForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Y <= 50 && e.X < xwidth)
                {
                    //topPanel_MouseClick(null, e);
                }
            }
        }


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
                    try
                    {
                        base.WndProc(ref m);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    
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

        //private void qqShowBg_Paint(object sender, PaintEventArgs e)
        //{
        //    //g = e.Graphics;
        //    //Bmp = ResClass.GetImgRes("ChatFrame_QQShow_background");
        //    //g.DrawImage(Bmp, new Rectangle(0, 0, 5, 5), 0, 0, 5, 5, GraphicsUnit.Pixel);
        //    //g.DrawImage(Bmp, new Rectangle(5, 0, this.qqShowBg.Width - 10, 5), 5, 0, Bmp.Width - 10, 5, GraphicsUnit.Pixel);
        //    //g.DrawImage(Bmp, new Rectangle(this.qqShowBg.Width - 5, 0, 5, 5), Bmp.Width - 5, 0, 5, 5, GraphicsUnit.Pixel);
        //    //g.DrawImage(Bmp, new Rectangle(0, 5, 5, this.qqShowBg.Height - 15), 0, 5, 5, Bmp.Height - 20, GraphicsUnit.Pixel);
        //    //g.DrawImage(Bmp, new Rectangle(5, 5, this.qqShowBg.Width - 10, this.qqShowBg.Height - 15), 5, 5, 5, Bmp.Height - 20, GraphicsUnit.Pixel);
        //    //g.DrawImage(Bmp, new Rectangle(this.qqShowBg.Width - 5, 5, 5, this.qqShowBg.Height - 15), Bmp.Width - 5, 5, 5, Bmp.Height - 20, GraphicsUnit.Pixel);
        //    //g.DrawImage(Bmp, new Rectangle(0, this.qqShowBg.Height - 10, 5, 10), 0, Bmp.Height - 10, 5, 10, GraphicsUnit.Pixel);
        //    //g.DrawImage(Bmp, new Rectangle(5, this.qqShowBg.Height - 10, this.qqShowBg.Width - 10, 10), 5, Bmp.Height - 10, Bmp.Width - 10, 10, GraphicsUnit.Pixel);
        //    //g.DrawImage(Bmp, new Rectangle(this.qqShowBg.Width - 5, this.qqShowBg.Height - 10, 5, 10), Bmp.Width - 5, Bmp.Height - 10, 5, 10, GraphicsUnit.Pixel);
        //}

    }
}
