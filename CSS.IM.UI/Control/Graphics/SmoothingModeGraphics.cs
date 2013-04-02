using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CSS.IM.UI.Control.Graphics
{

    public class SmoothingModeGraphics : IDisposable
    {
        private SmoothingMode _oldMode;
        private System.Drawing.Graphics _graphics;

        public SmoothingModeGraphics(System.Drawing.Graphics graphics)
            : this(graphics, SmoothingMode.AntiAlias)
        {
        }

        public SmoothingModeGraphics(System.Drawing.Graphics graphics, SmoothingMode newMode)
        {
            _graphics = graphics;
            _oldMode = graphics.SmoothingMode;
            graphics.SmoothingMode = newMode;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _graphics.SmoothingMode = _oldMode;
        }

        #endregion
    }
}
