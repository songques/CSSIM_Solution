using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CSS.IM.App.Settings
{
    public class GetCert
    {
        [DllImport("GetCert.dll", EntryPoint = "GetCertNum", SetLastError = true)]
        public static extern void GetCertNum(out int iCertNum);

        [DllImport("GetCert.dll", EntryPoint = "GetCertName", CharSet = CharSet.Ansi)]
        public static extern void GetCertName(int index, int iBufLen, StringBuilder pbCertName);
    }
}
