using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library.Class 
{
    /// <summary>
    /// 群组基本信息
    /// </summary>
    public  class GroupInfo
    {
        #region 变量区
        private byte[] groupID = new byte[4];//组号
        private byte[] groupName = new byte[20];//组名
        private byte[] createUserID = new byte[20];//创建者ID
        private byte[] notice = new byte[100];//组公告
        private byte[] usersLength = new byte[2];//用户ID集字节长度
        private byte[] users  = new byte[0];//组用户索引，表示当前发送或接收到的用户在组中的索引位置
        #endregion

        #region 设置或获取组号
        /// <summary>
        /// 设置或获取组号
        /// </summary>
        public int  GroupID
        {
            set
            {
                this.groupID = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this.groupID, 0);
            }
        }
        #endregion

        #region 设置或获取组名
        /// <summary>
        /// 设置或获取组名 
        /// </summary>
        public string GroupName
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this.groupName.Length)
                    Buffer.BlockCopy(buf, 0, this.groupName, 0, this.groupName.Length); 
                else
                    Buffer.BlockCopy(buf, 0, this.groupName, 0, buf.Length); 
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this.groupName).Trim('\0'); }
        }
        #endregion

        #region 设置或获取创建者ID
        /// <summary>
        /// 设置或获取创建者ID 
        /// </summary>
        public string CreateUserID
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this.createUserID.Length)
                    Buffer.BlockCopy(buf, 0, this.createUserID, 0, this.createUserID.Length);
                else
                    Buffer.BlockCopy(buf, 0, this.createUserID, 0, buf.Length);
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this.createUserID).Trim('\0'); }
        }
        #endregion

        #region 设置或获取组公告
        /// <summary>
        /// 设置或获取组公告 
        /// </summary>
        public string Notice
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this.notice.Length)
                    Buffer.BlockCopy(buf, 0, this.notice, 0, this.notice.Length);  
                else
                    Buffer.BlockCopy(buf, 0, this.notice, 0, buf.Length);  
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this.notice).Trim('\0'); }
        }
        #endregion

        #region 获取或设置组成员
        /// <summary>
        /// 获取或设置组成员
        /// </summary>
        public byte[] Users
        {
            set
            {
                this.usersLength = BitConverter.GetBytes((ushort)value.Length);//两个字节，获得用户ID集字节长度
                this.users = value;
            }
            get
            {
                return  this.users ;
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
                byte[] _msgBytes = new byte[this.getMsgLength];
                int dstOffSet = 0;

                Buffer.BlockCopy(groupID, 0, _msgBytes, dstOffSet, groupID.Length);
                dstOffSet += groupID.Length;

                Buffer.BlockCopy(groupName, 0, _msgBytes, dstOffSet, groupName.Length);
                dstOffSet += groupName.Length;

                Buffer.BlockCopy(createUserID, 0, _msgBytes, dstOffSet, createUserID.Length);
                dstOffSet += createUserID.Length;

                Buffer.BlockCopy(notice, 0, _msgBytes, dstOffSet, notice.Length );
                dstOffSet += notice.Length;

                Buffer.BlockCopy(usersLength, 0, _msgBytes, dstOffSet, usersLength.Length);
                dstOffSet += usersLength.Length;

                Buffer.BlockCopy(users, 0, _msgBytes, dstOffSet, users.Length );
                dstOffSet += users.Length;

                return _msgBytes;
            }
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 将字节数组初始化为群组基本信息类
        /// </summary>
        /// <param name="Data"></param>
        public  GroupInfo(byte[] Data)
        {
            int dstOffSet = 0;

            Buffer.BlockCopy(Data, dstOffSet, groupID, 0, groupID.Length );
            dstOffSet += groupID.Length;

            Buffer.BlockCopy(Data, dstOffSet, groupName, 0, groupName.Length );
            dstOffSet += groupName.Length;

            Buffer.BlockCopy(Data, dstOffSet, createUserID, 0, createUserID.Length);
            dstOffSet += createUserID.Length;

            Buffer.BlockCopy(Data, dstOffSet, notice, 0, notice.Length );
            dstOffSet += notice.Length;

            Buffer.BlockCopy(Data, dstOffSet, usersLength, 0, usersLength.Length);
            dstOffSet += usersLength.Length;

            users = new byte[BitConverter.ToUInt16(this.usersLength, 0)];//用户ID集字节长度
            Buffer.BlockCopy(Data, dstOffSet, users, 0, users.Length );
            dstOffSet += users.Length;
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="_groupID">群组ID号</param>
        /// <param name="_groupName">群组名</param>
        /// <param name="_createUserID">创建者ID</param>
        /// <param name="_notice">公告</param>
        /// <param name="_users">参与者，多个参与者用分号隔开</param>
        public  GroupInfo( int _groupID,string  _groupName,string _createUserID,string _notice ,byte[] _users)
        {
            this.GroupID = _groupID;
            this.GroupName = _groupName;// 
            this.CreateUserID = _createUserID;
            this.Notice = _notice;
            this.Users =_users;
         }

        /// <summary>
        /// 初始化
        /// </summary>
        public  GroupInfo( )
        {
 
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
                int msgCount = groupID.Length + groupName.Length + createUserID.Length + notice.Length +  usersLength.Length  + users.Length;
                return msgCount;
            }
        }
        #endregion

    }
}
