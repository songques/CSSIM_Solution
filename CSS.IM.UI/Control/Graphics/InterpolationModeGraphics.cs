using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace CSS.IM.UI.Control.Graphics
{


    public class InterpolationModeGraphics : IDisposable
    {
        private InterpolationMode _oldMode;
        private System.Drawing.Graphics _graphics;

        public InterpolationModeGraphics(System.Drawing.Graphics graphics)
            : this(graphics, InterpolationMode.HighQualityBicubic)
        {
        }

        public InterpolationModeGraphics(
            System.Drawing.Graphics graphics, InterpolationMode newMode)
        {
            _graphics = graphics;
            _oldMode = graphics.InterpolationMode;
            graphics.InterpolationMode = newMode;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _graphics.InterpolationMode = _oldMode;
        }

        #endregion
    }
}
