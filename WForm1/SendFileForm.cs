using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.Library.Controls.UdpSendFile;
using CSS.IM.UI.Control.Graphics.FileTransfersControl;
using System.Net;

namespace WForm1
{
    public partial class SendFileForm : Form
    {
        /// <summary>
        /// 发送文件
        /// </summary>
        private UdpSendFile udpSendFile;



        //接收文件
        private UdpReceiveFile udpReceiveFile;
        private Color _baseColor = Color.DarkGoldenrod;
        private Color _borderColor = Color.FromArgb(64, 64, 0);
        private Color _progressBarBarColor = Color.Gold;
        private Color _progressBarBorderColor = Color.Olive;
        private Color _progressBarTextColor = Color.Olive;
        

        public SendFileForm()
        {
            InitializeComponent();
        }

        #region Help Methods

        private void AppendLog(string text, bool async)
        {
            if (async)
            {
                Invoke(new MethodInvoker(delegate()
                {
                    int index = listBox1.Items.Add(text);
                    listBox1.SelectedIndex = index;
                }));
            }
            else
            {
                int index = listBox1.Items.Add(text);
                listBox1.SelectedIndex = index;
            }
        }

        #endregion

        private void SendFileForm_Load(object sender, EventArgs e)
        {
            
        }

        #region 发送文件
        private void FileSendBuffer(object sender, FileSendBufferEventArgs e)
        {
            FileTransfersItem item =e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    item.TotalTransfersSize += e.Size;
                }));
            }
        }

        private void FileSendAccept(object sender, FileSendEventArgs e)
        {
            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                //BeginInvoke(new MethodInvoker(delegate()
                //{
                item.Start();
                //}));
            }

            AppendLog(string.Format("对方同意接收文件 {0}。",e.SendFileManager.Name), true);
        }

        private void FileSendRefuse(object sender, FileSendEventArgs e)
        {
            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    fileTansfersContainer1.RemoveItem(item);
                    item.Dispose();
                }));
            }

            AppendLog(string.Format("对方拒绝接收文件 {0} 。",e.SendFileManager.Name), true);
        }

        private void FileSendCancel(object sender, FileSendEventArgs e)
        {
            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    fileTansfersContainer1.RemoveItem(item);
                    item.Dispose();
                }));
            }

            AppendLog(string.Format("对方取消接收文件 {0} 。",e.SendFileManager.Name), true);
        }

        private void FileSendComplete(object sender, FileSendEventArgs e)
        {
            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    fileTansfersContainer1.RemoveItem(item);
                    item.Dispose();
                }));
            }

            AppendLog(string.Format("文件 {0} 发送完成。",e.SendFileManager.Name), true);
        }
        #endregion


        #region 接收文件
        private void FileReceiveCancel(object sender, FileReceiveEventArgs e)
        {
            string md5 = string.Empty;
            if (e.ReceiveFileManager != null)
            {
                md5 = e.ReceiveFileManager.MD5;
            }
            else
            {
                md5 = e.Tag.ToString();
            }

            FileTransfersItem item = fileTansfersContainer2.Search(md5);

            BeginInvoke(new MethodInvoker(delegate()
            {
                fileTansfersContainer2.RemoveItem(item);
            }));

            AppendLog(string.Format("对方取消发送文件文件 {0} 。",item.FileName), true);
        }

        private void FileReceiveComplete(object sender, FileReceiveEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate()
            {
                fileTansfersContainer2.RemoveItem(e.ReceiveFileManager.MD5);
            }));

            AppendLog(string.Format("文件 {0} 接收完成，MD5 校验: {1}。",e.ReceiveFileManager.Name, e.ReceiveFileManager.Success), true);
        }

        private void FileReceiveBuffer(object sender, FileReceiveBufferEventArgs e)
        {
            FileTransfersItem item = fileTansfersContainer2.Search(
                e.ReceiveFileManager.MD5);
            if (item != null)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    item.TotalTransfersSize += e.Size;
                }));
            }
        }

        private void RequestSendFile(object sender, RequestSendFileEventArgs e)
        {
            TraFransfersFileStart traFransfersFileStart = e.TraFransfersFileStart;
            BeginInvoke(new MethodInvoker(delegate()
            {
                FileTransfersItem item = fileTansfersContainer2.AddItem(
                    traFransfersFileStart.MD5,
                    "接收文件",
                    traFransfersFileStart.FileName,
                    traFransfersFileStart.Image,
                    traFransfersFileStart.Length,
                    FileTransfersItemStyle.ReadyReceive);

                item.BaseColor = _baseColor;
                item.BorderColor = _borderColor;
                item.ProgressBarBarColor = _progressBarBarColor;
                item.ProgressBarBorderColor = _progressBarBorderColor;
                item.ProgressBarTextColor = _progressBarTextColor;

                item.Tag = e;
                item.SaveButtonClick += new EventHandler(ItemSaveButtonClick);
                item.SaveToButtonClick += new EventHandler(ItemSaveToButtonClick);
                item.RefuseButtonClick += new EventHandler(ItemRefuseButtonClick);
            }));

            AppendLog(string.Format("请求发送文件 {0}。",traFransfersFileStart.FileName), true);
        }




        private void ItemRefuseButtonClick(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            RequestSendFileEventArgs rse = item.Tag as RequestSendFileEventArgs;
            rse.Cancel = true;
            fileTansfersContainer2.RemoveItem(item);
            item.Dispose();
            AppendLog(string.Format(
                "拒绝接收文件 {0}。",
                rse.TraFransfersFileStart.FileName), false);
            udpReceiveFile.AcceptReceive(rse);
        }

        private void ItemSaveToButtonClick(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            RequestSendFileEventArgs rse = item.Tag as RequestSendFileEventArgs;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                rse.Path = fbd.SelectedPath;
                AppendLog(string.Format(
                    "同意接收文件 {0}。",
                    rse.TraFransfersFileStart.FileName), false);
                ControlTag tag = new ControlTag(
                    rse.TraFransfersFileStart.MD5,
                    rse.TraFransfersFileStart.FileName,
                    rse.RemoteIP);
                item.Tag = tag;
                item.Style = FileTransfersItemStyle.Receive;
                item.CancelButtonClick += new EventHandler(ItemCancelButtonClick2);
                item.Start();

                udpReceiveFile.AcceptReceive(rse);
            }
        }

        private void ItemSaveButtonClick(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            RequestSendFileEventArgs rse = item.Tag as RequestSendFileEventArgs;

            rse.Path = Application.StartupPath;
            AppendLog(string.Format(
                   "同意接收文件 {0}。",
                   rse.TraFransfersFileStart.FileName), false);
            ControlTag tag = new ControlTag(
                rse.TraFransfersFileStart.MD5,
                rse.TraFransfersFileStart.FileName,
                rse.RemoteIP);
            item.Tag = tag;
            item.Style = FileTransfersItemStyle.Receive;
            item.CancelButtonClick += new EventHandler(ItemCancelButtonClick2);
            item.Start();

            udpReceiveFile.AcceptReceive(rse);
        }

        private void ItemCancelButtonClick2(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            ControlTag tag = item.Tag as ControlTag;
            udpReceiveFile.CancelReceive(tag.MD5, tag.RemoteIP);
            fileTansfersContainer2.RemoveItem(item);
            item.Dispose();
            AppendLog(string.Format(
               "取消接收文件 {0}。",
               tag.FileName), false);
        }

        #endregion

        private void btn_open_Click(object sender, EventArgs e)
        {
            udpSendFile = new UdpSendFile(int.Parse(txt_sendLocalPort.Text));
            //sendFile.Log += new TraFransfersFileLogEventHandler(SendFileLog);
            udpSendFile.FileSendBuffer += new FileSendBufferEventHandler(FileSendBuffer);
            udpSendFile.FileSendAccept += new FileSendEventHandler(FileSendAccept);
            udpSendFile.FileSendRefuse += new FileSendEventHandler(FileSendRefuse);
            udpSendFile.FileSendCancel += new FileSendEventHandler(FileSendCancel);
            udpSendFile.FileSendComplete += new FileSendEventHandler(FileSendComplete);
            udpSendFile.Start();
            AppendLog(string.Format("开始侦听，端口：{0}", udpSendFile.Port), false);

            



            udpReceiveFile = new UdpReceiveFile(int.Parse(txt_receiveLocalPort.Text));
            udpReceiveFile.RequestSendFile +=new RequestSendFileEventHandler(RequestSendFile);
            udpReceiveFile.FileReceiveBuffer +=new FileReceiveBufferEventHandler(FileReceiveBuffer);
            udpReceiveFile.FileReceiveComplete +=new FileReceiveEventHandler(FileReceiveComplete);
            udpReceiveFile.FileReceiveCancel +=new FileReceiveEventHandler(FileReceiveCancel);
            udpReceiveFile.Start();
            AppendLog(string.Format("开始侦听，端口：{0}", udpReceiveFile.Port), false);



        }

        private void btn_selectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SendFileManager sendFileManager = new SendFileManager(
                        ofd.FileName);
                    if (udpSendFile.CanSend(sendFileManager))
                    {
                        FileTransfersItem item = fileTansfersContainer1.AddItem(
                            sendFileManager.MD5,
                            "发送文件",
                            sendFileManager.Name,
                            Icon.ExtractAssociatedIcon(ofd.FileName).ToBitmap(),
                            sendFileManager.Length,
                            FileTransfersItemStyle.Send);
                        item.CancelButtonClick += new EventHandler(ItemCancelButtonClick1);
                        item.Tag = sendFileManager;
                        sendFileManager.Tag = item;
                        udpSendFile.RemoteEP =new IPEndPoint(IPAddress.Parse(txt_tbRemoteIP.Text), int.Parse(txt_tbRemotePort.Text));
                        udpSendFile.SendFile(sendFileManager, item.Image);
                    }
                    else
                    {
                        MessageBox.Show("文件正在发送，不能发送重复的文件。");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ItemCancelButtonClick1(object sender, EventArgs e)
        {
            FileTransfersItem item =
                sender as FileTransfersItem;
            SendFileManager sendFileManager =
                       item.Tag as SendFileManager;
            udpSendFile.CancelSend(sendFileManager.MD5);

            fileTansfersContainer1.RemoveItem(item);
            AppendLog(string.Format("取消发送文件 {0} 。",sendFileManager.Name), true);
        }
    }

}
