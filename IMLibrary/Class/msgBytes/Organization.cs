using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class 
{
    /// <summary>
    /// 组织、用户版本号 
    /// </summary>
    [Serializable]
    public class Organization
    {
       #region 变量区
        private byte[] _depVersion= new byte[32];//组织机构版本 MD5
        private byte[] _depCount = new byte[4];//组织机构部门总数
        private byte[] _UserVersion = new byte[32];//用户版本 MD5
        private byte[] _UserCount = new byte[4];//用户总数

        private byte[] _mtu = new byte[2];//到服务器端MTU值

        private byte[] _isSendSMS = new byte[1];//标识是否允许发送短消息
        private byte[] _editUserData = new byte[1];//标识是否允许编辑用户资料

        private byte[] _serverFileUDPPort=new byte[4];//文件服务器中转UDP服务端口
        private byte[] _serverFileTCPPort = new byte[4];//文件服务器中转TCP服务端口
        private byte[] _serverAVUDPPort = new byte[4];//视频服务器中转UDP服务端口
        private byte[] _serverAVTCPPort = new byte[4];//视频服务器中转TCP服务端口

        private byte[] _lastLoginIP = new byte[4];//获取用户最后登录IP
        private byte[] _lastLoginDate = new byte[24];//获取用户最后登录时间
        private byte[] _onlineLength=new byte[4];//在线时长
        private byte[] _HardDiskURI = new byte[100];//获得网络硬盘根目录的URI
        private byte[] _HardDiskUserName = new byte[20];//获得网络硬盘的用户名
        private byte[] _HardDiskPassword= new byte[32];//获得网络硬盘的用户密码
        private byte[] _HardDiskDomain = new byte[100];//获得网络硬盘的域
        #endregion

       #region 设置或获取组织机构版本 MD5
        /// <summary>
        /// 设置或获取组织机构版本 MD5
        /// </summary>
        public string DepVersion
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._depVersion.Length)
                    Buffer.BlockCopy(buf, 0, this._depVersion, 0, this._depVersion.Length);//获得消息发送者ID 
                else
                    Buffer.BlockCopy(buf, 0, this._depVersion, 0, buf.Length);//获得消息发送者ID 
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._depVersion).Trim('\0'); }
        }
        #endregion

       #region 设置或获取组织机构部门总数
        /// <summary>
        /// 设置或获取组织机构部门总数
        /// </summary>
        public int DepCount
        {
            set
            {
                this._depCount = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._depCount, 0);
            }
        }
        #endregion

       #region 设置或获取用户版本 MD5
        /// <summary>
        /// 设置或获取用户版本 MD5
        /// </summary>
        public string UserVersion
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._UserVersion.Length)
                    Buffer.BlockCopy(buf, 0, this._UserVersion, 0, this._UserVersion.Length);//获得消息发送者ID 
                else
                    Buffer.BlockCopy(buf, 0, this._UserVersion, 0, buf.Length);//获得消息发送者ID 
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._UserVersion).Trim('\0'); }
        }
        #endregion

       #region 设置或获取用户总数
        /// <summary>
        /// 设置或获取用户总数
        /// </summary>
        public int UserCount
        {
            set
            {
                this._UserCount = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._UserCount, 0);
            }
        }
        #endregion

       #region 设置或获取用户MTU值
        /// <summary>
        /// 设置或获取用户总数
        /// </summary>
        public ushort MTU
        {
            set
            {
                this._mtu = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToUInt16(this._mtu, 0);
            }
        }
        #endregion

       #region 设置或获取用户发送短信的权限
        /// <summary>
        /// 设置或获取用户发送短信的权限
        /// </summary>
        public byte IsSendSMS
        {
            set
            {
                this._isSendSMS[0] = value;
            }
            get
            {
                return this._isSendSMS[0];
            }
        }
        #endregion

       #region 设置或获取用户编辑用户资料的权限
        /// <summary>
        /// 设置或获取用户编辑用户资料的权限
        /// </summary>
        public byte  EditUserData
        {
            set
            {
                this._editUserData[0] = value;
            }
            get
            {
                return this._editUserData[0];
            }
        }
        #endregion

       #region 文件服务器中转UDP服务端口
        /// <summary>
        /// 文件服务器中转UDP服务端口
        /// </summary>
        public int  ServerFileUDPPort
        {
            set
            {
                this._serverFileUDPPort = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._serverFileUDPPort, 0);
            }
        }
        #endregion

       #region 文件服务器中转TCP服务端口
        /// <summary>
        /// 文件服务器中转TCP服务端口
        /// </summary>
        public int ServerFileTCPPort
        {
            set
            {
                this._serverFileTCPPort = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._serverFileTCPPort, 0);
            }
        }
        #endregion

       #region 视频服务器中转UDP服务端口
        /// <summary>
        /// 视频服务器中转UDP服务端口
        /// </summary>
        public int ServerAVUDPPort
        {
            set
            {
                this._serverAVUDPPort = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._serverAVUDPPort, 0);
            }
        }
        #endregion

       #region 视频服务器中转TCP服务端口
        /// <summary>
        /// 视频服务器中转TCP服务端口
        /// </summary>
        public int ServerAVTCPPort
        {
            set
            {
                this._serverAVTCPPort = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._serverAVTCPPort, 0);
            }
        }
        #endregion

       #region 设置或获取用户最后一次登录IP
        /// <summary>
        /// 设置或获取用户最后一次登录IP
        /// </summary>
        public System.Net.IPAddress LastLoginIP
        {
            set
            {
                this._lastLoginIP = IMLibrary.Class.IP.IPAddressToBytes(value);  //将IP转换成32位整型
            }
            get
            {
                return IMLibrary.Class.IP.BytesToIPAddress(this._lastLoginIP);//将int转换成IP地址
            }
        }
        #endregion

       #region 设置或获取用户最后一次登录时间
        /// <summary>
        /// 设置或获取用户最后一次登录时间
        /// </summary>
        public string  LastLoginDate
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._lastLoginDate.Length)
                    Buffer.BlockCopy(buf, 0, this._lastLoginDate, 0, this._lastLoginDate.Length);//获得消息发送者ID 
                else
                    Buffer.BlockCopy(buf, 0, this._lastLoginDate, 0, buf.Length);//获得消息发送者ID 
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._lastLoginDate).Trim('\0'); }
        }
       #endregion

       #region 设置或获取用户在线时长
        /// <summary>
        /// 设置或获取用户在线时长
        /// </summary>
        public int OnlineLength
        {
            set
            {
                this._onlineLength = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._onlineLength, 0);
            }
        }
        #endregion

       #region 设置或获取用户网络硬盘的URI
        /// <summary>
        /// 设置或获取用户网络硬盘的URI
        /// </summary>
        public string  HardDiskURI
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._HardDiskURI.Length)
                    Buffer.BlockCopy(buf, 0, this._HardDiskURI, 0, this._HardDiskURI.Length);//获得消息发送者ID 
                else
                    Buffer.BlockCopy(buf, 0, this._HardDiskURI, 0, buf.Length);//获得消息发送者ID 
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._HardDiskURI).Trim('\0'); }
        }
        #endregion

       #region 设置或获取用户网络硬盘的用户名
        /// <summary>
        /// 设置或获取用户网络硬盘的用户名
        /// </summary>
        public string HardDiskUserName
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._HardDiskUserName.Length)
                    Buffer.BlockCopy(buf, 0, this._HardDiskUserName, 0, this._HardDiskUserName.Length);//获得消息发送者ID 
                else
                    Buffer.BlockCopy(buf, 0, this._HardDiskUserName, 0, buf.Length);//获得消息发送者ID 
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._HardDiskUserName).Trim('\0'); }
        }
        #endregion

       #region 设置或获取用户网络硬盘的用户密码
        /// <summary>
        /// 设置或获取用户网络硬盘的用户密码
        /// </summary>
        public string HardDiskPassword
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._HardDiskPassword.Length)
                    Buffer.BlockCopy(buf, 0, this._HardDiskPassword, 0, this._HardDiskPassword.Length);//获得消息发送者ID 
                else
                    Buffer.BlockCopy(buf, 0, this._HardDiskPassword, 0, buf.Length);//获得消息发送者ID 
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._HardDiskPassword).Trim('\0'); }
        }
        #endregion

       #region 设置或获取用户网络硬盘的网络域名
        /// <summary>
        /// 设置或获取用户网络硬盘的网络域名
        /// </summary>
        public string HardDiskDomain
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._HardDiskDomain.Length)
                    Buffer.BlockCopy(buf, 0, this._HardDiskDomain, 0, this._HardDiskDomain.Length);//获得消息发送者ID 
                else
                    Buffer.BlockCopy(buf, 0, this._HardDiskDomain, 0, buf.Length);//获得消息发送者ID 
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._HardDiskDomain).Trim('\0'); }
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
                byte[] _msgBytes = new byte[this.getMsgLength];

                int dstOffSet = 0;

                Buffer.BlockCopy(_depVersion, 0, _msgBytes, dstOffSet, _depVersion.Length);//组织机构版本
                dstOffSet += _depVersion.Length;

                Buffer.BlockCopy(_depCount, 0, _msgBytes, dstOffSet, _depCount.Length);//部门数
                dstOffSet += _depCount.Length;

                Buffer.BlockCopy(_UserVersion, 0, _msgBytes, dstOffSet, _UserVersion.Length);//用户版本
                dstOffSet += _UserVersion.Length;

                Buffer.BlockCopy(_UserCount, 0, _msgBytes, dstOffSet, _UserCount.Length);//用户数
                dstOffSet += _UserCount.Length;

                Buffer.BlockCopy(_mtu, 0, _msgBytes, dstOffSet, _mtu.Length);//获取MTU值
                dstOffSet += _mtu.Length;

                Buffer.BlockCopy(_isSendSMS, 0, _msgBytes, dstOffSet, _isSendSMS.Length);//
                dstOffSet += _isSendSMS.Length;

                Buffer.BlockCopy(_editUserData, 0, _msgBytes, dstOffSet, _editUserData.Length);//
                dstOffSet += _editUserData.Length;

                Buffer.BlockCopy(_serverFileUDPPort, 0, _msgBytes, dstOffSet, _serverFileUDPPort.Length);//
                dstOffSet += _serverFileUDPPort.Length;

                Buffer.BlockCopy(_serverFileTCPPort, 0, _msgBytes, dstOffSet, _serverFileTCPPort.Length);//
                dstOffSet += _serverFileTCPPort.Length;

                Buffer.BlockCopy(_serverAVUDPPort, 0, _msgBytes, dstOffSet, _serverAVUDPPort.Length);//
                dstOffSet += _serverAVUDPPort.Length;

                Buffer.BlockCopy(_serverAVTCPPort, 0, _msgBytes, dstOffSet, _serverAVTCPPort.Length);//
                dstOffSet += _serverAVTCPPort.Length;

                Buffer.BlockCopy(_lastLoginIP, 0, _msgBytes, dstOffSet, _lastLoginIP.Length);//用户最后登录IP
                dstOffSet += _lastLoginIP.Length;

                Buffer.BlockCopy(_lastLoginDate, 0, _msgBytes, dstOffSet, _lastLoginDate.Length);//获取用户最后登录时间 
                dstOffSet += _lastLoginDate.Length;

                Buffer.BlockCopy(_onlineLength, 0, _msgBytes, dstOffSet, _onlineLength.Length);//用户在线时长
                dstOffSet += _onlineLength.Length;

                Buffer.BlockCopy(_HardDiskURI, 0, _msgBytes, dstOffSet, _HardDiskURI.Length);//获得网络硬盘根目录的URI
                dstOffSet += _HardDiskURI.Length;

                Buffer.BlockCopy(_HardDiskUserName, 0, _msgBytes, dstOffSet, _HardDiskUserName.Length);//获得网络硬盘的用户名 
                dstOffSet += _HardDiskUserName.Length;

                Buffer.BlockCopy(_HardDiskPassword, 0, _msgBytes, dstOffSet, _HardDiskPassword.Length);//获得网络硬盘的用户密码
                dstOffSet += _HardDiskPassword.Length;

                Buffer.BlockCopy(_HardDiskDomain, 0, _msgBytes, dstOffSet, _HardDiskDomain.Length);//获得网络硬盘的网络域名
                dstOffSet += _HardDiskDomain.Length;

                return _msgBytes;
            }
        }
        #endregion

       #region 初始化 
        /// <summary>
        /// 将字节数组转换为组织机构版本信息
        /// </summary>
        /// <param name="Data"></param>
        public Organization(byte[] Data)
        {
            if (Data.Length != this.getMsgLength ) return;//如果不是部门信息，则退出

            int dstOffSet = 0;

            Buffer.BlockCopy(Data, dstOffSet, _depVersion, 0, _depVersion.Length);
            dstOffSet += _depVersion.Length;

            Buffer.BlockCopy(Data, dstOffSet, _depCount, 0, _depCount.Length);
            dstOffSet += _depCount.Length;

            Buffer.BlockCopy(Data, dstOffSet, _UserVersion, 0, _UserVersion.Length);
            dstOffSet += _UserVersion.Length;

            Buffer.BlockCopy(Data, dstOffSet, _UserCount, 0, _UserCount.Length);
            dstOffSet += _UserCount.Length;

            Buffer.BlockCopy(Data, dstOffSet, _mtu , 0, _mtu.Length);
            dstOffSet += _mtu.Length;

            Buffer.BlockCopy(Data, dstOffSet, _isSendSMS, 0, _isSendSMS.Length);
            dstOffSet += _isSendSMS.Length;

            Buffer.BlockCopy(Data, dstOffSet, _editUserData, 0, _editUserData.Length);
            dstOffSet += _editUserData.Length;


            Buffer.BlockCopy(Data, dstOffSet, _serverFileUDPPort, 0, _serverFileUDPPort.Length);
            dstOffSet += _serverFileUDPPort.Length;

            Buffer.BlockCopy(Data, dstOffSet, _serverFileTCPPort, 0, _serverFileTCPPort.Length);
            dstOffSet += _serverFileTCPPort.Length;

            Buffer.BlockCopy(Data, dstOffSet, _serverAVUDPPort, 0, _serverAVUDPPort.Length);
            dstOffSet += _serverAVUDPPort.Length;

            Buffer.BlockCopy(Data, dstOffSet, _serverAVTCPPort, 0, _serverAVTCPPort.Length);
            dstOffSet += _serverAVTCPPort.Length;


            Buffer.BlockCopy(Data, dstOffSet, _lastLoginIP, 0, _lastLoginIP.Length);
            dstOffSet += _lastLoginIP.Length;

            Buffer.BlockCopy(Data, dstOffSet, _lastLoginDate, 0, _lastLoginDate.Length);
            dstOffSet += _lastLoginDate.Length;

            Buffer.BlockCopy(Data, dstOffSet, _onlineLength, 0, _onlineLength.Length);
            dstOffSet += _onlineLength.Length;

            Buffer.BlockCopy(Data, dstOffSet, _HardDiskURI, 0, _HardDiskURI.Length);
            dstOffSet += _HardDiskURI.Length;

            Buffer.BlockCopy(Data, dstOffSet, _HardDiskUserName, 0, _HardDiskUserName.Length);
            dstOffSet += _HardDiskUserName.Length;

            Buffer.BlockCopy(Data, dstOffSet, _HardDiskPassword, 0, _HardDiskPassword.Length);
            dstOffSet += _HardDiskPassword.Length;

            Buffer.BlockCopy(Data, dstOffSet, _HardDiskDomain, 0, _HardDiskDomain.Length);
            dstOffSet += _HardDiskDomain.Length;

        }
        #endregion

       #region 初始化类
        /// <summary>
        /// 初始化类
        /// </summary>
        public Organization()
        {

        }
      
        /// <summary>
        /// 初始化组织、用户版本号消息类
        /// </summary>
        /// <param name="depVersion">组织机构版本号MD5值</param>
        /// <param name="depCount">组织机构部门数</param>
        /// <param name="userVersion">用户版本号MD5值</param>
        /// <param name="userCount">用户数</param>
        public Organization(string depVersion, int depCount, string  userVersion, int  userCount)
        {
            this.DepVersion = depVersion;
            this.DepCount = depCount;
            this.UserVersion = userVersion;
            this.UserCount = userCount;
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
                int msgCount = _depVersion.Length + _depCount.Length + _UserVersion.Length + _UserCount.Length + _mtu.Length 
                    + _isSendSMS.Length + _editUserData.Length + _serverFileUDPPort.Length  + _serverFileTCPPort.Length  + _serverAVUDPPort.Length + _serverAVTCPPort.Length 
                    + _lastLoginIP.Length + _lastLoginDate.Length + _onlineLength.Length + _HardDiskURI.Length
                    + _HardDiskUserName.Length + _HardDiskPassword.Length + _HardDiskDomain.Length;

                return msgCount;
            }
        }
        #endregion

    }
}
