using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using CSS.IM.UI.Util;


namespace CSS.IM.UI.Form
{
    public partial class BasicMsgBoxForm : BasicForm
    {
        private string message = "";
        private int mode = 1;

        public BasicMsgBoxForm(string message,string title,MessageBoxButtons mbb,MessageBoxIcon mbi)
        {
            this.message=message;
            this.Text=title;
            InitializeComponent();
            Font f = null;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                f = new Font("微软雅黑", 10F, FontStyle.Regular);
            }
            else
            {
                f = new Font("宋体", 10F, FontStyle.Regular);
            }
            messageText.Font = f;
            f.Dispose();
            switch (mbb)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    break;
                case MessageBoxButtons.OK:
                    ok.Left = this.Width - ok.Width - 10;
                    break;
                case MessageBoxButtons.OKCancel:
                    mode = 1;
                    ok.Texts = "确定";
                    canel.Texts = "取消";
                    canel.Visible = true;
                    break;
                case MessageBoxButtons.RetryCancel:
                    no.Visible = true;
                    ok.Visible = false;
                    Retry.Visible = true;
                    break;
                case MessageBoxButtons.YesNo:
                    mode = 2;
                    ok.Left = ok.Left-15;
                    canel.Left = canel.Left - 15;
                    ok.Texts = "是";
                    canel.Texts = "否";
                    canel.Visible = true;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    mode = 3;
                    ok.Texts = "是";
                    no.Texts = "取消";
                    canel.Texts = "否";
                    canel.Visible = true;
                    no.Visible = true;
                    break;
            }

            switch (mbi)
            {
                case MessageBoxIcon.Asterisk:
                    iconPic.Image = ResClass.GetImgRes("sysmessagebox_inforFile");
                    break;
                case MessageBoxIcon.Error:
                    iconPic.Image = ResClass.GetImgRes("sysmessagebox_errorFile");
                    break;
                case MessageBoxIcon.Exclamation:
                    iconPic.Image = ResClass.GetImgRes("sysmessagebox_warningFile");
                    break;
                case MessageBoxIcon.None:
                    break;
                case MessageBoxIcon.Question:
                    iconPic.Image = ResClass.GetImgRes("sysmessagebox_questionFile");
                    break;
            }
            if (timer_update.Enabled==false)
            {
                timer_update.Enabled = true;
            }
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            messageText.Text = message;
        }

        private void iButton1_Click(object sender, EventArgs e)
        {
            if (mode == 1)
            {
                this.DialogResult = DialogResult.OK;
            }
            if (mode == 2)
            {
                this.DialogResult = DialogResult.Yes;
            }
            this.Close();
        }

        private void canel_Click(object sender, EventArgs e)
        {
            if (mode == 2)
            {
                this.DialogResult = DialogResult.No;
            }
            this.Close();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Enter)
            {
                iButton1_Click(null, null);
            }
        }

        private void Retry_MouseClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void no_MouseClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void timer_update_Tick(object sender, EventArgs e)
        {
            TopMost = true;
        }
    }
}
