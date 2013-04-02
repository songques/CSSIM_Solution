using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace CSS.IM.Library.AV.Controls
{
    public partial class AVcommunicationEx : Component
    {
        public BITMAPINFOHEADER bitmapInfoHeader;

        public int selfUDPPort{get;set;}//自己的UDP侦听端口
        //private int OppositeUDPPort = -1;//对方UDP端口
        public CSS.IM.Library.Class.UserInfo _OppositeUserInfo;//对方用户在线信息
        public CSS.IM.Library.Class.UserInfo _selfUserInfo;//自己的用户在线信息
        public int OppositeUDPPort = -1;//对方UDP端口

        #region 组件初使化

        public AVcommunicationEx(IContainer container)
        {
            selfUDPPort = -1;
            container.Add(this);
            InitializeComponent();
        }
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
        //public event AVCancelEventHandler AVCancel;
        /// <summary>
        /// 获得服务ID事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="proxyID"></param>
        public delegate void AVGetProxyIDEventHandler(object sender, int proxyID);
        /// <summary>
        /// 获得服务ID事件
        /// </summary>
        //public event AVGetProxyIDEventHandler AVGetProxyID;

        /// <summary>
        /// 联接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="netCommuctionClass"></param>
        public delegate void AVConnectedEventHandler(object sender, CSS.IM.Library.Class.NetCommunicationClass netCommuctionClass);
        /// <summary>
        /// 联接事件
        /// </summary>
        //public event AVConnectedEventHandler AVConnected;

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
        //public event AVGetUDPPortEventHandler AVGetUDPPort;

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

            //this.TCPClient1.Disconnect();
            //this.TCPClient1.Dispose();
            //this.TCPClient1 = null;
        }
        #endregion

        #region UDP服务
        /// <summary>
        /// 开始侦听来自外部的消息
        /// </summary>
        public int UDPListen()//UDP开始侦听来自外部的消息.
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

        private void sockUDP1_DataArrival(object sender, Net.SockEventArgs e)
        {
            //if (e.Data.Length < 10) return;
            CSS.IM.Library.Class.msgAV msg = new CSS.IM.Library.Class.msgAV(e.Data);
            this.DataArrival(msg, CSS.IM.Library.Class.NatClass.FullCone, e.IP, e.Port);
        }
        #endregion

        #region 数据到达
        private void DataArrival(CSS.IM.Library.Class.msgAV msg, CSS.IM.Library.Class.NatClass netClass, System.Net.IPAddress Ip, int Port)
        {
            switch (msg.InfoClass)
            {
                case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.GetVideoData:// 获得对方传输的视频数据包
                    {
                        this.OnVideoData(msg.DataBlock);
                        //Calculate.WirteLog("收到v"+ msg.DataBlock.Length.ToString());
                        //this.ReceivedFileBlock(msg);//对方发送文件数据过来,保存数据到文件
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.GetAudioData:// 获得对方传输的音频数据包
                    {
                        this.OnAudioData(msg.DataBlock);
                        //Calculate.WirteLog("收到a"+ msg.DataBlock.Length.ToString());
                        //this.ReceivedFileBlock(msg);//对方发送文件数据过来,保存数据到文件
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.GetBITMAPINFOHEADER://获得对方视频图像头信息
                    {
                        //this.OnGetBITMAPINFOHEADER(msg.DataBlock);
                        break;
                    }
                //case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.GetAVTransmitProxyID://获得自己从服务器上获得中转服务ID 
                //    {
                //        this.serverSelfID = msg.SendID;
                //        if (this.serverOppositeID != -1)//如果已经获得对方中转服务ID,则告诉对方开始视频传输
                //        {
                //            this.netClass = CSS.IM.Library.Class.NetCommunicationClass.TCP;//采用TCP协议传输文件 
                //            msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolAVTransmit.BeginTransmit;
                //            msg.SendID = this.serverSelfID;
                //            msg.RecID = this.serverOppositeID;
                //            this.sendData(msg);
                //        }
                //        else if (this.AVGetProxyID != null)//如果对方中转服务ID没有获得，则产生事件通知对方自己的ID//如果是接收方则触发获得中转服务ID
                //            this.AVGetProxyID(this, this.serverSelfID);
                //    }
                //    break;

                //case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.BeginTransmit://连接已经建立，对方要求开始接收文件
                //    {
                //        this.serverOppositeID = msg.SendID;//获得对方ID
                //        if (netClass == CSS.IM.Library.Class.NatClass.Tcp)//如果是TCP通信
                //        {
                //            this.netClass = CSS.IM.Library.Class.NetCommunicationClass.TCP;//采用TCP协议传输文件 
                //        }
                //        else
                //        {
                //            this.netClass = CSS.IM.Library.Class.NetCommunicationClass.WanNoProxyUDP;//采用UDP协议传输  
                //        }
                //        if (this.AVConnected != null)//触发通信成功事件，并退出通信测试
                //            this.AVConnected(this, this.netClass);

                //        //通信已经建立，开始传输数据
                //        //Calculate.WirteLog("连接已经建立");
                //    }
                //    break;

                //case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.HandshakeLAN://收到对方局域网UDP握手数据
                //    {
                //        this._OppositeUserInfo.LocalIP = Ip;//重新设置对方的局域网IP
                //        this.OppositeUDPPort = Port;//重新设置对方的局域网UDP端口
                //        msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolAVTransmit.IsOppositeRecSelfLanUDPData;//告诉对方收到握手数据信息
                //        this.sockUDP1.Send(this._OppositeUserInfo.LocalIP, this.OppositeUDPPort, msg.getBytes());
                //        //Calculate.WirteLog(this._IsSend.ToString()+ "收到对方局域网UDP握手数据:"+ Ip.ToString() +":"+ Port.ToString());
                //    }
                //    break;
                //case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.IsOppositeRecSelfLanUDPData://对方收到自己发送的局域网UDP握手数据
                //    {
                //        this.netClass = CSS.IM.Library.Class.NetCommunicationClass.LanUDP;//标识与对方建立局域网通信成功
      
                //case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.HandshakeWAN://收到对方广域网UDP握手数据
                //    {
                //        this._OppositeUserInfo.IP = Ip;//重新设置对方的广域网IP
                //        this.OppositeUDPPort = Port;//重新设置对方的广域网UDP端口
                //        msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolAVTransmit.IsOppositeRecSelfWanUDPData;//告诉对方收到握手数据信息
                //        this.sockUDP1.Send(this._OppositeUserInfo.IP, this.OppositeUDPPort, msg.getBytes());
                //        //Calculate.WirteLog(this._IsSend.ToString()+ "收到对方广域网UDP握手数据:"+ Ip.ToString() +":"+ Port.ToString());
                //    }
                //    break;
                //case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.IsOppositeRecSelfWanUDPData://对方收到自己发送的局域网UDP握手数据
                //    {
                //        this.netClass = CSS.IM.Library.Class.NetCommunicationClass.WanNoProxyUDP;//标识与对方建立广域网直接通信成功

                //        msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolAVTransmit.BeginTransmit;
                //        msg.SendID = 0;
                //        this.sendData(msg);//告诉对方开始视频
                //        //Wan通信已经建立，开始传输数据
                //    }
                //    break;

                //case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.GetUDPWANInfo://获得服务器返回的文件传输套接字广域网UDP端口
                //    {
                //        this.selfUDPPort = msg.SendID;//重新设置对方的广域网UDP端口
                //        if (!this.IsGetWanUDP && this.AVGetUDPPort != null)//如果自己还未与对方套接字建立通信则触发获得端口事件
                //        {
                //            this.IsGetWanUDP = true;
                //            this.AVGetUDPPort(this, this.selfUDPPort, true);
                //        }
                //    }
                //    break;                  msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolAVTransmit.BeginTransmit;
                //        msg.SendID = 0;
                //        this.sendData(msg);//告诉对方开始发送文件
                //        //Lan通信已经建立，开始传输数据
                //    }
            }
        }
        #endregion

        #region 音视频操作及事件
        /// <summary>
        /// 音频数据到达事件
        /// </summary>
        public event CSS.IM.Library.AV.AVcommunication.AVEventHandler AudioData;
        /// <summary>
        /// 视频数据到达事件
        /// </summary>
        public event CSS.IM.Library.AV.AVcommunication.AVEventHandler VideoData;
        /// <summary>
        /// 获得对方视频图像头信息事件
        /// </summary>
        //public event CSS.IM.Library.AV.AVcommunication.AVEventHandler GetBITMAPINFOHEADER;

        /// <summary>
        /// 音频到达事件
        /// </summary>
        /// <param name="data"></param>
        private void OnAudioData(byte[] data)//
        {
            if (this.AudioData != null)
                this.AudioData(this, new CSS.IM.Library.AV.AVcommunication.AVEventArgs(data));
        }

        /// <summary>
        /// 视频到达事件
        /// </summary>
        /// <param name="data"></param>
        private void OnVideoData(byte[] data)//
        {
            if (this.VideoData != null)
                this.VideoData(this, new CSS.IM.Library.AV.AVcommunication.AVEventArgs(data));
        }



        /// <summary>
        /// 发送音频数据到对方
        /// </summary>
        /// <param name="data">音频数据</param>
        public void SendAudio(byte[] data)//
        {
            CSS.IM.Library.Class.msgAV msg = new CSS.IM.Library.Class.msgAV();
            msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolAVTransmit.GetAudioData;
            msg.DataBlock = data;
            this.sendData(msg);
        }

        /// <summary>
        /// 发送视频数据到对方
        /// </summary>
        /// <param name="data">视频数据</param>
        public void SendVideo(byte[] data)
        {
            CSS.IM.Library.Class.msgAV msg = new CSS.IM.Library.Class.msgAV();
            msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolAVTransmit.GetVideoData;
            msg.DataBlock = data;
            this.sendData(msg);
        }


        #endregion

        #region winSock 发送数据
        /// <summary>
        /// sockUDP 发送文件数据
        /// </summary>
        /// <param name="fInfo">文件信息</param>
        private void sendData(CSS.IM.Library.Class.msgAV msg)
        {
            try
            {
                //if (this.netClass == CSS.IM.Library.Class.NetCommunicationClass.LanUDP)//如果是局域网通信
                    this.sockUDP1.Send(this._OppositeUserInfo.LocalIP, this.OppositeUDPPort, msg.getBytes());//采用UDP发送数据到对方局域网IP与端口
                //else if (this.netClass == CSS.IM.Library.Class.NetCommunicationClass.WanNoProxyUDP)//如果是广域网直接通信
                //    this.sockUDP1.Send(this._OppositeUserInfo.IP, this.OppositeUDPPort, msg.getBytes());//采用UDP发送数据到对方广域网IP与端口
                //else if (this.netClass == CSS.IM.Library.Class.NetCommunicationClass.WanProxyUDP)//如果是广域网服务器中转通信
                //    this.sockUDP1.Send(this._serverIp, this._serverUDPPort, msg.getBytes());//采用UDP发送数据到服务器中转IP与端口
                //else if (this.netClass == CSS.IM.Library.Class.NetCommunicationClass.TCP)
                //    this.TCPClient1.SendData(msg.getBytes());//采用TCP发送数据
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
    }
}
