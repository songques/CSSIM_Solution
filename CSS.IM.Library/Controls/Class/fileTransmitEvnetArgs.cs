using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library.Controls 
{
    #region 文件传输事件参数
    public class fileTransmitEvnetArgs : System.EventArgs
    {
        /// <summary>
        /// 是否文件发送者
        /// </summary>
        public bool isSend;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errorMessage;

        /// <summary>
        /// 传输的文件名的读写路径
        /// </summary>
        public string fullFileName;

        /// <summary>
        /// 传输的文件名
        /// </summary>
        public string fileName;

        /// <summary>
        /// 传输文件的大小
        /// </summary>
        public int fileLen;

        /// <summary>
        /// 当前传输完成的文件长度
        /// </summary>
        public int currTransmittedLen;

        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string FileMD5Value;

        public fileTransmitEvnetArgs(bool IsSend, string FullFileName, string FileName, string ErrorMessage, int FileLen, int CurrTransmittedLen, string fileMD5Value)
        {
            this.isSend = IsSend;
            this.fullFileName = FullFileName;
            this.fileName = FileName;
            this.errorMessage = ErrorMessage;
            this.fileLen = FileLen;
            this.currTransmittedLen = CurrTransmittedLen;
            this.FileMD5Value = fileMD5Value;
        }
    }
    #endregion
}
