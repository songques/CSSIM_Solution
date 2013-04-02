using System;
using System.Collections.Generic;
using System.Text;
using CSS.IM.XMPP.Xml.Dom;

namespace CSS.IM.XMPP.protocol.Base
{
    public class Query : Element
    {
        public Query()
        {
            this.TagName = "query";	
        }
    }
}
