using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using CSS.IM.UI.Control.Graphics.Win32;
using CSS.IM.UI.Control.Graphics.Win32.Struct;
using CSS.IM.UI.Control.Graphics.Win32.Const;

namespace CSS.IM.UI.Control.Graphics.ProgressBarEx
{


    [ToolboxBitmap(typeof(ProgressBar))]
    public class ProgressBarEx : ProgressBar
    {
        #region Fields

        private BufferedGraphicsContext _context;
        private BufferedGraphics _bufferedGraphics;
        private ProgressBarColorTable _colorTable;
        private bool _bPainting = false;
        private int _trackX = -100;
        private Timer _timer;
        private string _formatString = "{0:0.0%}";

        private const int Internal = 10;
        private const int MarqueeWidth = 100;

        #endregion

        #region Constructors

        public ProgressBarEx()
            : base()
        {
            _context = BufferedGraphicsManager.Current;  
            _context.MaximumBuffer = new Size(Width+1, Height+1);
            _bufferedGraphics = _context.Allocate(
                CreateGraphics(),
                new Rectangle(Point.Empty, Size));
            SetRegion();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 显示进度完成信息的格式化字符串。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("{0:0.0%}")]
        public string FormatString
        {
            get { return _formatString; }
            set
            {
                if (_formatString != value)
                {
                    _formatString = value;
                    base.Invalidate();
                }
            }
        }

        public new ProgressBarStyle Style
        {
            get { return base.Style; }
            set
            {
                if (base.Style != value)
                {
                    base.Style = value;

                    if (value == ProgressBarStyle.Marquee)
                    {
                        if (_timer != null)
                        {
                            _timer.Dispose();
                        }

                        _timer = new Timer();
                        _timer.Interval = Internal;
                        _timer.Tick += delegate(object sender, EventArgs e)
                        {
                            _trackX += (int)Math.Ceiling((float)Width / base.MarqueeAnimationSpeed);
                            if (_trackX > Width)
                            {
                                _trackX = -MarqueeWidth;
                            }
                            base.Invalidate();
                        };

                        if (!base.DesignMode)
                        {
                            _timer.Start();
                        }
                    }
                    else
                    {
                        if (_timer != null)
                        {
                            _timer.Dispose();
                            _timer = null;
                        }
                    }
                }
            }
        }

        [Browsable(true)]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [Browsable(false)]
        public ProgressBarColorTable ColorTable
        {
            get
            {
                if (_colorTable == null)
                {
                    _colorTable = new ProgressBarColorTable();
                }
                return _colorTable;
            }
            set
            {
                _colorTable = value;
                base.Invalidate();
            }
        }

        #endregion

        #region Override Methods

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            SetRegion();

            _context.MaximumBuffer = new Size(Width + 1, Height + 1);
            if (_bufferedGraphics != null)
            {
                _bufferedGraphics.Dispose();
                _bufferedGraphics = null;
            }

            _bufferedGraphics = _context.Allocate(
                CreateGraphics(),
                new Rectangle(Point.Empty, Size));
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM.WM_PAINT:
                    if (!_bPainting)
                    {
                        _bPainting = true;

                        PAINTSTRUCT ps = new PAINTSTRUCT();

                        NativeMethods.BeginPaint(m.HWnd, ref ps);

                        try
                        {
                            DrawProgressBar(m.HWnd);
                        }
                        catch
                        {
                        }

                        NativeMethods.ValidateRect(m.HWnd, ref ps.rcPaint);
                        NativeMethods.EndPaint(m.HWnd, ref ps);

                        _bPainting = false;
                        m.Result = Result.TRUE;
                    }
                    else
                    {
                        base.WndProc(ref m);
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
                }

                if (_bufferedGraphics != null)
                {
                    _bufferedGraphics.Dispose();
                    _bufferedGraphics = null;
                }

                if (_context != null)
                {
                    _context = null;
                }

                _colorTable = null;
            }
        }

        #endregion

        #region Help Methods

        private void DrawProgressBar(IntPtr hWnd)
        {
            System.Drawing.Graphics g = _bufferedGraphics.Graphics;
            Rectangle rect = new Rectangle(Point.Empty, Size);
            ProgressBarColorTable colorTable = ColorTable;

            bool bBlock = Style != ProgressBarStyle.Marquee || base.DesignMode;
            float basePosition = bBlock ? .30f : .45f;

            SmoothingModeGraphics sg = new SmoothingModeGraphics(g);

            RenderHelper.RenderBackgroundInternal(
                g,
                rect,
                colorTable.TrackBack,
                colorTable.Border,
                colorTable.InnerBorder,
                RoundStyle.All,
                8,
                basePosition,
                true,
                true,
                LinearGradientMode.Vertical);

            Rectangle trackRect = rect;
            trackRect.Inflate(-2, -2);

            if (bBlock)
            {
                trackRect.Width = (int)(((double)Value / (Maximum - Minimum)) * trackRect.Width);

                RenderHelper.RenderBackgroundInternal(
                    g,
                    trackRect,
                    colorTable.TrackFore,
                    colorTable.Border,
                    colorTable.InnerBorder,
                    RoundStyle.All,
                    8,
                    basePosition,
                    false,
                    true,
                    LinearGradientMode.Vertical);

                if (!string.IsNullOrEmpty(_formatString))
                {
                    TextRenderer.DrawText(
                        g,
                        string.Format(_formatString, (double)Value / (Maximum - Minimum)),
                        base.Font,
                        rect,
                        base.ForeColor,
                        TextFormatFlags.VerticalCenter |
                        TextFormatFlags.HorizontalCenter |
                        TextFormatFlags.SingleLine |
                        TextFormatFlags.WordEllipsis);
                }
            }
            else
            {
                GraphicsState state = g.Save();

                g.SetClip(trackRect);

                trackRect.X = _trackX;
                trackRect.Width = MarqueeWidth;

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(trackRect);
                    g.SetClip(path, CombineMode.Intersect);
                }

                RenderHelper.RenderBackgroundInternal(
                    g,
                    trackRect,
                    colorTable.TrackFore,
                    colorTable.Border,
                    colorTable.InnerBorder,
                    RoundStyle.None,
                    8,
                    basePosition,
                    false,
                    false,
                    LinearGradientMode.Vertical);

                using (LinearGradientBrush brush = new LinearGradientBrush(
                    trackRect, colorTable.InnerBorder, Color.Transparent, 0f))
                {
                    Blend blend = new Blend();
                    blend.Factors = new float[] { 0f, 1f, 0f };
                    blend.Positions = new float[] { 0f, .5f, 1f };
                    brush.Blend = blend;

                    g.FillRectangle(brush, trackRect);
                }

                g.Restore(state);
            }

            sg.Dispose();

            IntPtr hDC = NativeMethods.GetDC(hWnd);
            _bufferedGraphics.Render(hDC);
            NativeMethods.ReleaseDC(hWnd, hDC);
        }

        private void SetRegion()
        {
            RegionHelper.CreateRegion(this, new Rectangle(Point.Empty, Size));
        }

        #endregion
    }
}
