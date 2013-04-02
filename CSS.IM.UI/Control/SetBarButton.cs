using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    [DefaultEvent("Click")]
    public class SetBarButton : UserControl
    {

        private System.Drawing.Graphics g;
        private Bitmap Bmp;
        private string m_buttonText = "Button";
        private PointF point;
        public SetBarButton()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true); 
            Bmp = CSS.IM.UI.Util.ResClass.GetImgRes("setbar_btn_normal");
            System.GC.Collect();
            this.Size = new Size(147, 29);
        }

        [Description("文本"), Category("Appearance")]
        public string Texts
        {
            get
            {
                return m_buttonText;
            }
            set
            {
                m_buttonText = value;
                this.Invalidate();
            }
        }



        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            m_buttonText = null;

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Environment.OSVersion.Version.Major >= 6)
            {
                this.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
            }
            else
            {
                this.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            }
            System.GC.Collect();
            //this.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
        }

        private PointF GetPointF(string buttonText)
        {
            float x;
            float y;
            int l = 0;
            char[] c = buttonText.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (Convert.ToInt32(c[i]) > Convert.ToInt32((char)128))
                {
                    l += 2;
                }
                else
                {
                    l += 1;
                }
            }
            System.Drawing.Graphics graphics = CreateGraphics();
            SizeF sizeF = graphics.MeasureString(buttonText, Font);
            //MessageBox.Show(string.Format("字体宽度：{0}，高度：{1}", sizeF.Width, sizeF.Height));
            graphics.Dispose();
            graphics = null;

            x = (float)(Width - l * (Font.Size - 1.5)) / 2;
            y = ((float)Height - sizeF.Height) / 2;
            System.GC.Collect();
            return new PointF(x, y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;
            if (Bmp != null)
            {
                g.DrawImage(Bmp, new Rectangle(0, 0, 3, 3), 0, 0, 3, 3, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(0, 3, 3, this.Height - 6), 0, 3, 3, Bmp.Height - 6, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(0, this.Height - 3, 3, 3), 0, Bmp.Height - 3, 3, 3, GraphicsUnit.Pixel);

                g.DrawImage(Bmp, new Rectangle(3, 0, this.Width - 6, 3), 3, 0, Bmp.Width - 6, 3, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(3, 3, this.Width - 6, this.Height - 6), 3, 3, Bmp.Width - 6, Bmp.Height - 6, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(3, this.Height - 3, this.Width - 6, 3), 3, Bmp.Height - 3, Bmp.Width - 6, 3, GraphicsUnit.Pixel);

                g.DrawImage(Bmp, new Rectangle(this.Width - 3, 0, 3, 3), Bmp.Width - 3, 0, 3, 3, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width - 3, 3, 3, this.Height - 6), Bmp.Width - 3, 3, 3, Bmp.Height - 6, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width - 3, this.Height - 3, 3, 3), Bmp.Width - 3, Bmp.Height - 3, 3, 3, GraphicsUnit.Pixel);
            }
            point = GetPointF(this.Texts);

            if (this.Enabled)
                g.DrawString(this.Texts, Font, Brushes.Black, point);
            else
                g.DrawString(this.Texts, Font, Brushes.Gray, point);
            System.GC.Collect();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            //Bmp = ResClass.GetImgRes("setbar_btn_highlight");
            Bmp = ResClass.GetImgRes("setbar_btn_down");
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            OnMouseLeave(e);
            System.GC.Collect();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Bmp = ResClass.GetImgRes("setbar_btn_down");
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (Focused)
                Bmp = ResClass.GetImgRes("setbar_btn_highlight");
            else
                Bmp = ResClass.GetImgRes("setbar_btn_normal");
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnEnter(EventArgs e)
        {
            Bmp = ResClass.GetImgRes("setbar_btn_normal");
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnLeave(EventArgs e)
        {
            Bmp = ResClass.GetImgRes("setbar_btn_normal");
            this.Invalidate();
            System.GC.Collect();
        }
    }
}
