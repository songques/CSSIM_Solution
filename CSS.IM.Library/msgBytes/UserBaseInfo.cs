using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library.Class 
{
    /// <summary>
    /// 用户基本信息类
    /// </summary>
    public class  UserBaseInfo
    {

       #region 变量区
        private byte[] _userID = new byte[20];//
        private byte[] _userName = new byte[20];//
        private byte[] _faceIndex = new byte[1];//
        private byte[] _sex = new byte[1];//
        private byte[] _depID = new byte[4];//
        private byte[] _orderID = new byte[4];//
        #endregion

       #region 设置或获取UserID
        /// <summary>
        ///  设置或获取用户ID
        /// </summary>
        public string UserID
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if(buf.Length>this._userID.Length )
                    Buffer.BlockCopy(buf, 0, this._userID, 0, this._userID.Length);
                else
                    Buffer.BlockCopy(buf, 0, this._userID, 0, buf.Length);
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this._userID).Trim('\0'); }
        }
        #endregion

       #region 设置或获取用户姓名 
        /// <summary>
        ///  设置或获取用户姓名
        /// </summary>
        public string UserName
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._userName.Length )
                    Buffer.BlockCopy(buf, 0, this._userName, 0, this._userName.Length);
                else
                    Buffer.BlockCopy(buf, 0, this._userName, 0, buf.Length);
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this._userName).Trim('\0'); }
        }
        #endregion

       #region 设置或获取用户头像索引
       /// <summary>
       /// 设置或获取用户头像索引
       /// </summary>
        public byte FaceIndex
        {
            set
            {
                this._faceIndex[0] = value ;
            }
            get
            {
                return this._faceIndex[0];
            }
        }
        #endregion

       #region 设置或获取用户Sex
        /// <summary>
        /// 设置获取用户性别
        /// </summary>
        public byte Sex
        {
            set
            {
                this._sex[0] = value ;
            }
            get
            {
                return this._sex[0];
            }
        }
        #endregion

       #region 设置或获取分组ID
        /// <summary>
        /// 设置或获取用户的分组ID
        /// </summary>
        public int DepID
        {
            set
            {
                this._depID = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._depID, 0);
            }
        }
        #endregion

       #region 设置或获取orderID
        /// <summary>
        /// 设置或获取用户在分组内的排序
        /// </summary>
        public int OrderID
        {
            set
            {
                this._orderID = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._orderID, 0);
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
                byte[] _msgBytes = new byte[50];

                int dstOffSet = 0;
 
                Buffer.BlockCopy(_userID, 0, _msgBytes, dstOffSet, _userID.Length);
                dstOffSet += _userID.Length;

                Buffer.BlockCopy(_userName, 0, _msgBytes, dstOffSet, _userName.Length);
                dstOffSet += _userName.Length;

                Buffer.BlockCopy(_faceIndex, 0, _msgBytes, dstOffSet, _faceIndex.Length);
                dstOffSet += _faceIndex.Length;

                Buffer.BlockCopy(_sex, 0, _msgBytes, dstOffSet, _sex.Length);
                dstOffSet += _sex.Length;

                Buffer.BlockCopy(_depID, 0, _msgBytes, dstOffSet, _depID.Length);
                dstOffSet += _depID.Length;

                Buffer.BlockCopy(_orderID, 0, _msgBytes, dstOffSet, _orderID.Length);
                dstOffSet += _orderID.Length;

                return _msgBytes;
            }
        }
        #endregion

       #region 初始化类
        /// <summary>
        /// 将字节数据转换为用户在线信息类
        /// </summary>
        /// <param name="Data"></param>
        public UserBaseInfo(byte[] Data)
        {
            if (Data.Length != 50) return;//如果不是部门信息，则退出

            int dstOffSet = 0;

            Buffer.BlockCopy(Data, dstOffSet, _userID, 0, _userID.Length);
            dstOffSet += _userID.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userName, 0, _userName.Length);
            dstOffSet += _userName.Length;

            Buffer.BlockCopy(Data, dstOffSet, _faceIndex, 0, _faceIndex.Length);
            dstOffSet += _faceIndex.Length;

            Buffer.BlockCopy(Data, dstOffSet, _sex, 0, _sex.Length);
            dstOffSet += _sex.Length;

            Buffer.BlockCopy(Data, dstOffSet, _depID, 0, _depID.Length);
            dstOffSet += _depID.Length;

            Buffer.BlockCopy(Data, dstOffSet, _orderID, 0, _orderID.Length);
            dstOffSet += _orderID.Length;
        }
        
       /// <summary>
       /// 初始化类
       /// </summary>
        public UserBaseInfo()
        {

        }
      
        /// <summary>
        /// 初始化类
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="faceIndex">用户头像索引</param>
        /// <param name="sex">用户性别</param>
        /// <param name="depID">用户分组ID</param>
        /// <param name="orderID">用户在分组内的排序</param>
        public UserBaseInfo(string userID, string userName, byte faceIndex, byte sex, int depID, int orderID)
        {
            this.UserID  = userID;
            this.UserName = userName;
            this.FaceIndex  = faceIndex;
            this.Sex  = sex;
            this.DepID  = depID;
            this.OrderID = orderID;
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
                int msgCount = 50;
                return msgCount;
            }
        }
        #endregion
    }
}
