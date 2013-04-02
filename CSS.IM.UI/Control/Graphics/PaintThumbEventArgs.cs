using System;
using System.Drawing;
using System.Windows.Forms;

namespace CSS.IM.UI.Control.Graphics
{


    public class PaintThumbEventArgs : PaintEventArgs
    {
        private ControlState _controlState;

        public ControlState ControlState
        {
            get { return _controlState; }
        }

        public PaintThumbEventArgs(
            System.Drawing.Graphics g, Rectangle clipRect, ControlState state)
            : base(g, clipRect)
        {
            _controlState = state;
        }
    }
}
