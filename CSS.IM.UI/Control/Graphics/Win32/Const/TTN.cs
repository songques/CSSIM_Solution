﻿using System;
using System.Runtime.InteropServices;

namespace CSS.IM.UI.Control.Graphics.Win32.Const
{
    internal static class TTN
    {
        // ownerdraw
        public const int TTN_FIRST = (-520);

        public const int TTN_GETDISPINFOA = (TTN_FIRST - 0);
        public const int TTN_GETDISPINFOW = (TTN_FIRST - 10);
        public const int TTN_SHOW = (TTN_FIRST - 1);
        public const int TTN_POP = (TTN_FIRST - 2);
        public const int TTN_LINKCLICK = (TTN_FIRST - 3);

        public const int TTN_NEEDTEXTA = TTN_GETDISPINFOA;
        public const int TTN_NEEDTEXTW = TTN_GETDISPINFOW;

        public const int TTN_LAST = (-549);

        public static int TTN_GETDISPINFO;
        public static int TTN_NEEDTEXT;

        static TTN()
        {

        }

        private static void UseUnicode()
        {
            bool unicode = Marshal.SystemDefaultCharSize != 1;
            if (unicode)
            {
                TTN_GETDISPINFO = TTN_GETDISPINFOW;
                TTN_NEEDTEXT = TTN_NEEDTEXTW;
            }
            else
            {
                TTN_GETDISPINFO = TTN_GETDISPINFOA;
                TTN_NEEDTEXT = TTN_NEEDTEXTA;
            }
        }
    }
}
