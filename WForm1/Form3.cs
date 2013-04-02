using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.XMPP.Xml.Dom;

namespace WForm1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Document document = new Document();
            document.LoadFile(@"C:\Users\Administrator\Desktop\test.txt");
            CSS.IM.XMPP.protocol.client.Message top_msg = (CSS.IM.XMPP.protocol.client.Message)document.RootElement;
            ElementList properte_list =top_msg.SelectElements("properties");

 

            int count = properte_list.Item(0).SelectElements("property").Count;
            MqMessage mqMsg = new MqMessage();
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

            MessageBox.Show(mqMsg.ToString());
        }
    }
}
