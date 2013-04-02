using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    public class QQToolStripMenuItem : ToolStripMenuItem
    {
        private System.Drawing.Graphics g;
        private Bitmap Bmp;
        //private Font f;

        public QQToolStripMenuItem()
        { 
            this.CheckedChanged+=new EventHandler(QQToolStripMenuItem_CheckedChanged);
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

            base.Dispose(disposing);

            System.GC.Collect();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Font f = new Font("微软雅黑", 9F);
            g = e.Graphics;

            if (Bmp != null)
            {
                g.DrawImage(Bmp, new Rectangle(3, 1, 3, this.Height - 2), 0, 0, 3, Bmp.Height, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(6, 1, this.Width - 11, this.Height - 2), 3, 0, Bmp.Width - 6, Bmp.Height, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width - 5, 1, 3, this.Height - 2), Bmp.Width - 3, 0, 3, Bmp.Height, GraphicsUnit.Pixel);
            }
            if (this.Image != null)
            {
                g.DrawImage(this.Image, new Rectangle(8, (Height - Image.Height) / 2, Image.Width, Image.Height), 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel);
                
            }

            g.DrawString(this.Text, f, Brushes.Black, 30, 2);

            f.Dispose();
            f = null;
            System.GC.Collect();
        }
        
        protected override void OnMouseEnter(EventArgs e)
        {
            Bmp = ResClass.GetImgRes("menu_highlight");
            this.Invalidate();
            System.GC.Collect();
        }

        void QQToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (Checked)
            {
                this.Image = ResClass.GetImgRes("menu_bkg_check");
            }
            else
            {
                this.Image=null;
            }
            System.GC.Collect();
        }

        protected override void  OnMouseLeave(EventArgs e)
        {
            Bmp = null;
            this.Invalidate();
            System.GC.Collect();
        }
    }
}
