using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.XMPP;
using CSS.IM.XMPP.protocol.iq.disco;
using System.Threading;
using CSS.IM.App.Settings;

namespace CSS.IM.App
{
    public partial class RegisterForm : BasicForm
    {
        public XmppClientConnection XmppCon = new XmppClientConnection();

        public string ServerIP = "127.0.0.1";
        public string Port = "5222";

        private delegate void ShowMessageBoxDelegate(string args,bool isClose);
        private ShowMessageBoxDelegate ShowMessageBoxEvent;

        public RegisterForm(string ServerIP,string port)
        {
            InitializeComponent();
            this.ServerIP = ServerIP;
            this.Port = port;

            //XmppCon.Server = System.Net.Dns.GetHostByAddress(this.ServerIP).HostName.ToString();
            XmppCon.Server = this.ServerIP;
            XmppCon.Resource = "CSS.IM.App";
            XmppCon.Priority = 10;
            XmppCon.Port = int.Parse(this.Port);
            XmppCon.AutoResolveConnectServer = true;
            XmppCon.UseCompression = false;
            XmppCon.RegisterAccount = true;  //是否注册.
            XmppCon.EnableCapabilities = true;
            XmppCon.ClientVersion = "1.0";

            XmppCon.Capabilities.Node = "http://www.css.com.cn/";
            XmppCon.OnRegistered += new ObjectHandler(XmppCon_OnRegistered);
            XmppCon.OnRegisterError += new XmppElementHandler(XmppCon_OnRegisterError);
            XmppCon.OnRegisterInformation += new XMPP.protocol.iq.register.RegisterEventHandler(XmppCon_OnRegisterInformation);
            ShowMessageBoxEvent = new ShowMessageBoxDelegate(ShowMessageBoxMethod);
            
            
        }

        void XmppCon_OnRegisterInformation(object sender, XMPP.protocol.iq.register.RegisterEventArgs args)
        {
            //MessageBox.Show("a");
            //if (InvokeRequired)
            //{
            //    Invoke(new CSS.IM.XMPP.protocol.iq.register.RegisterEventHandler(_connection_OnRegisterInformation), new object[] { sender, args });
            //}

            //MessageBox.Show( "注册失败，" + args.Register.ToString());
            //XmppCon.Close();
            //this.Close();
        }

        private void XmppCon_OnRegisterError(object sender, XMPP.Xml.Dom.Element e)
        {
            try
            {
                this.Invoke(ShowMessageBoxEvent, new object[] { "注册失败，请更换用户名后，请重试！", true });
                //MsgBox.Show(this, "系统", "注册失败，请更换用户名后，请重试！");
            }
            catch (Exception)
            {

            }
        }

        private void XmppCon_OnRegistered(object sender)
        {
            try
            {
                //MsgBox.Show(this, "系统", "注册成功！");
                this.Invoke(ShowMessageBoxEvent, new object[] { "注册成功！", true });
                
            }
            catch (Exception)
            {

            }
           
        }

        private void ShowMessageBoxMethod(string args, bool isClose)
        {
            MsgBox.Show(this, "CSS&IM", args, MessageBoxIcon.Asterisk);

            try
            {
                XmppCon.Close();
            }
            catch (Exception)
            {

            }

            if (isClose)
            {
                try
                {
                    this.Close();
                }
                catch (Exception)
                {

                }
            }
        }

        private void btn_regedit_Click(object sender, EventArgs e)
        {

            if (txt_user.Texts.Trim().Length==0)
            {
                MsgBox.Show(this, "CSS&IM", "用户名不能为空！", MessageBoxButtons.OK);
                txt_user.Focus();
                return;
            }

            if (txt_pswd01.Texts.Trim().Length == 0)
            {
                MsgBox.Show(this, "CSS&IM", "用户名密码不能为空！", MessageBoxButtons.OK);
                txt_pswd01.Focus();
                return;
            }


            if (txt_pswd02.Texts.Trim().Length == 0)
            {
                MsgBox.Show(this, "CSS&IM", "用户名密码不能为空！", MessageBoxButtons.OK);
                txt_pswd02.Focus();
                return;
            }


            if (txt_pswd01.Texts.Trim() != txt_pswd02.Texts.Trim())
            {
                MsgBox.Show(this, "CSS&IM", "密码不统一！", MessageBoxButtons.OK);
                txt_pswd01.Focus();
                return;
            }

            if (!Util.IsWholeString(txt_user.Texts.ToString()))
            {
                MsgBox.Show(this, "CSS&IM", "用户名必须为字母与数字的组合", MessageBoxButtons.OK);
                txt_user.Focus();
                return;
            }


            try
            {
                XmppCon.Username = txt_user.Texts.ToString();
                XmppCon.Password = txt_pswd02.Texts.ToString().Trim();
                XmppCon.RegisterAccount = true;
                XmppCon.Open();
            }
            catch (Exception ex)
            {

                MsgBox.Show(this, "CSS&IM", "注册失败," + ex.Message, MessageBoxButtons.OK);
            }
            
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
