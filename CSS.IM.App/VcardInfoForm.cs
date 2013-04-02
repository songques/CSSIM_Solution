using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.XMPP;
using CSS.IM.XMPP.protocol.iq.vcard;
using System.Drawing.Imaging;
using CSS.IM.XMPP.protocol.client;
using CSS.IM.App.Properties;
using System.IO;
using CSS.IM.App.Settings;

namespace CSS.IM.App
{
    public partial class VcardInfoForm : BasicForm
    {

        private XmppClientConnection XmppConn;
        private string packetId;
        private Jid TO_Jid;

        public VcardInfoForm(Jid jid,XmppClientConnection conn)
        {
            InitializeComponent();
            XmppConn = conn;
            TO_Jid = jid;

        }


        private void VcardResult(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {		
                BeginInvoke(new IqCB(VcardResult), new object[] { sender, iq, data });
                return;
            }
            if (iq.Type == IqType.result)
            {
                Vcard vcard = iq.Vcard;
                if (vcard != null)
                {

                    try
                    {
                        txt_username.Texts = TO_Jid.Bare;
                        txt_name.Texts = vcard.Fullname;
                        txt_nickname.Texts = vcard.Nickname;
                        txt_sex.SelectText = vcard.GetTag("sex") != null ? vcard.GetTag("sex").ToString() : "男";
                        if (vcard.GetTelephoneNumber(TelephoneType.VOICE, TelephoneLocation.HOME) != null)
                            txt_phone1.Texts = vcard.GetTelephoneNumber(TelephoneType.VOICE, TelephoneLocation.HOME).Number;
                        if (vcard.GetTelephoneNumber(TelephoneType.CELL, TelephoneLocation.HOME) != null)
                            txt_phone2.Texts = vcard.GetTelephoneNumber(TelephoneType.CELL, TelephoneLocation.HOME).Number;
                        if (vcard.GetEmailAddress(EmailType.HOME) != null)
                            txt_email.Texts = vcard.GetEmailAddress(EmailType.HOME).UserId;
                        if (vcard.GetTelephoneNumber(TelephoneType.FAX, TelephoneLocation.HOME) != null)
                            txt_fox.Texts = vcard.GetTelephoneNumber(TelephoneType.FAX, TelephoneLocation.HOME).Number;
                        if (vcard.Organization != null)
                            txt_depar.Texts = vcard.Organization.Unit;
                        txt_job.Texts = vcard.Title;
                        txt_desc.Texts = vcard.Description;

                    PBirthday:
                        try
                        {
                            txt_bar.Texts = vcard.Birthday.ToString("yyyy年MM月dd日");
                        }
                        catch (Exception)
                        {
                            vcard.Birthday = DateTime.Now;
                            goto PBirthday;
                        }


                        if (vcard.Organization != null)
                            txt_companyName.Texts = vcard.Organization.Name;
                        if (vcard.GetAddress(AddressLocation.WORK) != null)
                            txt_postalCode.Texts = vcard.GetAddress(AddressLocation.WORK).PostalCode;
                        if (vcard.GetAddress(AddressLocation.WORK) != null)
                            txt_province.Texts = vcard.GetAddress(AddressLocation.WORK).Region;
                        if (vcard.GetAddress(AddressLocation.WORK) != null)
                            txt_county.Texts = vcard.GetAddress(AddressLocation.WORK).Street;
                        if (vcard.GetAddress(AddressLocation.WORK) != null)
                            txt_companyAddress.Texts = vcard.GetAddress(AddressLocation.WORK).Country;//公司地址
                        txt_companyDefault.Texts = vcard.Url;

                        if (vcard.GetAddress(AddressLocation.HOME) != null)
                            txt_meDefault.Texts = vcard.GetAddress(AddressLocation.HOME).Region;//个人主页
                        if (vcard.GetAddress(AddressLocation.HOME) != null)
                            txt_meMarsk.Texts = vcard.GetAddress(AddressLocation.HOME).Street;//个人说明 

                        Photo photo = vcard.Photo;
                        if (photo != null)
                            pic_top.Image = vcard.Photo.Image;
                        else
                            pic_top.Image = CSS.IM.UI.Properties.Resources.big194;

    
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(this, "CSS&IM", "获取基本信息错误:" + ex.Message, MessageBoxButtons.OK);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 表单加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetInfoForm_Load(object sender, EventArgs e)
        {
            panel_basice.Visible = false;
            panel_extend.Visible = false;

            VcardIq viq = new VcardIq(IqType.get, new Jid(TO_Jid.Bare));
            packetId = viq.Id;
            XmppConn.IqGrabber.SendIq(viq, new IqCB(VcardResult), null, true);

            btn_basic_Click(null, null);
        }


        /// <summary>
        /// 基本资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_basic_Click(object sender, EventArgs e)
        {
            panel_basice.Visible = true;
            txt_username.Focus();
            txt_username.IsFocused = true;
            panel_extend.Visible = false;
            
        }

        /// <summary>
        /// 更多资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_basicext_Click(object sender, EventArgs e)
        {
            panel_basice.Visible = false;
            txt_companyName.Focus();
            txt_companyName.IsFocused = true;
            panel_extend.Visible = true;
            
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            SetInfoForm_Load(null, null);

        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 打开设置中心
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GotoSeting_Click(object sender, EventArgs e)
        {
            Util.SetingFormEventMothe();
        }

        
    }
}
