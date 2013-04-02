using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CSS.IM.Library.Controls;
using CSS.IM.Library.Class;

namespace CSS.IM.App.Controls 
{
    public partial class p2pFile  : UserControl
    {
        public p2pFile()
        {
            InitializeComponent();
        }

        #region �Զ��������
 
        /// <summary>
        /// �ϴδ����ļ����ݵĳ���
        /// </summary>
        private int lastTransmitLen = 0;

        /// <summary>
        /// ��һ���յ��������ݵ�ʱ��
        /// </summary>
        private DateTime lastTime = System.DateTime.Now;

        public string FileMD5Value;//�ļ�MD5ֵ
        
        #endregion

        #region ���ô����ļ�����
         
        /// <summary>
        /// ���ô����ļ�����
        /// </summary>
        /// <param name="isSend">��ʶ�ļ��Ƿ��ͻ��ǽ���</param>
        /// <param name="FullFileName">��������ļ���������Ҫ���ŵ��ļ�·��</param>
        /// <param name="FileName">�ļ���</param>
        /// <param name="FileLen">�ļ�����</param>
        /// <param name="fileExtension">�ļ���չ��</param>
        /// <param name="FileMD5Value">�ļ�MD5ֵ</param>
        /// <param name="ServerIP">������IP��ַ</param>
        /// <param name="ServerUDPPort">������UDP����˿�</param>
        /// <param name="ServerTCPPort">������TCP����˿�</param>
        /// <param name="selfUserInfo">�Լ��������û���Ϣ</param>
        /// <param name="OppositeUserInfo">�Է��������û���Ϣ</param>
        public void SetParameter(bool IsSend, string FullFileName, string FileName, int FileLen, string fileExtension, string fileMD5Value,UserInfo selfUserInfo, UserInfo OppositeUserInfo)
        {
            ///�����ļ��������
            this.p2pFileTransmit1.SetParameter(IsSend, FullFileName, FileName, FileLen, fileExtension, fileMD5Value, selfUserInfo, OppositeUserInfo);
            this.PBar1.PositionMax = FileLen;//���������ֵΪ�ļ�����
            this.labProcess.Text = "(0/" + Calculate.GetSizeStr(FileLen) + ")";//�������ֽ���
            this.labFileName.Text = FileName;
            this.FileMD5Value = fileMD5Value;
            if (IsSend)
            {
                this.linkSaveAs.Visible = false;
                this.labOr.Visible = false;
                this.labelState.Text = "�ȴ��Է������ļ�...";
            }
            else
            {
                this.labelState.Text = "�Է��ڵȴ��������ļ�";
            }

        }
        #endregion

        #region  �ļ������¼�
        /// <summary>
        /// �ļ���������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmittedEventHandler(object sender, fileTransmitEvnetArgs e);//�ļ���������¼�
        public event fileTransmittedEventHandler fileTransmitted;

        /// <summary>
        /// ȡ���ļ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitCancelEventHandler(object sender, fileTransmitEvnetArgs e);//ȡ���ļ������¼�
        public event fileTransmitCancelEventHandler fileTransmitCancel;

        /// <summary>
        /// �ļ����俪ʼ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitBeforeEventHandler(object sender, fileTransmitEvnetArgs e);//�����ļ������¼�
        public event fileTransmitBeforeEventHandler fileTransmitBefore;

        /// <summary>
        ///  �ļ����䳬ʱ�¼� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitOutTimeEventHandler(object sender, fileTransmitEvnetArgs e);//�����ļ����ͳ�ʱ
        public event fileTransmitOutTimeEventHandler fileTransmitOutTime;

        /// <summary>
        /// �ļ���������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitErrorEventHandler(object sender, fileTransmitEvnetArgs e);//�ļ����ʹ��� 
        public event fileTransmitErrorEventHandler fileTransmitError;

        /// <summary>
        /// �¼������ͻ��յ��ļ�����
        /// </summary>
        /// <param name="sender">����</param>
        /// <param name="e"></param>
        public delegate void fileTransmittingEventHandler(object sender, fileTransmitEvnetArgs e);//���ͻ��յ��ļ����� 
        public event fileTransmittingEventHandler fileTransmitting;

        public delegate void getFileProxyIDEventHandler(object sender, int  proxyID);//���ͻ��յ��ļ����� 
        public event getFileProxyIDEventHandler getFileProxyID;


        public delegate void GetUDPPortEventHandler(object sender, int Port, bool udpHandshakeInfoClass);//��ñ���UDP�˿��¼� 
        public event GetUDPPortEventHandler fileTransmitGetUDPPort;


        public delegate void ConnectedEventHandler(object sender,  NetCommunicationClass NetCommunicationClass);//���ͻ��յ��ļ����� 
        public event ConnectedEventHandler fileTransmitConnected;


        private void p2pFileTransmit1_fileTransmitBefore(object sender, fileTransmitEvnetArgs e)
        {
            TransmitBeforeDelegate d = new TransmitBeforeDelegate(TransmitBefore);
            this.BeginInvoke(d,sender,e);
        }

        private delegate void TransmitBeforeDelegate(object sender, fileTransmitEvnetArgs e);
        private void TransmitBefore(object sender, fileTransmitEvnetArgs e)
        {
            if (e.isSend)
                this.labelState.Text = "���ڷ����ļ�...";
            else
                this.labelState.Text = "���ڽ����ļ�...";

            if (this.fileTransmitBefore != null)
                this.fileTransmitBefore(this, e);
        }

        private void p2pFileTransmit1_fileTransmitCancel(object sender, fileTransmitEvnetArgs e)
        {
            CancelDelegate d=new CancelDelegate(Cancel);
            this.BeginInvoke(d,sender,e);
        }

        private delegate void CancelDelegate(object sender, fileTransmitEvnetArgs e);
        private void Cancel(object sender, fileTransmitEvnetArgs e)
        {
             if (this.fileTransmitCancel != null)
                this.fileTransmitCancel(this, e);
        }

        private void p2pFileTransmit1_fileTransmitError(object sender, fileTransmitEvnetArgs e)
        {
            if (this.fileTransmitError != null)
                this.fileTransmitError(this, e);
        }

        private void p2pFileTransmit1_fileTransmitOutTime(object sender, fileTransmitEvnetArgs e)
        {
            if (this.fileTransmitOutTime != null)
                this.fileTransmitOutTime(this, e);
        }

        private void p2pFileTransmit1_fileTransmitted(object sender, fileTransmitEvnetArgs e)
        {
            TransmittedDelegate d = new TransmittedDelegate(Transmitted);
            this.BeginInvoke(d,sender,e);
        }

        private delegate void TransmittedDelegate(object sender, fileTransmitEvnetArgs e);
        private void Transmitted(object sender, fileTransmitEvnetArgs e)
        {
            if (this.fileTransmitted != null)
                this.fileTransmitted(this, e);
        }

        private void p2pFileTransmit1_getFileProxyID(object sender, int proxyID)
        {
            if (this.getFileProxyID != null)
                this.getFileProxyID(this, proxyID);
        }

        private void p2pFileTransmit1_fileTransmitting(object sender, fileTransmitEvnetArgs e)
        {
            TransmittingDelegate d = new TransmittingDelegate(Transmitting);
            this.BeginInvoke(d,sender,e );
        }

        private delegate void TransmittingDelegate(object sender, fileTransmitEvnetArgs e);
        private void Transmitting(object sender, fileTransmitEvnetArgs e)
        {  
            this.PBar1.Position = e.currTransmittedLen;//���ý�������ʾ����

            if ( Calculate.DateDiff(this.lastTime, System.DateTime.Now) > 1)//һ���Ӽ���һ�δ����ٶ�
            {
                this.labProcess.Text = "ʣ��" + Calculate.getResidualTime(e.fileLen, e.currTransmittedLen, this.lastTransmitLen);//����ļ�����ʣ��ʱ��
                this.labProcess.Text += "\n(" +  Calculate.GetSizeStr(e.currTransmittedLen) + "/" + this.p2pFileTransmit1.FileLenStr + ")\n";
                this.lastTime = System.DateTime.Now;
                this.lastTransmitLen = e.currTransmittedLen;
            }

            if (this.fileTransmitting != null)
                this.fileTransmitting(this, e);
        }

        private void p2pFileTransmit1_fileTransmitGetUDPPort(object sender, int Port, bool udpHandshakeInfoClass)
        {
            GetUDPPortDelegate d = new GetUDPPortDelegate(GetUDPPort);
            this.BeginInvoke(d, sender, Port, udpHandshakeInfoClass);
        }

        private delegate void GetUDPPortDelegate(object sender, int Port, bool udpHandshakeInfoClass);
        private void GetUDPPort(object sender, int Port, bool udpHandshakeInfoClass)
        {
            if (this.fileTransmitGetUDPPort != null)
                this.fileTransmitGetUDPPort(this, Port, udpHandshakeInfoClass);
        }

        private void p2pFileTransmit1_fileTransmitConnected(object sender, NetCommunicationClass netClass)
        {
            try
            {
                ConnectedDelegate d = new ConnectedDelegate(Connected);
                this.BeginInvoke(d, sender, netClass);
            }
            catch (Exception)
            {

            }
           
        }

        private delegate void ConnectedDelegate(object sender, NetCommunicationClass netClass);
        private void Connected(object sender, NetCommunicationClass netClass)
        {
            if (this.fileTransmitConnected != null)
                this.fileTransmitConnected(sender, netClass);
        }

        #endregion

        #region �����ļ�
        private void linkSaveAs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Windows.Forms.SaveFileDialog fd = new SaveFileDialog();
                fd.Filter = "�����ļ�(*" + this.p2pFileTransmit1.Extension + ")|*" + this.p2pFileTransmit1.Extension;
                fd.FileName = this.labFileName.Text;

                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.linkSaveAs.Visible = false;
                    this.labOr.Visible = false;
                    this.Refresh();

                    if (System.IO.File.Exists(fd.FileName))
                    {
                        System.IO.File.Delete(fd.FileName);
                        System.Threading.Thread.Sleep(2000);
                    }

                    this.lastTime = DateTime.Now;//�������һ�δ����ļ���ʱ��
                    this.p2pFileTransmit1.startIncept(fd.FileName);
                }
            }
            catch { }
        }
        #endregion

        #region ȡ���ļ�����
        /// <summary>
        /// ȡ���ļ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelCancel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.p2pFileTransmit1.CancelTransmit(true);//ȡ���ļ�����
        }

        /// <summary>
        /// ȡ���ļ�����
        /// </summary>
        public void CancelTransmit(bool isMe)
        {
            this.p2pFileTransmit1.CancelTransmit(isMe);
        }
        #endregion
    }
}
