using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CSS.IM.Library.Class 
{
    /// <summary>
    /// ����������
    /// </summary>
    sealed public class Calculate
    {
        #region ����ļ��ߴ��ַ��� GetSizeStr()
        private const float GB = 1024 * 1024 * 1024;
        private const float MB = 1024 * 1024;
        private const float KB = 1024;

        /// <summary>
        /// ����ļ��ĳߴ��ַ���
        /// </summary>
        /// <param name="fileSize">�ļ�����(��ǰ�汾ֻ֧��2G)</param>
        /// <returns></returns>
        public static string GetSizeStr(int fileSize)
        {
            try
            {
                float TempSize = fileSize / GB;
                if (TempSize > 1)
                {
                    return TempSize.ToString("0.00") + "GB";
                }

                TempSize = fileSize / MB;
                if (TempSize > 1)
                {
                    return TempSize.ToString("0.00") + "MB";
                }

                TempSize = fileSize / KB;
                if (TempSize > 1)
                {
                    return TempSize.ToString("0.00") + "KB";
                }
                return fileSize + "�ֽ�";
            }
            catch { return fileSize + "�ֽ�"; }
        }
        #endregion

        #region ����ʱ���
        /// <summary>
        /// ����ʱ���,�������
        /// </summary>
        /// <param name="DateTime1">��ʼʱ��</param>
        /// <param name="DateTime2">����ʱ��</param>
        /// <returns>�������</returns>
        public static  int DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            int dateDiff = 0;
            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                dateDiff = ts2.Subtract(ts1).Seconds;
            }
            catch
            { }
            return dateDiff;
        }
        #endregion
        
        #region ��ò���ʾ�ļ�����ʣ��ʱ��
       /// <summary>
        /// ��ò���ʾ�ļ�����ʣ��ʱ��
       /// </summary>
       /// <param name="fileLen">�ļ�����</param>
       /// <param name="currTransmittedLen">��ǰ������ɵ����ݳ���</param>
       /// <param name="lastTransmittedLen">�ϴδ�����ɵ����ݳ���</param>
       /// <returns></returns>
       public  static string getResidualTime(int fileLen, int currTransmittedLen, int lastTransmittedLen)
        {
            try
            {
                int speed = (fileLen - currTransmittedLen) /(currTransmittedLen-lastTransmittedLen) + 1;
                string s = ""; 

                int tempSpeed = speed / 3600;
                if (tempSpeed > 0)
                {
                    s = tempSpeed.ToString() + "Сʱ";
                    speed = speed % 3600;
                }

                tempSpeed = speed / 60;//��÷���
                if (tempSpeed > 0)
                {
                    s += tempSpeed.ToString() + "��";
                    speed = speed % 60;
                }

                s += speed.ToString() + "��";

                return s.ToString();
            }
            catch { return lastTransmittedLen.ToString(); }
        }
        #endregion

        #region ����Ϣд����־�ļ� WirteLog(string str)
        /// <summary>
        /// LanMsgд��־����
        /// </summary>
        /// <param name="str">Ҫд�����־�����ַ���</param>
        public static  void WirteLog(string str)
        {
            try
            {
                FileStream fs = new FileStream(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter _streamWriter = new StreamWriter(fs);
                _streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                _streamWriter.WriteLine(str + " " + DateTime.Now.ToString() + "\n");
                _streamWriter.Flush();
                _streamWriter.Close();
                fs.Close();
            }
            catch
            {
            }
        }
        #endregion

        #region ����ʱ���ַ���
        /// <summary>
        /// ����ʱ���ַ���
        /// </summary>
        /// <param name="dateLength">ʱ�䳤�ȣ���Ϊ��λ��</param>
        /// <returns>����ʱ���ַ���</returns>
        public static string getDateConverToStr(int dateLength)
        {
            string dateStr = "";

            int mod = dateLength;//�������

            int year = mod / 31536000;//�����
            if (year > 0)
            {
                dateStr += year.ToString() + "��";
            }
            mod = mod % 31536000;

            int mon = mod / 2592000;//����� 
            if (mon > 0)
            {
                dateStr += mon.ToString() + "��";
            }
            mod = mod % 2592000;


            int day = mod / 86400;
            if (day > 0)
            {
                dateStr += day.ToString() + "��";
            }
            mod = mod % 86400;


            int hour = mod / 3600;//���Сʱ
            if (hour > 0)
            {
                dateStr += hour.ToString() + "Сʱ";
                mod = mod % 3600;
            }
            mod = mod % 3600;


            int mu = mod / 60;//��÷�
            if (mu > 0)
            {
                dateStr += mu.ToString() + "��";
            }
            mod = mod % 60;


            int ss = mod / 1;
            if (ss > 0)
                dateStr += ss.ToString() + "��";

            return dateStr;
        }
        #endregion
        
    }
}
