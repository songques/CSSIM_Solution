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
    public partial class SPanle : UserControl
    {

        private System.Drawing.Graphics g;
        private Bitmap Bmp;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (Bmp!=null)
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

        public SPanle()
        {
            InitializeComponent();
            System.GC.Collect();
        }

        private void SPanle_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Bmp = ResClass.GetImgRes("ChatFrame_QQShow_background");
            g.DrawImage(Bmp, new Rectangle(0, 0, 5, 15), 0, 0, 5, 20, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(5, 0, this.Width, 15), 5, 0, Bmp.Width - 10, 20, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, 0, 5, 15), Bmp.Width - 5, 0, 5, 20, GraphicsUnit.Pixel);

            g.DrawImage(Bmp, new Rectangle(0, 15, 5, this.Height - 1), 0, 15, 5, Bmp.Height - 17, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(5, 15, this.Width, this.Height - 1), 5, 15, 5, Bmp.Height - 17, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, 15, 5, this.Height - 1), Bmp.Width - 5, 15, 5, Bmp.Height - 17, GraphicsUnit.Pixel);


            g.DrawImage(Bmp, new Rectangle(0, this.Height - 1, 5, 1), 0, Bmp.Height - 1, 5, 1, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(5, this.Height-1, this.Width, 1), 5, Bmp.Height - 1, Bmp.Width - 10, 1, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, this.Height - 1, 5, 1), Bmp.Width - 5, Bmp.Height - 1, 5, 1, GraphicsUnit.Pixel);
            System.GC.Collect();
        }
    }
}
