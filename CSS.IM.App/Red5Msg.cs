using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;

namespace CSS.IM.App
{
    public partial class Red5Msg : BasicForm
    {
        public string Red5Url { set; get; }
        public string FriendName { set; get; }

        public Red5Msg()
        {
            InitializeComponent();
        }

        public void Cell(string url)
        {
            this.Text = FriendName;
            web_red5.Url = new Uri(url);
        }

        public void Accept(string url)
        {
            this.Text = FriendName;
            web_red5.Url = new Uri(url);
        }
    }
}
