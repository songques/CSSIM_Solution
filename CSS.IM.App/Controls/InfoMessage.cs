using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CSS.IM.App.Controls
{
    public partial class InfoMessage : UserControl
    {
        public InfoMessage()
        {
            InitializeComponent();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            timer1.Enabled = false;    
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Visible = false;
            timer1.Enabled = false;    
        }

        /// <summary>
        /// 显示提示消息
        /// </summary>
        /// <param name="Msg">要显示的消息字符串</param>
        public void showMsg(string Msg)
        {
            this.labMsg.Text ="！"+ Msg;
            this.Visible = true;
            timer1.Enabled = false;
            timer1.Enabled = true;    
        }
    }
}
