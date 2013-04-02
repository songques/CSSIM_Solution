using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class 
{
    /// <summary>
    /// 文件传输用户
    /// </summary>
    public class FileTransmitUser
    {
        /// <summary>
        /// 服务器上的索引
        /// </summary>
        public int index; 
        /// <summary>
        /// 是否断开
        /// </summary>
        public bool IsUse;
        /// <summary>
        /// 网络类型
        /// </summary>
        public NatClass NetClass; 
        /// <summary>
        /// TCP连接类
        /// </summary>
        public object  myTcp; 
        /// <summary>
        /// UDP IP地址
        /// </summary>
        public System.Net.IPAddress IP; 
        /// <summary>
        /// udp端口
        /// </summary>
        public int Port; 

        /// <summary>
        /// 
        /// </summary>
        public FileTransmitUser()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userTcp"></param>
        public FileTransmitUser(object  userTcp)
        {
            this.myTcp = userTcp;
            NetClass = NatClass.Tcp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public FileTransmitUser(System.Net.IPAddress ip,int port)
        {
            this.IP  = ip ;
            this.Port = port;
            this.NetClass  =  NatClass.FullCone ;
        }
    }

    /// <summary>
    /// 文件传输用户集合
    /// </summary>
    public class FileTransmitUsers : System.Collections.CollectionBase
    {
        /// <summary>
        /// 
        /// </summary>
        public FileTransmitUsers()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public FileTransmitUser this[int index]
        {
            get
            {
                return ((FileTransmitUser)InnerList[index]);
            }
        }

        /// <summary>
        /// 添加一个文件传输中转用户
        /// </summary>
        /// <param name="fileTransmitUser">中转用户网络信息</param>
        public void  add(FileTransmitUser fileTransmitUser)
        {
           base.InnerList.Add(fileTransmitUser);
           fileTransmitUser.index = this.Count - 1;
        }

        /// <summary>
        /// 从在线文件传输中转用户集合中删除一个用户
        /// </summary>
        /// <param name="fileTransmitUser">要删除的用户</param>
        public void Romove(FileTransmitUser fileTransmitUser)
        {
            base.InnerList.Remove(fileTransmitUser);
        }

        /// <summary>
        /// 查找中转用户集合中未使用的用户
        /// </summary>
        /// <returns></returns>
        public FileTransmitUser findNoUsefileTransmitUser()
        {
            foreach (FileTransmitUser user in this)
                if (user.IsUse==false)
                    return user;
            return null;
        }

     
    }
}
