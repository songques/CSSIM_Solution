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
    public partial class FileListForm : BasicForm
    {
        public FileListForm()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FileListForm_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
