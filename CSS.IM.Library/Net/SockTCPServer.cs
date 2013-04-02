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
    /// TCP�������
    /// </summary>
    public partial class SockTCPServer : Component
    {

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public SockTCPServer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="container">����</param>
        public SockTCPServer(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region ����
         
        /// <summary>
        /// ��ǰ�����Socket����
        /// </summary>
        private System.Net.Sockets.Socket _Socket = null;

  
        private bool _isActive = false;

        /// <summary>
        /// �ѽ��յĿͻ����б�
        /// </summary>
        private ArrayList _clientSocketList = new ArrayList();

        /// <summary>
        /// �Ƿ�ֹͣ����
        /// </summary>
        private bool IsStopServer = false;

        /// <summary>
        /// ���������������ӿͻ��˵�����
        /// </summary>
        private int _allowMaxConnectCount = 50000; 

        #endregion

        #region ����

        /// <summary>
        /// ���������������ӿͻ��˵�����
        /// </summary>
        public int AllowMaxConnectCount
        {
            get {return  _allowMaxConnectCount; }
            set { _allowMaxConnectCount = value; }
        }

        /// <summary>
        /// �Ѿ��������Ŀͻ�������
        /// </summary>
        public int  ConnectedCount
        {
            get { return _clientSocketList.Count; }
        }
         
        /// <summary>
        /// Socketͨ��״̬
        /// </summary>
        public bool IsActive 
        {
            get { return _isActive; }
        }
 
        ///// <summary>
        ///// �ͻ��˹������
        ///// </summary>
        //public ArrayList ClientSocketList
        //{
        //    get { return _clientSocketList;  }
        //}
        #endregion

        #region ����ί��

        /// <summary>
        /// Socket�쳣�����¼�
        /// </summary>
        public delegate void ErrorEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        /// <summary>
        /// �����µ�Socketͨ����������
        /// </summary>
        public delegate void AcceptSocketEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        /// <summary>
        /// ��������
        /// </summary>
        public delegate void DataArrivalEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        /// <summary>
        /// �ͻ��˶Ͽ�����
        /// </summary>
        public delegate void CloseEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);
         
        /// <summary>
        /// ���ӿͻ��������ﵽ�����õ�����ֵ
        /// </summary>
        public delegate void MaxConnectCountArrivalEventHandler(object sender, CSS.IM.Library.Net.SockEventArgs e);

        #endregion

        #region �¼�����

        /// <summary>
        /// �׽��ִ����¼� 
        /// </summary>
        public event ErrorEventHandler OnError;
        /// <summary>
        /// ���տͻ��������¼� 
        /// </summary>
        public event AcceptSocketEventHandler OnAcceptClientSocket;
        /// <summary>
        /// ���ݵ����¼�
        /// </summary>
        public event DataArrivalEventHandler OnDataArrival;
        /// <summary>
        /// �ͻ����׽��ֹر��¼�
        /// </summary>
        public event CloseEventHandler OnClose;
        /// <summary>
        /// ��������������¼� 
        /// </summary>
        public event MaxConnectCountArrivalEventHandler OnMaxConnectCountArrival;

        #endregion

        #region �¼�������

        /// <summary>
        /// �쳣����
        /// </summary>
        private void Error(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            if (OnError != null)
                OnError(sender ,e);
        }

        /// <summary>
        /// �����µ�Socketͨ����������
        /// </summary>
        private void AcceptClientSocket(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            if (OnAcceptClientSocket != null)
                OnAcceptClientSocket(sender, e);
        }

        /// <summary>
        /// ���ն���������
        /// </summary>
        private void DataArrival(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            if (OnDataArrival != null)
                OnDataArrival(sender ,e);
        }

        /// <summary>
        /// �ͻ��˶Ͽ�����
        /// </summary>
        private void Close(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            if (OnClose  != null)   
                OnClose(sender,e );
        }

        /// <summary>
        /// ���ӿͻ��������ﵽ�����õ�����ֵ
        /// </summary>
        private void MaxConnectCountArrival(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            if (OnMaxConnectCountArrival != null)
                OnMaxConnectCountArrival(sender,e);
        }
        #endregion

        #region �ڲ�����

        /// <summary>
        /// ��ʼ��Socket����
        /// </summary>
        /// <param name="LocalIPAddress">������ַ</param>
        /// <param name="LocalPort">�����˿�</param>
        /// <returns>���ز����Ƿ�ɹ�</returns>
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
        /// ��ʼ����
        /// </summary>
        /// <param name="LimitAcceptCount">�������Ӷ��е���󳤶�</param>
        /// <returns>���ز����Ƿ�ɹ�</returns>
        private bool StartListen(int LimitAcceptCount)
        {
            bool Active = false;
            try
            {
                if (_Socket != null)
                {
                    _Socket.Listen(LimitAcceptCount);
                    //�����첽����Socket���������¼�
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
        /// �����رտͻ�������
        /// </summary>
        /// <param name="myTcp">�ͻ��˶���</param>
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
                    Close(myTcp, new CSS.IM.Library.Net.SockEventArgs());//�����ر��¼�
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
        /// �ͻ���Socket�����첽��������
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
                //���������쳣����رոÿͻ�������
                //CSS.IM.Library.Calculate.WirteLog("���������쳣����رոÿͻ�������"+ex.Source +ex.Message );
                CloseClientSocket(myTcp);
                Error(myTcp, new CSS.IM.Library.Net.SockEventArgs(4, ex.Message));
            }
        }

        /// <summary>
        /// �첽����Socket��������
        /// </summary>
        /// <param name="aResult">IAsyncResult�ӿڶ���</param>
        private void AsyncAcceptSocket(IAsyncResult aResult)
        {
            System.Net.Sockets.Socket sock = (System.Net.Sockets.Socket)aResult.AsyncState;

            try
            {
                if (IsStopServer == false)//���û����ֹ����
                {
                    System.Net.Sockets.Socket handler = sock.EndAccept(aResult);//����������

                    //�ж�handler�ǲ��Ǳ�����Ϊnull�����Ϊnull��ʾ������ִ��AcceptSocket�������û���OnAcceptSocket�а������ͷŵ�
                    if (handler != null)
                    {
                        //�ж��Ƿ񱣳����ӣ����Ϊfalse����ʾ������ִ��AcceptSocket�������û���OnAcceptSocket�а����ӶϿ���
                        if (handler.Connected)
                        {
                            if (_clientSocketList.Count == AllowMaxConnectCount)//�������������ﵽ����ʱ��ֹͣ�����µķ���
                            {
                                MaxConnectCountArrival(this, new CSS.IM.Library.Net.SockEventArgs());//�������������ﵽ�����¼�
                                handler.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                                handler.Close();
                                handler = null;
                            }
                            else
                            {
                                MyTcp myTcp = new MyTcp();
                                myTcp.Socket = handler;
                                _clientSocketList.Add(myTcp);

                                myTcp.OnClose += new MyTcp.CloseEventHandler(myTcp_OnClose);//����MyTcp��Socket�ر��¼�

                                AcceptClientSocket(myTcp, new CSS.IM.Library.Net.SockEventArgs());//���������������¼�

                                if (IsStopServer == false)//���û����ֹ��������ʼ�첽���������ӵ�����
                                    myTcp.Socket.BeginReceive(myTcp.Buffer, 0, myTcp.Buffer.Length, System.Net.Sockets.SocketFlags.None, new AsyncCallback(AsyncClientSocketReceive), myTcp);
                            }
                        }
                        else
                        {
                            handler = null;
                        }
                    }
                    if (IsStopServer == false)//���û����ֹ������������첽��������������
                    {
                        sock.BeginAccept(new AsyncCallback(AsyncAcceptSocket), sock);
                    }
                }
            }
            catch (Exception ex)
            {
                //�������ӹ��̷����쳣�ر�����
                Stop();
                Error(this, new CSS.IM.Library.Net.SockEventArgs(5, ex.Message));
            }
        }

        private void myTcp_OnClose(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            CloseClientSocket(sender as MyTcp);//�ر�SOCKET
        }

        /// <summary>
        /// ͨ���ͻ���Socket�첽������Ϣ
        /// </summary>
        /// <param name="aResult">IAsyncResult�ӿڶ���</param>
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
        /// ������Ϣ
        /// </summary>
        /// <param name="Data">Ҫ���͵���Ϣ</param>
        /// <param name="myTcp">������Ϣ�Ķ���</param>
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

        #region ���ŵĽӿڷ���

        /// <summary>
        /// ����Socketͨ��
        /// </summary>
        /// <param name="LocalIPAddress">������ַ</param>
        /// <param name="LocalPort">�����˿�</param>
        /// <param name="LimitAcceptCount">�������Ӷ��е���󳤶�</param>
        /// <returns>���ز����Ƿ�ɹ�</returns>
        public bool Listen(System.Net.IPAddress  LocalIPAddress, int LocalPort, int LimitAcceptCount)
        {
            bool Active = InitSocket(LocalIPAddress, LocalPort);
            if (Active)//���socket��ʼ���ɹ�����ʼ����
            {
                Active = StartListen(LimitAcceptCount);
            }
            _isActive = Active;
            return _isActive;
        }

        /// <summary>
        /// ֹͣSocketͨ��
        /// </summary>
        /// <returns>���ز����Ƿ�ɹ�</returns>
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

                    if (_clientSocketList.Count > 0)//�����ǰ��1����������
                        for (int i = 0; i < _clientSocketList.Count; i++)//�Ͽ���������
                            CloseClientSocket(_clientSocketList[i] as MyTcp);
                      
                    Active = true;
                   
                }
                catch (Exception ex)
                {
                    Active = false;
                    Error(this, new CSS.IM.Library.Net.SockEventArgs(8, ex.Message));
                }
                _clientSocketList.Clear();//������пͻ���
                _isActive = !Active;
            }
            return Active;
        }
  
        /// <summary>
        /// ������Ϣ(�첽����)
        /// </summary>
        /// <param name="Data">Ҫ���͵���Ϣ</param>
        /// <param name="myTcp">������Ϣ��myTcp����</param>
        public void SendData(byte[] Data, MyTcp myTcp)
        {
            Send(Data, myTcp);
        }

        /// <summary>
        /// �㲥��Ϣ
        /// </summary>
        /// <param name="Data">Ҫ�㲥����Ϣ</param>
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
