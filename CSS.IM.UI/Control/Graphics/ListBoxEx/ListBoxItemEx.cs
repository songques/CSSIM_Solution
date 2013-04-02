using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.ComponentModel;

namespace CSS.IM.UI.Control.Graphics.ListBoxEx
{

    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ListBoxExItem : IDisposable
    {
        #region Fields

        private string _text = "ListBoxExItem";
        private Image _image;
        private object _tag;

        #endregion

        #region Constructors

        public ListBoxExItem()
        {
        }

        public ListBoxExItem(string text)
            : this(text, null)
        {
        }

        public ListBoxExItem(string text, Image image)
        {
            _text = text;
            _image = image;
        }

        #endregion

        #region Properties

        [DefaultValue("ImageComboBoxItem")]
        [Localizable(true)]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        [DefaultValue(typeof(Image), "null")]
        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }

        [Bindable(true)]
        [Localizable(false)]
        [DefaultValue("")]
        [TypeConverter(typeof(StringConverter))]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        #endregion

        #region Override Methods

        public override string ToString()
        {
            return _text;
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _image = null;
            _tag = null;
        }

        #endregion
    }
}
