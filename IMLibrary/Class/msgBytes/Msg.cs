using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class 
{
    /// <summary>
    /// 即时消息类
    /// </summary>
    public class Msg 
    {
        #region 变量区

        //private byte[] length=new byte[2];//消息长度 0 
        //private byte[] infoClass = new byte[1];//消息类型 2
        //private byte[] transferInfoClass = new byte[1];//中转消息类型 3
        //private byte[] totalFragment=new byte[1];//消息总分片数 4
        //private byte[] sequenceFragment =new byte[1];//消息分片索引 5
        //private byte[] fontNameLength = new byte[1];//消息字体名长度 6
        //private byte[] fontSize = new byte[1];//字体大小 7
        //private byte[] fontBold = new byte[1];//消息字体榜度 8
        //private byte[] fontItalic = new byte[1];//消息文字是否倾斜 9
        //private byte[] fontStrikeout = new byte[1];//消息文字是否有删除线 10
        //private byte[] fontUnderline = new byte[1];//消息文字是否有下划线 11
        //private byte[] fontColor = new byte[4];//消息文本颜色分量 int32 12
        //private byte[] sendIndex = new byte[4];//消息发送者在服务器上的ID索引 16
        //private byte[] receiveIndex = new byte[4];//消息接收者在服务器上的索引 20
        //private byte[] sendIDLength = new byte[1];//消息发送者ID的长度 24
        //private byte[] receiveIDLength = new byte[1];//消息接收者ID的长度 25
        //private byte[] contentLength = new byte[2];//消息内容的长度,不大于65535，值取为ushort 26
        //private byte[] imageInfoLength = new byte[2];//消息包含的图片信息内容的长度,不大于65535，值取为ushort 28
        //private byte[] password = new byte[32];//消息发送者在服务器上的密码 30

        private byte[] sendID = new byte[0];//消息发送者在服务器上的ID号 62
        private byte[] receiveID = new byte[0];//消息接收者ID  
        private byte[] content = new byte[0];//消息内容实体
        private byte[] imageInfo = new byte[0];//消息包含图片信息的实体

        private byte[] fontName = new byte[0];//消息字体名 

        private byte[] groupID = new byte[0];//群组的ID号 

        private byte[] data = new byte[62];//消息转换后的字节数组

        #endregion

        #region 获取消息长度
        /// <summary>
        /// 获取消息长度 
        /// </summary>
        public ushort Length
        {
            get
            {
                return BitConverter.ToUInt16(this.data, 0);
            }
        }
        #endregion

        #region 设置或获取消息协议
        /// <summary>
        /// 设置或获取消息协议
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

        #region 设置或获取中转消息类型
        /// <summary>
        /// 设置或获取中转消息类型
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

        #region 设置或获取消息分片总数
        /// <summary>
        /// 设置或获取消息分片总数
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

        #region 设置或获取消息分片索引
        /// <summary>
        /// 设置或获取消息分片索引
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

        #region 获取消息字体名长度
        /// <summary>
        /// 获取消息字体名长度
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

        #region 获取消息字体名大小
        /// <summary>
        /// 获取消息字体名大小
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

        #region 获取消息字体是否粗体
        /// <summary>
        /// 获取消息字体是否粗体
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

        #region 获取消息字体是否斜体
        /// <summary>
        /// 获取消息字体是否斜体
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

        #region 获取消息字体是否删除线
        /// <summary>
        /// 获取消息字体是否删除线
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

        #region 获取消息字体是否下划线
        /// <summary>
        /// 获取消息字体是否下划线
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

        #region 获取消息字体名
        /// <summary>
        /// 获取消息字体名
        /// </summary>
        private string  FontName
        {
            set
            {
                this.fontName = TextEncoder.textToBytes(value);//获得字体名称
                this.FontNameLength = Convert.ToByte(this.fontName.Length);//获得字体字节数组长度
            }
            get
            {
                byte[] buf = new byte[this.FontNameLength];
                Buffer.BlockCopy(this.data, 62 + SendIDLength + ReceiveIDLength + ContentLength + ImageInfoLength,  buf, 0,   buf.Length);
                return TextEncoder.bytesToText(buf);
            }
        }
        #endregion

        #region 设置或获取消息字体
        /// <summary>
        /// 设置或获取消息字体 
        /// </summary>
        public System.Drawing.Font font
        {
            set
            {
                ////获得消息字体各项值开始
                this.FontName = value.Name;//获得字体名称
                this.FontSize = value.Size;//字体大小
                this.FontBold = value.Bold;//是否粗体
                this.FontItalic = value.Italic;//是否斜体
                this.FontStrikeout = value.Strikeout;//是否删除线
                this.FontUnderline = value.Underline;//是否下划线
                /////获得消息字体各项值结束
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

        #region 消息文本颜色
        /// <summary>
        ///  获取或设置消息文本颜色
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
           
        #region 设置或获取消息发送者在服务器上的索引
        /// <summary>
        /// 设置或获取消息发送者ID
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

        #region 设置或获取消息接收者在服务器上的索引
        /// <summary>
        /// 设置或获取消息接收者在服务器上的索引
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

        #region 获取消息发送者ID的长度
        /// <summary>
        /// 获取消息发送者ID的长度
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

        #region 获取消息接收者ID的长度
        /// <summary>
        /// 获取消息接收者ID的长度
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

        #region 获取消息内容的长度
        /// <summary>
        /// 获取消息内容的长度
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

        #region 获取消息图片信息内容的长度
        /// <summary>
        /// 获取消息图片信息内容的长度
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

        #region 设置或获取消息密码
        /// <summary>
        /// 设置或获取消息密码 
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

        #region 设置或获取消息发送者ID
        /// <summary>
        /// 设置或获取消息发送者ID
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

        #region 设置或获取消息接收者ID
        /// <summary>
        /// 设置或获取消息接收者ID 
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

        #region 获取或设置消息内容
        /// <summary>
        /// 获取或设置消息内容
        /// </summary>
        public byte[] Content
        {
            set
            {
                this.content= value;
                this.ContentLength =Convert.ToUInt16(this.content.Length);//两个字节，获得用户发送消息实体的长度
            }
            get
            {
                byte[] buf = new byte[this.ContentLength];
                Buffer.BlockCopy(this.data, 62 + SendIDLength + ReceiveIDLength, buf, 0, buf.Length);
                return  buf;
            }
        }
        #endregion

        #region 获取或设置图片消息内容
        /// <summary>
        /// 获取或设置消息内容
        /// </summary>
        public byte[] ImageInfo
        {
            set
            {
                this.imageInfo  = value;
                this.ImageInfoLength = Convert.ToUInt16(this.imageInfo.Length);//两个字节，获得用户发送消息实体的长度
            }
            get
            {
                byte[] buf = new byte[this.ImageInfoLength];
                Buffer.BlockCopy(this.data, 62 + SendIDLength + ReceiveIDLength + ContentLength,  buf, 0, buf.Length);
                return  buf;
            }
        }
        #endregion

        #region 获取或设置群组ID
        /// <summary>
        /// 获取或设置群组ID（如果发送群组消息必须设置此项）
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

        #region 获取消息是否合法
        /// <summary>
        /// 获取消息是否合法 
        /// </summary>
        public bool IsMsg
        {
            get;
            private set;
        }
        #endregion

        #region 获取消息字节数组
        /// <summary>
        /// 获取消息字节数组 
        /// </summary>
        public byte[] Data
        {
            get { return data; }
            set {
                if (value.Length != BitConverter.ToUInt16(value, 0))//如果消息长度不等于消息中前两个字节记录的长度则是非法消息
                    IsMsg = false;
                else
                    IsMsg = true;

                data = value; 
            }
        }
        #endregion

        #region 获取消息字节数组
        /// <summary>
        /// 获取消息字节数组
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

        #region 初始化消息类
        /// <summary>
        /// 初始化消息类
        /// </summary>
        /// <param name="Data">字节数组</param>
        public Msg(byte[] Data)
        {
            if (Data.Length != BitConverter.ToUInt16(Data, 0))//如果消息长度不等于消息中前两个字节记录的长度则是非法消息
            {
                IsMsg = false;
            }
            else
            { IsMsg = true; }
            this.data = Data;
        }
        #endregion

        #region 初始化消息类
        /// <summary>
        /// 初始化消息类
        /// </summary>
        public Msg()
        {

        }
        #endregion

        #region 初始化消息
        /// <summary>
        /// 初始化消息
        /// </summary>
        /// <param name="InfoClass">消息协议类型</param>
        /// <param name="MsgContent">消息内容实体</param>
        /// <param name="MsgImageInfo">消息包括的图片信息实体</param>
        /// <param name="msgFont">消息字体</param>
        /// <param name="msgTextColor">消息颜色</param>
        /// <param name="msgReceiveID">消息接收者ID</param>
        /// <param name="msgReceiveIndex">消息接收者在服务器上的用户状态索引</param>
        public Msg(byte InfoClass, string  MsgContent, string  MsgImageInfo,System.Drawing.Font msgFont, System.Drawing.Color msgTextColor, string msgReceiveID,  int msgReceiveIndex)
        {
            this.InfoClass  = InfoClass;
            this.Content = IMLibrary.Class.TextEncoder.textToBytes(MsgContent);
            this.ImageInfo = IMLibrary.Class.TextEncoder.textToBytes(MsgImageInfo);
            this.font = msgFont;//获得消息字体各项值开始
            this.Color = msgTextColor;
            this.ReceiveID = msgReceiveID;
            this.ReceiveIndex = msgReceiveIndex;
         }

        /// <summary>
        /// 初始化消息
        /// </summary>
        /// <param name="InfoClass">消息协议类型</param>
        /// <param name="MsgContent">消息内容实体</param>
        /// <param name="MsgImageInfo">消息包括的图片信息实体</param>
        /// <param name="msgFont">消息字体</param>
        /// <param name="msgTextColor">消息颜色</param>
        /// <param name="msgReceiveID">消息接收者ID</param>
        /// <param name="msgReceiveIndex">消息接收者在服务器上的用户状态索引</param>
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
        /// 初始化消息
        /// </summary>
        /// <param name="InfoClass">消息协议类型</param>
        /// <param name="MsgContent">消息内容实体</param>
        /// <param name="msgSendID">发送者ID</param>
        /// <param name="msgSendIndex">发送者在服务器上的用户状态索引</param>
        /// <param name="msgSendPassword">发送者在服务器上获得的联系密码</param>
        public Msg(byte InfoClass, byte[] MsgContent, string msgSendID, int msgSendIndex, string msgSendPassword)
        {
            this.InfoClass = InfoClass;
            this.Content = MsgContent;
            this.SendID = msgSendID;//获得消息发送者ID 
            this.SendIndex = msgSendIndex;//设置或获取消息发送者在服务器上的索引
            this.Password = msgSendPassword;
         }

         /// <summary>
         /// 初始化消息
         /// </summary>
         /// <param name="InfoClass">消息协议类型</param>
         /// <param name="MsgContent">消息内容实体</param>
         /// <param name="msgSendID">发送者ID</param>
         public Msg(byte InfoClass, byte[] MsgContent, string msgSendID)
         {
             this.InfoClass = InfoClass;
             this.Content = MsgContent;
             this.SendID = msgSendID;//获得消息发送者ID 
         }

        /// <summary>
         /// 初始化消息
        /// </summary>
        /// <param name="InfoClass">消息协议类型</param>
        /// <param name="MsgContent">消息内容实体</param>
        public Msg(byte InfoClass, byte[] MsgContent)
        {
            this.InfoClass = InfoClass;
            this.Content = MsgContent;
         }

         /// <summary>
        /// 初始化消息
         /// </summary>
         /// <param name="InfoClass">消息协议类型</param>
        /// <param name="msgSendID">发送者ID</param>
         public Msg(byte InfoClass, string msgSendID)
         {
             this.InfoClass = InfoClass;
             this.SendID = msgSendID;//获得消息发送者ID 
         }
 

         /// <summary>
         /// 初始化消息
         /// </summary>
         /// <param name="InfoClass">消息协议类型</param>
         public Msg(byte InfoClass)
         {
             this.InfoClass = InfoClass;
         }

        #endregion
    }
}
