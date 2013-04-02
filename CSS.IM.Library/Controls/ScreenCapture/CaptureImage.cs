using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CSS.IM.Library.Controls
{
    public class CaptureImage : Form
    {
        #region Fields

        private Rectangle _bmpRect;
        private Image _image;
        private Point _point;
        private int _index;
        //private Color _nodeColor = Color.FromArgb(12, 115, 150);
        private Color _nodeColor = Color.Red;
        //private Color _borderColor = Color.FromArgb(11, 108, 140);
        private Color _borderColor = Color.Black;
        private Color _chooseColor = Color.FromArgb(240, 240, 251);
        private bool _mouseDown;

        #endregion

        #region Constructors

        public CaptureImage()
        {
            Init();
        }

        #endregion

        #region Properties

        public Image Image
        {
            get { return _image; }
        }

        public Color NodeColor
        {
            get { return _nodeColor; }
            set { _nodeColor = value; }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        public Color ChooseColor
        {
            get { return _chooseColor; }
            set { _chooseColor = value; }
        }

        #endregion

        #region APIs

        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(
            IntPtr hObject, int nXDest, int nYDest, int nWidth,
           int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc,
            TernaryRasterOperations dwRop);

        private enum TernaryRasterOperations
        {
            SRCCOPY = 0x00CC0020, /* dest = source*/
            SRCPAINT = 0x00EE0086, /* dest = source OR dest*/
            SRCAND = 0x008800C6, /* dest = source AND dest*/
            SRCINVERT = 0x00660046, /* dest = source XOR dest*/
            SRCERASE = 0x00440328, /* dest = source AND (NOT dest )*/
            NOTSRCCOPY = 0x00330008, /* dest = (NOT source)*/
            NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */
            MERGECOPY = 0x00C000CA, /* dest = (source AND pattern)*/
            MERGEPAINT = 0x00BB0226, /* dest = (NOT source) OR dest*/
            PATCOPY = 0x00F00021, /* dest = pattern*/
            PATPAINT = 0x00FB0A09, /* dest = DPSnoo*/
            PATINVERT = 0x005A0049, /* dest = pattern XOR dest*/
            DSTINVERT = 0x00550009, /* dest = (NOT dest)*/
            BLACKNESS = 0x00000042, /* dest = BLACK*/
            WHITENESS = 0x00FF0062, /* dest = WHITE*/
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);

            TopMost = true;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            Bounds = Screen.GetBounds(this);
            BackgroundImage = GetDestopImage();
        }

        private Image GetDestopImage()
        {
            Rectangle rect = Screen.GetBounds(this);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(bmp);

            IntPtr gHdc = g.GetHdc();
            IntPtr deskHandle = GetDesktopWindow();

            IntPtr dHdc = GetDC(deskHandle);
            BitBlt(
                gHdc,
                0,
                0,
                Width,
                Height,
                dHdc,
                0,
                0,
                TernaryRasterOperations.SRCCOPY);
            ReleaseDC(deskHandle, dHdc);
            g.ReleaseHdc(gHdc);
            return bmp;
        }

        private int GetSelectedHandle(Point point)
        {
            int index = -1;
            for (int i = 1; i < 9; i++)
            {
                if (GetHandleRect(i).Contains(point))
                {
                    index = i;
                    break;
                }
            }
            if (_bmpRect.Contains(point)) index = 0;
            return index;
        }

        private void MoveHandleTo(Point point)
        {
            int left = _bmpRect.Left;
            int top = _bmpRect.Top;
            int right = _bmpRect.Right;
            int bottom = _bmpRect.Bottom;

            switch (_index)
            {
                case 0:
                    _bmpRect.X += point.X - _point.X;
                    _bmpRect.Y += point.Y - _point.Y;
                    _point = point;
                    return;
                case 1:
                    left = point.X;
                    top = point.Y;
                    break;
                case 2:
                    top = point.Y;
                    break;
                case 3:
                    right = point.X;
                    top = point.Y;
                    break;
                case 4:
                    right = point.X;
                    break;
                case 5:
                    right = point.X;
                    bottom = point.Y;
                    break;
                case 6:
                    bottom = point.Y;
                    break;
                case 7:
                    left = point.X;
                    bottom = point.Y;
                    break;
                case 8:
                    left = point.X;
                    break;
            }
            _point = point;
            _bmpRect.X = left;
            _bmpRect.Y = top;
            _bmpRect.Width = right - left;
            _bmpRect.Height = bottom - top;
        }

        private void SetCursor()
        {
            Cursor cursor = Cursors.Default;
            if (_index == 1 || _index == 5)
            {
                cursor = Cursors.SizeNWSE;
            }
            else if (_index == 2 || _index == 6)
            {
                cursor = Cursors.SizeNS;
            }
            else if (_index == 3 || _index == 7)
            {
                cursor = Cursors.SizeNESW;
            }
            else if (_index == 4 || _index == 8)
            {
                cursor = Cursors.SizeWE;
            }
            else if (_index == 0)
            {
                cursor = Cursors.SizeAll;
            }
            Cursor.Current = cursor;
        }

        private Rectangle GetHandleRect(int index)
        {
            Point point = GetHandle(index);
            return new Rectangle(point.X - 2, point.Y - 2, 5, 5);
        }

        private Point GetHandle(int index)
        {
            int x, y, xCenter, yCenter;

            xCenter = _bmpRect.X + _bmpRect.Width / 2;
            yCenter = _bmpRect.Y + _bmpRect.Height / 2;
            x = _bmpRect.X;
            y = _bmpRect.Y;

            switch (index)
            {
                case 1:
                    x = _bmpRect.X;
                    y = _bmpRect.Y;
                    break;
                case 2:
                    x = xCenter;
                    y = _bmpRect.Y;
                    break;
                case 3:
                    x = _bmpRect.Right;
                    y = _bmpRect.Y;
                    break;
                case 4:
                    x = _bmpRect.Right;
                    y = yCenter;
                    break;
                case 5:
                    x = _bmpRect.Right;
                    y = _bmpRect.Bottom;
                    break;
                case 6:
                    x = xCenter;
                    y = _bmpRect.Bottom;
                    break;
                case 7:
                    x = _bmpRect.X;
                    y = _bmpRect.Bottom;
                    break;
                case 8:
                    x = _bmpRect.X;
                    y = yCenter;
                    break;
            }

            return new Point(x, y);
        }

        private Rectangle ChangeRect()
        {
            int left = _bmpRect.Left;
            int top = _bmpRect.Top;
            int right = _bmpRect.Right;
            int bottom = _bmpRect.Bottom;

            int x, y, width, height;
            x = Math.Min(left, right);
            y = Math.Min(top, bottom);
            width = Math.Abs(left - right);
            height = Math.Abs(top - bottom);

            return new Rectangle(x, y, width, height);
        }

        #endregion

        #region Override Methods

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            if (_bmpRect.Width <= 0 || _bmpRect.Height <= 0)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            Bitmap bmp = new Bitmap(_bmpRect.Width, _bmpRect.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(BackgroundImage, 0, 0, _bmpRect, GraphicsUnit.Pixel);
            _image = bmp;
            DialogResult = DialogResult.OK;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                _mouseDown = true;
                if (_bmpRect == Rectangle.Empty)
                    _bmpRect.Location = e.Location;
                else
                    Invalidate();
            }
            _point = e.Location;
            _index = GetSelectedHandle(_point);
            SetCursor();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_mouseDown)
            {
                MoveHandleTo(e.Location);
                Invalidate();
            }
            else
            {
                _index = GetSelectedHandle(e.Location);
                SetCursor();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            int left = _bmpRect.Left;
            int top = _bmpRect.Top;
            int right = _bmpRect.Right;
            int bottom = _bmpRect.Bottom;
            _bmpRect = ChangeRect();
            if (e.Button == MouseButtons.Right)
            {
                if (_bmpRect == Rectangle.Empty)
                {
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    _bmpRect = Rectangle.Empty;
                    Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                _mouseDown = false;
                if (_bmpRect != Rectangle.Empty)
                    Invalidate();
            }
            _index = GetSelectedHandle(e.Location);
            SetCursor();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = ChangeRect();
            if (_mouseDown)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(90, _chooseColor)))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }

            using (Pen pen = new Pen(_borderColor))
            {
                e.Graphics.DrawRectangle(pen, rect);


                for (int i = 1; i < 9; i++)
                {
                    using (SolidBrush brush = new SolidBrush(_nodeColor))
                    {
                        e.Graphics.FillRectangle(
                            brush,
                            GetHandleRect(i));
                    }
                }
            }
        }

        #endregion
    }
}
