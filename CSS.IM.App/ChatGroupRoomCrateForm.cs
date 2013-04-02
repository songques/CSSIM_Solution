using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.XMPP;
using CSS.IM.XMPP.protocol.client;
using System.Diagnostics;
using CSS.IM.XMPP.protocol.x.data;
using CSS.IM.XMPP.protocol.iq.vcard;
using CSS.IM.XMPP.protocol.x;
using CSS.IM.XMPP.protocol.x.vcard_update;
using CSS.IM.XMPP.protocol.x.muc;
using CSS.IM.XMPP.util;
using CSS.IM.XMPP.protocol.iq.disco;
using CSS.IM.XMPP.Collections;

namespace CSS.IM.App
{
    public partial class ChatGroupRoomCrateForm : BasicFormNC
    {
        public Jid MJid { set; get; }
        public XmppClientConnection XmppCon;

        private MucManager mucManager = null;

        /// <summary>
        /// 房间创建完成事件
        /// </summary>
        /// <param name="jid"></param>
        public delegate void CreateRoomOverDelegate(Jid jid,String pswd);
        public event CreateRoomOverDelegate CreateRoomOverEvent;


        public ChatGroupRoomCrateForm(Jid args, XmppClientConnection con)
        {
            this.XmppCon = con;
            MJid = args;
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 聊天室的消息
        /// </summary>
        public void MessageCallback(object sender, CSS.IM.XMPP.protocol.client.Message msg, object data)
        {
            if (msg.Body=="该房间现在已解锁。")
            {
                SetConfiguration();
            }
        }

        private void btn_crate_Click(object sender, EventArgs e)
        {

            if (txt_pswd1.Texts.Trim()!=txt_pswd2.Texts.Trim())
            {
                MsgBox.Show(this, "CSS&IM", "两次密码不一制!", MessageBoxButtons.OK);
                return;
            }

            MJid=new Jid(txt_name.Texts.Trim().ToString(), MJid.Server, null);
            XmppCon.MessageGrabber.Add(MJid, new BareJidComparer(), new MessageCB(MessageCallback), null);

            mucManager = new MucManager(XmppCon);
            mucManager.CreateReservedRoom(new Jid(txt_name.Texts.Trim().ToString(), MJid.Server, null), new IqCB(CreateReservedRoom), null);
           

        }

        /// <summary>
        /// 创建房间回调事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="iq"></param>
        /// <param name="data"></param>
        public void CreateReservedRoom(object sender, IQ iq, object data)
        {
            mucManager.JoinRoom(iq.From, XmppCon.MyJID.User,true);;
            mucManager.AcceptDefaultConfiguration(MJid);

        }

       
        /// <summary>
        /// 设置聊天室的参数
        /// </summary>
        public void SetConfiguration()
        {
            IQ IqSetRquest = new IQ(IqType.set);
            IqSetRquest.Namespace = null;
            IqSetRquest.Id = CSS.IM.XMPP.Id.GetNextId();
            IqSetRquest.To = this.MJid;

            CSS.IM.XMPP.protocol.Base.Query query = new CSS.IM.XMPP.protocol.Base.Query();
            query.Namespace = CSS.IM.XMPP.Uri.MUC_OWNER;

            Data query_x = new Data(XDataFormType.submit);

            Field field1 = new Field();
            field1.Var = "FORM_TYPE";
            field1.Type = FieldType.Hidden;
            field1.AddValue(Features.FEAT_MUC_ROOMCONFIG);
            query_x.AddField(field1);

            Field field2 = new Field();
            field2.Var = "muc#roomconfig_roomname";
            field2.Type = FieldType.Text_Single;
            field2.AddValue(txt_name.Texts.Trim());
            query_x.AddField(field2);

            Field field3 = new Field();
            field3.Var = "muc#roomconfig_roomdesc";
            field3.Type = FieldType.Text_Single;
            field3.AddValue(txt_tm.Texts.Trim());
            query_x.AddField(field3);

            Field field4 = new Field();
            field4.Var = "muc#roomconfig_persistentroom";
            field4.Type = FieldType.Boolean;
            field4.AddValue(txt_gd.Checked == true ? "1" : "0");
            query_x.AddField(field4);

            Field field5 = new Field();
            field5.Var = "muc#roomconfig_passwordprotectedroom";
            field5.Type = FieldType.Boolean;
            field5.AddValue(txt_sy.Checked == true ? "1" : "0");
            query_x.AddField(field5);

            Field field6 = new Field();
            field6.Var = "muc#roomconfig_roomsecret";
            field6.Type = FieldType.Text_Private;
            field6.AddValue(txt_pswd2.Texts.ToString());
            query_x.AddField(field6);

            Field field7 = new Field();
            field7.Var = "muc#roomconfig_roomowners";
            field7.Type = FieldType.Jid_Multi;
            field7.AddValue(XmppCon.MyJID.ToString());
            query_x.AddField(field7);

            query.AddChild(query_x);
            IqSetRquest.AddChild(query);
            XmppCon.IqGrabber.SendIq(IqSetRquest, new IqCB(SetConfigurationCell), null, true);
        }

        /// <summary>
        /// 设置聊天室参数后返回信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="iq"></param>
        /// <param name="data"></param>
        public void SetConfigurationCell(object sender, IQ iq, object data)
        {
            if (CreateRoomOverEvent!=null)
            {
                CreateRoomOverEvent(MJid, txt_pswd2.Texts.ToString());
            }
            //Debug.WriteLine("设置聊天室参数后的信息:"+iq.ToString());
        }
    }
}
