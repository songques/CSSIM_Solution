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
    public partial class MoveFriendGroup : BasicForm
    {
        public MoveFriendGroup()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_move_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void MoveFriendGroup_Load(object sender, EventArgs e)
        {
            basicComboBox1.SelectItem = basicComboBox1.Items[basicComboBox1.SelectIndex];
        }
    }
}
