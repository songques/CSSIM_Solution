using System;
using System.Runtime.InteropServices;

namespace CSS.IM.UI.Control.Graphics.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct STYLESTRUCT
    {
        internal int styleOld;
        internal int styleNew;
    }
}
