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
    public partial class BasicLable : UserControl
    {
        private System.Drawing.Graphics g;
        Bitmap Bmp;

        private string _text = "basiclabel";

        private bool MouseEnterState = false;

        [Description("文本"), Category("Appearance")]
        public new string Text
        {
            get
            {
                //_text = labText.Text;
                return _text;
            }
            set
            {
                _text = value;
                //labText.Text = _text;
                this.Invalidate();
            }
        }

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

        public BasicLable()
        {
            Bmp = ResClass.GetImgRes("allbtn_highlight");
            InitializeComponent();
        }

        private void BasicLable_MouseEnter(object sender, EventArgs e)
        {
            if (!MouseEnterState)
	        {
		         MouseEnterState = true;
                    this.Invalidate();
	        }
            System.GC.Collect();
            
        }

        private void BasicLable_MouseLeave(object sender, EventArgs e)
        {
            if (MouseEnterState)
            {
                MouseEnterState = false;
                this.Invalidate();
            }
            System.GC.Collect();
        }

        private PointF GetPointF(string buttonText)
        {
            //float x;
            //int l = 0;
            //char[] c = buttonText.ToCharArray();
            //for (int i = 0; i < c.Length; i++)
            //{
            //    if (Convert.ToInt32(c[i]) > Convert.ToInt32((char)128))
            //    {
            //        l += 2;
            //    }
            //    else
            //    {
            //        l += 1;
            //    }
            //}

            //x = (float)(Width - l * (Font.Size - 1.5)) / 2;
            return new PointF(1, 0);
        }

        private void BasicLable_Paint(object sender, PaintEventArgs e)
        {
            PointF point = GetPointF(this.Text);
            g = e.Graphics;

            if (MouseEnterState)
            {
                e.Graphics.DrawImage(Bmp, new Rectangle(0, 0, 2, this.Height), 0, 0, 2, Bmp.Height, GraphicsUnit.Pixel);
                e.Graphics.DrawImage(Bmp, new Rectangle(2, 0, this.Width - 5, this.Height), 2, 0, Bmp.Width - 5, Bmp.Height, GraphicsUnit.Pixel);
                e.Graphics.DrawImage(Bmp, new Rectangle(this.Width - 3, 0, 3, this.Height), Bmp.Width - 3, 0, 3, Bmp.Height, GraphicsUnit.Pixel);
            }

            if (this.Text == null)
            {
                this.Text = "编辑备注";
                e.Graphics.DrawString(this.Text, this.Font, Brushes.Black, point);
            }
            else if (this.Text.Trim().Length == 0)
            {
                this.Text = "编辑备注";
                e.Graphics.DrawString(this.Text, this.Font, Brushes.Black, point);
            }

            else
            {
                e.Graphics.DrawString(this.Text, this.Font, Brushes.Black, point);
            }
            System.GC.Collect();
        }
    }
}
