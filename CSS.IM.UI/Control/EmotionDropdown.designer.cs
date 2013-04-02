using CSS.IM.UI.Face;
namespace CSS.IM.UI.Control
{
    partial class EmotionDropdown
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.emotionContainer1 = new CSS.IM.UI.Face.EmotionContainer();
            this.SuspendLayout();
            // 
            // emotionContainer1
            // 
            this.emotionContainer1.BackColor = System.Drawing.Color.White;
            this.emotionContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emotionContainer1.GridSize = 26;
            this.emotionContainer1.Location = new System.Drawing.Point(0, 0);
            this.emotionContainer1.Name = "emotionContainer1";
            this.emotionContainer1.Row = 15;
            this.emotionContainer1.TabIndex = 0;
            // 
            // EmotionDropdown
            // 
            this.Controls.Add(this.emotionContainer1);
            this.Name = "EmotionDropdown";
            this.Size = new System.Drawing.Size(390, 236);
            this.ResumeLayout(false);

        }

        #endregion

        private EmotionContainer emotionContainer1;


    }
}
