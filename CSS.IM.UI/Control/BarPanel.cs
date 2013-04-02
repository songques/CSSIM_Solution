using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Util;
using System.Drawing;

namespace CSS.IM.UI.Control
{
    public class BarPanel:UserControl
    {
        private System.Drawing.Bitmap Bmp;
        private System.Drawing.Graphics g;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;
            if (Bmp == null)
            {
                Bmp = ResClass.GetImgRes("ChatFrame_QuickbarFrame_background");
            }
            g.DrawImage(Bmp, new Rectangle(0, 0, 2, 22), 0, 0, 2, 22, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(2, 0, this.Width, 22), 2, 0, Bmp.Width - 4, 22, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 2, 0, 2, 22), Bmp.Width - 2, 0, 2, 22, GraphicsUnit.Pixel);
            System.GC.Collect();
            //g.Dispose();
        }
    }
}
