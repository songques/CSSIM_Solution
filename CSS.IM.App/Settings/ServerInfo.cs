using System;
using System.Collections.Generic;
using System.Text;
using CSS.IM.XMPP.Xml.Dom;

namespace CSS.IM.App.Settings
{

    public class ServerInfo : Element
    {
        public ServerInfo()
        {
            this.TagName = "ServerInfo";
            this.Namespace = null;
        }

        public string ServerIP
        {
            get { return GetTag("ServerIP"); }
            set { SetTag("ServerIP", value); }
        }

        public string ServerPort
        {
            get { return GetTag("ServerPort"); }
            set { SetTag("ServerPort", value); }
        }
    }
}
