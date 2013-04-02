using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.UI.Util;
using CSS.IM.App.Settings;
using System.Diagnostics;
using Microsoft.Win32;

namespace CSS.IM.App
{
    public partial class SetingForm : BasicFormNC
    {
        /// <summary>
        /// 常规使用
        /// </summary>
        Document doc_login = null;
        Settings.Verify settings = null;
        Settings.Login login = null;

        /// <summary>
        /// 声音和提示
        /// </summary>
        Document doc_setting = null;
        Settings.Settings config = null;
        Settings.Paths path = null;

        public SetingForm()
        {
            InitializeComponent();
        }

        private void SetingForm_Load(object sender, EventArgs e)
        {
            btn_info_Click(null, null);

            this.TopMost = true;
            this.TopMost = false;

        }

        /// <summary>
        // 保存文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_appliy_Click(object sender, EventArgs e)
        {


            if (panel_sound.Visible)
            {
                SaveSound();
            }
            else if (panel_basice.Visible)
            {
                SaveBasice();
            }
            else if (panel_hotkey.Visible)
            {
                SaveHotkey();
            }
        }

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_info_Click(object sender, EventArgs e)
        {
            panel_info.Visible = true;
            panel_basice.Visible = false;
            panel_sendfile.Visible = false;
            panel_fastreply.Visible = false;
            panel_sound.Visible = false;
            panel_hotkey.Visible = false;
        }

        #region 个人信息
        private void btn_editinfo_Click(object sender, EventArgs e)
        {
            Util.SetInfoFormEventMothe();
        }
        #endregion

        /// <summary>
        /// 常规
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_basice_Click(object sender, EventArgs e)
        {
            #region 初使化是否自动登录

            doc_login = new Document();
            settings = new Settings.Verify();

            doc_login.LoadFile(string.Format(CSS.IM.UI.Util.Path.SettingsFilename, Program.UserName));
            login = doc_login.RootElement.SelectSingleElement(typeof(Settings.Login)) as Settings.Login;
            chb_autoLogin.Checked = login.Auto;//自动登录
            
            //是否开机启动
            if (login.InitIal)
                chb_auto.CheckState= CSS.IM.UI.Control.BasicCheckBox.CheckStates.Checked;
            else
                chb_auto.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;
            #endregion

            panel_info.Visible = false;
            panel_basice.Visible = true;
            panel_sendfile.Visible = false;
            panel_fastreply.Visible = false;
            panel_sound.Visible = false;
            panel_hotkey.Visible = false;
        }

        #region 常规

        /// <summary>
        /// 自动登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Checked"></param>
        private void chb_autoLogin_CheckedChanged(object sender, bool Checked)
        {

            login.Auto = Checked;
            if (Checked)
            {
                login.Save = true;
            }
        }

        /// <summary>
        /// 开机自动启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Checked"></param>
        private void chb_auto_CheckedChanged(object sender, bool Checked)
        {
            Util.RunWhenStart(chb_auto.Checked, "CSS&IM", Application.StartupPath + @"\CSSIM.exe");
            login.InitIal = Checked;
            CSS.IM.UI.Util.Path.Initial = login.InitIal;
            

        }

        /// <summary>
        /// 保存常规
        /// </summary>
        private void SaveBasice()
        {
            doc_login.Save(string.Format(CSS.IM.UI.Util.Path.SettingsFilename, Program.UserName));
        }

        #endregion

        /// <summary>
        /// 文件传输
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_sendfile_Click(object sender, EventArgs e)
        {
            panel_info.Visible = false;
            panel_basice.Visible = false;
            panel_sendfile.Visible = true;
            panel_fastreply.Visible = false;
            panel_sound.Visible = false;
            panel_hotkey.Visible = false;

            txt_savePath.Texts = Util.receiveImage;
        }

        #region 文件传输

        private void btn_openSaveFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(txt_savePath.Texts);
            }
            catch (Exception)
            {
                MsgBox.Show(this, "CSS&IM", "目录不正确无法打开！", MessageBoxButtons.OK);
            }
            
        }

        private void btn_editSaveFilePath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                DialogResult result=folder.ShowDialog();
                if (result==DialogResult.OK)
                {
                    Util.receiveImage = folder.SelectedPath;
                    txt_savePath.Texts = Util.receiveImage;
                }
            }
        }

        #endregion

        /// <summary>
        /// 快捷回复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_fastreply_Click(object sender, EventArgs e)
        {
            panel_info.Visible = false;
            panel_basice.Visible = false;
            panel_sendfile.Visible = false;
            panel_fastreply.Visible = true;
            panel_sound.Visible = false;
            panel_hotkey.Visible = false;
        }

        /// <summary>
        /// 声音和提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_sound_Click(object sender, EventArgs e)
        {
            #region 初使化设置信息

            doc_setting = new Document();
            config = new Settings.Settings();

            doc_setting.LoadFile(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
            path = doc_setting.RootElement.SelectSingleElement(typeof(Settings.Paths), false) as Settings.Paths;

            bool enable = false;
            txt_MsgPath.Texts = path.MsgPath;
            enable = path.SelectSingleElement("MsgPath").GetAttributeBool("Enable");
            if (enable)
                btn_MsgPathOpen.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Checked;
            else
                btn_MsgPathOpen.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;

            txt_SystemPath.Texts = path.SystemPath;
            enable = path.SelectSingleElement("SystemPath").GetAttributeBool("Enable");
            if (enable)
                btn_SystemPathOpen.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Checked;
            else
                btn_SystemPathOpen.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;

            txt_CallPath.Texts = path.CallPath;
            enable = path.SelectSingleElement("CallPath").GetAttributeBool("Enable");
            if (enable)
                btn_CallPathOpen.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Checked;
            else
                btn_CallPathOpen.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;

            txt_FolderPath.Texts = path.FolderPath;
            enable = path.SelectSingleElement("FolderPath").GetAttributeBool("Enable");
            if (enable)
                btn_FolderPathOpen.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Checked;
            else
                btn_FolderPathOpen.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;


            txt_GlobalPath.Texts = path.GlobalPath;
            enable = path.SelectSingleElement("GlobalPath").GetAttributeBool("Enable");
            if (enable)
                btn_GlobalPathOpen.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Checked;
            else
                btn_GlobalPathOpen.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;


            txt_InputAlertPath.Texts = path.InputAlertPath;
            enable = path.SelectSingleElement("InputAlertPath").GetAttributeBool("Enable");
            if (enable)
                btn_InputAlertPathOpen.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Checked;
            else
                btn_InputAlertPathOpen.CheckState = CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;


            chb_ReceiveSystem.CheckState = path.ReveiveSystemNotification == true ? CSS.IM.UI.Control.BasicCheckBox.CheckStates.Checked : CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;

            chb_ChatOpen.CheckState = path.ChatOpen == true ? CSS.IM.UI.Control.BasicCheckBox.CheckStates.Checked : CSS.IM.UI.Control.BasicCheckBox.CheckStates.Unchecked;


            #endregion

            panel_info.Visible = false;
            panel_basice.Visible = false;
            panel_sendfile.Visible = false;
            panel_fastreply.Visible = false;
            panel_sound.Visible = true;
            panel_hotkey.Visible = false;
        }

        #region 声音和提醒

        #region 声音

        private void btn_MsgPathPlay_Click(object sender, EventArgs e)
        {
            SoundPlayEx.MsgPlay(txt_MsgPath.Texts);
        }

        private void btn_SystemPathPlay_Click(object sender, EventArgs e)
        {
            SoundPlayEx.MsgPlay(txt_SystemPath.Texts);
        }

        private void btn_CallPathPlay_Click(object sender, EventArgs e)
        {
            SoundPlayEx.MsgPlay(txt_CallPath.Texts);
        }

        private void btn_FolderPathPlay_Click(object sender, EventArgs e)
        {
            SoundPlayEx.MsgPlay(txt_FolderPath.Texts);
        }

        private void btn_GlobalPathPlay_Click(object sender, EventArgs e)
        {
            SoundPlayEx.MsgPlay(txt_GlobalPath.Texts);
        }

        private void btn_InputAlertPathPlay_Click(object sender, EventArgs e)
        {
            SoundPlayEx.MsgPlay(txt_InputAlertPath.Texts);
        }

        private void btn_MsgPathBrow_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "声音文件(*.wav)|*.wav";
            //file.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg|*.gif|*.bmp";
            DialogResult reslut = file.ShowDialog();
            if (reslut == DialogResult.OK)
            {
                txt_MsgPath.Texts = file.FileName;
            }
        }

        private void btn_SystemPathBrow_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "声音文件(*.wav)|*.wav";
            //file.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg|*.gif|*.bmp";
            DialogResult reslut = file.ShowDialog();
            if (reslut == DialogResult.OK)
            {
                txt_SystemPath.Texts = file.FileName;
            }
        }

        private void btn_CallPathBrow_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "声音文件(*.wav)|*.wav";
            //file.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg|*.gif|*.bmp";
            DialogResult reslut = file.ShowDialog();
            if (reslut == DialogResult.OK)
            {
                txt_CallPath.Texts = file.FileName;
            }
        }

        private void btn_FolderPathBrow_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "声音文件(*.wav)|*.wav";
            //file.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg|*.gif|*.bmp";
            DialogResult reslut = file.ShowDialog();
            if (reslut == DialogResult.OK)
            {
                txt_FolderPath.Texts = file.FileName;
            }
        }

        private void btn_GlobalPathBrow_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "声音文件(*.wav)|*.wav";
            //file.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg|*.gif|*.bmp";
            DialogResult reslut = file.ShowDialog();
            if (reslut == DialogResult.OK)
            {
                txt_GlobalPath.Texts = file.FileName;
            }
        }

        private void btn_InputAlertPathBrow_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "声音文件(*.wav)|*.wav";
            //file.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg|*.gif|*.bmp";
            DialogResult reslut = file.ShowDialog();
            if (reslut == DialogResult.OK)
            {
                txt_InputAlertPath.Texts = file.FileName;
            }
        }

        private void btn_MsgPathOpen_Click(object sender, bool Checked)
        {
            path.SelectSingleElement("MsgPath").SetAttribute("Enable", Checked);
        }

        private void btn_SystemPathOpen_Click(object sender, bool Checked)
        {
            path.SelectSingleElement("SystemPath").SetAttribute("Enable", Checked);
        }

        private void btn_CallPathOpen_Click(object sender, bool Checked)
        {
            path.SelectSingleElement("CallPath").SetAttribute("Enable", Checked);
        }

        private void btn_FolderPathOpen_Click(object sender, bool Checked)
        {
            path.SelectSingleElement("FolderPath").SetAttribute("Enable", Checked);
        }

        private void btn_GlobalPathOpen_Click(object sender, bool Checked)
        {
            path.SelectSingleElement("GlobalPath").SetAttribute("Enable", Checked);
        }

        private void btn_InputAlertPathOpen_Click(object sender, bool Checked)
        {
            path.SelectSingleElement("InputAlertPath").SetAttribute("Enable", Checked);
        }

       
        #endregion

        #region 提醒

        private void chb_ReceiveSystem_CheckedChanged(object sender, bool Checked)
        {
            path.ReveiveSystemNotification = Checked;
        }

        private void chb_ChatOpen_CheckedChanged(object sender, bool Checked)
        {
            path.ChatOpen = Checked;
        }

        #endregion


        /// <summary>
        /// 保存声音的应用
        /// </summary>
        private void SaveSound()
        {
            path.MsgPath = txt_MsgPath.Texts;
            path.SystemPath = txt_SystemPath.Texts;
            path.CallPath = txt_CallPath.Texts;
            path.FolderPath = txt_FolderPath.Texts;
            path.GlobalPath = txt_GlobalPath.Texts;
            path.InputAlertPath = txt_InputAlertPath.Texts;

            config.Paths = path;
            config.Font = doc_setting.RootElement.SelectSingleElement(typeof(Settings.SFont)) as Settings.SFont;
            config.Color = doc_setting.RootElement.SelectSingleElement(typeof(Settings.SColor)) as Settings.SColor;

            doc_setting.Clear();
            doc_setting.ChildNodes.Add(config);
            doc_setting.Save(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));


            CSS.IM.UI.Util.Path.MsgPath = path.MsgPath;
            CSS.IM.UI.Util.Path.MsgSwitch = path.SelectSingleElement("MsgPath").GetAttributeBool("Enable");
            CSS.IM.UI.Util.Path.SystemPath = path.SystemPath;
            CSS.IM.UI.Util.Path.SystemSwitch = path.SelectSingleElement("SystemPath").GetAttributeBool("Enable");
            CSS.IM.UI.Util.Path.CallPath = path.CallPath;
            CSS.IM.UI.Util.Path.CallSwitch = path.SelectSingleElement("CallPath").GetAttributeBool("Enable");
            CSS.IM.UI.Util.Path.FolderPath = path.FolderPath;
            CSS.IM.UI.Util.Path.FolderSwitch = path.SelectSingleElement("FolderPath").GetAttributeBool("Enable");
            CSS.IM.UI.Util.Path.GlobalPath = path.GlobalPath;
            CSS.IM.UI.Util.Path.GlobalSwitch = path.SelectSingleElement("GlobalPath").GetAttributeBool("Enable");
            CSS.IM.UI.Util.Path.InputAlertPath = path.InputAlertPath;
            CSS.IM.UI.Util.Path.InputAlertSwitch = path.SelectSingleElement("InputAlertPath").GetAttributeBool("Enable");

            CSS.IM.UI.Util.Path.ReveiveSystemNotification = path.ReveiveSystemNotification;
            CSS.IM.UI.Util.Path.ChatOpen = path.ChatOpen;
        }

        #endregion

        /// <summary>
        /// 热键设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_hotkey_Click(object sender, EventArgs e)
        {
            #region 加载数据
            doc_setting = new Document();
            config = new Settings.Settings();

            doc_setting.LoadFile(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));
            path = doc_setting.RootElement.SelectSingleElement(typeof(Settings.Paths), false) as Settings.Paths;

            if (path.GetOutMsgKeyTYpe == "W+ Control+ Alt")//默认获取消息快捷按键
            {
                rdb_default_msg.Checked = true;
                txt_custom_msg.Texts = "W+ Control+ Alt";
                txt_custom_msg.Enabled = false;
                txt_custom_msg.ReadOn = true;
            }
            else
            {
                rdb_costom_msg.Checked = true;
                txt_custom_msg.Enabled = true;
                txt_custom_msg.ReadOn = true;
                txt_custom_msg.Texts = path.GetOutMsgKeyTYpe;
            }

            if(path.ScreenKeyTYpe == "S+ Control+ Alt")//默认截图快捷按键
            {
                rdb_default_image.Checked = true;
                txt_custom_image.Texts = "S+ Control+ Alt";
                txt_custom_image.Enabled = false;
                txt_custom_image.ReadOn = true;
            }
            else
            {
                rdb_custom_image.Checked = true;
                txt_custom_image.Enabled = true;
                txt_custom_image.ReadOn = true;
                txt_custom_image.Texts = path.ScreenKeyTYpe;
            }

            if (path.SendKeyType==1)
            {
                rdb_enter.Checked = true;
                rdb_ctrl_enter.Checked = false;
            }
            else
            {
                rdb_enter.Checked = false;
                rdb_ctrl_enter.Checked = true;
            }

            #endregion

            panel_info.Visible = false;
            panel_basice.Visible = false;
            panel_sendfile.Visible = false;
            panel_fastreply.Visible = false;
            panel_sound.Visible = false;
            panel_hotkey.Visible = true;

            txt_custom_msg.Focus();
            txt_custom_msg.IsFocused = true;
        }

        #region 热键设置

        #region 获取消息
        private void rdb_default_msg_CheckedChanged(object sender, bool Checked)
        {
            if (Checked)
            {
                rdb_costom_msg.Checked = false;
                txt_custom_msg.Enabled = false;
                txt_custom_msg.ReadOn = false;
                txt_custom_msg.Texts = "W+ Control+ Alt";
            }
            
        }

        private void rdb_costom_msg_CheckedChanged(object sender, bool Checked)
        {
            if (Checked)
            {
                rdb_default_msg.Checked = false;
                txt_custom_msg.Enabled = true;
                txt_custom_msg.ReadOn = true;
                txt_custom_msg.Focus();
            }

        }
        #endregion

        #region 截图
        private void rdb_default_image_CheckedChanged(object sender, bool Checked)
        {
            if (Checked)
            {
                rdb_custom_image.Checked = false;
                txt_custom_image.Enabled = false;
                txt_custom_image.ReadOn = false;
                txt_custom_image.Texts = "S+ Control+ Alt";
            }
        }

        private void rdb_custom_image_CheckedChanged(object sender, bool Checked)
        {
            if (Checked)
            {
                rdb_default_image.Checked = false;
                txt_custom_image.Enabled = true;
                txt_custom_image.ReadOn = true;
                txt_custom_image.Focus();
            }
        }
        #endregion

        #region 发送消息快捷按键
        private void rdb_enter_CheckedChanged(object sender, bool Checked)
        {
            if (rdb_enter.Checked)
            {
                rdb_ctrl_enter.Checked = false;
            }
        }

        private void rdb_ctrl_enter_CheckedChanged(object sender, bool Checked)
        {
            if (rdb_ctrl_enter.Checked)
            {
                rdb_enter.Checked = false;
            }
        }
        #endregion

        private void SaveHotkey()
        {
            path.GetOutMsgKeyTYpe = txt_custom_msg.Texts;
            path.ScreenKeyTYpe = txt_custom_image.Texts;
            path.SendKeyType = rdb_enter.Checked == true ? 1 : 2;



            CSS.IM.UI.Util.Path.SendKeyType = path.SendKeyType;
            CSS.IM.UI.Util.Path.GetOutMsgKeyTYpe = path.GetOutMsgKeyTYpe;
            CSS.IM.UI.Util.Path.ScreenKeyTYpe = path.ScreenKeyTYpe;

            config.Paths = path;
            config.Font = doc_setting.RootElement.SelectSingleElement(typeof(Settings.SFont)) as Settings.SFont;
            config.Color = doc_setting.RootElement.SelectSingleElement(typeof(Settings.SColor)) as Settings.SColor;

            doc_setting.Clear();
            doc_setting.ChildNodes.Add(config);
            doc_setting.Save(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, Program.UserName));

            HotKey.UnregisterHotKey(Util.MainHandle, 102);//取消注册的热键
            HotKey.UnregisterHotKey(Util.MainHandle, 101);//取消注册的热键

            #region 注册快捷按键
            string[] keys = CSS.IM.UI.Util.Path.GetOutMsgKeyTYpe.Split('+');
            if (keys.Length == 2)
            {
                HotKey.RegisterHotKey(Util.MainHandle, 101, Util.GetFunctionKeyValue(keys[1]), Util.GetKeyValue(keys[0].Trim()));
            }
            else if (keys.Length == 3)
            {
                HotKey.RegisterHotKey(Util.MainHandle, 101, Util.GetFunctionKeyValue(keys[1]) | Util.GetFunctionKeyValue(keys[2]), Util.GetKeyValue(keys[0]));
            }

            keys = null;
            keys = CSS.IM.UI.Util.Path.ScreenKeyTYpe.Split('+');
            if (keys.Length == 2)
            {
                HotKey.RegisterHotKey(Util.MainHandle, 102, Util.GetFunctionKeyValue(keys[1]), Util.GetKeyValue(keys[0].Trim()));
            }
            else if (keys.Length == 3)
            {
                HotKey.RegisterHotKey(Util.MainHandle, 102, Util.GetFunctionKeyValue(keys[1]) | Util.GetFunctionKeyValue(keys[2]), Util.GetKeyValue(keys[0]));
            }
            #endregion;

        }

        #endregion


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (rdb_costom_msg.Checked)
            {


                if (txt_custom_msg.IsFocused)
                {
                    txt_custom_msg.Texts = keyData.ToString().Replace(",", "+");  
                }
            }


            if (rdb_custom_image.Checked)
            {
                if (txt_custom_image.IsFocused)
                {
                    txt_custom_image.Texts = keyData.ToString().Replace(",", "+");
                }
            }

            System.Diagnostics.Debug.WriteLine(keyData.ToString());

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
