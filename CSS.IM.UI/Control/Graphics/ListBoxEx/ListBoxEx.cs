using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using CSS.IM.UI.Control.Graphics.Win32;
using CSS.IM.UI.Control.Graphics.Win32.Struct;

namespace CSS.IM.UI.Control.Graphics.ListBoxEx
{

    [ToolboxBitmap(typeof(ListBox))]
    public class ListBoxEx : ListBox
    {
        #region Fields

        private Color _rowBackColor1 = Color.White;
        private Color _rowBackColor2 = Color.FromArgb(254, 216, 249);
        private Color _selectedColor = Color.FromArgb(102, 206, 255);
        private Color _borderColor = Color.FromArgb(55, 126, 168);
        private ListBoxExItemCollection _items;

        #endregion

        #region Constructors

        public ListBoxEx()
            : base()
        {
            _items = new ListBoxExItemCollection(this);
            base.DrawMode = DrawMode.OwnerDrawFixed;
        }

        #endregion

        #region Properties

        [Localizable(true)]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Content)]
        public new ListBoxExItemCollection Items
        {
            get { return _items; }
        }

        [DefaultValue(typeof(Color),"White")]
        public Color RowBackColor1
        {
            get { return _rowBackColor1; }
            set
            {
                _rowBackColor1 = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "254, 216, 249")]
        public Color RowBackColor2
        {
            get { return _rowBackColor2; }
            set
            {
                _rowBackColor2 = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "102, 206, 255")]
        public Color SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "55, 126, 168")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                base.Invalidate(true);
            }
        }

        internal ListBox.ObjectCollection OldItems
        {
            get { return base.Items; }
        }

        private RECT AbsoluteClientRECT
        {
            get
            {
                RECT lpRect = new RECT();
                CreateParams createParams = CreateParams;
                NativeMethods.AdjustWindowRectEx(
                    ref lpRect,
                    createParams.Style,
                    false,
                    createParams.ExStyle);
                int left = -lpRect.Left;
                int right = -lpRect.Top;
                NativeMethods.GetClientRect(
                    base.Handle,
                    ref lpRect);

                lpRect.Left += left;
                lpRect.Right += left;
                lpRect.Top += right;
                lpRect.Bottom += right;
                return lpRect;
            }
        }

        private Rectangle AbsoluteClientRectangle
        {
            get
            {
                RECT absoluteClientRECT = AbsoluteClientRECT;

                Rectangle rect = Rectangle.FromLTRB(
                    absoluteClientRECT.Left,
                    absoluteClientRECT.Top,
                    absoluteClientRECT.Right,
                    absoluteClientRECT.Bottom);
                CreateParams cp = base.CreateParams;
                bool bHscroll = (cp.Style &
                    (int)NativeMethods.WindowStyle.WS_HSCROLL) != 0;
                bool bVscroll = (cp.Style &
                    (int)NativeMethods.WindowStyle.WS_VSCROLL) != 0;

                if (bHscroll)
                {
                    rect.Height += SystemInformation.HorizontalScrollBarHeight;
                }

                if (bVscroll)
                {
                    rect.Width += SystemInformation.VerticalScrollBarWidth;
                }

                return rect;
            }
        }

        #endregion

        #region Override Methods

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (e.Index != -1 && base.Items.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine(e.State);
                Rectangle bounds = e.Bounds;
                ListBoxExItem item = Items[e.Index];
                System.Drawing.Graphics g = e.Graphics;

                if ((e.State & DrawItemState.Selected)
                    == DrawItemState.Selected)
                {
                    RenderBackgroundInternal(
                        g,
                        bounds,
                        _selectedColor,
                        _selectedColor,
                        Color.FromArgb(200, 255, 255, 255),
                        0.45f,
                        true,
                        LinearGradientMode.Vertical);
                }
                else
                {
                    Color backColor;
                    if (e.Index % 2 == 0)
                    {
                        backColor = _rowBackColor2;
                    }
                    else
                    {
                        backColor = _rowBackColor1;
                    }
                    using (SolidBrush brush = new SolidBrush(backColor))
                    {
                        g.FillRectangle(brush, bounds);
                    }
                }

                Image image = item.Image;

                Rectangle imageRect = new Rectangle(
                    bounds.X + 2,
                    bounds.Y + 2,
                    bounds.Height - 4,
                    bounds.Height - 4);
                Rectangle textRect = new Rectangle(
                    imageRect.Right + 2,
                    bounds.Y,
                    bounds.Width - imageRect.Right - 2,
                    bounds.Height);

                string text = item.ToString();
                TextFormatFlags formatFlags = 
                    TextFormatFlags.VerticalCenter;
                if (RightToLeft == RightToLeft.Yes)
                {
                    imageRect.X = bounds.Right - imageRect.Right;
                    textRect.X = bounds.Right - textRect.Right;
                    formatFlags |= TextFormatFlags.RightToLeft;
                    formatFlags |= TextFormatFlags.Right;
                }
                else
                {
                    formatFlags |= TextFormatFlags.Left;
                }

                if (image != null)
                {
                    g.InterpolationMode = 
                        InterpolationMode.HighQualityBilinear;
                    g.DrawImage(
                        image,
                        imageRect,
                        0,
                        0,
                        image.Width,
                        image.Height,
                        GraphicsUnit.Pixel);
                }

                TextRenderer.DrawText(
                    g,
                    text,
                    Font,
                    textRect,
                    ForeColor,
                    formatFlags);

                if ((e.State & DrawItemState.Focus) ==
                    DrawItemState.Focus)
                {
                    e.DrawFocusRectangle();
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)NativeMethods.WindowsMessgae.WM_NCPAINT:
                    WmNcPaint(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

        #region Windows Message Methods

        private void WmNcPaint(ref Message m)
        {
            base.WndProc(ref m);
            if (base.BorderStyle == BorderStyle.None)
            {
                return;
            }

            IntPtr hDC = NativeMethods.GetWindowDC(m.HWnd);
            if (hDC == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
            try
            {
                Color backColor = BackColor;
                Color borderColor = _borderColor;

                Rectangle bounds = new Rectangle(0, 0, Width, Height);
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromHdc(hDC))
                {
                    using (Region region = new Region(bounds))
                    {
                        region.Exclude(AbsoluteClientRectangle);
                        using (Brush brush = new SolidBrush(backColor))
                        {
                            g.FillRegion(brush, region);
                        }
                    }

                    ControlPaint.DrawBorder(
                        g,
                        bounds,
                        borderColor,
                        ButtonBorderStyle.Solid);
                }
            }
            finally
            {
                NativeMethods.ReleaseDC(m.HWnd, hDC);
            }
            m.Result = IntPtr.Zero;
        }

        #endregion

        #region Draw Help Methods

        internal void RenderBackgroundInternal(
          System.Drawing.Graphics g,
          Rectangle rect,
          Color baseColor,
          Color borderColor,
          Color innerBorderColor,
          float basePosition,
          bool drawBorder,
          LinearGradientMode mode)
        {
            if (drawBorder)
            {
                rect.Width--;
                rect.Height--;
            }
            using (LinearGradientBrush brush = new LinearGradientBrush(
               rect, Color.Transparent, Color.Transparent, mode))
            {
                Color[] colors = new Color[4];
                colors[0] = GetColor(baseColor, 0, 35, 24, 9);
                colors[1] = GetColor(baseColor, 0, 13, 8, 3);
                colors[2] = baseColor;
                colors[3] = GetColor(baseColor, 0, 68, 69, 54);

                ColorBlend blend = new ColorBlend();
                blend.Positions = new float[] { 0.0f, basePosition, basePosition + 0.05f, 1.0f };
                blend.Colors = colors;
                brush.InterpolationColors = blend;
                g.FillRectangle(brush, rect);
            }
            if (baseColor.A > 80)
            {
                Rectangle rectTop = rect;
                if (mode == LinearGradientMode.Vertical)
                {
                    rectTop.Height = (int)(rectTop.Height * basePosition);
                }
                else
                {
                    rectTop.Width = (int)(rect.Width * basePosition);
                }
                using (SolidBrush brushAlpha =
                    new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                {
                    g.FillRectangle(brushAlpha, rectTop);
                }
            }

            if (drawBorder)
            {
                using (Pen pen = new Pen(borderColor))
                {
                    g.DrawRectangle(pen, rect);
                }

                rect.Inflate(-1, -1);
                using (Pen pen = new Pen(innerBorderColor))
                {
                    g.DrawRectangle(pen, rect);
                }
            }
        }

        private Color GetColor(
            Color colorBase, int a, int r, int g, int b)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a + a0 > 255) { a = 255; } else { a = Math.Max(a + a0, 0); }
            if (r + r0 > 255) { r = 255; } else { r = Math.Max(r + r0, 0); }
            if (g + g0 > 255) { g = 255; } else { g = Math.Max(g + g0, 0); }
            if (b + b0 > 255) { b = 255; } else { b = Math.Max(b + b0, 0); }

            return Color.FromArgb(a, r, g, b);
        }

        #endregion
    }
}
