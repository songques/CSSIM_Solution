using System;
using System.Collections.Generic;
using System.Text;

namespace WForm1
{
    /// <summary>
    /// 毛奇项目组所用的消息体
    /// </summary>
    public class MqMessage
    {
        public String IsOpen { set; get; }
        public String Token { set; get; }
        public String Herf { set; get; }
        public String Msg { set; get; }
        public String Url { set; get; }
    }
}
