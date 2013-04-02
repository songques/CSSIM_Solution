using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class 
{
    /// <summary>
    /// 用户登录时发送给服务器的本地相关信息类
    /// </summary>
    public class LoginInfo
    {
        #region 变量区
        private byte[] _userID = new byte[20];//登录用户ID
        private byte[] _userPassword = new byte[32];//登录用户密码
        private byte[] _softwareVersion = new byte[32];//登录用户软件版本号 MD5 值
        private byte[] _userLocalIP = new byte[4];//登录用户的Lan IP 
        private byte[] _userLocalPort = new byte[4];//登录用户的Lan Port 
        private byte[] _userNetClass = new byte[1];//用户采用的是何种通信协议及类型
        #endregion

        #region 设置或获取登录用户ID
        /// <summary>
        /// 设置或获取登录用户ID
        /// </summary>
        public string UserID
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._userID.Length)
                    Buffer.BlockCopy(buf, 0, this._userID, 0, this._userID.Length); 
                else
                    Buffer.BlockCopy(buf, 0, this._userID, 0, buf.Length); 
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._userID).Trim('\0'); }
        }
        #endregion

        #region 设置或获取登录用户密码
        /// <summary>
        /// 设置或获取登录用户密码(最长32字符，过长系统自动截断)
        /// </summary>
        public string userPassword 
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._userPassword.Length)
                    Buffer.BlockCopy(buf, 0, this._userPassword, 0, this._userPassword.Length);
                else
                    Buffer.BlockCopy(buf, 0, this._userPassword, 0, buf.Length);
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._userPassword).Trim('\0'); }
        }
        #endregion

        #region 登录用户软件版本号
        /// <summary>
        /// 登录用户软件版本号(最长32字符，过长系统自动截断) 
        /// </summary>
        public string softwareVersion
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(IMLibrary.Class.Hasher.GetMD5Hash(IMLibrary.Class.TextEncoder.textToBytes(value)));
                if (buf.Length > this._softwareVersion.Length)
                    Buffer.BlockCopy(buf, 0, this._softwareVersion, 0, this._softwareVersion.Length);
                else
                    Buffer.BlockCopy(buf, 0, this._softwareVersion, 0, buf.Length);
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._softwareVersion).Trim('\0'); }
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
                this._userLocalIP = IMLibrary.Class.IP.IPAddressToBytes(value); //将IP转换成32位整型
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
        public int  userLocalPort
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

        #region 设置或获取登录用户的网络NAT类型
        /// <summary>
        /// 设置或获取登录用户的网络NAT类型
        /// </summary>
        public byte NetClass
        {
            set
            {
                this._userNetClass[0] = value;
            }
            get
            {
                return  this._userNetClass[0];
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

                Buffer.BlockCopy(_userID, 0, _msgBytes, dstOffSet, _userID.Length);
                dstOffSet += _userID.Length;

                Buffer.BlockCopy(_userPassword, 0, _msgBytes, dstOffSet, _userPassword.Length);
                dstOffSet += _userPassword.Length;

                Buffer.BlockCopy(_softwareVersion, 0, _msgBytes, dstOffSet, _softwareVersion.Length);
                dstOffSet += _softwareVersion.Length;

                Buffer.BlockCopy(_userLocalIP, 0, _msgBytes, dstOffSet, _userLocalIP.Length);
                dstOffSet += _userLocalIP.Length;

                Buffer.BlockCopy(_userLocalPort, 0, _msgBytes, dstOffSet, _userLocalPort.Length);
                dstOffSet += _userLocalPort.Length;

                Buffer.BlockCopy(_userNetClass, 0, _msgBytes, dstOffSet, _userNetClass.Length);
                dstOffSet += _userNetClass.Length;
 
                return _msgBytes;
            }
        }
        #endregion

        #region 初始化 
        /// <summary>
        /// 将字节数组转换为登录信息类
        /// </summary>
        /// <param name="Data">字节数组</param>
        public LoginInfo(byte[] Data)
        {
            if (Data.Length != getMsgLength) return;//如果不是合法数据信息，则退出

            int dstOffSet = 0;

            Buffer.BlockCopy(Data, dstOffSet, _userID, 0, _userID.Length);
            dstOffSet += _userID.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userPassword, 0, _userPassword.Length);
            dstOffSet += _userPassword.Length;

            Buffer.BlockCopy(Data, dstOffSet, _softwareVersion, 0, _softwareVersion.Length);
            dstOffSet += _softwareVersion.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userLocalIP, 0, _userLocalIP.Length);
            dstOffSet += _userLocalIP.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userLocalPort, 0, _userLocalPort.Length);
            dstOffSet += _userLocalPort.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userNetClass, 0, _userNetClass.Length);
            dstOffSet += _userNetClass.Length;
         }

        /// <summary>
        /// 初始化
        /// </summary>
        public LoginInfo()
        {

        }

        /// <summary>
        /// 初始化登录信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="password">用户密码</param>
        /// <param name="SoftwareVersion">用户客户端版本号</param>
        /// <param name="UserLocalIP">用户局域网IP</param>
        /// <param name="UserLocalPort">用户局域网UDP端口</param>
        public LoginInfo(string userID,string password, string SoftwareVersion, System.Net.IPAddress UserLocalIP, int UserLocalPort)
        {
            this.UserID = userID;
            this.userPassword = password;
            this.softwareVersion = SoftwareVersion;
            this.userLocalIP = UserLocalIP;
            this.userLocalPort = UserLocalPort;
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
                int msgCount = _userID.Length + _userPassword.Length+_softwareVersion.Length
                               +_userLocalIP.Length + _userLocalPort.Length + _userNetClass.Length ;
                return msgCount;
            }
        }
        #endregion
    }
}
