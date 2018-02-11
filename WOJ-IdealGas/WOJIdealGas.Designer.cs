namespace WOJ_IdealGas
{
    partial class WOJIdealGas
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
            this.components = new System.ComponentModel.Container();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.CtrlBtn = new System.Windows.Forms.Button();
            this.StepBtn = new System.Windows.Forms.Button();
            this.volume_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Interval = 25;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // CtrlBtn
            // 
            this.CtrlBtn.Location = new System.Drawing.Point(12, 12);
            this.CtrlBtn.Name = "CtrlBtn";
            this.CtrlBtn.Size = new System.Drawing.Size(75, 23);
            this.CtrlBtn.TabIndex = 0;
            this.CtrlBtn.Text = "Run";
            this.CtrlBtn.UseVisualStyleBackColor = true;
            this.CtrlBtn.Click += new System.EventHandler(this.CtrlBtn_Click);
            // 
            // StepBtn
            // 
            this.StepBtn.Location = new System.Drawing.Point(93, 12);
            this.StepBtn.Name = "StepBtn";
            this.StepBtn.Size = new System.Drawing.Size(75, 23);
            this.StepBtn.TabIndex = 1;
            this.StepBtn.Text = "Step";
            this.StepBtn.UseVisualStyleBackColor = true;
            this.StepBtn.Click += new System.EventHandler(this.StepBtn_Click);
            // 
            // volume_lbl
            // 
            this.volume_lbl.AutoSize = true;
            this.volume_lbl.Location = new System.Drawing.Point(12, 46);
            this.volume_lbl.Name = "volume_lbl";
            this.volume_lbl.Size = new System.Drawing.Size(59, 12);
            this.volume_lbl.TabIndex = 2;
            this.volume_lbl.Text = "volume : ";
            // 
            // WOJIdealGas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 567);
            this.Controls.Add(this.volume_lbl);
            this.Controls.Add(this.StepBtn);
            this.Controls.Add(this.CtrlBtn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WOJIdealGas";
            this.Text = "WOJ IdealGas";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WOJIdealGas_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.Button CtrlBtn;
        private System.Windows.Forms.Button StepBtn;
        private System.Windows.Forms.Label volume_lbl;
    }
}

