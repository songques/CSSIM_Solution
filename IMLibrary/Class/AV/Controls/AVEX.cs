using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace IMLibrary.AV.Controls
{
    public partial class AVEX : Component
    {
        /// <summary>
        /// 构造
        /// </summary>
        public AVEX()
        {
            //InitializeComponent();
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="container"></param>
        public AVEX(IContainer container)
        {
            container.Add(this);

            //InitializeComponent();
        }

        #region 事件

        /// <summary>
        /// 联接产生事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="netCommuctionClass"></param>
        public delegate void AVConnectedEventHandler(object sender, IMLibrary.Class.NetCommunicationClass netCommuctionClass);//联接产生事件 
        /// <summary>
        /// 联接产生事件
        /// </summary>
        public event AVConnectedEventHandler AVConnected;

        #endregion

        #region 变量
        /// <summary>
        /// 标识是否已经设置过AV组件
        /// </summary>
        private bool IsIni = false;

        /// <summary>
        /// 初始化本地视频控件
        /// </summary>
        Control cLocal=null;
        /// <summary>
        /// 初始化远程视频控件
        /// </summary>
        Control cRemote = new Control();
        /// <summary>
        /// 初始化麦克风控制控件
        /// </summary>
        TrackBar trackBarIn = new TrackBar();
        /// <summary>
        /// 初始化声卡声音控制控件
        /// </summary>
        TrackBar trackBarOut = new TrackBar();

        /// <summary>
        /// 视频捕捉组件
        /// </summary>
        VideoCapturer VC = null;
        /// <summary>
        /// 视频回显组件
        /// </summary>
        VideoRender VR = null;
        /// <summary>
        /// 视频编码器
        /// </summary>
        VideoEncoder VE  = null;
        /// <summary>
        /// 视频解码器
        /// </summary>
        VideoEncoder VD = null;
        /// <summary>
        /// 音频捕捉组件
        /// </summary>
        AudioCapturer AC = null;
        /// <summary>
        /// 音频回显组件
        /// </summary>
        AudioRender AR = null;
        /// <summary>
        /// 音频编解码器
        /// </summary>
        AudioEncoder AE = null;
        #endregion 


        #region 设置AV基本控制单元
        /// <summary>
        /// 设置AV基本控制单元
        /// </summary>
        /// <param name="local">本地视频控件</param>
        /// <param name="remote">远程视频控件</param>
        /// <param name="trackBarOut">声音音量调制器</param>
        /// <param name="trackBarIn">话筒音量调制器</param>
        public void SetControls(Control local, Control remote, TrackBar trackBarOut, TrackBar trackBarIn)
        {
            this.cLocal = local;//初始化本地视频控件
            this.cRemote = remote;//初始化远程视频控件
            this.trackBarIn = trackBarIn;//初始化麦克风控制控件
            this.trackBarOut = trackBarOut;//初始化声卡声音控制控件
        }
        /// <summary>
        /// 设置AV基本控制单元
        /// </summary>
        /// <param name="local">本地视频控件</param>
        /// <param name="remote">远程视频控件</param>
        public void SetControls(Control local, Control remote)
        {
            this.cLocal = local;//初始化本地视频控件
            this.cRemote = remote;//初始化远程视频控件
        }
        #endregion

        #region 初始化音视频捕获资源
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

            if (VC == null)
            {
                VC = new VideoCapturer(this.cLocal);//创建新的视频捕捉组件
                VC.VideoCapturerBefore += new VideoCapturer.VideoCapturerBeforeEventHandler(VC_VideoCapturerBefore);
                VC.VideoDataCapturered += new VideoCapturer.VideoDataCaptureredEventHandler(VC_VideoDataCapturered);
                VC.StartVideoCapture(Model);//开始捕捉视频
            }

            if (AE == null)
            {
                AE = new AudioEncoder();//创建G711音频编解码器
            }

            if (AC == null)
            {
                AC = new AudioCapturer(this.trackBarOut, this.trackBarIn);//创建新的音频捕捉组件
                AC.AudioDataCapturered += new AudioCapturer.AudioDataCaptureredEventHandler(AC_AudioDataCapturered);
            }

            if (AR == null)
            {
                AR = new AudioRender();//创建G711音频回放组件
            }
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
                this.AR.play(e.Data);
                //Console.WriteLine("声音："+e.Data.Length.ToString());
                //this.AVcommunication1.SendAudio(AE.Encode(e.Data));//将音频数据编码后发送给对方
            }
        }

        /// <summary>
        /// 捕捉到视频数据事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VC_VideoDataCapturered(object sender, VideoCapturedEventArgs e)
        {
           if (VE != null)
            {
               //this.AVcommunication1.SendVideo(VE.Encode(e.Data));//将视频数据编码后发送给对方
            }
        }

        /// <summary>
        /// 视频捕捉前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VC_VideoCapturerBefore(object sender, VideoCapturedEventArgs e)
        {
            if (VE == null)
                VE = new VideoEncoder(e.BITMAPINFO.bmiHeader, true);//根据摄像头采集数据的格式，创建新的视频编码器

            //防止丢包，发送三次本地视频图像头信息给对方，以便对方解码器正确解码
            //AVcommunication1.SendBITMAPINFOHEADER(e.BITMAPINFO.bmiHeader);//发送本地视频图像头信息给对方，以便对方解码器正确解码
            System.Threading.Thread.Sleep(300);
            //AVcommunication1.SendBITMAPINFOHEADER(e.BITMAPINFO.bmiHeader);//发送本地视频图像头信息给对方，以便对方解码器正确解码
            System.Threading.Thread.Sleep(300);
            //AVcommunication1.SendBITMAPINFOHEADER(e.BITMAPINFO.bmiHeader);//发送本地视频图像头信息给对方，以便对方解码器正确解码
        }

        #endregion

        #region 通信事件
        private void aVcommunication1_AVConnected(object sender, IMLibrary.Class.NetCommunicationClass netCommuctionClass)
        {
            if (this.AVConnected != null)
                this.AVConnected(sender, netCommuctionClass);
        }
        
        /// <summary>
        /// 收到对方音频数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aVcommunication1_AudioData(object sender, AVcommunication.AVEventArgs e)
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
        private void aVcommunication1_VideoData(object sender, AVcommunication.AVEventArgs e)
        {
            if (this.VD != null && this.VR != null)
            {
                this.VR.DrawVideo(this.VD.Decode(e.Data));//将收到的视频数据解码后回显
            }
        }

        /// <summary>
        /// 收到对方视频格式事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aVcommunication1_GetBITMAPINFOHEADER(object sender, AVcommunication.AVEventArgs e)
        {
            if (VD == null)
                VD = new VideoEncoder(e.BITMAPINFOHEADER, false);//创建视频解码器

            if (VR == null)
                VR = new VideoRender(this.cRemote);//创建视频回显组件

            VR.BITMAPINFOHEADER = e.BITMAPINFOHEADER;
        }
        #endregion

        #region 关闭 
        /// <summary>
        /// 关闭 
        /// </summary>
        public void Close()
        {
            if (this.VC != null)
            {
                if (VC != null)
                {
                    VC.Close();
                    VC = null;
                }
            }
        }
        #endregion
    }
}