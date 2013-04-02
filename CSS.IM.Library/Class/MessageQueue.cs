using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library.Class
{
    #region 接收到的消息类
    /// <summary>
    /// 接收到的消息类
    /// </summary>
    public class UDPReceiveMessage
    {
        /// <summary>
        /// 消息类
        /// </summary>
        public CSS.IM.Library.Class.Msg Msg;//消息ID
        /// <summary>
        /// 发送者IP
        /// </summary>
        public System.Net.IPAddress Ip;
        /// <summary>
        /// 发送者端口
        /// </summary>
        public int Port = 0;
        /// <summary>
        /// 发送者网络类型
        /// </summary>
        public byte netclass= 0; 
        /// <summary>
        /// 发送者套接字对像
        /// </summary>
        public object Sender;
        /// <summary>
        /// 是否接收
        /// </summary>
        public bool isReceived ;
        /// <summary>
        /// 收到的数据
        /// </summary>
        public byte[] Data;

        /// <summary>
        /// 初始化接收消息队列
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ip">发送者IP</param>
        /// <param name="port">发送者端口</param>
        /// <param name="sender">发送者套接字对像</param>
        public UDPReceiveMessage(CSS.IM.Library.Class.Msg msg, System.Net.IPAddress ip, int port, object sender)
        {
            this.Msg = msg;
            this.Ip = ip;
            this.Port = port;
            this.Sender = sender;
        }

        /// <summary>
        /// 初始化接收消息队列
        /// </summary>
        /// <param name="data">收到的数据</param>
        /// <param name="ip">发送者IP</param>
        /// <param name="port">发送者端口</param>
        public UDPReceiveMessage(byte [] data, System.Net.IPAddress ip, int port )
        {
            this.Data = data;
            this.Ip = ip;
            this.Port = port;
        }
    }
    #endregion

    #region 发送消息类
    /// <summary>
    /// 发送消息类
    /// </summary>
    public class UDPSendMessage 
    {
        /// <summary>
        /// 消息 ID
        /// </summary>
        public string ID; 
        /// <summary>
        ///  接收者ID
        /// </summary>
        public string UserID; 
        /// <summary>
        /// 接收者索引
        /// </summary>
        public int UserIndex=0;
        /// <summary>
        /// 已发送次数
        /// </summary>
        public byte SendCount=0;
        /// <summary>
        /// 发送的消息数据
        /// </summary>
        public byte[] MessageData; 

        /// <summary>
        /// 初始化发送的消息
        /// </summary>
        /// <param name="userIndex">用户索引</param>
        /// <param name="userID">用户ID</param>
        /// <param name="msgData">消息数据</param>
        public UDPSendMessage(int userIndex, string userID, byte[] msgData)
        {
            this.UserIndex = userIndex;
            this.UserID = userID;
            this.MessageData = msgData;
        }

        /// <summary>
        /// 初始化发送的消息
        /// </summary>
        /// <param name="userIndex">用户索引</param>
        /// <param name="msgData">消息数据</param>
        public UDPSendMessage(int userIndex, byte[] msgData)
        {
            this.UserIndex = userIndex;
            this.MessageData = msgData;
        }
    }
    #endregion
}
