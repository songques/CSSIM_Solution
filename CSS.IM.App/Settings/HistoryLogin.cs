using System;
using System.Collections.Generic;
using System.Text;
using CSS.IM.XMPP.Xml.Dom;

namespace CSS.IM.App.Settings
{
    class HistoryLogin : Element
    {
        public HistoryLogin()
        {
            this.TagName = "HistoryLogin";
            this.Namespace  = null;
        }


        public string LoginName
        {
            get { return GetTag("LoginName"); }
            set { SetTag("LoginName", value); }
        }
    }
}
