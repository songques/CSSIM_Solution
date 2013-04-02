using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library.Class 
{
    /// <summary>
    /// 文件传输消息
    /// </summary>
    public class msgFile 
    {
        //private byte[] _InfoClass = new byte[1];//文件发送消息类别 0
        //private byte[] _sendID = new byte[4];//数据发送者ID标识 1
        //private byte[] _recID = new byte[4];//数据接收者ID 5
        //private byte[] _pSendPos = new byte[8];//标记上次发送的位置 9
        //private byte[] _fileBlockLength = new byte[2];//标识发送的文件块长度 17

        private byte[]  fileBlock = new byte[0];//当前发送的文件块
        private byte[] data = new byte[19];//消息转换后的字节数组

        #region  构造函数逻辑
        /// <summary>
        /// 构造函数逻辑
        /// </summary>
        public msgFile ()
        {
            //
            // TODO: 在此处添加
            //
        }

        /// <summary>
        /// 将字节数组转换为消息类
        /// </summary>
        /// <param name="Data"></param>
        public msgFile (byte[] Data)
        {
            this.data=Data;
        }

        /// <summary>
        /// 初始化消息
        /// </summary>
        /// <param name="infoClass">消息协议类型</param>
        /// <param name="sendId">发送者ID</param>
        /// <param name="recId">接收者ID</param>
        /// <param name="msgPSendPos"></param>
        /// <param name="msgFileBlock"></param>
        public msgFile(byte infoClass,int sendId, int recId,long  msgPSendPos, byte[] msgFileBlock)
        {
            this.InfoClass = infoClass;
            this.SendID  = sendId;
            this.RecID = recId;
            this.pSendPos = msgPSendPos;
            this.FileBlock = msgFileBlock;
        }
        #endregion

        #region 设置或获取消息协议
        /// <summary>
        /// 设置或获取消息协议
        /// </summary>
        public byte InfoClass
        {
            set
            {
                Buffer.SetByte(this.data,0, value);
            }
            get
            {
                return Buffer.GetByte(this.data, 0);
            }
        }
        #endregion

        #region 设置或获取数据发送者ID 
        /// <summary>
        /// 设置或获取数据发送者ID 
        /// </summary>
        public int SendID
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 1, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data,1);
            }
        }
        #endregion

        #region 设置或获取数据接收者ID
        /// <summary>
        /// 设置或获取数据接收者ID 
        /// </summary>
        public int RecID
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 5, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data,5);
            }
        }
        #endregion

        #region 设置或获取上次发送的位置
        /// <summary>
        /// 设置或获取上次发送的位置
        /// </summary>
        public long pSendPos
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 9, 8);
            }
            get
            {
                return BitConverter.ToInt64(this.data, 9);
            }
        }
        #endregion

        #region 设置或获取内容长度
        /// <summary>
        /// 设置或获取内容长度
        /// </summary>
        private ushort FileBlockLength
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 17, 2);
            }
            get
            {
                return BitConverter.ToUInt16(this.data, 17);
            }
        }
        #endregion

        #region 获取或设置数据块内容
        /// <summary>
        /// 获取或设置数据块内容
        /// </summary>
        public byte[] FileBlock  
        {
            set
            {
                this.fileBlock = value;
                this.FileBlockLength = (ushort)value.Length;//两个字节，获得用户发送消息实体的长度
            }
            get
            {
                this.fileBlock = new byte[this.FileBlockLength];
                Buffer.BlockCopy(this.data, 19, this.fileBlock, 0, this.fileBlock.Length);
                return this.fileBlock;
            }
        }
        #endregion

        #region 获取消息字节数组
        /// <summary>
        /// 获得消息字节数组
        /// </summary>
        public byte[] getBytes()
        {
            List<byte> result = new List<byte>();
            result.AddRange(this.data);
            result.AddRange(this.fileBlock);
            this.data= result.ToArray();
            return this.data;
        }
        #endregion

        #region 获取消息字节数组
        /// <summary>
        /// 获取消息字节数组 
        /// </summary>
        public byte[] Data
        {
            get { return data; }
        }
        #endregion
    }
       
}
