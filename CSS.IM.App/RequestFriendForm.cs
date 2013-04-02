using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.XMPP.protocol.client;

namespace CSS.IM.App
{
    public partial class RequestFriendForm : BasicForm
    {
        private XMPP.XmppClientConnection XmppCon;
        private XMPP.Jid jid;

        public RequestFriendForm(XMPP.XmppClientConnection XmppCon, XMPP.Jid jid)
        {
            // TODO: Complete member initialization
            this.XmppCon = XmppCon;
            this.jid = jid;
            InitializeComponent();
            this.lab_friend.Text = jid.ToString();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            PresenceManager pm = new PresenceManager(XmppCon);
            pm.ApproveSubscriptionRequest(jid);

            XmppCon.RosterManager.AddRosterItem(jid);
            XmppCon.PresenceManager.Subscribe(jid);

            this.Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            PresenceManager pm = new PresenceManager(XmppCon);
            pm.RefuseSubscriptionRequest(jid);

            this.Close();
        }
    }
}
