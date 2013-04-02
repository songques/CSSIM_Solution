using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.UI.Util;
using CSS.IM.App.Settings;

namespace CSS.IM.App
{
    public partial class SetingServerInfo : BasicForm
    {
        //Document doc_login = new Document();
        //Settings.Settings settings = new Settings.Settings();
        //Settings.ServerInfo serverInfo = null;
        public string ServerIP { set; get; }
        public string Port { set; get; }

        public SetingServerInfo()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                //serverInfo.ServerIP = txt_ServerIP.Texts;
                //serverInfo.ServerPort = txt_ServerPort.Texts;
                //doc_login.Save(Path.SettingsFilename);
                ServerIP = txt_ServerIP.Texts;
                Port = txt_ServerPort.Texts;
                this.Close();
            }
            catch (Exception ex)
            {
                MsgBox.Show(this, "CSS&IM", "保存失败，" + ex.Message, MessageBoxButtons.OK);
            }
            
        }

        private void SetingServerInfo_Load(object sender, EventArgs e)
        {
             txt_ServerIP.Texts=ServerIP;
             txt_ServerPort.Texts=Port;

            //if (!System.IO.File.Exists(CSS.IM.UI.Util.Path.SettingsFilename))
            //{
            //    Document doc = new Document();

            //    Settings.Settings settings = new Settings.Settings();

            //    Settings.Login login = new Settings.Login();
            //    ServerInfo serverinfo = new ServerInfo();

            //    settings.ServerInfo = serverinfo;

            //    doc.ChildNodes.Add(settings);
            //    doc.Save(CSS.IM.UI.Util.Path.SettingsFilename);
            //}

            //doc_login.LoadFile(Path.SettingsFilename);
            //serverInfo = doc_login.RootElement.SelectSingleElement(typeof(Settings.ServerInfo)) as Settings.ServerInfo;
            //txt_ServerIP.Texts = serverInfo.ServerIP;
            //txt_ServerPort.Texts = serverInfo.ServerPort;
            
        }
    }
}
