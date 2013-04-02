using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CSS.IM.UI.Face
{
    /// <summary>
    /// ���ߣ�Starts_2000
    /// ���ڣ�2009-07-15
    /// ��վ��http://www.csharpwin.com CS ����Ա֮����
    /// ��������ʹ�û��޸����´��룬���뱣����Ȩ��Ϣ��
    /// ������鿴 CS����Ա֮����ԴЭ�飨http://www.csharpwin.com/csol.html����
    /// ���ܣ�һ��ר������ʾGIF����ͼ��Ŀؼ���
    /// </summary>
    public class GifBox : System.Windows.Forms.Control
    {
        #region ����

        private Image _image;
        private Rectangle _imageRectangle;
        private EventHandler _eventAnimator;
        private bool _canAnimate;
        private Color _borderColor = Color.BlueViolet;

        #endregion

        #region ���캯��

        public GifBox()
            : base()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.CacheText |
                ControlStyles.ResizeRedraw, true);
        }

        #endregion

        #region ����

        public Image Image
        {
            get { return _image; }
            set
            {
                StopAnimate();
                _image = value;
                _imageRectangle = Rectangle.Empty;
                if (value != null)
                    _canAnimate = ImageAnimator.CanAnimate(_image);
                else
                    _canAnimate = false;
                Invalidate(ImageRectangle);
                if(!DesignMode)
                    StartAnimate();
            }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                base.Invalidate();
            }
        }

        private Rectangle ImageRectangle
        {
            get
            {
                if (_imageRectangle == Rectangle.Empty &&
                    _image != null)
                {
                    _imageRectangle.X = (Width - _image.Width) / 2;
                    _imageRectangle.Y = (Height - _image.Height) / 2;
                    _imageRectangle.Width = _image.Width;
                    _imageRectangle.Height = _image.Height;
                }
                return _imageRectangle;
            }
        }

        private bool CanAnimate
        {
            get { return _canAnimate; }
        }

        private EventHandler EventAnimator
        {
            get
            {
                if (_eventAnimator == null)
                    _eventAnimator = delegate(object sender, EventArgs e)
                    {
                        Invalidate(ImageRectangle);
                    };
                return _eventAnimator;
            }
        }

        #endregion

        #region Override

        protected override void OnSizeChanged(EventArgs e)
        {
            _imageRectangle = Rectangle.Empty;
            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_image != null)
            {
                //ÿ�λ�֮ǰ���µ�ͼƬ����һ֡��
                UpdateImage();
                e.Graphics.DrawImage(
                    _image,
                    ImageRectangle,
                    0,
                    0,
                    _image.Width,
                    _image.Height,
                    GraphicsUnit.Pixel);
            }

            ControlPaint.DrawBorder(
                    e.Graphics,
                    ClientRectangle,
                    _borderColor,
                    ButtonBorderStyle.Solid);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _eventAnimator = null;
                _canAnimate = false;
                if (_image != null)
                    _image = null;
            }

        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            StopAnimate();
        }

        #endregion

        #region Private Method

        private void StartAnimate()
        {
            if (CanAnimate)
            {
                ImageAnimator.Animate(_image, EventAnimator);
            }
        }

        private void StopAnimate()
        {
            if (CanAnimate)
            {
                ImageAnimator.StopAnimate(_image, EventAnimator);
            }
        }

        private void UpdateImage()
        {
            if (CanAnimate)
            {
                ImageAnimator.UpdateFrames(_image);
            }
        }

        #endregion
    }
}
