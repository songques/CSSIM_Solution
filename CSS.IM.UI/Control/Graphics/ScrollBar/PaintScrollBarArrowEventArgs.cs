using System;
using System.Drawing;
using System.Windows.Forms;

namespace CSS.IM.UI.Control.Graphics.ScrollBar
{


    public class PaintScrollBarArrowEventArgs : IDisposable
    {
        private System.Drawing.Graphics _graphics;
        private Rectangle _arrowRect;
        private ControlState _controlState;
        private ArrowDirection _arrowDirection;
        private Orientation _orientation;
        private bool _enabled;

        public PaintScrollBarArrowEventArgs(
            System.Drawing.Graphics graphics,
            Rectangle arrowRect,
            ControlState controlState,
            ArrowDirection arrowDirection,
            Orientation orientation)
            : this(
            graphics,
            arrowRect,
            controlState,
            arrowDirection,
            orientation,
            true)
        {
        }

        public PaintScrollBarArrowEventArgs(
            System.Drawing.Graphics graphics,
            Rectangle arrowRect,
            ControlState controlState,
            ArrowDirection arrowDirection,
            Orientation orientation,
            bool enabled)
        {
            _graphics = graphics;
            _arrowRect = arrowRect;
            _controlState = controlState;
            _arrowDirection = arrowDirection;
            _orientation = orientation;
            _enabled = enabled;
        }

        public System.Drawing.Graphics Graphics
        {
            get { return _graphics; }
            set { _graphics = value; }
        }

        public Rectangle ArrowRectangle
        {
            get { return _arrowRect; }
            set { _arrowRect = value; }
        }

        public ControlState ControlState
        {
            get { return _controlState; }
            set { _controlState = value; }
        }

        public ArrowDirection ArrowDirection
        {
            get { return _arrowDirection; }
            set { _arrowDirection = value; }
        }

        public Orientation Orientation
        {
            get { return _orientation; }
            set { _orientation = value; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public void Dispose()
        {
            _graphics = null;
        }
    }
}
