using System;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CSS.IM.Library.AV
{

    /// <summary>
    /// Compress 的摘要说明。
    /// </summary>
    public class ICBase
    {
        protected int hic;
        protected ICMODE mode;
        protected BITMAPINFO _in;
        protected BITMAPINFO _out;
        protected COMPVARS pp;
        protected int fourcc;
        protected COMPVARS Compvars
        {
            get { return this.pp; }
        }
        public ICBase(COMPVARS cp, BITMAPINFO biIn, ICMODE mode, int fourcc)
        {
            this.pp = cp;
            this._in = biIn;
            this.mode = mode;
            this.fourcc = fourcc;
            _out = new BITMAPINFO();
        }
        #region
        public int HIC
        {
            get { return this.hic; }
        }
        public BITMAPINFO InFormat
        {
            get { return this._in; }
        }
        public BITMAPINFO OutFormat
        {
            get { return this._out; }
        }
        public ICMODE Mode
        {
            get { return this.mode; }
        }
        public int Fourcc
        {
            get { return this.fourcc; }
        }
        #endregion
        public virtual void Open()//(System.IntPtr p)
        {
            this.hic = ICOpen(FOURCC.ICTYPE_VIDEO, this.fourcc, this.mode);
            this.Compvars.hic = this.hic;
            /*		pp=new COMPVARS();
                    pp.cbSize=Marshal.SizeOf(pp);
                    pp.dwFlags=1;
                    pp.fccHandler=this.fourcc;
                    pp.fccType=FOURCC.ICTYPE_VIDEO;
                    pp.hic=hic;
                    pp.lDataRate=120;
                    pp.lKey=15;
                    pp.lQ=-1;

                    try
                    {
                        // 		bool b=ICM.ICCompressorChoose(p,0,0,0,pp,"SSSSSS");
                    }
                    catch(System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    ICINFO info=new ICINFO();
                    try
                    {
                        info.dwSize=Marshal.SizeOf(info);
                    }
                    catch(System.Exception eee)
                    {
                        MessageBox.Show(eee.Message);
                    }
		
                    try
                    {
                        //	int rrrrrrrrr=ICM.ICGetInfo(hic,ref info,Marshal.SizeOf(info));
                    }
                    catch(System.Exception exx)
                    {
                        MessageBox.Show(exx.Message);
                    }*/
            //	int sssssf=ICM.ICSendMessage(hic,ICM.ICM_COMPRESS_BEGIN,ref this._in,ref this._out);
            //	int rrr=ICM.ICSendMessage(hic,ICM.ICM_COMPRESS_GET_FORMAT,ref this._in,ref this._out);
            //	bool isstart=ICSeqCompressFrameStart(pp,ref this._in);

            //	int rrr=	ICM.ICSendMessage(hic,0x4000+0x1000+2,info,Marshal.SizeOf(info));
            //	this.hic=ICM.ICLocate(FOURCC.ICTYPE_VIDEO,FOURCC.mmioFOURCC('M','P','4','2'),ref _in.bmiHeader,ref _out.bmiHeader,1);
            //	ICM.ICSendMessage(this.hic,ICM.DRV_USER+0x1000+10,(int)p,0);
            //	int result=ICSendMessage(hic,ICM_COMPRESS_BEGIN,ref _in,ref _out);
            //	bool sssst=	ICM.ICSeqCompressFrameStart(pp,ref this._in);
        }
        public virtual byte[] Process(byte[] data)
        {
            /*	bool key=false;long size=0;
                try
                {
                    IntPtr r=(IntPtr)ICSeqCompressFrame(this.pp,0,data,ref key,ref size);
                    System.Diagnostics.Trace.WriteLine(string.Format("isKeyFrame {0},size :{1}",key.ToString(),size.ToString()));
                    byte[] b=new byte[size];
                    Marshal.Copy(r,b,0,(int)size);
                    return b;
                    //	int r=ICCompress(hic,0,ref this._out.bmiHeader,pdata,ref this._in.bmiHeader,data,0,0,0,0,0,0,0);
                }
                catch(System.Exception ex)
                {
                    gowk.utility.Diagnostics.Debug.Write(ex);
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                }*/
            return null;
        }

        public virtual void Close()
        {
            ICClose(hic);
        }

        #region

        [DllImport("MSVFW32.dll")]
        public static extern void ICSeqCompressFrameEnd(ref COMPVARS pc);
        /*	[DllImport("MSVFW32.dll")]
    public static extern IntPtr ICSeqCompressFrame(
                 COMPVARS pc,  
                 int uiFlags,  
                 IntPtr lpBits, 
                 ref bool pfKey,  
                 ref int plSize  
                 );*/
        [DllImport("MSVFW32.dll")]
        public static extern int ICCompressGetFormatSize(
            int hic,
            ref BITMAPINFO lpbiInput
            );

        [DllImport("MSVFW32.dll")]
        public static extern bool ICSeqCompressFrameStart(
            COMPVARS pc,
            ref BITMAPINFO lpbiIn
            );
        [DllImport("MSVFW32.dll")]
        public static extern int ICSeqCompressFrame(
            COMPVARS pc,
            int uiFlags,
            byte[] lpBits,
            ref bool pfKey,
            ref long plSize
            );
        [DllImport("MSVFW32.dll", CharSet = CharSet.Ansi)]
        public static extern int ICGetInfo(
            int hic,
            ICINFO lpicinfo,
            int cb
            );
        [DllImport("MSVFW32.dll")]
        public static extern bool ICCompressorChoose(
            IntPtr hwnd,
            int uiFlags,
            //ref BITMAPINFO pvIn,   
            int pvIn,
            int lpData,
            COMPVARS pc,
            string lpszTitle
            );
        [DllImport("MSVFW32.dll")]
        public static extern int ICLocate(
            int fccType,
            int fccHandler,
            ref BITMAPINFOHEADER lpbiIn,
            ref BITMAPINFOHEADER lpbiOut,
            short wFlags
            );
        [DllImport("MSVFW32.dll"), PreserveSig]
        public static extern int ICOpen(int fccType, int fccHandler, ICMODE wMode);
        [DllImport("MSVFW32.dll")]
        public static extern int ICClose(int hic);
        [DllImport("MSVFW32.dll")]
        public static extern int ICCompress(
            int hic,
            int dwFlags,        // flags
            ref BITMAPINFOHEADER lpbiOutput,     // output format
            IntPtr lpData,         // output data
            ref BITMAPINFOHEADER lpbiInput,      // format of frame to compress
            IntPtr lpBits,         // frame data to compress
            int lpckid,         // ckid for data in AVI file
            int lpdwFlags,      // flags in the AVI index.
            int lFrameNum,      // frame number of seq.
            int dwFrameSize,    // reqested size in bytes. (if non zero)
            int dwQuality,      // quality within one frame
            //	BITMAPINFOHEADER  lpbiPrev,       // format of previous frame
            int lpbiPrev,       // format of previous frame
            int lpPrev          // previous frame
            );
        [DllImport("MSVFW32.dll")]
        public static extern int ICDecompress(
            int hic,
            int dwFlags,
            ref BITMAPINFOHEADER lpbiFormat,
            byte[] lpData,
            ref BITMAPINFOHEADER lpbi,
            byte[] lpBits
            );
        [DllImport("MSVFW32.dll")]
        public static extern int ICSendMessage(int hic, int msg, ref BITMAPINFO dw1, ref BITMAPINFO dw2);
        [DllImport("MSVFW32.dll")]
        public static extern int ICSendMessage(int hic, int msg, int dw1, int dw2);
        [DllImport("MSVFW32.dll")]
        public static extern int ICSendMessage(int hic, int msg, ICINFO dw1, int dw2);
        public static readonly int DRV_USER = 0x4000;
        public static readonly int ICM_USER = (DRV_USER + 0x0000);
        public static readonly int ICM_COMPRESS_BEGIN = (ICM_USER + 7);    // begin a series of compress calls.
        public static readonly int ICM_COMPRESS = (ICM_USER + 8);   // compress a frame
        public static readonly int ICM_COMPRESS_END = (ICM_USER + 9);   // end of a series of compress calls.
        public static readonly int ICM_COMPRESS_GET_FORMAT = (ICM_USER + 4);
        public static readonly int ICM_DECOMPRESS_BEGIN = (ICM_USER + 12);   // start a series of decompress calls
        public static readonly int ICM_DECOMPRESS = (ICM_USER + 13);   // decompress a frame
        public static readonly int ICM_DECOMPRESS_END = (ICM_USER + 14);
        #endregion
    }
    public class ICCompressor : ICBase
    {
        public ICCompressor(COMPVARS cp, BITMAPINFO biIn, int fourcc)
            : base(cp, biIn, ICMODE.ICMODE_COMPRESS, fourcc)
        {
        }
        public override void Open()
        {
            base.Open();
            int r = ICSendMessage(hic, ICM_COMPRESS_GET_FORMAT, ref this._in, ref this._out);
            bool s = ICSeqCompressFrameStart(this.Compvars, ref this._in);
        }
        public override byte[] Process(byte[] data)
        {
            if (this.hic == 0) return null;
            bool key = false; long size = 0;
            try
            {
                lock (data)
                {
                    IntPtr r = (IntPtr)ICSeqCompressFrame(this.pp, 0, data, ref key, ref size);
                    byte[] b = new byte[size];
                    Marshal.Copy(r, b, 0, (int)size);
                    return b;
                }
                //	int r=ICCompress(hic,0,ref this._out.bmiHeader,pdata,ref this._in.bmiHeader,data,0,0,0,0,0,0,0);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            return null;
        }
        public override void Close()
        {
            if (this.hic != 0)
            {
                try
                {
                    ICSeqCompressFrameEnd(ref this.pp);
                }
                catch (System.Exception)
                {
                    //gowk.utility.Diagnostics.Debug.Write(ex);
                }
            }
            base.Close();
        }
    }
    public class ICDecompressor : ICBase
    {
        public ICDecompressor(COMPVARS cp, BITMAPINFO biIn, int fourcc)
            : base(cp, biIn, ICMODE.ICMODE_DECOMPRESS, fourcc)
        {
        }
        public override void Open()
        {
            base.Open();
            int r = ICSendMessage(hic, ICM_USER + 10, ref this._in, ref this._out);//get the output bitmapinfo
            r = ICSendMessage(hic, ICM_DECOMPRESS_BEGIN, ref this._in, ref this._out);
        }
        public override byte[] Process(byte[] data)
        {
            if (this.hic == 0) return null;
            byte[] b = new byte[this._out.bmiHeader.biSizeImage]; ;
            try
            {
                int i = ICDecompress(this.hic, 0, ref this._in.bmiHeader, data, ref this._out.bmiHeader, b);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return b;
        }
        public override void Close()
        {
            if (this.hic != 0)
            {
                ICSendMessage(hic, ICM_USER + 14, 0, 0);//get the output bitmapinfo
            }
            base.Close();
        }
    }
    #region
    /*
	public class ICDraw:ICBase
	{
		public Control Control;
		public ICDraw(COMPVARS cp,BITMAPINFO biIn,int fourcc):base(cp,biIn,ICMODE.ICMODE_DRAW,fourcc)
		{
		}
		public override void Open()
		{
			base.Open ();
			System.Drawing.Graphics g=this.Control.CreateGraphics();
			IntPtr hdc=g.GetHdc();
			int rrr=ICSendMessage(hic,ICM_USER+10,ref this._in,ref this._out);
			int ret=ICDrawBegin(
				this.HIC,
				ICDRAWFlag.ICDRAW_HDC|ICDRAWFlag.ICDRAW_CONTINUE,
				Graphics.GetHalftonePalette(),
				this.Control.Handle,
				hdc,
				0,0,160,120,
				ref this._in.bmiHeader,
				0,0,160,120,
				120,
				8
			//	(int)(this.Compvars.lDataRate/this.Compvars.lKey)
				);
		}
		public override byte[] Process(byte[] data)
		{
			byte[] b=new byte[57600];;
			IntPtr p=IntPtr.Zero;
			try
			{
				int i=ICDecompress(this.hic,0,ref this._in.bmiHeader,data,ref this._out.bmiHeader,b);
				System.Diagnostics.Trace.WriteLine(p.ToString());
			}
			catch(System.Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return b;
		}
		protected override void Close()
		{
			base.Close ();
		}
		[DllImport("MSVFW32.dll")]
		static extern int ICDrawBegin(
			int hic,
			ICDRAWFlag dwFlags,        // flags
			IntPtr hpal,           // palette to draw with
			IntPtr    hwnd,           // window to draw to
			IntPtr  hdc,            // HDC to draw to
			int   xDst,           // destination rectangle
			int yDst,
			int dxDst,
			int dyDst,
			ref BITMAPINFOHEADER  lpbi,           // format of frame to draw
			int  xSrc,           // source rectangle
			int  ySrc,
			int  dxSrc,
			int  dySrc,
			int   dwRate,         // frames/second = (dwRate/dwScale)
			int dwScale
			);

	}*/
    #endregion
}