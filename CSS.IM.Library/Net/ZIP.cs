using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;


namespace CSS.IM.Library.Net
{
    public class ZIP
    {
        /// <summary>
        /// 压缩byte数组
        /// </summary>
        /// <param name="inBytes">需要压缩的byte数组</param>
        /// <returns>压缩好的byte数组</returns>
        public static byte[] CompressByte(byte[] inBytes)
        {
            MemoryStream outStream = new MemoryStream();
            Stream zipStream = new GZipOutputStream(outStream);
            zipStream.Write(inBytes, 0, inBytes.Length);
            zipStream.Close();
            byte[] outData = outStream.ToArray();
            outStream.Close();
            return outData;
        }

        /// <summary>
        /// 解压缩byte数组
        /// </summary>
        /// <param name="inBytes">需要解压缩的byte数组</param>
        /// <returns></returns>
        public static byte[] DecompressByte(byte[] inBytes)
        {
            byte[] writeData = new byte[2048];
            MemoryStream inStream = new MemoryStream(inBytes);
            Stream zipStream = new GZipInputStream(inStream) as Stream;
            MemoryStream outStream = new MemoryStream();
            while (true)
            {
                int size = zipStream.Read(writeData, 0, writeData.Length);
                if (size > 0)
                {
                    outStream.Write(writeData, 0, size);
                }
                else
                {
                    break;
                }
            }
            inStream.Close();
            byte[] outData = outStream.ToArray();
            outStream.Close();
            return outData;
        }
    }
}
