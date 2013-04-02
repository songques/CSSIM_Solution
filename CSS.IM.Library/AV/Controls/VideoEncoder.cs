using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CSS.IM.Library.AV.Controls
{
    /// <summary>
    /// 视频编解码器
    /// </summary>
    public class VideoEncoder
    {
        /// <summary>
        /// 视频编码器 
        /// </summary>
        private ICCompressor Compressor;

        /// <summary>
        /// 视频解码器
        /// </summary>
        private ICDecompressor Decompressor;

        /// <summary>
        /// 标识当前采用的是编码还是解码功能
        /// </summary>
        private bool IsEncode = true;

       /// <summary>
       /// 初始化视频编解码器
       /// </summary>
       /// <param name="bitmapInfoHeader">图像头信息</param>
       /// <param name="isEncode">标识完成编码还是解码功能</param>
        public VideoEncoder(BITMAPINFOHEADER bitmapInfoHeader, bool isEncode)
        {
            #region
            //BITMAPINFOHEADER bmi = new BITMAPINFOHEADER ();
            //bmi.biWidth = bitmapInfoHeader.biWidth;
            //bmi.biHeight = bitmapInfoHeader.biHeight;
            //if (isEncode)
            //{
            //    bmi.biCompression =bitmapInfoHeader.biCompression; 
            //}
            //else
            //{
            //    bmi.biCompression = FOURCC.MP42;
            //}
            //bmi.biSizeImage =bitmapInfoHeader.biSizeImage;  
            //bmi.biPlanes = bitmapInfoHeader.biPlanes;
            //bmi.biBitCount =   bitmapInfoHeader.biBitCount; 
            //bmi.biXPelsPerMeter = bitmapInfoHeader.biXPelsPerMeter;
            //bmi.biYPelsPerMeter = bitmapInfoHeader.biYPelsPerMeter;
            //bmi.biClrUsed = bitmapInfoHeader.biClrUsed;
            //bmi.biClrImportant = bitmapInfoHeader.biClrImportant;
            //bmi.biSize = bitmapInfoHeader.biSize;
            //bitmapInfo.bmiHeader = bmi;
            #endregion

            BITMAPINFO bitmapInfo = new BITMAPINFO();
            bitmapInfo.bmiHeader = bitmapInfoHeader;

            this.IsEncode = isEncode;
            if (isEncode)
            {
                COMPVARS compvars = new COMPVARS();
                compvars.cbSize = Marshal.SizeOf(compvars);
                compvars.dwFlags = 1;
                compvars.fccHandler = FOURCC.MP42;
                compvars.fccType = FOURCC.ICTYPE_VIDEO;
                compvars.lDataRate = 780;
                compvars.lKey = 15;
                compvars.lQ = -1;
                compvars.lQ = 500;

                this.Compressor = new ICCompressor(compvars, bitmapInfo, FOURCC.MP42);
                this.Compressor.Open();//打开编码器
            }
            else
            {
                bitmapInfo.bmiHeader.biCompression = FOURCC.MP42;
                this.Decompressor = new ICDecompressor(new COMPVARS(), bitmapInfo, FOURCC.MP42);
                this.Decompressor.Open();
            }
        }

        /// <summary>
        /// 视频编码(视频压缩)
        /// </summary>
        /// <param name="data">要压缩的数据</param>
        public byte[] Encode(byte[] data)
        {
           return this.Compressor.Process(data);
        }

        /// <summary>
        /// 视频解码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Decode(byte[] data)
        {
            try
            {
                return this.Decompressor.Process(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        /// <summary>
        /// 关闭视频编解码器
        /// </summary>
        public void Close()
        {
            if (IsEncode)
                this.Compressor.Close();
            else
                this.Decompressor.Close();
        }
    }
}
