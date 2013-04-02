using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CSS.IM.UI.Form
{
    public class MsgBox
    {
        private MsgBox() {
        
        }
        public static DialogResult Show(string message)
        {
            BasicMsgBoxForm im = new BasicMsgBoxForm(message,"", MessageBoxButtons.OK,MessageBoxIcon.Information);
            im.StartPosition = FormStartPosition.CenterScreen;
            return im.ShowDialog();
        }

        public static void Show(string title, string message)
        {
            BasicMsgBoxForm im = new BasicMsgBoxForm(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            im.StartPosition = FormStartPosition.CenterScreen;
            im.Show();
        }

        public static DialogResult Show(IWin32Window form, string message)
        {
            BasicMsgBoxForm im = new BasicMsgBoxForm(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            im.StartPosition = FormStartPosition.CenterParent;
            return im.ShowDialog(form);
        }

        public static DialogResult Show(IWin32Window form, string title, string message)
        {
            BasicMsgBoxForm im = new BasicMsgBoxForm(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            im.StartPosition = FormStartPosition.CenterParent;
            return im.ShowDialog(form);
        }

        public static DialogResult Show(IWin32Window form, string title, string message, MessageBoxIcon mbi)
        {
            BasicMsgBoxForm im = new BasicMsgBoxForm(message, title, MessageBoxButtons.OK,mbi);
            im.StartPosition = FormStartPosition.CenterParent;
            return im.ShowDialog(form);
        }

        public static DialogResult Show(IWin32Window form, string title, string message, MessageBoxButtons mbb)
        {
            BasicMsgBoxForm im = new BasicMsgBoxForm(message, title, mbb, MessageBoxIcon.Information);
            im.StartPosition = FormStartPosition.CenterParent;
            return im.ShowDialog(form);
        }

        public static DialogResult Show(IWin32Window form, string title, string message, MessageBoxButtons mbb, MessageBoxIcon mbi)
        {
            BasicMsgBoxForm im = new BasicMsgBoxForm(message, title, mbb, mbi);
            im.StartPosition = FormStartPosition.CenterParent;
            return im.ShowDialog(form);
        }
    }
}
