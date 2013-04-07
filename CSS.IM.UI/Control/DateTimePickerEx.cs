using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    public class DateTimePickerEx : DateTimePicker
    {
        private Bitmap bmp;

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;

        }

        //[DllImport("user32.dll", EntryPoint = "SendMessageA")]
        //private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, object lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr intPtr, int WM_ERASEBKGND, IntPtr hDC, ref COPYDATASTRUCT cds);

        [DllImport("user32.dll", EntryPoint = "GetWindowDC", CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC", CallingConvention = CallingConvention.Winapi)]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        const int WM_ERASEBKGND = 0x14;
        const int WM_PAINT = 0xF;
        const int WM_NC_HITTEST = 0x84;
        const int WM_NC_PAINT = 0x85;
        const int WM_PRINTCLIENT = 0x318;
        const int WM_SETCURSOR = 0x20;


        private Pen BorderPen = new Pen(Color.Red, 0);
        private Pen BorderPenControl = new Pen(SystemColors.ControlDark, 0);
        private bool DroppedDown = false;
        private int InvalidateSince = 0;
        COPYDATASTRUCT cds;
        //private static int DropDownButtonWidth = 17;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {

            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null;
            }

            if (BorderPen != null)
            {
                BorderPen.Dispose();
                BorderPen = null;
            }

            if (BorderPenControl != null)
            {
                BorderPenControl.Dispose();
                BorderPenControl = null;
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

        public DateTimePickerEx()
            : base()
        {
            cds.dwData = (IntPtr)0;
            cds.lpData = null;
            cds.cbData = -1;
            bmp = ResClass.GetImgRes("frameBorderEffect_normalDraw");
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        protected override void OnValueChanged(EventArgs eventargs)
        {
            base.OnValueChanged(eventargs);
            this.Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            IntPtr hDC = IntPtr.Zero;
            System.Drawing.Graphics gdc = null;
            switch (m.Msg)
            {
                case WM_NC_PAINT:
                    hDC = GetWindowDC(m.HWnd);
                    gdc = System.Drawing.Graphics.FromHdc(hDC);
                    SendMessage(this.Handle, WM_ERASEBKGND, hDC, ref cds);
                    SendPrintClientMsg();
                    SendMessage(this.Handle, WM_PAINT, IntPtr.Zero,ref cds);
                    OverrideControlBorder(gdc);
                    m.Result = (IntPtr)1;	// indicate msg has been processed
                    ReleaseDC(m.HWnd, hDC);
                    gdc.Dispose();
                    break;
                case WM_PAINT:
                    base.WndProc(ref m);
                    hDC = GetWindowDC(m.HWnd);
                    gdc = System.Drawing.Graphics.FromHdc(hDC);
                    OverrideDropDown(gdc);
                    OverrideControlBorder(gdc);
                    ReleaseDC(m.HWnd, hDC);
                    gdc.Dispose();
                    break;
                /*
                // We don't need this anymore, handle by WM_SETCURSOR
                case WM_NC_HITTEST: 
                    base.WndProc(ref m);
                    if (DroppedDown)
                    {
                        OverrideDropDown(gdc);
                        OverrideControlBorder(gdc);
                    }
                    break;
                */
                case WM_SETCURSOR:
                    base.WndProc(ref m);
                    // The value 3 is discovered by trial on error, and cover all kinds of scenarios
                    // InvalidateSince < 2 wil have problem if the control is not in focus and dropdown is clicked
                    if (DroppedDown && InvalidateSince < 3)
                    {
                        Invalidate();
                        InvalidateSince++;
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
            System.GC.Collect();
        }


        private void SendPrintClientMsg()
        {
            // We send this message for the control to redraw the client area
            System.Drawing.Graphics gClient = this.CreateGraphics();
            IntPtr ptrClientDC = gClient.GetHdc();
            SendMessage(this.Handle, WM_PRINTCLIENT, ptrClientDC, ref cds);
            gClient.ReleaseHdc(ptrClientDC);
            gClient.Dispose();
            System.GC.Collect();
        }

        private void OverrideDropDown(System.Drawing.Graphics g)
        {
            //if (!this.ShowUpDown)
            //{
            //    Rectangle rect = new Rectangle(this.Width - DropDownButtonWidth, 0, DropDownButtonWidth, this.Height);
            //    ControlPaint.DrawComboButton(g, rect, ButtonState.Flat);
            //}
            System.GC.Collect();
        }

        private void OverrideControlBorder(System.Drawing.Graphics g)
        {
            if (this.Focused == false || this.Enabled == false)
            {
                bmp = ResClass.GetImgRes("frameBorderEffect_normalDraw");
                // g.DrawRectangle(BorderPenControl, new Rectangle(0, 0, this.Width, this.Height));
                g.DrawImage(bmp, new Rectangle(0, 0, 4, 4), 0, 0, 4, 4, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(4, 0, this.Width - 8, 4), 4, 0, bmp.Width - 8, 4, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(this.Width - 4, 0, 4, 4), bmp.Width - 4, 0, 4, 4, GraphicsUnit.Pixel);

                g.DrawImage(bmp, new Rectangle(0, 4, 4, this.Height - 6), 0, 4, 4, bmp.Height - 8, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(this.Width - 4, 4, 4, this.Height - 6), bmp.Width - 4, 4, 4, bmp.Height - 6, GraphicsUnit.Pixel);

                g.DrawImage(bmp, new Rectangle(0, this.Height - 2, 2, 2), 0, bmp.Height - 2, 2, 2, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(2, this.Height - 2, this.Width - 2, 2), 2, bmp.Height - 2, bmp.Width - 4, 2, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(this.Width - 2, this.Height - 2, 2, 2), bmp.Width - 2, bmp.Height - 2, 2, 2, GraphicsUnit.Pixel);
            }
            else
            {
                 bmp = ResClass.GetImgRes("frameBorderEffect_mouseDownDraw");
                //g.DrawRectangle(BorderPen, new Rectangle(0, 0, this.Width, this.Height));
                g.DrawImage(bmp, new Rectangle(0, 0, 4, 4), 0, 0, 4, 4, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(4, 0, this.Width - 8, 4), 4, 0, bmp.Width - 8, 4, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(this.Width - 4, 0, 4, 4), bmp.Width - 4, 0, 4, 4, GraphicsUnit.Pixel);

                g.DrawImage(bmp, new Rectangle(0, 4, 4, this.Height - 6), 0, 4, 4, bmp.Height - 8, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(this.Width - 4, 4, 4, this.Height - 6), bmp.Width - 4, 4, 4, bmp.Height - 6, GraphicsUnit.Pixel);

                g.DrawImage(bmp, new Rectangle(0, this.Height - 2, 2, 2), 0, bmp.Height - 2, 2, 2, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(2, this.Height - 2, this.Width - 2, 2), 2, bmp.Height - 2, bmp.Width - 4, 2, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(this.Width - 2, this.Height - 2, 2, 2), bmp.Width - 2, bmp.Height - 2, 2, 2, GraphicsUnit.Pixel);
            }
            System.GC.Collect();
            
        }

        protected override void OnDropDown(EventArgs eventargs)
        {
            InvalidateSince = 0;
            DroppedDown = true;
            base.OnDropDown(eventargs);
            System.GC.Collect();
        }
        protected override void OnCloseUp(EventArgs eventargs)
        {
            DroppedDown = false;
            base.OnCloseUp(eventargs);
            System.GC.Collect();
        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
            System.GC.Collect();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
            System.GC.Collect();
        }

    }
}
