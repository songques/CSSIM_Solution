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

        #region ������
        private System.Net.NetworkCredential netCre;// = new NetworkCredential();
        /// <summary>
        /// �ļ�MD5ֵ
        /// </summary>
        public string FileMD5Value;

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
        //public delegate void fileTransmitCancelEventHandler(object sender, fileTransmitEvnetArgs e);//ȡ���ļ������¼�
        //public event fileTransmitCancelEventHandler fileTransmitCancel;

        /// <summary>
        /// �ļ����俪ʼ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public delegate void fileTransmitBeforeEventHandler(object sender, fileTransmitEvnetArgs e);//�����ļ������¼�
        //public event fileTransmitBeforeEventHandler fileTransmitBefore;

        /// <summary>
        ///  �ļ����䳬ʱ�¼� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public delegate void fileTransmitOutTimeEventHandler(object sender, fileTransmitEvnetArgs e);//�����ļ����ͳ�ʱ
        //public event fileTransmitOutTimeEventHandler fileTransmitOutTime;

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

        #endregion

        #region ����Web�ļ�
         ////<summary>
         ////����Web�ļ�
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
        /// ����Web�ļ�
        /// </summary>
        /// <param name="WebURI">Ҫ�����ļ�����ַ</param>
        /// <param name="LocalFile">�ļ�����·��</param>
        /// <param name="userName">�����û���</param>
        /// <param name="password">����</param>
        /// <param name="domain">����</param>
        public void DownloadFile(string webURI, string localFile, string userName, string password, string domain)
        {
            try
            {
                WebClient myWebClient = new WebClient();

                this.netCre = new NetworkCredential(userName, password, domain);
                myWebClient.Credentials = this.netCre;

                long fileLen = getDownloadFileLen(webURI);//��������ļ��Ĵ�С

                Stream readStream = myWebClient.OpenRead(webURI);//��WEB�ļ���׼����

                if (readStream.CanRead)
                {
                    int CurrDownLoadLen = 0;//���ļ�����
                    int maxReadWriteFileBlock = 1024 * 5;//һ�ζ��ļ�5k   
                    byte[] FileBlock;
                    FileBlock = new byte[maxReadWriteFileBlock];
                    while (true)
                    {
                        int l = readStream.Read(FileBlock, 0, FileBlock.Length);//��WEB�ļ�����������
                        if (l == 0) break;
                        ////////////////////////�ļ�����
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
         
        #region web�ļ��ϴ�
        /// <summary>
        /// web�ļ��ϴ�
        /// </summary>
        /// <param name="WebURI">�ļ��ϴ������ַ</param>
        /// <param name="LocalFile">�����ϴ��ļ���·��</param>
        /// <param name="isMD5file">�ļ����Ƿ�Ҫ����MD5�㷨��ȡ</param>
        public void UploadFile(string WebURI, string LocalFile,bool isMD5file)
        {
            if (this.netCre == null) this.netCre = new NetworkCredential("", "", "");
            this.UploadFile(WebURI, LocalFile, this.netCre.UserName, this.netCre.Password, this.netCre.Password, isMD5file);
        }
         
        /// <summary>
        ///  web�ļ��ϴ�
        /// </summary>
        /// <param name="WebURI">�ļ��ϴ������ַ</param>
        /// <param name="LocalFile">�����ϴ��ļ���·��</param>
        /// <param name="userName">�����û���</param>
        /// <param name="password">����</param>
        /// <param name="domain">����</param>
        /// <param name="isMD5file">�ļ����Ƿ�Ҫ����MD5�㷨��ȡ</param>
        public void UploadFile(string webURI, string localFile, string userName, string password, string domain, bool isMD5file)
        {
            try
            {
                // Local Directory File Info
                if (!System.IO.File.Exists(localFile))//����ļ�������
                {
                    if (this.fileTransmitError != null)
                        this.fileTransmitError(this, new fileTransmitEvnetArgs(true, localFile, localFile, "Ҫ�ϴ����ļ������ڣ���ȷ���ļ�·���Ƿ���ȷ��", 0, 0, this.FileMD5Value));
                    return;
                }

                System.IO.FileInfo fInfo = new FileInfo(localFile);//��ȡ�ļ�����Ϣ

                if (isMD5file)//�����Ҫ���ļ�MD5,��ִ��MD5
                    webURI += "\\" + IMLibrary.Class.Hasher.GetMD5Hash(localFile) + fInfo.Extension;//��ȡ�ļ���MD5ֵ
                else
                    webURI += "\\" + fInfo.Name;// +fInfo.Extension;//��ȡ�ļ���MD5ֵ

                // Create a new WebClient instance.
                WebClient myWebClient = new WebClient();
                this.netCre = new NetworkCredential(userName, password, domain);
                myWebClient.Credentials = this.netCre;

                if (getDownloadFileLen(webURI) == fInfo.Length)//������������Ѿ��д��ļ��������˳�
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
                        int readFileCount = 0;//���ļ�����
                        int maxReadWriteFileBlock = 1024 * 5;//һ�ζ��ļ�5k   
                        long offset = 0;

                        readFileCount = (int)fInfo.Length / maxReadWriteFileBlock;//����ļ���д����

                        if ((int)fInfo.Length % maxReadWriteFileBlock != 0)
                            readFileCount++;//�����д�ļ����࣬�����д������1

                        for (int i = 0; i < readFileCount; i++)
                        {   //�����Ƕ�һ���ļ����ڴ����
                            if (i + 1 == readFileCount)//��������һ�ζ�д�ļ����������ļ�β����ȫ�����뵽�ڴ�
                                FileBlock = new byte[(int)fInfo.Length - i * maxReadWriteFileBlock];
                            else
                                FileBlock = new byte[maxReadWriteFileBlock];

                            ////////////////////////�ļ�����
                            FileStream fw = new FileStream(localFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                            offset = i * maxReadWriteFileBlock;
                            fw.Seek(offset, SeekOrigin.Begin);//�ϴη��͵�λ��
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
         
        #region ��ȡҪ�����ļ��Ĵ�С
        public long getDownloadFileLen(string WebURI)
        {
            if (this.netCre == null) this.netCre = new NetworkCredential("", "", "");
            return getDownloadFileLen(WebURI, this.netCre.UserName, this.netCre.Password, this.netCre.Password);
        }

        /// <summary>
        /// ��ȡҪ�����ļ��Ĵ�С
        /// </summary>
        /// <param name="WebURI">��Ҫ�����ļ�����ַ</param>
        /// <param name="userName">�����û���</param>
        /// <param name="password">����</param>
        /// <param name="domain">����</param>
        /// <returns></returns>
        public long getDownloadFileLen(string WebURI, string userName, string password, string domain)
        {
            long len = 0;//��¼Ҫ�����ļ��Ĵ�С 
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
