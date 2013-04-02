using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class
{
   
    /// <summary>
    /// 音视频通信协议类
    /// </summary>
    public class msgAV
    {
        //private byte[] _InfoClass = new byte[1];//文件发送消息类别 0
        //private byte[] _sendID = new byte[4];//数据发送者ID标识 1
        //private byte[] _recID = new byte[4];//数据接收者ID 5
        //private byte[] _DataBlockLength = new byte[2];//标识发送的数据块长度 9
        //private byte[] _DataTime = new byte[8];//消息时间戳 11
        //private byte[] _Count = new byte[1];//消息拆分的数据包数量 19
        //private byte[] _Index = new byte[1];//消息拆分的数据包索引 20

        private byte[] data = new byte[21];//消息转换后的字节数组 

        private byte[] dataBlock = new byte[0];//当前发送的数据 11

        /// <summary>
        /// 初始化消息
        /// </summary>
        public msgAV()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 初始化消息
        /// </summary>
        /// <param name="Data">要初始化的消息内容</param>
        public msgAV(byte[] Data)  
        {
            this.data = Data;
        }

        /// <summary>
        /// 初始化消息
        /// </summary>
        /// <param name="msgInfoClass">消息类别</param>
        /// <param name="sendId">发送者ID</param>
        /// <param name="recId">接收者ID</param>
        /// <param name="msgDataBlock">数据数组</param>
        public msgAV(byte msgInfoClass, int sendId, int recId, byte[] msgDataBlock)
        {
            this.InfoClass = msgInfoClass;
            this.SendID = sendId;
            this.RecID = recId;
            this.DataBlock = msgDataBlock;
        }

        /// <summary>
        /// 初始化消息
        /// </summary>
        /// <param name="msgInfoClass">消息类别</param>
        /// <param name="sendId">数据发送者ID标识</param>
        /// <param name="recId">数据接收者ID</param>
        /// <param name="timestamp">消息时间戳</param>
        /// <param name="packetCount">消息拆分的数据包数量</param>
        /// <param name="packetIndex">消息拆分的数据包索引</param>
        /// <param name="msgDataBlock"></param>
        public msgAV(byte msgInfoClass, int sendId, int recId, ulong timestamp, byte packetCount, byte packetIndex, byte[] msgDataBlock)
        {
            this.InfoClass = msgInfoClass;
            this.SendID = sendId;
            this.RecID = recId;
            this.Timestamp = timestamp;
            this.PacketCount = packetCount;
            this.PacketIndex = packetIndex;
            this.DataBlock = msgDataBlock;
        }

        #region 设置或获取文件发送消息协议
        /// <summary>
        /// 设置或获取文件发送消息协议
        /// </summary>
        public byte InfoClass
        {
            set
            {
                Buffer.SetByte(this.data, 0, value);
            }
            get
            {
                return Buffer.GetByte(this.data, 0);
            }
        }
        #endregion

        #region 设置或获取数据发送者服务ID
        /// <summary>
        /// 设置或获取数据发送者服务ID 
        /// </summary>
        public int SendID
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 1, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data, 1);
            }
        }
        #endregion

        #region 设置或获取数据接收者服务ID
        /// <summary>
        /// 设置或获取数据接收者服务ID 
        /// </summary>
        public int RecID
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 5, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data, 5);
            }
        }
        #endregion

        #region 设置或获取内容长度
        /// <summary>
        /// 设置或获取内容长度
        /// </summary>
        public ushort DataBlockLength
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 9, 2);
            }
            get
            {
                return BitConverter.ToUInt16(this.data, 9);
            }
        }
        #endregion

        #region 设置或获取时间戳
        /// <summary>
        /// 设置或获取时间戳
        /// </summary>
        public ulong Timestamp
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 11, 8);
            }
            get
            {
                return BitConverter.ToUInt64(this.data, 11);
            }
        }
        #endregion

        #region 设置或获取文件发送消息拆分的数据包数量
        /// <summary>
        /// 设置或获取文件发送消息拆分的数据包数量
        /// </summary>
        public byte PacketCount
        {
            set
            {
                Buffer.SetByte(this.data, 19, value);
            }
            get
            {
                return Buffer.GetByte(this.data, 19);
            }
        }
        #endregion

        #region 设置或获取文件发送消息拆分的数据包索引
        /// <summary>
        /// 设置或获取文件发送消息拆分的数据包索引
        /// </summary>
        public byte PacketIndex
        {
            set
            {
                Buffer.SetByte(this.data, 20, value);
            }
            get
            {
                return Buffer.GetByte(this.data, 20);
            }
        }
        #endregion

        #region 获取或设置文件块内容
        /// <summary>
        /// 获取或设置文件块内容
        /// </summary>
        public byte[] DataBlock
        {
            set
            {
                this.dataBlock = value;
                this.DataBlockLength = (ushort)value.Length;//两个字节，获得用户发送消息实体的长度
            }
            get
            {
                this.dataBlock = new byte[this.DataBlockLength];
                Buffer.BlockCopy(this.data, 21, this.dataBlock, 0, this.dataBlock.Length);
                return this.dataBlock;
            }
        }
        #endregion

        #region 设置或获取消息字节数组
        /// <summary>
        /// 获得消息字节数组
        /// </summary>
        public byte[] getBytes()
        {
            List<byte> result = new List<byte>();
            result.AddRange(this.data);
            result.AddRange(this.dataBlock);
            this.data = result.ToArray();
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
