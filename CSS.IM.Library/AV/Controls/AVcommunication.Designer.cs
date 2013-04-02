namespace CSS.IM.Library.AV
{
    partial class AVcommunication
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerConnection = new System.Timers.Timer();
            this.timersUdpPenetrate = new System.Timers.Timer();
            this.TCPClient1 = new CSS.IM.Library.Net.SockTCPClient(this.components);
            this.sockUDP1 = new CSS.IM.Library.Net.SockUDP(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.timerConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timersUdpPenetrate)).BeginInit();
            // 
            // timerConnection
            // 
            this.timerConnection.Elapsed += new System.Timers.ElapsedEventHandler(this.timerConnection_Elapsed);
            // 
            // timersUdpPenetrate
            // 
            this.timersUdpPenetrate.Interval = 300;
            this.timersUdpPenetrate.Elapsed += new System.Timers.ElapsedEventHandler(this.timersUdpPenetrate_Elapsed);
            // 
            // TCPClient1
            // 
            this.TCPClient1.Description = null;
            this.TCPClient1.IsAsync = false;
            this.TCPClient1.OnConnected += new CSS.IM.Library.Net.SockTCPClient.ConnectedEventHandler(this.TCPClient1_OnConnected);
            this.TCPClient1.OnDataArrival += new CSS.IM.Library.Net.SockTCPClient.DataArrivalEventHandler(this.TCPClient1_OnDataArrival);
            // 
            // sockUDP1
            // 
            this.sockUDP1.Description = "";
            this.sockUDP1.IsAsync = false;
            this.sockUDP1.DataArrival += new CSS.IM.Library.Net.SockUDP.DataArrivalEventHandler(this.sockUDP1_DataArrival);
            ((System.ComponentModel.ISupportInitialize)(this.timerConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timersUdpPenetrate)).EndInit();

        }

        #endregion

        private CSS.IM.Library.Net.SockTCPClient  TCPClient1;
        private CSS.IM.Library.Net.SockUDP sockUDP1;
        private System.Timers.Timer timerConnection;
        private System.Timers.Timer timersUdpPenetrate;
    }
}
