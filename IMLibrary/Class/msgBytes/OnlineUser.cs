using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class 
{
    /// <summary>
    /// 在线用户基本信息类
    /// </summary>
    public class OnlineUser
    {
       #region 变量区
        private byte[] _userIndex = new byte[4];//用户索引
        private byte[] _userID = new byte[20];//用户ID
        private byte[] _userIp = new byte[4];//用户WAN IP
        private byte[] _userPort = new byte[4];//用户WAN 端口
        private byte[] _userState=new byte[1];//用户在线状态
        private byte[] _netClass = new byte[1];//用户所在网络出口类型
        private byte[] _userLocalIP = new byte[4];//登录用户的Lan IP 
        private byte[] _userLocalPort = new byte[4];//登录用户的Lan Port 
       #endregion

       #region 设置或获取用户索引UserIndex
        /// <summary>
        /// 设置或获取用户索引
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

       #region 设置或获取UserID
        /// <summary>
        ///  设置或获取用户ID
        /// </summary>
        public string UserID
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._userID.Length)
                    Buffer.BlockCopy(buf, 0, this._userID, 0, this._userID.Length);//获得ID 
                else
                    Buffer.BlockCopy(buf, 0, this._userID, 0, buf.Length);//获得ID 
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._userID).Trim('\0'); }
        }
        #endregion

       #region 设置或获取UserIP
        /// <summary>
        /// 设置或获取用户IP
        /// </summary>
        public System.Net.IPAddress UserIP
        {
            set
            {
                this._userIp = IMLibrary.Class.IP.IPAddressToBytes(value);//将IP转换成32位整型
            }
            get
            {
                return IMLibrary.Class.IP.BytesToIPAddress(this._userIp);//将int转换成IP地址
            }
        }
        #endregion

       #region 设置或获取用户索引UserPort
       /// <summary>
       /// 设置或获取用户端口
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

       #region 设置或获取用户UserState
        /// <summary>
        /// 设置或获取用户在线状态
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

       #region 设置或获取用户网络类型
        /// <summary>
        /// 设置或获取用户网络类型
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

       #region 设置或获取登录用户的Lan IP
        /// <summary>
        /// 设置或获取登录用户的局域网 IP
        /// </summary>
        public System.Net.IPAddress userLocalIP
        {
            set
            {
                this._userLocalIP = IMLibrary.Class.IP.IPAddressToBytes(value);  //将IP转换成32位整型
            }
            get
            {
                return IMLibrary.Class.IP.BytesToIPAddress(this._userLocalIP);//将int转换成IP地址
            }
        }
        #endregion

       #region 设置或获取登录用户的Lan Port
        /// <summary>
        /// 设置或获取登录用户的局域网UDP端口
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

       #region 设置或获取消息字节数组
        /// <summary>
        /// 获得消息字节数组
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

       #region 初始化 
        /// <summary>
        /// 将字节数组转化为在线用户信息类
        /// </summary>
        /// <param name="Data"></param>
        public  OnlineUser(byte[] Data)
        {
            if (Data.Length != getMsgLength) return;//如果不是部门信息，则退出

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

       #region 初始化 
        /// <summary>
        /// 初始化
        /// </summary>
        public  OnlineUser()
        {

        }
        #endregion

       #region 初始化 
        /// <summary>
        /// 初始化在线用户基本信息类
        /// </summary>
        /// <param name="userIndex">用户在线索引</param>
        /// <param name="userID">用户ID</param>
        /// <param name="userIP">用户广域网IP</param>
        /// <param name="userPort">用户广域网端口</param>
        /// <param name="userState">用户在线状态</param>
        /// <param name="netClass">用户网络类型</param>
        /// <param name="localIP">用户局域网IP</param>
        /// <param name="localPort">用户局域网端口</param>
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

       #region 获得消息字节数组长度
        /// <summary>
        /// 获得消息字节数组长度
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
