using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    [DefaultEvent("CheckedChanged")]
    public partial class BasicRadioButton : UserControl
    {
        private System.Drawing.Graphics g ;
        private string m_buttonText = "RadioButton";
        private bool m_Checked = false;
        public delegate void CheckedChangedEventHandler(object sender, bool Checked);
        public event CheckedChangedEventHandler CheckedChanged;
        private Bitmap Bmp;
        private ImageAttributes imageAttr;

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

            if (imageAttr != null)
            {
                imageAttr.Dispose();
                imageAttr = null;
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

        public BasicRadioButton()
        {
            if (m_Checked)
            {
                Bmp = ResClass.GetImgRes("rb_click");
            }
            else
            {
                Bmp = ResClass.GetImgRes("rb_no");
            }
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.Transparent;
            this.Width = 95;
            this.Height = 15;
        }

        [Description("按钮上的文本"), Category("Appearance")]
        public string TextValue
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

        [Description("是否选中"), Category("Appearance")]
        public bool Checked
        {
            get
            {
                return m_Checked;
            }
            set
            {
                m_Checked = value;
                if (m_Checked)
                {
                    Bmp = ResClass.GetImgRes("rb_click");
                }
                else
                {
                    Bmp = ResClass.GetImgRes("rb_no");
                }
                this.Invalidate();
                if (CheckedChanged != null)
                    CheckedChanged(this, value);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;
            
            imageAttr = new ImageAttributes();
            imageAttr.SetColorKey(Bmp.GetPixel(14, 14), Bmp.GetPixel(14, 14));
            g.DrawImage(Bmp, new Rectangle(0, (this.Height-15)/2, 15, 15), 0, 0, Bmp.Width, Bmp.Height, GraphicsUnit.Pixel, imageAttr);
            g.DrawRectangle(new Pen(this.BackColor, 16), 25, 0, this.Width - 25, this.Height);
            g.DrawString(m_buttonText, this.Font, Brushes.Black, 16, (this.Height - 11) / 2);
            System.GC.Collect();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (m_Checked)
            {
                Bmp = ResClass.GetImgRes("rb_click_move");
            }
            else
            {
                Bmp = ResClass.GetImgRes("rb_no_move");
            }
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (m_Checked)
            {
                Bmp = ResClass.GetImgRes("rb_click");
            }
            else
            {
                Bmp = ResClass.GetImgRes("rb_no");
            }
            this.Invalidate();
            System.GC.Collect();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                if (!Checked)
                {
                    foreach (System.Windows.Forms.Control control in this.Parent.Controls)
	                {
                        if (control is BasicRadioButton && !control.Name.Equals(this.Name))
                        {
                            ((BasicRadioButton)control).Checked = false;
                        }
	                }
                    Checked = true;
                    Bmp = ResClass.GetImgRes("rb_click_move");
                }
                this.Invalidate();
            }
            System.GC.Collect();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.MinimumSize = new Size(15, 15);
            System.GC.Collect();
        }
    }
}
