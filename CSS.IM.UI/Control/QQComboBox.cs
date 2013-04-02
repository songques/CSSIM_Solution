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
    [DefaultEvent("SelectIndexChange")]
    public partial class QQComboBox : UserControl
    {

        private System.Drawing.Graphics g = null;
        private Bitmap bmp,img = null;
        private bool showed = false;

        public delegate void SelectIndexChangeEventHandler(object sender, int index);
        public event SelectIndexChangeEventHandler SelectIndexChange;

        public QQComboBox()
        {
            bmp = ResClass.GetImgRes("frameBorderEffect_normalDraw");
            img = ResClass.GetImgRes("login_inputbtn_normal");
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            InitializeComponent();
            textBox1.Text = Texts;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;
            g.DrawImage(bmp, new Rectangle(0, 0, 4, 4), 0, 0, 4, 4, GraphicsUnit.Pixel);
            g.DrawImage(bmp, new Rectangle(4, 0, Width - 8, 4), 4, 0, bmp.Width - 8, 4, GraphicsUnit.Pixel);
            g.DrawImage(bmp, new Rectangle(Width - 4, 0, 4, 4), bmp.Width - 4, 0, 4, 4, GraphicsUnit.Pixel);

            g.DrawImage(bmp, new Rectangle(0, 4, 4, Height - 6), 0, 4, 4, bmp.Height - 8, GraphicsUnit.Pixel);
            g.DrawImage(bmp, new Rectangle(Width - 4, 4, 4, Height - 6), bmp.Width - 4, 4, 4, bmp.Height - 6, GraphicsUnit.Pixel);

            g.DrawImage(bmp, new Rectangle(0, Height - 2, 2, 2), 0, bmp.Height - 2, 2, 2, GraphicsUnit.Pixel);
            g.DrawImage(bmp, new Rectangle(2, Height - 2, Width - 2, 2), 2, bmp.Height - 2, bmp.Width - 4, 2, GraphicsUnit.Pixel);
            g.DrawImage(bmp, new Rectangle(Width - 2, Height - 2, 2, 2), bmp.Width - 2, bmp.Height - 2, 2, 2, GraphicsUnit.Pixel);

            g.DrawImage(img, new Rectangle(Width - 25, 2, 23, 22), 0, 0, 23, 22, GraphicsUnit.Pixel);
            if (this.Height != 26)
                g.DrawRectangle(new Pen(Brushes.DodgerBlue, 1F), 0, Height, this.Width - 2, this.Height - 24);
            if (DropDownStyle != ComboBoxStyle.DropDown && Texts != null && Texts.Length > 0)
            {
                g.DrawString(Texts, new Font("微软雅黑", 9F), this.Enabled?Brushes.Black:Brushes.Gray, new Point(2, 4));
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!showed)
            {
                bmp = ResClass.GetImgRes("frameBorderEffect_mouseDownDraw");
                img = ResClass.GetImgRes("login_inputbtn_highlight");
                this.Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!showed)
            {
                bmp = ResClass.GetImgRes("frameBorderEffect_normalDraw");
                img = ResClass.GetImgRes("login_inputbtn_normal");
                this.Invalidate();
            }
        }
        private Panel p = null;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            this.Leave += new EventHandler(p_Leave);
            img = ResClass.GetImgRes("login_inputbtn_down");
            if (Items != null)
            {
                p = new Panel();
                p.Left = 2;
                p.Top = 24;
                p.Width = this.Width - 5;
                p.Height = Items.Length * 20;
                p.AutoScroll = true;

                p.Focus();
                p.Leave+=new EventHandler(p_Leave);

                bool isScroll = false;

                int maxHeight = this.Parent.Height - this.Top - 24;
                int h = 20 * Items.Length + 5;
                if (h > maxHeight)
                {
                    isScroll = true;
                }
                else
                {
                    isScroll = false;
                }

                for (int i = 0; i < Items.Length; i++)
                {
                    QQComboBoxItem item = new QQComboBoxItem();
                    item.Name = "qqcomboBoxItem_" + i;
                    item.Texts = Items[i];
                    item.Left = 0;
                    item.Top = 20 * i;
                    if (isScroll)
                        item.Width = p.Width - 20;
                    else
                        item.Width = p.Width;
                    item.Height = 20;
                    item.MouseClick += new MouseEventHandler(item_MouseClick);
                    p.Controls.Add(item);
                }
                this.Controls.Add(p);
                if (this.Height == 26)
                {
                    if (isScroll)
                    {
                        this.Height += 20 * (maxHeight / 20) + 5;
                        p.Height = 20 * (maxHeight / 20);
                    }
                    else
                    {
                        this.Height += h;
                        p.Height = h - 5;
                    }
                    showed = true;
                }
                else
                {
                    this.Height = 26;
                    p.Height = 0;
                    showed = false;
                    p.Controls.Remove(p);
                    p.Dispose();
                }
                ControlCollection cce = this.Parent.Controls;
                for (int i = 0; i < cce.Count; i++)
                {
                    if (cce[i] is BasicComboBox && !cce[i].Name.Equals(this.Name))
                        (cce[i] as BasicComboBox).CloseListPanel();
                }
            }
            this.BringToFront();
            this.Invalidate();
        }

        private void p_Leave(object sender, EventArgs e)
        {
            CloseListPanel();
        }

        public void CloseListPanel()
        {
            if (showed)
            {
                this.Height = 26;
                showed = false;
                OnMouseLeave(null);
            }
        }

        private void item_MouseClick(object sender, MouseEventArgs e)
        {
            QQComboBoxItem ci = sender as QQComboBoxItem;
            Texts = ci.Texts;
            SelectIndex = int.Parse(ci.Name.Substring(13));
            SelectItem = SelectText = ci.Texts;
            this.Height = 26;
            showed = false;
            OnMouseLeave(null);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.Height = 26;
            showed = false;
            OnMouseLeave(null);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Height = 26;
            int d = this.Width - 52;
            textBox1.Width = d; 
        }

        private string[] ss;
        [Description("集合"), Category("Appearance")]
        public string[] Items
        {
            get 
            {
                return ss;
            }
            set
            {
                ss = value;
            }
        }

        private string text1;
        [Description("文本"), Category("Appearance")]
        public string Texts
        {
            get
            {
                if (DropDownStyle != ComboBoxStyle.DropDown)
                    return text1;
                else
                    return textBox1.Text;
            }
            set
            {
                text1 = value;
                textBox1.Text = value;
            }
        }

        private int selectindex;
        [Description("当前选择的项的索引"), Category("Appearance")]
        public int SelectIndex
        {
            get
            {
                return selectindex;
            }
            set
            {
                selectindex = value;
                if (Items!=null&&Items.Length > 0)
                {
                    textBox1.Text = Texts = Items[value];
                    if (SelectIndexChange!=null)
                        SelectIndexChange(this, value);
                }
                this.Invalidate();
            }
        }

        private string selecttext;
        [Description("当前选择的项的值"), Category("Appearance")]
        public string SelectText
        {
            get
            {
                return selecttext;
            }
            set
            {
                selecttext = value;
                if (value != null && Items != null && Items.Length > 0)
                {
                    int v = GetID(value);
                    if (v!=-1)
                    {
                        textBox1.Text = Texts = value;
                        if (SelectIndexChange != null)
                            SelectIndexChange(this, v);
                    }
                }
                this.Invalidate();
            }
        }

        private object selectitem;
        [Description("当前选择的项"), Category("Appearance")]
        public object SelectItem
        {
            get
            {
                return selectitem;
            }
            set
            {
                selectitem = value;
                if (value!=null && Items != null && Items.Length > 0)
                {
                    int v = GetID(value.ToString());
                    if (v != -1)
                    {
                        textBox1.Text = Texts = value.ToString();
                        if (SelectIndexChange != null)
                            SelectIndexChange(this, v);
                    }
                }
                this.Invalidate();
            }
        }

        private int GetID(string value)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (value == Items[i])
                    return i;
            }
            return -1;
        }

        private ComboBoxStyle dropDownstyle = ComboBoxStyle.DropDownList;
        [Description("下拉风格"), Category("Appearance")]
        public ComboBoxStyle DropDownStyle
        {
            get
            {
                return dropDownstyle;
            }
            set
            {
                dropDownstyle = value;
                if (value == ComboBoxStyle.DropDown)
                    textBox1.Visible = true;
                else
                    textBox1.Visible = false;
            }
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(null);
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(null);
        }

    }

    public class QQComboBoxItem : UserControl
    {
        private System.Drawing.Graphics g1 = null;
        private Bitmap bmp1 = null;

        public QQComboBoxItem()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.White;
            bmp1 = ResClass.GetImgRes("mune_select_bkg");
        }

        private string text1;
        [Description("文本"), Category("Appearance")]
        public string Texts
        {
            get
            {
                return text1;
            }
            set
            {
                text1 = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            g1 = e.Graphics;
            g1.Clear(Color.White);
            g1.DrawString(Texts, new Font("微软雅黑", 9F), Brushes.DodgerBlue, new Point(0, 3));
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            g1 = this.CreateGraphics();
            g1.DrawImage(bmp1, new Rectangle(0, 0, 3, this.Height), 0, 0, 3, 20, GraphicsUnit.Pixel);
            g1.DrawImage(bmp1, new Rectangle(3, 0, this.Width - 6, this.Height), 3, 0, bmp1.Width - 6, 20, GraphicsUnit.Pixel);
            g1.DrawImage(bmp1, new Rectangle(this.Width - 3, 0, 3, this.Height), bmp1.Width - 3, 0, 3, 20, GraphicsUnit.Pixel);
            g1.DrawString(Texts, new Font("微软雅黑", 9F), Brushes.White, new Point(0, 3));
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.Invalidate(); 
        }
    }
}
