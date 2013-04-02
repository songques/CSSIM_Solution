using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using System.Runtime.InteropServices;
using System.Diagnostics;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.App.Msg;
using CSS.IM.XMPP;
using CSS.IM.App.Settings;

namespace CSS.IM.App
{
    public partial class MessageBoxForm : MsgBasicForm
    {
        CSS.IM.XMPP.protocol.client.Message Msg { get;set;}
        private string username;
        private string pswd;


        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        //下面是可用的常量，根据不同的动画效果声明自己需要的
        private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志该标志
        private const int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展
        private const int AW_HIDE = 0x10000;//隐藏窗口
        private const int AW_ACTIVE = 0x20000;//激活窗口，在使用了AW_HIDE标志后不要使用这个标志
        private const int AW_SLIDE = 0x40000;//使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略
        private const int AW_BLEND = 0x80000;//使用淡入淡出效果

        public MessageBoxForm(String title,CSS.IM.XMPP.protocol.client.Message msg,string username,string pswd)
        {
            
            InitializeComponent();
            Msg = msg;
            this.username = username;
            this.pswd = pswd;
            //Msg.SetTag("subject","mq");

            this.Text = title;

            switch (msg.GetTag("subject"))
            {
                case null:
                    this.lllab.Text = Msg.Body;
                    break;
                case "window":
                    MqMessage mqMsg = MarkMessage_Mq(Msg);
                    try
                    {
                        this.lllab.Text = mqMsg.Msg;
                        this.lllab.Tag = mqMsg;
                    }
                    catch (Exception ex)
                    {
                        this.lllab.Text = "消息格式错误，" + ex.Message;
                    }
                    break;
            }
            //this.Location = new System.Drawing.Point(100, 100);
            //this.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        private void MessageBoxForm_Load(object sender, EventArgs e)
        {
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.Location = new Point(x, y);//设置窗体在屏幕右下角显示
            AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_ACTIVE | AW_VER_NEGATIVE);
        }

        private void lllab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            MqMessage mqMsg=null;

            if (((LinkLabel)sender).Tag != null)
            {
                mqMsg= (MqMessage)((LinkLabel)sender).Tag;
            }

            if (mqMsg == null)
                return;

            string isOpen="false";

            try
            {
                isOpen = mqMsg.IsOpen;
            }
            catch (Exception)
            {

            }

            if (isOpen=="true")
            {
                Process.Start(mqMsg.Herf + "?url=" + mqMsg.Url + "&token=" + mqMsg.Token + "&loginname=" + username + "&pwd=" + Base64.EncodeBase64(pswd));
            }
        }

        public static MqMessage MarkMessage_Mq(CSS.IM.XMPP.protocol.client.Message msg)
        {
            MqMessage mqMsg=null;
            ElementList properte_list = msg.SelectElements("properties");

            if (properte_list.Count <= 0)
                return  null;

            int count = properte_list.Item(0).SelectElements("property").Count;
            mqMsg = new MqMessage();
            for (int i = 0; i < count; i++)
            {
                string nameValue = "";
                string keyValue = "";

                #region 获取property值
                try
                {
                    nameValue = properte_list.Item(0).SelectElements("property").Item(i).SelectElements("name").Item(0).Value;
                }
                catch (Exception)
                {
                    break;
                }


                try
                {
                    keyValue = properte_list.Item(0).SelectElements("property").Item(i).SelectElements("value").Item(0).Value;
                }
                catch (Exception)
                {
                }
                #endregion

                switch (nameValue)
                {
                    case "isopen":
                        mqMsg.IsOpen = keyValue;
                        break;
                    case "token":
                        mqMsg.Token = keyValue;
                        break;
                    case "herf":
                        mqMsg.Herf = keyValue;
                        break;
                    case "msg":
                        mqMsg.Msg = keyValue;
                        break;
                    case "url":
                        mqMsg.Url = keyValue;
                        break;
                    default:
                        break;
                }
            }

            return mqMsg;
        }
    }
}
