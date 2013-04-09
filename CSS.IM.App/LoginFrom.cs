using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.XMPP;
using CSS.IM.App.Settings;
using CSS.IM.UI.Util;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace CSS.IM.App
{
    public partial class LoginFrom : BasicForm
    {
        /*
         *X mppCon_OnAuthError 中注视 LogOut(false, false);
         * 打开
         * //Application.Exit();
         */
        //USB Key验证功能

        private MainForm _MainForm;
        private string HistoryFilename="";//最后一次登录的用户名;

        public delegate void LoginDelegate(User user);//登录事件代理
        public event LoginDelegate Login_Event;//登录事件

        public bool ISAutoLogin { set; get; }//是否从设置中读取自动登录功能

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qqMainForm"></param>
        /// <param name="isAuto">是否由错误引起的登录 true 是</param>
        public LoginFrom(MainForm qqMainForm, bool isAuto)
        {

            ISAutoLogin = true;
            _MainForm = qqMainForm;
            InitializeComponent();
            txt_name.Enabled = false;
            txt_pswd.Enabled = false;
            txt_name.Texts = Program.UserName;
            chb_autu.Enabled = false;
            chb_save.Enabled = false;
            btn_login.Enabled = false;
            btn_setings.Enabled = false;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {

            UpdateSoftware();

            if (txt_name.Texts.Trim().Length == 0)
            {
                MsgBox.Show(this, "CSS&IM", "用户名不能为空！", MessageBoxButtons.OK);
                txt_name.Focus();
                return;
            }

            if (txt_pswd.Texts.Trim().Length == 0)
            {
                MsgBox.Show(this, "CSS&IM", "密码不能为空！", MessageBoxButtons.OK);
                txt_pswd.Focus();
                return;
            }


            try
            {
                //Program.IsLogin = false;
                timer1.Enabled = false;
                User user = new User();
                user.UserName = txt_name.Texts.Trim();
                user.PassWord = txt_pswd.Texts.Trim();
                user.Save = chb_save.Checked;
                user.Auto = chb_autu.Checked;
                //SaveSettings();
                if (Login_Event != null)
                    Login_Event(user);
                this.Close();
            }
            catch (Exception)
            {

            }
        }

        private void ButtonClose_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            timer_keyLogin.Enabled = false;
            Application.Exit();
        }

        private void LoginFrom_Load(object sender, EventArgs e)
        {

            /*key验证杜宾用*/
            //int iCertNum = 0;
            //GetCert.GetCertNum(out iCertNum);

            //if (iCertNum == 0)
            //{
            //    MsgBox.Show(this, "CSS&IM", "请插入启动钥匙后在启动程序。", MessageBoxButtons.OK);
            //    Application.Exit();
            //    return;
            //}
            /*key验证杜宾用*/

            txt_name.Focus();
            if (System.IO.File.Exists(CSS.IM.UI.Util.Path.HistoryFilename))
            {
                Document load_doc = new Document();
                load_doc.LoadFile(CSS.IM.UI.Util.Path.HistoryFilename);
                Settings.HistoryLogin HLlgin = load_doc.RootElement as Settings.HistoryLogin;
                HistoryFilename = HLlgin.LoginName;
            }

            chb_autu.Enabled = true;
            chb_save.Enabled = true;
            txt_name.Enabled = true;
            txt_pswd.Enabled = true;


            if (HistoryFilename.Trim().Length > 0)
            {
                LoadSettings();
            }

            btn_login.Enabled = true;
            btn_setings.Enabled = true;

            if (chb_autu.Checked)
            {
                timer_keyLogin.Enabled = true;
            }
        }

        private void llab_regedit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void LoadSettings()
        {
            /*key验证杜宾用*/
            //int iCertNum = 0;
            //GetCert.GetCertNum(out iCertNum);

            //if (iCertNum == 0)
            //{
            //    MsgBox.Show(this, "CSS&IM", "请插入启动钥匙后在启动程序。", MessageBoxButtons.OK);
            //    Application.Exit();
            //    return;
            //}
            /*key验证杜宾用*/

            if (System.IO.File.Exists(string.Format(CSS.IM.UI.Util.Path.SettingsFilename, HistoryFilename)))
            {
                Document login_doc = new Document();
                login_doc.LoadFile(string.Format(CSS.IM.UI.Util.Path.SettingsFilename, HistoryFilename));
                Settings.Login login = login_doc.RootElement.SelectSingleElement(typeof(Settings.Login)) as Settings.Login;
                Settings.ServerInfo serverInfo = login_doc.RootElement.SelectSingleElement(typeof(Settings.ServerInfo)) as Settings.ServerInfo;
                if (login.Save)
                {
                    txt_name.Texts = login.Jid == null ? "" : login.Jid.ToString();
                    txt_pswd.Texts = login.Password == null ? "" : login.Password;
                    if (ISAutoLogin)
                    {
                        chb_autu.Checked = login.Auto;
                    }
                    else
                    {
                        chb_autu.Checked = false;
                    }

                    chb_save.Checked = login.Save;
                }
                Program.ServerIP = serverInfo.ServerIP;
                Program.Port = serverInfo.ServerPort;
                CSS.IM.UI.Util.Path.Initial = login.InitIal;
            }

            if (System.IO.File.Exists(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, HistoryFilename)))
            {

                Document local_doc = new Document();
                local_doc.LoadFile(string.Format(CSS.IM.UI.Util.Path.ConfigFilename, HistoryFilename));

                Settings.Paths local_path = local_doc.RootElement.SelectSingleElement(typeof(Settings.Paths)) as Settings.Paths;
                CSS.IM.UI.Util.Path.MsgPath = local_path.MsgPath;
                CSS.IM.UI.Util.Path.MsgSwitch = local_path.SelectSingleElement("MsgPath").GetAttributeBool("Enable");
                CSS.IM.UI.Util.Path.SystemPath = local_path.SystemPath;
                CSS.IM.UI.Util.Path.SystemSwitch = local_path.SelectSingleElement("SystemPath").GetAttributeBool("Enable");
                CSS.IM.UI.Util.Path.CallPath = local_path.CallPath;
                CSS.IM.UI.Util.Path.CallSwitch = local_path.SelectSingleElement("CallPath").GetAttributeBool("Enable");
                CSS.IM.UI.Util.Path.FolderPath = local_path.FolderPath;
                CSS.IM.UI.Util.Path.FolderSwitch = local_path.SelectSingleElement("FolderPath").GetAttributeBool("Enable");
                CSS.IM.UI.Util.Path.GlobalPath = local_path.GlobalPath;
                CSS.IM.UI.Util.Path.GlobalSwitch = local_path.SelectSingleElement("GlobalPath").GetAttributeBool("Enable");
                CSS.IM.UI.Util.Path.InputAlertPath = local_path.InputAlertPath;
                CSS.IM.UI.Util.Path.InputAlertSwitch = local_path.SelectSingleElement("InputAlertPath").GetAttributeBool("Enable");
                CSS.IM.UI.Util.Path.ReveiveSystemNotification = local_path.ReveiveSystemNotification;
                CSS.IM.UI.Util.Path.ChatOpen = local_path.ChatOpen;
                CSS.IM.UI.Util.Path.SendKeyType = local_path.SendKeyType;
                CSS.IM.UI.Util.Path.GetOutMsgKeyTYpe = local_path.GetOutMsgKeyTYpe;
                CSS.IM.UI.Util.Path.ScreenKeyTYpe = local_path.ScreenKeyTYpe;
                CSS.IM.UI.Util.Path.FriendContainerType = local_path.FriendContainerType;

                CSS.IM.UI.Util.Path.DefaultURL = local_path.DefaultURL;
                CSS.IM.UI.Util.Path.EmailURL = local_path.EmailURL;

                SFont font1 = local_doc.RootElement.SelectSingleElement(typeof(Settings.SFont)) as Settings.SFont;
                System.Drawing.FontStyle fontStyle = new System.Drawing.FontStyle();
                System.Drawing.Font ft = null;
                #region 获取字体
                if (font1 != null)
                {
                    try
                    {
                        if (font1.Bold)
                        {
                            fontStyle = System.Drawing.FontStyle.Bold;
                        }
                        if (font1.Italic)
                        {
                            fontStyle = fontStyle | System.Drawing.FontStyle.Italic;
                        }
                        if (font1.Strikeout)
                        {
                            fontStyle = fontStyle | System.Drawing.FontStyle.Strikeout;
                        }
                        if (font1.Underline)
                        {
                            fontStyle = fontStyle | System.Drawing.FontStyle.Underline;
                        }

                        ft = new System.Drawing.Font(font1.Name, font1.Size, fontStyle);
                    }
                    catch (Exception)
                    {
                        ft = txt_name.Font;
                    }
                }
                else
                {
                    ft = txt_name.Font;
                }

                #endregion
                CSS.IM.UI.Util.Path.SFong = ft;

                SColor color1 = local_doc.RootElement.SelectSingleElement(typeof(Settings.SColor)) as Settings.SColor;
                #region 获取颜色
                Color cl;
                if (color1 != null)
                {
                    try
                    {
                        byte[] cby = new byte[4];
                        cby[0] = color1.CA;
                        cby[1] = color1.CR;
                        cby[2] = color1.CG;
                        cby[3] = color1.CB;
                        cl = Color.FromArgb(BitConverter.ToInt32(cby, 0));

                    }
                    catch
                    {
                        cl = txt_name.ForeColor;
                    }
                }
                else
                {
                    cl = txt_name.ForeColor;
                }

                #endregion
                CSS.IM.UI.Util.Path.SColor = cl;
            }

            try
            {
                Util.RunWhenStart(CSS.IM.UI.Util.Path.Initial, "CSS&IM", Application.StartupPath + @"\CSSIM.exe");
            }
            catch (Exception)
            {
                
            }
            


            /*key验证杜宾用*/
            //try
            //{
            //    StringBuilder names = new StringBuilder("");
            //    GetCert.GetCertName(0, 100, names);
            //    txt_name.ReadOn = true;
            //    txt_pswd.ReadOn = true;

            //    txt_name.Texts = names.ToString();
            //    txt_pswd.Texts = "1";
            //    chb_autu.Checked = true;
            //    chb_save.Checked = true;
            //    timer_keyLogin.Enabled = true;
            //}
            //catch (Exception)
            //{

            //}
            /*key验证杜宾用*/
        }

        private void SaveSettings()
        {
        //    //LoadSettings();//重新加载服务器地址
        //    //login.Jid = new Jid(txt_name.Texts.Trim());
        //    //login.Password = txt_pswd.Texts.Trim();
        //    //login.Save = chb_save.Checked;
        //    //login.Auto = chb_autu.Checked;

        //    //serverInfo.ServerIP = Program.ServerIP;
        //    //serverInfo.ServerPort = Program.Port;
        //    //login_doc.Save(HistoryFilename + CSS.IM.UI.Util.Path.SettingsFilename);
        }

        int count = 30;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (count == 0)
            {
                timer1.Enabled = false;
                btn_login_Click(null, null);

            }
            else
            {
                count--;
                lab_isAutoLogin.Text = count + "秒后开始重新登录";
            }
        }

        private void chb_autu_CheckedChanged(object sender, bool Checked)
        {
            if (Checked)
            {
                chb_save.Checked = true;
            }
        }

        private void chb_save_CheckedChanged(object sender, bool Checked)
        {
            if (!Checked)
            {
                chb_autu.Checked = false;
            }
        }

        private void timer_keyLogin_Tick(object sender, EventArgs e)
        {
            timer_keyLogin.Enabled = false;
            btn_login_Click(null, null);
        }

        SetingServerInfo _SetingServerInfo = null;
        private void btn_setings_Click(object sender, EventArgs e)
        {
            QQcm_menu.Show(this, btn_setings.Location);
        }

        private void QQtsm_ServerAddress_Click(object sender, EventArgs e)
        {
            if (_SetingServerInfo == null)
            {
                _SetingServerInfo = new SetingServerInfo();
            }
            if (_SetingServerInfo.IsDisposed)
            {
                _SetingServerInfo = new SetingServerInfo();
            }

            _SetingServerInfo.ServerIP = Program.ServerIP;
            _SetingServerInfo.Port = Program.Port;
            _SetingServerInfo.ShowDialog();

            Program.ServerIP = _SetingServerInfo.ServerIP;
            Program.Port = _SetingServerInfo.Port;
        }

        private void QQtsm_region_Click(object sender, EventArgs e)
        {
            try
            {
                RegisterForm _RegisterForm = new RegisterForm(Program.ServerIP, Program.Port);
                _RegisterForm.Show();
            }
            catch (Exception)
            {

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Return)
            {
                btn_login_Click(null, null);
            }
            return base.ProcessCmdKey(ref   msg, keyData);
        }

        /// <summary>
        /// 升级检测
        /// </summary>
        private void UpdateSoftware()
        {

            #region 升级检测
            string versionUrl = "error";
            string url = @"http://" + Program.ServerIP + "/update.aspx?version=" + Program.Vsion;
            //string url = @"http://" + Program.ServerIP + "/oa/im/update.jsp?version=" + Program.Vsion;
            System.Net.HttpWebRequest request = null;
            System.IO.Stream stream = null;
            try
            {
                //MessageBox.Show(url);
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            }
            catch (Exception ex)
            {
                MsgBox.Show(this, "CSS&IM", "创建升级检测功能失败," + ex.Message + "请检查系统服务器！", MessageBoxButtons.OK);
                Application.DoEvents();
                Application.Exit();
                return;
            }

            try
            {
                stream = request.GetResponse().GetResponseStream();
            }
            catch (Exception ex)
            {
                chb_autu.Enabled = true;
                chb_save.Enabled = true;
                txt_name.Enabled = true;
                txt_pswd.Enabled = true;
                btn_login.Enabled = true;
                btn_setings.Enabled = true;
                MsgBox.Show(this, "CSS&IM", "获取升级信息失败," + ex.Message + "请检查系统服务器！", MessageBoxButtons.OK);
                return;
            }

            byte[] arrybyte = new byte[1024];
            int MemoryStreamLength = 0;
            MemoryStream memory = new MemoryStream();
            int readLength = 0;

            try
            {
                readLength = stream.Read(arrybyte, 0, arrybyte.Length);
                memory.Write(arrybyte, 0, readLength);
                MemoryStreamLength = readLength;
                memory.Seek(MemoryStreamLength, SeekOrigin.Begin);


                while (readLength == 1024)
                {
                    readLength = stream.Read(arrybyte, 0, arrybyte.Length);
                    MemoryStreamLength += readLength;
                    memory.Seek(MemoryStreamLength, SeekOrigin.Begin);
                }

                versionUrl = Encoding.UTF8.GetString(memory.ToArray());

            }
            catch (Exception ex)
            {

                MsgBox.Show(this, "CSS&IM", "升级数据处理失败," + ex.Message, MessageBoxButtons.OK);
                Application.DoEvents();
                Application.Exit();
                return;
            }
            #endregion
            //string versionUrl = "new";
            if (versionUrl == "new")
            {
                chb_autu.Enabled = true;
                chb_save.Enabled = true;
                txt_name.Enabled = true;
                txt_pswd.Enabled = true;
                btn_login.Enabled = true;
                btn_setings.Enabled = true;
                if (chb_autu.Checked)
                {
                    timer_keyLogin.Enabled = true;
                }
            }
            else if (versionUrl == "error")
            {
                MsgBox.Show(this, "CSS&IM", "升级检测升级错误，请到官网下载请最程序进行安装！", MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    Application.DoEvents();
                    System.Diagnostics.Process.Start(CSS.IM.UI.Util.Path.AppPath + "\\IMUpdate.exe", versionUrl);
                    Application.Exit();
                }
                catch (Exception)
                {
                    MsgBox.Show(this, "CSS&IM", "启动升级程序失败，请到官网下载请最程序进行安装！", MessageBoxButtons.OK);
                }
            }
        }

        private void LoginFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ButtonClose_MouseClick(null, null);
        }
    }

    public class User
    {
        public String UserName { set; get; }
        public String PassWord { set; get; }
        public bool Auto { set; get; }
        public bool Save { set; get; }
    }
}
