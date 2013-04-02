using System;
using System.Net;

namespace CSS.IM.Library.Class
{

    #region �û���Ϣ��
    /// <summary>
    /// �û���Ϣ�� 
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// �û���Ϣ��
        /// </summary>
        public UserInfo()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        /// <summary>
        /// �û���Ϣ��
        /// </summary>
        /// <param name="UserID">�û�ID</param>
        public UserInfo(string UserID)
        {
            this.UserID = UserID;
        }

        /// <summary>
        /// ��ʼ���û���Ϣ��
        /// </summary>
        /// <param name="ID">�û�����</param>
        /// <param name="UserID">�û�ID</param>
        /// <param name="UserName">�û�����</param>
        /// <param name="DepId">�û����ڷ���ID</param>
        /// <param name="orderID">�û��ڷ����ڵ�����</param>
        public UserInfo(int ID, string UserID, string UserName, int DepId, int orderID)
        {
            this.Index  = ID;
            this.UserID  = UserID;
            this.userName  = UserName;
            this.depId = DepId;
            this.orderID = orderID;
        }

        #region �¼�
        /// <summary>
        /// �û�����״̬�ı��¼�
        /// </summary>
        /// <param name="sender">�����¼��Ķ���</param>
        public delegate void userStateChangedEventHandler(object sender);
        /// <summary>
        /// �û�����״̬�ı��¼�
        /// </summary>
        public event userStateChangedEventHandler userStateChanged;

        /// <summary>
        /// �û����������¼�
        /// </summary>
        /// <param name="sender">�����¼��Ķ���</param>
        public delegate void userNameChangedEventHandler(object sender);
        /// <summary>
        /// �û����������¼�
        /// </summary>
        public event userNameChangedEventHandler userNameChanged;

        /// <summary>
        /// �û�ID�����¼�
        /// </summary>
        /// <param name="sender">�����¼��Ķ���</param> 
        public delegate void userIdChangedEventHandler(object sender);
        /// <summary>
        /// �û�ID�����¼�
        /// </summary>
        public event userIdChangedEventHandler userIdChanged;

        /// <summary>
        /// �û����Ÿ����¼�
        /// </summary>
        /// <param name="sender">�����¼��Ķ���</param>
        public delegate void depIdChangedEventHandler(object sender);
        /// <summary>
        /// �û����Ÿ����¼�
        /// </summary>
        public event depIdChangedEventHandler depIdChanged;

        /// <summary>
        /// �û�IP��ַ�����¼�
        /// </summary>
        /// <param name="sender">�����¼��Ķ���</param>
        public delegate void IpChangedEventHandler(object sender);
        /// <summary>
        /// �û�IP��ַ�����¼�
        /// </summary>
        public event IpChangedEventHandler IpChanged;

        /// <summary>
        /// �û�Port�����¼�
        /// </summary>
        /// <param name="sender">�����¼��Ķ���</param>
        public delegate void PortChangedEventHandler(object sender);
        /// <summary>
        /// �û�Port�����¼�
        /// </summary>
        public event PortChangedEventHandler PortChanged;
        #endregion

        /// <summary>
        /// ���û��ȡ���͵���Ϣ�Ƿ�ɹ��� 
        /// </summary>
        public bool SendIsSuccess = false;//��ʶ���͸�����ϵ�˵���һ�������Ƿ�ɹ�


        private int _index = 0;
        /// <summary>
        ///  �û��ڷ������ϵ���������λ��
        /// </summary>
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        /// <summary>
        ///  �û�ͷ������λ��
        /// </summary>
        public byte FaceIndex= 0;
         

        /// <summary>
        ///  �û�ͷ������λ��
        /// </summary>
        public byte Sex = 1;
         

        /// <summary>
        ///  �û����������ͨ������
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// �û�Tag
        /// </summary>
        public object Tag = null; 


        private string _userID = "";// ��ʶ�û����Ψһ��ID�����ü���������棩
        /// <summary>
        /// ϵͳ�û�ID�� 
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
        /// ��ȡ�������û�����
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

 
        private int _depID = 0;//�û����ڲ��ŵ�ID
        /// <summary>
        /// ���û��ȡ�û����ڲ��ŵ�ID�� 
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

        private int _orderID = 0;//�û����ڲ��ŵ�orderID
        /// <summary>
        /// ���û��ȡ�û����ڲ��ŵ�ID�� 
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
        /// ��ȡ�������û���IP��ַ�� 
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
        /// ��ȡ�������û��Ķ˿ںš� 
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
        /// ��ȡ�������û��ľ�����IP��ַ�� 
        /// </summary>
        public IPAddress LocalIP= IPAddress.Parse("127.0.0.1");
        
         
        /// <summary>
        /// ��ȡ�������û��ľ������˿ںš� 
        /// </summary>
        public int  LocalPort = 0;
         
        /// <summary>
        /// �������ݸ��û���������紫�䵥Ԫֵ��MTU���� 
        /// </summary>
        public ushort MTU = 512;
       

        /// <summary>
        /// ��ʶ���û�ͨ�ŵ��������ͣ�UDP��TCP��������
        /// </summary>
        public byte NetClass = (byte)CSS.IM.Library.Class.NatClass.None;
        

        /// <summary>
        /// �����������ͨ�ŵ�TCP���
        /// </summary>
        public object   myTcp;

        /// <summary>
        /// ��ʶ�û����û�֮�����������Ƿ�ֱ������(��Ϊֱ�����������ݿ��ԶԷ�����Ϊ��ֱ������������ͨ��������ת��)
        /// </summary>
        public bool isConnected = false;
       
         
        /// <summary>
        /// �ж��û����û�֮���Ƿ�ͬ��һ����������
        /// </summary>
        public bool isInLan;

        /// <summary>
        /// UDP�򶴴���
        /// </summary>
        public byte UDPPenetrateCount;
        
        /// <summary>
        /// ��ȡ�������û�������״̬�ı���Ϣ�� 
        /// </summary>
        public string StateInfo= "(�ѻ�)";
        

        
        /// <summary>
        /// ��ȡ�������û�����ʱ����״̬�� 
        /// </summary>
        public byte tempState=0;
        

        private byte _state = 0;
        /// <summary>
        /// ��ȡ�������û�������״̬�� 
        /// </summary>
        public byte State
        {
            get { return _state; }
            set
            {
                switch (value)
                {
                    case 0:
                        this.isConnected  = false;//�����������û�Ϊ������ֱ��
                        this.IP = System.Net.IPAddress.Parse("127.0.0.1");//�����û�IP��ַΪ����
                        this.NetClass = 0;//�����û���������
                        this.UDPPenetrateCount = 0;//UDP�򶴴�������
                        StateInfo = "(�ѻ�)";
                        break;
                    case 1:
                        StateInfo = "(����)";
                        break;
                    case 2:
                        StateInfo = "(æµ)";
                        break;
                    case 4:
                        StateInfo = "(�뿪)";
                        break;
                    case 3:
                        StateInfo = "(�����绰)";
                        break;
                    case 5:
                        StateInfo = "(����Ͳ�)";
                        break;
                }

                if (_state != value)//�������״̬��ĸı䣬�򴥷��¼�
                {
                    _state = value; 
                    if (userStateChanged != null)
                        userStateChanged(this);
                }
            }
        }


    }
    #endregion

    #region  �û�����
    /// <summary>
    /// �û����� 
    /// </summary>
    public class UserCollections : System.Collections.CollectionBase
    {
        /// <summary>
        /// �û�����
        /// </summary>
        public UserCollections()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        // Get UserInfo at the specified index
        /// <summary>
        /// ��ȡ�������û��ڼ����ڵ�����
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
        /// �������û����������һ���û�
        /// </summary>
        /// <param name="_UserInfo">Ҫ��ӵ��û�</param>
        public void add(UserInfo _UserInfo)
        {
            base.InnerList.Add(_UserInfo);
            _UserInfo.Index = this.Count-1;
        }
        /// <summary>
        /// �������û�������ɾ��һ���û�
        /// </summary>
        /// <param name="_UserInfo">Ҫɾ�����û�</param>
        public void Romove(UserInfo _UserInfo)
        {
            base.InnerList.Remove(_UserInfo);
        }

        /// <summary>
        /// �ڼ����в����û�
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <returns>�����û���Ϣ</returns>
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