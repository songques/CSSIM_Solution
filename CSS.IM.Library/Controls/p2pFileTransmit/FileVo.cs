using System;
using System.Collections.Generic;
using System.Text;
using CSS.IM.Library.Class;

namespace CSS.IM.Library.Controls
{
    public class FileVo
    {
        public bool IsSend{get;set;}
        public string fullFileName { get; set; }
        public string FileName { get; set; }
        public int FileLen { get; set; }
        public string fileExtension { get; set; }
        public string FileMD5Value { get; set; }
        public Msg our_msg { get; set; }//用于发送文件发送的消息
    }
}
