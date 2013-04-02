using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    public class BarImageButton : PictureBox
    {
        private Bitmap Bmp;
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

            if (Image!=null)
            {
                Image.Dispose();
                Image = null;
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

        public BarImageButton()
        {
            InitializeComponent();

            //BackColor = Color.Transparent;
            this.Size = new Size(35, 35);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            if (Image==null)
            {
                //Image = ResClass.GetImgRes("all_iconbutton_pushedBackground");
                Image = ResClass.GetImgRes("allbtn_highlight");
            }
            System.GC.Collect();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // TopBarImageButton
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BarImageButton_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BarImageButton_MouseDown);
            this.MouseEnter += new System.EventHandler(this.BarImageButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.BarImageButton_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void BarImageButton_MouseDown(object sender, MouseEventArgs e)
        {
            //g = CreateGraphics();
            Bmp = ResClass.GetImgRes("allbtn_highlight");
            //g.DrawImage(Bmp, new Rectangle(0, 0, 35, 35), 0, 0, 20, 20, GraphicsUnit.Pixel);
            //g.DrawImage(Image, new Rectangle(0, 0, 35, 35), 0, 0, 35, 35, GraphicsUnit.Pixel);
            //g.Dispose();
            Invalidate();
            System.GC.Collect();
        }

        private void BarImageButton_MouseEnter(object sender, EventArgs e)
        {
            //g = CreateGraphics();
            Bmp = ResClass.GetImgRes("allbtn_down");
            //g.DrawImage(Bmp, new Rectangle(0, 0, 35, 35), 0, 0, 20, 20, GraphicsUnit.Pixel);
            //g.DrawImage(Image, new Rectangle(0, 0, 35, 35), 0, 0, 35, 35, GraphicsUnit.Pixel);
            //g.Dispose();
            Invalidate();
            System.GC.Collect();

        }

        private void BarImageButton_MouseLeave(object sender, EventArgs e)
        {
            Bmp.Dispose();
            Bmp = null;
            Invalidate();
            System.GC.Collect();
        }

        private void BarImageButton_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            if (Bmp != null)
            {
                g.DrawImage(Bmp, new Rectangle(0, 0, 3, 3), 0, 0, 3,3, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(0, 3,3, this.Height - 6), 0, 3, 3, Bmp.Height - 6, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(0, this.Height - 3, 3, 3), 0, Bmp.Height - 3, 3, 3, GraphicsUnit.Pixel);

                g.DrawImage(Bmp, new Rectangle(3, 0, this.Width-6, 3), 3, 0, Bmp.Width-6, 3, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(3, 3, this.Width - 6, this.Height - 6), 3, 3, Bmp.Width - 6, Bmp.Height - 6, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(3, this.Height - 3, this.Width-6, 3), 3, Bmp.Height - 3, Bmp.Width-6, 3, GraphicsUnit.Pixel);

                g.DrawImage(Bmp, new Rectangle(this.Width-3, 0, 3, 3), Bmp.Width-3, 0, 3, 3, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width - 3, 3, 3, this.Height - 6), Bmp.Width - 3, 3, 3, Bmp.Height - 6, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width - 3, this.Height - 3, 3, 3), Bmp.Width - 3, Bmp.Height - 3, 3, 3, GraphicsUnit.Pixel);

            }
        }
    }
}
