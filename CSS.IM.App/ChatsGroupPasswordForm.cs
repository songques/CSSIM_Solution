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
    public partial class ChatsGroupPasswordForm : BasicFormNC
    {
        public String Pswd { set;get;}
        public IQ iq { set;get;}

        /// <summary>
        /// 返回密码
        /// </summary>
        /// <param name="pswd"></param>
        public delegate void ChatsPasswordSetDelegate(IQ iq,String pswd);
        public event ChatsPasswordSetDelegate ChatsPasswordSetEvent;

        public ChatsGroupPasswordForm()
        {
            InitializeComponent();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            Pswd=this.txt_pswd.Texts;
            if (ChatsPasswordSetEvent!=null)
            {
                ChatsPasswordSetEvent(iq,Pswd);
            }
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
