using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CSS.IM.UI.Util
{
    public class Path
    {
        /// <summary>
        /// 应用程序目录
        /// </summary>
        public static string AppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// 最后的登录用户记录
        /// </summary>
        public static string HistoryFilename = AppPath + @"\UserData\history.ini";

        /// <summary>
        /// 本地配置文件目录
        /// </summary>
        public static string SettingsFilename = AppPath + @"\UserData\[{0}]verify.ini";

        /// <summary>
        /// 本地配置文件目录
        /// </summary>
        public static string ConfigFilename = AppPath + @"\UserData\[{0}]settings.ini";

        /// <summary>
        /// 新消息声音
        /// </summary>
        public static string MsgPath = AppPath + @"\Sound\msg.wav";
        public static bool MsgSwitch = true;

        /// <summary>
        /// 视频呼叫息声音
        /// </summary>
        public static string CallPath = AppPath + @"\Sound\call.wav";
        public static bool CallSwitch = true;

        /// <summary>
        /// 系统推送消息
        /// </summary>
        public static string SystemPath = AppPath + @"\Sound\system.wav";
        public static bool SystemSwitch = true;

        /// <summary>
        /// 分组打开或关闭
        /// </summary>
        public static string FolderPath = AppPath + @"\Sound\folder.wav";
        public static bool FolderSwitch = true;

        /// <summary>
        /// 好友上线声音
        /// </summary>
        public static string GlobalPath = AppPath + @"\Sound\Global.wav";
        public static bool GlobalSwitch = true;

        /// <summary>
        /// 好友下线声音
        /// </summary>
        public static string InputAlertPath = AppPath + @"\Sound\InputAlert.wav";
        public static bool InputAlertSwitch = true;

        /// <summary>
        /// 是否接收服务器消息
        /// </summary>
        public static bool ReveiveSystemNotification = true;

        /// <summary>
        /// 是否自动打开消息窗体
        /// </summary>
        public static bool ChatOpen = false;

        /// <summary>
        /// 开机自动启动
        /// </summary>
        public static bool Initial = true;


        /// <summary>
        /// 消息发送快捷键类型
        /// </summary>
        public static int SendKeyType = 1;


        /// <summary>
        /// 头像显示类型，大头像或小头像
        /// </summary>
        public static bool FriendContainerType = true;

        /// <summary>
        /// 返回设置好的字体
        /// </summary>
        public static Font SFong;

        /// <summary>
        /// 返回设置好的字体颜色
        /// </summary>
        public static Color SColor;

        /// <summary>
        /// 取出消息快捷按键
        /// </summary>
        public static string GetOutMsgKeyTYpe = "";

        /// <summary>
        /// 取出消息快捷按键
        /// </summary>
        public static string ScreenKeyTYpe = "";


        public static string DefaultURL = "";


        public static string EmailURL = "";
    }
}
