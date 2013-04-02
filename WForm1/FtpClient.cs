using System;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace WForm1
{
    /// <summary>
    /// FTP Client
    /// </summary>
    public class FTPClient
    {

        #region 事件
        /// <summary>
        /// 上传文件中 返回当前的进度值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public delegate void PushProgressDelegate(object sender, int args);
        public event PushProgressDelegate PushProgressEvent;


        /// <summary>
        /// 上传单个文件完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public delegate void PushFileSucceedDelegate(object sender, string args);
        public event PushFileSucceedDelegate PushFileSucceedEvent;


        /// <summary>
        /// 一批文件上传完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public delegate void PushFilesSucceedDelegate(object sender, string args);
        public event PushFilesSucceedDelegate PushFilesSucceedEvent;


        /// <summary>
        /// 文件传输中出现异常错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public delegate void PushFileErrorDelegate(object sender, string args);
        public event PushFileErrorDelegate PushFileErrorEvent;


        /// <summary>
        /// 下载一个文件的进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public delegate void GetFileProgressDelegate(object sender, int args);
        public event GetFileProgressDelegate GetFileProgressEvent;

        /// <summary>
        /// 下载单个文件完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public delegate void GetFileSucceedDelegate(object sender, string args);
        public event GetFileSucceedDelegate GetFileSucceedEvent;


        /// <summary>
        /// 文件传输中出现异常错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public delegate void GetFileErrorDelegate(object sender, string args);
        public event GetFileErrorDelegate GetFileErrorEvent;

        #endregion

        #region 内部变量

        public bool Cancel { set; get; }

        /// <summary>
        /// 服务器返回的应答信息(包含应答码)
        /// </summary>
        private string strMsg;
        /// <summary>
        /// 服务器返回的应答信息(包含应答码)
        /// </summary>
        private string strReply;
        /// <summary>
        /// 服务器返回的应答码
        /// </summary>
        private int iReplyCode;
        /// <summary>
        /// 进行控制连接的socket
        /// </summary>
        private Socket socketControl;
        /// <summary>
        /// 传输模式
        /// </summary>
        private TransferType trType;
        /// <summary>
        /// 接收和发送数据的缓冲区
        /// </summary>
        private static int BLOCK_SIZE = 1048576 * 3;
        //private static int BLOCK_SIZE = 1024;
        Byte[] buffer = new Byte[BLOCK_SIZE];
        /// <summary>
        /// 编码方式(为防止出现中文乱码采用 GB2312编码方式)
        /// </summary>
        Encoding GB2312 = Encoding.GetEncoding("gb2312");


        /// <summary>
        /// 用于处理socket连接超时问题
        /// </summary>
        //private Timer TimeoutObject = null;
        
        #endregion

        #region 内部函数
        /// <summary>
        /// 将一行应答字符串记录在strReply和strMsg
        /// 应答码记录在iReplyCode
        /// </summary>
        private void ReadReply()
        {
            try
            {
                strMsg = "";
                strReply = ReadLine();
                iReplyCode = Int32.Parse(strReply.Substring(0, 3));
            }
            catch (Exception error)
            {
                throw error;
            }
            
        }

        /// <summary>
        /// 建立进行数据连接的socket
        /// </summary>
        /// <returns>数据连接socket</returns>
        private Socket CreateDataSocket()
        {
            SendCommand("PASV");
            if (iReplyCode != 227)
            {
                throw new IOException(strReply.Substring(4));
            }
            int index1 = strReply.IndexOf('(');
            int index2 = strReply.IndexOf(')');
            string ipData =
            strReply.Substring(index1 + 1, index2 - index1 - 1);
            int[] parts = new int[6];
            int len = ipData.Length;
            int partCount = 0;
            string buf = "";
            for (int i = 0; i < len && partCount <= 6; i++)
            {
                char ch = Char.Parse(ipData.Substring(i, 1));
                if (Char.IsDigit(ch))
                    buf += ch;
                else if (ch != ',')
                {
                    throw new IOException("Malformed PASV strReply: " +
                    strReply);
                }
                if (ch == ',' || i + 1 == len)
                {
                    try
                    {
                        parts[partCount++] = Int32.Parse(buf);
                        buf = "";
                    }
                    catch (Exception)
                    {
                        throw new IOException("Malformed PASV strReply: " +
                         strReply);
                    }
                }
            }
            string ipAddress = parts[0] + "." + parts[1] + "." +
            parts[2] + "." + parts[3];
            int port = (parts[4] << 8) + parts[5];
            Socket s = new
            Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new
            IPEndPoint(IPAddress.Parse(ipAddress), port);
            try
            {
                s.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("Can't connect to remote server");
            }
            return s;
        }


        /// <summary>
        /// 关闭socket连接(用于登录以前)
        /// </summary>
        private void CloseSocketConnect()
        {
            if (socketControl != null)
            {
                socketControl.Close();
                socketControl = null;
            }
            bConnected = false;
        }

        /// <summary>
        /// 读取Socket返回的所有字符串
        /// </summary>
        /// <returns>包含应答码的字符串行</returns>
        private string ReadLine()
        {
            while (true)
            {
                try
                {
                    int iBytes = socketControl.Receive(buffer, buffer.Length, 0);
                    strMsg += GB2312.GetString(buffer, 0, iBytes);
                    if (iBytes < buffer.Length)
                    {
                        break;
                    }
                }
                catch (Exception error)
                {
                    throw error;
                }
                
            }

            char[] seperator = { '\n' };
            string[] mess = strMsg.Split(seperator);
            if (strMsg.Length > 2)
            {
                strMsg = mess[mess.Length - 2];
                //seperator[0]是10,换行符是由13和0组成的,分隔后10后面虽没有字符串,
                //但也会分配为空字符串给后面(也是最后一个)字符串数组,
                //所以最后一个mess是没用的空字符串
                //但为什么不直接取mess[0],因为只有最后一行字符串应答码与信息之间有空格
            }
            else
            {
                strMsg = mess[0];
            }
            if (!strMsg.Substring(3, 1).Equals(" "))//返回字符串正确的是以应答码(如220开头,后面接一空格,再接问候字符串)
            {
                return ReadLine();
            }
            return strMsg;
        }


        /// <summary>
        /// 发送命令并获取应答码和最后一行应答字符串
        /// </summary>
        /// <param name="strCommand">命令</param>
        private void SendCommand(String strCommand)
        {
            try
            {
                Byte[] cmdBytes = GB2312.GetBytes((strCommand + "\r\n").ToCharArray());
                socketControl.Send(cmdBytes, cmdBytes.Length, 0);
                ReadReply();
            }
            catch (Exception error)
            {
                throw error;
            }
            
        }

        #endregion

        #region 构造函数
        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public FTPClient()
        {
            strRemoteHost = "";
            strRemotePath = "";
            strRemoteUser = "";
            strRemotePass = "";
            strRemotePort = 21;
            bConnected = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="remoteHost">FTP服务器IP地址</param>
        /// <param name="remotePath">当前服务器目录</param>
        /// <param name="remoteUser">登录用户账号</param>
        /// <param name="remotePass">登录用户密码</param>
        /// <param name="remotePort">FTP服务器端口</param>
        public FTPClient(string remoteHost, string remotePath, string remoteUser, string remotePass, int remotePort)
        {
            strRemoteHost = remoteHost;
            strRemotePath = remotePath;
            strRemoteUser = remoteUser;
            strRemotePass = remotePass;
            strRemotePort = remotePort;

            //Connect();
        }
        #endregion

        #region 链接

        //private void ConnectionCallback(object state)
        //{
        //    TimeoutObject.Dispose();
        //    if (bConnected==false)
        //    {
        //        CloseSocketConnect();
        //    }
        //    System.Diagnostics.Debug.WriteLine("超时关闭连接!");
        //}

        /// <summary>
        /// 建立连接 
        /// </summary>
        public void Connect()
        {
            //if (TimeoutObject==null)
            //{
            //    TimerCallback timerDelegate = new TimerCallback(ConnectionCallback);
            //    TimeoutObject = new Timer(timerDelegate, null, 5000, 5000);
            //}

            socketControl = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(RemoteHost), strRemotePort);
            // 链接
            try
            {
                socketControl.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("Couldn't connect to remote server");
            }

            try
            {
                // 获取应答码
                ReadReply();
            }
            catch (Exception error)
            {
                DisConnect(false);
                throw error;
            }
           
            if (iReplyCode != 220)
            {
                DisConnect(false);
                throw new IOException(strReply.Substring(4));
            }

            // 登陆
            SendCommand("USER " + strRemoteUser);
            if (!(iReplyCode == 331 || iReplyCode == 230))
            {
                CloseSocketConnect();//关闭连接
                throw new IOException(strReply.Substring(4));
            }
            if (iReplyCode != 230)
            {
                SendCommand("PASS " + strRemotePass);
                if (!(iReplyCode == 230 || iReplyCode == 202))
                {
                    CloseSocketConnect();//关闭连接
                    throw new IOException(strReply.Substring(4));
                }
            }
            bConnected = true;

            // 切换到初始目录
            if (!string.IsNullOrEmpty(strRemotePath))
            {
                ChDir(strRemotePath);
            }
        }


        /// <summary>
        /// 关闭连接
        /// </summary>
        public void DisConnect(bool args)
        {
            Cancel = args;
            if (socketControl != null)
            {
                SendCommand("QUIT");
            }
            CloseSocketConnect();
        }

        #endregion

        #region 登陆字段、属性
        /// <summary>
        /// FTP服务器IP地址
        /// </summary>
        private string strRemoteHost;
        public string RemoteHost
        {
            get
            {
                return strRemoteHost;
            }
            set
            {
                strRemoteHost = value;
            }
        }
        /// <summary>
        /// FTP服务器端口
        /// </summary>
        private int strRemotePort;
        public int RemotePort
        {
            get
            {
                return strRemotePort;
            }
            set
            {
                strRemotePort = value;
            }
        }
        /// <summary>
        /// 当前服务器目录
        /// </summary>
        private string strRemotePath;
        public string RemotePath
        {
            get
            {
                return strRemotePath;
            }
            set
            {
                strRemotePath = value;
            }
        }
        /// <summary>
        /// 登录用户账号
        /// </summary>
        private string strRemoteUser;
        public string RemoteUser
        {
            set
            {
                strRemoteUser = value;
            }
        }
        /// <summary>
        /// 用户登录密码
        /// </summary>
        private string strRemotePass;
        public string RemotePass
        {
            set
            {
                strRemotePass = value;
            }
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        private Boolean bConnected;
        public bool Connected
        {
            get
            {
                return bConnected;
            }
        }
        #endregion

        #region 目录操作
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="strDirName">目录名</param>
        public void MkDir(string strDirName)
        {
            if (!bConnected)
            {
                try
                {
                    Connect();
                }
                catch (Exception error)
                {
                    CloseSocketConnect();
                    throw error;
                }
                
            }

            try
            {
                SendCommand("MKD " + strDirName);
            }
            catch (Exception error)
            {
                CloseSocketConnect();
                throw error;
            }
            
            if (iReplyCode != 257 && iReplyCode != 250)
            {
                CloseSocketConnect();
                throw new IOException(strReply.Substring(4));

            }
        }


        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="strDirName">目录名</param>
        public void RmDir(string strDirName)
        {
            if (!bConnected)
            {
                try
                {
                    Connect();
                }
                catch (Exception error)
                {
                    CloseSocketConnect();
                    throw error;
                }
                
            }
            try
            {
                SendCommand("RMD " + strDirName);
            }
            catch (Exception error)
            {
                CloseSocketConnect();
                throw error;
            }
            
            if (iReplyCode != 250)
            {
                CloseSocketConnect();
                throw new IOException(strReply.Substring(4));
            }
        }


        /// <summary>
        /// 改变目录
        /// </summary>
        /// <param name="strDirName">新的工作目录名</param>
        public void ChDir(string strDirName)
        {
            if (strDirName.Equals(".") || strDirName.Equals(""))
            {
                return;
            }
            if (!bConnected)
            {
                try
                {
                    Connect();
                }
                catch (Exception error)
                {
                    CloseSocketConnect();
                    throw error;
                }
            }

            try
            {
                SendCommand("CWD " + strDirName);
            }
            catch (Exception error)
            {
                CloseSocketConnect();
                throw error;
            }

            
            if (iReplyCode != 250)
            {
                CloseSocketConnect();
                throw new IOException(strReply.Substring(4));
            }
            this.strRemotePath = strDirName;
        }

        #endregion

        #region 传输模式

        /// <summary>
        /// 传输模式:二进制类型、ASCII类型
        /// </summary>
        public enum TransferType
        {
            Binary,
            ASCII
        };

        /// <summary>
        /// 设置传输模式
        /// </summary>
        /// <param name="ttType">传输模式</param>
        public void SetTransferType(TransferType ttType)
        {
            if (ttType == TransferType.Binary)
            {
                SendCommand("TYPE I");//binary类型传输
            }
            else
            {
                SendCommand("TYPE A");//ASCII类型传输
            }
            if (iReplyCode != 200)
            {
                throw new IOException(strReply.Substring(4));
            }
            else
            {
                trType = ttType;
            }
        }


        /// <summary>
        /// 获得传输模式
        /// </summary>
        /// <returns>传输模式</returns>
        public TransferType GetTransferType()
        {
            return trType;
        }

        #endregion

        #region 文件操作
        /// <summary>
        /// 获得文件列表
        /// </summary>
        /// <param name="strMask">文件名的匹配字符串</param>
        /// <returns></returns>
        public string[] Dir(string strMask)
        {
            // 建立链接
            if (!bConnected)
            {
                try
                {
                    Connect();
                }
                catch (Exception error)
                {
                    CloseSocketConnect();
                    throw error;
                }
                
            }

            //建立进行数据连接的socket
            Socket socketData;
            try
            {
                //建立进行数据连接的socket
                socketData = CreateDataSocket();
            }
            catch (Exception error)
            {
                CloseSocketConnect();
                throw error;
            }
            

            try
            {
                if (strMask==null)
                {
                    SendCommand("LIST");
                }
                else
                {
                    SendCommand("LIST " + strMask);
                }
            }
            catch (Exception error)
            {
                CloseSocketConnect();
                throw error;
            }
            //传送命令
            

            //分析应答代码
            if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226))
            {
                string result;
                try
                {
                    result=strReply.Substring(4);
                }
                catch (Exception error)
                {
                    CloseSocketConnect();
                    throw error;
                }
                try
                {
                    CloseSocketConnect();
                    throw new IOException(result);
                }
                catch (Exception error)
                {

                    CloseSocketConnect();
                    throw error;
                }
                
            }

            //获得结果
            strMsg = "";
            while (true)
            {
                try
                {
                    int iBytes = socketData.Receive(buffer, buffer.Length, 0);
                    strMsg += GB2312.GetString(buffer, 0, iBytes);
                    if (iBytes < buffer.Length)
                    {
                        break;
                    }
                }
                catch (Exception error)
                {

                    CloseSocketConnect();
                    throw error;
                }
                
            }
            char[] seperator = { '\n' };
            string[] strsFileList = strMsg.Split(seperator);
            socketData.Close();//数据socket关闭时也会有返回码
            if (iReplyCode != 226)
            {
                try
                {
                    ReadReply();
                }
                catch (Exception error)
                {
                    CloseSocketConnect();
                    throw error;
                }
                
                if (iReplyCode != 226)
                {
                    CloseSocketConnect();
                    throw new IOException(strReply.Substring(4));
                }
            }
            return strsFileList;
        }


        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <returns>文件大小</returns>
        public long GetFileSize(string strFileName)
        {
            if (!bConnected)
            {
                try
                {
                    Connect();
                }
                catch (Exception error)
                {

                    CloseSocketConnect();
                    throw error;
                }
            }

            try
            {
                SendCommand("SIZE " + Path.GetFileName(strFileName));
            }
            catch (Exception error)
            {
                CloseSocketConnect();
                throw error;
            }

            long lSize = 0;
            if (iReplyCode == 213)
            {
                lSize = Int64.Parse(strReply.Substring(4));
            }
            else
            {
                CloseSocketConnect();
                throw new IOException(strReply.Substring(4));
            }
            return lSize;
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strFileName">待删除文件名</param>
        public void Delete(string strFileName)
        {
            if (!bConnected)
            {
                try
                {
                    Connect();
                }
                catch (Exception error)
                {
                    throw error;
                }
            }

            try
            {
                SendCommand("DELE " + strFileName);
            }
            catch (Exception error)
            {
                CloseSocketConnect();
                throw error;
            }

            if (iReplyCode != 250)
            {
                CloseSocketConnect();
                throw new IOException(strReply.Substring(4));
            }
        }


        /// <summary>
        /// 重命名(如果新文件名与已有文件重名,将覆盖已有文件)
        /// </summary>
        /// <param name="strOldFileName">旧文件名</param>
        /// <param name="strNewFileName">新文件名</param>
        public void Rename(string strOldFileName, string strNewFileName)
        {
            if (!bConnected)
            {

                try
                {
                    Connect();
                }
                catch (Exception error)
                {
                    CloseSocketConnect();
                    throw error;
                }
                
            }

            try
            {
                SendCommand("RNFR " + strOldFileName);
            }
            catch (Exception error)
            {
                CloseSocketConnect();
                throw error;
            }

            
            if (iReplyCode != 350)
            {
                CloseSocketConnect();
                throw new IOException(strReply.Substring(4));
            }
            //  如果新文件名与原有文件重名,将覆盖原有文件

            try
            {
                SendCommand("RNTO " + strNewFileName);
            }
            catch (Exception error)
            {
                CloseSocketConnect();
                throw error;
            }

            if (iReplyCode != 250)
            {
                CloseSocketConnect();
                throw new IOException(strReply.Substring(4));
            }
        }
        #endregion

        #region 上传和下载
        /// <summary>
        /// 下载一批文件
        /// </summary>
        /// <param name="strFileNameMask">文件名的匹配字符串</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        public void Get(string strFileNameMask, string strFolder)
        {
            if (!bConnected)
            {
                try
                {
                    Connect();
                }
                catch (Exception error)
                {
                    throw error;
                }
                
            }

            string[] strFiles;
            try
            {
                strFiles = Dir(strFileNameMask);
            }
            catch (Exception error)
            {
                throw error;
            }
            
            foreach (string strFile in strFiles)
            {
                if (!strFile.Equals(""))//一般来说strFiles的最后一个元素可能是空字符串
                {
                    if (strFile.LastIndexOf(".") > -1)
                    {
                        try
                        {
                            Get(strFile.Replace("\r", ""), strFolder, strFile.Replace("\r", ""));
                        }
                        catch (Exception error)
                        {
                            throw error;
                        }
                        
                    }
                }
            }
        }


        /// <summary>
        /// 下载目录
        /// </summary>
        /// <param name="strRemoteFileName">要下载的文件名</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strLocalFileName">保存在本地时的文件名</param>
        public void Get(string strRemoteFileName, string strFolder, string strLocalFileName)
        {
            if (strLocalFileName.StartsWith("-r"))
            {
                string[] infos = strLocalFileName.Split(' ');
                strRemoteFileName = strLocalFileName = infos[infos.Length - 1];

                if (!bConnected)
                {
                    try
                    {
                        Connect();
                    }
                    catch (Exception error)
                    {
                        CloseSocketConnect();
                        throw error;
                    }
                }

                SetTransferType(TransferType.Binary);
                if (strLocalFileName.Equals(""))
                {
                    strLocalFileName = strRemoteFileName;
                }


                if (!File.Exists(strLocalFileName))
                {
                    try
                    {
                        Stream st = File.Create(strLocalFileName);
                        st.Close();
                    }
                    catch (Exception error)
                    {
                        CloseSocketConnect();
                        throw error;
                    }
                }

                FileStream output;
                try
                {
                    output = new FileStream(strFolder + "\\" + strLocalFileName, FileMode.Create);
                }
                catch (Exception error)
                {
                    throw error;
                }               

                Socket socketData;
                try
                {
                    socketData = CreateDataSocket();
                }
                catch (Exception error)
                {
                    throw error;
                }

                try
                {
                    SendCommand("RETR " + strRemoteFileName);
                }
                catch (Exception error)
                {
                    output.Close();
                    CloseSocketConnect();
                    throw error;
                }

                if (!(iReplyCode == 150 || iReplyCode == 125
                || iReplyCode == 226 || iReplyCode == 250))
                {
                    throw new IOException(strReply.Substring(4));
                }
                while (true)
                {
                    try
                    {
                        int iBytes = socketData.Receive(buffer, buffer.Length, 0);
                        output.Write(buffer, 0, iBytes);
                        if (iBytes <= 0)
                        {
                            break;
                        }
                    }
                    catch (Exception error)
                    {
                        output.Close();
                        CloseSocketConnect();
                        throw error;
                    }
                    
                }
                output.Close();
                if (socketData.Connected)
                {
                    socketData.Close();
                }
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    try
                    {
                        ReadReply();
                    }
                    catch (Exception error)
                    {
                        throw error;
                    }
                    
                    if (!(iReplyCode == 226 || iReplyCode == 250))
                    {
                        throw new IOException(strReply.Substring(4));
                    }
                }
            }
        }

        /// <summary>
        /// 下载一个文件
        /// </summary>
        /// <param name="strRemoteFileName">要下载的文件名</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strLocalFileName">保存在本地时的文件名</param>
        public void GetFile(string strRemoteFileName, string strFolder)
        {
            string strLocalFileName = strRemoteFileName;

            if (strLocalFileName!=null)
            {
                string[] infos = strLocalFileName.Split(' ');
                strRemoteFileName = strLocalFileName = infos[infos.Length - 1];

                if (!bConnected)
                {
                    try
                    {
                        Connect();
                    }
                    catch (Exception error)
                    {
                        CloseSocketConnect();

                        if (GetFileErrorEvent != null)
                        {
                            GetFileErrorEvent(error.Message.ToString(), strRemoteFileName);
                        }
                        throw error;
                    }
                    
                }
                SetTransferType(TransferType.Binary);
                if (strLocalFileName.Equals(""))
                {
                    strLocalFileName = strRemoteFileName;
                }

                if (!File.Exists(strLocalFileName))
                {
                    try
                    {
                        Stream st = File.Create(strLocalFileName);
                        st.Close();
                    }
                    catch (Exception error)
                    {
                        if (GetFileErrorEvent != null)
                        {
                            GetFileErrorEvent(error.Message.ToString(), strRemoteFileName);
                        }
                        throw error;
                    }
                    
                }


                FileStream output;
                try
                {
                    output = new FileStream(strFolder + "\\" + strLocalFileName, FileMode.Create);
                }
                catch (Exception error)
                {
                    if (GetFileErrorEvent != null)
                    {
                        GetFileErrorEvent(error.Message.ToString(), strRemoteFileName);
                    }
                    throw error;
                }                

                Socket socketData;
                try
                {
                    socketData = CreateDataSocket();
                }
                catch (Exception error)
                {
                    if (GetFileErrorEvent != null)
                    {
                        GetFileErrorEvent(error.Message.ToString(), strRemoteFileName);
                    }
                    throw error;
                }

                try
                {
                    SendCommand("RETR " + strRemoteFileName);
                }
                catch (Exception error)
                {
                    output.Close();
                    CloseSocketConnect();
                    if (GetFileErrorEvent != null)
                    {
                        GetFileErrorEvent(error.Message.ToString(), strRemoteFileName);
                    }
                    throw error;
                }
                
                
                if (!(iReplyCode == 150 || iReplyCode == 125
                || iReplyCode == 226 || iReplyCode == 250))
                {
                    if (GetFileErrorEvent != null)
                    {
                        GetFileErrorEvent(iReplyCode.ToString(), strRemoteFileName);
                    }
                    throw new IOException(strReply.Substring(4));
                }

                int ProgressValue = 0;
                while (true)
                {
                    try
                    {
                        int iBytes = socketData.Receive(buffer, buffer.Length, 0);                       
                        output.Write(buffer, 0, iBytes);
                        ProgressValue += iBytes;
                        if (GetFileProgressEvent!=null)
                        {
                            GetFileProgressEvent(strRemoteFileName, ProgressValue);
                        }
                        if (iBytes <= 0)
                        {
                            break;
                        }
                    }
                    catch (Exception error)
                    {
                        output.Close();
                        CloseSocketConnect();
                        if (GetFileErrorEvent != null)
                        {
                            GetFileErrorEvent(error.Message.ToString(), strRemoteFileName);
                        }
                        throw error;
                    }
                }

                output.Close();

                if (socketData.Connected)
                {
                    socketData.Close();
                }

                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    try
                    {
                        ReadReply();

                        if (!(iReplyCode == 226 || iReplyCode == 250))
                        {
                            if (GetFileErrorEvent != null)
                            {
                                GetFileErrorEvent(iReplyCode.ToString(), strRemoteFileName);
                            }
                            throw new IOException(strReply.Substring(4));
                        }
                    }
                    catch (Exception)
                    {
                        if (GetFileErrorEvent != null)
                        {
                            GetFileErrorEvent(Cancel, strRemoteFileName);
                        }
                        return;
                    }
                }

                if (GetFileSucceedEvent!=null)
                {
                    GetFileSucceedEvent(strFolder, strRemoteFileName);
                }
            }
        }

        /// <summary>
        /// 下载一个文件
        /// </summary>
        /// <param name="strRemoteFileName">要下载的文件名</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strLocalFileName">保存在本地时的文件名</param>
        public void GetBrokenFile(string strRemoteFileName, string strFolder, string strLocalFileName, long size)
        {
            if (!bConnected)
            {
                Connect();
            }

            SetTransferType(TransferType.Binary);

            FileStream output=null;
            try
            {
                output = new FileStream(strFolder + "\\" + strLocalFileName, FileMode.Append);
            }
            catch (Exception error)
            {
                if (output!=null)
                    output.Close();
                CloseSocketConnect();
                throw error;
            }

            Socket socketData;
            try
            {
                socketData = CreateDataSocket();
            }
            catch (Exception error)
            {
                CloseSocketConnect();
                output.Close();
                throw error;
            }

            try
            {
                SendCommand("REST " + size.ToString());
                SendCommand("RETR " + strRemoteFileName);
            }
            catch (Exception error)
            {
                output.Close();
                CloseSocketConnect();
                throw error;
            }
            
            


            if (!(iReplyCode == 150 || iReplyCode == 125
            || iReplyCode == 226 || iReplyCode == 250))
            {
                throw new IOException(strReply.Substring(4));
            }

            while (true)
            {
                try
                {
                    int iBytes = socketData.Receive(buffer, buffer.Length, 0);
                    output.Write(buffer, 0, iBytes);
                    if (iBytes <= 0)
                    {
                        break;
                    }
                }
                catch (Exception error)
                {
                    output.Close();
                    CloseSocketConnect();
                    throw error;
                }
                
            }
            output.Close();

            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                try
                {
                    ReadReply();
                }
                catch (Exception error)
                {
                    throw error;
                }
                
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    throw new IOException(strReply.Substring(4));
                }
            }
        }



        /// <summary>
        /// 上传一批文件
        /// </summary>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strFileNameMask">文件名匹配字符(可以包含*和?)</param>
        public void Put(string strFolder, string strFileNameMask)
        {

            string[] strFiles = null;
            try
            {
                strFiles = Directory.GetFiles(strFolder, strFileNameMask);
            }
            catch (Exception error)
            {
                throw error;
            }

            foreach (string strFile in strFiles)
            {
                //strFile是完整的文件名(包含路径)
                try
                {
                    Put(strFile, false);
                }
                catch (Exception error)
                {

                    throw error;
                }
                
            }
            if (PushFilesSucceedEvent != null)
            {
                PushFilesSucceedEvent(strFiles, strFileNameMask);
            }
        }


        /// <summary>
        /// 上传一个文件
        /// </summary>
        /// <param name="strFileName">本地文件名</param>
        /// <param name="isFrist">上传完成是否激活事件</param>
        public void Put(string strFileName,bool isActive)
        {
            string ftpname = Guid.NewGuid().ToString().Replace("-", "") + "_" + Path.GetFileName(strFileName);
            if (!bConnected)
            {
                try
                {
                    Connect();
                }
                catch (Exception error)
                {
                    if (PushFileErrorEvent != null)
                    {
                        PushFileErrorEvent(error.Message.ToString(), strFileName);
                    }
                    CloseSocketConnect();
                    throw error;
                }
               
            }

            Socket socketData=null;
            try
            {
                socketData = CreateDataSocket();
            }
            catch (Exception error)
            {
                if (PushFileErrorEvent != null)
                {
                    PushFileErrorEvent(error.Message.ToString(), strFileName);
                }
                CloseSocketConnect();
                throw error;
            }

            try
            {
                SendCommand("STOR " + ftpname);
            }
            catch (Exception error)
            {
                if (PushFileErrorEvent != null)
                {
                    PushFileErrorEvent(error.Message.ToString(), strFileName);
                }
                throw error;
            }
             
            if (!(iReplyCode == 125 || iReplyCode == 150))
            {
                if (PushFileErrorEvent != null)
                {
                    PushFileErrorEvent(iReplyCode.ToString(), strFileName);
                }
                throw new IOException(strReply.Substring(4));
            }

            FileStream input=null;
            try
            {
                input = new FileStream(strFileName, FileMode.Open);
            }
            catch (Exception error)
            {
                input.Close();
                if (socketData.Connected)
                {
                    socketData.Close();
                }
                if (PushFileErrorEvent != null)
                {
                    PushFileErrorEvent(error.Message.ToString(), strFileName);
                }
                throw error;
            }
             
            int ProgressValue = 0;
            int iBytes = 0;
            while ((iBytes = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (PushProgressEvent!=null)
                {
                    ProgressValue += iBytes;
                    PushProgressEvent(null, ProgressValue);
                }
                try
                {
                    socketData.Send(buffer, iBytes, 0);
                }
                catch (Exception)
                {
                    CloseSocketConnect();
                    input.Close();
                    if (PushFileErrorEvent != null)
                    {
                        PushFileErrorEvent(Cancel, strFileName);
                    }
                    return;
                }
                
            }
            input.Close();
            if (isActive)
            {
                if (PushFileSucceedEvent != null)
                {
                    PushFileSucceedEvent(isActive, ftpname);
                }
            }
            
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                try
                {
                    ReadReply();
                }
                catch (Exception error)
                {
                    if (PushFileErrorEvent != null)
                    {
                        PushFileErrorEvent(error.Message.ToString(), strFileName);
                    }
                    throw error;
                }
               
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    if (PushFileErrorEvent != null)
                    {
                        PushFileErrorEvent(iReplyCode.ToString(), strFileName);
                    }
                    throw new IOException(strReply.Substring(4));
                }
            }
        }

        #endregion

    }
}