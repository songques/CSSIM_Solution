using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class 
{
    /// <summary>
    /// ��ʱ��Ϣ��
    /// </summary>
    public class Msg 
    {
        #region ������

        //private byte[] length=new byte[2];//��Ϣ���� 0 
        //private byte[] infoClass = new byte[1];//��Ϣ���� 2
        //private byte[] transferInfoClass = new byte[1];//��ת��Ϣ���� 3
        //private byte[] totalFragment=new byte[1];//��Ϣ�ܷ�Ƭ�� 4
        //private byte[] sequenceFragment =new byte[1];//��Ϣ��Ƭ���� 5
        //private byte[] fontNameLength = new byte[1];//��Ϣ���������� 6
        //private byte[] fontSize = new byte[1];//�����С 7
        //private byte[] fontBold = new byte[1];//��Ϣ������ 8
        //private byte[] fontItalic = new byte[1];//��Ϣ�����Ƿ���б 9
        //private byte[] fontStrikeout = new byte[1];//��Ϣ�����Ƿ���ɾ���� 10
        //private byte[] fontUnderline = new byte[1];//��Ϣ�����Ƿ����»��� 11
        //private byte[] fontColor = new byte[4];//��Ϣ�ı���ɫ���� int32 12
        //private byte[] sendIndex = new byte[4];//��Ϣ�������ڷ������ϵ�ID���� 16
        //private byte[] receiveIndex = new byte[4];//��Ϣ�������ڷ������ϵ����� 20
        //private byte[] sendIDLength = new byte[1];//��Ϣ������ID�ĳ��� 24
        //private byte[] receiveIDLength = new byte[1];//��Ϣ������ID�ĳ��� 25
        //private byte[] contentLength = new byte[2];//��Ϣ���ݵĳ���,������65535��ֵȡΪushort 26
        //private byte[] imageInfoLength = new byte[2];//��Ϣ������ͼƬ��Ϣ���ݵĳ���,������65535��ֵȡΪushort 28
        //private byte[] password = new byte[32];//��Ϣ�������ڷ������ϵ����� 30

        private byte[] sendID = new byte[0];//��Ϣ�������ڷ������ϵ�ID�� 62
        private byte[] receiveID = new byte[0];//��Ϣ������ID  
        private byte[] content = new byte[0];//��Ϣ����ʵ��
        private byte[] imageInfo = new byte[0];//��Ϣ����ͼƬ��Ϣ��ʵ��

        private byte[] fontName = new byte[0];//��Ϣ������ 

        private byte[] groupID = new byte[0];//Ⱥ���ID�� 

        private byte[] data = new byte[62];//��Ϣת������ֽ�����

        #endregion

        #region ��ȡ��Ϣ����
        /// <summary>
        /// ��ȡ��Ϣ���� 
        /// </summary>
        public ushort Length
        {
            get
            {
                return BitConverter.ToUInt16(this.data, 0);
            }
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
                Buffer.SetByte(this.data, 2, value);
            }
            get
            {
                return Buffer.GetByte(this.data, 2);
            }
        }
        #endregion

        #region ���û��ȡ��ת��Ϣ����
        /// <summary>
        /// ���û��ȡ��ת��Ϣ����
        /// </summary>
        public byte TransferInfoClass
        {
            set
            {
                Buffer.SetByte(this.data, 3, value);
            }
            get
            {
                return Buffer.GetByte(this.data, 3);
            }
        }
        #endregion

        #region ���û��ȡ��Ϣ��Ƭ����
        /// <summary>
        /// ���û��ȡ��Ϣ��Ƭ����
        /// </summary>
        public byte TotalFragment
        {
            set
            {
                Buffer.SetByte(this.data,4, value);
            }
            get
            {
                     return  Buffer.GetByte(this.data, 4);
             }
        }
        #endregion

        #region ���û��ȡ��Ϣ��Ƭ����
        /// <summary>
        /// ���û��ȡ��Ϣ��Ƭ����
        /// </summary>
        public byte SequenceFragment
        {
            set
            {
                Buffer.SetByte(this.data, 5, value);
            }
            get
            {
                return Buffer.GetByte(this.data, 5);
            }
        }
        #endregion

        #region ��ȡ��Ϣ����������
        /// <summary>
        /// ��ȡ��Ϣ����������
        /// </summary>
        private byte FontNameLength
        {
            set
            {
                Buffer.SetByte(this.data, 6, value);
            }
            get
            {
                return Buffer.GetByte(this.data, 6);
            }
        }
        #endregion

        #region ��ȡ��Ϣ��������С
        /// <summary>
        /// ��ȡ��Ϣ��������С
        /// </summary>
        private float FontSize
        {
            set
            {
                int size = Convert.ToInt32(value);
                Buffer.SetByte(this.data,7,Convert.ToByte(size));
            }
            get
            {
                byte  f=Buffer.GetByte(this.data,7);
                if (f == 0)
                    f = 9;
                    return f;
            }
        }
        #endregion

        #region ��ȡ��Ϣ�����Ƿ����
        /// <summary>
        /// ��ȡ��Ϣ�����Ƿ����
        /// </summary>
        private bool FontBold
        {
            set
            {
                Buffer.SetByte(this.data, 8, Convert.ToByte(value));
            }
            get
            {
                return Convert.ToBoolean(Buffer.GetByte(this.data, 8));
            }
        }
        #endregion

        #region ��ȡ��Ϣ�����Ƿ�б��
        /// <summary>
        /// ��ȡ��Ϣ�����Ƿ�б��
        /// </summary>
        private bool FontItalic
        {
            set
            {
                Buffer.SetByte(this.data, 9, Convert.ToByte(value));
            }
            get
            {
                return Convert.ToBoolean(Buffer.GetByte(this.data, 9));
            }
        }
        #endregion

        #region ��ȡ��Ϣ�����Ƿ�ɾ����
        /// <summary>
        /// ��ȡ��Ϣ�����Ƿ�ɾ����
        /// </summary>
        private bool FontStrikeout
        {
            set
            {
                Buffer.SetByte(this.data, 10, Convert.ToByte(value));
            }
            get
            {
                return Convert.ToBoolean( Buffer.GetByte(this.data, 10));
            }
        }
        #endregion

        #region ��ȡ��Ϣ�����Ƿ��»���
        /// <summary>
        /// ��ȡ��Ϣ�����Ƿ��»���
        /// </summary>
        private bool FontUnderline
        {
            set
            {
                Buffer.SetByte(this.data, 11, Convert.ToByte(value));
            }
            get
            {
                return  Convert.ToBoolean(Buffer.GetByte(this.data, 11));
            }
        }
        #endregion

        #region ��ȡ��Ϣ������
        /// <summary>
        /// ��ȡ��Ϣ������
        /// </summary>
        private string  FontName
        {
            set
            {
                this.fontName = TextEncoder.textToBytes(value);//�����������
                this.FontNameLength = Convert.ToByte(this.fontName.Length);//��������ֽ����鳤��
            }
            get
            {
                byte[] buf = new byte[this.FontNameLength];
                Buffer.BlockCopy(this.data, 62 + SendIDLength + ReceiveIDLength + ContentLength + ImageInfoLength,  buf, 0,   buf.Length);
                return TextEncoder.bytesToText(buf);
            }
        }
        #endregion

        #region ���û��ȡ��Ϣ����
        /// <summary>
        /// ���û��ȡ��Ϣ���� 
        /// </summary>
        public System.Drawing.Font font
        {
            set
            {
                ////�����Ϣ�������ֵ��ʼ
                this.FontName = value.Name;//�����������
                this.FontSize = value.Size;//�����С
                this.FontBold = value.Bold;//�Ƿ����
                this.FontItalic = value.Italic;//�Ƿ�б��
                this.FontStrikeout = value.Strikeout;//�Ƿ�ɾ����
                this.FontUnderline = value.Underline;//�Ƿ��»���
                /////�����Ϣ�������ֵ����
            }
            get
            {
                System.Drawing.FontStyle fontStyle = new System.Drawing.FontStyle();
                if (this.FontBold) fontStyle = System.Drawing.FontStyle.Bold;
                if (this.FontItalic) fontStyle = fontStyle | System.Drawing.FontStyle.Italic;
                if (this.FontStrikeout) fontStyle = fontStyle | System.Drawing.FontStyle.Strikeout;
                if (this.FontUnderline) fontStyle = fontStyle | System.Drawing.FontStyle.Underline;
                System.Drawing.Font ft = new System.Drawing.Font(this.FontName, this.FontSize, fontStyle);
                return ft;
            }
        }
        #endregion

        #region ��Ϣ�ı���ɫ
        /// <summary>
        ///  ��ȡ��������Ϣ�ı���ɫ
        /// </summary>
        public System.Drawing.Color Color
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value.ToArgb()), 0, this.data, 12, 4);
            }
            get
            {

                return System.Drawing.Color.FromArgb(BitConverter.ToInt32(this.data, 12));
            }
        }
        #endregion
           
        #region ���û��ȡ��Ϣ�������ڷ������ϵ�����
        /// <summary>
        /// ���û��ȡ��Ϣ������ID
        /// </summary>
        public int SendIndex
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 16, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data, 16);
            }
        }
        #endregion

        #region ���û��ȡ��Ϣ�������ڷ������ϵ�����
        /// <summary>
        /// ���û��ȡ��Ϣ�������ڷ������ϵ�����
        /// </summary>
        public int ReceiveIndex
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 20, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data, 20);
            }
        }
        #endregion

        #region ��ȡ��Ϣ������ID�ĳ���
        /// <summary>
        /// ��ȡ��Ϣ������ID�ĳ���
        /// </summary>
        private byte SendIDLength
        {
            set
            {
                Buffer.SetByte(this.data, 24, value);
            }
            get
            {
                return Buffer.GetByte(this.data, 24);
            }
        }
        #endregion

        #region ��ȡ��Ϣ������ID�ĳ���
        /// <summary>
        /// ��ȡ��Ϣ������ID�ĳ���
        /// </summary>
        private byte ReceiveIDLength
        {
            set
            {
                Buffer.SetByte(this.data, 25, value);
            }
            get
            {
                return Buffer.GetByte(this.data, 25);
            }
        }
        #endregion

        #region ��ȡ��Ϣ���ݵĳ���
        /// <summary>
        /// ��ȡ��Ϣ���ݵĳ���
        /// </summary>
        private ushort ContentLength
        {
            set
            {
                Buffer.BlockCopy (BitConverter.GetBytes(value),0,this.data, 26,2);
            }
            get
            {
                return BitConverter.ToUInt16 (this.data, 26);
            }
        }
        #endregion

        #region ��ȡ��ϢͼƬ��Ϣ���ݵĳ���
        /// <summary>
        /// ��ȡ��ϢͼƬ��Ϣ���ݵĳ���
        /// </summary>
        private ushort ImageInfoLength
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 28, 2);
            }
            get
            {
                return BitConverter.ToUInt16(this.data, 28);
            }
        }
        #endregion

        #region ���û��ȡ��Ϣ����
        /// <summary>
        /// ���û��ȡ��Ϣ���� 
        /// </summary>
        public string Password
        {
            set
            {
                byte[] buf = IMLibrary.Class.TextEncoder.textToBytes(value);
                if (buf.Length > 32)
                    Buffer.BlockCopy(buf, 0, this.data, 30, 32);
                else
                    Buffer.BlockCopy(buf, 0, this.data, 30, buf.Length);
            }
            get
            {
                byte[] buf = new byte[32];
                Buffer.BlockCopy(this.data, 30, buf, 0, 32);
                return IMLibrary.Class.TextEncoder.bytesToText(buf).Trim('\0');
            }
        }
        #endregion

        #region ���û��ȡ��Ϣ������ID
        /// <summary>
        /// ���û��ȡ��Ϣ������ID
        /// </summary>
        public string SendID
        {
            set
            {
                this.sendID = IMLibrary.Class.TextEncoder.textToBytes(value.ToString());
                this.SendIDLength = Convert.ToByte(this.sendID.Length);
            }
            get
            {
                byte[] buf = new byte[this.SendIDLength];
                Buffer.BlockCopy(this.data, 62, buf, 0, buf.Length);
                return IMLibrary.Class.TextEncoder.bytesToText(buf);
            }
        }
        #endregion

        #region ���û��ȡ��Ϣ������ID
        /// <summary>
        /// ���û��ȡ��Ϣ������ID 
        /// </summary>
        public string ReceiveID
        {
            set
            {
                this.receiveID = IMLibrary.Class.TextEncoder.textToBytes(value.ToString());
                this.ReceiveIDLength = Convert.ToByte(this.receiveID.Length);
            }
            get
            {
                byte[] buf = new byte[this.ReceiveIDLength];
                Buffer.BlockCopy(this.data, 62 + this.SendIDLength, buf, 0, buf.Length);
                return IMLibrary.Class.TextEncoder.bytesToText(buf);
            }
        }
        #endregion

        #region ��ȡ��������Ϣ����
        /// <summary>
        /// ��ȡ��������Ϣ����
        /// </summary>
        public byte[] Content
        {
            set
            {
                this.content= value;
                this.ContentLength =Convert.ToUInt16(this.content.Length);//�����ֽڣ�����û�������Ϣʵ��ĳ���
            }
            get
            {
                byte[] buf = new byte[this.ContentLength];
                Buffer.BlockCopy(this.data, 62 + SendIDLength + ReceiveIDLength, buf, 0, buf.Length);
                return  buf;
            }
        }
        #endregion

        #region ��ȡ������ͼƬ��Ϣ����
        /// <summary>
        /// ��ȡ��������Ϣ����
        /// </summary>
        public byte[] ImageInfo
        {
            set
            {
                this.imageInfo  = value;
                this.ImageInfoLength = Convert.ToUInt16(this.imageInfo.Length);//�����ֽڣ�����û�������Ϣʵ��ĳ���
            }
            get
            {
                byte[] buf = new byte[this.ImageInfoLength];
                Buffer.BlockCopy(this.data, 62 + SendIDLength + ReceiveIDLength + ContentLength,  buf, 0, buf.Length);
                return  buf;
            }
        }
        #endregion

        #region ��ȡ������Ⱥ��ID
        /// <summary>
        /// ��ȡ������Ⱥ��ID���������Ⱥ����Ϣ�������ô��
        /// </summary>
        public int GroupID
        {
            set
            {
                this.groupID=BitConverter.GetBytes(value);
            }
            get
            {
                return BitConverter.ToInt32(this.data, this.data.Length - 4);
            }
        }
        #endregion

        #region ��ȡ��Ϣ�Ƿ�Ϸ�
        /// <summary>
        /// ��ȡ��Ϣ�Ƿ�Ϸ� 
        /// </summary>
        public bool IsMsg
        {
            get;
            private set;
        }
        #endregion

        #region ��ȡ��Ϣ�ֽ�����
        /// <summary>
        /// ��ȡ��Ϣ�ֽ����� 
        /// </summary>
        public byte[] Data
        {
            get { return data; }
            set {
                if (value.Length != BitConverter.ToUInt16(value, 0))//�����Ϣ���Ȳ�������Ϣ��ǰ�����ֽڼ�¼�ĳ������ǷǷ���Ϣ
                    IsMsg = false;
                else
                    IsMsg = true;

                data = value; 
            }
        }
        #endregion

        #region ��ȡ��Ϣ�ֽ�����
        /// <summary>
        /// ��ȡ��Ϣ�ֽ�����
        /// </summary>
        public byte[] getBytes()
        {
            List<byte> result = new List<byte>();
            result.AddRange(this.data);

            result.AddRange(this.sendID);
            result.AddRange(this.receiveID);
            result.AddRange(this.content);
            result.AddRange(this.imageInfo);
            result.AddRange(this.fontName);
            result.AddRange(this.groupID);

            this.data= result.ToArray();
            Buffer.BlockCopy(BitConverter.GetBytes(((ushort)this.data.Length)), 0, this.data, 0, 2);
            return this.data;
        }
        #endregion

        #region ��ʼ����Ϣ��
        /// <summary>
        /// ��ʼ����Ϣ��
        /// </summary>
        /// <param name="Data">�ֽ�����</param>
        public Msg(byte[] Data)
        {
            if (Data.Length != BitConverter.ToUInt16(Data, 0))//�����Ϣ���Ȳ�������Ϣ��ǰ�����ֽڼ�¼�ĳ������ǷǷ���Ϣ
            {
                IsMsg = false;
            }
            else
            { IsMsg = true; }
            this.data = Data;
        }
        #endregion

        #region ��ʼ����Ϣ��
        /// <summary>
        /// ��ʼ����Ϣ��
        /// </summary>
        public Msg()
        {

        }
        #endregion

        #region ��ʼ����Ϣ
        /// <summary>
        /// ��ʼ����Ϣ
        /// </summary>
        /// <param name="InfoClass">��ϢЭ������</param>
        /// <param name="MsgContent">��Ϣ����ʵ��</param>
        /// <param name="MsgImageInfo">��Ϣ������ͼƬ��Ϣʵ��</param>
        /// <param name="msgFont">��Ϣ����</param>
        /// <param name="msgTextColor">��Ϣ��ɫ</param>
        /// <param name="msgReceiveID">��Ϣ������ID</param>
        /// <param name="msgReceiveIndex">��Ϣ�������ڷ������ϵ��û�״̬����</param>
        public Msg(byte InfoClass, string  MsgContent, string  MsgImageInfo,System.Drawing.Font msgFont, System.Drawing.Color msgTextColor, string msgReceiveID,  int msgReceiveIndex)
        {
            this.InfoClass  = InfoClass;
            this.Content = IMLibrary.Class.TextEncoder.textToBytes(MsgContent);
            this.ImageInfo = IMLibrary.Class.TextEncoder.textToBytes(MsgImageInfo);
            this.font = msgFont;//�����Ϣ�������ֵ��ʼ
            this.Color = msgTextColor;
            this.ReceiveID = msgReceiveID;
            this.ReceiveIndex = msgReceiveIndex;
         }

        /// <summary>
        /// ��ʼ����Ϣ
        /// </summary>
        /// <param name="InfoClass">��ϢЭ������</param>
        /// <param name="MsgContent">��Ϣ����ʵ��</param>
        /// <param name="MsgImageInfo">��Ϣ������ͼƬ��Ϣʵ��</param>
        /// <param name="msgFont">��Ϣ����</param>
        /// <param name="msgTextColor">��Ϣ��ɫ</param>
        /// <param name="msgReceiveID">��Ϣ������ID</param>
        /// <param name="msgReceiveIndex">��Ϣ�������ڷ������ϵ��û�״̬����</param>
        public Msg(byte InfoClass, byte[] MsgContent, byte[] MsgImageInfo, System.Drawing.Font msgFont, System.Drawing.Color msgTextColor, string msgReceiveID,  int msgReceiveIndex)
        {
            this.InfoClass = InfoClass;
            this.Content = MsgContent ;
            this.ImageInfo = MsgImageInfo;
            this.font = msgFont;
            this.Color = msgTextColor;
            this.ReceiveID = msgReceiveID;
            this.ReceiveIndex = msgReceiveIndex;
        }

        /// <summary>
        /// ��ʼ����Ϣ
        /// </summary>
        /// <param name="InfoClass">��ϢЭ������</param>
        /// <param name="MsgContent">��Ϣ����ʵ��</param>
        /// <param name="msgSendID">������ID</param>
        /// <param name="msgSendIndex">�������ڷ������ϵ��û�״̬����</param>
        /// <param name="msgSendPassword">�������ڷ������ϻ�õ���ϵ����</param>
        public Msg(byte InfoClass, byte[] MsgContent, string msgSendID, int msgSendIndex, string msgSendPassword)
        {
            this.InfoClass = InfoClass;
            this.Content = MsgContent;
            this.SendID = msgSendID;//�����Ϣ������ID 
            this.SendIndex = msgSendIndex;//���û��ȡ��Ϣ�������ڷ������ϵ�����
            this.Password = msgSendPassword;
         }

         /// <summary>
         /// ��ʼ����Ϣ
         /// </summary>
         /// <param name="InfoClass">��ϢЭ������</param>
         /// <param name="MsgContent">��Ϣ����ʵ��</param>
         /// <param name="msgSendID">������ID</param>
         public Msg(byte InfoClass, byte[] MsgContent, string msgSendID)
         {
             this.InfoClass = InfoClass;
             this.Content = MsgContent;
             this.SendID = msgSendID;//�����Ϣ������ID 
         }

        /// <summary>
         /// ��ʼ����Ϣ
        /// </summary>
        /// <param name="InfoClass">��ϢЭ������</param>
        /// <param name="MsgContent">��Ϣ����ʵ��</param>
        public Msg(byte InfoClass, byte[] MsgContent)
        {
            this.InfoClass = InfoClass;
            this.Content = MsgContent;
         }

         /// <summary>
        /// ��ʼ����Ϣ
         /// </summary>
         /// <param name="InfoClass">��ϢЭ������</param>
        /// <param name="msgSendID">������ID</param>
         public Msg(byte InfoClass, string msgSendID)
         {
             this.InfoClass = InfoClass;
             this.SendID = msgSendID;//�����Ϣ������ID 
         }
 

         /// <summary>
         /// ��ʼ����Ϣ
         /// </summary>
         /// <param name="InfoClass">��ϢЭ������</param>
         public Msg(byte InfoClass)
         {
             this.InfoClass = InfoClass;
         }

        #endregion
    }
}
