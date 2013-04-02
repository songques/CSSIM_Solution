using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.IO;

namespace IMLibrary.Controls
{
    public partial class WebFile : Component
    {
        public WebFile()
        {
            InitializeComponent();
        }

        public WebFile(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        #region 变量区
        private System.Net.NetworkCredential netCre;// = new NetworkCredential();
        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string FileMD5Value;

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
        //public delegate void fileTransmitCancelEventHandler(object sender, fileTransmitEvnetArgs e);//取消文件传输事件
        //public event fileTransmitCancelEventHandler fileTransmitCancel;

        /// <summary>
        /// 文件传输开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public delegate void fileTransmitBeforeEventHandler(object sender, fileTransmitEvnetArgs e);//接收文件传输事件
        //public event fileTransmitBeforeEventHandler fileTransmitBefore;

        /// <summary>
        ///  文件传输超时事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public delegate void fileTransmitOutTimeEventHandler(object sender, fileTransmitEvnetArgs e);//接收文件发送超时
        //public event fileTransmitOutTimeEventHandler fileTransmitOutTime;

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

        #endregion

        #region 下载Web文件
         ////<summary>
         ////下载Web文件
         ////</summary>
         ////<param name="WebURI"></param>
         ////<param name="LocalFile"></param>
        public void DownloadFile(string WebURI, string LocalFile)
        {
            if (this.netCre == null) this.netCre = new NetworkCredential("", "", "");
            DownloadFile(WebURI, LocalFile, this.netCre.UserName, this.netCre.Password, this.netCre.Password);
        }

        //public void DownloadFile(string WebURI, string LocalFile)
        //{
        //    WebClient myWebClient = new WebClient();
        //    myWebClient.DownloadFile(WebURI,LocalFile);
        //}

        /// <summary>
        /// 下载Web文件
        /// </summary>
        /// <param name="WebURI">要下载文件的网址</param>
        /// <param name="LocalFile">文件保存路径</param>
        /// <param name="userName">网络用户名</param>
        /// <param name="password">密码</param>
        /// <param name="domain">域名</param>
        public void DownloadFile(string webURI, string localFile, string userName, string password, string domain)
        {
            try
            {
                WebClient myWebClient = new WebClient();

                this.netCre = new NetworkCredential(userName, password, domain);
                myWebClient.Credentials = this.netCre;

                long fileLen = getDownloadFileLen(webURI);//获得下载文件的大小

                Stream readStream = myWebClient.OpenRead(webURI);//打开WEB文件流准备读

                if (readStream.CanRead)
                {
                    int CurrDownLoadLen = 0;//读文件次数
                    int maxReadWriteFileBlock = 1024 * 5;//一次读文件5k   
                    byte[] FileBlock;
                    FileBlock = new byte[maxReadWriteFileBlock];
                    while (true)
                    {
                        int l = readStream.Read(FileBlock, 0, FileBlock.Length);//读WEB文件流到缓冲区
                        if (l == 0) break;
                        ////////////////////////文件操作
                        FileStream fw = new FileStream(localFile, FileMode.Append, FileAccess.Write, FileShare.Read);
                        fw.Write(FileBlock, 0, l);
                        fw.Close();
                        fw.Dispose();
                        /////////////////////////
                        CurrDownLoadLen += l;

                        if (this.fileTransmitting != null)
                            this.fileTransmitting(this, new fileTransmitEvnetArgs(false, localFile, localFile, "", Convert.ToInt32(fileLen), CurrDownLoadLen, this.FileMD5Value));
                    }
                }
                readStream.Close();
                myWebClient.Dispose();

                if (this.fileTransmitted != null)
                    this.fileTransmitted(this, new fileTransmitEvnetArgs(false, localFile, localFile, "", Convert.ToInt32(fileLen), Convert.ToInt32(fileLen), this.FileMD5Value));

            }
            catch (Exception ex)
            {
                if (this.fileTransmitError != null)
                    this.fileTransmitError(this, new fileTransmitEvnetArgs(false, webURI, webURI, ex.Message + ex.Source, 0, 0, this.FileMD5Value));
            }
        }
 
        #endregion
         
        #region web文件上传
        /// <summary>
        /// web文件上传
        /// </summary>
        /// <param name="WebURI">文件上传后的网址</param>
        /// <param name="LocalFile">本地上传文件的路径</param>
        /// <param name="isMD5file">文件名是否要根据MD5算法获取</param>
        public void UploadFile(string WebURI, string LocalFile,bool isMD5file)
        {
            if (this.netCre == null) this.netCre = new NetworkCredential("", "", "");
            this.UploadFile(WebURI, LocalFile, this.netCre.UserName, this.netCre.Password, this.netCre.Password, isMD5file);
        }
         
        /// <summary>
        ///  web文件上传
        /// </summary>
        /// <param name="WebURI">文件上传后的网址</param>
        /// <param name="LocalFile">本地上传文件的路径</param>
        /// <param name="userName">网络用户名</param>
        /// <param name="password">密码</param>
        /// <param name="domain">域名</param>
        /// <param name="isMD5file">文件名是否要根据MD5算法获取</param>
        public void UploadFile(string webURI, string localFile, string userName, string password, string domain, bool isMD5file)
        {
            try
            {
                // Local Directory File Info
                if (!System.IO.File.Exists(localFile))//如果文件不存在
                {
                    if (this.fileTransmitError != null)
                        this.fileTransmitError(this, new fileTransmitEvnetArgs(true, localFile, localFile, "要上传的文件不存在，请确定文件路径是否正确。", 0, 0, this.FileMD5Value));
                    return;
                }

                System.IO.FileInfo fInfo = new FileInfo(localFile);//获取文件的信息

                if (isMD5file)//如果需要将文件MD5,则执行MD5
                    webURI += "\\" + IMLibrary.Class.Hasher.GetMD5Hash(localFile) + fInfo.Extension;//获取文件的MD5值
                else
                    webURI += "\\" + fInfo.Name;// +fInfo.Extension;//获取文件的MD5值

                // Create a new WebClient instance.
                WebClient myWebClient = new WebClient();
                this.netCre = new NetworkCredential(userName, password, domain);
                myWebClient.Credentials = this.netCre;

                if (getDownloadFileLen(webURI) == fInfo.Length)//如果服务器上已经有此文件存在则退出
                {
                    if (this.fileTransmitted != null)
                        this.fileTransmitted(this, new fileTransmitEvnetArgs(true, localFile, fInfo.Name, "", Convert.ToInt32(fInfo.Length), Convert.ToInt32(fInfo.Length), this.FileMD5Value));
                    return;
                }
                else
                {
                    Stream postStream = myWebClient.OpenWrite(webURI, "PUT");
                    if (postStream.CanWrite)
                    {
                        byte[] FileBlock;
                        int readFileCount = 0;//读文件次数
                        int maxReadWriteFileBlock = 1024 * 5;//一次读文件5k   
                        long offset = 0;

                        readFileCount = (int)fInfo.Length / maxReadWriteFileBlock;//获得文件读写次数

                        if ((int)fInfo.Length % maxReadWriteFileBlock != 0)
                            readFileCount++;//如果读写文件有余，则读次写次数增1

                        for (int i = 0; i < readFileCount; i++)
                        {   //下面是读一次文件到内存过程
                            if (i + 1 == readFileCount)//如果是最后一次读写文件，则将所有文件尾数据全部读入到内存
                                FileBlock = new byte[(int)fInfo.Length - i * maxReadWriteFileBlock];
                            else
                                FileBlock = new byte[maxReadWriteFileBlock];

                            ////////////////////////文件操作
                            FileStream fw = new FileStream(localFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                            offset = i * maxReadWriteFileBlock;
                            fw.Seek(offset, SeekOrigin.Begin);//上次发送的位置
                            fw.Read(FileBlock, 0, FileBlock.Length);
                            fw.Close();
                            fw.Dispose();
                            ///////////////////////////

                            postStream.Write(FileBlock, 0, FileBlock.Length);

                            if (this.fileTransmitting != null)
                                this.fileTransmitting(this, new fileTransmitEvnetArgs(true, localFile, fInfo.Name, "", Convert.ToInt32(fInfo.Length), (int)offset + FileBlock.Length, this.FileMD5Value));
                        }
                    }
                    postStream.Close();
                    myWebClient.Dispose();
                }
                if (this.fileTransmitted != null)
                    this.fileTransmitted(this, new fileTransmitEvnetArgs(true, localFile, fInfo.Name, "", Convert.ToInt32(fInfo.Length), Convert.ToInt32(fInfo.Length), this.FileMD5Value));
            }
            catch (Exception ex)
            {
                if (this.fileTransmitError != null)
                    this.fileTransmitError(this, new fileTransmitEvnetArgs(true, localFile, localFile, ex.Message + ex.Source, 0, 0, this.FileMD5Value));
            }
        }
        #endregion
         
        #region 获取要下载文件的大小
        public long getDownloadFileLen(string WebURI)
        {
            if (this.netCre == null) this.netCre = new NetworkCredential("", "", "");
            return getDownloadFileLen(WebURI, this.netCre.UserName, this.netCre.Password, this.netCre.Password);
        }

        /// <summary>
        /// 获取要下载文件的大小
        /// </summary>
        /// <param name="WebURI">所要下载文件的网址</param>
        /// <param name="userName">网络用户名</param>
        /// <param name="password">密码</param>
        /// <param name="domain">域名</param>
        /// <returns></returns>
        public long getDownloadFileLen(string WebURI, string userName, string password, string domain)
        {
            long len = 0;//记录要下载文件的大小 
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(WebURI);
                req.Credentials = new NetworkCredential(userName, password, domain);
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                len = res.ContentLength;
                res.Close();
            }
            catch// (Exception ex)
            {
                
                //if (this.fileTransmitError != null)
                //    this.fileTransmitError(this, new fileTransmitEvnetArgs(false, WebURI, WebURI, ex.Message + ex.Source, 0, 0));
            }
            return len;
        }
        #endregion
    }
}
