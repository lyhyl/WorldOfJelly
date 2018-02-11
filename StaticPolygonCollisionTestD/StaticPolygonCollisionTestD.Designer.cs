namespace StaticPolygonCollisionTestD
{
    partial class StaticPolygonCollisionTestD
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ForceLabel = new System.Windows.Forms.Label();
            this.ConstantC = new System.Windows.Forms.NumericUpDown();
            this.ConstantDlim = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.ConstantC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConstantDlim)).BeginInit();
            this.SuspendLayout();
            // 
            // ForceLabel
            // 
            this.ForceLabel.AutoSize = true;
            this.ForceLabel.Location = new System.Drawing.Point(12, 9);
            this.ForceLabel.Name = "ForceLabel";
            this.ForceLabel.Size = new System.Drawing.Size(41, 12);
            this.ForceLabel.TabIndex = 0;
            this.ForceLabel.Text = "Force:";
            // 
            // ConstantC
            // 
            this.ConstantC.DecimalPlaces = 2;
            this.ConstantC.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ConstantC.Location = new System.Drawing.Point(360, 7);
            this.ConstantC.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.ConstantC.Name = "ConstantC";
            this.ConstantC.Size = new System.Drawing.Size(120, 21);
            this.ConstantC.TabIndex = 1;
            this.ConstantC.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // ConstantDlim
            // 
            this.ConstantDlim.DecimalPlaces = 2;
            this.ConstantDlim.Location = new System.Drawing.Point(360, 34);
            this.ConstantDlim.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ConstantDlim.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.ConstantDlim.Name = "ConstantDlim";
            this.ConstantDlim.Size = new System.Drawing.Size(120, 21);
            this.ConstantDlim.TabIndex = 2;
            this.ConstantDlim.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // StaticPolygonCollisionTestD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 466);
            this.Controls.Add(this.ConstantDlim);
            this.Controls.Add(this.ConstantC);
            this.Controls.Add(this.ForceLabel);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "StaticPolygonCollisionTestD";
            this.Text = "StaticPolygonCollisionTestD";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.StaticPolygonCollisionTestD_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTestD_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTestD_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.ConstantC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConstantDlim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ForceLabel;
        private System.Windows.Forms.NumericUpDown ConstantC;
        private System.Windows.Forms.NumericUpDown ConstantDlim;
    }
}

