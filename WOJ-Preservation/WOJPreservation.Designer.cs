namespace UnionPreColl
{
    partial class WOJPreservation
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
            this.area_info = new System.Windows.Forms.Label();
            this.step_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // updatetimer
            // 
            this.updatetimer.Interval = 25;
            this.updatetimer.Tick += new System.EventHandler(this.updatetimer_Tick);
            // 
            // ctrl_btn
            // 
            this.ctrl_btn.Location = new System.Drawing.Point(12, 12);
            this.ctrl_btn.Name = "ctrl_btn";
            this.ctrl_btn.Size = new System.Drawing.Size(75, 23);
            this.ctrl_btn.TabIndex = 0;
            this.ctrl_btn.Text = "Pause/Run";
            this.ctrl_btn.UseVisualStyleBackColor = true;
            this.ctrl_btn.Click += new System.EventHandler(this.ctrl_btn_Click);
            // 
            // area_info
            // 
            this.area_info.AutoSize = true;
            this.area_info.Location = new System.Drawing.Point(93, 17);
            this.area_info.Name = "area_info";
            this.area_info.Size = new System.Drawing.Size(47, 12);
            this.area_info.TabIndex = 1;
            this.area_info.Text = "Area : ";
            // 
            // step_btn
            // 
            this.step_btn.Location = new System.Drawing.Point(12, 41);
            this.step_btn.Name = "step_btn";
            this.step_btn.Size = new System.Drawing.Size(75, 23);
            this.step_btn.TabIndex = 2;
            this.step_btn.Text = "Step";
            this.step_btn.UseVisualStyleBackColor = true;
            this.step_btn.Click += new System.EventHandler(this.step_btn_Click);
            // 
            // WOJPreservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.step_btn);
            this.Controls.Add(this.area_info);
            this.Controls.Add(this.ctrl_btn);
            this.DoubleBuffered = true;
            this.Name = "WOJPreservation";
            this.Text = "WOJ Preservation";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WOJPreservation_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WOJPreservation_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WOJPreservation_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer updatetimer;
        private System.Windows.Forms.Button ctrl_btn;
        private System.Windows.Forms.Label area_info;
        private System.Windows.Forms.Button step_btn;
    }
}

