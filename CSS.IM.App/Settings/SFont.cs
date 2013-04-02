using System;
using System.Collections.Generic;
using System.Text;
using CSS.IM.XMPP.Xml.Dom;

namespace CSS.IM.App.Settings
{
    public class SFont : Element
    {
        public SFont()
        {
            this.TagName = "SFont";
            this.Namespace  = null;
        }

        public string Name
        {
            get { return GetTag("Name"); }
            set { SetTag("Name", value); }
        }

        public float Size
        {
            get { return (float)GetTagDouble("Size"); }
            set { SetTag("Size", value); }
        }

        public bool Bold
        {
            get { return GetTagBool("Bold"); }
            set { SetTag("Bold", value); }
        }

        public bool Italic
        {
            get { return GetTagBool("Italic"); }
            set { SetTag("Italic", value); }
        }

        public bool Strikeout
        {
            get { return GetTagBool("Strikeout"); }
            set { SetTag("Strikeout", value); }
        }

        public bool Underline
        {
            get { return GetTagBool("Underline"); }
            set { SetTag("Underline", value); }
        }
    }
}
