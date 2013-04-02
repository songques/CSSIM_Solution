using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CSS.IM.Library.Net
{
    /// <summary>
    /// UDP组件
    /// </summary>
    public partial class SockUDP : Component
    {

        private UdpClient UDP_Server = new UdpClient();
        private System.Threading.Thread thdUdp;
        private bool GetUDPDataSwitch = true;


        /// <summary>
        /// 构造函数
        /// </summary>
        public  SockUDP()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="container"></param>
        public  SockUDP(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
         
       
        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (UDP_Server!=null)
            {
                UDP_Server.Close();
                UDP_Server = null;
            }

            if (thdUdp!=null)
            {
                thdUdp.Abort();
                thdUdp = null;
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);

            System.GC.Collect();
        }

        #region 事件
        /// <summary>
        /// 数据到达事件
        /// </summary>
        /// <param name="sender">套接字对像</param>
        /// <param name="e">事件相关参数</param>
        public delegate void DataArrivalEventHandler(object sender, SockEventArgs e);
        /// <summary>
        /// 数据到达事件
        /// </summary>
        public event DataArrivalEventHandler DataArrival;
        /// <summary>
        /// 套接字错误事件
        /// </summary>
        /// <param name="sender">套接字对像</param>
        /// <param name="e">事件相关参数</param>
        public delegate void ErrorEventHandler(object sender, SockEventArgs e);
        /// <summary>
        /// 套接字错误事件
        /// </summary>
        public event ErrorEventHandler Sock_Error;
        #endregion

        #region 属性
        /// <summary>
        /// 标识是否侦听
        /// </summary>
        public bool Listened
        {
            get;
            private set;
        }

        /// <summary>
        /// 侦听端口
        /// </summary>
        public int ListenPort
        {
            get;
            private set;
        }

        /// <summary>
        /// 功能描述
        /// </summary>
        [Browsable(true), Category("全局"), Description("功能描述.")]
        public string Description
        {
            get;  set;
        }

        /// <summary>
        /// 是否异步通信
        /// </summary>
        [Browsable(true), Category("全局"), Description("是否异步通信.")]
        public bool IsAsync
        {
            get; set ;
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 异步接收数据
        /// </summary>
        /// <param name="ar"></param>
        private void ReadCallback(IAsyncResult ar)
        {
            IPEndPoint ipend = null;
             byte[] RData =null;

             try
             {

                 RData = this.UDP_Server.EndReceive(ar, ref ipend);

                 //if (DataArrival != null)
                 //{
                 //    DataArrival(this, new SockEventArgs(RData, ipend.Address, ipend.Port));
                 //}

                 delegateReceviedMsg rMsg = new delegateReceviedMsg(ReceviedMsg);
                 rMsg.BeginInvoke(RData, ipend, null, null);

                 UDP_Server.BeginReceive(new AsyncCallback(ReadCallback), null);

             }
             catch (Exception e)
             {
                 if (Sock_Error != null)
                     Sock_Error(this, new SockEventArgs(1, e.Source + "," + e.Message));
             }
             finally
             {
                 ipend = null;
                 RData = null;
             }
        }

        /// <summary>
        /// 异步处理同步接收的数据
        /// </summary> 
        private void GetUDPData()
        {
            IPEndPoint ipend = null;
            byte[] RData=null;

            while (GetUDPDataSwitch)
            {
                try
                {
                    RData = UDP_Server.Receive(ref ipend);
                    if (DataArrival != null)
                        DataArrival(this, new SockEventArgs(RData, ipend.Address, ipend.Port));
                    //delegateReceviedMsg rMsg = new delegateReceviedMsg(ReceviedMsg);
                    //rMsg.BeginInvoke(RData, ipend, null, null);
                    //Thread.Sleep(0);
                }
                catch (Exception e)
                {
                    if (Sock_Error != null)
                        Sock_Error(this, new SockEventArgs(2, e.Source + "," + e.Message));
                    this.CloseSock();
                    
                }
            }

            ipend = null;
            RData = null;
        }

        private delegate void delegateReceviedMsg(byte[] Data, IPEndPoint e);
        private void ReceviedMsg(byte[] Data, IPEndPoint e)
        {
            if ( DataArrival != null)
                DataArrival(this, new SockEventArgs(Data, e.Address, e.Port));
        }

        /// <summary>
        /// 异步发送数据
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                UDP_Server.EndSend(ar);
            }
            catch (Exception e)
            {
                if (Sock_Error != null)
                    Sock_Error(this, new SockEventArgs(3, e.Source + "," + e.Message));
            }
        }
        #endregion

        #region 外部方法
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="Host">对方IP</param>
        /// <param name="Port">对方端口</param>
        /// <param name="Data">要发送的数据</param>
        public void Send(System.Net.IPAddress Host, int Port, byte[] Data)
        {
            IPEndPoint server=null;
            try
            {
                server = new IPEndPoint(Host, Port);
                if (IsAsync)
                    UDP_Server.BeginSend(Data, Data.Length, server, new AsyncCallback(SendCallback), null);
                else
                    UDP_Server.Send(Data, Data.Length, server);
            }
            catch (Exception e)
            {
                if (Sock_Error != null)
                    Sock_Error(this, new SockEventArgs(4, e.Source + "," + e.Message));
            }
            finally
            {
                server = null;
            }
        }
         
        /// <summary>
        /// 侦听
        /// </summary>
        /// <param name="Port">侦听端口</param>
        public void Listen(int Port)
        {
            try
            {
                if (Port < 1)
                {
                    System.Random r = new System.Random();
                resetPort:
                    Port = r.Next(2000, 60000);

                    try
                    {
                        UDP_Server = new UdpClient(Port);
                    }
                    catch
                    {
                        try
                        {
                            UDP_Server.Close();
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            UDP_Server = null;
                        }
                        catch (Exception)
                        {
                        }
                        
                        goto resetPort;
                    }
                }
                else
                {
                    UDP_Server = new UdpClient(Port);
                }

               

                //UDP_Server.Client.DontFragment = true;//允许分包IP协议层

                this.Listened = true;//侦听
                this.ListenPort = Port;

                if (IsAsync)//如果采用异步通信
                {
                    UDP_Server.BeginReceive(new AsyncCallback(ReadCallback), null);
                }
                else//如果是同步通信
                {
                    thdUdp = new Thread(new ThreadStart(GetUDPData));
                    thdUdp.Start();
                }
            }
            catch (Exception e)
            {
                if (Sock_Error != null)
                    Sock_Error(this, new SockEventArgs(5, e.Source + "," + e.Message));
            }
        }

        /// <summary>
        /// 侦听
        /// </summary>
        /// <param name="LocalIp">本地IP</param>
        /// <param name="Port">本地端口</param>
        public void Listen(IPAddress LocalIp, int Port)
        {
            try
            {
                IPEndPoint ipEnd = new IPEndPoint(LocalIp, Port);
                UDP_Server = new UdpClient(ipEnd);


                //UDP_Server.Client.DontFragment = true;//允许分包IP协议层
               
                this.Listened = true;//侦听
                this.ListenPort =  Port;

                if (IsAsync)//如果采用异步通信
                {
                    UDP_Server.BeginReceive(new AsyncCallback(ReadCallback), null);
                }
                else//如果是同步通信
                {
                    thdUdp = new Thread(new ThreadStart(GetUDPData));
                    thdUdp.Start();
                }
            }
            catch (Exception e)
            {
                if (Sock_Error != null)
                    Sock_Error(this, new SockEventArgs(6, e.Source + "," + e.Message));
            }
        }

        /// <summary>
        /// 关闭套接字
        /// </summary>
        public void CloseSock()
        {
            try
            {
                GetUDPDataSwitch = false;
                thdUdp.Abort();


            }
            catch (Exception e)
            {
                if (Sock_Error != null)
                    Sock_Error(this, new SockEventArgs(7, e.Source + "," + e.Message));
            }

            try
            {
                UDP_Server.Close();
            }
            catch (Exception e)
            {
                if (Sock_Error != null)
                    Sock_Error(this, new SockEventArgs(7, e.Source + "," + e.Message));
            }

            this.Dispose();

            System.GC.Collect();
        }
        #endregion
    }

    #region UDP通信事件参数类
    /// <summary>
    /// 异步TCP通信事件参数
    /// </summary>
    public class SockEventArgs : System.EventArgs
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage;

        /// <summary>
        /// 收到的数据
        /// </summary>
        public byte[] Data;

        /// <summary>
        /// 对方IP地址
        /// </summary>
        public IPAddress IP;

        /// <summary>
        /// 对方端口
        /// </summary>
        public int Port;

        /// <summary>
        /// 套接事件相关参数
        /// </summary>
        public SockEventArgs()
        {

        }

        /// <summary>
        /// UDP通信事件参数
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="errorMessage">错误信息</param>
        public SockEventArgs(int errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// UDP通信事件参数
        /// </summary>
        /// <param name="Data">接收到的数据</param>
        /// <param name="Ip">对方IP</param>
        /// <param name="port">对方端口</param>
        public SockEventArgs(byte[] Data, IPAddress Ip, int port)
        {
            this.Data = Data;
            this.IP = Ip;
            this.Port = port;
        }

    }

    #endregion
}
