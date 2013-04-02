using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    public partial class LoginButton : UserControl
    {
        private System.Drawing.Graphics g;
        private Bitmap Bmp;

        public LoginButton()
        {
            Bmp = ResClass.GetImgRes("login_btn_normal");
            if (this.Focused)
                Bmp = ResClass.GetImgRes("login_btn_focus");
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.Transparent;
            this.Size = new Size(74,27);
        }

        [Description("文本"), Category("Appearance")]
        public string Texts
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {

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

            try
            {
                base.Dispose(disposing);
            }
            catch (System.Exception)
            {

            }

            System.GC.Collect();
        }

        private PointF GetPointF(string text)
        {
            float x, y;

            switch (text.Length)
            {
                case 1:
                    x = (float)(this.Width - 12.5 * 1) / 2;
                    y = 4;
                    break;
                case 2:
                    x = (float)(this.Width - 12.5 * 2) / 2;
                    y = 4;
                    break;
                case 3:
                    x = (float)(this.Width - 12.5 * 3) / 2;
                    y = 4;
                    break;
                case 4:
                    x = (float)(this.Width - 12.5 * 4) / 2;
                    y = 4;
                    break;
                case 5:
                    x = (float)(this.Width - 12.3 * 5) / 2;
                    y = 4;
                    break;
                case 6:
                    x = (float)(this.Width - 12.3 * 6) / 2;
                    y = 4;
                    break;
                default:
                    x = (float)(this.Width - 12.3 * 4) / 2;
                    y = 4;
                    break;
            }
            System.GC.Collect();
            return new PointF(x, y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            g = e.Graphics;
            if (Bmp != null)
            {
                g.DrawImage(Bmp, new Rectangle(0, 0, this.Width, this.Height), 0, 0, Bmp.Width, Bmp.Height, GraphicsUnit.Pixel);
            }
                PointF point = GetPointF(this.Text);
            if (this.Enabled)
                g.DrawString(this.Texts, new Font("微软雅黑", 9F), Brushes.Black, point);
            else
                g.DrawString(this.Texts, new Font("微软雅黑", 9F), Brushes.Gray, point);
            System.GC.Collect();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Bmp = ResClass.GetImgRes("login_btn_highlight");
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Bmp = ResClass.GetImgRes("login_btn_normal");
            if (this.Focused)
                Bmp = ResClass.GetImgRes("login_btn_focus");
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Bmp = ResClass.GetImgRes("login_btn_down");
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Bmp = ResClass.GetImgRes("login_btn_normal");
            if (this.Focused)
                Bmp = ResClass.GetImgRes("login_btn_focus");
            this.Invalidate();
            System.GC.Collect();
        }
    }
}
