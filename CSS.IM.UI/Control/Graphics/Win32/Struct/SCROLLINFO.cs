using System;
using System.Runtime.InteropServices;

namespace CSS.IM.UI.Control.Graphics.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SCROLLINFO
    {
        internal uint cbSize;
        internal uint fMask;
        internal int nMin;
        internal int nMax;
        internal uint nPage;
        internal int nPos;
        internal int nTrackPos;
    }
}
