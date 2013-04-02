using System;
using System.Runtime.InteropServices;
using System.Drawing;
using CSS.IM.UI.Control.Graphics.Win32.Struct;

namespace CSS.IM.UI.Control.Graphics.Win32
{


    internal class NativeMethods
    {
        private NativeMethods()
        {
        }

        #region USER32.DLL

        [DllImport("user32", CharSet = CharSet.Ansi,
            SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr BeginPaint(
            IntPtr hWnd, ref PAINTSTRUCT ps);

        [DllImport("user32.dll")]
        public static extern bool EndPaint(
            IntPtr hWnd, ref PAINTSTRUCT ps);

        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(
            IntPtr hWnd, IntPtr rectUpdate, IntPtr hrgnUpdate, int flags);

        [DllImport("user32.dll", SetLastError = true,
            CharSet = CharSet.Unicode, BestFitMapping = false)]
        public static extern IntPtr CreateWindowEx(
            int exstyle, 
            string lpClassName,
            string lpWindowName, 
            int dwStyle, 
            int x, 
            int y, 
            int nWidth,
            int nHeight,
            IntPtr hwndParent, 
            IntPtr Menu, 
            IntPtr hInstance, 
            IntPtr lpParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadIcon(
            IntPtr hInstance, int lpIconName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(
            IntPtr hWnd, 
            IntPtr hWndAfter,
            int x,
            int y, 
            int cx, 
            int cy, 
            uint flags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(
            IntPtr hWnd, ref RECT r);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(
            IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        public extern static int OffsetRect(
            ref RECT lpRect, int x, int y);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(
            IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(
            IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr handle);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr handle, IntPtr hdc);

        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern bool TrackMouseEvent(
            ref TRACKMOUSEEVENT lpEventTrack);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PtInRect(ref Struct.RECT lprc, Point pt);

        [DllImport("user32.dll")]
        public static extern bool EqualRect(
            [In] ref RECT lprc1, [In] ref RECT lprc2);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr SetTimer(
            IntPtr hWnd, 
            int nIDEvent, 
            uint uElapse, 
            IntPtr lpTimerFunc);

        [DllImport("user32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool KillTimer(
            IntPtr hWnd, uint uIDEvent);

        [DllImport("user32.dll")]
        public static extern int SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public extern static int SendMessage(
            IntPtr hWnd, int msg, int wParam, int lParam);
        
        [DllImport("user32.dll")]
        public extern static int SendMessage(
            IntPtr hWnd, int msg, int wParam, ref TOOLINFO lParam);
        
        [DllImport("user32.dll")]
        public extern static int SendMessage(
            IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        
        [DllImport("user32.dll")]
        public extern static int SendMessage(
            IntPtr hWnd, int msg, int wParam, ref RECT lParam);
        
        [DllImport("user32.dll")]
        public extern static int SendMessage(
            IntPtr hWnd,
            int msg, 
            IntPtr wParam, 
            [MarshalAs(UnmanagedType.LPTStr)]string lParam);
        
        [DllImport("user32.dll")]
        public extern static int SendMessage(
            IntPtr hWnd, int msg, IntPtr wParam, ref NMHDR lParam);
        
        [DllImport("user32.dll")]
        public extern static int SendMessage(
            IntPtr hWnd, int msg, IntPtr wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(
            IntPtr hWnd, int msg, int wParam, ref SCROLLBARINFO lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(
            IntPtr hwnd, int msg, int wParam, ref HDITEM lParam);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        public static extern bool ValidateRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern int GetScrollBarInfo(
            IntPtr hWnd, uint idObject, ref SCROLLBARINFO psbi);


        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool AdjustWindowRectEx(
            ref RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

        #endregion

        #region GDI32.DLL

        [DllImport("gdi32.dll", EntryPoint = "GdiAlphaBlend")]
        public static extern bool AlphaBlend(
            IntPtr hdcDest, 
            int nXOriginDest, 
            int nYOriginDest, 
            int nWidthDest, 
            int nHeightDest,
            IntPtr hdcSrc,
            int nXOriginSrc, 
            int nYOriginSrc, 
            int nWidthSrc, 
            int nHeightSrc, 
            BLENDFUNCTION blendFunction);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool StretchBlt(
            IntPtr hDest, 
            int X, 
            int Y, 
            int nWidth, 
            int nHeight, 
            IntPtr hdcSrc,
            int sX, 
            int sY, 
            int nWidthSrc, 
            int nHeightSrc, 
            int dwRop);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(
            IntPtr hdc,
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc,
            int nXSrc,
            int nYSrc,
            int dwRop);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDCA(
            [MarshalAs(UnmanagedType.LPStr)]string lpszDriver,
            [MarshalAs(UnmanagedType.LPStr)]string lpszDevice, 
            [MarshalAs(UnmanagedType.LPStr)]string lpszOutput, 
            int lpInitData);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDCW(
            [MarshalAs(UnmanagedType.LPWStr)]string lpszDriver,
            [MarshalAs(UnmanagedType.LPWStr)]string lpszDevice, 
            [MarshalAs(UnmanagedType.LPWStr)]string lpszOutput, 
            int lpInitData);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(
            string lpszDriver, 
            string lpszDevice, 
            string lpszOutput, 
            int lpInitData);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(
            IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hObject);

        #endregion

        #region comctl32.dll

        [DllImport("comctl32.dll", 
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool InitCommonControlsEx(
            ref INITCOMMONCONTROLSEX iccex);

        #endregion

        #region kernel32.dll

        [DllImport("kernel32.dll")]
        public extern static int RtlMoveMemory(
            ref NMHDR destination, IntPtr source, int length);

        [DllImport("kernel32.dll")]
        public extern static int RtlMoveMemory(
            ref NMTTDISPINFO destination, IntPtr source, int length);
        
        [DllImport("kernel32.dll")]
        public extern static int RtlMoveMemory(
            IntPtr destination, ref NMTTDISPINFO Source, int length);
        
        [DllImport("kernel32.dll")]
        public extern static int RtlMoveMemory(
            ref POINT destination, ref RECT Source, int length);
        
        [DllImport("kernel32.dll")]
        public extern static int RtlMoveMemory(
            ref NMTTCUSTOMDRAW destination, IntPtr Source, int length);
        
        [DllImport("kernel32.dll")]
        public extern static int RtlMoveMemory(
            ref NMCUSTOMDRAW destination, IntPtr Source, int length);

        #endregion



        [DllImport("comctl32.dll", SetLastError = true)]
        public static extern bool ImageList_DrawEx(
            IntPtr himl,
            int i,
            IntPtr hdcDst,
            int x,
            int y,
            int dx,
            int dy,
            uint rgbBk,
            uint rgbFg,
            int fStyle);

        [StructLayout(LayoutKind.Sequential)]
        public struct HDITEM
        {
            public int mask;
            public int cxy;
            public string pszText;
            public IntPtr hbm;
            public int cchTextMax;
            public int fmt;
            public IntPtr lParam;
            public int iImage;
            public int iOrder;
            public uint type;
            public IntPtr pvFilter;
        }

        public enum WindowsMessgae
        {
            WM_NULL = 0x0000,
            WM_CREATE = 0x0001,
            WM_DESTROY = 0x0002,
            WM_MOVE = 0x0003,
            WM_SIZE = 0x0005,
            WM_ACTIVATE = 0x0006,
            WM_SETFOCUS = 0x0007,
            WM_KILLFOCUS = 0x0008,
            WM_ENABLE = 0x000A,
            WM_SETREDRAW = 0x000B,
            WM_SETTEXT = 0x000C,
            WM_GETTEXT = 0x000D,
            WM_GETTEXTLENGTH = 0x000E,
            WM_PAINT = 0x000F,
            WM_CLOSE = 0x0010,

            WM_QUIT = 0x0012,
            WM_ERASEBKGND = 0x0014,
            WM_SYSCOLORCHANGE = 0x0015,
            WM_SHOWWINDOW = 0x0018,

            WM_ACTIVATEAPP = 0x001C,

            WM_SETCURSOR = 0x0020,
            WM_MOUSEACTIVATE = 0x0021,
            WM_GETMINMAXINFO = 0x24,

            WM_SETFONT = 0x30,

            WM_WINDOWPOSCHANGING = 0x0046,
            WM_WINDOWPOSCHANGED = 0x0047,

            WM_CONTEXTMENU = 0x007B,
            WM_STYLECHANGING = 0x007C,
            WM_STYLECHANGED = 0x007D,
            WM_DISPLAYCHANGE = 0x007E,
            WM_GETICON = 0x007F,
            WM_SETICON = 0x0080,

            // non client area
            WM_NCCREATE = 0x0081,
            WM_NCDESTROY = 0x0082,
            WM_NCCALCSIZE = 0x0083,
            WM_NCHITTEST = 0x84,
            WM_NCPAINT = 0x0085,
            WM_NCACTIVATE = 0x0086,

            WM_GETDLGCODE = 0x0087,

            WM_SYNCPAINT = 0x0088,

            // non client mouse
            WM_NCMOUSEMOVE = 0x00A0,
            WM_NCLBUTTONDOWN = 0x00A1,
            WM_NCLBUTTONUP = 0x00A2,
            WM_NCLBUTTONDBLCLK = 0x00A3,
            WM_NCRBUTTONDOWN = 0x00A4,
            WM_NCRBUTTONUP = 0x00A5,
            WM_NCRBUTTONDBLCLK = 0x00A6,
            WM_NCMBUTTONDOWN = 0x00A7,
            WM_NCMBUTTONUP = 0x00A8,
            WM_NCMBUTTONDBLCLK = 0x00A9,

            // keyboard
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,
            WM_CHAR = 0x0102,

            WM_SYSCOMMAND = 0x0112,

            WM_HSCROLL = 0x0114,
            WM_VSCROLL = 0x0115,

            // menu
            WM_INITMENU = 0x0116,
            WM_INITMENUPOPUP = 0x0117,
            WM_MENUSELECT = 0x011F,
            WM_MENUCHAR = 0x0120,
            WM_ENTERIDLE = 0x0121,
            WM_MENURBUTTONUP = 0x0122,
            WM_MENUDRAG = 0x0123,
            WM_MENUGETOBJECT = 0x0124,
            WM_UNINITMENUPOPUP = 0x0125,
            WM_MENUCOMMAND = 0x0126,

            WM_CHANGEUISTATE = 0x0127,
            WM_UPDATEUISTATE = 0x0128,
            WM_QUERYUISTATE = 0x0129,

            // mouse
            WM_MOUSEFIRST = 0x0200,
            WM_MOUSEMOVE = 0x0200,
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_LBUTTONDBLCLK = 0x0203,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_RBUTTONDBLCLK = 0x0206,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WM_MBUTTONDBLCLK = 0x0209,
            WM_MOUSEWHEEL = 0x020A,
            WM_MOUSELAST = 0x020D,

            WM_PARENTNOTIFY = 0x0210,
            WM_ENTERMENULOOP = 0x0211,
            WM_EXITMENULOOP = 0x0212,

            WM_NEXTMENU = 0x0213,
            WM_SIZING = 0x0214,
            WM_CAPTURECHANGED = 0x0215,
            WM_MOVING = 0x0216,

            WM_MDIACTIVATE = 0x0222,

            WM_ENTERSIZEMOVE = 0x0231,
            WM_EXITSIZEMOVE = 0x0232,

            WM_MOUSELEAVE = 0x02A3,
            WM_MOUSEHOVER = 0x02A1,
            WM_NCMOUSEHOVER = 0x02A0,
            WM_NCMOUSELEAVE = 0x02A2,

            WM_PASTE = 0X302,

            WM_PRINT = 0x0317,
            WM_PRINTCLIENT = 0x0318,

            WM_THEMECHANGED = 0x31A,
        }

        [Flags]
        public enum WindowStyle : int
        {
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = unchecked((int)0x80000000),
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_CAPTION = 0x00C00000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_GROUP = 0x00020000,
            WS_TABSTOP = 0x00010000,
            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000,
            WS_TILED = WS_OVERLAPPED,
            WS_ICONIC = WS_MINIMIZE,
            WS_SIZEBOX = WS_THICKFRAME,
            WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,
            WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU |
                                    WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),
            WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU),
            WS_CHILDWINDOW = (WS_CHILD)
        }

        [Flags]
        public enum ImageListDrawFlags : int
        {
            ILD_NORMAL = 0x00000000,
            ILD_TRANSPARENT = 0x00000001,
            ILD_BLEND25 = 0x00000002,
            ILD_FOCUS = 0x00000002,
            ILD_BLEND50 = 0x00000004,
            ILD_SELECTED = 0x00000004,
            ILD_BLEND = 0x00000004,
            ILD_MASK = 0x00000010,
            ILD_IMAGE = 0x00000020,
            ILD_ROP = 0x00000040,
            ILD_OVERLAYMASK = 0x00000F00,
            ILD_PRESERVEALPHA = 0x00001000,
            ILD_SCALE = 0x00002000,
            ILD_DPISCALE = 0x00004000,
            ILD_ASYNC = 0x00008000
        }

        public enum ImageListColorFlags : uint
        {
            CLR_NONE = 0xFFFFFFFF,
            CLR_DEFAULT = 0xFF000000,
            CLR_HILIGHT = CLR_DEFAULT,
        }

        [DllImport("comctl32.dll", SetLastError = true)]
        public static extern IntPtr ImageList_GetIcon(
            IntPtr himl, int i, int flags);        
    }
}
