using System;
using System.Net;

namespace CSS.IM.Library.Class
{

    #region 用户信息类
    /// <summary>
    /// 用户信息类 
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户信息类
        /// </summary>
        public UserInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 用户信息类
        /// </summary>
        /// <param name="UserID">用户ID</param>
        public UserInfo(string UserID)
        {
            this.UserID = UserID;
        }

        /// <summary>
        /// 初始化用户信息类
        /// </summary>
        /// <param name="ID">用户索引</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="UserName">用户姓名</param>
        /// <param name="DepId">用户所在分组ID</param>
        /// <param name="orderID">用户在分组内的排序</param>
        public UserInfo(int ID, string UserID, string UserName, int DepId, int orderID)
        {
            this.Index  = ID;
            this.UserID  = UserID;
            this.userName  = UserName;
            this.depId = DepId;
            this.orderID = orderID;
        }

        #region 事件
        /// <summary>
        /// 用户在线状态改变事件
        /// </summary>
        /// <param name="sender">产生事件的对象</param>
        public delegate void userStateChangedEventHandler(object sender);
        /// <summary>
        /// 用户在线状态改变事件
        /// </summary>
        public event userStateChangedEventHandler userStateChanged;

        /// <summary>
        /// 用户姓名更新事件
        /// </summary>
        /// <param name="sender">产生事件的对象</param>
        public delegate void userNameChangedEventHandler(object sender);
        /// <summary>
        /// 用户姓名更新事件
        /// </summary>
        public event userNameChangedEventHandler userNameChanged;

        /// <summary>
        /// 用户ID更新事件
        /// </summary>
        /// <param name="sender">产生事件的对象</param> 
        public delegate void userIdChangedEventHandler(object sender);
        /// <summary>
        /// 用户ID更新事件
        /// </summary>
        public event userIdChangedEventHandler userIdChanged;

        /// <summary>
        /// 用户部门更新事件
        /// </summary>
        /// <param name="sender">产生事件的对象</param>
        public delegate void depIdChangedEventHandler(object sender);
        /// <summary>
        /// 用户部门更新事件
        /// </summary>
        public event depIdChangedEventHandler depIdChanged;

        /// <summary>
        /// 用户IP地址更新事件
        /// </summary>
        /// <param name="sender">产生事件的对象</param>
        public delegate void IpChangedEventHandler(object sender);
        /// <summary>
        /// 用户IP地址更新事件
        /// </summary>
        public event IpChangedEventHandler IpChanged;

        /// <summary>
        /// 用户Port更新事件
        /// </summary>
        /// <param name="sender">产生事件的对象</param>
        public delegate void PortChangedEventHandler(object sender);
        /// <summary>
        /// 用户Port更新事件
        /// </summary>
        public event PortChangedEventHandler PortChanged;
        #endregion

        /// <summary>
        /// 设置或获取发送的消息是否成功。 
        /// </summary>
        public bool SendIsSuccess = false;//标识发送给此联系人的上一次数据是否成功


        private int _index = 0;
        /// <summary>
        ///  用户在服务器上的数组索引位置
        /// </summary>
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        /// <summary>
        ///  用户头像索引位置
        /// </summary>
        public byte FaceIndex= 0;
         

        /// <summary>
        ///  用户头像索引位置
        /// </summary>
        public byte Sex = 1;
         

        /// <summary>
        ///  用户与服务器的通信密码
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// 用户Tag
        /// </summary>
        public object Tag = null; 


        private string _userID = "";// 标识用户身分唯一的ID（暂用计算机名代替）
        /// <summary>
        /// 系统用户ID。 
        /// </summary>
        public string UserID
        {
            get { return _userID; }
            set
            {
                _userID = value;
                if (userIdChanged != null)
                    userIdChanged(this);
            }
        }
         
        private string _userName = "";
        /// <summary>
        /// 获取或设置用户姓名
        /// </summary>
        public string userName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                if (userNameChanged != null)
                    userNameChanged(this);
            }
        }

 
        private int _depID = 0;//用户所在部门的ID
        /// <summary>
        /// 设置或获取用户所在部门的ID。 
        /// </summary>
        public int depId
        {
            get { return _depID; }
            set
            {
                _depID = value;
                if (depIdChanged != null)
                    depIdChanged(this);
            }
        }

        private int _orderID = 0;//用户所在部门的orderID
        /// <summary>
        /// 设置或获取用户所在部门的ID。 
        /// </summary>
        public int orderID
        {
            get { return _orderID; }
            set
            {
                _orderID = value;
            }
        }

        private IPAddress _Ip = IPAddress.Parse("127.0.0.1");
        /// <summary>
        /// 获取或设置用户的IP地址。 
        /// </summary>
        public IPAddress IP
        {
            get {return _Ip;}
            set
            {
                _Ip = value;
                if (IpChanged != null)
                    IpChanged(this);
            }
        }


        private int _port = 0;
        /// <summary>
        /// 获取或设置用户的端口号。 
        /// </summary>
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                if (PortChanged != null)
                    PortChanged(this);
            }
        }

        /// <summary>
        /// 获取或设置用户的局域网IP地址。 
        /// </summary>
        public IPAddress LocalIP= IPAddress.Parse("127.0.0.1");
        
         
        /// <summary>
        /// 获取或设置用户的局域网端口号。 
        /// </summary>
        public int  LocalPort = 0;
         
        /// <summary>
        /// 发送数据给用户的最大网络传输单元值（MTU）。 
        /// </summary>
        public ushort MTU = 512;
       

        /// <summary>
        /// 标识与用户通信的网络类型（UDP或TCP或其他）
        /// </summary>
        public byte NetClass = (byte)CSS.IM.Library.Class.NatClass.None;
        

        /// <summary>
        /// 用于与服务器通信的TCP组件
        /// </summary>
        public object   myTcp;

        /// <summary>
        /// 标识用户与用户之间在网络上是否直接相联(真为直接相联，数据可以对方，假为不直接相联，数据通过服务器转发)
        /// </summary>
        public bool isConnected = false;
       
         
        /// <summary>
        /// 判断用户与用户之间是否同处一个局域网内
        /// </summary>
        public bool isInLan;

        /// <summary>
        /// UDP打洞次数
        /// </summary>
        public byte UDPPenetrateCount;
        
        /// <summary>
        /// 获取或设置用户的在线状态文本信息。 
        /// </summary>
        public string StateInfo= "(脱机)";
        

        
        /// <summary>
        /// 获取或设置用户的临时在线状态。 
        /// </summary>
        public byte tempState=0;
        

        private byte _state = 0;
        /// <summary>
        /// 获取或设置用户的在线状态。 
        /// </summary>
        public byte State
        {
            get { return _state; }
            set
            {
                switch (value)
                {
                    case 0:
                        this.isConnected  = false;//重新设置与用户为非网络直联
                        this.IP = System.Net.IPAddress.Parse("127.0.0.1");//设置用户IP地址为本机
                        this.NetClass = 0;//设置用户网络类型
                        this.UDPPenetrateCount = 0;//UDP打洞次数清零
                        StateInfo = "(脱机)";
                        break;
                    case 1:
                        StateInfo = "(联机)";
                        break;
                    case 2:
                        StateInfo = "(忙碌)";
                        break;
                    case 4:
                        StateInfo = "(离开)";
                        break;
                    case 3:
                        StateInfo = "(接听电话)";
                        break;
                    case 5:
                        StateInfo = "(外出就餐)";
                        break;
                }

                if (_state != value)//如果在线状态真的改变，则触发事件
                {
                    _state = value; 
                    if (userStateChanged != null)
                        userStateChanged(this);
                }
            }
        }


    }
    #endregion

    #region  用户集合
    /// <summary>
    /// 用户集合 
    /// </summary>
    public class UserCollections : System.Collections.CollectionBase
    {
        /// <summary>
        /// 用户集合
        /// </summary>
        public UserCollections()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        // Get UserInfo at the specified index
        /// <summary>
        /// 获取与设置用户在集合内的索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public UserInfo this[int index]
        {
            get
            {
                return ((UserInfo)InnerList[index]);
            }
        }


        /// <summary>
        /// 向在线用户集合中添加一个用户
        /// </summary>
        /// <param name="_UserInfo">要添加的用户</param>
        public void add(UserInfo _UserInfo)
        {
            base.InnerList.Add(_UserInfo);
            _UserInfo.Index = this.Count-1;
        }
        /// <summary>
        /// 从在线用户集合中删除一个用户
        /// </summary>
        /// <param name="_UserInfo">要删除的用户</param>
        public void Romove(UserInfo _UserInfo)
        {
            base.InnerList.Remove(_UserInfo);
        }

        /// <summary>
        /// 在集合中查找用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>返回用户信息</returns>
        public UserInfo findUser(string userID)
        {
            foreach (UserInfo user in this)
                if (userID == user.UserID)
                    return user;
            return null;
        }

    }
    #endregion

}