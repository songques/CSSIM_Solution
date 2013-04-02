using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library.Class 
{
    /// <summary>
    /// Ⱥ�������Ϣ
    /// </summary>
    public  class GroupInfo
    {
        #region ������
        private byte[] groupID = new byte[4];//���
        private byte[] groupName = new byte[20];//����
        private byte[] createUserID = new byte[20];//������ID
        private byte[] notice = new byte[100];//�鹫��
        private byte[] usersLength = new byte[2];//�û�ID���ֽڳ���
        private byte[] users  = new byte[0];//���û���������ʾ��ǰ���ͻ���յ����û������е�����λ��
        #endregion

        #region ���û��ȡ���
        /// <summary>
        /// ���û��ȡ���
        /// </summary>
        public int  GroupID
        {
            set
            {
                this.groupID = BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this.groupID, 0);
            }
        }
        #endregion

        #region ���û��ȡ����
        /// <summary>
        /// ���û��ȡ���� 
        /// </summary>
        public string GroupName
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this.groupName.Length)
                    Buffer.BlockCopy(buf, 0, this.groupName, 0, this.groupName.Length); 
                else
                    Buffer.BlockCopy(buf, 0, this.groupName, 0, buf.Length); 
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this.groupName).Trim('\0'); }
        }
        #endregion

        #region ���û��ȡ������ID
        /// <summary>
        /// ���û��ȡ������ID 
        /// </summary>
        public string CreateUserID
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this.createUserID.Length)
                    Buffer.BlockCopy(buf, 0, this.createUserID, 0, this.createUserID.Length);
                else
                    Buffer.BlockCopy(buf, 0, this.createUserID, 0, buf.Length);
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this.createUserID).Trim('\0'); }
        }
        #endregion

        #region ���û��ȡ�鹫��
        /// <summary>
        /// ���û��ȡ�鹫�� 
        /// </summary>
        public string Notice
        {
            set
            {
                byte[] buf = CSS.IM.Library.Class.TextEncoder.textToBytes(value);
                if (buf.Length > this.notice.Length)
                    Buffer.BlockCopy(buf, 0, this.notice, 0, this.notice.Length);  
                else
                    Buffer.BlockCopy(buf, 0, this.notice, 0, buf.Length);  
            }
            get { return CSS.IM.Library.Class.TextEncoder.bytesToText(this.notice).Trim('\0'); }
        }
        #endregion

        #region ��ȡ���������Ա
        /// <summary>
        /// ��ȡ���������Ա
        /// </summary>
        public byte[] Users
        {
            set
            {
                this.usersLength = BitConverter.GetBytes((ushort)value.Length);//�����ֽڣ�����û�ID���ֽڳ���
                this.users = value;
            }
            get
            {
                return  this.users ;
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
                byte[] _msgBytes = new byte[this.getMsgLength];
                int dstOffSet = 0;

                Buffer.BlockCopy(groupID, 0, _msgBytes, dstOffSet, groupID.Length);
                dstOffSet += groupID.Length;

                Buffer.BlockCopy(groupName, 0, _msgBytes, dstOffSet, groupName.Length);
                dstOffSet += groupName.Length;

                Buffer.BlockCopy(createUserID, 0, _msgBytes, dstOffSet, createUserID.Length);
                dstOffSet += createUserID.Length;

                Buffer.BlockCopy(notice, 0, _msgBytes, dstOffSet, notice.Length );
                dstOffSet += notice.Length;

                Buffer.BlockCopy(usersLength, 0, _msgBytes, dstOffSet, usersLength.Length);
                dstOffSet += usersLength.Length;

                Buffer.BlockCopy(users, 0, _msgBytes, dstOffSet, users.Length );
                dstOffSet += users.Length;

                return _msgBytes;
            }
        }
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ���ֽ������ʼ��ΪȺ�������Ϣ��
        /// </summary>
        /// <param name="Data"></param>
        public  GroupInfo(byte[] Data)
        {
            int dstOffSet = 0;

            Buffer.BlockCopy(Data, dstOffSet, groupID, 0, groupID.Length );
            dstOffSet += groupID.Length;

            Buffer.BlockCopy(Data, dstOffSet, groupName, 0, groupName.Length );
            dstOffSet += groupName.Length;

            Buffer.BlockCopy(Data, dstOffSet, createUserID, 0, createUserID.Length);
            dstOffSet += createUserID.Length;

            Buffer.BlockCopy(Data, dstOffSet, notice, 0, notice.Length );
            dstOffSet += notice.Length;

            Buffer.BlockCopy(Data, dstOffSet, usersLength, 0, usersLength.Length);
            dstOffSet += usersLength.Length;

            users = new byte[BitConverter.ToUInt16(this.usersLength, 0)];//�û�ID���ֽڳ���
            Buffer.BlockCopy(Data, dstOffSet, users, 0, users.Length );
            dstOffSet += users.Length;
        }
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="_groupID">Ⱥ��ID��</param>
        /// <param name="_groupName">Ⱥ����</param>
        /// <param name="_createUserID">������ID</param>
        /// <param name="_notice">����</param>
        /// <param name="_users">�����ߣ�����������÷ֺŸ���</param>
        public  GroupInfo( int _groupID,string  _groupName,string _createUserID,string _notice ,byte[] _users)
        {
            this.GroupID = _groupID;
            this.GroupName = _groupName;// 
            this.CreateUserID = _createUserID;
            this.Notice = _notice;
            this.Users =_users;
         }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public  GroupInfo( )
        {
 
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
                int msgCount = groupID.Length + groupName.Length + createUserID.Length + notice.Length +  usersLength.Length  + users.Length;
                return msgCount;
            }
        }
        #endregion

    }
}
