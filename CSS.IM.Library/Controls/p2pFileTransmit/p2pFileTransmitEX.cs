using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Net;

namespace CSS.IM.Library.Controls
{
    #region 文件传输控件
    public partial class p2pFileTransmitEX : Component
    {
        #region 组件初始化
        public p2pFileTransmitEX()
        {
            InitializeComponent();
        }

        public p2pFileTransmitEX(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #endregion

        #region 自定义变量

        /// <summary>
        /// 读写文件的绝对路径
        /// </summary>
        private string _fullFileName ="";//发送或接收文件所保存的位置
       
        /// <summary>
        /// 显示的文件名（不包括路径）
        /// </summary>
        private  string _fileName="";
       

        /// <summary>
        /// 服务器ip地址 
        /// </summary>
        private System.Net.IPAddress _serverIp = System.Net.IPAddress.Parse("127.0.0.1");

        public System.Net.IPAddress ServerIp
        {
            get { return _serverIp; }
            set { _serverIp = value; }
        }

        /// <summary>
        /// 中转TCP服务器
        /// </summary>
        private int _serverUDPPort = 0;

        /// <summary>
        /// 中转UDP服务器
        /// </summary>
        //private int _serverTCPPort = 0;
         
        /// <summary>
        /// 文件尺寸
        /// </summary>
        private int _FileLen = 0;//
       
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Extension = "";
       
        /// <summary>
        /// 标记文件是否在发送过程中
        /// </summary>
        private bool IsSendState = false;

        /// <summary>
        /// 标识当前用户是发送文件还是接收文件
        /// </summary>
        private bool _IsSend = false;

        /// <summary>
        /// 是否取消文件传输
        /// </summary>
        private bool _isCancelTransmit = false ;

        /// <summary>
        /// 文件尺寸中文描述
        /// </summary>
        public  string  FileLenStr = "0";
       
        /// <summary>
        ///  一次读写文件的缓冲区大小，定义为10M
        /// </summary>
        private byte[] FileBlock;


        /// <summary>
        /// 记录总共要读写文件次数
        /// </summary>
        private int readFileCount = 1;//10M

        /// <summary>
        /// 当前读文件次数
        /// </summary>
        private int currReadCount = 0;//当前读文件次数

        /// <summary>
        /// 当前获得文件的数据长度
        /// </summary>
        private int currGetPos = 0; 

        /// <summary>
        /// 一次读写文件的大小，定义为10M
        /// </summary>
        private int _maxReadWriteFileBlock = 5242880*2;//5M
        [Category("全局设置")]
        [Description("一次读写文件的缓冲区大小，默认为10M")]
        public int maxReadWriteFileBlock
        {
            set { _maxReadWriteFileBlock = value; }
            get { return _maxReadWriteFileBlock; }
        }

        /// <summary>
        /// 每次发送的数据包缓冲容量
        /// </summary>
        private UInt16 _mtu = 1250;//标记一次传输文件数据块的大小，不能超过网络最大传输单元 MTU 576-1492 限制，否则在因特网上的数据发送将不成功


        private byte  outTime = 0;
        [Category("全局设置")]
        [Description("设置UDP每一次传输数据包的超时秒数")]
        [DefaultValue(2)]
        public byte OutTime
        {
            set { outTime = value; }
            get { return outTime; }
        }


        public string FileMD5Value;//文件MD5值
        //private CSS.IM.Library.Class.UserInfo _selfUserInfo;//自己的在线信息
        //private CSS.IM.Library.Class.UserInfo _OppositeUserInfo;//对方的在线信息
        private int serverSelfID = -1;//自己的中转服务ID 
        private int serverOppositeID = -1;//对方的中转服务ID
        private int OppositeUDPPort = -1;//对方文件传输本地UDP端口
        private IPAddress OppositeUDPIP = IPAddress.Parse("127.0.0.1");
        private int selfUDPPort = -1;//自己的文件传输本地UDP端口
        private CSS.IM.Library.Class.NetCommunicationClass netClass = CSS.IM.Library.Class.NetCommunicationClass.None;//文件传输所采用的通信协议,初始为通讯没有成功

        #endregion

        #region  文件传输事件 
        /// <summary>
        /// 文件传输结束事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmittedEventHandler(object sender, fileTransmitEvnetArgs e);//文件传输结束事件
        public event fileTransmittedEventHandler fileTransmitted ;

        /// <summary>
        /// 取消文件传输事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitCancelEventHandler(object sender, fileTransmitEvnetArgs e);//取消文件传输事件
        public event fileTransmitCancelEventHandler fileTransmitCancel ;

        /// <summary>
        /// 文件传输开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitBeforeEventHandler(object sender, fileTransmitEvnetArgs e);//接收文件传输事件
        public event fileTransmitBeforeEventHandler fileTransmitBefore;

        /// <summary>
        ///  文件传输超时事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitOutTimeEventHandler(object sender, fileTransmitEvnetArgs e);//接收文件发送超时
        public event fileTransmitOutTimeEventHandler fileTransmitOutTime ;

        /// <summary>
        /// 文件传输错误事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public delegate void fileTransmitErrorEventHandler(object sender, fileTransmitEvnetArgs e);//文件发送错误 
        //public event fileTransmitErrorEventHandler fileTransmitError ;

        /// <summary>
        /// 事件：发送或收到文件数据
        /// </summary>
        /// <param name="sender">对像</param>
        /// <param name="e"></param>
        public delegate void fileTransmittingEventHandler(object sender, fileTransmitEvnetArgs e);//发送或收到文件数据 
        public event fileTransmittingEventHandler fileTransmitting;

        public delegate void getFileProxyIDEventHandler(object sender, int proxyID);//发送或收到文件数据 
        public event getFileProxyIDEventHandler getFileProxyID ;

        public delegate void ConnectedEventHandler(object sender, CSS.IM.Library.Class.NetCommunicationClass NetCommunicationClass);//发送或收到文件数据 
        public event ConnectedEventHandler fileTransmitConnected;

        public delegate void GetUDPPortEventHandler(object sender, int Port, bool udpHandshakeInfoClass);//获得本地UDP端口事件 
        public event GetUDPPortEventHandler fileTransmitGetUDPPort;

        #endregion

        #region 设置传输文件参数
        /// <summary>
        /// 设置传输文件参数
        /// </summary>
        /// <param name="isSend">标识文件是发送还是接收</param>
        /// <param name="FullFileName">如果发送文件，则设置要发放的文件路径</param>
        /// <param name="FileName">文件名</param>
        /// <param name="FileLen">文件长度</param>
        /// <param name="fileExtension">文件扩展名</param>
        /// <param name="FileMD5Value">文件MD5值</param>
        /// <param name="ServerIP">服务器IP地址</param>
        /// <param name="ServerUDPPort">服务器UDP服务端口</param>
        /// <param name="ServerTCPPort">服务器TCP服务端口</param>
        /// <param name="selfUserInfo">自己的在线用户信息</param>
        /// <param name="OppositeUserInfo">对方的在线用户信息</param>
        public void SetParameter(bool IsSend, string FullFileName, string FileName, int FileLen, string fileExtension, string FileMD5Value)
        {
            //文件传输前建立双方连接的参数设置函数
            this._IsSend = IsSend;
            //this._serverIp = ServerIP;//获取服务器IP地址
            //this._serverUDPPort = ServerUDPPort;//获取文件服务器UDP服务端口
            //this._serverTCPPort = ServerTCPPort;//获取文件服务器TCP服务端口
            this.Extension = fileExtension;//获取文件扩展名
            this._FileLen = FileLen;//获取文件长度
            this._fileName = FileName;//文件名称
            this.FileMD5Value = FileMD5Value;//获取文件的MD5值
            //this._selfUserInfo = selfUserInfo;//获取自己的在线用户信息
            //this._OppositeUserInfo = OppositeUserInfo;//获取对方的在线用户信息

            this.FileLenStr = CSS.IM.Library.Class.Calculate.GetSizeStr(FileLen);//获得文件尺寸字符串

            this.readFileCount = FileLen / this.maxReadWriteFileBlock;//获得文件读写次数

            if (FileLen % this.maxReadWriteFileBlock != 0)
                this.readFileCount++;//如果读写文件有余，则读次写次数增1

            if (_IsSend)//如果是文件发送者
                this._fullFileName  = FullFileName;//读文件的绝对路径
        }
        #endregion 

        #region 发送文件
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="currTransmittedLen">当前已经传输完成的文件数据长度</param>
        private void sendFile(int currTransmittedLen)
        {

            //Console.WriteLine("sendFile_currTransmittedLen:" + currTransmittedLen);
            if (currTransmittedLen >= this._FileLen)
            {
                //Console.WriteLine("sendFile_currTransmittedLen:" + currTransmittedLen);
                onFileTransmitted();//触发文件传输结束事件
                return;//如果对方要求发送的数据块起始位置大于文件尺寸则认为是非法请求退出
            }

            if (!IsSendState)
            {
                IsSendState = true;//设置传输

                if (this.fileTransmitBefore != null)//触发文件开始传输前事件 
                    this.fileTransmitBefore(this, new fileTransmitEvnetArgs(this._IsSend, this._fullFileName, this._fileName, "", this._FileLen, this.currGetPos, this.FileMD5Value));
            }

            if (IsReadWriteFile(currTransmittedLen))//如果当前是需要读写文件
            {
                //读文件到内存过程
                //if (this.currReadCount + 1 == this.readFileCount)//如果是最后一次读写文件，则将所有文件尾数据全部读入到内存
                //    FileBlock = new byte[this._FileLen - this.currReadCount * this.maxReadWriteFileBlock];
                //else
                //    FileBlock = new byte[this.maxReadWriteFileBlock];

                FileBlock = new byte[this._FileLen];
                ////////////////////////文件操作
                FileStream fw = new FileStream(this._fullFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                fw.Seek(currTransmittedLen, SeekOrigin.Begin);//上次发送的位置
                fw.Read(FileBlock, 0, FileBlock.Length);
                //随机读写，还可以用异步方式   
                //fw.BeginRead(myData.Buffer, 0, assignSize, new AsyncCallback(AsyncRead), myData);
                ///实现多线程同时读写文件
                fw.Close();
                fw.Dispose();
                ///////////////////////////
                //FileStream file_s=new FileStream(this._fullFileName+"aa.jpg",FileMode.CreateNew,FileAccess.ReadWrite,FileShare.ReadWrite);
                //file_s.Write(FileBlock, 0, (int)FileBlock.Length);
                //file_s.Flush();
                //file_s.Close();
                //file_s.Dispose();

                //this.currReadCount++;//文件读次数自增1

                //FileBlock = new byte[this._FileLen];
            }

            //FileStream file_s = new FileStream(this._fullFileName + Guid.NewGuid().ToString()+".jpg", FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
            //file_s.Write(FileBlock, 0, (int)FileBlock.Length);
            //file_s.Flush();
            //file_s.Close();
            //file_s.Dispose();


            int offSet = currTransmittedLen % this.maxReadWriteFileBlock;// 获得要发送的绝对位置

            byte[] buffer;
            Console.WriteLine("FileLen:" + this._FileLen);
            Console.WriteLine("FileBlock:" + this.FileBlock.Length);
            if (offSet + this._mtu > this.FileBlock.Length)
                buffer = new byte[this.FileBlock.Length - offSet];//要发送的缓冲区
            else
                buffer = new byte[this._mtu];//要发送的缓冲区

            Buffer.BlockCopy(this.FileBlock, offSet, buffer, 0, buffer.Length);//将其保存于Buffer字节数组

            currTransmittedLen += buffer.Length;
            this.sendData(new CSS.IM.Library.Class.msgFile((byte)CSS.IM.Library.Class.ProtocolFileTransmit.GetFileBlock, this.serverSelfID, this.serverOppositeID, (long)currTransmittedLen, buffer));//发送已读取的文件数据给对方

            if (this.fileTransmitting != null)//触发收到或发送文件数据事件 
                this.fileTransmitting(this, new fileTransmitEvnetArgs(this._IsSend, this._fullFileName, this._fileName, "", this._FileLen, currTransmittedLen, this.FileMD5Value));

        }
        #endregion 

        #region  获取文件数据包请求
        /// <summary>
        /// 发送获取文件数据包请求
        /// </summary>
        private void sendRequestGetFileData()
        {
            this.timerGetFileOut.Enabled = false;
            this.OutTime = 0;
            this.sendData(new CSS.IM.Library.Class.msgFile((byte)CSS.IM.Library.Class.ProtocolFileTransmit.FileTransmit, this.serverSelfID, this.serverOppositeID, (long)this.currGetPos, new byte[0])); //请求对方发送文件数据包
            this.LastPos = this.currGetPos;
            this.timerGetFileOut.Enabled = true;
        }
        #endregion

        #region 处理收到的文件数据块
        /// <summary>
        /// 处理对方发送文件数据块
        /// </summary>
        private void ReceivedFileBlock(CSS.IM.Library.Class.msgFile msg)//当对方发送文件数据块过来
        {
            if (msg.pSendPos > this.currGetPos)//如果发送过来的数据大于当前获得的数据
            {
                if (this.IsReadWriteFile(this.currGetPos))
                {
                    //下面是读一次文件到内存过程
                    //FileBlock = new byte[this._FileLen];
                    if (this.currReadCount + 1 == this.readFileCount)//如果是最后一次读写文件，则将所有文件尾数据全部读入到内存
                        FileBlock = new byte[this._FileLen - this.currReadCount * this.maxReadWriteFileBlock];
                    else
                        FileBlock = new byte[this.maxReadWriteFileBlock];

                    this.currReadCount++;//文件读次数自增1
                }

                int offSet = this.currGetPos % this.maxReadWriteFileBlock;// 获得要读写内存的绝对位置
                Buffer.BlockCopy(msg.FileBlock, 0, this.FileBlock, offSet, msg.FileBlock.Length);//将其保存于Buffer字节数组

                this.currGetPos = (int)msg.pSendPos;

                //if (this.fileTransmitting != null)//触发收到或发送文件数据事件 
                //    this.fileTransmitting(this, new fileTransmitEvnetArgs(this._IsSend, this._fullFileName, this._fileName, "", this._FileLen, this.currGetPos, this.FileMD5Value));

                if (this.IsReadWriteFile(this.currGetPos) || this.currGetPos == this._FileLen)
                {
                    ////////////////////////文件操作
                    FileStream fw = new FileStream(this._fullFileName, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
                    //fw.Seek(fw.l, SeekOrigin.Begin);//上次发送的位置
                    fw.Write(this.FileBlock, 0, this.FileBlock.Length);
                    //随机读写，还可以用异步方式   
                    //fw.BeginRead(myData.Buffer, 0, assignSize, new AsyncCallback(AsyncRead), myData);
                    ///实现多线程同时读写文件
                    fw.Close();
                    fw.Dispose();

                    onFileTransmitted();//触发文件传输结束事件
                    ///////////////////////////
                }

                if (this.currGetPos == this._FileLen)//如果文件传输完成，触发传输完成事件
                {
                    msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.FileTranstmitOver;
                    msg.SendID = this.serverSelfID;
                    msg.RecID = this.serverOppositeID;
                    this.sendData(msg);//告诉对方文件传输结束
                    onFileTransmitted();//触发文件传输结束事件
                    //return;//文件传输
                }

                this.sendRequestGetFileData();//无论当前获得多少数据，均要求对方发送下一数据包
            }
        }
        #endregion

        #region 文件传输结束 
        /// <summary>
        /// 文件传输结束处理过程 
        /// </summary>
        private void onFileTransmitted()
        {
            if (this.fileTransmitted  != null)
                this.fileTransmitted(this, new fileTransmitEvnetArgs(this._IsSend, this._fullFileName, this._fileName, "", this._FileLen, this.currGetPos, this.FileMD5Value));
            try
            {
                if (this.netClass == CSS.IM.Library.Class.NetCommunicationClass.TCP)
                    this.asyncTCPClient1.Disconnect();
                else
                {
                    this.sockUDP1.CloseSock();
                    this.sockUDP1.Dispose();
                }
            }
            catch { }

            try
            {
                timerConnection.Enabled = false;
                timerGetFileOut.Enabled = false;
                timersUdpPenetrate.Enabled = false;
            }
            catch (Exception)
            {
                
            }
        }
        #endregion

        #region 判断当前是否需要读写文件
        /// <summary>
        /// 判断当前是否需要读写文件
        /// </summary>
        /// <param name="currTransmittedLen">当前文件传输的位置</param>
        /// <returns></returns>
        private bool IsReadWriteFile(int currTransmittedLen)
        {
            if (currTransmittedLen % this.maxReadWriteFileBlock == 0)
                return true;
            else
                return false;
        }

        #endregion
         
        #region winSock 发送数据
        /// <summary>
        /// sockUDP 发送文件数据
        /// </summary>
        /// <param name="msg">文件信息</param>
        private void sendData(CSS.IM.Library.Class.msgFile msg)
        {
            try
            {
                //if (this.netClass == CSS.IM.Library.Class.NetCommunicationClass.LanUDP)//如果是局域网通信
                    this.sockUDP1.Send(this.OppositeUDPIP,this.OppositeUDPPort, msg.getBytes());//采用UDP发送数据到对方局域网IP与端口
            }
            catch
            { }
        }

        /// <summary>
        /// sockUDP 发送数据
        /// </summary>
        /// <param name="Ip">接收者IP</param>
        /// <param name="Port">接收者端口</param>
        /// <param name="MsgContent">要发送的字节块</param>
        public void sendData(System.Net.IPAddress Ip, int Port, byte[] MsgContent)
        {
            try
            {
                this.sockUDP1.Send(Ip, Port, MsgContent);
            }
            catch { }
        }
        #endregion
            
        #region 文件传输超时器
        /// <summary>
        /// 最后一次获得文件数据包的位置
        /// </summary>
        private int LastPos = 0;

        private void timerGetFileOut_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this._isCancelTransmit || this.currGetPos ==this._FileLen)
            {
                this.timerGetFileOut.Enabled = false;
                this.Dispose();
                return;
            }
            OutTime++;

            if (OutTime > 3)
            {
                if (this.LastPos == this.currGetPos)//如果三秒钟后还未获得下一数据包，则超时
                    this.sendRequestGetFileData();
                ///触发文件接收超时事件
                if (this.fileTransmitOutTime != null)
                    this.fileTransmitOutTime(this, new fileTransmitEvnetArgs( this._IsSend , this._fullFileName , this._fileName , "", this._FileLen , this.currGetPos,this.FileMD5Value ));
            }
        }
        #endregion

        #region 取消文件传输
        /// <summary>
        /// 取消文件传输
        /// </summary>
        public void CancelTransmit(bool isMe)
        {
            this._isCancelTransmit = true;//取消文件传输为真

            try
            {
                if (this.netClass != CSS.IM.Library.Class.NetCommunicationClass.TCP)
                {
                    this.sockUDP1.CloseSock();//关闭sockUDP1端口，清楚占用的资源 
                    this.sockUDP1 = null;
                }
                {
                    this.asyncTCPClient1.Disconnect();
                    this.asyncTCPClient1.Dispose();
                    this.asyncTCPClient1 = null;
                }
            }
            catch { }

            if (this.fileTransmitCancel != null)
                this.fileTransmitCancel(this, new fileTransmitEvnetArgs(isMe, this._fullFileName, this._fileName, "", this._FileLen, this.currGetPos, this.FileMD5Value));//触发“文件取消发送事件”(自己取消的)
        }
        #endregion

        #region 开始侦听来自外部的消息 Listen()
        /// <summary>
        /// 开始侦听来自外部的消息
        /// </summary>
        private int UDPListen()//UDP开始侦听来自外部的消息.
        {
        xx:
            System.Random i = new Random();
            int j = i.Next(2000, 65530);
            try
            {
                this.sockUDP1.Listen(j);
                this.selfUDPPort = j;
                //Calculate.WirteLog(this._IsSend.ToString() + "侦听UDP端口号：" + j.ToString());
                return this.selfUDPPort;
            }
            catch
            { goto xx; }
        }
        #endregion

        #region TCP服务

        private void asyncTCPClient1_OnConnected(object sender,  CSS.IM.Library.Net.SockEventArgs  e)
        {
            //CSS.IM.Library.Calculate.WirteLog("已连接");
            CSS.IM.Library.Class.msgFile msg = new CSS.IM.Library.Class.msgFile((byte)CSS.IM.Library.Class.ProtocolFileTransmit.GetFileTransmitProxyID, -1, -1, 0, new byte[1]);
            this.asyncTCPClient1.SendData(msg.getBytes());//向服务器申请中转服务ID号
        }

        private void asyncTCPClient1_OnDataArrival(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            //if (e.Data.Length < 10) return;
            CSS.IM.Library.Class.msgFile msg = new CSS.IM.Library.Class.msgFile(e.Data);
            this.DataArrival(msg,CSS.IM.Library.Class.NatClass.Tcp ,null,0);
        }
         
        private void asyncTCPClient1_OnDisconnected(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            //CSS.IM.Library.Calculate.WirteLog("与服务器断开联接");
        }

        private void asyncTCPClient1_OnError(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            //CSS.IM.Library.Calculate.WirteLog("文件传输错误！"+ e.ErrorCode + e.ErrorMessage );
        }
        #endregion
        
        #region UDP数据到达事件
        /// <summary>
        /// UDP数据到达事件
        /// </summary>
        /// <param name="e">UDP数据参数</param>
        private void sockUDP1_DataArrival(object sender,CSS.IM.Library.Net.SockEventArgs  e)
        {
            //if (e.Data.Length < 10) return;
            CSS.IM.Library.Class.msgFile msg = new CSS.IM.Library.Class.msgFile(e.Data);
            this.DataArrival(msg,CSS.IM.Library.Class.NatClass.FullCone, e.IP,e.Port);
        }
        #endregion

        #region 数据到达 
        private void DataArrival(CSS.IM.Library.Class.msgFile msg, CSS.IM.Library.Class.NatClass netClass, System.Net.IPAddress Ip, int Port)
        {
            switch (msg.InfoClass)
            {
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.GetFileTransmitProxyID://获得自己从服务器上获得中转服务ID 
                    {
                        this.serverSelfID = msg.SendID;
                        if (this.serverOppositeID != -1)//如果是发送方获得中转服务ID，则告诉对方开始收接文件
                        {
                            this.netClass = CSS.IM.Library.Class.NetCommunicationClass.TCP;//标识当前通信协议为TCP
                            //this._mtu = 1200;//广域网传输文件时，将MTU值设置为1200使路由器与防火墙转发数据
                            msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.BeginTransmit;
                            msg.SendID = this.serverSelfID;
                            msg.RecID = this.serverOppositeID;
                            this.sendData(msg);
                        }
                        else if (this.getFileProxyID != null)//如果是接收方则触发获得中转服务ID告之对方
                            this.getFileProxyID(this, this.serverSelfID);
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.FileTransmit://获得文件传输请求
                    {
                        //Calculate.WirteLog("发送文件给对方");
                        this.sendFile((int)msg.pSendPos);//发送文件给对方
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.GetFileBlock :// 获得对方传输的文件数据包
                    {
                        //Calculate.WirteLog("收到文件数据");
                        this.ReceivedFileBlock(msg);//对方发送文件数据过来,保存数据到文件
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.BeginTransmit://连接已经建立，对方要求开始接收文件
                    {
                        this.serverOppositeID = msg.SendID;//获得对方ID
                        if (netClass == CSS.IM.Library.Class.NatClass.Tcp)//如果是TCP通信
                        {
                            this.netClass = CSS.IM.Library.Class.NetCommunicationClass.TCP;//采用TCP协议传输文件 
                        }
                        else
                        {
                            this.netClass = CSS.IM.Library.Class.NetCommunicationClass.WanNoProxyUDP;//采用UDP协议传输  
                        }

                        if (this.fileTransmitConnected != null)//触发通信成功事件，并退出通信测试
                            this.fileTransmitConnected(this, this.netClass);

                        if (!this.IsSendState)//如果文件还没有开始发送，则发送
                            this.sendRequestGetFileData();//发送文件给对方
                        //Calculate.WirteLog("连接已经建立，对方要求开始接收文件");
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.FileTranstmitOver ://文件传输结束
                    {
                        this.onFileTransmitted();
                    }
                    break;


                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.HandshakeLAN://收到对方局域网UDP握手数据
                    {
                        this.OppositeUDPIP = Ip;//重新设置对方的局域网IP
                        this.OppositeUDPPort = Port;//重新设置对方的局域网UDP端口
                        msg.InfoClass =(byte)CSS.IM.Library.Class.ProtocolFileTransmit.IsOppositeRecSelfLanUDPData;//告诉对方收到握手数据信息
                        this.sockUDP1.Send(this.OppositeUDPIP, this.OppositeUDPPort, msg.getBytes());
                        //Calculate.WirteLog(this._IsSend.ToString()+ "收到对方局域网UDP握手数据:"+ Ip.ToString() +":"+ Port.ToString());
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.IsOppositeRecSelfLanUDPData://对方收到自己发送的局域网UDP握手数据
                    {
                        this.netClass = CSS.IM.Library.Class.NetCommunicationClass.LanUDP;//标识与对方建立局域网通信成功
                       if (this._IsSend)//如果是发送文件的一方，且对方告之收到自己的握手数据，则通信通道打通
                       {
                           //this._mtu = 1400;//局域网传输文件时，将MTU值设置为5120即5k以提高速度
                           msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.BeginTransmit;
                           msg.SendID = 0;
                           this.sendData(msg);//告诉对方开始发送文件
                       } 
                       //Calculate.WirteLog(Ip.ToString() + ":" + Port.ToString() + "对方收到自己发送的局域网UDP握手数据" + this._IsSend.ToString());
                    }
                    break;

                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.HandshakeWAN ://收到对方广域网UDP握手数据
                    {
                        this.OppositeUDPIP = Ip;//重新设置对方的广域网IP
                        this.OppositeUDPPort = Port;//重新设置对方的广域网UDP端口
                        msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.IsOppositeRecSelfWanUDPData ;//告诉对方收到握手数据信息
                        this.sockUDP1.Send(this.OppositeUDPIP, this.OppositeUDPPort, msg.getBytes());
                        //Calculate.WirteLog(this._IsSend.ToString()+ "收到对方广域网UDP握手数据:"+ Ip.ToString() +":"+ Port.ToString());
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.IsOppositeRecSelfWanUDPData://对方收到自己发送的局域网UDP握手数据
                    {
                        this.netClass = CSS.IM.Library.Class.NetCommunicationClass.WanNoProxyUDP;//标识与对方建立广域网直接通信成功
                        if (this._IsSend)//如果是发送文件的一方，且对方告之收到自己的握手数据，则通信通道打通
                        {
                            //this._mtu = 1200;//局域网传输文件时，将MTU值设置为1200即1k速度
                            msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.BeginTransmit;
                            msg.SendID = 0;
                            this.sendData(msg);//告诉对方开始发送文件
                        }
                        //Calculate.WirteLog(Ip.ToString() + ":" + Port.ToString() + "对方收到自己发送的局域网UDP握手数据" + this._IsSend.ToString());
                    }
                    break;

                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.GetUDPWANInfo://获得服务器返回的文件传输套接字广域网UDP端口
                    {
                        this.selfUDPPort=msg.SendID;//重新设置对方的广域网UDP端口
                        if (!this.IsGetWanUDP && this.fileTransmitGetUDPPort != null)
                        {
                            this.IsGetWanUDP = true;//标识已经触发过WAN UDP端口获取事件 
                            this.fileTransmitGetUDPPort(this, this.selfUDPPort, true);
                        }
                        //Calculate.WirteLog(this._IsSend.ToString() + "获得服务器返回的文件传输套接字广域网UDP端口:" + this.selfUDPPort);
                    }
                    break;
            }
        }
        #endregion


        #region 设置对方文件传输UDP本地端口
        /// <summary>
        /// 设置对方文件传输UDP本地端口 
        /// </summary>
        /// <param name="Port">文件传输UDP本地端口</param>
        public void setFileTransmitGetUdpLocalPort(IPAddress ip,int Port, bool udpHandshakeInfoClass)
        {
            this.ServerIp = ip;
            this.OppositeUDPIP = ip;
            this.OppositeUDPPort = Port;//设置对方UDP端口号
            this.UdpHandshakeInfoClass = udpHandshakeInfoClass;

            System.Threading.Thread.Sleep(100);

            if (!udpHandshakeInfoClass)//如果自己还未UDP侦听,采用局域网方式通信
            {
                if (!this.sockUDP1.Listened)//如果没有侦听
                    this.UDPListen();//随机UDP侦听
                if (!this.IsGetLanUDP && this.fileTransmitGetUDPPort != null)//告之对方本地端口，产生获得端口事件
                {
                    this.IsGetLanUDP = true;//标识已经触发过LAN UDP事件
                    this.fileTransmitGetUDPPort(this, this.selfUDPPort, false);
                }
                //Calculate.WirteLog(this._IsSend.ToString() + "如果自己还未UDP侦听,采用局域网方式通信");
            }
            else if (udpHandshakeInfoClass)//采用广域网方式通信
            {
                if (!this.sockUDP1.Listened)//如果没有侦听
                    this.UDPListen();//随机UDP侦听
                CSS.IM.Library.Class.msgFile msg = new CSS.IM.Library.Class.msgFile();
                msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.GetUDPWANInfo;//通信协议
                this.sockUDP1.Send(ServerIp, _serverUDPPort, msg.getBytes());//获得文件传输套接字的广域网UDP端口
                //Calculate.WirteLog(this._IsSend.ToString() + "获得文件传输套接字的广域网UDP端口");
            }

            if (!timersUdpPenetrate.Enabled)//如果未握手，则开始握手
                timersUdpPenetrate.Enabled = true;//开始向对方UDP端口握手(打洞),如果成功，表示可以进行UDP通信
        }

        /// <summary>
        /// UDP握手有两种可能，第一种为局域网，记为false，第二种为广域网，记为true;
        /// </summary>
        private bool UdpHandshakeInfoClass = false ;//UDP握手有两种可能，第一种为局域网，记为false，第二种为广域网，记为true;

        #region timers发送握手数据
        int HandshakeCount = 0;//握手次数
        private void timersUdpPenetrate_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            HandshakeCount++;
            this.UdpHandshake();// 向对方UDP端口发送握手数据
            if (HandshakeCount == 10)//如果3秒钟，则停止打洞
            {
                timersUdpPenetrate.Enabled = false;//开始向对方UDP端口打洞
                HandshakeCount = 0;
            }
        }

        /// <summary>
        /// 向对方UDP端口发送握手数据
        /// </summary>
        private void UdpHandshake()
        {
            try
            {
                CSS.IM.Library.Class.msgFile msg = new CSS.IM.Library.Class.msgFile();
                if (!UdpHandshakeInfoClass)//如果为局域网握手
                {
                    msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.HandshakeLAN;
                    this.sockUDP1.Send(this.OppositeUDPIP, this.OppositeUDPPort, msg.getBytes());
                }
                else if (UdpHandshakeInfoClass)//如果为广域网握手
                {
                    msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.HandshakeWAN;
                    this.sockUDP1.Send(this.OppositeUDPIP, this.OppositeUDPPort, msg.getBytes());
                }
            }
            catch { }
        }
        #endregion

        #endregion

        #region 接收文件方法
        /// <summary>
        /// 开始接收文件
        /// </summary>
        /// <param name="savefullFileName">文件保存路径</param>
        public void startIncept(string savefullFileName)
        {
            this._fullFileName = savefullFileName;//标记接收的文件保存路径
            this.timerConnection.Enabled = true;//开始检测双方联接
        }

        /// <summary>
        /// 标识是否触发过LanUDP端口触发事件
        /// </summary>
        private bool IsGetLanUDP = false;
        /// <summary>
        ///  标识是否触发过WanUDP端口触发事件
        /// </summary>
        private bool IsGetWanUDP = false;
        /// <summary>
        /// 超时次数
        /// </summary>
        private byte TimeOutCount = 0; 


        /// <summary>
        /// 检测双方联接的过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerConnection_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.netClass != CSS.IM.Library.Class.NetCommunicationClass.None)//如果UDP通信成功
            {
                this.timerConnection.Enabled = false;//停止通信状态检测
                if (this.fileTransmitConnected != null)//触发通信成功事件，并退出通信测试
                    this.fileTransmitConnected(this, this.netClass);
                return;
            }

            TimeOutCount++;

            if (TimeOutCount == 1) ///假设双方均在同一局域网内，则采用P2P UDP方式收发数据
            {
                this.UdpHandshakeInfoClass = false;//UDP握手有两种可能，第一种为局域网，记为false，第二种为广域网，记为true;此时标记为局域网false
                if (!this.sockUDP1.Listened)//如果没有侦听，则侦听
                    this.UDPListen();
                if (!this.IsGetLanUDP && this.fileTransmitGetUDPPort != null)//触发UDP端口侦听成功事件，以便对方获知自己的UDP端口
                {
                    this.IsGetLanUDP = true;//标识已经触发过LanUDP端口触发事件
                    this.fileTransmitGetUDPPort(this, this.selfUDPPort, this.UdpHandshakeInfoClass);
                }
            }
        }

        #endregion
    }
    #endregion

    #region  P2P文件传输集合
    /// <summary>
    /// 在线用户集合。
    /// </summary>
    [Serializable]
    public class p2pFileTransmitCollectionsEX : System.Collections.CollectionBase,IDisposable
    {
        public p2pFileTransmitCollectionsEX()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        // Get UserInfo at the specified index
        public p2pFileTransmitEX this[int index]
        {
            get
            {
                return ((p2pFileTransmitEX)InnerList[index]);
            }
        }

 
        public void add(p2pFileTransmitEX _p2pFileTransmit)
        {
            base.InnerList.Add(_p2pFileTransmit);
        }
       
        public void Romove(p2pFileTransmitEX _p2pFileTransmit)
        {
            base.InnerList.Remove(_p2pFileTransmit);
        }

        public p2pFileTransmitEX find(string FileMD5)
        {
            foreach (p2pFileTransmitEX _p2pFileTransmit in this)
                if (_p2pFileTransmit.FileMD5Value == FileMD5)
                    return _p2pFileTransmit;
            return null;
        }


        public void Dispose()
        {
            foreach (p2pFileTransmitEX _p2pFileTransmit in this)
                _p2pFileTransmit.Dispose();
        }
    }
    #endregion     
}
