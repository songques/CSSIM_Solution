using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    public class TabImageButton : PictureBox
    {
        private Bitmap Bmp = ResClass.GetImgRes("main_tab_bkg");

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

            try
            {
                base.Dispose(disposing);
            }
            catch (System.Exception)
            {

            }

            System.GC.Collect();
        }


        public TabImageButton()
        {
            InitializeComponent();

            BackColor = Color.Transparent;
            this.Size = new Size(59, 27);
            this.SizeMode = PictureBoxSizeMode.CenterImage;
            BackgroundImage = Bmp;
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
            Bmp = ResClass.GetImgRes("main_tab_highlight");
            BackgroundImage = Bmp;
            System.GC.Collect();
        }

        private void MiniImageButton_MouseLeave(object sender, EventArgs e)
        {
            if (SelectTab)
            {
                Bmp = ResClass.GetImgRes("main_tab_check");
                BackgroundImage = Bmp;
            }

            else
            {
                Bmp = ResClass.GetImgRes("main_tab_bkg");
                BackgroundImage = Bmp;
            }
            System.GC.Collect();
        }
    }
}
