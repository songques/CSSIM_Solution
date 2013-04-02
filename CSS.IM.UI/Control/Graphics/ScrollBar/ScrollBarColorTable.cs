using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CSS.IM.UI.Control.Graphics.ScrollBar
{

    public class ScrollBarColorTable
    {
        private static readonly Color _base = Color.FromArgb(224, 235, 239);
        private static readonly Color _backNormal = Color.FromArgb(203, 221, 231);
        private static readonly Color _backHover = Color.FromArgb(121, 216, 243);
        private static readonly Color _backPressed = Color.FromArgb(70, 202, 239);
        private static readonly Color _border = Color.FromArgb(224, 235, 239);
        private static readonly Color _innerBorder = Color.FromArgb(200, 250, 250, 250);
        private static readonly Color _fore = Color.FromArgb(48, 135, 192);

        public ScrollBarColorTable() { }

        public virtual Color Base
        {
            get { return _base; }
        }

        public virtual Color BackNormal
        {
            get { return _backNormal; }
        }

        public virtual Color BackHover
        {
            get { return _backHover; }
        }

        public virtual Color BackPressed
        {
            get { return _backPressed; }
        }

        public virtual Color Border
        {
            get { return _border; }
        }

        public virtual Color InnerBorder
        {
            get { return _innerBorder; }
        }

        public virtual Color Fore
        {
            get { return _fore; }
        }
    }
}
