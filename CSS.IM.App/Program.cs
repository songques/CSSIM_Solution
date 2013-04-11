using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using CSS.IM.Library.AV;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using CSS.IM.App.Settings;
using Microsoft.Win32;

namespace CSS.IM.App
{
    static class Program
    {
        public static string ServerIP = Util.ServerAddress;
        public static string Port = Util.OpenFirePort.ToString();
        public static IPAddress LocalHostIP;
        public static string UserName = "";//保存登录的用户名
        //public static bool IsLogin = false;//是否是点登录按键登录，出现的socket连接关闭的错误
        public static string Vsion = "2.2.1.4";//当前版本号

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);


        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;

        }
        const int WM_COPYDATA = 0x004A;

        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //Loop   through   the   running   processes   in   with   the   same   name
            foreach (Process process in processes)
            {
                //Ignore   the   current   process
                if (process.Id != current.Id)
                {
                    //Make   sure   that   the   process   is   running   from   the   exe   file.
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") ==
                        current.MainModule.FileName)
                    {
                        //Return   the   other   process   instance.
                        return process;
                    }
                }
            }

            //No   other   instance   was   found,   return   null.
            return null;
        }

        public static void HandleRunningInstance(Process instance)
        {
            //Make   sure   the   window   is   not   minimized   or   maximized
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);

            //Set   the   real   intance   to   foreground   window
            SetForegroundWindow(instance.MainWindowHandle);
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Application.StartupPath + @"\CSSIM.exe"
            //RegistryKey HCROOT = Registry.ClassesRoot;
            //RegistryKey HCR_CSSIM=HCROOT.CreateSubKey("CSSIM");
            //HCROOT.Close();



            /**
            * 当前用户是管理员的时候，直接启动应用程序
            * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
            */
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            //创建Windows用户主题
            Application.EnableVisualStyles();

            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员


            Dictionary<string, string> HrefMap = new Dictionary<string, string>();

            string[] arg_array = { };
            if (args.Length > 0)
            {
                args = args[0].Split(':');
                arg_array = args[1].Split('&');
            }


            if (arg_array != null)
            {

                foreach (string item in arg_array)
                {
                    if (item != null)
                    {
                        string[] items = item.Split('=');
                        if (items.Length == 2)
                        {
                            HrefMap.Add(items[0].ToString(), items[1].ToString());
                        }

                    }
                }
                /*if (HrefMap.ContainsKey("UD"))
                {
                    MessageBox.Show(HrefMap["UD"].ToString());
                }
                if (HrefMap.ContainsKey("PD"))
                {
                    MessageBox.Show(HrefMap["PD"].ToString());
                }
                if (HrefMap.ContainsKey("SD"))
                {
                    MessageBox.Show(HrefMap["SD"].ToString());
                }*/
            }



            Cursor.Current = Cursors.WaitCursor;
            Process instance = RunningInstance();
            if (instance == null)
            {
                //System.Net.IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;
                //if (addressList.Length > 1)
                //{
                //    LocalHostIP = addressList[1];
                //    //Console.WriteLine(addressList[1].ToString());
                //}
                //else
                //{
                //    LocalHostIP = addressList[0];
                //    //Console.WriteLine(addressList[1].ToString());
                //}

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                try
                {
                    Application.Run(new MainForm());
                }
                catch (Exception)
                {
                    MessageBox.Show("对不起，调用系统Win32API错误，请重新启动应用程序！");
                    Application.DoEvents();
                    Application.Exit();
                }

                /*//判断当前登录用户是否为管理员
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    //如果是管理员，则直接运行

                    Application.EnableVisualStyles();
                    try
                    {
                        Application.Run(new MainForm());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("对不起，调用系统Win32API错误，请重新启动应用程序！");
                        Application.DoEvents();
                        Application.Exit();
                    }
                }
                else
                {
                    //创建启动对象
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    //设置运行文件
                    startInfo.FileName = System.Windows.Forms.Application.ExecutablePath;
                    //设置启动参数
                    startInfo.Arguments = String.Join(" ", args);
                    //设置启动动作,确保以管理员身份运行
                    startInfo.Verb = "runas";
                    //如果不是管理员，则启动UAC
                    System.Diagnostics.Process.Start(startInfo);
                    //退出
                    System.Windows.Forms.Application.Exit();
                }*/
            }
            else
            {
                HandleRunningInstance(instance);
                string sendText = HrefMap["SD"].ToString();
                int WINDOW_HANDLER = FindWindow(null, @"CSS&IM");
                byte[] sarr = System.Text.Encoding.Default.GetBytes(sendText);
                int len = sarr.Length;
                COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)100;
                cds.lpData = sendText;
                cds.cbData = len + 1;
                SendMessage(WINDOW_HANDLER, WM_COPYDATA, 0, ref cds);
                //Util.HrefOpenFriendEventMethod(HrefMap["SD"].ToString());
                //MessageBox.Show("abc");
            }


        }
    }
}
