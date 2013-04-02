using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Net; 
using System.IO;
using System.Threading;

namespace CSS.IM.Library.Net
{
    /// <summary>
    /// tcp客户端组件
    /// </summary>
    public partial class SockTCPClient : Component
    {

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SockTCPClient()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="container"></param>
        public SockTCPClient(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region 变量
        private System.Net.Sockets.Socket _Socket = null;
        private CSS.IM.Library.Net.MyTcp _myTcp = new  CSS.IM.Library.Net.MyTcp();
           
        #endregion

        #region 属性 
        /// <summary>
        /// 本机地址
        /// </summary>
        public System.Net.IPAddress LocalIPAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取当前服务是否停止
        /// </summary>
        public bool IsStopServer
        {
            get;
            private set;
        }

        /// <summary>
        /// 本机端口
        /// </summary>
        public int LocalPort
        {
            get;
            private set;
        }

        /// <summary>
        /// 主机地址
        /// </summary>
        public System.Net.IPAddress RemoteIPAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// 主机端口
        /// </summary>
        public int RemotePort
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否联接
        /// </summary>
        public bool Connected
        {
            get {return  _Socket.Connected; }
        }

        /// <summary>
        /// 功能描述
        /// </summary>
        [Browsable(true), Category("全局"), Description("功能描述.")]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 是否异步通信
        /// </summary>
        [Browsable(true), Category("全局"), Description("是否异步通信.")]
        public bool IsAsync
        {
            get;
            set;
        }

        #endregion

        #region 代理委托
        /// <summary>
        /// Socket异常错误 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">异步TCP通信事件参数</param>
        public delegate void ErrorEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

       /// <summary>
       /// 数据到达事件
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        public delegate void DataArrivalEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        /// <summary>
        /// 断开连接 
        /// </summary>
        public delegate void DisconnectedEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);


        /// <summary>
        /// 连接建立 
        /// </summary>
        public delegate void ConnectedEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        /// <summary>
        /// 拒绝服务 
        /// </summary>
        public delegate void DisServerEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        #endregion

        #region 事件声明

        /// <summary>
        /// 套接字错误事件
        /// </summary>
        public event ErrorEventHandler OnError;
        /// <summary>
        /// 数据达到事件
        /// </summary>
        public event DataArrivalEventHandler OnDataArrival;
        /// <summary>
        /// 联接断开事件
        /// </summary>
        public event DisconnectedEventHandler OnDisconnected;
        /// <summary>
        /// 联接建立事件
        /// </summary>
        public event ConnectedEventHandler OnConnected;
       
        #endregion

        #region 内部方法
        /// <summary>
        /// 异步接收数据
        /// </summary>
        /// <param name="aResult">IAsyncResult接口对象</param>
        private void AsyncDataReceive(IAsyncResult aResult)
        {
            CSS.IM.Library.Net.MyTcp myTcp = (CSS.IM.Library.Net.MyTcp)aResult.AsyncState;
            try
            {
                if (IsStopServer == false)
                {
                    int iReceiveCount = myTcp.Socket.EndReceive(aResult);
                    if (iReceiveCount > 0)
                    {

                        byte[] data =CSS.IM.Library.Net.SealedMsg.RecvMsgBytes(myTcp.Buffer);    
                        if (OnDataArrival != null)
                            OnDataArrival(this, new CSS.IM.Library.Net.SockEventArgs(data,myTcp.IP,myTcp.Port));

                        if ( IsStopServer == false)
                        {
                            myTcp.Socket.BeginReceive(myTcp.Buffer,0, myTcp.Buffer.Length, System.Net.Sockets.SocketFlags.None, new AsyncCallback(AsyncDataReceive), myTcp);
                        }
                    }
                    else
                    {
                        Disconnect();
                    }
                }
             
            }
            catch (Exception ex)
            {
                if (OnError != null)
                    OnError(this, new CSS.IM.Library.Net.SockEventArgs(1, ex.Message));
            }
        }


        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="aResult">IAsyncResult接口对象</param>
        private void AsyncSend(IAsyncResult aResult)
        {
            try
            {
                int iSendCount = _Socket.EndSend(aResult);
            } 
            catch (Exception ex)
            {
                if (OnError != null)
                    OnError(this, new CSS.IM.Library.Net.SockEventArgs(2, ex.Message));
            }
        }

        /// <summary>
        /// 发送消息(异步发送)
        /// </summary>
        /// <param name="Data">要发送的消息</param>
        private void Send(byte[] Data)
        {
            Data =CSS.IM.Library.Net.SealedMsg.SendMsgBytes(Data);    

            try
            {
                if (_Socket.Connected)
                    if (IsAsync)//如果是异步通信
                        _Socket.BeginSend(Data, 0, Data.Length, System.Net.Sockets.SocketFlags.None, new AsyncCallback(AsyncSend), null);
                    else
                    {
                        _Socket.Send(Data, 0, Data.Length, System.Net.Sockets.SocketFlags.None);
                    }
            }
            catch (Exception ex)
            {
                if (OnError != null)
                    OnError(this, new CSS.IM.Library.Net.SockEventArgs(3, ex.Message));
            }
        }

        /// <summary>
        /// 异步联接
        /// </summary>
        /// <param name="aResult"></param>
        private void AsyncConnect(IAsyncResult aResult)
        {
            System.Net.Sockets.Socket sock = (System.Net.Sockets.Socket)aResult.AsyncState;
            try
            {
                sock.EndConnect(aResult);

                if (sock != null && sock.Connected)
                {
                    if (OnConnected != null)
                        OnConnected(this, null);//触发连接建立事件

                    _myTcp.Socket = sock;
                    if (IsStopServer == false)
                    {
                        _myTcp.Socket.BeginReceive(_myTcp.Buffer, 0, _myTcp.Buffer.Length, System.Net.Sockets.SocketFlags.None, new AsyncCallback(AsyncDataReceive), _myTcp);
                    }
                }

            }
            catch (Exception ex)
            {
                if (OnError != null)
                    OnError(this, new CSS.IM.Library.Net.SockEventArgs(4, ex.Message));
            }
        }

        private void target()
        {
            while (true)
            {
                int iReceiveCount = this._Socket.Receive(_myTcp.Buffer, _myTcp.Buffer.Length, System.Net.Sockets.SocketFlags.None);
                if (iReceiveCount > 0)
                {
                    byte[] data = CSS.IM.Library.Net.SealedMsg.RecvMsgBytes(_myTcp.Buffer);
                    if (OnDataArrival != null)
                        OnDataArrival(this, new CSS.IM.Library.Net.SockEventArgs(data, RemoteIPAddress, RemotePort));
                }
                else
                {
                    Disconnect();
                }
            }
        }
         
        #endregion

        #region 对外开发的接口方法
        /// <summary>
        /// 初始化本机Socket通信机制
        /// </summary>
        /// <param name="localIPAddress">本机地址</param>
        /// <param name="localPort">本机端口</param>
        /// <returns>返回操作是否成功</returns>
        public bool InitSocket(System.Net.IPAddress localIPAddress, int localPort)
        {
            bool Active = false;
             IsStopServer = false;
            _Socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
            System.Net.Sockets.LingerOption option = new System.Net.Sockets.LingerOption(false, 10);
            _Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.Linger, option);
            try
            {
                if (LocalPort == 0)
                {
                    Random rd = new Random();
                    LocalPort = rd.Next(2000, 60000);
                }
                this.LocalIPAddress = localIPAddress;
                this.LocalPort = localPort;
                IPEndPoint ep = new IPEndPoint(LocalIPAddress, LocalPort);
                _Socket.Bind(ep);
                Active = true;
            }
            catch (Exception ex)
            {
                Active = false;
                if (OnError != null)
                    OnError(this, new CSS.IM.Library.Net.SockEventArgs(5, ex.Message));
            }
            return Active;
        }

        /// <summary>
        /// 连接主机
        /// </summary>
        /// <param name="remoteIPAddress">主机地址</param>
        /// <param name="remotePort">主机端口</param>
        /// <returns>返回操作是否成功</returns>
        public bool Connect(System.Net.IPAddress remoteIPAddress, int remotePort)
        {
            if (_Socket == null)
                return false;
            try
            {
                 RemoteIPAddress  = remoteIPAddress;
                 RemotePort = remotePort;
                if (IsAsync)//如果是异步通信
                { 
                    _Socket.Blocking = false;
                    _Socket.BeginConnect(RemoteIPAddress, RemotePort, new AsyncCallback(AsyncConnect), _Socket);
                }
                else
                {
                    _Socket.Connect(RemoteIPAddress, RemotePort);
                    Thread thread = new Thread(new ThreadStart(target));
                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                if (OnError != null)
                    OnError(this, new CSS.IM.Library.Net.SockEventArgs(6, ex.Message));
            }
            return _Socket.Connected;
        }

        /// <summary>
        /// 断开与主机的连接
        /// </summary>
        /// <returns></returns>
        public bool Disconnect( )
        {
            try
            {
                 IsStopServer = true;

                if (_Socket != null)
                {
                    if (_Socket.Connected)//如果已建立与服务器的连接，则断开联接
                    {
                        _Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                        if (OnDisconnected != null)
                            OnDisconnected(this, new CSS.IM.Library.Net.SockEventArgs());//触发断开联接事件
                    }
                    _Socket.Close();
                    _Socket = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                if (OnError != null)
                    OnError(this, new CSS.IM.Library.Net.SockEventArgs(7, ex.Message));
                return false;
            }
        }

        /// <summary>
        /// 发送消息(异步发送)
        /// </summary>
        /// <param name="Data">要发送的消息</param>
        public void SendData(byte[] Data)
        {
            Send(Data);
        }

        #endregion
    }
}