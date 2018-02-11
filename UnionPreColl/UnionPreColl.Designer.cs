namespace UnionPreColl
{
    partial class UnionPreColl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.updatetimer = new System.Windows.Forms.Timer(this.components);
            this.ctrl_btn = new System.Windows.Forms.Button();
            this.setp_btn = new System.Windows.Forms.Button();
            this.record_btn = new System.Windows.Forms.Button();
            this.output_btn = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // updatetimer
            // 
            this.updatetimer.Enabled = true;
            this.updatetimer.Interval = 15;
            this.updatetimer.Tick += new System.EventHandler(this.updatetimer_Tick);
            // 
            // ctrl_btn
            // 
            this.ctrl_btn.Location = new System.Drawing.Point(12, 12);
            this.ctrl_btn.Name = "ctrl_btn";
            this.ctrl_btn.Size = new System.Drawing.Size(75, 23);
            this.ctrl_btn.TabIndex = 0;
            this.ctrl_btn.Text = "Run/Pause";
            this.ctrl_btn.UseVisualStyleBackColor = true;
            this.ctrl_btn.Click += new System.EventHandler(this.ctrl_btn_Click);
            // 
            // setp_btn
            // 
            this.setp_btn.Location = new System.Drawing.Point(93, 12);
            this.setp_btn.Name = "setp_btn";
            this.setp_btn.Size = new System.Drawing.Size(75, 23);
            this.setp_btn.TabIndex = 1;
            this.setp_btn.Text = "Step";
            this.setp_btn.UseVisualStyleBackColor = true;
            this.setp_btn.Click += new System.EventHandler(this.setp_btn_Click);
            // 
            // record_btn
            // 
            this.record_btn.Location = new System.Drawing.Point(174, 12);
            this.record_btn.Name = "record_btn";
            this.record_btn.Size = new System.Drawing.Size(75, 23);
            this.record_btn.TabIndex = 2;
            this.record_btn.Text = "Record";
            this.record_btn.UseVisualStyleBackColor = true;
            this.record_btn.Click += new System.EventHandler(this.record_btn_Click);
            // 
            // output_btn
            // 
            this.output_btn.Location = new System.Drawing.Point(255, 12);
            this.output_btn.Name = "output_btn";
            this.output_btn.Size = new System.Drawing.Size(91, 23);
            this.output_btn.TabIndex = 3;
            this.output_btn.Text = "Output Record";
            this.output_btn.UseVisualStyleBackColor = true;
            this.output_btn.Click += new System.EventHandler(this.output_btn_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "files(*.txt)|*.txt";
            // 
            // UnionPreColl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 666);
            this.Controls.Add(this.output_btn);
            this.Controls.Add(this.record_btn);
            this.Controls.Add(this.setp_btn);
            this.Controls.Add(this.ctrl_btn);
            this.DoubleBuffered = true;
            this.Name = "UnionPreColl";
            this.Text = "Union Preservation Collision";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UnionPreColl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UnionPreColl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UnionPreColl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UnionPreColl_MouseUp);
            this.Resize += new System.EventHandler(this.UnionPreColl_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer updatetimer;
        private System.Windows.Forms.Button ctrl_btn;
        private System.Windows.Forms.Button setp_btn;
        private System.Windows.Forms.Button record_btn;
        private System.Windows.Forms.Button output_btn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

