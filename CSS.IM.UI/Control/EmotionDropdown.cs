using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Face;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    public partial class EmotionDropdown : UserControl
    {
        private Popup _popup;
        public Dictionary<String,String> faces=new Dictionary<string,string>();

        public EmotionDropdown()
        {
            InitializeComponent();
            _popup = new Popup(this);

            EmotionContainer.ItemClick += 
                new EmotionItemMouseEventHandler(EmotionContainerItemClick);

            for (int i = 0; i < 135; i++)
            {
                EmotionItem item = new EmotionItem();
                item.Image = ResClass.GetImgRes("_" + i);
                item.ToolTip = i.ToString();
                item.Text = i.ToString();
                int index = i;
                String str = i.ToString() ;
                if (str.Length==1)
                {
                    str = "00" + str;
                }
                if (str.Length==2)
                {
                    str = "0" + str;
                }
                //str="/^" + str;
                //str = str;
                item.Tag = str;
                faces.Add(str, str);
                EmotionContainer.Items.Add(item);
                System.GC.Collect();
            }
            System.GC.Collect();
        }

        void EmotionContainerItemClick(
            object sender, EmotionItemMouseClickEventArgs e)
        {
            _popup.Close();
            System.GC.Collect();
        }

        public EmotionContainer EmotionContainer
        {
            get { return emotionContainer1; }
        }

        public void Show(System.Windows.Forms.Control owner)
        {
            _popup.Show(owner, true);
            System.GC.Collect();
        }


        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (faces != null)
            {
                faces.Clear();
                faces = null;
            }

            if (emotionContainer1 != null)
            {
                emotionContainer1.Dispose();
            }

            System.GC.Collect();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            System.GC.Collect();
        }
    }
}
