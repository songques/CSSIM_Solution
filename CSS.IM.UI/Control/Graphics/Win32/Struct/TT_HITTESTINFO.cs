using System;
using System.Runtime.InteropServices;

namespace CSS.IM.UI.Control.Graphics.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct TT_HITTESTINFO
    {
        internal IntPtr hwnd;
        internal POINT pt;
        internal TOOLINFO ti;
    }
}
