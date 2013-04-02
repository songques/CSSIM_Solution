using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace WForm1
{
    public partial class Form1 : Form
    {
        string filename;
        string remotingFolder;

        //static string remotingFolder = System.Configuration.ConfigurationSettings.AppSettings["remotingFolder"];  //远程ftp文件目录
        //static string localFolder = System.Configuration.ConfigurationSettings.AppSettings["localFolder"];  //要下载到的本地目录
        //static string ftpServer = System.Configuration.ConfigurationSettings.AppSettings["ftpServer"];  //ftp服务器
        //static string user = System.Configuration.ConfigurationSettings.AppSettings["user"];  //用户名
        //static string pwd = System.Configuration.ConfigurationSettings.AppSettings["pwd"];  //密码
        //static string port = System.Configuration.ConfigurationSettings.AppSettings["port"];  //端口

        public Form1()
        {
            InitializeComponent();
        }


        private void btn_file_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofile=new OpenFileDialog())
            {
                DialogResult result=ofile.ShowDialog();

                if (result==DialogResult.OK)
                {
                    filename = ofile.FileName;
                    txt_file.Text = filename;
                }
            };
        }

        FTPClient client1;
        private void btn_upload_Click(object sender, EventArgs e)
        {
            client1 = new FTPClient(txt_address.Text, "/", txt_user.Text, txt_pswd.Text, 21);
            new Thread(new ThreadStart(UploadFile)).Start();

            
        }

        public void UploadFile()
        {

            try
            {
                client1.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败:" + ex.Message);
                return;
            }
            
            aa:
            string isRoot = "";
            string[] dir = client1.Dir("/");
            foreach (string item in dir)
            {
                string[] items = item.Split(' ');
                string nitem = items[items.Length - 1].Replace('\r', ' ').Trim();
                if (nitem == txt_user.Text.Trim())
                {
                    isRoot = nitem;
                    break;
                }
            }

            if (isRoot == "")
            {
                client1.MkDir(txt_user.Text.Trim());
                goto aa;
            }
            client1.ChDir("/" + txt_user.Text.Trim() + "/");
            
            client1.PushProgressEvent += new FTPClient.PushProgressDelegate(client_PushProgressEvent);
            client1.PushFileSucceedEvent += new FTPClient.PushFileSucceedDelegate(client_PushFileSucceedEvent);
            client1.PushFileErrorEvent += new FTPClient.PushFileErrorDelegate(client_PushFileErrorEvent);
            client1.Connect();
            try
            {
                client1.Put(filename, true);
            }
            catch (Exception error)
            {
                MessageBox.Show("上传文件错误:" + error.Message);
            }
            
            //client.Put(@"D:\Program Files\FlashFXP\", "*");
        }

        void client_PushFileErrorEvent(object sender, string args)
        {
            MessageBox.Show("上传文件错误:" + sender.ToString());
        }

        void client_PushFileSucceedEvent(object sender, string args)
        {
            MessageBox.Show("上传完成:"+args.ToString());
        }

        void client_PushProgressEvent(object sender, int args)
        {
            if (InvokeRequired)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new WForm1.FTPClient.PushProgressDelegate(client_PushProgressEvent), new object[] { sender, args });
                }
            }
            try
            {
                label5.Text = args.ToString();
            }
            catch (Exception)
            {

            }
            
        }

        FTPClient client2;
        private void button1_Click(object sender, EventArgs e)
        {
            client2 = new FTPClient(txt_address.Text, "/", txt_user.Text, txt_pswd.Text, 21);
            new Thread(new ThreadStart(GetFile)).Start();
        }

        void client_GetFileProgressEvent(object sender, int args)
        {
            if (InvokeRequired)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new WForm1.FTPClient.GetFileProgressDelegate(client_PushProgressEvent), new object[] { sender, args });
                }
            }
            try
            {
                label5.Text = args.ToString();
            }
            catch (Exception)
            {

            }
        }

        public void GetFile()
        {

            client2.GetFileProgressEvent += new FTPClient.GetFileProgressDelegate(client_GetFileProgressEvent);
            client2.GetFileSucceedEvent += new FTPClient.GetFileSucceedDelegate(client_GetFileSucceedEvent);
            client2.GetFileErrorEvent += new FTPClient.GetFileErrorDelegate(client2_GetFileErrorEvent);
            try
            {
                client2.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败:" + ex.Message);
                return;
            }
            client2.ChDir("/" + txt_user.Text.Trim() + "/");
            string[] dir = client2.Dir(null);
            //Debug.WriteLine(dir[0]);
            client2.GetFile("5833a8a86ea24c289eb5b16d5494d6e7_QQ2012正式版.exe", @"D:\");
        }

        void client2_GetFileErrorEvent(object sender, string args)
        {
            MessageBox.Show("下载错误:"+sender);
        }

        void client_GetFileSucceedEvent(object sender, string args)
        {
            MessageBox.Show("下载完成:" + sender.ToString()+@"\"+args);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //client1.DisConnect(true);
            client2.DisConnect(true);
        }
    }
}
