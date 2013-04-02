using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using CSS.IM.UI.Util;
using System.Windows.Forms;

namespace CSS.IM.UI.Control
{
    public class HeadPortrait: PictureBox
    {
        private Bitmap Bmp = ResClass.GetImgRes("Padding4Normal");

        private Boolean _SelectTab = false;
        public Boolean SelectTab
        {
            get
            {
                return _SelectTab;
            }
            set
            {
                _SelectTab = value;
                MiniImageButton_MouseLeave(null, null);
            }
        }

         /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {

            if (Bmp != null)
            {
                Bmp.Dispose();
                Bmp = null;
            }

            if (this.Image!=null)
            {
                Image.Dispose();
                Image = null;
            }

            try
            {
                base.Dispose(disposing);
            }
            catch (System.Exception)
            {

            }

            System.GC.Collect();
        }


        public HeadPortrait()
        {
            InitializeComponent();

            BackColor = Color.Transparent;
            this.Size = new Size(59, 27);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            BackgroundImage = Bmp;
            if (Image==null)
            {
                Image = ResClass.GetHead("big194");
            }
        }

        private void InitializeComponent()
        {

            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // MiniImageButton
            // 
            this.MouseEnter += new System.EventHandler(this.MiniImageButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MiniImageButton_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void MiniImageButton_MouseEnter(object sender, EventArgs e)
        {
            Bmp = ResClass.GetImgRes("Padding4Select");
            BackgroundImage = Bmp;
            System.GC.Collect();
        }

        private void MiniImageButton_MouseLeave(object sender, EventArgs e)
        {
            Bmp = ResClass.GetImgRes("Padding4Normal");
            BackgroundImage = Bmp;
            System.GC.Collect();
        }
    }
}
