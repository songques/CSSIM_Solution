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
    public partial class SetInfoForm : BasicForm
    {

        public delegate void update_top_image();
        public event update_top_image update_top_image_event;

        private XmppClientConnection XmppConn;

        private Vcard SaveVcard;

        public SetInfoForm(XmppClientConnection conn)
        {
            InitializeComponent();
            XmppConn = conn;
            GetVcardResult();

            //VcardIq viq = new VcardIq(IqType.get, null, new Jid(_connection.MyJID.User));
            //_connection.IqGrabber.SendIq(viq, new IqCB(GetVcardResult), null);
        }


        /// <summary>
        /// 获取基本资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="iq"></param>
        /// <param name="data"></param>
        //private void GetVcardResult(object sender, IQ iq, object data)
        private void GetVcardResult()
        {
            try
            {
                Vcard vcard = Util.vcard;
                if (vcard != null)
                {
                    txt_username.Texts = XmppConn.MyJID.Bare.ToString();
                    txt_name.Texts = vcard.Fullname;
                    txt_nickname.Texts = vcard.Nickname;
                    txt_sex.SelectText = vcard.GetTag("sex")!= null ? vcard.GetTag("sex").ToString() : "男";
                    if (vcard.GetTelephoneNumber(TelephoneType.VOICE, TelephoneLocation.HOME)!=null)
                        txt_phone1.Texts = vcard.GetTelephoneNumber(TelephoneType.VOICE, TelephoneLocation.HOME).Number;
                    if (vcard.GetTelephoneNumber(TelephoneType.CELL, TelephoneLocation.HOME)!=null)
                        txt_phone2.Texts = vcard.GetTelephoneNumber(TelephoneType.CELL, TelephoneLocation.HOME).Number;
                    if (vcard.GetEmailAddress(EmailType.HOME)!=null)
                        txt_email.Texts = vcard.GetEmailAddress(EmailType.HOME).UserId;
                    if (vcard.GetTelephoneNumber(TelephoneType.FAX, TelephoneLocation.HOME)!=null)
                        txt_fox.Texts = vcard.GetTelephoneNumber(TelephoneType.FAX, TelephoneLocation.HOME).Number;
                    if (vcard.Organization!=null)
                        txt_depar.Texts = vcard.Organization.Unit;
                    txt_job.Texts = vcard.Title;
                    txt_desc.Texts = vcard.Description;

                PBirthday:
                    try
                    {
                        txt_bar.Value = vcard.Birthday;
                    }
                    catch (Exception)
                    {
                        vcard.Birthday = txt_bar.Value;
                        goto PBirthday;
                    }


                    if (vcard.Organization!=null)
                        txt_companyName.Texts = vcard.Organization.Name;
                    if (vcard.GetAddress(AddressLocation.WORK)!=null)
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

                        vcard = null;
                }
                else
                {
                    //this.tab_state.Text = "";
                    if (update_top_image_event != null)
                    {
                        update_top_image_event();
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(this, "CSS&IM", "获取基本信息错误:" + ex.Message, MessageBoxButtons.OK);
                return;
            }
        }

        /// <summary>
        /// 回调保存基本资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="iq"></param>
        /// <param name="data"></param>
        private void SaveVcardResult(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new IqCB(SaveVcardResult), new object[] { sender, iq, data });
                return;
            }

            if (iq.Type == IqType.result)
            {
                //this.tab_state.Text = "";
                Util.vcard = SaveVcard;
                SaveVcard = null;

                GetVcardResult();

                Util.VcardChangeEventMethod();//通知名片更新事件

                MsgBox.Show(this, "CSS&IM", "基本资料保存成功！", MessageBoxButtons.OK);
            }
            else
            {
                //this.tab_state.Text = "";
                MsgBox.Show(this, "CSS&IM", "基本资料保存失败", MessageBoxButtons.OK);
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
            btn_basic_Click(null, null);
        }


        
        /// <summary>
        /// 选择头像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_brow_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "图片文件(*.bmp;*.jpg;*.gif)|*.bmp;*.jpg;*.gif";
                DialogResult reslut = file.ShowDialog();
                if (reslut == DialogResult.OK)
                {

                    FileInfo fileinfo = new FileInfo(file.FileName);

                    if (fileinfo.Length < 50000)
                    {
                        pic_top.Image = Image.FromFile(file.FileName);
                    }
                    else
                    {
                        MsgBox.Show(this, "CSS&IM", "头像超过50kb，请剪辑后在上传头像！", MessageBoxButtons.OK);
                    }

                }
            }
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
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            //if (txt_name.Texts.Trim().Length == 0)
            //{
            //    MsgBox.Show(this, "CSS&IM", "名字不能为空！", MessageBoxButtons.OK);
            //    return;
            //}

            if (txt_nickname.Texts.Trim().Length == 0)
            {
                txt_nickname.Texts = "";
                //MsgBox.Show(this, "CSS&IM", "名字不能为空！", MessageBoxButtons.OK);
                //return;
            }


            if (txt_desc.Texts.Trim().Length == 0)
            {
                txt_desc.Texts = "";
                //MsgBox.Show(this, "CSS&IM", "名字不能为空！", MessageBoxButtons.OK);
                //return;
            }


            //this.tab_state.Text = "数据更新中请等待……！";

            

            SaveVcard = new Vcard();
            SaveVcard.SetTag("sex", txt_sex.SelectText);//性别
            SaveVcard.Fullname = txt_name.Texts.ToString();//名字
            SaveVcard.Nickname = txt_nickname.Texts.ToString();//昵称
            SaveVcard.Birthday = txt_bar.Value;//生日


            Telephone telephone1 = new Telephone(TelephoneLocation.HOME, TelephoneType.VOICE, txt_phone1.Texts);//电话
            SaveVcard.AddTelephoneNumber(telephone1);
            Telephone telephone2 = new Telephone(TelephoneLocation.HOME, TelephoneType.CELL, txt_phone2.Texts);//手机
            SaveVcard.AddTelephoneNumber(telephone2);
            Email email = new Email(EmailType.HOME, txt_email.Texts, true);//邮箱
            SaveVcard.AddEmailAddress(email);
            Telephone telephone3 = new Telephone(TelephoneLocation.HOME, TelephoneType.FAX, txt_fox.Texts);//传真 
            SaveVcard.AddTelephoneNumber(telephone3);
            SaveVcard.Title = txt_job.Texts;//职务
            SaveVcard.Description = txt_desc.Texts.ToString();//个人签名

            Organization org = new Organization(txt_companyName.Texts, txt_depar.Texts);//公司名称
            SaveVcard.Organization = org;

            Address address1=new Address(AddressLocation.WORK,"",txt_county.Texts,"",txt_province.Texts,txt_postalCode.Texts,txt_companyAddress.Texts,true);
            SaveVcard.AddAddress(address1);
            SaveVcard.Url = txt_companyDefault.Texts;

            Address address2 = new Address(AddressLocation.HOME, "", txt_meMarsk.Texts, "", txt_meDefault.Texts, "", "", true);
            SaveVcard.AddAddress(address2);

            Photo po = new Photo(pic_top.Image, ImageFormat.Jpeg);
            SaveVcard.Photo = po;

            VcardIq viq = new VcardIq(IqType.set, null, new Jid(XmppConn.MyJID.User), SaveVcard);
            XmppConn.IqGrabber.SendIq(viq, new IqCB(SaveVcardResult), null);

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
