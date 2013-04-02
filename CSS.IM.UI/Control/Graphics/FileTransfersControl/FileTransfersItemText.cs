namespace CSS.IM.UI.Control.Graphics.FileTransfersControl
{
    using System;

    internal class FileTransfersItemText : IFileTransfersItemText
    {
        public string CancelTransfers
        {
            get
            {
                return "取消";
            }
        }

        public string RefuseReceive
        {
            get
            {
                return "拒绝";
            }
        }

        public string Save
        {
            get
            {
                return "接收";
            }
        }

        public string SaveTo
        {
            get
            {
                return "另存为...";
            }
        }


        public string OffLineFiles
        {
            get { return "离线文件"; }
        }
    }
}

