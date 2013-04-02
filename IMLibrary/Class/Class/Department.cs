using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class
{
    #region 组织机构分组信息类
    /// <summary>
    ///  组织机构分组信息类
    /// </summary>
    public class Department  
    {
        /// <summary>
        /// 组织机构分组信息类
        /// </summary>
        public Department()
        {

        }

        /// <summary>
        /// 分组信息名称改变后事件代理
        /// </summary>
        /// <param name="sender"></param>
        public delegate void depNameChangedEventHandler(object sender);

        /// <summary>
        /// 分组信息名称改变后事件 
        /// </summary>
        public event depNameChangedEventHandler depNameChanged;

        /// <summary>
        /// 分组内在线用户数改变后事件代理
        /// </summary>
        /// <param name="sender"></param>
        public delegate void onLineUserCountChangedEventHandler(object sender);

        /// <summary>
        /// 分组内在线用户数改变后事件 
        /// </summary>
        public event onLineUserCountChangedEventHandler onLineUserCountChanged;
 
        /// <summary>
        /// 分组内用户数改变后事件代理
        /// </summary>
        /// <param name="sender"></param>
        public delegate void  UserCountChangedEventHandler(object sender);

        /// <summary>
        /// 分组内用户数改变后事件 
        /// </summary>
        public event  UserCountChangedEventHandler  UserCountChanged;


        /// <summary>
        /// 分组ID
        /// </summary>
        public int  depId;//部门Id


        private  string  _depName;
        /// <summary>
        /// 设置或获取分组名称
        /// </summary>
        public string depName
        {
            set
            {
                _depName = value;
                if (this.depNameChanged != null)this.depNameChanged(this);//触发部门名称改变事件 
            }
            get { return _depName; }
        }

        /// <summary>
        /// 分组的上级分组ID
        /// </summary>
        public int  superiorId;//上级部门Id

        /// <summary>
        /// 分组Tag
        /// </summary>
        public object Tag=null; 

        private int _orderID = 0;//用户所在部门的orderID

        /// <summary>
        /// 设置或获取分组所在分组内的orderID。 
        /// </summary>
        public int OrderID
        {
            get { return _orderID; }
            set
            {
                _orderID = value;
            }
        }

        /// <summary>
        /// 初始化分组信息类
        /// </summary>
        /// <param name="DepId">分组ID</param>
        /// <param name="DepName">分组名</param>
        /// <param name="SuperiorId">上级分组ID</param>
        /// <param name="orderID">分组排序ID</param>
        public Department(int DepId, string  DepName, int  SuperiorId,int orderID)
        {
            this.depId = DepId;
            this.depName = DepName;
            this.superiorId = SuperiorId;
            this.OrderID = orderID;
        }

       
        private int _onLineUserCount = 0;
        /// <summary>
        /// 设置或获取分组内的在线用户数
        /// </summary>
        public int onLineUserCount
        {
            get { return _onLineUserCount; }
            set
            {
                oldOnLineUserCount = _onLineUserCount;
                _onLineUserCount = value;
                if (this.onLineUserCountChanged != null) this.onLineUserCountChanged(this);//触发部门在线用户数改变事件
            }
        }

        /// <summary>
        /// 设置或获取分组内原在线用户数
        /// </summary>
        public int oldOnLineUserCount = 0;

        /// <summary>
        /// 设置或获取分组内原用户数
        /// </summary>
        public int oldUserCount = 0;

        
        private int _UserCount = 0;
        /// <summary>
        /// 设置或获取分组内用户数
        /// </summary>
        public int UserCount
        {
            get { return _UserCount; }
            set
            {
                oldUserCount = _UserCount;
                _UserCount = value;
                if (this.UserCountChanged != null) this.UserCountChanged(this);//触发部门用户总数改变事件
            }
        }
    }
    #endregion

    #region 组织机构集合
    /// <summary>
    /// 组织机构集合
    /// </summary>
    public class DepartmentCollections : System.Collections.CollectionBase
    {
        /// <summary>
        /// 集合构造函数
        /// </summary>
        public DepartmentCollections()
        {

        }

        // Get Department at the specified index
        /// <summary>
        /// 分组索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Department this[int index]
        {
            get
            {
                return ((Department)InnerList[index]);
            }
        }

        /// <summary>
        /// 向组织机构集中添加一个分组 
        /// </summary>
        /// <param name="d">要添加的分组</param>
        public void add(IMLibrary.Class.Department d)
        {
            base.InnerList.Add(d);
        }
        /// <summary>
        /// 从组织机构集中删除一个分组 
        /// </summary>
        /// <param name="d">要删除的分组</param>
        public void Romove(IMLibrary.Class.Department d)
        {
            base.InnerList.Remove(d);
        }

        /// <summary>
        /// 在集合中查找分组
        /// </summary>
        /// <param name="DepID">分组ID</param>
        /// <returns>返回分组信息</returns>
        public Department find(int DepID)
        {
            foreach (Department Dep in this)
                if (DepID == Dep.depId)
                    return Dep;
            return null;
        }
    }
    #endregion
}
