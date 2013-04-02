using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Net;
using System.IO;
using System.Threading;

namespace CSS.IM.Library.Net
{
    /// <summary>
    /// TCP服务组件
    /// </summary>
    public partial class SockTCPServer : Component
    {

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public SockTCPServer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="container">容器</param>
        public SockTCPServer(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region 变量
         
        /// <summary>
        /// 当前服务的Socket对像
        /// </summary>
        private System.Net.Sockets.Socket _Socket = null;

  
        private bool _isActive = false;

        /// <summary>
        /// 已接收的客户端列表
        /// </summary>
        private ArrayList _clientSocketList = new ArrayList();

        /// <summary>
        /// 是否停止服务
        /// </summary>
        private bool IsStopServer = false;

        /// <summary>
        /// 允许服务器最多连接客户端的数量
        /// </summary>
        private int _allowMaxConnectCount = 50000; 

        #endregion

        #region 属性

        /// <summary>
        /// 允许服务器最多连接客户端的数量
        /// </summary>
        public int AllowMaxConnectCount
        {
            get {return  _allowMaxConnectCount; }
            set { _allowMaxConnectCount = value; }
        }

        /// <summary>
        /// 已经服务器的客户端数量
        /// </summary>
        public int  ConnectedCount
        {
            get { return _clientSocketList.Count; }
        }
         
        /// <summary>
        /// Socket通信状态
        /// </summary>
        public bool IsActive 
        {
            get { return _isActive; }
        }
 
        ///// <summary>
        ///// 客户端管理队列
        ///// </summary>
        //public ArrayList ClientSocketList
        //{
        //    get { return _clientSocketList;  }
        //}
        #endregion

        #region 代理委托

        /// <summary>
        /// Socket异常错误事件
        /// </summary>
        public delegate void ErrorEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        /// <summary>
        /// 接受新的Socket通信连接请求
        /// </summary>
        public delegate void AcceptSocketEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        /// <summary>
        /// 接收数据
        /// </summary>
        public delegate void DataArrivalEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        /// <summary>
        /// 客户端断开连接
        /// </summary>
        public delegate void CloseEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);
         
        /// <summary>
        /// 连接客户端数量达到所设置的上限值
        /// </summary>
        public delegate void MaxConnectCountArrivalEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        #endregion

        #region 事件声明

        /// <summary>
        /// 套接字错误事件 
        /// </summary>
        public event ErrorEventHandler OnError;
        /// <summary>
        /// 接收客户端联接事件 
        /// </summary>
        public event AcceptSocketEventHandler OnAcceptClientSocket;
        /// <summary>
        /// 数据到达事件
        /// </summary>
        public event DataArrivalEventHandler OnDataArrival;
        /// <summary>
        /// 客户端套接字关闭事件
        /// </summary>
        public event CloseEventHandler OnClose;
        /// <summary>
        /// 到达最大联接数事件 
        /// </summary>
        public event MaxConnectCountArrivalEventHandler OnMaxConnectCountArrival;

        #endregion

        #region 事件代理方法

        /// <summary>
        /// 异常错误
        /// </summary>
        private void Error(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            if (OnError != null)
                OnError(sender ,e);
        }

        /// <summary>
        /// 接受新的Socket通信连接请求
        /// </summary>
        private void AcceptClientSocket(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            if (OnAcceptClientSocket != null)
                OnAcceptClientSocket(sender, e);
        }

        /// <summary>
        /// 接收二进制数据
        /// </summary>
        private void DataArrival(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            if (OnDataArrival != null)
                OnDataArrival(sender ,e);
        }

        /// <summary>
        /// 客户端断开连接
        /// </summary>
        private void Close(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            if (OnClose  != null)   
                OnClose(sender,e );
        }

        /// <summary>
        /// 连接客户端数量达到所设置的上限值
        /// </summary>
        private void MaxConnectCountArrival(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            if (OnMaxConnectCountArrival != null)
                OnMaxConnectCountArrival(sender,e);
        }
        #endregion

        #region 内部方法

        /// <summary>
        /// 初始化Socket连接
        /// </summary>
        /// <param name="LocalIPAddress">本机地址</param>
        /// <param name="LocalPort">本机端口</param>
        /// <returns>返回操作是否成功</returns>
        private bool InitSocket(System.Net.IPAddress LocalIPAddress, int LocalPort)
        {
            bool Active = false;
            IsStopServer = false;

            _Socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
            System.Net.Sockets.LingerOption Option = new  System.Net.Sockets.LingerOption(false, 10);
            _Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.Linger, Option);
            try
            {
                IPEndPoint ep = new IPEndPoint(LocalIPAddress, LocalPort);
                _Socket.Bind(ep);
                Active = true;
            }
            catch (Exception ex)
            {
                Active = false;
                Error(this, new CSS.IM.Library.Net.SockEventArgs(1, ex.Message));
            }
            return Active;
        }


        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="LimitAcceptCount">挂起连接队列的最大长度</param>
        /// <returns>返回操作是否成功</returns>
        private bool StartListen(int LimitAcceptCount)
        {
            bool Active = false;
            try
            {
                if (_Socket != null)
                {
                    _Socket.Listen(LimitAcceptCount);
                    //调用异步接收Socket连接请求事件
                    _Socket.BeginAccept(new AsyncCallback(AsyncAcceptSocket), _Socket);
                    Active = true;
                }
            }
            catch (Exception ex)
            {
                Active = false;
                Error(this, new CSS.IM.Library.Net.SockEventArgs(2, ex.Message));
            }
            return Active;
        }

        /// <summary>
        /// 主动关闭客户端连接
        /// </summary>
        /// <param name="myTcp">客户端对象</param>
        private void CloseClientSocket(MyTcp myTcp)
        {
            if (myTcp.Closed == true) return;

            myTcp.Closed = true;
 
            try
            {
                if (myTcp.Socket  != null)
                {
                    if (myTcp.Socket.Connected)
                        myTcp.Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    myTcp.Socket.Close();
                    myTcp.Socket = null;
                }
                try
                {
                    _clientSocketList.Remove(myTcp);
                    Close(myTcp, new CSS.IM.Library.Net.SockEventArgs());//触发关闭事件
                }
                catch
                { }

            }
            catch (Exception ex)
            {
                Error(myTcp, new CSS.IM.Library.Net.SockEventArgs(3, ex.Message));
            }
        }

        /// <summary>
        /// 客户端Socket连接异步接收数据
        /// </summary>
        /// <param name="aResult"></param>
        private void AsyncClientSocketReceive(IAsyncResult aResult)
        {
            MyTcp myTcp = (MyTcp)aResult.AsyncState;
            try
            {
                if (myTcp.Socket != null)
                {
                    if (myTcp.Socket.Connected)
                    {
                        int iReceiveCount = myTcp.Socket.EndReceive(aResult);
                        if (iReceiveCount > 0)
                        {
                            byte[] data = SealedMsg.RecvMsgBytes(myTcp.Buffer);    

                            DataArrival(myTcp, new CSS.IM.Library.Net.SockEventArgs(data,myTcp.IP,myTcp.Port));

                            if (IsStopServer == false)
                                myTcp.Socket.BeginReceive(myTcp.Buffer, 0, myTcp.Buffer.Length, System.Net.Sockets.SocketFlags.None, new AsyncCallback(AsyncClientSocketReceive), myTcp);
                        }
                        else
                        {
                            CloseClientSocket(myTcp);
                        }
                    }
                    else
                    {
                        CloseClientSocket(myTcp);
                    }
                }
            }
            catch (Exception ex)
            {
                //接收数据异常，则关闭该客户端连接
                //CSS.IM.Library.Calculate.WirteLog("接收数据异常，则关闭该客户端连接"+ex.Source +ex.Message );
                CloseClientSocket(myTcp);
                Error(myTcp, new CSS.IM.Library.Net.SockEventArgs(4, ex.Message));
            }
        }

        /// <summary>
        /// 异步接受Socket连接请求
        /// </summary>
        /// <param name="aResult">IAsyncResult接口对象</param>
        private void AsyncAcceptSocket(IAsyncResult aResult)
        {
            System.Net.Sockets.Socket sock = (System.Net.Sockets.Socket)aResult.AsyncState;

            try
            {
                if (IsStopServer == false)//如果没有终止侦听
                {
                    System.Net.Sockets.Socket handler = sock.EndAccept(aResult);//接收新连接

                    //判断handler是不是被设置为null，如果为null表示连接在执行AcceptSocket过程中用户在OnAcceptSocket中把连接释放掉
                    if (handler != null)
                    {
                        //判断是否保持连接，如果为false，表示连接在执行AcceptSocket过程中用户在OnAcceptSocket中把连接断开掉
                        if (handler.Connected)
                        {
                            if (_clientSocketList.Count == AllowMaxConnectCount)//当现有连接数达到上限时，停止接收新的服务
                            {
                                MaxConnectCountArrival(this, new CSS.IM.Library.Net.SockEventArgs());//触发连接数量达到上限事件
                                handler.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                                handler.Close();
                                handler = null;
                            }
                            else
                            {
                                MyTcp myTcp = new MyTcp();
                                myTcp.Socket = handler;
                                _clientSocketList.Add(myTcp);

                                myTcp.OnClose += new MyTcp.CloseEventHandler(myTcp_OnClose);//产生MyTcp类Socket关闭事件

                                AcceptClientSocket(myTcp, new CSS.IM.Library.Net.SockEventArgs());//触发接收新连接事件

                                if (IsStopServer == false)//如果没有终止侦听，则开始异步接收新连接的数据
                                    myTcp.Socket.BeginReceive(myTcp.Buffer, 0, myTcp.Buffer.Length, System.Net.Sockets.SocketFlags.None, new AsyncCallback(AsyncClientSocketReceive), myTcp);
                            }
                        }
                        else
                        {
                            handler = null;
                        }
                    }
                    if (IsStopServer == false)//如果没有终止侦听，则继续异步接收新连接请求
                    {
                        sock.BeginAccept(new AsyncCallback(AsyncAcceptSocket), sock);
                    }
                }
            }
            catch (Exception ex)
            {
                //接受连接过程发生异常关闭连接
                Stop();
                Error(this, new CSS.IM.Library.Net.SockEventArgs(5, ex.Message));
            }
        }

        private void myTcp_OnClose(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            CloseClientSocket(sender as MyTcp);//关闭SOCKET
        }

        /// <summary>
        /// 通过客户端Socket异步发送消息
        /// </summary>
        /// <param name="aResult">IAsyncResult接口对象</param>
        private void AsyncClientSend(IAsyncResult aResult)
        {
            MyTcp myTcp = (MyTcp)aResult.AsyncState;
            try
            {
                int iSendCount = myTcp.Socket.EndSend(aResult);
            }
            catch (Exception ex)
            {
                Error(myTcp, new CSS.IM.Library.Net.SockEventArgs(6, ex.Message));
            }
        }
        
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Data">要发送的消息</param>
        /// <param name="myTcp">接收消息的对象</param>
        private void Send(byte[] Data, MyTcp myTcp)
        {
            Data= SealedMsg.SendMsgBytes(Data);    
            try
            {
                if(myTcp.Socket.Connected)
                myTcp.Socket.BeginSend(Data,0,Data.Length, System.Net.Sockets.SocketFlags.None,new AsyncCallback(AsyncClientSend),myTcp);
            }
            catch (Exception ex)
            {
                Error(myTcp, new CSS.IM.Library.Net.SockEventArgs(7, ex.Message));
            }
        }

        #endregion

        #region 开放的接口方法

        /// <summary>
        /// 启动Socket通信
        /// </summary>
        /// <param name="LocalIPAddress">本机地址</param>
        /// <param name="LocalPort">本机端口</param>
        /// <param name="LimitAcceptCount">挂起连接队列的最大长度</param>
        /// <returns>返回操作是否成功</returns>
        public bool Listen(System.Net.IPAddress  LocalIPAddress, int LocalPort, int LimitAcceptCount)
        {
            bool Active = InitSocket(LocalIPAddress, LocalPort);
            if (Active)//如果socket初始化成功，则开始侦听
            {
                Active = StartListen(LimitAcceptCount);
            }
            _isActive = Active;
            return _isActive;
        }

        /// <summary>
        /// 停止Socket通信
        /// </summary>
        /// <returns>返回操作是否成功</returns>
        public bool Stop()
        {
            bool Active = false;
            if (_Socket != null)
            {
                try
                {
                    IsStopServer = true;
                    Thread.Sleep(10);
                    if (_Socket.Connected)
                    {
                        _Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    }
                    _Socket.Close();
                    _Socket  = null;

                    if (_clientSocketList.Count > 0)//如果当前有1个以上联接
                        for (int i = 0; i < _clientSocketList.Count; i++)//断开所有连接
                            CloseClientSocket(_clientSocketList[i] as MyTcp);
                      
                    Active = true;
                   
                }
                catch (Exception ex)
                {
                    Active = false;
                    Error(this, new CSS.IM.Library.Net.SockEventArgs(8, ex.Message));
                }
                _clientSocketList.Clear();//清除所有客户端
                _isActive = !Active;
            }
            return Active;
        }
  
        /// <summary>
        /// 发送消息(异步发送)
        /// </summary>
        /// <param name="Data">要发送的消息</param>
        /// <param name="myTcp">发送消息的myTcp对像</param>
        public void SendData(byte[] Data, MyTcp myTcp)
        {
            Send(Data, myTcp);
        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="Data">要广播的消息</param>
        public void BroadcastMessage(byte[] Data)
        {
            for (int i = 0; i < _clientSocketList.Count; i++)
            {
                MyTcp myTcp = (MyTcp)_clientSocketList[i];
                try
                {
                    Send(Data, myTcp);
                }
                catch (Exception ex)
                {
                    Error(myTcp, new CSS.IM.Library.Net.SockEventArgs(9, ex.Message));
                }
            }
        }
        #endregion
    }
}
