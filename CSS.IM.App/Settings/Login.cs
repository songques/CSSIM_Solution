using System;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.XMPP;



namespace CSS.IM.App.Settings
{
    /*    
    This class shows how agsXMPP could also be used read and write custom xml files.
    Here we use it for the application settings which are stored in xml files    
    */
    public class Login : Element
    {
        public Login()
        {
            this.TagName    = "Login";
            this.Namespace  = null;
        }

        public Jid Jid
        {
            get { return GetTagJid("Jid"); }
            set { SetTag("Jid", value); }
        }

        public string Password
        {
            get { return GetTag("Password"); }
            set { SetTag("Password", value); }
        }

        public string Resource
        {
            get { return GetTag("Resource"); }
            set { SetTag("Resource", value); }
        }

        public int Priority
        {
            get { return GetTagInt("Priority"); }
            set { SetTag("Priority", value); }
        }

        public int Port
        {
            get { return GetTagInt("Port"); }
            set { SetTag("Port", value); }
        }

        public bool Ssl
        {
            get { return GetTagBool("Ssl"); }
            set { SetTag("Ssl", value); }
        }

        public bool Save
        {
            get { return GetTagBool("Save"); }
            set { SetTag("Save", value); }
        }

        public bool Auto
        {
            get { return GetTagBool("Auto"); }
            set { SetTag("Auto", value); }
        }

        /// <summary>
        /// 开机自动启动
        /// </summary>
        public bool InitIal
        {
            get { return GetTagBool("InitIal"); }
            set { SetTag("InitIal", value); }
        }
    }
}