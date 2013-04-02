using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CSS.IM.UI.Control.Graphics.ProgressBarEx
{

    public class ProgressBarColorTable
    {
        private static readonly Color _trackBack = Color.FromArgb(185, 185, 185);
        private static readonly Color _trackFore = Color.FromArgb(15, 181, 43);
        private static readonly Color _border = Color.FromArgb(158, 158, 158);
        private static readonly Color _innerBorder = Color.FromArgb(200, 250, 250, 250);

        public ProgressBarColorTable() { }

        public virtual Color TrackBack
        {
            get { return _trackBack; }
        }

        public virtual Color TrackFore
        {
            get { return _trackFore; }
        }

        public virtual Color Border
        {
            get { return _border; }
        }

        public virtual Color InnerBorder
        {
            get { return _innerBorder; }
        }
    }
}
