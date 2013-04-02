using System;
using System.Collections.Generic;
using System.Text;
using CSS.IM.XMPP.Xml.Dom;

namespace CSS.IM.App.Settings
{
    /// <summary>
    /// 用户的备注表
    /// </summary>
    public class TUserRemark : Element
    {
        public TUserRemark()
        {
            this.TagName = "TUserRemark";
            this.Namespace  = null;
        }
    }
}
