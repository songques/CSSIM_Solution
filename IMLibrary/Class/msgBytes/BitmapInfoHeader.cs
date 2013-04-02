using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary.Class 
{

    /// <summary>
    /// 视频图像头信息
    /// </summary>
    public  class BitmapInfoHeader
    {
        //public struct BITMAPINFOHEADER
        //{
        //    public int biSize;0
        //    public int biWidth;4
        //    public int biHeight;8
        //    public short biPlanes;12
        //    public short biBitCount;14
        //    public int biCompression;16
        //    public int biSizeImage;20
        //    public int biXPelsPerMeter;24
        //    public int biYPelsPerMeter;28
        //    public int biClrUsed;32
        //    public int biClrImportant;36
        //} 

        private byte[] data = new byte[40];//类转换后的字节数组 

        /// <summary>
        /// 初始化视频图像头信息
        /// </summary>
        /// <param name="Data">要初始化的内容</param>
        public BitmapInfoHeader(byte[] Data)  
        {
            this.data = Data;
        }

        /// <summary>
        /// 初始化视频图像头信息
        /// </summary>
        public BitmapInfoHeader( )
        {

        }

        #region 设置或获取biSize
        /// <summary>
        /// 设置或获取biSize
        /// </summary>
        public int biSize
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 0, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data, 0);
            }
        }
        #endregion

        #region 设置或获取biWidth
        /// <summary>
        /// 设置或获取biWidth 
        /// </summary>
        public int biWidth
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 4, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data, 4);
            }
        }
        #endregion

        #region 设置或获取biHeight
        /// <summary>
        /// 设置或获取biHeight 
        /// </summary>
        public int biHeight
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 8, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data, 8);
            }
        }
        #endregion

        #region 设置或获取biPlanes
        /// <summary>
        /// 设置或获取biPlanes 
        /// </summary>
        public short biPlanes
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data,12, 2);
            }
            get
            {
                return BitConverter.ToInt16(this.data, 12);
            }
        }
        #endregion

        #region 设置或获取biBitCount
        /// <summary>
        /// 设置或获取biBitCount 
        /// </summary>
        public short biBitCount
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 14, 2);
            }
            get
            {
                return BitConverter.ToInt16(this.data, 14);
            }
        }
        #endregion

        #region 设置或获取biCompression
        /// <summary>
        /// 设置或获取biCompression 
        /// </summary>
        public int  biCompression
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

        #region 设置或获取biSizeImage
        /// <summary>
        /// 设置或获取biSizeImage 
        /// </summary>
        public int biSizeImage
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

        #region 设置或获取biXPelsPerMeter
        /// <summary>
        /// 设置或获取biXPelsPerMeter 
        /// </summary>
        public int biXPelsPerMeter
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 24, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data, 24);
            }
        }
        #endregion

        #region 设置或获取biYPelsPerMeter
        /// <summary>
        /// 设置或获取biYPelsPerMeter                         
        /// </summary>
        public int biYPelsPerMeter
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 28, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data, 28);
            }
        }
        #endregion

        #region 设置或获取biClrUsed
        /// <summary>
        /// 设置或获取biClrUsed                         
        /// </summary>
        public int biClrUsed
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 32, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data, 32);
            }
        }
        #endregion

        #region 设置或获取biClrImportant
        /// <summary>
        /// 设置或获取biClrImportant                         
        /// </summary>
        public int biClrImportant
        {
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.data, 36, 4);
            }
            get
            {
                return BitConverter.ToInt32(this.data, 36);
            }
        }
        #endregion

        #region 设置或获取类字节数组
        /// <summary>
        /// 设置或获取类字节数组
        /// </summary>
        public byte[] getBytes()
        {
             return this.data;
        }
        #endregion
    }
}
