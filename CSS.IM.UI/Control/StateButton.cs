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

    public partial class StateButton : UserControl
    {
        private System.Drawing.Graphics g;
        private Bitmap Bmp;
        private Bitmap ABmp;
        private Bitmap pic;

        public StateButton()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            State = 1;
        }

        private int state;
        [Description("状态"), Category("Appearance")]
        public int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                updatePic();
            }
        }


        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (pic != null)
            {
                pic.Dispose();
                pic = null;
            }

            if (Bmp != null)
            {
                Bmp.Dispose();
                Bmp = null;
            }

            if (ABmp != null)
            {
                ABmp.Dispose();
                ABmp = null;
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
            try
            {
                base.Dispose(disposing);
            }
            catch (System.Exception)
            {

            }

            System.GC.Collect();
        }

        private void updatePic()
        {
            switch (State)
            {
                case 0:
                    pic = ResClass.GetImgRes("imoffline");
                    break;
                case 1:
                    pic = ResClass.GetImgRes("imonline");
                    break;
                case 2:
                    pic = ResClass.GetImgRes("away");
                    break;
                case 3:
                    pic = ResClass.GetImgRes("mute");
                    break;
                case 4:
                    pic = ResClass.GetImgRes("busy");
                    break;
                case 5:
                    pic = ResClass.GetImgRes("invisible");
                    break;
                default:
                    pic = ResClass.GetImgRes("imonline");
                    break;
            }
            this.Invalidate();
            System.GC.Collect();
        }

        private void stateButton_MouseEnter(object sender, EventArgs e)
        {
            Bmp = ResClass.GetImgRes("allbtn_highlight");
            this.Invalidate();
            System.GC.Collect();
        }

        private void stateButton_MouseLeave(object sender, EventArgs e)
        {
            Bmp = null;
            this.Invalidate();
            System.GC.Collect();
        }

        private void stateButton_MouseDown(object sender, MouseEventArgs e)
        {
            Bmp = ResClass.GetImgRes("allbtn_down");
            this.Invalidate();
            System.GC.Collect();
        }

        private void stateButton_MouseUp(object sender, MouseEventArgs e)
        {
            Bmp = null;
            this.Invalidate();
            System.GC.Collect();
        }

        private void stateButton_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            if (Bmp != null)
            {
                g.DrawImage(Bmp, new Rectangle(0, 0, 3, this.Height), 0, 0, 3, Bmp.Height, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(3, 0, this.Width - 6, this.Height), 3, 0, Bmp.Width - 6, Bmp.Height, GraphicsUnit.Pixel);
                g.DrawImage(Bmp, new Rectangle(this.Width - 3, 0, 3, this.Height), Bmp.Width - 3, 0, 3, Bmp.Height, GraphicsUnit.Pixel);
            }

            ABmp = ResClass.GetImgRes("All_down_arrow");

            g.DrawImage(pic, new Rectangle(3, 5, 11, 11), 0, 0, pic.Width, pic.Height, GraphicsUnit.Pixel);
            g.DrawImage(ABmp, new Rectangle(16, 2, 16, 16), 0, 0, 16, 16, GraphicsUnit.Pixel);
            System.GC.Collect();
        }
    }

}
