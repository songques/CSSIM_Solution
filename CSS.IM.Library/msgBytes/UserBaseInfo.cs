using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library.Class 
{
    /// <summary>
    /// �û�������Ϣ��
    /// </summary>
    public class  UserBaseInfo
    {

       #region ������
        private byte[] _userID = new byte[20];//
        private byte[] _userName = new byte[20];//
        private byte[] _faceIndex = new byte[1];//
        private byte[] _sex = new byte[1];//
        private byte[] _depID = new byte[4];//
        private byte[] _orderID = new byte[4];//
        #endregion

       #region ���û��ȡUserID
        /// <summary>
        ///  ���û��ȡ�û�ID
        /// </summary>
        public string UserID
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if(buf.Length>this._userID.Length )
                    Buffer.BlockCopy(buf, 0, this._userID, 0, this._userID.Length);
                else
                    Buffer.BlockCopy(buf, 0, this._userID, 0, buf.Length);
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this._userID).Trim('\0'); }
        }
        #endregion

       #region ���û��ȡ�û����� 
        /// <summary>
        ///  ���û��ȡ�û�����
        /// </summary>
        public string UserName
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this._userName.Length )
                    Buffer.BlockCopy(buf, 0, this._userName, 0, this._userName.Length);
                else
                    Buffer.BlockCopy(buf, 0, this._userName, 0, buf.Length);
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this._userName).Trim('\0'); }
        }
        #endregion

       #region ���û��ȡ�û�ͷ������
       /// <summary>
       /// ���û��ȡ�û�ͷ������
       /// </summary>
        public byte FaceIndex
        {
            set
            {
                this._faceIndex[0] = value ;
            }
            get
            {
                return this._faceIndex[0];
            }
        }
        #endregion

       #region ���û��ȡ�û�Sex
        /// <summary>
        /// ���û�ȡ�û��Ա�
        /// </summary>
        public byte Sex
        {
            set
            {
                this._sex[0] = value ;
            }
            get
            {
                return this._sex[0];
            }
        }
        #endregion

       #region ���û��ȡ����ID
        /// <summary>
        /// ���û��ȡ�û��ķ���ID
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

       #region ���û��ȡorderID
        /// <summary>
        /// ���û��ȡ�û��ڷ����ڵ�����
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

       #region ���û��ȡ��Ϣ�ֽ�����
        /// <summary>
        /// �����Ϣ�ֽ�����
        /// </summary>
        public byte[] msgBytes
        {
            get
            {
                byte[] _msgBytes = new byte[50];

                int dstOffSet = 0;
 
                Buffer.BlockCopy(_userID, 0, _msgBytes, dstOffSet, _userID.Length);
                dstOffSet += _userID.Length;

                Buffer.BlockCopy(_userName, 0, _msgBytes, dstOffSet, _userName.Length);
                dstOffSet += _userName.Length;

                Buffer.BlockCopy(_faceIndex, 0, _msgBytes, dstOffSet, _faceIndex.Length);
                dstOffSet += _faceIndex.Length;

                Buffer.BlockCopy(_sex, 0, _msgBytes, dstOffSet, _sex.Length);
                dstOffSet += _sex.Length;

                Buffer.BlockCopy(_depID, 0, _msgBytes, dstOffSet, _depID.Length);
                dstOffSet += _depID.Length;

                Buffer.BlockCopy(_orderID, 0, _msgBytes, dstOffSet, _orderID.Length);
                dstOffSet += _orderID.Length;

                return _msgBytes;
            }
        }
        #endregion

       #region ��ʼ����
        /// <summary>
        /// ���ֽ�����ת��Ϊ�û�������Ϣ��
        /// </summary>
        /// <param name="Data"></param>
        public UserBaseInfo(byte[] Data)
        {
            if (Data.Length != 50) return;//������ǲ�����Ϣ�����˳�

            int dstOffSet = 0;

            Buffer.BlockCopy(Data, dstOffSet, _userID, 0, _userID.Length);
            dstOffSet += _userID.Length;

            Buffer.BlockCopy(Data, dstOffSet, _userName, 0, _userName.Length);
            dstOffSet += _userName.Length;

            Buffer.BlockCopy(Data, dstOffSet, _faceIndex, 0, _faceIndex.Length);
            dstOffSet += _faceIndex.Length;

            Buffer.BlockCopy(Data, dstOffSet, _sex, 0, _sex.Length);
            dstOffSet += _sex.Length;

            Buffer.BlockCopy(Data, dstOffSet, _depID, 0, _depID.Length);
            dstOffSet += _depID.Length;

            Buffer.BlockCopy(Data, dstOffSet, _orderID, 0, _orderID.Length);
            dstOffSet += _orderID.Length;
        }
        
       /// <summary>
       /// ��ʼ����
       /// </summary>
        public UserBaseInfo()
        {

        }
      
        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="userName">�û�����</param>
        /// <param name="faceIndex">�û�ͷ������</param>
        /// <param name="sex">�û��Ա�</param>
        /// <param name="depID">�û�����ID</param>
        /// <param name="orderID">�û��ڷ����ڵ�����</param>
        public UserBaseInfo(string userID, string userName, byte faceIndex, byte sex, int depID, int orderID)
        {
            this.UserID  = userID;
            this.UserName = userName;
            this.FaceIndex  = faceIndex;
            this.Sex  = sex;
            this.DepID  = depID;
            this.OrderID = orderID;
        }
        #endregion

       #region �����Ϣ�ֽ����鳤��
        /// <summary>
        /// �����Ϣ�ֽ����鳤��
        /// </summary>
        /// <returns></returns>
        public int getMsgLength
        {
            get
            {
                int msgCount = 50;
                return msgCount;
            }
        }
        #endregion
    }
}
