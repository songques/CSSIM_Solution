using System;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Net;
using CSS.IM.UI.Form;

namespace CSS.IM.Update
{
    class RusumeDownload
    {

        private static bool isOpen = true;

        //public delegate void CProgressBarTextDeletate(object text);
        //public event CProgressBarTextDeletate CProgressBarText;

        public delegate void CProgressBarMaximumDeletate(int text);
        public event CProgressBarMaximumDeletate CProgressBarMaximum;

        public delegate void CProgressBarValueDeletate(int text);
        public event CProgressBarValueDeletate CProgressBarValue;

        public delegate void CLabelTextDelegate(object text);
        public event CLabelTextDelegate CLabeText;

        private static long filesize = 0;   //目标文件的大小
        private static Stream ns = null;    //流

        /// <summary>
        /// 线程池函数
        /// 获得目标文件大小和流
        /// </summary>
        /// <param name="ar"></param>
        private static void PoolFunc(IAsyncResult ar)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null; 
            try
            {
                request = (HttpWebRequest)ar.AsyncState;
                response = (HttpWebResponse)request.EndGetResponse(ar);
                filesize = response.ContentLength;
                ns = response.GetResponseStream();   //向服务器请求，获得服务器回应数据流 
            }
            catch (Exception ex)
            {
                isOpen = false;
                throw ex;
                
            }
            finally
            {
                response = null;
                request = null;
            }
        }

        public void downLoad(string StrFileName, string StrUrl)
        {
            System.Net.HttpWebRequest request = null;

            //打开上次下载的文件或新建文件 
            long lStartPos = 0;
            System.IO.FileStream fs;
            if (File.Exists(StrFileName))
            {
                fs = File.OpenWrite(StrFileName);
                lStartPos = fs.Length;
                fs.Seek(lStartPos, System.IO.SeekOrigin.Current); //移动文件流中的当前指针 
            }
            else
            {
                fs = new System.IO.FileStream(StrFileName, System.IO.FileMode.Create);
                lStartPos = 0;
            }

            //打开网络连接 
            try
            {
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(StrUrl);
                IAsyncResult ar;
                ar = request.BeginGetResponse(new AsyncCallback(PoolFunc), request);

                if (lStartPos > 0)
                {
                    request.AddRange((int)lStartPos); //设置Range值 
                }
                while (isOpen)
                {
                    if (filesize == 0)
                    {
                        Thread.Sleep(60);
                    }
                    else
                    {

                        CProgressBarMaximum((int)filesize);
                        //_progressBar.Maximum = (int)filesize;
                        break;

                    }
                    Application.DoEvents();
                }
                long totalDownloadedByte = 0;       //当前下载进度（长度）

                byte[] nbytes = new byte[1024];      //512
                int nReadSize = 0;
                nReadSize = ns.Read(nbytes, 0, (int)nbytes.Length);
                while (nReadSize > 0)
                {
                    totalDownloadedByte = nReadSize + totalDownloadedByte;
                    Application.DoEvents();
                    fs.Write(nbytes, 0, nReadSize);


                    //_progressBar.Value = (int)totalDownloadedByte;
                    CProgressBarValue((int)totalDownloadedByte);



                    string str = "已完成: " + ((double)totalDownloadedByte / filesize * 100).ToString("0") + "%";//完成百分比
                    CLabeText(str);

                    nReadSize = ns.Read(nbytes, 0, (int)nbytes.Length);
                }
                fs.Close();
                ns.Close();



                CProgressBarValue((int)totalDownloadedByte);
                //_progressBar.Value = 0;

            }
            catch (ThreadAbortException)
            {
                throw new Exception("ThreadAbort");
            }
            catch (Exception)
            {
                throw new Exception("downLoadError");
            }
            finally
            {
                if(ns != null)
                    ns.Dispose();
                if(fs != null)
                    fs.Dispose();
                if(request != null)
                    request.Abort();
            }
        }

        
    }
}
