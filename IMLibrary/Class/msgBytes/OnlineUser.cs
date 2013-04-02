using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class 
{
    /// <summary>
    /// �����û�������Ϣ��
    /// </summary>
    public class OnlineUser
    {
       #region ������
        private byte[] _userIndex = new byte[4];//�û�����
        private byte[] _userID = new byte[20];//�û�ID
        private byte[] _userIp = new byte[4];//�û�WAN IP
        private byte[] _userPort = new byte[4];//�û�WAN �˿�
        private byte[] _userState=new byte[1];//�û�����״̬
        private byte[] _netClass = new byte[1];//�û����������������
        private byte[] _userLocalIP = new byte[4];//��¼�û���Lan IP 
        private byte[] _userLocalPort = new byte[4];//��¼�û���Lan Port 
       #endregion

       #region ���û��ȡ�û�����UserIndex
        /// <summary>
        /// ���û��ȡ�û�����
        /// </summary>
        public int UserIndex
        {
            set
            {
                this._userIndex = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._userIndex, 0);
            }
        }
        #endregion

       #region ���û��ȡUserID
        /// <summary>
        ///  ���û��ȡ�û�ID
        /// </summary>
        public string UserID
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._userID.Length)
                    Buffer.BlockCopy(buf, 0, this._userID, 0, this._userID.Length);//���ID 
                else
                    Buffer.BlockCopy(buf, 0, this._userID, 0, buf.Length);//���ID 
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._userID).Trim('\0'); }
        }
        #endregion

       #region ���û��ȡUserIP
        /// <summary>
        /// ���û��ȡ�û�IP
        /// </summary>
        public System.Net.IPAddress UserIP
        {
            set
            {
                this._userIp = IMLibrary.Class.IP.IPAddressToBytes(value);//��IPת����32λ����
            }
            get
            {
                return IMLibrary.Class.IP.BytesToIPAddress(this._userIp);//��intת����IP��ַ
            }
        }
        #endregion

       #region ���û��ȡ�û�����UserPort
       /// <summary>
       /// ���û��ȡ�û��˿�
       /// </summary>
        public int UserPort
        {
            set
            {
                this._userPort = BitConverter.GetBytes( value );
            }
            get
            {
                return BitConverter.ToInt32(this._userPort, 0);
            }
        }
        #endregion

       #region ���û��ȡ�û�UserState
        /// <summary>
        /// ���û��ȡ�û�����״̬
        /// </summary>
        public byte UserState
        {
            set
            {
                this._userState[0] = value;
            }
            get
            {
                return this._userState[0];
            }
        }
        #endregion

       #region ���û��ȡ�û���������
        /// <summary>
        /// ���û��ȡ�û���������
        /// </summary>
        public byte NetClass
        {
            set
            {
                this._netClass[0] = value;
            }
            get
            {
                return this._netClass[0];
            }
        }
        #endregion

       #region ���û��ȡ��¼�û���Lan IP
        /// <summary>
        /// ���û��ȡ��¼�û��ľ����� IP
        /// </summary>
        public System.Net.IPAddress userLocalIP
        {
            set
            {
                this._userLocalIP = IMLibrary.Class.IP.IPAddressToBytes(value);  //��IPת����32λ����
            }
            get
            {
                return IMLibrary.Class.IP.BytesToIPAddress(this._userLocalIP);//��intת����IP��ַ
            }
        }
        #endregion

       #region ���û��ȡ��¼�û���Lan Port
        /// <summary>
        /// ���û��ȡ��¼�û��ľ�����UDP�˿�
        /// </summary>
        public int userLocalPort
        {
            set
            {
                this._userLocalPort = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._userLocalPort, 0);
            }
        }
        #endregion

       #region ���û��ȡ��Ϣ�ֽ�����
        /// <summary>
        /// �����Ϣ�ֽ�����
        /// </summary>
        public byte[] msgBytes
        {
            get
            {
                byte[] _msgBytes = new byte[getMsgLength];

                int dstOffSet = 0;

                Buffer.BlockCopy(_userIndex, 0, _msgBytes, dstOffSet, _userIndex.Length);
                dstOffSet += _userIndex.Length;

                Buffer.BlockCopy(_userID, 0, _msgBytes, dstOffSet, _userID.Length);
                dstOffSet += _userID.Length;

                Buffer.BlockCopy(_userIp, 0, _msgBytes, dstOffSet, _userIp.Length);
                dstOffSet += _userIp.Length;

                Buffer.BlockCopy(_userPort, 0, _msgBytes, dstOffSet, _userPort.Length);
                dstOffSet += _userPort.Length;

                Buffer.BlockCopy(_userState, 0, _msgBytes, dstOffSet, _userState.Length);
                dstOffSet += _userState.Length;

                Buffer.BlockCopy(_netClass, 0, _msgBytes, dstOffSet, _netClass.Length);
                dstOffSet += _netClass.Length;

                Buffer.BlockCopy(_userLocalIP, 0, _msgBytes, dstOffSet, _userLocalIP.Length);
                dstOffSet += _userLocalIP.Length;

                Buffer.BlockCopy(_userLocalPort, 0, _msgBytes, dstOffSet, _userLocalPort.Length);
                dstOffSet += _userLocalPort.Length;

                return _msgBytes;
            }
        }
        #endregion

       #region ��ʼ�� 
        /// <summary>
        /// ���ֽ�����ת��Ϊ�����û���Ϣ��
        /// </summary>
        /// <param name="Data"></param>
        public  OnlineUser(byte[] Data)
        {
            if (Data.Length != getMsgLength) return;//������ǲ�����Ϣ�����˳�

            int dstOffSet = 0;

            Buffer.BlockCopy(Data , dstOffSet, _userIndex, 0, _userIndex.Length);
            dstOffSet += _userIndex.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userID, 0, _userID.Length);
            dstOffSet += _userID.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userIp, 0, _userIp.Length);
            dstOffSet += _userIp.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userPort, 0, _userPort.Length);
            dstOffSet += _userPort.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userState, 0, _userState.Length);
            dstOffSet += _userState.Length;

            Buffer.BlockCopy(Data, dstOffSet, _netClass, 0, _netClass.Length);
            dstOffSet += _netClass.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userLocalIP, 0, _userLocalIP.Length);
            dstOffSet += _userLocalIP.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userLocalPort, 0, _userLocalPort.Length);
            dstOffSet += _userLocalPort.Length;
        }
        #endregion

       #region ��ʼ�� 
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public  OnlineUser()
        {

        }
        #endregion

       #region ��ʼ�� 
        /// <summary>
        /// ��ʼ�������û�������Ϣ��
        /// </summary>
        /// <param name="userIndex">�û���������</param>
        /// <param name="userID">�û�ID</param>
        /// <param name="userIP">�û�������IP</param>
        /// <param name="userPort">�û��������˿�</param>
        /// <param name="userState">�û�����״̬</param>
        /// <param name="netClass">�û���������</param>
        /// <param name="localIP">�û�������IP</param>
        /// <param name="localPort">�û��������˿�</param>
       public  OnlineUser(int userIndex, string userID, System.Net.IPAddress userIP, int userPort, byte userState,byte netClass, System.Net.IPAddress localIP,int localPort)
        {
            this.UserIndex = userIndex;
            this.UserID = userID;
            this.UserIP = userIP;
            this.UserPort = userPort;
            this.UserState = userState;
            this.NetClass = netClass;
            this.userLocalIP = localIP;
            this.userLocalPort = localPort;
        }
        #endregion

       #region �����Ϣ�ֽ����鳤��
        /// <summary>
        /// �����Ϣ�ֽ����鳤��
        /// </summary>
        /// <returns></returns>
        public int getMsgLength
        {
            get
            {
                int msgCount = _userIndex.Length + _userID.Length + _userIp.Length + _userPort.Length;
                msgCount += _userState.Length + _netClass.Length + _userLocalIP.Length + _userLocalPort.Length;
                return msgCount;
            }
        }
        #endregion
    }
}
