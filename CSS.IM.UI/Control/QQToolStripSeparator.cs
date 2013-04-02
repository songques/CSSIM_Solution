using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    public class QQToolStripSeparator : ToolStripSeparator
    {
        private System.Drawing.Graphics g;
        private System.Drawing.Bitmap Bmp;

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
            Bmp = ResClass.GetImgRes("menu_cutling");
            g = e.Graphics;
            g.DrawImage(Bmp, new Rectangle(25, 2, this.Width - 25, 2), 0, 0, 50, 3, GraphicsUnit.Pixel);
            System.GC.Collect();
        }
    }
}
