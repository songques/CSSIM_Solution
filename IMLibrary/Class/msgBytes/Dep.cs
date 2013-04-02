using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class
{
    /// <summary>
    /// ������Ϣ��
    /// </summary>
    public class Dep
    {
        #region ������
        private byte[] _depID = new byte[4];//����ID
        private byte[] _superiorID = new byte[4];//�˲����ϼ�����ID
        private byte[] _depName = new byte[40];//�������ƣ��̶�
        private byte[] _orderID = new byte[4];//
        #endregion

        #region ���û��ȡ����ID
        /// <summary>
        /// ���û��ȡ����ID
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

        #region ���û��ȡ�˲����ϼ�����ID
        /// <summary>
        /// ���û��ȡ�˲����ϼ�����ID
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

        #region ���û��ȡ������ 
        /// <summary>
        /// ���û��ȡ������  
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

        #region ���û��ȡorderID
        /// <summary>
        /// ���û��ȡ��������ID
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

        #region  ��ȡ���ֽ�����
        /// <summary>
        /// ��ȡ���ֽ�����
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

        #region ��ʼ��������Ϣ��
        /// <summary>
        /// ��ʼ��������Ϣ��
        /// </summary>
        /// <param name="Data">Ҫת�����ֽ�����</param>
        public  Dep(byte[] Data)
        {
            if (Data.Length != 52) return;//������ǲ�����Ϣ�����˳�

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

        #region  ��ʼ��������Ϣ
        /// <summary>
        /// ��ʼ��������Ϣ
        /// </summary>
        public  Dep()
        {

        }
        #endregion

        #region ��ʼ��������Ϣ��
        /// <summary>
        /// ��ʼ��������Ϣ��
        /// </summary>
        /// <param name="ID">����ID</param>
        /// <param name="superiorID">�ϼ�����ID</param>
        /// <param name="depName">�ϼ���������</param>
        /// <param name="orderID">����ID</param>
        public  Dep(int ID, int superiorID, string depName, int orderID)
        {
            this.DepID = ID;
            this.SuperiorID = superiorID;
            this.DepName = depName;
            this.OrderID = orderID;
        }
        #endregion

        #region ��÷�����Ϣ���ֽ����鳤��
        /// <summary>
        /// ��÷�����Ϣ���ֽ����鳤��
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
