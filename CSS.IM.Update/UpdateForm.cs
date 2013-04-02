using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using CSS.IM.UI.Form;

namespace CSS.IM.Update
{
    public partial class UpdateForm : BasicForm
    {
        public delegate void UpdateProgressDelegate(object text);
        UpdateProgressDelegate _UpdateProgressDelegate;

        public delegate void CProgressBarValue(int text);
        CProgressBarValue _CProgressBarValue;

        public delegate void CProgressBarMaximum(int text);
        CProgressBarMaximum _CProgressBarMaximum;

        public delegate void CLabeText(object text);
        CLabeText _CLabeText;

        public delegate void ItemExitDelegate();
        public event ItemExitDelegate _ItemExitDelegateEvent;

        public delegate void MsgBoxShowDelegate(string args,bool exit);
        public event MsgBoxShowDelegate _MsgBoxShowDelegateEvent;


        Thread _Thread;
        string StrUrl = null;
        RusumeDownload _rusumeDownload;

        string FileName = "temp.exe";

        public UpdateForm(string updateUrl)
        {
            InitializeComponent();
            StrUrl = updateUrl;
            
        }

        public void StartDownLoad()
        {
            try
            {
                //lblProgress.Text = "正在下载更新文件,请稍后...";
                this.Invoke(_UpdateProgressDelegate, new Object[] { "正在下载更新文件,请稍后..." });
                if (StrUrl == "GetDownloadUrlError")       //获得url错误，推出程序
                {

                    _ItemExitDelegateEvent();  
                    return;
                }
                System.Diagnostics.Trace.WriteLine(StrUrl);
                _rusumeDownload.downLoad(FileName, StrUrl);
                this.Invoke(_UpdateProgressDelegate, new Object[] { "" });
            }
            catch (System.ObjectDisposedException)
            {

            }
            catch (Exception ex)
            {
                if (ex.Message == "ThreadAbort")
                {

                    _ItemExitDelegateEvent();  

                    return;
                }
                else
                {
                    _MsgBoxShowDelegateEvent("下载过程中出现错误，请检查网络并重新下载。",true);
                    //_ItemExitDelegateEvent();  
                    return;
                }
            }
            finally
            {
                
                _rusumeDownload = null;
            }

            //下载完毕，自动执行
            Process.Start(FileName, null);

            _ItemExitDelegateEvent();  
            ExitProgram();
        }

        void _rusumeDownload_CProgressBarValue(int text)
        {
            this.Invoke(_CProgressBarValue, new Object[] {text });
        }

        void _rusumeDownload_CProgressBarMaximum(int text)
        {
            this.Invoke(_CProgressBarMaximum, new Object[] {text });
        }

        void _rusumeDownload_CLabeText(object text)
        {
            this.Invoke(_CLabeText, new object[] { text});
        }

        public void UpdateProgress(object text)
        {
            lblProgress.Text = text.ToString();
        }

        public void ProgressBarValue(int text)
        {
            progressBar1.Value = text;
        }

        public void ProgressBarMaximum(int text)
        {
            progressBar1.Maximum = text;
        }

        public void LabeText(object text)
        {
            lblProgress.Text = text.ToString();
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            _rusumeDownload = new RusumeDownload();
            _rusumeDownload.CLabeText += new RusumeDownload.CLabelTextDelegate(_rusumeDownload_CLabeText);
            _rusumeDownload.CProgressBarMaximum += new RusumeDownload.CProgressBarMaximumDeletate(_rusumeDownload_CProgressBarMaximum);
            _rusumeDownload.CProgressBarValue += new RusumeDownload.CProgressBarValueDeletate(_rusumeDownload_CProgressBarValue);
            _UpdateProgressDelegate = new UpdateProgressDelegate(UpdateProgress);
            _CProgressBarValue = new CProgressBarValue(ProgressBarValue);
            _CProgressBarMaximum = new CProgressBarMaximum(ProgressBarMaximum);
            _CLabeText = new CLabeText(LabeText);
            _ItemExitDelegateEvent += new ItemExitDelegate(UpdateForm__ItemExitDelegateEvent);
            _MsgBoxShowDelegateEvent += new MsgBoxShowDelegate(UpdateForm__MsgBoxShowDelegateEvent);

            try
            {
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }
            }
            catch (Exception ex)
            {
                _MsgBoxShowDelegateEvent("删除老文件错误，" + ex.Message,true);
                ExitProgram();
            }
            _Thread = new Thread(new ThreadStart(StartDownLoad));
            _Thread.Start();            
            
        }

        void UpdateForm__MsgBoxShowDelegateEvent(string args,bool exit)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MsgBoxShowDelegate(UpdateForm__MsgBoxShowDelegateEvent),new object[]{args,true});
            }
            MsgBox.Show(this, "CSS&IM", args, MessageBoxButtons.OK, MessageBoxIcon.Question);

            if (exit)
            {
                Application.DoEvents();
                Application.Exit();
            }

        }

        void UpdateForm__ItemExitDelegateEvent()
        {
            try
            {
                Application.Exit();
            }
            catch (Exception)
            {
            }
        }


        private void ExitProgram()
        {
           
            try
            {
                if (_Thread != null)
                {
                    _Thread.Abort();
                    _Thread = null;
                }
            }
            catch
            {
            }
            _ItemExitDelegateEvent();  
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

            DialogResult result = MsgBox.Show(this, "CSS&IM", "下载正在进行中，是否退出？", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                ExitProgram();
            }
        }
    }
}