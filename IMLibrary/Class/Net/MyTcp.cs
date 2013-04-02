using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace IMLibrary.Net
{
    #region 封装TCP消息类
    /// <summary>
    /// 封装TCP消息类
    /// </summary>
    sealed  class SealedMsg
    {
        /// <summary>
        ///  封装发送的消息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static  byte[] SendMsgBytes(byte[] data)
        {
            ushort msgLen =Convert.ToUInt16 (data.Length);
            byte[] buf = new byte[msgLen+ 2];
            byte[] msgLenBytes = BitConverter.GetBytes(msgLen);
            Buffer.BlockCopy(msgLenBytes, 0, buf, 0, 2);
            Buffer.BlockCopy(data, 0, buf, 2, data.Length);
            return buf;
        }

        /// <summary>
        /// 将封装的消息还原
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] RecvMsgBytes(byte[] data)
        {
            ushort msgLen =BitConverter.ToUInt16(data,0);
            byte[] buf = new byte[msgLen];
            Buffer.BlockCopy(data,2,buf,0,msgLen);
            return buf;
        }
    }
    #endregion

    #region TCP套接字状态类
    /// <summary>
    /// TCP套接字状态类
    /// </summary>
    public class  MyTcp 
    {
        /// <summary>
        /// 套接字ID
        /// </summary>
        public string ID= Guid.NewGuid().ToString().Replace("-", ""); 
        /// <summary>
        /// 侦听套接字
        /// </summary>
        public System.Net.Sockets.Socket Socket= null;
        /// <summary>
        /// 套接字缓冲区大小
        /// </summary>
        public const int BufferSize = 1400;
        /// <summary>
        /// 套接字缓冲区
        /// </summary>
        public byte[] Buffer = new byte[BufferSize];
        /// <summary>
        /// 定时器
        /// </summary>
        private  System.Timers.Timer timer = new System.Timers.Timer();
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValidate = false;
        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool Closed = false;
        /// <summary>
        /// 接收联接客户列表
        /// </summary>
        public IList<byte> result  ;

        /// <summary>
        /// 获取远程IP
        /// </summary>
        public System.Net.IPAddress IP
        {
            get
            {
                if (Socket != null)
                {
                    System.Net.IPEndPoint ip = (System.Net.IPEndPoint)Socket.RemoteEndPoint;
                     
                    return ip.Address;
                }
                return null;
            }
        }
        
        /// <summary>
        /// 获取远程端口
        /// </summary>
        public int Port
        {
            get
            {
                if (Socket != null)
                {
                    System.Net.IPEndPoint ip = (System.Net.IPEndPoint)Socket.RemoteEndPoint;

                    return ip.Port;
                }
                return 0;
            }
        }

      /// <summary>
      /// 设置对像有效时间
      /// </summary>
        public double ValidateTimeLong
        {
            get { return this.timer.Interval; }
            set { this.timer.Interval = value; }
        }

        /// <summary>
        /// 客户端断开连接事件
        /// </summary>
        public delegate void CloseEventHandler(object sender, IMLibrary.Net.SockEventArgs e);
        /// <summary>
        /// 客户端断开连接事件
        /// </summary>
        public event CloseEventHandler OnClose;

       /// <summary>
       /// 初始化 
       /// </summary>
        public MyTcp()
        {
            this.timer.Interval = 5000;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        }

        /// <summary>
        /// 检测对像有效期的计时器开始工作
        /// </summary>
        public void timerStrat()
        {
            this.timer.Enabled = true;
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (!IsValidate && Socket != null)
                {
                    if (this.Socket.Connected)
                        this.Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);

                    this.Socket.Close();
                    this.Socket = null;

                    if (OnClose != null)
                        OnClose(this, null);
                }
            }
            catch
            {

            }
            timer.Enabled = false;
        }
    }
    #endregion
     
}
