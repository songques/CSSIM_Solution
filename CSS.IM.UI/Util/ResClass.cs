using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace CSS.IM.UI.Util
{
    public class ResClass
    {
        private static string _SkinFileName = "";
        public static string SkinFilePath = "";
        public static string Pass = "1234";

        public static Bitmap GetImgRes(string name)
        {
            if (name == null || name == "")
                return null;
            //string s1 = Path.GetFileNameWithoutExtension(SkinFileName) + "\\" + name + ".png";
            //if (!File.Exists(SkinFilePath + s1))
            //{
            //    s1 = Path.GetFileNameWithoutExtension(SkinFileName) + "\\" + name + ".bmp";
            //    if (!File.Exists(SkinFilePath + s1))
            //    {
            //        s1 = Path.GetFileNameWithoutExtension(SkinFileName) + "\\" + name + ".jpg";
            //    }
            //}
            //Bitmap bmp = null;
            //try
            //{
            //    bmp = (Bitmap)Bitmap.FromFile(SkinFilePath + s1);
            //}
            //catch (Exception)
            //{
            //    bmp = null;
            //}
            //GC.Collect();
            Bitmap bim = null;
            try
            {
                bim = (Bitmap)Properties.Resources.ResourceManager.GetObject(name);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return bim;
        }

        public static Bitmap GetHead(string name)
        {
            if (name == null || name == "")
                name = "big1";
            return (Bitmap)Properties.Resources.ResourceManager.GetObject(name);
        }

        public static string SkinFileName
        {
            get 
            { 
                return _SkinFileName; 
            }
            set 
            {
                if (value != _SkinFileName)
                {
                    ExtractSkin(SkinFilePath, value);
                    _SkinFileName = value;
                }
            }
        }

        private static void ExtractSkin(string saveto, string SkinFileName)
        {
            //if (!Directory.Exists(saveto))
            //{
            //    Directory.CreateDirectory(saveto);
            //}
            //string sfile = SkinFileName;
            //if (File.Exists(sfile))
            //{
            //    FastZip fz = new FastZip();
            //    fz.Password = Pass;
            //    fz.ExtractZip(sfile, saveto + "\\" + Path.GetFileNameWithoutExtension(SkinFileName), "");
            //    fz = null;
            //}
            //else
            //{
            //    SkinFileName = "qq2010.qsf";
            //}
            //GC.Collect();
        }

        public static Bitmap MarkTopHead(Image image)
        {
            Bitmap OldHeadImg = new Bitmap(image);
            Bitmap headImg = null;
            try
            {
                int Height = OldHeadImg.Height;
                int Width = OldHeadImg.Width;

                headImg = new Bitmap(Width, Height);
                Color pixel;
                for (int x = 0; x < Width; x++)
                    for (int y = 0; y < Height; y++)
                    {
                        pixel = OldHeadImg.GetPixel(x, y);
                        int r, g, b, Result = 0;
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;
                        //实例程序以加权平均值法产生黑白图像
                        int iType = 2;
                        switch (iType)
                        {
                            case 0://平均值法
                                Result = ((r + g + b) / 3);
                                break;
                            case 1://最大值法
                                Result = r > g ? r : g;
                                Result = Result > b ? Result : b;
                                break;
                            case 2://加权平均值法
                                Result = ((int)(0.7 * r) + (int)(0.2 * g) + (int)(0.1 * b));
                                break;
                        }
                        headImg.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                    }

            }
            catch (Exception) { }
            OldHeadImg.Dispose();
            OldHeadImg = null;
            return headImg;
        }
    }
}
