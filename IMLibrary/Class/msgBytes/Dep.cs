using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class
{
    /// <summary>
    /// 分组信息类
    /// </summary>
    public class Dep
    {
        #region 变量区
        private byte[] _depID = new byte[4];//部门ID
        private byte[] _superiorID = new byte[4];//此部门上级部门ID
        private byte[] _depName = new byte[40];//部门名称，固定
        private byte[] _orderID = new byte[4];//
        #endregion

        #region 设置或获取分组ID
        /// <summary>
        /// 设置或获取分组ID
        /// </summary>
        public int DepID
        {
            set
            {
                this._depID = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._depID, 0);
            }
        }
        #endregion

        #region 设置或获取此部门上级分组ID
        /// <summary>
        /// 设置或获取此部门上级分组ID
        /// </summary>
        public int SuperiorID
        {
            set
            {
                this._superiorID = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._superiorID, 0);
            }
        }
        #endregion

        #region 设置或获取分组名 
        /// <summary>
        /// 设置或获取分组名  
        /// </summary>
        public string DepName
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._depName.Length)
                    Buffer.BlockCopy(buf, 0, this._depName, 0, this._depName.Length);
                else
                    Buffer.BlockCopy(buf, 0, this._depName, 0, buf.Length);
            }
            get { return IMLibrary.Class.TextEncoder.bytesToText(this._depName).Trim('\0'); }
        }
        #endregion

        #region 设置或获取orderID
        /// <summary>
        /// 设置或获取分组排序ID
        /// </summary>
        public int OrderID
        {
            set
            {
                this._orderID = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this._orderID, 0);
            }
        }
        #endregion

        #region  获取类字节数组
        /// <summary>
        /// 获取类字节数组
        /// </summary>
        public byte[]  getBytes
        {
            get
            {
                byte[] _msgBytes = new byte[52];

                int dstOffSet = 0;

                Buffer.BlockCopy(_depID, 0, _msgBytes, dstOffSet, _depID.Length);
                dstOffSet += _depID.Length;

                Buffer.BlockCopy(_superiorID, 0, _msgBytes, dstOffSet, _superiorID.Length);
                dstOffSet += _superiorID.Length;

                Buffer.BlockCopy(_depName, 0, _msgBytes, dstOffSet, _depName.Length);
                dstOffSet += _depName.Length;

                Buffer.BlockCopy(_orderID, 0, _msgBytes, dstOffSet, _orderID.Length);
                dstOffSet += _orderID.Length;

                return _msgBytes;
            }
        }
        #endregion

        #region 初始化分组信息类
        /// <summary>
        /// 初始化分组信息类
        /// </summary>
        /// <param name="Data">要转换的字节数组</param>
        public  Dep(byte[] Data)
        {
            if (Data.Length != 52) return;//如果不是部门信息，则退出

            int dstOffSet = 0;

            Buffer.BlockCopy(Data , dstOffSet, _depID, 0, _depID.Length);
            dstOffSet += _depID.Length;

            Buffer.BlockCopy(Data , dstOffSet, _superiorID, 0, _superiorID.Length);
            dstOffSet += _superiorID.Length;

            Buffer.BlockCopy(Data , dstOffSet, _depName, 0, _depName.Length);
            dstOffSet += _depName.Length;

            Buffer.BlockCopy(Data, dstOffSet, _orderID, 0, _orderID.Length);
            dstOffSet += _orderID.Length;
            
        }
        #endregion

        #region  初始化分组信息
        /// <summary>
        /// 初始化分组信息
        /// </summary>
        public  Dep()
        {

        }
        #endregion

        #region 初始化分组信息类
        /// <summary>
        /// 初始化分组信息类
        /// </summary>
        /// <param name="ID">分组ID</param>
        /// <param name="superiorID">上级分组ID</param>
        /// <param name="depName">上级分组名称</param>
        /// <param name="orderID">排序ID</param>
        public  Dep(int ID, int superiorID, string depName, int orderID)
        {
            this.DepID = ID;
            this.SuperiorID = superiorID;
            this.DepName = depName;
            this.OrderID = orderID;
        }
        #endregion

        #region 获得分组信息类字节数组长度
        /// <summary>
        /// 获得分组信息类字节数组长度
        /// </summary>
        /// <returns></returns>
        public int getMsgLength
        {
            get
            {
                int msgCount = 52;
                return msgCount;
            }
        }
        #endregion
    }
}
