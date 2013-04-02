using System;
using System.Runtime.InteropServices;

namespace CSS.IM.UI.Control.Graphics.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct INITCOMMONCONTROLSEX
    {
        internal INITCOMMONCONTROLSEX(int flags)
        {
            this.dwSize = Marshal.SizeOf(typeof(INITCOMMONCONTROLSEX));
            this.dwICC = flags;
        }

        internal int dwSize;
        internal int dwICC;
    }
}
