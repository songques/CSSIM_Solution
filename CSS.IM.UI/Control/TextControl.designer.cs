namespace CSS.IM.UI.Control
{
    partial class TextControl
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TextPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // TextPanel
            // 
            this.TextPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextPanel.Location = new System.Drawing.Point(0, 0);
            this.TextPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TextPanel.Name = "TextPanel";
            this.TextPanel.Size = new System.Drawing.Size(357, 176);
            this.TextPanel.TabIndex = 0;
            this.TextPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TextPanel_Paint);
            // 
            // TextControl
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.TextPanel);
            this.Name = "TextControl";
            this.Size = new System.Drawing.Size(374, 176);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TextPanel;

    }
}
