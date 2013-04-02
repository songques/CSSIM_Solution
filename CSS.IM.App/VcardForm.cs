using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.XMPP.protocol.iq.vcard;
using CSS.IM.XMPP;
using CSS.IM.XMPP.protocol.client;

namespace CSS.IM.App
{
    public partial class VcardForm : BasicForm
    {

        string packetId;
        private XmppClientConnection _connection;

        public VcardForm(Jid jid, XmppClientConnection con)
        {
            InitializeComponent();
            _connection = con;
            VcardIq viq = new VcardIq(IqType.get, new Jid(jid.Bare));
            
            packetId = viq.Id;
            con.IqGrabber.SendIq(viq, new IqCB(VcardResult), null,true);
            
        }


        private void VcardForm_Load(object sender, EventArgs e)
        {
	
        }


        private void VcardResult(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new IqCB(VcardResult), new object[] { sender, iq, data });
                return;
            }
            if (iq.Type == IqType.result)
            {
                Vcard vcard = iq.Vcard;
                if (vcard != null)
                {
                    txt_name.Text = vcard.Fullname;
                    txt_nickname.Text = vcard.Nickname;
                    txt_birthday.Text = vcard.Birthday.ToString("yyy-MM-dd");
                    txt_desc.Text = vcard.Description;
                    Photo photo = vcard.Photo;
                    if (photo != null)
                        pic_top.Image = vcard.Photo.Image;
                    else
                        pic_top.Image = CSS.IM.UI.Util.ResClass.GetHead("big194");
                }
            }
        }

        private void txt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
