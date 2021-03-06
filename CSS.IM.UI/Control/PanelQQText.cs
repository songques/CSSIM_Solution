﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    public partial class PanelQQText : UserControl
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

        public PanelQQText()
        {
            InitializeComponent();
        }

        private void PanelQQText_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                g = e.Graphics;
                Bmp = ResClass.GetImgRes("ChatFrame_ShowMsgFrame_background");
                g.DrawImage(Bmp, new Rectangle(0, 0, 5, 15), 0, 0, 5, 20, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(5, 0, this.Width, 15), 5, 0, Bmp.Width - 10, 20, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width-5, 0, 5, 15), Bmp.Width - 5, 0, 5, 20, GraphicsUnit.Pixel);

                g.DrawImage(Bmp, new Rectangle(0, 15, 5, this.Height - 1), 0, 15, 5, Bmp.Height - 17, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(5, 15, this.Width, this.Height - 1), 5, 15, 5, Bmp.Height - 17, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width - 5, 15, 5, this.Height - 1), Bmp.Width - 5, 15, 5, Bmp.Height - 17, GraphicsUnit.Pixel);


                Bmp = ResClass.GetImgRes("ChatFrame_EditMsgFrame_background");
                g.DrawImage(Bmp, new Rectangle(0, this.Height - 1, 5, 1), 0, Bmp.Height - 1, 5, 1, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(5, this.Height-1, this.Width, 1), 5, Bmp.Height - 1, Bmp.Width - 10, 1, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width - 5, this.Height - 1, 5, 1), Bmp.Width - 5, Bmp.Height - 1, 5, 1, GraphicsUnit.Pixel);

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
                //g.Dispose();

            }
            catch (Exception)
            {

            }
            System.GC.Collect();
        }


    }
}
