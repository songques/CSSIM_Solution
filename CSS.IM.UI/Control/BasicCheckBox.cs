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
    [DefaultEvent("CheckedChanged")]
    public partial class BasicCheckBox : UserControl
    {
        private System.Drawing.Graphics g;
        private bool m_Checked = false;
        private string m_buttonText = "CheckBox";
        public enum CheckStates { Unchecked, Checked, Indeterminate }
        private CheckStates m_CheckState = CheckStates.Unchecked;
    
        public delegate void CheckedChangedEventHandler(object sender, bool Checked);
        public event CheckedChangedEventHandler CheckedChanged;
        private Bitmap Bmp;

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

        public BasicCheckBox()
        {
            if (Checked)
            {
                switch (CheckState)
                {
                    case CheckStates.Checked:
                        Bmp = ResClass.GetImgRes("cb_c_l");
                        break;
                    case CheckStates.Indeterminate:
                        Bmp = ResClass.GetImgRes("cb_b_l");
                        break;
                    default:
                        Bmp = ResClass.GetImgRes("cb_c_l");
                        break;
                }
            }
            else
            {
                Bmp = ResClass.GetImgRes("cb_n_l");
            }
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.Transparent;
            this.Size = new Size(95,20);
        }

        [Description("按钮上的文本"), Category("Appearance")]
        public string Texts
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
                if (value)
                    CheckState = CheckStates.Checked;
                else
                    CheckState = CheckStates.Unchecked;
                if (CheckedChanged != null)
                    CheckedChanged(this, value);
            }
        }

        [Description("选中状态"), Category("Appearance")]
        public CheckStates CheckState
        {
            get
            {
                return m_CheckState;
            }
            set
            {
                m_CheckState = value;
                switch (value)
                {
                    case CheckStates.Unchecked:
                        m_Checked = false;
                        Bmp = ResClass.GetImgRes("cb_n_e");
                        break;
                    case CheckStates.Checked:
                        m_Checked = true;
                        Bmp = ResClass.GetImgRes("cb_c_e");
                        break;
                    case CheckStates.Indeterminate:
                        m_Checked = true;
                        Bmp = ResClass.GetImgRes("cb_b_e");
                        break;
                }
                this.Invalidate(new Rectangle(0, (Height - 15) / 2, 15, 15));
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Bmp != null)
            {
                g = e.Graphics;
                g.DrawImage(Bmp, new Rectangle(0, (this.Height - 15) / 2, 15, 15), 0, 0, Bmp.Width, Bmp.Height, GraphicsUnit.Pixel);
                g.DrawString(m_buttonText, this.Font, Brushes.Black, 16, (this.Height - 15) / 2);
            }
            System.GC.Collect();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.MinimumSize = new Size(15, 15);
            System.GC.Collect();
        }

        protected override void OnClick(EventArgs e)
        {
            if (!Checked)
            {
                Checked = true;

            }
            else
            {
                Checked = false;
            }
            System.GC.Collect();
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            if (!Checked)
            {
                Checked = true;

            }
            else
            {
                Checked = false;
            }
            System.GC.Collect();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (Checked)
            {
                switch (CheckState)
                {
                    case CheckStates.Checked:
                        Bmp = ResClass.GetImgRes("cb_c_e");
                        break;
                    case CheckStates.Indeterminate:
                        Bmp = ResClass.GetImgRes("cb_b_e");
                        break;
                    default:
                        Bmp = ResClass.GetImgRes("cb_c_e");
                        break;
                }
            }
            else
            {
                Bmp = ResClass.GetImgRes("cb_n_e");
            }
            this.Invalidate(new Rectangle(0,(Height - 15)/2,15,15));
            System.GC.Collect();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (Checked)
            {
                switch (CheckState)
                {
                    case CheckStates.Checked:
                        Bmp = ResClass.GetImgRes("cb_c_l");
                        break;
                    case CheckStates.Indeterminate:
                        Bmp = ResClass.GetImgRes("cb_b_l");
                        break;
                    default:
                        Bmp = ResClass.GetImgRes("cb_c_l");
                        break;
                }
            }
            else
            {
                Bmp = ResClass.GetImgRes("cb_n_l");
            }
            this.Invalidate(new Rectangle(0, (Height - 15) / 2, 15, 15));
            System.GC.Collect();
        }

    }
}
