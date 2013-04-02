namespace IMLibrary.Controls 
{
    partial class p2pFileTransmit
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
            try
            {
                this.sockUDP1.CloseSock();//关闭sockUDP1端口，清楚占用的资源 
            }
            catch { }

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
            this.timersUdpPenetrate = new System.Timers.Timer();
            this.timerGetFileOut = new System.Timers.Timer();
            this.timerConnection = new System.Timers.Timer();
            this.sockUDP1 = new IMLibrary.Net.SockUDP(this.components);
            this.asyncTCPClient1 = new IMLibrary.Net.SockTCPClient (this.components);
            ((System.ComponentModel.ISupportInitialize)(this.timersUdpPenetrate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerGetFileOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerConnection)).BeginInit();
            // 
            // timersUdpPenetrate
            // 
            this.timersUdpPenetrate.Interval = 300;
            this.timersUdpPenetrate.Elapsed += new System.Timers.ElapsedEventHandler(this.timersUdpPenetrate_Elapsed);
            // 
            // timerGetFileOut
            // 
            this.timerGetFileOut.Interval = 300;
            this.timerGetFileOut.Elapsed += new System.Timers.ElapsedEventHandler(this.timerGetFileOut_Elapsed);
            // 
            // timerConnection
            // 
            this.timerConnection.Elapsed += new System.Timers.ElapsedEventHandler(this.timerConnection_Elapsed);
            // 
            // sockUDP1
            // 
            this.sockUDP1.Description = "";
            this.sockUDP1.DataArrival += new IMLibrary.Net.SockUDP.DataArrivalEventHandler(this.sockUDP1_DataArrival);
            // 
            // asyncTCPClient1
            // 
            this.asyncTCPClient1.OnError += new IMLibrary.Net.SockTCPClient.ErrorEventHandler(this.asyncTCPClient1_OnError);
            this.asyncTCPClient1.OnDisconnected += new IMLibrary.Net.SockTCPClient.DisconnectedEventHandler(this.asyncTCPClient1_OnDisconnected);
            this.asyncTCPClient1.OnConnected += new IMLibrary.Net.SockTCPClient.ConnectedEventHandler(this.asyncTCPClient1_OnConnected);
            this.asyncTCPClient1.OnDataArrival += new IMLibrary.Net.SockTCPClient.DataArrivalEventHandler(this.asyncTCPClient1_OnDataArrival);
            ((System.ComponentModel.ISupportInitialize)(this.timersUdpPenetrate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerGetFileOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerConnection)).EndInit();

        }

        #endregion

        private IMLibrary.Net.SockUDP  sockUDP1;
        private System.Timers.Timer timersUdpPenetrate;
        private System.Timers.Timer timerGetFileOut;
        private IMLibrary.Net.SockTCPClient asyncTCPClient1;
        private System.Timers.Timer timerConnection;
    }
}
