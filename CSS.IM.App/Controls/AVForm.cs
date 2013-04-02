using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.Library.AV.Controls;
using CSS.IM.Library.AV;
using CSS.IM.XMPP;
using CSS.IM.Library.Class;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using CSS.IM.App.Settings;
using System.IO;
using HuaSDKSolution.DirectShow;

namespace CSS.IM.App.Controls
{
    public partial class AVForm : BasicForm
    {

        private Thread sendthread = null;//发送数据线程

        public System.Media.SoundPlayer callSoundPlayer = null;

        public bool isBtn_hangup = false;//是否由挂断按键判断窗体的

        /// <summary>
        /// 标识是否已经设置过AV组件
        /// </summary>
        private bool IsIni = false;

        #region 事件

        /// <summary>
        /// 联接产生事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="netCommuctionClass"></param>
        public delegate void AVConnectedEventHandler(object sender, CSS.IM.Library.Class.NetCommunicationClass netCommuctionClass);//联接产生事件 
        /// <summary>
        /// 联接产生事件
        /// </summary>
        public event AVConnectedEventHandler AVConnected;


        /// <summary>
        /// 关闭视频的时候
        /// </summary>
        public delegate void AVCloseDelegate();

        public event AVCloseDelegate AVCloseEvent;


        /// <summary>
        /// 接受视频会话
        /// </summary>
        public delegate void AgreeDelegate();

        public event AgreeDelegate AgreeEvent;

        #endregion



        private HuaSDKSolution.DirectShow.Capture WebCam = null;

        private bool sendding = false;
        Bitmap image = null;
        IntPtr ip = IntPtr.Zero;
        ImageCodecInfo codeinfo = null;
        EncoderParameters coderpam = null;
        EncoderParameter ep;

        /// <summary>
        /// 音频捕捉组件
        /// </summary>
        AudioCapturer AC = null;
        /// <summary>
        /// 音频回显组件
        /// </summary>
        CSS.IM.Library.AV.Controls.AudioRender AR = null;

        /// <summary>
        /// 音频编解码器
        /// </summary>
        AudioEncoder AE = null;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {

            sendding = false;

            if (sendthread!=null)
            {
                try
                {
                    sendthread.Abort();
                }
                catch (Exception)
                {

                }

                sendthread = null;
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        public AVForm(Jid to_Jid)
        {

            
            try
            {
                callSoundPlayer = new System.Media.SoundPlayer(CSS.IM.UI.Util.Path.CallPath);//视频的呼叫声音;
            }
            catch (Exception)
            {
                
            }
            
            InitializeComponent();
            this.Text = "正在与" + to_Jid.User + "视频通话中";
            aVcommunicationEx1.UDPListen();
            if (CSS.IM.UI.Util.Path.CallSwitch)
            {
                try
                {
                      callSoundPlayer.PlayLooping();
                }
                catch (Exception)
                {
                    
                }
            }
              
        }


        /// <summary>
        /// 初始化音视频通信组件
        /// </summary>
        /// <param name="Model">视频显示大小模式</param>
        public void iniAV(VideoSizeModel Model)
        {
            if (!IsIni)
                IsIni = true;//标识已经初始化
            else
                return; //如果已经初始化，则退出

            if (WebCam == null)
            {
                DsDevice[] capDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
                if (capDevices.Length>0)
                {
                    codeinfo = GetEncoderInfo("image/jpeg");
                    ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)64);
                    coderpam = new EncoderParameters(1);
                    coderpam.Param[0] = ep;

                    try
                    {
                        WebCam = new HuaSDKSolution.DirectShow.Capture(0, 0, 320, 240);
                        WebCam.Start(); 
                    }
                    catch (Exception ex)
                    {
                        WebCam.Dispose();
                        WebCam = null;
                        MsgBox.Show("CSS&IM", ex.Message + " 当前系统摄像头正在使用！");
                    }
                    
                }
            }

            if (AE == null)
            {
                AE = new AudioEncoder();//创建G711音频编解码器
            }

            if (AC == null)
            {
                AC = new AudioCapturer(this.trackBar1, this.trackBar2);//创建新的音频捕捉组件
                AC.AudioDataCapturered += new AudioCapturer.AudioDataCaptureredEventHandler(AC_AudioDataCapturered);
            }

            if (AR == null)
            {
                try
                {
                    AR = new CSS.IM.Library.AV.Controls.AudioRender();//创建G711音频回放组件
                }
                catch (Exception)
                {
                    AR = null;
                }

            }

            sendding = true;
            sendthread = new Thread(this.Sending);
            sendthread.Start();
        }

        /// <summary>
        /// 捕捉到音频数据事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AC_AudioDataCapturered(object sender, AudioCapturedEventArgs e)
        {
            if (AE != null)
            {
                this.aVcommunicationEx1.SendAudio(AE.Encode(e.Data));//将音频数据编码后发送给对方
            }
        }


        /// <summary>
        /// 设置远程视频访问的端口和地址
        /// </summary>
        /// <param name="user"></param>
        /// <param name="VideoRemotUDPPort"></param>
        public void SetRemoteAddress(UserInfo user, int VideoRemotUDPPort)
        {

            aVcommunicationEx1.OppositeUDPPort = VideoRemotUDPPort;
            aVcommunicationEx1._OppositeUserInfo = user;
        }

        /// <summary>
        /// 保存地址的地址
        /// </summary>
        /// <param name="user"></param>
        public void SetLocalAddress(UserInfo user)
        {
            aVcommunicationEx1._selfUserInfo = user;
        }

        #region 通信事件
        private void aVcommunicationEx1_AVConnected(object sender, CSS.IM.Library.Class.NetCommunicationClass netCommuctionClass)
        {
            if (this.AVConnected != null)
                this.AVConnected(sender, netCommuctionClass);
        }
        
        /// <summary>
        /// 收到对方音频数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aVcommunicationEx1_AudioData(object sender, AVcommunication.AVEventArgs e)
        {
            if (this.AE != null && this.AR != null)
            {
                this.AR.play(this.AE.Decode(e.Data));//将收到的音频数据解码后播放
            }
        }

        /// <summary>
        /// 收到对方的视频数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void aVcommunicationEx1_VideoData(object sender, AVcommunication.AVEventArgs e)
        {
            if (e.Data != null)
            {
                try
                {

                    this.BeginInvoke(new VOIDD(renewimg), new object[] { e.Data });
                }
                catch (Exception)
                {

                }
                finally
                {

                }

                //using (MemoryStream ms = new MemoryStream(e.Data, 0, (int)e.Data.Length, true))
                //{
                //    try
                //    {

                //        //Bitmap newimg = new Bitmap(ms);
                //        this.BeginInvoke(new VOIDD(renewimg), new object[] { e.Data });
                //    }
                //    catch (Exception)
                //    {

                //    }
                //    finally
                //    {
                //        ms.Dispose();
                //    }
                //}

            }
        }
        #endregion

        #region 关闭 
        /// <summary>
        /// 关闭 
        /// </summary>
        public void AVClose()
        {
            try
            {
                callSoundPlayer.Stop();

            }
            catch (Exception)
            {

            }
            finally
            {
                sendding = false;
            }
            

            try
            {
                aVcommunicationEx1.closeCommunication();
            }
            catch (Exception)
            {
                
            }

            try
            {
                aVcommunicationEx1.sockUDP1.CloseSock();
            }
            catch (Exception)
            {

            }
            
            if (WebCam!=null)
            {
                WebCam.Dispose();
            }
            if (AC!=null)
            {
                AC = null;
            }
            if (AR!=null)
            {
                AR = null;
            }
            if (AE!=null)
            {
                AE = null;
            }

            try
            {
                this.Close();
            }
            catch (Exception)
            {
                
            }

            try
            {
                this.Dispose();
            }
            catch (Exception)
            {

            }
            
        }
        #endregion
       
        private void btn_hangup_Click(object sender, EventArgs e)
        {
            callSoundPlayer.Stop();
            try
            {
                isBtn_hangup = true;

                if (AVCloseEvent != null)
                    AVCloseEvent();

                AVClose();
            }
            catch (Exception)
            {

            }
        }

        delegate void VOIDD(byte[] bytearray);
        private void renewimg(byte[] bytearray)
        {
            if (bytearray != null)
            {
                using (MemoryStream imageStream = new MemoryStream(bytearray))
                {
                    try
                    {
                        this.pic_remote.Image.Dispose();
                        this.pic_remote.Image = new Bitmap(imageStream);
                    }
                    catch (Exception)
                    {

                    }
                    
                }
            }
        }

        private delegate void VOIDD1(Image localimage);

        private void renewimg1(Image images)
        {
            if (images != null)
            {
                try
                {
                    this.pic_local.Image.Dispose();
                    this.pic_local.Image = images;
                }
                catch (Exception)
                {

                }
               
            }
        }

        private void Sending(Object obj)
        {
            
            while (sendding)
            {
                if (WebCam == null)
                    break;
                using (MemoryStream ms =new MemoryStream(2000))
                {
                    try
                    {

                        ip = WebCam.GetBitMap();
                        image = new Bitmap(WebCam.Width, WebCam.Height, WebCam.Stride, PixelFormat.Format24bppRgb, ip);
                        image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        ms.Position = 0;
                        image.Save(ms, codeinfo, coderpam);
                        this.BeginInvoke(new VOIDD1(renewimg1), new object[] { image });
                        aVcommunicationEx1.SendVideo(ms.ToArray());
                        ms.SetLength(0);
                        ms.Close();
                        Marshal.FreeCoTaskMem(ip);
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (ms != null)
                            ms.Dispose();
                    }
                }
            }
        }

        private ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void btn_agree_Click(object sender, EventArgs e)
        {
            callSoundPlayer.Stop();

            btn_agree.Visible = false;
            btn_refuse.Visible = false;
            btn_hangup.Visible = true;

            if (AgreeEvent != null)
                AgreeEvent();
        }

        private void btn_refuse_Click(object sender, EventArgs e)
        {
            btn_hangup_Click(null, null);
        }

        private void AVForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isBtn_hangup)
            {
                DialogResult reslut = MsgBox.Show(this, "CSS&IM", "当前正在视频通话中是否挂断？", MessageBoxButtons.YesNo);
                if (reslut == DialogResult.Yes)
                {
                    btn_hangup_Click(null, null);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
