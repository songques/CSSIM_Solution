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
    /// tcp�ͻ������
    /// </summary>
    public partial class SockTCPClient : Component
    {

        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        public SockTCPClient()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="container"></param>
        public SockTCPClient(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region ����
        private System.Net.Sockets.Socket _Socket = null;
        private CSS.IM.Library.Net.MyTcp _myTcp = new  CSS.IM.Library.Net.MyTcp();
           
        #endregion

        #region ���� 
        /// <summary>
        /// ������ַ
        /// </summary>
        public System.Net.IPAddress LocalIPAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// ��ȡ��ǰ�����Ƿ�ֹͣ
        /// </summary>
        public bool IsStopServer
        {
            get;
            private set;
        }

        /// <summary>
        /// �����˿�
        /// </summary>
        public int LocalPort
        {
            get;
            private set;
        }

        /// <summary>
        /// ������ַ
        /// </summary>
        public System.Net.IPAddress RemoteIPAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// �����˿�
        /// </summary>
        public int RemotePort
        {
            get;
            private set;
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool Connected
        {
            get {return  _Socket.Connected; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        [Browsable(true), Category("ȫ��"), Description("��������.")]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ��첽ͨ��
        /// </summary>
        [Browsable(true), Category("ȫ��"), Description("�Ƿ��첽ͨ��.")]
        public bool IsAsync
        {
            get;
            set;
        }

        #endregion

        #region ����ί��
        /// <summary>
        /// Socket�쳣���� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">�첽TCPͨ���¼�����</param>
        public delegate void ErrorEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

       /// <summary>
       /// ���ݵ����¼�
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        public delegate void DataArrivalEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        /// <summary>
        /// �Ͽ����� 
        /// </summary>
        public delegate void DisconnectedEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);


        /// <summary>
        /// ���ӽ��� 
        /// </summary>
        public delegate void ConnectedEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        /// <summary>
        /// �ܾ����� 
        /// </summary>
        public delegate void DisServerEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        #endregion

        #region �¼�����

        /// <summary>
        /// �׽��ִ����¼�
        /// </summary>
        public event ErrorEventHandler OnError;
        /// <summary>
        /// ���ݴﵽ�¼�
        /// </summary>
        public event DataArrivalEventHandler OnDataArrival;
        /// <summary>
        /// ���ӶϿ��¼�
        /// </summary>
        public event DisconnectedEventHandler OnDisconnected;
        /// <summary>
        /// ���ӽ����¼�
        /// </summary>
        public event ConnectedEventHandler OnConnected;
       
        #endregion

        #region �ڲ�����
        /// <summary>
        /// �첽��������
        /// </summary>
        /// <param name="aResult">IAsyncResult�ӿڶ���</param>
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
        /// �첽������Ϣ
        /// </summary>
        /// <param name="aResult">IAsyncResult�ӿڶ���</param>
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
        /// ������Ϣ(�첽����)
        /// </summary>
        /// <param name="Data">Ҫ���͵���Ϣ</param>
        private void Send(byte[] Data)
        {
            Data =CSS.IM.Library.Net.SealedMsg.SendMsgBytes(Data);    

            try
            {
                if (_Socket.Connected)
                    if (IsAsync)//������첽ͨ��
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
        /// �첽����
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
                        OnConnected(this, null);//�������ӽ����¼�

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

        #region ���⿪���Ľӿڷ���
        /// <summary>
        /// ��ʼ������Socketͨ�Ż���
        /// </summary>
        /// <param name="localIPAddress">������ַ</param>
        /// <param name="localPort">�����˿�</param>
        /// <returns>���ز����Ƿ�ɹ�</returns>
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
        /// ��������
        /// </summary>
        /// <param name="remoteIPAddress">������ַ</param>
        /// <param name="remotePort">�����˿�</param>
        /// <returns>���ز����Ƿ�ɹ�</returns>
        public bool Connect(System.Net.IPAddress remoteIPAddress, int remotePort)
        {
            if (_Socket == null)
                return false;
            try
            {
                 RemoteIPAddress  = remoteIPAddress;
                 RemotePort = remotePort;
                if (IsAsync)//������첽ͨ��
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
        /// �Ͽ�������������
        /// </summary>
        /// <returns></returns>
        public bool Disconnect( )
        {
            try
            {
                 IsStopServer = true;

                if (_Socket != null)
                {
                    if (_Socket.Connected)//����ѽ���������������ӣ���Ͽ�����
                    {
                        _Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                        if (OnDisconnected != null)
                            OnDisconnected(this, new CSS.IM.Library.Net.SockEventArgs());//�����Ͽ������¼�
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
        /// ������Ϣ(�첽����)
        /// </summary>
        /// <param name="Data">Ҫ���͵���Ϣ</param>
        public void SendData(byte[] Data)
        {
            Send(Data);
        }

        #endregion
    }
}