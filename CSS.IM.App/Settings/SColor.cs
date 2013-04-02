using System;
using System.Collections.Generic;
using System.Text;
using CSS.IM.XMPP.Xml.Dom;

namespace CSS.IM.App.Settings
{
    public class SColor : Element
    {
        public SColor()
        {
            this.TagName = "SColor";
            this.Namespace  = null;
        }

        public byte CA
        {
            get { return (byte)GetTagInt("CA"); }
            set { SetTag("CA", value); }
        }

        public byte CR
        {
            get { return (byte)GetTagInt("CR"); }
            set { SetTag("CR", value); }
        }

        public byte CG
        {
            get { return (byte)GetTagInt("CG"); }
            set { SetTag("CG", value); }
        }

        public byte CB
        {
            get { return (byte)GetTagInt("CB"); }
            set { SetTag("CB", value); }
        }
    }
}
