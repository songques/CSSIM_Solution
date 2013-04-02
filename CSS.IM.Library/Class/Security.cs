using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace CSS.IM.Library.Security
{
    /// <summary>
    /// �ַ����ӽ���
    /// </summary>
    public sealed class  DES
    {
        //Ĭ����Կ����
        private static byte[] IV = { 0x65, 0x88, 0x35, 0x71, 0x60, 0x1B, 0x2D, 0x7F };
        private static string key = "ligy@163";
        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ��</returns>
        public static string Encrypt (string toEncryptString)
        {
            try
            {
                if (toEncryptString == null || toEncryptString == "" || toEncryptString == string.Empty) return "";
                byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] rgbIV = IV;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(toEncryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return toEncryptString;
            }
        }

        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <param name="toDecryptString">�����ܵ��ַ���</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ�Դ��</returns>
        public static string Decrypt(string toDecryptString)
        {
            try
            {
                if (toDecryptString == null || toDecryptString == "" || toDecryptString == string.Empty) return "";
                byte[] rgbKey = Encoding.UTF8.GetBytes(key);
                byte[] rgbIV = IV;
                byte[] inputByteArray = Convert.FromBase64String(toDecryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return toDecryptString;
            }
        }

        /// <summary>
        /// DES���ܷ���
        /// </summary>
        /// <param name="toEncryptString">����</param>
        /// <param name="Key">��Կ</param>
        /// <param name="IV">����</param>
        /// <returns>����</returns>
        public static string Encrypt(string toEncryptString, string Key, string IV)
        {
            //����Կת�����ֽ�����
            byte[] bytesDESKey = ASCIIEncoding.ASCII.GetBytes(Key);
            //������ת�����ֽ�����
            byte[] bytesDESIV = ASCIIEncoding.ASCII.GetBytes(IV);
            //����1���µ�DES����
            DESCryptoServiceProvider desEncrypt = new DESCryptoServiceProvider();
            //����һ���ڴ���
            MemoryStream msEncrypt = new MemoryStream();
            //���ڴ��������װ�ɼ���������
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, desEncrypt.CreateEncryptor(bytesDESKey, bytesDESIV), CryptoStreamMode.Write);
            //�Ѽ����������װ��д��������
            StreamWriter swEncrypt = new StreamWriter(csEncrypt);
            //д��������д������
            swEncrypt.WriteLine(toEncryptString);
            //д�����ر�
            swEncrypt.Close();
            //�������ر�
            csEncrypt.Close();
            //���ڴ���ת�����ֽ����飬�ڴ��������Ѿ���������
            byte[] bytesCipher = msEncrypt.ToArray();
            //�ڴ����ر�
            msEncrypt.Close();
            //�������ֽ�����ת��Ϊ�ַ�����������
            return Encoding.UTF8.GetString (bytesCipher);
        }


        /// <summary>
        /// DES���ܷ���
        /// </summary>
        /// <param name="toDecryptString">����</param>
        /// <param name="Key">��Կ</param>
        /// <param name="IV">����</param>
        /// <returns>����</returns>
        public static string Decrypt(string toDecryptString, string Key, string IV)
        {
            //����Կת�����ֽ�����
            byte[] bytesDESKey = ASCIIEncoding.ASCII.GetBytes(Key);
            //������ת�����ֽ�����
            byte[] bytesDESIV = ASCIIEncoding.ASCII.GetBytes(IV);
            //������ת�����ֽ�����
            byte[] bytesCipher = UnicodeEncoding.Unicode.GetBytes(toDecryptString);
            //����1���µ�DES����
            DESCryptoServiceProvider desDecrypt = new DESCryptoServiceProvider();
            //����һ���ڴ���������������ֽ�����
            MemoryStream msDecrypt = new MemoryStream(bytesCipher);
            //���ڴ��������װ�ɽ���������
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, desDecrypt.CreateDecryptor(bytesDESKey, bytesDESIV), CryptoStreamMode.Read);
            //�ѽ����������װ�ɶ���������
            StreamReader srDecrypt = new StreamReader(csDecrypt);
            //����=�������Ķ�������
            string strPlainText = srDecrypt.ReadLine();
            //�������ر�
            srDecrypt.Close();
            //�������ر�
            csDecrypt.Close();
            //�ڴ����ر�
            msDecrypt.Close();
            //��������
            return strPlainText;
        }

    }
}
