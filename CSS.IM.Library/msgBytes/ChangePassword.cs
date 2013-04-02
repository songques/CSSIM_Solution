using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library.Class 
{
    /// <summary>
    /// 修改密码信息类
    /// </summary>
    public class ChangePassword
    {
        #region 变量区
        private byte[] _userID = new byte[20];//登录用户ID
        private byte[] _userNewPassword = new byte[32];//登录用户密码
        private byte[] _userOldPassword = new byte[32];//登录用户密码
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
                int msgCount = _userID.Length + _userNewPassword.Length + _userOldPassword.Length;
                return msgCount;
            }
        }
        #endregion

        #region 设置或获取用户ID
        /// <summary>
        /// 设置或获取登录用户ID
        /// </summary>
        public string UserID
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._userID.Length)
                    Buffer.BlockCopy(buf, 0, this._userID, 0, this._userID.Length);
                else
                    Buffer.BlockCopy(buf, 0, this._userID, 0, buf.Length);
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this._userID).Trim('\0'); }
        }
        #endregion

        #region 设置或获取登录用户新密码
        /// <summary>
        /// 设置或获取登录用户新密码 
        /// </summary>
        public string NewPassword
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._userNewPassword.Length)
                    Buffer.BlockCopy(buf, 0, this._userNewPassword, 0, this._userNewPassword.Length);
                else
                    Buffer.BlockCopy(buf, 0, this._userNewPassword, 0, buf.Length);
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this._userNewPassword).Trim('\0'); }
        }
        #endregion

        #region 设置或获取登录用户旧密码
        /// <summary>
        /// 设置或获取登录用户旧密码 
        /// </summary>
        public string OldPassword
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._userNewPassword.Length)
                    Buffer.BlockCopy(buf, 0, this._userOldPassword, 0, this._userOldPassword.Length);
                else
                    Buffer.BlockCopy(buf, 0, this._userOldPassword, 0, buf.Length);
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this._userOldPassword).Trim('\0'); }
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

                Buffer.BlockCopy(_userNewPassword, 0, _msgBytes, dstOffSet, _userNewPassword.Length);
                dstOffSet += _userNewPassword.Length;

                Buffer.BlockCopy(_userOldPassword, 0, _msgBytes, dstOffSet, _userOldPassword.Length);
                dstOffSet += _userOldPassword.Length;

                return _msgBytes;
            }
        }
        #endregion

        #region 初始化消息类
        /// <summary>
        /// 将字节数组转换为修改密码信息类
        /// </summary>
        /// <param name="Data"></param>
        public  ChangePassword(byte[] Data)
        {
            if (Data.Length != getMsgLength) return;//如果不是合法数据信息，则退出

            int dstOffSet = 0;

            Buffer.BlockCopy(Data, dstOffSet, _userID, 0, _userID.Length);
            dstOffSet += _userID.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userNewPassword, 0, _userNewPassword.Length);
            dstOffSet += _userNewPassword.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userOldPassword, 0, _userOldPassword.Length);
            dstOffSet += _userOldPassword.Length;
         }
        #endregion

        #region 初始化类
        /// <summary>
        /// 初始化类
        /// </summary>
        public ChangePassword()
        {

        }

        /// <summary>
        /// 初始化修改密码信息类
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="userNewPassword">用户的新密码</param>
        /// <param name="userOldPassword">用户的旧密码</param>
        public  ChangePassword(string userID, string userNewPassword, string userOldPassword)
        {
            this.UserID = userID;
            this.NewPassword = userNewPassword;
            this.OldPassword = userOldPassword;
        }
        #endregion

    }
}
