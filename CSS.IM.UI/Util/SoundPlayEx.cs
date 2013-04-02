using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CSS.IM.UI.Util
{
    public class SoundPlayEx
    {
        public static void MsgPlay(String filepathname)
        {
            try
            {
                //System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(Application.StartupPath + @"/Sound/" + name + ".wav");
                System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(filepathname);
                sndPlayer.Play();
            }
            catch (Exception)
            {

            }
           
        }
    }
}
