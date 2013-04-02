using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace IMLibrary.Net
{
    #region ��װTCP��Ϣ��
    /// <summary>
    /// ��װTCP��Ϣ��
    /// </summary>
    sealed  class SealedMsg
    {
        /// <summary>
        ///  ��װ���͵���Ϣ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static  byte[] SendMsgBytes(byte[] data)
        {
            ushort msgLen =Convert.ToUInt16 (data.Length);
            byte[] buf = new byte[msgLen+ 2];
            byte[] msgLenBytes = BitConverter.GetBytes(msgLen);
            Buffer.BlockCopy(msgLenBytes, 0, buf, 0, 2);
            Buffer.BlockCopy(data, 0, buf, 2, data.Length);
            return buf;
        }

        /// <summary>
        /// ����װ����Ϣ��ԭ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] RecvMsgBytes(byte[] data)
        {
            ushort msgLen =BitConverter.ToUInt16(data,0);
            byte[] buf = new byte[msgLen];
            Buffer.BlockCopy(data,2,buf,0,msgLen);
            return buf;
        }
    }
    #endregion

    #region TCP�׽���״̬��
    /// <summary>
    /// TCP�׽���״̬��
    /// </summary>
    public class  MyTcp 
    {
        /// <summary>
        /// �׽���ID
        /// </summary>
        public string ID= Guid.NewGuid().ToString().Replace("-", ""); 
        /// <summary>
        /// �����׽���
        /// </summary>
        public System.Net.Sockets.Socket Socket= null;
        /// <summary>
        /// �׽��ֻ�������С
        /// </summary>
        public const int BufferSize = 1400;
        /// <summary>
        /// �׽��ֻ�����
        /// </summary>
        public byte[] Buffer = new byte[BufferSize];
        /// <summary>
        /// ��ʱ��
        /// </summary>
        private  System.Timers.Timer timer = new System.Timers.Timer();
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public bool IsValidate = false;
        /// <summary>
        /// �Ƿ�ر�
        /// </summary>
        public bool Closed = false;
        /// <summary>
        /// �������ӿͻ��б�
        /// </summary>
        public IList<byte> result  ;

        /// <summary>
        /// ��ȡԶ��IP
        /// </summary>
        public System.Net.IPAddress IP
        {
            get
            {
                if (Socket != null)
                {
                    System.Net.IPEndPoint ip = (System.Net.IPEndPoint)Socket.RemoteEndPoint;
                     
                    return ip.Address;
                }
                return null;
            }
        }
        
        /// <summary>
        /// ��ȡԶ�̶˿�
        /// </summary>
        public int Port
        {
            get
            {
                if (Socket != null)
                {
                    System.Net.IPEndPoint ip = (System.Net.IPEndPoint)Socket.RemoteEndPoint;

                    return ip.Port;
                }
                return 0;
            }
        }

      /// <summary>
      /// ���ö�����Чʱ��
      /// </summary>
        public double ValidateTimeLong
        {
            get { return this.timer.Interval; }
            set { this.timer.Interval = value; }
        }

        /// <summary>
        /// �ͻ��˶Ͽ������¼�
        /// </summary>
        public delegate void CloseEventHandler(object sender, IMLibrary.Net.SockEventArgs e);
        /// <summary>
        /// �ͻ��˶Ͽ������¼�
        /// </summary>
        public event CloseEventHandler OnClose;

       /// <summary>
       /// ��ʼ�� 
       /// </summary>
        public MyTcp()
        {
            this.timer.Interval = 5000;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        }

        /// <summary>
        /// ��������Ч�ڵļ�ʱ����ʼ����
        /// </summary>
        public void timerStrat()
        {
            this.timer.Enabled = true;
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (!IsValidate && Socket != null)
                {
                    if (this.Socket.Connected)
                        this.Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);

                    this.Socket.Close();
                    this.Socket = null;

                    if (OnClose != null)
                        OnClose(this, null);
                }
            }
            catch
            {

            }
            timer.Enabled = false;
        }
    }
    #endregion
     
}
