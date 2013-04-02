using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using System.IO;
using System.Runtime.Remoting;
using CSS.IM.Library.Net;
using System.Diagnostics;

namespace CSS.IM.App
{
    public partial class RemoteDesktopForm : MsgBasicForm
    {

        int vh = 0;
        int vg = 0;

        public delegate void AVEventHandler(object sender, CSS.IM.Library.AV.AVcommunication.AVEventArgs e);

        /// <summary>
        /// 视频数据到达事件
        /// </summary>
        public event CSS.IM.Library.AV.AVcommunication.AVEventHandler VideoData;


        private delegate void UpImage(byte[] mtemmp);

        protected override void Dispose(bool disposing)
        {

            try
            {
                if (RemoteAssist_Socket.Listened)
                {
                    RemoteAssist_Socket.CloseSock();
                }

                RemoteAssist_Socket.Dispose();
            }
            catch (Exception)
            {
                
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 返回监听的端口
        /// </summary>
        /// <returns></returns>
        public int ListenPort()
        {
            try
            {
                if (!RemoteAssist_Socket.Listened)
                {
                    RemoteAssist_Socket.Listen(0);
                }
                return RemoteAssist_Socket.ListenPort;
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public RemoteDesktopForm()
        {
            InitializeComponent();
        }

        public void UpdatePictureBox(byte[] m_img)
        {
            if (m_img == null)
            {
                pictureBox1.Image = null;
            }
            else
            {
                Image img = Bitmap.FromStream(new System.IO.MemoryStream(m_img));

                if (img.Width != 0 && img.Width != 0)
                {
                    vh = img.Width;
                    this.hScrollBar1.Maximum = vh - panel1.Width + vScrollBar1.Width;
                }
                if (img.Height != 0 && img.Height != 0)
                {
                    vg = img.Height;
                    this.vScrollBar1.Maximum = vg - panel1.Height + hScrollBar1.Height;
                }
                pictureBox1.Image = img;
                img = null;
            }
        }

        private void Timer_DispTop_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    byte[] dby = servFunction.GetDiskTopImage();
            //    byte[] dbys = ZIP.DecompressByte(dby);
            //    //Image bitmap = Bitmap.FromStream(new System.IO.MemoryStream(dbys));

            //    UpImage upimage = new UpImage(UpdatePictureBox);
            //    this.Invoke(upimage, new object[] { dbys });

            //}
            //catch (Exception)
            //{
            //}
        }

        //H
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.pictureBox1.Top = -this.vScrollBar1.Value;

        }

        //G
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.pictureBox1.Left = -this.hScrollBar1.Value;
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void RemoteDesktopForm_Load(object sender, EventArgs e)
        {
            VideoData += new Library.AV.AVcommunication.AVEventHandler(RemoteDesktopForm_VideoData);
            Timer_DispTop.Enabled = true;
        }

        private void RemoteAssist_Socket_DataArrival(object sender, SockEventArgs e)
        {
            CSS.IM.Library.Class.msgAV msg = new CSS.IM.Library.Class.msgAV(e.Data);
            this.DataArrival(msg, CSS.IM.Library.Class.NatClass.FullCone, e.IP, e.Port);
        }

        private void DataArrival(CSS.IM.Library.Class.msgAV msg, CSS.IM.Library.Class.NatClass netClass, System.Net.IPAddress Ip, int Port)
        {
            switch (msg.InfoClass)
            {
                case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.GetVideoData:// 获得对方传输的视频数据包
                    {
                        if (VideoData!=null)
                        {
                            this.Invoke(new AVEventHandler(RemoteDesktopForm_VideoData), new object[] { this, new CSS.IM.Library.AV.AVcommunication.AVEventArgs(msg.DataBlock) });
                        }
                        //this.OnVideoData(msg.DataBlock);
                        //Calculate.WirteLog("收到v"+ msg.DataBlock.Length.ToString());
                        //this.ReceivedFileBlock(msg);//对方发送文件数据过来,保存数据到文件
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.GetAudioData:// 获得对方传输的音频数据包
                    {
                        //this.OnAudioData(msg.DataBlock);
                        //Calculate.WirteLog("收到a"+ msg.DataBlock.Length.ToString());
                        //this.ReceivedFileBlock(msg);//对方发送文件数据过来,保存数据到文件
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolAVTransmit.GetBITMAPINFOHEADER://获得对方视频图像头信息
                    {
                        //this.OnGetBITMAPINFOHEADER(msg.DataBlock);
                        break;
                    }
            }
        }

        void RemoteDesktopForm_VideoData(object sender, Library.AV.AVcommunication.AVEventArgs e)
        {
            try
            {
                byte[] dbys = ZIP.DecompressByte(e.Data);
                Debug.WriteLine(dbys.Length);
                Image bitmap = Bitmap.FromStream(new System.IO.MemoryStream(dbys));
                pictureBox1.Image = bitmap;
            }
            catch (Exception)
            {
                
 
            }
            
        }
    }
}
