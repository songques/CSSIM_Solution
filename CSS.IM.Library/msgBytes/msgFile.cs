using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library.Class 
{
    /// <summary>
    /// �ļ�������Ϣ
    /// </summary>
    public class msgFile 
    {
        //private byte[] _InfoClass = new byte[1];//�ļ�������Ϣ��� 0
        //private byte[] _sendID = new byte[4];//���ݷ�����ID��ʶ 1
        //private byte[] _recID = new byte[4];//���ݽ�����ID 5
        //private byte[] _pSendPos = new byte[8];//����ϴη��͵�λ�� 9
        //private byte[] _fileBlockLength = new byte[2];//��ʶ���͵��ļ��鳤�� 17

        private byte[]  fileBlock = new byte[0];//��ǰ���͵��ļ���
        private byte[] data = new byte[19];//��Ϣת������ֽ�����

        #region  ���캯���߼�
        /// <summary>
        /// ���캯���߼�
        /// </summary>
        public msgFile ()
        {
            //
            // TODO: �ڴ˴����
            //
        }

        /// <summary>
        /// ���ֽ�����ת��Ϊ��Ϣ��
        /// </summary>
        /// <param name="Data"></param>
        public msgFile (byte[] Data)
        {
            this.data=Data;
        }

        /// <summary>
        /// ��ʼ����Ϣ
        /// </summary>
        /// <param name="infoClass">��ϢЭ������</param>
        /// <param name="sendId">������ID</param>
        /// <param name="recId">������ID</param>
        /// <param name="msgPSendPos"></param>
        /// <param name="msgFileBlock"></param>
        public msgFile(byte infoClass,int sendId, int recId,long  msgPSendPos, byte[] msgFileBlock)
        {
            this.InfoClass = infoClass;
            this.SendID  = sendId;
            this.RecID = recId;
            this.pSendPos = msgPSendPos;
            this.FileBlock = msgFileBlock;
        }
        #endregion

        #region ���û��ȡ��ϢЭ��
        /// <summary>
        /// ���û��ȡ��ϢЭ��
        /// </summary>
        public byte InfoClass
        {
            set
            {
                Buffer.SetByte(this.data,0, value);
            }
            get
            {
                return Buffer.GetByte(this.data, 0);
            }
        }
        #endregion

        #region ���û��ȡ���ݷ�����ID 
        /// <summary>
        /// ���û��ȡ���ݷ�����ID 
        /// </summary>
        public int SendID
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 1, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data,1);
            }
        }
        #endregion

        #region ���û��ȡ���ݽ�����ID
        /// <summary>
        /// ���û��ȡ���ݽ�����ID 
        /// </summary>
        public int RecID
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 5, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data,5);
            }
        }
        #endregion

        #region ���û��ȡ�ϴη��͵�λ��
        /// <summary>
        /// ���û��ȡ�ϴη��͵�λ��
        /// </summary>
        public long pSendPos
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 9, 8);
            }
            get
            {
                return BitConverter.ToInt64(this.data, 9);
            }
        }
        #endregion

        #region ���û��ȡ���ݳ���
        /// <summary>
        /// ���û��ȡ���ݳ���
        /// </summary>
        private ushort FileBlockLength
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 17, 2);
            }
            get
            {
                return BitConverter.ToUInt16(this.data, 17);
            }
        }
        #endregion

        #region ��ȡ���������ݿ�����
        /// <summary>
        /// ��ȡ���������ݿ�����
        /// </summary>
        public byte[] FileBlock  
        {
            set
            {
                this.fileBlock = value;
                this.FileBlockLength = (ushort)value.Length;//�����ֽڣ�����û�������Ϣʵ��ĳ���
            }
            get
            {
                this.fileBlock = new byte[this.FileBlockLength];
                Buffer.BlockCopy(this.data, 19, this.fileBlock, 0, this.fileBlock.Length);
                return this.fileBlock;
            }
        }
        #endregion

        #region ��ȡ��Ϣ�ֽ�����
        /// <summary>
        /// �����Ϣ�ֽ�����
        /// </summary>
        public byte[] getBytes()
        {
            List<byte> result = new List<byte>();
            result.AddRange(this.data);
            result.AddRange(this.fileBlock);
            this.data= result.ToArray();
            return this.data;
        }
        #endregion

        #region ��ȡ��Ϣ�ֽ�����
        /// <summary>
        /// ��ȡ��Ϣ�ֽ����� 
        /// </summary>
        public byte[] Data
        {
            get { return data; }
        }
        #endregion
    }
       
}
