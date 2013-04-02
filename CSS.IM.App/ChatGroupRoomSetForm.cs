using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.XMPP.protocol.x.data;
using CSS.IM.XMPP.protocol.client;
using CSS.IM.XMPP;
using CSS.IM.XMPP.protocol.Base;

namespace CSS.IM.App
{
    public partial class ChatGroupRoomSetForm : BasicFormNC
    {
        /// <summary>
        /// 保存设置信息的所有字段
        /// </summary>
        public Data fields { get; set; }

        /// <summary>
        /// 保存发发送的jjid
        /// </summary>
        public Jid to_jid { get; set; }

        /// <summary>
        /// 设置网络管理
        /// </summary>
        public XmppClientConnection XMPPConn { set; get; }

        public ChatGroupRoomSetForm()
        {
            InitializeComponent();
        }

        private void ChatGroupRoomSetForm_Load(object sender, EventArgs e)
        {
            string selectValue = "";
            string[] selectValues;
            //名称
            txt_roomconfig_roomname.Texts = fields.GetField("muc#roomconfig_roomname").GetValue();
            //描述
            txt_roomconfig_roomdesc.Texts = fields.GetField("muc#roomconfig_roomdesc").GetValue();
            //允许占有者改更主题
            chb_roomconfig_changesubject.Checked = fields.GetField("muc#roomconfig_changesubject").GetValue() == "1" ? true : false;

            //其Presence是Broadcase的角色
            selectValue = fields.GetField("muc#roomconfig_maxusers").GetValue();
            Option[] options = fields.GetField("muc#roomconfig_maxusers").GetOptions();
            string[] cbb_roomconfig_maxusersValue = new string[options.Length];

            for (int i = 0; i < options.Length; i++)
            {
                cbb_roomconfig_maxusersValue[i] = options[i].GetValue();
            }

            cbb_roomconfig_maxusers.Items = cbb_roomconfig_maxusersValue;
            cbb_roomconfig_maxusers.SelectItem = selectValue;

            string[] roomconfig_presencebroadcastValue = fields.GetField("muc#roomconfig_presencebroadcast").GetValues();

            for (int i = 0; i < roomconfig_presencebroadcastValue.Length; i++)
            {
                switch (roomconfig_presencebroadcastValue[i])
                {
                    case "moderator":
                        chb_moderator.Visible = true;
                        continue;
                    case "participant":
                        chb_participant.Visible = true;
                        continue;
                    case "visitor":
                        chb_visitor.Visible = true;
                        continue;
                }
            }

            //列出目录中的房间
            chb_roomconfig_publicroom.Checked = fields.GetField("muc#roomconfig_publicroom").GetValue() == "1" ? true : false;
            //房间是持久的
            chb_roomconfig_persistentroom.Checked = fields.GetField("muc#roomconfig_persistentroom").GetValue() == "1" ? true : false;
            //房间是适度的
            chb_roomconfig_moderatedroom.Checked = fields.GetField("muc#roomconfig_moderatedroom").GetValue() == "1" ? true : false;
            //房间公对成员开放
            chb_roomconfig_membersonly.Checked = fields.GetField("muc#roomconfig_membersonly").GetValue() == "1" ? true : false;
            //允许占有者邀请其他人
            chb_roomconfig_allowinvites.Checked = fields.GetField("muc#roomconfig_allowinvites").GetValue() == "1" ? true : false;
            //需要密码才能进入房间
            chb_roomconfig_passwordprotectedroom.Checked = fields.GetField("muc#roomconfig_passwordprotectedroom").GetValue() == "1" ? true : false;
            //密码
            chb_roomconfig_roomsecret.Texts = fields.GetField("muc#roomconfig_roomsecret").GetValue();

            //能够发现占有者真实 JID 的角色
            selectValue = fields.GetField("muc#roomconfig_whois").GetValue();
            options = fields.GetField("muc#roomconfig_whois").GetOptions();
            string[] cbb_roomconfig_whoisValue = new string[options.Length];
            for (int i = 0; i < options.Length; i++)
            {
                cbb_roomconfig_whoisValue[i] = options[i].GetValue();
            }

            cbb_roomconfig_whois.Items = cbb_roomconfig_whoisValue;
            cbb_roomconfig_whois.SelectItem = selectValue;

            //登录房间对话
            chb_roomconfig_enablelogging.Checked = fields.GetField("muc#roomconfig_enablelogging").GetValue() == "1" ? true : false;
            //仅允许注册的昵称登录
            chb_roomconfig_reservednick.Checked = fields.GetField("x-muc#roomconfig_reservednick").GetValue() == "1" ? true : false;
            //允许使用者修改昵称
            chb_roomconfig_canchangenick.Checked = fields.GetField("x-muc#roomconfig_canchangenick").GetValue() == "1" ? true : false;
            //允许用户注册房间
            chb_roomconfig_registration.Checked = fields.GetField("x-muc#roomconfig_registration").GetValue() == "1" ? true : false;

            //房间管理员
            list_roomconfig_roomadmins.Items.Clear();
            selectValues=fields.GetField("muc#roomconfig_roomadmins").GetValues();
            foreach (string item in selectValues)
            {
                list_roomconfig_roomadmins.Items.Add(item);
            }


            //房间拥有者
            list_roomconfig_roomowners.Items.Clear();
            selectValues = fields.GetField("muc#roomconfig_roomowners").GetValues();
            foreach (string item in selectValues)
            {
                list_roomconfig_roomowners.Items.Add(item);
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {

            //fields.RemoveTag("title");
            //fields.RemoveTag("instructions");


            //selectValue = fields.GetField("muc#roomconfig_maxusers").GetValue();
            //Option[] options = fields.GetField("muc#roomconfig_maxusers").GetOptions();
            //string[] cbb_roomconfig_maxusersValue = new string[options.Length];



            //chb_moderator

            //chb_participant

            //chb_visitor



            //for (int i = 0; i < options.Length; i++)
            //{
            //    cbb_roomconfig_maxusersValue[i] = options[i].GetValue();
            //}

            //cbb_roomconfig_maxusers.Items = cbb_roomconfig_maxusersValue;
            //cbb_roomconfig_maxusers.SelectItem = selectValue;


            fields.GetField("muc#roomconfig_roomsecret").SetValue(chb_roomconfig_roomsecret.Texts);//设置密码

            IQ iqSendSet = new IQ();
            iqSendSet.Namespace = null;
            iqSendSet.Id = CSS.IM.XMPP.Id.GetNextId();
            iqSendSet.To = to_jid;
            iqSendSet.Type = IqType.set;
            Query query = new Query();
            query.Namespace = CSS.IM.XMPP.Uri.MUC_OWNER;

            Data datas = new Data();
            datas.Namespace = CSS.IM.XMPP.Uri.X_DATA;
            datas.Type = XDataFormType.submit;
            //1
            Field FORM_TYPE = new Field();
            FORM_TYPE.Type = FieldType.Hidden;
            FORM_TYPE.Var = "FORM_TYPE";
            FORM_TYPE.SetValue(@"http://jabber.org/protocol/muc#roomconfig");
            datas.AddField(FORM_TYPE);
            //2
            Field roomconfig_roomname = new Field();
            roomconfig_roomname.Type = FieldType.Text_Single;
            roomconfig_roomname.Var = "muc#roomconfig_roomname";
            roomconfig_roomname.SetValue(txt_roomconfig_roomname.Texts.ToString());
            datas.AddField(roomconfig_roomname);
            //3
            Field roomconfig_roomdesc = new Field();
            roomconfig_roomdesc.Type = FieldType.Text_Single;
            roomconfig_roomdesc.Var = "muc#roomconfig_roomdesc";
            roomconfig_roomdesc.SetValue(txt_roomconfig_roomdesc.Texts.ToString());
            datas.AddField(roomconfig_roomdesc);
            //4
            Field roomconfig_changesubject = new Field();
            roomconfig_changesubject.Type = FieldType.Boolean;
            roomconfig_changesubject.Var = "muc#roomconfig_changesubject";
            roomconfig_changesubject.SetValueBool(chb_roomconfig_changesubject.Checked);
            datas.AddField(roomconfig_changesubject);
            //5
            Field roomconfig_maxusers = new Field();
            roomconfig_maxusers.Type = FieldType.List_Single;
            roomconfig_maxusers.Var = "muc#roomconfig_maxusers";
            roomconfig_maxusers.SetValue(cbb_roomconfig_maxusers.SelectItem.ToString());
            datas.AddField(roomconfig_maxusers);
            //6
            Field roomconfig_publicroom = new Field();
            roomconfig_publicroom.Type = FieldType.Boolean;
            roomconfig_publicroom.Var = "muc#roomconfig_publicroom";
            roomconfig_publicroom.SetValueBool(chb_roomconfig_publicroom.Checked);
            datas.AddField(roomconfig_publicroom);
            //7
            Field roomconfig_persistentroom = new Field();
            roomconfig_persistentroom.Type = FieldType.Boolean;
            roomconfig_persistentroom.Var = "muc#roomconfig_persistentroom";
            roomconfig_persistentroom.SetValueBool(chb_roomconfig_persistentroom.Checked);
            datas.AddField(roomconfig_persistentroom);
            //8
            Field roomconfig_moderatedroom = new Field();
            roomconfig_moderatedroom.Type = FieldType.Boolean;
            roomconfig_moderatedroom.Var = "muc#roomconfig_moderatedroom";
            roomconfig_moderatedroom.SetValueBool(chb_roomconfig_moderatedroom.Checked);
            datas.AddField(roomconfig_moderatedroom);
            //9
            Field roomconfig_membersonly = new Field();
            roomconfig_membersonly.Type = FieldType.Boolean;
            roomconfig_membersonly.Var = "muc#roomconfig_membersonly";
            roomconfig_membersonly.SetValueBool(chb_roomconfig_membersonly.Checked);
            datas.AddField(roomconfig_membersonly);
            //10
            Field roomconfig_allowinvites = new Field();
            roomconfig_allowinvites.Type = FieldType.Boolean;
            roomconfig_allowinvites.Var = "muc#roomconfig_allowinvites";
            roomconfig_allowinvites.SetValueBool(chb_roomconfig_allowinvites.Checked);
            datas.AddField(roomconfig_allowinvites);
            //11
            Field roomconfig_passwordprotectedroom = new Field();
            roomconfig_passwordprotectedroom.Type = FieldType.Boolean;
            roomconfig_passwordprotectedroom.Var = "muc#roomconfig_passwordprotectedroom";
            roomconfig_allowinvites.SetValueBool(chb_roomconfig_passwordprotectedroom.Checked);
            datas.AddField(roomconfig_passwordprotectedroom);
            //12
            Field roomconfig_whois = new Field();
            roomconfig_whois.Type = FieldType.Text_Single;
            roomconfig_whois.Var = "muc#roomconfig_whois";
            roomconfig_whois.SetValue(cbb_roomconfig_whois.SelectItem.ToString());
            datas.AddField(roomconfig_whois);
            //13
            Field roomconfig_enablelogging = new Field();
            roomconfig_enablelogging.Type = FieldType.Boolean;
            roomconfig_enablelogging.Var = "muc#roomconfig_enablelogging";
            roomconfig_enablelogging.SetValueBool(chb_roomconfig_enablelogging.Checked);
            datas.AddField(roomconfig_enablelogging);
            //14
            Field roomconfig_reservednick = new Field();
            roomconfig_reservednick.Type = FieldType.Boolean;
            roomconfig_reservednick.Var = "x-muc#roomconfig_reservednick";
            roomconfig_reservednick.SetValueBool(chb_roomconfig_reservednick.Checked);
            datas.AddField(roomconfig_reservednick);
            //15
            Field roomconfig_canchangenick = new Field();
            roomconfig_canchangenick.Type = FieldType.Boolean;
            roomconfig_canchangenick.Var = "x-muc#roomconfig_canchangenick";
            roomconfig_canchangenick.SetValueBool(chb_roomconfig_canchangenick.Checked);
            datas.AddField(roomconfig_canchangenick);
            //16
            Field roomconfig_registration = new Field();
            roomconfig_registration.Type = FieldType.Boolean;
            roomconfig_registration.Var = "x-muc#roomconfig_registration";
            roomconfig_registration.SetValueBool(chb_roomconfig_registration.Checked);
            datas.AddField(roomconfig_registration);

            Field roomconfig_roomadmins = new Field();
            roomconfig_roomadmins.Type = FieldType.Jid_Multi;
            roomconfig_roomadmins.Var = "muc#roomconfig_roomadmins";

            string[] itemsToStrings = new string[list_roomconfig_roomadmins.Items.Count];
            for (int i = 0; i <  list_roomconfig_roomadmins.Items.Count; i++)
            {
                itemsToStrings[i] =  list_roomconfig_roomadmins.Items[i].Text;
            }

            roomconfig_roomadmins.SetValues(itemsToStrings);
            datas.AddField(roomconfig_roomadmins);

            Field roomconfig_roomowners = new Field();
            roomconfig_roomowners.Type = FieldType.Jid_Multi;
            roomconfig_roomowners.Var = "muc#roomconfig_roomowners";
            
            if (list_roomconfig_roomowners.Items.Count>0)
            {
                itemsToStrings = new string[list_roomconfig_roomowners.Items.Count];
                for (int i = 0; i < list_roomconfig_roomowners.Items.Count; i++)
                {
                    itemsToStrings[i] = list_roomconfig_roomowners.Items[i].Text;
                }
            }
            else
            {
                string[] selectValues = fields.GetField("muc#roomconfig_roomadmins").GetValues();
                itemsToStrings = new string[selectValues.Length];

                for (int i = 0; i < selectValues.Length; i++)
                {
                    itemsToStrings[i] = selectValues[i];
                }
            }
    
            roomconfig_roomowners.SetValues(itemsToStrings);
            datas.AddField(roomconfig_roomowners);



            foreach (Field item in fields.GetFields())
            {
                datas.AddField(item);
            }

            query.AddChild(datas);
            iqSendSet.AddChild(query);

            System.Diagnostics.Debug.WriteLine("发送的数据:\n" + iqSendSet.ToString());
            XMPPConn.IqGrabber.SendIq(iqSendSet,new IqCB(SendSetingCell),null,true);

        }

        public void SendSetingCell(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {
                this.Invoke(new IqCB(SendSetingCell), new object[] {sender,iq,data });
            }
            System.Diagnostics.Debug.WriteLine("返回的数据:\n" + iq.ToString());
            btn_cancel_Click(null, null);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btn_add_roomadmins_Click(object sender, EventArgs e)
        {
            FindFriendForm _FindFriendForm = new FindFriendForm(XMPPConn);
            _FindFriendForm.ISSelect = true;
            DialogResult result = _FindFriendForm.ShowDialog();
            if (result==DialogResult.OK)
            {
                list_roomconfig_roomadmins.Items.Add(_FindFriendForm.SelectJid.Bare.ToString());
            }
            _FindFriendForm.Dispose();
        }

        private void btn_add_roomowners_Click(object sender, EventArgs e)
        {
            FindFriendForm _FindFriendForm = new FindFriendForm(XMPPConn);
            _FindFriendForm.ISSelect = true;
            DialogResult result = _FindFriendForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                list_roomconfig_roomowners.Items.Add(_FindFriendForm.SelectJid.Bare.ToString());
            }
            _FindFriendForm.Dispose();
        }

        private void list_roomconfig_roomadmins_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (list_roomconfig_roomadmins.SelectedIndices!=null)
            {
                if (list_roomconfig_roomadmins.SelectedIndices.Count>0)
                {
                    list_roomconfig_roomadmins.Items.RemoveAt(list_roomconfig_roomadmins.SelectedIndices[0]);
                }
            }
        }

        private void list_roomconfig_roomowners_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (list_roomconfig_roomowners.SelectedIndices != null)
            {
                if (list_roomconfig_roomowners.SelectedIndices.Count > 0)
                {
                    list_roomconfig_roomowners.Items.RemoveAt(list_roomconfig_roomowners.SelectedIndices[0]);
                }
            }
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            this.Invalidate();
        }
    }
}
