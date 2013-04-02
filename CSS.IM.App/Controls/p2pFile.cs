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

        #region 自定义变量区
 
        /// <summary>
        /// 上次传输文件数据的长度
        /// </summary>
        private int lastTransmitLen = 0;

        /// <summary>
        /// 上一次收到或发送数据的时间
        /// </summary>
        private DateTime lastTime = System.DateTime.Now;

        public string FileMD5Value;//文件MD5值
        
        #endregion

        #region 设置传输文件参数
         
        /// <summary>
        /// 设置传输文件参数
        /// </summary>
        /// <param name="isSend">标识文件是发送还是接收</param>
        /// <param name="FullFileName">如果发送文件，则设置要发放的文件路径</param>
        /// <param name="FileName">文件名</param>
        /// <param name="FileLen">文件长度</param>
        /// <param name="fileExtension">文件扩展名</param>
        /// <param name="FileMD5Value">文件MD5值</param>
        /// <param name="ServerIP">服务器IP地址</param>
        /// <param name="ServerUDPPort">服务器UDP服务端口</param>
        /// <param name="ServerTCPPort">服务器TCP服务端口</param>
        /// <param name="selfUserInfo">自己的在线用户信息</param>
        /// <param name="OppositeUserInfo">对方的在线用户信息</param>
        public void SetParameter(bool IsSend, string FullFileName, string FileName, int FileLen, string fileExtension, string fileMD5Value,UserInfo selfUserInfo, UserInfo OppositeUserInfo)
        {
            ///设置文件传输参数
            this.p2pFileTransmit1.SetParameter(IsSend, FullFileName, FileName, FileLen, fileExtension, fileMD5Value, selfUserInfo, OppositeUserInfo);
            this.PBar1.PositionMax = FileLen;//进度条最大值为文件长度
            this.labProcess.Text = "(0/" + Calculate.GetSizeStr(FileLen) + ")";//设置文字进度
            this.labFileName.Text = FileName;
            this.FileMD5Value = fileMD5Value;
            if (IsSend)
            {
                this.linkSaveAs.Visible = false;
                this.labOr.Visible = false;
                this.labelState.Text = "等待对方接收文件...";
            }
            else
            {
                this.labelState.Text = "对方在等待您接收文件";
            }

        }
        #endregion

        #region  文件传输事件
        /// <summary>
        /// 文件传输结束事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmittedEventHandler(object sender, fileTransmitEvnetArgs e);//文件传输结束事件
        public event fileTransmittedEventHandler fileTransmitted;

        /// <summary>
        /// 取消文件传输事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitCancelEventHandler(object sender, fileTransmitEvnetArgs e);//取消文件传输事件
        public event fileTransmitCancelEventHandler fileTransmitCancel;

        /// <summary>
        /// 文件传输开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitBeforeEventHandler(object sender, fileTransmitEvnetArgs e);//接收文件传输事件
        public event fileTransmitBeforeEventHandler fileTransmitBefore;

        /// <summary>
        ///  文件传输超时事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitOutTimeEventHandler(object sender, fileTransmitEvnetArgs e);//接收文件发送超时
        public event fileTransmitOutTimeEventHandler fileTransmitOutTime;

        /// <summary>
        /// 文件传输错误事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitErrorEventHandler(object sender, fileTransmitEvnetArgs e);//文件发送错误 
        public event fileTransmitErrorEventHandler fileTransmitError;

        /// <summary>
        /// 事件：发送或收到文件数据
        /// </summary>
        /// <param name="sender">对像</param>
        /// <param name="e"></param>
        public delegate void fileTransmittingEventHandler(object sender, fileTransmitEvnetArgs e);//发送或收到文件数据 
        public event fileTransmittingEventHandler fileTransmitting;

        public delegate void getFileProxyIDEventHandler(object sender, int  proxyID);//发送或收到文件数据 
        public event getFileProxyIDEventHandler getFileProxyID;


        public delegate void GetUDPPortEventHandler(object sender, int Port, bool udpHandshakeInfoClass);//获得本地UDP端口事件 
        public event GetUDPPortEventHandler fileTransmitGetUDPPort;


        public delegate void ConnectedEventHandler(object sender,  NetCommunicationClass NetCommunicationClass);//发送或收到文件数据 
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
                this.labelState.Text = "正在发送文件...";
            else
                this.labelState.Text = "正在接收文件...";

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
            this.PBar1.Position = e.currTransmittedLen;//设置进度条显示进度

            if ( Calculate.DateDiff(this.lastTime, System.DateTime.Now) > 1)//一秒钟计算一次传输速度
            {
                this.labProcess.Text = "剩余" + Calculate.getResidualTime(e.fileLen, e.currTransmittedLen, this.lastTransmitLen);//获得文件传输剩余时间
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

        #region 接收文件
        private void linkSaveAs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Windows.Forms.SaveFileDialog fd = new SaveFileDialog();
                fd.Filter = "所有文件(*" + this.p2pFileTransmit1.Extension + ")|*" + this.p2pFileTransmit1.Extension;
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

                    this.lastTime = DateTime.Now;//设置最后一次传输文件的时间
                    this.p2pFileTransmit1.startIncept(fd.FileName);
                }
            }
            catch { }
        }
        #endregion

        #region 取消文件传输
        /// <summary>
        /// 取消文件传输
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelCancel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.p2pFileTransmit1.CancelTransmit(true);//取消文件传输
        }

        /// <summary>
        /// 取消文件传输
        /// </summary>
        public void CancelTransmit(bool isMe)
        {
            this.p2pFileTransmit1.CancelTransmit(isMe);
        }
        #endregion
    }
}
