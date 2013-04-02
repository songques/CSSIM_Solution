using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using CSS.IM.UI.Control.Graphics.Imaging;
using System.Drawing.Drawing2D;

namespace CSS.IM.UI.Control.Graphics.ScrollBar
{
    /* 作者：Starts_2000
     * 日期：2010-07-30
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    public class HScrollBarEx : HScrollBar, IScrollBarPaint
    {
        private ScrollBarManager _manager;
        private ScrollBarColorTable _colorTable;

        public HScrollBarEx()
            : base()
        {
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ScrollBarColorTable ColorTable
        {
            get 
            {
                if (_colorTable == null)
                {
                    _colorTable = new ScrollBarColorTable();
                }
                return _colorTable;
            }
            set
            {
                _colorTable = value;
                base.Invalidate();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (_manager != null)
            {
                _manager.Dispose();
            }

            if (!base.DesignMode)
            {
                _manager = new ScrollBarManager(this);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_manager != null)
                {
                    _manager.Dispose();
                    _manager = null;
                }
            }
            base.Dispose(disposing);
        }

        protected virtual void OnPaintScrollBarTrack(
            PaintScrollBarTrackEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            Rectangle rect = e.TrackRectangle;

            Color baseColor = GetGray(ColorTable.Base);

            ControlPaintEx.DrawScrollBarTrack(
                g, rect, baseColor, Color.White, e.Orientation);
        }

        protected virtual void OnPaintScrollBarArrow(
           PaintScrollBarArrowEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            Rectangle rect = e.ArrowRectangle;
            ControlState controlState = e.ControlState;
            ArrowDirection direction = e.ArrowDirection;
            bool bHorizontal = e.Orientation == Orientation.Horizontal;
            bool bEnabled = e.Enabled;

            Color backColor = ColorTable.BackNormal;
            Color baseColor = ColorTable.Base;
            Color borderColor = ColorTable.Border;
            Color innerBorderColor = ColorTable.InnerBorder;
            Color foreColor = ColorTable.Fore;

            bool changeColor = false;

            if (bEnabled)
            {
                switch (controlState)
                {
                    case ControlState.Hover:
                        baseColor = ColorTable.BackHover;
                        break;
                    case ControlState.Pressed:
                        baseColor = ColorTable.BackPressed;
                        changeColor = true;
                        break;
                    default:
                        baseColor = ColorTable.Base;
                        break;
                }
            }
            else
            {
                backColor = GetGray(backColor);
                baseColor = GetGray(ColorTable.Base);
                borderColor = GetGray(borderColor);
                foreColor = GetGray(foreColor);
            }

            using (SmoothingModeGraphics sg = new SmoothingModeGraphics(g))
            {
                ControlPaintEx.DrawScrollBarArraw(
                    g,
                    rect,
                    baseColor,
                    backColor,
                    borderColor,
                    innerBorderColor,
                    foreColor,
                    e.Orientation,
                    direction,
                    changeColor);
            }
        }

        protected virtual void OnPaintScrollBarThumb(
           PaintScrollBarThumbEventArgs e)
        {
            bool bEnabled = e.Enabled;
            if (!bEnabled)
            {
                return;
            }

            System.Drawing.Graphics g = e.Graphics;
            Rectangle rect = e.ThumbRectangle;
            ControlState controlState = e.ControlState;

            Color backColor = ColorTable.BackNormal;
            Color baseColor = ColorTable.Base;
            Color borderColor = ColorTable.Border;
            Color innerBorderColor = ColorTable.InnerBorder;

            bool changeColor = false;

            switch (controlState)
            {
                case ControlState.Hover:
                    baseColor = ColorTable.BackHover;
                    break;
                case ControlState.Pressed:
                    baseColor = ColorTable.BackPressed;
                    changeColor = true;
                    break;
                default:
                    baseColor = ColorTable.Base;
                    break;
            }

            using (SmoothingModeGraphics sg = new SmoothingModeGraphics(g))
            {
                ControlPaintEx.DrawScrollBarThumb(
                    g,
                    rect,
                    baseColor,
                    backColor,
                    borderColor,
                    innerBorderColor,
                    e.Orientation,
                    changeColor);
            }
        }

        private Color GetGray(Color color)
        {
            return ColorConverterEx.RgbToGray(
                new RGB(color)).Color;
        }

        #region IScrollBarPaint 成员

        void IScrollBarPaint.OnPaintScrollBarArrow(PaintScrollBarArrowEventArgs e)
        {
            OnPaintScrollBarArrow(e);
        }

        void IScrollBarPaint.OnPaintScrollBarThumb(PaintScrollBarThumbEventArgs e)
        {
            OnPaintScrollBarThumb(e);
        }

        void IScrollBarPaint.OnPaintScrollBarTrack(PaintScrollBarTrackEventArgs e)
        {
            OnPaintScrollBarTrack(e);
        }

        #endregion
    }
}
