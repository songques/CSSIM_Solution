using System;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.XMPP;



namespace CSS.IM.App.Settings
{
    /*    
    This class shows how agsXMPP could also be used read and write custom xml files.
    Here we use it for the application settings which are stored in xml files    
    */
    public class Paths : Element
    {
        public Paths()
        {
            this.TagName    = "Paths";
            this.Namespace  = null;
        }

        public string MsgPath
        {
            get { return GetTag("MsgPath"); }
            set { SetTag("MsgPath", value); }
        }

        public string CallPath
        {
            get { return GetTag("CallPath"); }
            set { SetTag("CallPath", value); }
        }

        public string SystemPath
        {
            get { return GetTag("SystemPath"); }
            set { SetTag("SystemPath", value); }
        }

        public string FolderPath
        {
            get { return GetTag("FolderPath"); }
            set { SetTag("FolderPath", value); }
        }

        public string GlobalPath
        {
            get { return GetTag("GlobalPath"); }
            set { SetTag("GlobalPath", value); }
        }

        public string InputAlertPath
        {
            get { return GetTag("InputAlertPath"); }
            set { SetTag("InputAlertPath", value); }
        }

        public bool ChatOpen
        {
            get { return GetTagBool("ChatOpen"); }
            set { SetTag("ChatOpen", value); }
        }

        public bool ReveiveSystemNotification
        {
            get { return GetTagBool("ReveiveSystemNotification"); }
            set { SetTag("ReveiveSystemNotification", value); }
        }

        /// <summary>
        /// 发送按键类型
        /// </summary>
        public int SendKeyType
        {
            /*
             * 1 enter
             * 2 ctrl + enter
             */

            get { return GetTagInt("SendKeyType"); }
            set { SetTag("SendKeyType", value); }
        }

        /// <summary>
        /// 取出消息快捷按键
        /// </summary>
        public string GetOutMsgKeyTYpe
        {
            get { return GetTag("GetOutMsgKeyTYpe"); }
            set { SetTag("GetOutMsgKeyTYpe", value); }
        }

        /// <summary>
        /// 屏幕截图快捷按键
        /// </summary>
        public string ScreenKeyTYpe
        {
            get { return GetTag("ScreenKeyTYpe"); }
            set { SetTag("ScreenKeyTYpe", value); }
        }



        /// <summary>
        ///显示头像类型
        /// </summary>
        public bool FriendContainerType
        {
            /*
             * true 大头像
             * false 小头像
             */

            get { return GetTagBool("FriendContainerType"); }
            set { SetTag("FriendContainerType", value); }
        }


        /// <summary>
        ///毛奇使用
        /// </summary>
        public string DefaultURL
        {
            get { return GetTag("DefaultURL"); }
            set { SetTag("DefaultURL", value); }
        }


        /// <summary>
        ///毛奇使用
        /// </summary>
        public string EmailURL
        {
            get { return GetTag("EmailURL"); }
            set { SetTag("EmailURL", value); }
        }
    }
}