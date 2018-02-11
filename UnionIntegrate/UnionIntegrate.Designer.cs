namespace UnionIntegrate
{
    partial class UnionIntegrate
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
            this.output_btn = new System.Windows.Forms.Button();
            this.record_btn = new System.Windows.Forms.Button();
            this.setp_btn = new System.Windows.Forms.Button();
            this.ctrl_btn = new System.Windows.Forms.Button();
            this.updatetimer = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // output_btn
            // 
            this.output_btn.Location = new System.Drawing.Point(255, 12);
            this.output_btn.Name = "output_btn";
            this.output_btn.Size = new System.Drawing.Size(91, 23);
            this.output_btn.TabIndex = 7;
            this.output_btn.Text = "Output Record";
            this.output_btn.UseVisualStyleBackColor = true;
            this.output_btn.Click += new System.EventHandler(this.output_btn_Click);
            // 
            // record_btn
            // 
            this.record_btn.Location = new System.Drawing.Point(174, 12);
            this.record_btn.Name = "record_btn";
            this.record_btn.Size = new System.Drawing.Size(75, 23);
            this.record_btn.TabIndex = 6;
            this.record_btn.Text = "Record";
            this.record_btn.UseVisualStyleBackColor = true;
            this.record_btn.Click += new System.EventHandler(this.record_btn_Click);
            // 
            // setp_btn
            // 
            this.setp_btn.Location = new System.Drawing.Point(93, 12);
            this.setp_btn.Name = "setp_btn";
            this.setp_btn.Size = new System.Drawing.Size(75, 23);
            this.setp_btn.TabIndex = 5;
            this.setp_btn.Text = "Step";
            this.setp_btn.UseVisualStyleBackColor = true;
            this.setp_btn.Click += new System.EventHandler(this.setp_btn_Click);
            // 
            // ctrl_btn
            // 
            this.ctrl_btn.Location = new System.Drawing.Point(12, 12);
            this.ctrl_btn.Name = "ctrl_btn";
            this.ctrl_btn.Size = new System.Drawing.Size(75, 23);
            this.ctrl_btn.TabIndex = 4;
            this.ctrl_btn.Text = "Run/Pause";
            this.ctrl_btn.UseVisualStyleBackColor = true;
            this.ctrl_btn.Click += new System.EventHandler(this.ctrl_btn_Click);
            // 
            // updatetimer
            // 
            this.updatetimer.Enabled = true;
            this.updatetimer.Interval = 15;
            this.updatetimer.Tick += new System.EventHandler(this.updatetimer_Tick);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "files(*.txt)|*.txt";
            // 
            // UnionIntegrate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 666);
            this.Controls.Add(this.output_btn);
            this.Controls.Add(this.record_btn);
            this.Controls.Add(this.setp_btn);
            this.Controls.Add(this.ctrl_btn);
            this.DoubleBuffered = true;
            this.Name = "UnionIntegrate";
            this.Text = "Union Integrate";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UnionPreColl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UnionPreColl_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UnionPreColl_MouseUp);
            this.Resize += new System.EventHandler(this.UnionPreColl_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button output_btn;
        private System.Windows.Forms.Button record_btn;
        private System.Windows.Forms.Button setp_btn;
        private System.Windows.Forms.Button ctrl_btn;
        private System.Windows.Forms.Timer updatetimer;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;

    }
}

