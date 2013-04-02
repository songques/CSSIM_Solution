using System;
namespace CSS.IM.UI.Control.Graphics.ScrollBar
{

    public interface IScrollBarPaint
    {
        void OnPaintScrollBarArrow(PaintScrollBarArrowEventArgs e);
        void OnPaintScrollBarThumb(PaintScrollBarThumbEventArgs e);
        void OnPaintScrollBarTrack(PaintScrollBarTrackEventArgs e);
    }
}
