using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    public class SetLine : UserControl
    {
        private System.Drawing.Graphics g;
        private Bitmap Bmp;
        private PointF point;
        private string m_Text = "SetLine";
        private SizeF fontSize;

        [Description("文本"), Category("Appearance")]
        public string Texts
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
                this.Invalidate();
            }
        }

        public SetLine()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Bmp = ResClass.GetImgRes("set_line");
            System.GC.Collect();
            this.Size = new Size(100, (int)fontSize.Height);
            this.BackColor = Color.Transparent;
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            m_Text = null;

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

            //x = (float)(Width - l * (Font.Size - 1.5)) / 2;
            x = 0;
            y = ((float)Height - sizeF.Height) / 2;
            System.GC.Collect();
            return new PointF(x, y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;

            System.Drawing.Graphics graphics = CreateGraphics();
            fontSize = graphics.MeasureString(Texts, Font);
            int y = ((int)fontSize.Height / 2) + (3 / 2);
            graphics.Dispose();    

            point = GetPointF(this.Texts);
            if (this.Enabled)
                g.DrawString(this.Texts, Font, Brushes.Black, point);
            else
                g.DrawString(this.Texts, Font, Brushes.Gray, point);

            if (Bmp != null)
            {
                g.DrawImage(Bmp, new Rectangle(((int)fontSize.Width), y, Width - ((int)fontSize.Width), 3), 0, 0, Bmp.Width, 3, GraphicsUnit.Pixel);
                
            }

            System.GC.Collect();
        }
    }
}
