using System;
using CSS.IM.XMPP.Xml.Dom;


namespace CSS.IM.App.Settings
{
    /*   
    This class shows how agsXMPP could also be used read and write custom xml files.
    Here we use it for the application settings which are stored in xml files    
    */
    public class Verify : Element
    {
        public Verify()
        {
            this.TagName = "settings";
        }        
        
        //public Login Login
        //{
        //    get { return (Login)SelectSingleElement(typeof(Login)); }
        //    set
        //    {
        //        RemoveTag(typeof(Login));
        //        if (value != null)
        //            AddChild(value);
        //    }
        //}

        public Login Login
        {
            get { return (Login)SelectSingleElement(typeof(Login)); }
            set
            {
                RemoveTag(typeof(Login));
                if (value != null)
                    AddChild(value);
            }
        }

        public ServerInfo ServerInfo
        {
            get { return (ServerInfo)SelectSingleElement(typeof(ServerInfo)); }
            set
            {
                RemoveTag(typeof(ServerInfo));
                if (value != null)
                    AddChild(value);
            }
        }
       
    }
}