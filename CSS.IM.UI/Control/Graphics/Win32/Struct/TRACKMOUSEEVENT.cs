using System;
using System.Runtime.InteropServices;
using CSS.IM.UI.Control.Graphics.Win32.Enum;

namespace CSS.IM.UI.Control.Graphics.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct TRACKMOUSEEVENT
    {
        internal uint cbSize;
        internal TRACKMOUSEEVENT_FLAGS dwFlags;
        internal IntPtr hwndTrack;
        internal uint dwHoverTime;
    }
}
