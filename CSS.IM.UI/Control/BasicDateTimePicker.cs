using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CSS.IM.UI.Util;
using System.ComponentModel;

namespace CSS.IM.UI.Control
{
    public class BasicDateTimePicker : UserControl
    {
        private System.Drawing.Graphics g;
        private Bitmap bmp ;
        private DateTimePickerEx textBox1;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {

            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null;
            }

            if (g != null)
            {
                g.Dispose();
                g = null;
            }

            if (textBox1 != null)
            {
                textBox1.Dispose();
                textBox1 = null;
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

        public BasicDateTimePicker()
        {
            InitTextBox();
            bmp = ResClass.GetImgRes("frameBorderEffect_normalDraw");
            this.BackColor = System.Drawing.Color.Transparent;
            this.Size = new Size(Width, 22);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void InitTextBox()
        {
            textBox1 = new DateTimePickerEx();
            //textBox1.Handle;
            textBox1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            textBox1.BackColor = Color.White;
            //textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            textBox1.Location = new Point(4, 3);
            textBox1.Name = "textBox1";
            //textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(Width-6, 16);
            textBox1.TabIndex = 0;
            //textBox1.WordWrap = false;
            textBox1.MouseLeave += new EventHandler(textBox1_MouseLeave);
            textBox1.MouseEnter += new EventHandler(textBox1_MouseEnter);
            textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown);
            Controls.Add(textBox1);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        [Description("文本"), Category("Appearance")]
        public string Texts
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
                textBox1.Size = new Size(Width - 6, Height-6);
            }
        }

        //[Description("多行"), Category("Appearance")]
        //public bool Multi
        //{
        //    get
        //    {
        //        return textBox1.Multiline;
        //    }
        //    set
        //    {
        //        textBox1.Multiline = value;
        //        if (value)
        //            textBox1.Height = this.Height - 6;
        //    }
        //}

        //[Description("只读"), Category("Appearance")]
        //public bool ReadOn
        //{
        //    get
        //    {
        //        return textBox1.ReadOnly;
        //    }
        //    set
        //    {
        //        textBox1.ReadOnly = value;
        //    }
        //}

        //[Description("密码框"), Category("Appearance")]
        //public bool IsPass
        //{
        //    get
        //    {
        //        return textBox1.UseSystemPasswordChar;
        //    }
        //    set
        //    {
        //        textBox1.UseSystemPasswordChar = value;
        //    }
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            if (bmp != null)
            {
                g = e.Graphics;
                g.DrawImage(bmp, new Rectangle(0, 0, 4, 4), 0, 0, 4, 4, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(4, 0, this.Width - 8, 4), 4, 0, bmp.Width - 8, 4, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(this.Width - 4, 0, 4, 4), bmp.Width - 4, 0, 4, 4, GraphicsUnit.Pixel);

                g.DrawImage(bmp, new Rectangle(0, 4, 4, this.Height - 6), 0, 4, 4, bmp.Height - 8, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(this.Width - 4, 4, 4, this.Height - 6), bmp.Width - 4, 4, 4, bmp.Height - 6, GraphicsUnit.Pixel);

                g.DrawImage(bmp, new Rectangle(0, this.Height - 2, 2, 2), 0, bmp.Height - 2, 2, 2, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(2, this.Height - 2, this.Width - 2, 2), 2, bmp.Height - 2, bmp.Width - 4, 2, GraphicsUnit.Pixel);
                g.DrawImage(bmp, new Rectangle(this.Width - 2, this.Height - 2, 2, 2), bmp.Width - 2, bmp.Height - 2, 2, 2, GraphicsUnit.Pixel);
            }
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            bmp = ResClass.GetImgRes("frameBorderEffect_mouseDownDraw");
            this.Invalidate();
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            bmp = ResClass.GetImgRes("frameBorderEffect_normalDraw");
            this.Invalidate();
        }

        //public void AppendText(string ss) 
        //{
        //    textBox1.AppendText(ss);
        //}

        //protected override void OnResize(EventArgs e)
        //{
        //    if (this.Height > 23)
        //    {
        //        Multi = true;
        //    }
        //    else
        //    {
        //        this.Height = 23;
        //        Multi = false;
        //    }
        //}
    }
}
