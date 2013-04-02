using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class
{

    //public delegate void DataArrivalEventHandler(object sender, DataArrivalEventArgs e);
    /// <summary>
    /// 数据达到事件参数。 
    /// </summary>
    public class DataArrivalEventArgs : System.EventArgs
    {

        /// <summary>
        ///  消息
        /// </summary>
        public Msg msg;
         

        /// <summary>
        /// 收到的数据
        /// </summary>
        public byte[] Data;

        /// <summary>
        /// 发送方的UDP端口
        /// </summary>
        public int Port;

        /// <summary>
        /// 发送方的IP地址
        /// </summary>
        public System.Net.IPAddress IP;

        /// <summary>
        /// 网络类型
        /// </summary>
        public  NatClass  NetClass=NatClass.FullCone ;

        /// <summary>
        /// Sock对像
        /// </summary>
        public object SockObj  ;

        /// <summary>
        /// <param name="data">接收到的数据字节数组</param>
        /// </summary>
        public DataArrivalEventArgs(byte[] data)
        {
            this.Data = data;
        }

        /// <summary>
        /// <param name="ip">发送数据方的IP地址</param>
        /// <param name="port">发送数据方的端口号</param>
        /// </summary>
        public DataArrivalEventArgs(System.Net.IPAddress ip, int port)
        { 
            this.IP = ip;
            this.Port = port;
        }

        /// <summary>
        /// <param name="data">接收到的数据字节数组</param>
        /// <param name="ip">发送数据方的IP地址</param>
        /// <param name="port">发送数据方的端口号</param>
        /// </summary>
        public DataArrivalEventArgs(byte[] data, System.Net.IPAddress ip, int port)
        {
            this.Data = data;
            this.IP = ip;
            this.Port = port;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public DataArrivalEventArgs( Msg  message)
        {
            this.msg = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public DataArrivalEventArgs( Msg message, System.Net.IPAddress ip, int port)
        {
            this.msg = message;
            this.IP = ip;
            this.Port = port;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="netClass"></param>
        /// <param name="Sock"></param>
        public DataArrivalEventArgs( Msg message, System.Net.IPAddress ip, int port,NatClass netClass,object Sock)
        {
            this.msg = message;
            this.IP = ip;
            this.Port = port;
            this.NetClass = netClass;
            this.SockObj = Sock;
        }

    }

}
