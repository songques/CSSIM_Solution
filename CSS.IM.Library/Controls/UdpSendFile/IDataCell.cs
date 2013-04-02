using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library.Controls.UdpSendFile
{

    public interface IDataCell
    {
        byte[] ToBuffer();

        void FromBuffer(byte[] buffer);
    }
}
