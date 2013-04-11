using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using CSS.IM.XMPP;
using CSS.IM.XMPP.protocol.client;
using Microsoft.Win32;
using System.Windows.Forms;
using CSS.IM.XMPP.protocol.iq.vcard;
using System.IO;
using System.Drawing;
using CSS.IM.UI.Control;
using System.Text.RegularExpressions;
using CSS.IM.Library.Controls.UdpSendFile;

namespace CSS.IM.App.Settings
{
    public class Util
    {

        /// <summary>
        /// 更新个人信息事件
        /// </summary>
        /// <param name="sender"></param>
        public delegate void ObjectHandler(object sender);
        public static event ObjectHandler VcardChangeEvent;

        /// <summary>
        /// 更新分组聊天的好头信息列新
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public delegate void PresenceHandler(object sender);
        public static event PresenceHandler GroupPresenceEvent;

        /// <summary>
        /// 打开个人资料事件
        /// </summary>
        /// <param name="sender"></param>
        public static event ObjectHandler SetInfoFormEvent;

        /// <summary>
        /// 打开个人资料事件
        /// </summary>
        /// <param name="sender"></param>
        public static event ObjectHandler SetingFormEvent;

        /// <summary>
        /// 打开指定的联系人
        /// </summary>
        /// <param name="sender">jid</param>
        /// <returns></returns>
        public delegate void HrefOpenFriendHandler(string sender);
        public static event HrefOpenFriendHandler HrefOpenFriendEvent;

        /// <summary>
        /// 应用程序目录
        /// </summary>
        public static string AppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// 发送图片保存目录
        /// </summary>
        public static string sendImage = AppPath + @"\sendImage\";

        /// <summary>
        /// 保存数据发送目录
        /// </summary>
        public static string receiveImage = AppPath + @"\receiveImage\";


        private static Vcard _vcard;//用于保存用户名片和扩展的信息

        public static Vcard vcard
        {
            get {
                //if (Util._vcard!=null)
                //{
                //    if (Util._vcard.Description==null)
                //    {
                //        Util._vcard.Description = "这家伙很懒，什么都没有留下！";
                //    }
                //    else if (Util._vcard.Description == "")
                //    {
                //        Util._vcard.Description = "这家伙很懒，什么都没有留下！";
                //    }
                //}
                return Util._vcard;
            }
            set { Util._vcard = value; }
        }

        public static Hashtable ChatForms = new Hashtable();
        public static Hashtable GroupChatForms = new Hashtable();

        public class XmppServices
        {
            public XmppServices()
            {
            }

            public List<Jid> Search = new List<Jid>();
            public List<Jid> Rooms = new List<Jid>();
            public List<Jid> Finds = new List<Jid>();
            public List<Jid> Proxy = new List<Jid>();
            public List<Jid> Pubsub = new List<Jid>();
            
        }

        public static XmppServices Services = new XmppServices();

        public static int GetRosterImageIndex(Presence pres)
        {
            if (pres.Type == PresenceType.unavailable)
            {
                return 0;
            }
            else if (pres.Type == PresenceType.error)
            {
                // presence error, we dont care in the miniclient here
            }
            else
            {
                switch (pres.Show)
                {
                    case ShowType.NONE:
                        return 1;
                    case ShowType.away:
                        return 2;
                    case ShowType.chat:
                        return 4;
                    case ShowType.xa:
                        return 3;
                    case ShowType.dnd:
                        return 5;
                }
            }
            return 0;
        }


        public static void RunWhenStart(bool Started, string name, string path)
        {
            RegistryKey HKLM = Registry.LocalMachine;
            RegistryKey Run = HKLM.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            if (Started == true)
            {
                try
                {
                    Run.SetValue(name, path);
                    HKLM.Close();
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message, "系统", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw err;
                }
            }
            else
            {
                try
                {
                    if (Run.GetValue(name) != null)
                    {
                        Run.DeleteValue(name, false);
                        HKLM.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// 获取用户好友的备注
        /// </summary>
        /// <param name="jid"></param>
        /// <returns></returns>
        public static string getFriendReamrk(string jid)
        {
            try
            {
                if (Util.vcard.SelectElements("TUserRemark").Count > 0)
                {

                    if (Util.vcard.SelectElements("TUserRemark").Count > 0)
                    {
                        return Util.vcard.SelectElements("TUserRemark").Item(0).GetTag(jid);
                    }
                }
            }
            catch (Exception)
            {
                
            }
            return null; 
        }

        /// <summary>
        /// 激活名片改变事件
        /// </summary>
        public static void VcardChangeEventMethod()
        {
            if (VcardChangeEvent != null)
                VcardChangeEvent(Util.vcard);
        }


        /// <summary>
        /// 更新分组聊天的好头信息列新变事件
        /// </summary>
        public static void GroupPresenceEventMethod(Jid jid)
        {
            if (GroupPresenceEvent != null)
                GroupPresenceEvent(jid);
        }

        /// <summary>
        /// 激活打开个人资料
        /// </summary>
        public static void SetInfoFormEventMothe()
        {
            if (SetInfoFormEvent != null)
                SetInfoFormEvent(Util.vcard);
        }

        /// <summary>
        /// 激活打开设置
        /// </summary>
        public static void SetingFormEventMothe()
        {
            if (SetingFormEvent != null)
                SetingFormEvent(Util.vcard);
        }

        /// <summary>
        /// 打开指定联系人事件
        /// </summary>
        public static void HrefOpenFriendEventMethod(string args)
        {

            if (HrefOpenFriendEvent != null)
            {
                HrefOpenFriendEvent(args);
                MessageBox.Show("args:"+args);
            }
        }

        /// <summary>
        /// 制作黑头像
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap MarkTopHead(Image image)
        {
            Bitmap OldHeadImg = new Bitmap(image);
            Bitmap headImg = null;
            try
            {
                int Height = OldHeadImg.Height;
                int Width = OldHeadImg.Width;

                headImg = new Bitmap(Width, Height);
                Color pixel;
                for (int x = 0; x < Width; x++)
                    for (int y = 0; y < Height; y++)
                    {
                        pixel = OldHeadImg.GetPixel(x, y);
                        int r, g, b, Result = 0;
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;
                        //实例程序以加权平均值法产生黑白图像
                        int iType = 2;
                        switch (iType)
                        {
                            case 0://平均值法
                                Result = ((r + g + b) / 3);
                                break;
                            case 1://最大值法
                                Result = r > g ? r : g;
                                Result = Result > b ? Result : b;
                                break;
                            case 2://加权平均值法
                                Result = ((int)(0.7 * r) + (int)(0.2 * g) + (int)(0.1 * b));
                                break;
                        }
                        headImg.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                    }
 
            }catch (Exception){ }
            return headImg;
        }

        /// <summary>
        /// 返回热键的组件
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static HotKey.KeyModifiers GetFunctionKeyValue(string args)
        {
            if (args.Trim()=="Control")
            {
                return HotKey.KeyModifiers.Ctrl;
            }
            else if (args.Trim() == "Alt")
            {
                return HotKey.KeyModifiers.Alt;
            }
            else if (args.Trim() == "Shift")
            {
                return HotKey.KeyModifiers.Shift;
            }
            else if (args.Trim() == "WindowsKey")
            {
                return HotKey.KeyModifiers.WindowsKey;
            }
            else
            {
                return HotKey.KeyModifiers.None;
            }
        }

        /// <summary>
        /// 返回热键的组件
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Keys GetKeyValue(string args)
        {
            return (Keys)Enum.Parse(typeof(Keys), args);
            //foreach (var item in Keys.e)
            //{
            //    if (args==)
            //    {
                    
            //    }
            //}
        }


        /// <summary>
        /// 备份，最后一次活动窗口的Jid用于截图归属
        /// </summary>
        public static Jid TO_Jid;

        /// <summary>
        /// 用于保存当前是否在截图状态
        /// </summary>
        public static bool ScreenImage = false;


        /// <summary>
        /// 用于保存主窗体的句柄
        /// </summary>
        public static IntPtr MainHandle = IntPtr.Zero;


        /// <summary>
        /// 验证不能为特殊符号
        /// </summary>
        /// <param name="strNumber">被验证信息</param>
        /// <returns></returns>
        public static bool IsWholeString(string strNum)
        {
            //Regex notWholePattern = new Regex(@"^[0-9a-zA-Z\$]+$");
            Regex notWholePattern = new Regex(@"^[0-9a-zA-Z\$]+$");
            return notWholePattern.IsMatch(strNum, 0);
        }

        /// <summary>
        /// 用于保存ftp上传功能
        /// </summary>
        private static Dictionary<string, SendFileManager> _sendFileManagerList;
        public static Dictionary<string, SendFileManager> SendFileManagerList
        {
            get
            {
                if (_sendFileManagerList == null)
                {
                    _sendFileManagerList = new Dictionary<string, SendFileManager>(10);
                }
                return _sendFileManagerList;
            }
        }


        /// <summary>
        /// 用于保存ftp下载功能
        /// </summary>
        private static Dictionary<string, RequestSendFileEventArgs> _receiveFileManagerList;
        public static Dictionary<string, RequestSendFileEventArgs> ReceiveFileManagerList
        {
            get
            {
                if (_receiveFileManagerList == null)
                {
                    _receiveFileManagerList = new Dictionary<string, RequestSendFileEventArgs>(10);
                }
                return _receiveFileManagerList;
            }
        }

        //public static string ServerAddress = "6.136.8.14";
        //public static string ServerAddress = "10.0.0.208";
        //public static string ServerAddress = "10.0.3.10";
        public static string ServerAddress = "192.168.0.145";
        public static int OpenFirePort = 5222;

        public static int ftpPort = 21;
        public static string ftpUser = "root";
        //public static string ftpUser = "root";
        //public static string ftpPswd = "123456";
        public static string ftpPswd = "123456";
        public static string ftpPath = @"/";
    }
}
