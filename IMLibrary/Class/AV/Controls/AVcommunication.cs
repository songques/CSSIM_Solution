using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;

namespace IMLibrary.AV
{
    public partial class AVcommunication : Component
    {
        #region 组件初始化
        public AVcommunication()
        {
            InitializeComponent();
        }

        public AVcommunication(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #endregion

        #region 私有变量
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

        private IMLibrary.Class.NetCommunicationClass netClass = IMLibrary.Class.NetCommunicationClass.None;//标识没有建立通信
        private bool UdpHandshakeInfoClass = false;//UDP握手有两种可能，第一种为局域网，记为false，第二种为广域网，记为true;

        private IMLibrary.Class.UserInfo _OppositeUserInfo;//对方用户在线信息
        private IMLibrary.Class.UserInfo _selfUserInfo;//自己的用户在线信息

        private int selfUDPPort=-1;//自己的UDP侦听端口
        private int OppositeUDPPort = -1;//对方UDP端口

        private System.Net.IPAddress _serverIp;//视频服务器IP地址

        private int _serverUDPPort = -1;//视频服务器UDP服务端口
        private int _serverTCPPort = -1;//视频服务器TCP服务端口

        private int serverSelfID = -1;//自己在视频服务器上的中转ID
        private int serverOppositeID = -1;//对方在视频服务器上的中转ID
        #endregion

        #region  AV事件
        /// <summary>
        /// 取消音视频对话事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="isSelf"></param>
        public delegate void AVCancelEventHandler(object sender, bool isSelf); 
        /// <summary>
        /// 取消音视频对话事件
        /// </summary>
        public event AVCancelEventHandler AVCancel;
        /// <summary>
        /// 获得服务ID事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="proxyID"></param>
        public delegate void AVGetProxyIDEventHandler(object sender, int proxyID); 
        /// <summary>
        /// 获得服务ID事件
        /// </summary>
        public event AVGetProxyIDEventHandler AVGetProxyID;

        /// <summary>
        /// 联接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="netCommuctionClass"></param>
        public delegate void AVConnectedEventHandler(object sender, IMLibrary.Class.NetCommunicationClass netCommuctionClass); 
        /// <summary>
        /// 联接事件
        /// </summary>
        public event AVConnectedEventHandler AVConnected;

        /// <summary>
        /// 获得UDP端口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Port"></param>
        /// <param name="udpHandshakeInfoClass">真是是局域网端口，假是广域网端口</param>
        public delegate void AVGetUDPPortEventHandler(object sender, int Port, bool udpHandshakeInfoClass);  
        /// <summary>
        /// 获得UDP端口事件
        /// </summary>
        public event AVGetUDPPortEventHandler AVGetUDPPort;
         
        #endregion

        #region 关闭通信
        /// <summary>
        /// 关闭通信
        /// </summary>
        public void closeCommunication()
        {
            if (this.sockUDP1.Listened)
                this.sockUDP1.CloseSock();
            this.sockUDP1.Dispose();
            this.sockUDP1 = null;

            this.TCPClient1.Disconnect();
            this.TCPClient1.Dispose();
            this.TCPClient1 = null;
        }
        #endregion

        #region 接收请求并开始联接
        /// <summary>
        /// 接收请求并开始联接
        /// </summary>
        public void startIncept()
        {
            this.timerConnection.Enabled = true;//开始检测双方联接
        }

        private void timerConnection_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.netClass != IMLibrary.Class.NetCommunicationClass.None)//如果UDP通信成功
            {
                this.timerConnection.Enabled = false;//停止通信状态检测
                if (this.AVConnected != null)//触发通信成功事件，并退出通信测试
                    this.AVConnected(this, this.netClass);
                return;
            }

            TimeOutCount++;

            if (TimeOutCount == 1 ) ///假设双方均在同一局域网内，则采用P2P UDP方式收发数据
            {
                this.UdpHandshakeInfoClass = false;//UDP握手有两种可能，第一种为局域网，记为false，第二种为广域网，记为true;此时标记为局域网false
                if (!this.sockUDP1.Listened)//如果没有侦听，则侦听
                    this.UDPListen();
                if (!this.IsGetLanUDP && this.AVGetUDPPort != null)//如果未与对方建立通信，触发UDP端口侦听成功事件，告之对方自己的UDP端口
                {
                    this.IsGetLanUDP = true;
                    this.AVGetUDPPort(this, this.selfUDPPort, this.UdpHandshakeInfoClass);
                }
            }

            ///程序执行到这里表示双方假设为局域网通信不成立
            ///2秒开始测试广域网UDP通信
            if (TimeOutCount == 20 )
                if (this._selfUserInfo.NetClass < (byte)IMLibrary.Class.NatClass.Tcp && this._OppositeUserInfo.NetClass < (byte)IMLibrary.Class.NatClass.Tcp)
                {
                    //如果双方均在广域网上，采用UDP通信，且双方都不是 Symmetric NAT，则采用广域网UDP P2P通信
                    this.UdpHandshakeInfoClass = true;//UDP握手有两种可能，第一种为局域网，记为false，第二种为广域网，记为true;此时标记为广域网
                    if (!this.sockUDP1.Listened)//如果没有侦听，则侦听
                        this.UDPListen();

                    IMLibrary.Class.msgAV msg = new IMLibrary.Class.msgAV();
                    msg.InfoClass = (byte)IMLibrary.Class.ProtocolAVTransmit.GetUDPWANInfo;//通信协议，获得UDP套接字的广域网UDP端口
                    this.sockUDP1.Send(_serverIp, _serverUDPPort, msg.getBytes());
                    //Calculate.WirteLog("测试采用广域网方式通信");
                }
                else
                {
                    TimeOutCount = 40;//如果只能通过代理传输，则开始使用代理
                }

            //4秒后开始测试广域网
            if (TimeOutCount == 40) 
            {
                //如果用户自己与对方其中一方使用TCP登录，则需要使用TCP代理服务器中转数据传输
                this.TCPClient1.InitSocket(this._selfUserInfo.LocalIP, 0);//邦定本机TCP随机端口
                this.TCPClient1.Connect(this._serverIp, _serverTCPPort);//TCP检测联接服务器
            }

            ///程序执行到此表示不能建立任何传输联接，触发无法联接事件 
            //7秒后开始测试广域网
            if (TimeOutCount > 70)
            {
                if (this.AVConnected != null)//触发通信成功事件，并退出通信测试
                    this.AVConnected(this, this.netClass);
                this.timerConnection.Enabled = false;//停止通信状态检测
                this.CancelTransmit(true);//取消传输
                return;
            }
        }
        #endregion

        #region 设置传输音视频参数
        /// <summary>
        /// 设置传输音视频参数
        /// </summary>
        /// <param name="ServerIP">服务器IP地址</param>
        /// <param name="ServerUDPPort">服务器UDP服务端口</param>
        /// <param name="ServerTCPPort">服务器TCP服务端口</param>
        /// <param name="selfUserInfo">自己的在线用户信息</param>
        /// <param name="OppositeUserInfo">对方的在线用户信息</param>
        public void SetParameter(System.Net.IPAddress ServerIP, int ServerUDPPort, int ServerTCPPort, IMLibrary.Class.UserInfo selfUserInfo, IMLibrary.Class.UserInfo OppositeUserInfo)
        {
            //音视频传输前建立双方连接的参数设置函数
            this._serverIp = ServerIP;//获取服务器IP地址
            this._serverUDPPort = ServerUDPPort;//获取文件服务器UDP服务端口
            this._serverTCPPort = ServerTCPPort;//获取文件服务器TCP服务端口
            this._selfUserInfo = selfUserInfo;//获取自己的在线用户信息
            this._OppositeUserInfo = OppositeUserInfo;//获取对方的在线用户信息
        }
        #endregion

        #region 设置对方传输UDP本地端口
        /// <summary>
        /// 设置对方传输UDP本地端口 
        /// </summary>
        /// <param name="Port">传输UDP本地端口</param>
        public void setGetUdpLocalPort(int Port, bool udpHandshakeInfoClass)
        {
            this.OppositeUDPPort = Port;//设置对方UDP端口号
            this.UdpHandshakeInfoClass = udpHandshakeInfoClass;//握手方式

            System.Threading.Thread.Sleep(100);

            if (!udpHandshakeInfoClass)//如果自己还未UDP侦听,采用局域网方式通信
            {
                if (!this.sockUDP1.Listened)//如果没有侦听
                    this.UDPListen();//随机UDP侦听
                if (!this.IsGetLanUDP && this.AVGetUDPPort != null)//如果未与对方建立通信,则产生获得端口事件
                {
                    this.IsGetLanUDP = true;
                    this.AVGetUDPPort(this, this.selfUDPPort, false);
                }
                 //Calculate.WirteLog( "对方要求采用局域网方式通信");
            }
            else if (udpHandshakeInfoClass)//采用广域网方式通信
            {
                if (!this.sockUDP1.Listened)//如果没有侦听
                    this.UDPListen();//随机UDP侦听
                IMLibrary.Class.msgAV msg = new IMLibrary.Class.msgAV();
                msg.InfoClass = (byte)IMLibrary.Class.ProtocolAVTransmit.GetUDPWANInfo;//通信协议
                this.sockUDP1.Send(_serverIp, _serverUDPPort, msg.getBytes());//获得文件传输套接字的广域网UDP端口
                //Calculate.WirteLog("对方要求采用广域网方式通信");
            }

            if (!timersUdpPenetrate.Enabled)//如果未握手，则开始握手
                timersUdpPenetrate.Enabled = true;//开始向对方UDP端口握手(打洞),如果成功，表示可以进行UDP通信
        }
         
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
                IMLibrary.Class.msgAV msg = new IMLibrary.Class.msgAV();
                if (!UdpHandshakeInfoClass)//如果为局域网握手
                {
                    msg.InfoClass = (byte)IMLibrary.Class.ProtocolAVTransmit.HandshakeLAN;
                    this.sockUDP1.Send(this._OppositeUserInfo.LocalIP, this.OppositeUDPPort, msg.getBytes());
                }
                else if (UdpHandshakeInfoClass)//如果为广域网握手
                {
                    msg.InfoClass = (byte)IMLibrary.Class.ProtocolAVTransmit.HandshakeWAN;
                    this.sockUDP1.Send(this._OppositeUserInfo.IP, this.OppositeUDPPort, msg.getBytes());
                }
            }
            catch { }
        }
        #endregion         

        #region 设置对方中转服务ID
        /// <summary>
        /// 设置对方中转服务ID 
        /// </summary>
        /// <param name="OppositeID">ID号</param>
        public void setAVGetOppositeID(int OppositeID)
        {
            this.serverOppositeID = OppositeID;//设置对方代理ID

            if (this.serverSelfID == -1)//如果自己还未获取代理ID，则向服务器获取
            {
                this.TCPClient1.InitSocket(System.Net.IPAddress.Parse("127.0.0.1"), 0);//邦定本机TCP随机端口
                this.TCPClient1.Connect(this._serverIp, _serverTCPPort);//TCP检测联接服务器
            }
        }
        #endregion

        #region 取消音视频
        /// <summary>
        /// 取消音视频
        /// </summary>
        public void CancelTransmit(bool isMe)
        {

            try
            {
                if (this.netClass != IMLibrary.Class.NetCommunicationClass.TCP)
                {
                    this.sockUDP1.CloseSock();//关闭sockUDP1端口，清楚占用的资源 
                    this.sockUDP1 = null;
                }
                {
                    this.TCPClient1.Disconnect();
                    this.TCPClient1.Dispose();
                    this.TCPClient1 = null;
                }
            }
            catch { }

            if (this.AVCancel != null)
                this.AVCancel(this, isMe);//触发“取消事件”(自己取消的)
        }
        #endregion

        #region TCP服务
        private void TCPClient1_OnConnected(object sender, IMLibrary.Net.SockEventArgs e)
        {
            //IMLibrary.Calculate.WirteLog("已连接");
            IMLibrary.Class.msgAV msg = new IMLibrary.Class.msgAV();
            msg.InfoClass = (byte)IMLibrary.Class.ProtocolAVTransmit.GetAVTransmitProxyID;
            this.TCPClient1.SendData(msg.getBytes());//向服务器申请中转服务ID号
        }

        private void TCPClient1_OnDataArrival(object sender, IMLibrary.Net.SockEventArgs e)
        {
            //if (e.Data.Length < 10) return;
            IMLibrary.Class.msgAV msg = new IMLibrary.Class.msgAV(e.Data);
            this.DataArrival(msg, IMLibrary.Class.NatClass.Tcp, null, 0);
        }
        #endregion

        #region UDP服务

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
                return this.selfUDPPort;
            }
            catch
            { goto xx; }
        }


        private void sockUDP1_DataArrival(object sender, IMLibrary.Net.SockEventArgs e)
        {
            //if (e.Data.Length < 10) return;
            IMLibrary.Class.msgAV msg = new IMLibrary.Class.msgAV(e.Data);
            this.DataArrival(msg, IMLibrary.Class.NatClass.FullCone, e.IP, e.Port);
        }
        #endregion

        #region 数据到达
        private void DataArrival(IMLibrary.Class.msgAV msg, IMLibrary.Class.NatClass netClass, System.Net.IPAddress Ip, int Port)
        {
            switch (msg.InfoClass)
            {
                case (byte)IMLibrary.Class.ProtocolAVTransmit.GetVideoData:// 获得对方传输的视频数据包
                    {
                        this.OnVideoData(msg.DataBlock);
                        //Calculate.WirteLog("收到v"+ msg.DataBlock.Length.ToString());
                        //this.ReceivedFileBlock(msg);//对方发送文件数据过来,保存数据到文件
                    }
                    break;
                case (byte)IMLibrary.Class.ProtocolAVTransmit.GetAudioData:// 获得对方传输的音频数据包
                    {
                        this.OnAudioData(msg.DataBlock);
                        //Calculate.WirteLog("收到a"+ msg.DataBlock.Length.ToString());
                        //this.ReceivedFileBlock(msg);//对方发送文件数据过来,保存数据到文件
                    }
                    break;
                case (byte)IMLibrary.Class.ProtocolAVTransmit.GetBITMAPINFOHEADER://获得对方视频图像头信息
                    {
                        this.OnGetBITMAPINFOHEADER(msg.DataBlock);
                        break;
                    }
                case (byte)IMLibrary.Class.ProtocolAVTransmit.GetAVTransmitProxyID://获得自己从服务器上获得中转服务ID 
                    {
                        this.serverSelfID = msg.SendID;
                        if (this.serverOppositeID != -1)//如果已经获得对方中转服务ID,则告诉对方开始视频传输
                        {
                            this.netClass = IMLibrary.Class.NetCommunicationClass.TCP;//采用TCP协议传输文件 
                            msg.InfoClass = (byte)IMLibrary.Class.ProtocolAVTransmit.BeginTransmit;
                            msg.SendID = this.serverSelfID;
                            msg.RecID = this.serverOppositeID;
                            this.sendData(msg);
                        }
                        else if (this.AVGetProxyID != null)//如果对方中转服务ID没有获得，则产生事件通知对方自己的ID//如果是接收方则触发获得中转服务ID
                            this.AVGetProxyID(this, this.serverSelfID);
                    }
                    break;

                case (byte)IMLibrary.Class.ProtocolAVTransmit.BeginTransmit://连接已经建立，对方要求开始接收文件
                    {
                        this.serverOppositeID = msg.SendID;//获得对方ID
                        if (netClass == IMLibrary.Class.NatClass.Tcp)//如果是TCP通信
                        {
                            this.netClass = IMLibrary.Class.NetCommunicationClass.TCP;//采用TCP协议传输文件 
                        }
                        else
                        {
                            this.netClass = IMLibrary.Class.NetCommunicationClass.WanNoProxyUDP;//采用UDP协议传输  
                        }
                        if (this.AVConnected != null)//触发通信成功事件，并退出通信测试
                            this.AVConnected(this, this.netClass);

                        //通信已经建立，开始传输数据
                        //Calculate.WirteLog("连接已经建立");
                    }
                    break;

                case (byte)IMLibrary.Class.ProtocolAVTransmit.HandshakeLAN://收到对方局域网UDP握手数据
                    {
                        this._OppositeUserInfo.LocalIP = Ip;//重新设置对方的局域网IP
                        this.OppositeUDPPort = Port;//重新设置对方的局域网UDP端口
                        msg.InfoClass =(byte)IMLibrary.Class.ProtocolAVTransmit.IsOppositeRecSelfLanUDPData;//告诉对方收到握手数据信息
                        this.sockUDP1.Send(this._OppositeUserInfo.LocalIP, this.OppositeUDPPort, msg.getBytes());
                        //Calculate.WirteLog(this._IsSend.ToString()+ "收到对方局域网UDP握手数据:"+ Ip.ToString() +":"+ Port.ToString());
                    }
                    break;
                case (byte)IMLibrary.Class.ProtocolAVTransmit.IsOppositeRecSelfLanUDPData://对方收到自己发送的局域网UDP握手数据
                    {
                        this.netClass = IMLibrary.Class.NetCommunicationClass.LanUDP;//标识与对方建立局域网通信成功
                        msg.InfoClass = (byte)IMLibrary.Class.ProtocolAVTransmit.BeginTransmit;
                       msg.SendID = 0;
                       this.sendData(msg);//告诉对方开始发送文件
                       //Lan通信已经建立，开始传输数据
                    }
                    break;

                case (byte)IMLibrary.Class.ProtocolAVTransmit.HandshakeWAN://收到对方广域网UDP握手数据
                    {
                        this._OppositeUserInfo.IP = Ip;//重新设置对方的广域网IP
                        this.OppositeUDPPort = Port;//重新设置对方的广域网UDP端口
                        msg.InfoClass = (byte)IMLibrary.Class.ProtocolAVTransmit.IsOppositeRecSelfWanUDPData;//告诉对方收到握手数据信息
                        this.sockUDP1.Send(this._OppositeUserInfo.IP, this.OppositeUDPPort, msg.getBytes());
                        //Calculate.WirteLog(this._IsSend.ToString()+ "收到对方广域网UDP握手数据:"+ Ip.ToString() +":"+ Port.ToString());
                    }
                    break;
                case (byte)IMLibrary.Class.ProtocolAVTransmit.IsOppositeRecSelfWanUDPData://对方收到自己发送的局域网UDP握手数据
                    {
                        this.netClass = IMLibrary.Class.NetCommunicationClass.WanNoProxyUDP;//标识与对方建立广域网直接通信成功

                        msg.InfoClass = (byte)IMLibrary.Class.ProtocolAVTransmit.BeginTransmit;
                            msg.SendID = 0;
                            this.sendData(msg);//告诉对方开始视频
                        //Wan通信已经建立，开始传输数据
                    }
                    break;

                case (byte)IMLibrary.Class.ProtocolAVTransmit.GetUDPWANInfo://获得服务器返回的文件传输套接字广域网UDP端口
                    {
                        this.selfUDPPort=msg.SendID;//重新设置对方的广域网UDP端口
                        if (!this.IsGetWanUDP && this.AVGetUDPPort != null)//如果自己还未与对方套接字建立通信则触发获得端口事件
                        {
                            this.IsGetWanUDP = true;
                            this.AVGetUDPPort(this, this.selfUDPPort, true);
                        }
                    }
                    break;
            }
        }
        #endregion

        #region winSock 发送数据
        /// <summary>
        /// sockUDP 发送文件数据
        /// </summary>
        /// <param name="fInfo">文件信息</param>
        private void sendData(IMLibrary.Class.msgAV msg)
        {
            try
            {
                if (this.netClass == IMLibrary.Class.NetCommunicationClass.LanUDP)//如果是局域网通信
                    this.sockUDP1.Send(this._OppositeUserInfo.LocalIP, this.OppositeUDPPort, msg.getBytes());//采用UDP发送数据到对方局域网IP与端口
                else if (this.netClass == IMLibrary.Class.NetCommunicationClass.WanNoProxyUDP)//如果是广域网直接通信
                    this.sockUDP1.Send(this._OppositeUserInfo.IP, this.OppositeUDPPort, msg.getBytes());//采用UDP发送数据到对方广域网IP与端口
                else if (this.netClass == IMLibrary.Class.NetCommunicationClass.WanProxyUDP)//如果是广域网服务器中转通信
                    this.sockUDP1.Send(this._serverIp, this._serverUDPPort, msg.getBytes());//采用UDP发送数据到服务器中转IP与端口
                else if (this.netClass == IMLibrary.Class.NetCommunicationClass.TCP)
                    this.TCPClient1.SendData(msg.getBytes());//采用TCP发送数据
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
         
        #region 音视频操作及事件
        /// <summary>
        /// 音频数据到达事件
        /// </summary>
        public event AVEventHandler AudioData;
        /// <summary>
        /// 视频数据到达事件
        /// </summary>
        public event AVEventHandler VideoData;
        /// <summary>
        /// 获得对方视频图像头信息事件
        /// </summary>
        public event AVEventHandler GetBITMAPINFOHEADER;

        /// <summary>
        /// 音频到达事件
        /// </summary>
        /// <param name="data"></param>
        private void OnAudioData(byte[]  data)//
        {
            if (this.AudioData != null)
                this.AudioData(this, new AVEventArgs(data));
        }

        /// <summary>
        /// 视频到达事件
        /// </summary>
        /// <param name="data"></param>
        private void OnVideoData(byte[] data)//
        {
            if (this.VideoData != null)
                this.VideoData(this, new AVEventArgs(data));
        }

        /// <summary>
        /// 获得对方视频图像头信息事件
        /// </summary>
        /// <param name="data"></param>
        private void OnGetBITMAPINFOHEADER(byte[] data)//
        {
            BITMAPINFOHEADER bim = new BITMAPINFOHEADER();

            IMLibrary.Class.BitmapInfoHeader bit = new IMLibrary.Class.BitmapInfoHeader(data);
            bim.biBitCount = bit.biBitCount;
            bim.biClrImportant = bit.biClrImportant;
            bim.biClrUsed = bit.biClrUsed;
            bim.biCompression = bit.biCompression;
            bim.biHeight = bit.biHeight;
            bim.biPlanes = bit.biPlanes;
            bim.biSize = bit.biSize;
            bim.biSizeImage = bit.biSizeImage;
            bim.biWidth = bit.biWidth;
            bim.biXPelsPerMeter = bit.biXPelsPerMeter;
            bim.biYPelsPerMeter = bit.biYPelsPerMeter;

            if (this.GetBITMAPINFOHEADER != null)
                this.GetBITMAPINFOHEADER(this, new AVEventArgs(bim));
        }


        /// <summary>
        /// 发送音频数据到对方
        /// </summary>
        /// <param name="data">音频数据</param>
        public void SendAudio(byte[] data)//
        {
            IMLibrary.Class.msgAV msg = new IMLibrary.Class.msgAV();
            msg.InfoClass = (byte)IMLibrary.Class.ProtocolAVTransmit.GetAudioData;
            msg.DataBlock = data ;
            this.sendData(msg);
        }

        /// <summary>
        /// 发送视频数据到对方
        /// </summary>
        /// <param name="data">视频数据</param>
        public void SendVideo(byte[] data)
        {
            IMLibrary.Class.msgAV msg = new  IMLibrary.Class.msgAV();
            msg.InfoClass = (byte)IMLibrary.Class.ProtocolAVTransmit.GetVideoData;
            msg.DataBlock = data ;
            this.sendData(msg);
        }

        /// <summary>
        /// 发送视频图像头信息到对方
        /// </summary>
        /// <param name="BITMAPINFOHEADER">视频图像头信息</param>
        public void SendBITMAPINFOHEADER(BITMAPINFOHEADER BITMAPINFOHEADER)
        {
            IMLibrary.Class.BitmapInfoHeader bitmapInfoHeader = new IMLibrary.Class.BitmapInfoHeader();
            bitmapInfoHeader.biBitCount = BITMAPINFOHEADER.biBitCount;
            bitmapInfoHeader.biClrImportant = BITMAPINFOHEADER.biClrImportant;
            bitmapInfoHeader.biClrUsed = BITMAPINFOHEADER.biClrUsed;
            bitmapInfoHeader.biCompression = BITMAPINFOHEADER.biCompression;
            bitmapInfoHeader.biHeight = BITMAPINFOHEADER.biHeight;
            bitmapInfoHeader.biPlanes = BITMAPINFOHEADER.biPlanes;
            bitmapInfoHeader.biSize = BITMAPINFOHEADER.biSize;
            bitmapInfoHeader.biSizeImage = BITMAPINFOHEADER.biSizeImage;
            bitmapInfoHeader.biWidth = BITMAPINFOHEADER.biWidth;
            bitmapInfoHeader.biXPelsPerMeter = BITMAPINFOHEADER.biXPelsPerMeter;
            bitmapInfoHeader.biYPelsPerMeter = BITMAPINFOHEADER.biYPelsPerMeter;

            IMLibrary.Class.msgAV msg = new IMLibrary.Class.msgAV();
            msg.InfoClass = (byte)IMLibrary.Class.ProtocolAVTransmit.GetBITMAPINFOHEADER;
            msg.DataBlock = bitmapInfoHeader.getBytes();
            this.sendData(msg);
        }

        #region AV事件委托
        /// <summary>
        /// AV事件参数
        /// </summary>
        /// <param name="sender">产生事件的对像</param>
        /// <param name="e">事件参数</param>
        public delegate void AVEventHandler(object sender, AVEventArgs e);
        /// <summary>
        /// AV事件参数
        /// </summary>
        public class AVEventArgs : System.EventArgs
        {
            /// <summary>
            /// 视频图像头信息
            /// </summary>
            public BITMAPINFOHEADER BITMAPINFOHEADER;
            /// <summary>
            /// 音视频数据
            /// </summary>
            public byte[] Data;
            /// <summary>
            /// 初始化
            /// </summary>
            /// <param name="data"></param>
            public AVEventArgs(byte[] data)
            {
                this.Data = data;
            }
            /// <summary>
            /// 初始化
            /// </summary>
            /// <param name="BITMAPINFOHEADER"></param>
             public AVEventArgs(BITMAPINFOHEADER BITMAPINFOHEADER)
            {
                this.BITMAPINFOHEADER = BITMAPINFOHEADER;
            }
        }
        #endregion

        #endregion
    }

}