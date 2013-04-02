namespace CSS.IM.UI.Control
{
    partial class StateButton
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
            this.SuspendLayout();
            // 
            // StateButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.DoubleBuffered = true;
            this.Name = "StateButton";
            this.Size = new System.Drawing.Size(32, 20);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.stateButton_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.stateButton_MouseDown);
            this.MouseEnter += new System.EventHandler(this.stateButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.stateButton_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.stateButton_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
