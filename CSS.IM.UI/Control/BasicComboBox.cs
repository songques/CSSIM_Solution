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
    public partial class BasicComboBox : UserControl
    {

        private System.Drawing.Graphics g;
        private Bitmap bmp,img;
        private Panel p;
        private bool showed = false;

        public delegate void SelectIndexChangeEventHandler(object sender, int index);
        public event SelectIndexChangeEventHandler SelectIndexChange;

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

            if (img != null)
            {
                img.Dispose();
                img = null;
            }

            if (p != null)
            {
                p.Dispose();
                p = null;
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

        public BasicComboBox()
        {
            bmp = ResClass.GetImgRes("frameBorderEffect_normalDraw");
            img = ResClass.GetImgRes("combobox_buttonNormalDraw");
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
            g.DrawImage(bmp, new Rectangle(4, 0, this.Width - 8, 4), 4, 0, bmp.Width - 8, 4, GraphicsUnit.Pixel);
            g.DrawImage(bmp, new Rectangle(this.Width - 4, 0, 4, 4), bmp.Width - 4, 0, 4, 4, GraphicsUnit.Pixel);

            g.DrawImage(bmp, new Rectangle(0, 4, 4, 16), 0, 4, 4, bmp.Height - 8, GraphicsUnit.Pixel);
            g.DrawImage(bmp, new Rectangle(this.Width - 4, 4, 4, 16), bmp.Width - 4, 4, 4, bmp.Height - 6, GraphicsUnit.Pixel);

            g.DrawImage(bmp, new Rectangle(0, 20, 2, 2), 0, bmp.Height - 2, 2, 2, GraphicsUnit.Pixel);
            g.DrawImage(bmp, new Rectangle(2, 20, this.Width - 2, 2), 2, bmp.Height - 2, bmp.Width - 4, 2, GraphicsUnit.Pixel);
            g.DrawImage(bmp, new Rectangle(this.Width - 2, 20, 2, 2), bmp.Width - 2, bmp.Height - 2, 2, 2, GraphicsUnit.Pixel);

            g.DrawImage(img, new Rectangle(this.Width - 22, 2, 20, 18), 0, 0, 20, 18, GraphicsUnit.Pixel);
            if (this.Height != 22)
                g.DrawRectangle(new Pen(Brushes.DodgerBlue, 1F), 0, 22, this.Width - 2, this.Height - 24);
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
                img = ResClass.GetImgRes("combobox_buttonHighlightDraw");
                this.Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!showed)
            {
                bmp = ResClass.GetImgRes("frameBorderEffect_normalDraw");
                img = ResClass.GetImgRes("combobox_buttonNormalDraw");
                this.Invalidate();
            }
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            this.Leave += new EventHandler(p_Leave);
            img = ResClass.GetImgRes("combobox_buttonPushedDraw");
            if (p != null)
                p.Dispose();
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
                    ComboBoxItem item = new ComboBoxItem();
                    item.Name = "comboBoxItem_" + i;
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
                if (this.Height == 22)
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
                    this.Height = 22;
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
                this.Height = 22;
                showed = false;
                OnMouseLeave(null);
            }
        }

        private void item_MouseClick(object sender, MouseEventArgs e)
        {
            ComboBoxItem ci = sender as ComboBoxItem;
            Texts = ci.Texts;
            SelectIndex = int.Parse(ci.Name.Substring(13));
            SelectItem = SelectText = ci.Texts;
            this.Height = 22;
            showed = false;
            OnMouseLeave(null);
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
                textBox1.Text = "";
                this.Invalidate();
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
                        //if (SelectIndexChange != null)
                        //    SelectIndexChange(this, v);
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
                        //if (SelectIndexChange != null)
                        //    SelectIndexChange(this, v);
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
            System.GC.Collect();
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(null);
            System.GC.Collect();
        }

        private void BasicComboBox_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
            System.GC.Collect();
        }

        private void BasicComboBox_Leave(object sender, EventArgs e)
        {
            this.Height = 22;
            showed = false;
            OnMouseLeave(null);
            System.GC.Collect();
        }

    }

    public class ComboBoxItem : UserControl
    {
        private System.Drawing.Graphics g1 = null;
        private Bitmap bmp1 = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {

            if (bmp1 != null)
            {
                bmp1.Dispose();
                bmp1 = null;
            }

            if (g1 != null)
            {
                g1.Dispose();
                g1 = null;
            }

            base.Dispose(disposing);

            System.GC.Collect();
        }

        public ComboBoxItem()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.White;
            bmp1 = ResClass.GetImgRes("menu_highlight");
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
            System.GC.Collect();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            g1 = this.CreateGraphics();
            g1.DrawImage(bmp1, new Rectangle(0, 0, 3, this.Height), 0, 0, 3, 20, GraphicsUnit.Pixel);
            g1.DrawImage(bmp1, new Rectangle(3, 0, this.Width - 6, this.Height), 3, 0, bmp1.Width - 6, 20, GraphicsUnit.Pixel);
            g1.DrawImage(bmp1, new Rectangle(this.Width - 3, 0, 3, this.Height), bmp1.Width - 3, 0, 3, 20, GraphicsUnit.Pixel);
            g1.DrawString(Texts, new Font("微软雅黑", 9F), Brushes.White, new Point(0, 3));
            System.GC.Collect();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.Invalidate();
            System.GC.Collect();
        }
    }
}
