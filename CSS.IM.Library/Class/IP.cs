using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library.Class 
{
    /// <summary>
    /// ip地址数据类型操作类
    /// </summary>
    public sealed  class IP
    {
        #region 将IP地址转换为字节数组
        /// <summary>
        ///  将IP地址转换为字节数组
        /// </summary>
        /// <param name="ipAddress">要转换的ipAddress</param>
        /// <returns>返回转换后的字节数组</returns>
        public static byte[] IPAddressToBytes(System.Net.IPAddress ipAddress)
        {
            byte[] ip = new byte[4];
            try
            {
                string[] arrayIP = ipAddress.ToString().Split('.');
                ip[0] = Convert.ToByte(arrayIP[0]);
                ip[1] = Convert.ToByte(arrayIP[1]);
                ip[2] = Convert.ToByte(arrayIP[2]);
                ip[3] = Convert.ToByte(arrayIP[3]);
            }
            catch { }
            return ip;//返回IP地址转换后 的数字
        }
        #endregion

        #region 将字节数组转换为IP地址
        /// <summary>
        /// 将字节数组转换为IP地址 
        /// </summary>
        /// <param name="IPAddressBytes">要转换的字节数组</param>
        /// <returns>返回IP地址</returns>
        public static System.Net.IPAddress BytesToIPAddress(byte[] IPAddressBytes)
        {
            System.Net.IPAddress tempIp = null;
            try
            {
                string strIPAddress = IPAddressBytes[0].ToString() + "." + IPAddressBytes[1].ToString() + "." + IPAddressBytes[2].ToString() + "." + IPAddressBytes[3].ToString();

                tempIp = System.Net.IPAddress.Parse(strIPAddress);//返回IP地址
            }
            catch { }
            return tempIp;
        }
        #endregion

        #region 将IP地址转换成整型数据
        /// <summary>
        /// 将IP地址转换成整型数据
        /// </summary>
        /// <param name="ipAddress">要转换的ipAddress</param>
        /// <returns>返回转换后的整型数字</returns>
        public static int IPAddressToInt(System.Net.IPAddress ipAddress)
        {
            byte[] ip = IPAddressToBytes(ipAddress);
            return BitConverter.ToInt32(ip, 0);
        }
        #endregion

        #region 将整型数据转换成IP地址
        /// <summary>
        /// 将整型数据转换成IP地址
        /// </summary>
        /// <param name="intIP">要转换的IP</param>
        /// <returns>返回INT值</returns>
        public static System.Net.IPAddress intToIPAddress(int intIP)
        {
            byte[] ip = BitConverter.GetBytes(intIP);
            return BytesToIPAddress(ip);
        }
        #endregion
    }
}
