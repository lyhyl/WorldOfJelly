namespace StaticPolygonCollisionTest
{
    partial class StaticPolygonCollisionTest
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
            this.InfoStatusStrip = new System.Windows.Forms.StatusStrip();
            this.MousePositionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.restrict_btn = new System.Windows.Forms.Button();
            this.apply_btn = new System.Windows.Forms.Button();
            this.UseTimeLabel = new System.Windows.Forms.Label();
            this.MaxTimeLabel = new System.Windows.Forms.Label();
            this.InfoStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // InfoStatusStrip
            // 
            this.InfoStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MousePositionLabel});
            this.InfoStatusStrip.Location = new System.Drawing.Point(0, 440);
            this.InfoStatusStrip.Name = "InfoStatusStrip";
            this.InfoStatusStrip.Size = new System.Drawing.Size(484, 22);
            this.InfoStatusStrip.TabIndex = 0;
            this.InfoStatusStrip.Text = "statusStrip1";
            // 
            // MousePositionLabel
            // 
            this.MousePositionLabel.Name = "MousePositionLabel";
            this.MousePositionLabel.Size = new System.Drawing.Size(33, 17);
            this.MousePositionLabel.Text = "(0,0)";
            // 
            // restrict_btn
            // 
            this.restrict_btn.Location = new System.Drawing.Point(12, 12);
            this.restrict_btn.Name = "restrict_btn";
            this.restrict_btn.Size = new System.Drawing.Size(75, 23);
            this.restrict_btn.TabIndex = 1;
            this.restrict_btn.Text = "Restrict";
            this.restrict_btn.UseVisualStyleBackColor = true;
            this.restrict_btn.Click += new System.EventHandler(this.restrict_btn_Click);
            // 
            // apply_btn
            // 
            this.apply_btn.Location = new System.Drawing.Point(93, 12);
            this.apply_btn.Name = "apply_btn";
            this.apply_btn.Size = new System.Drawing.Size(75, 23);
            this.apply_btn.TabIndex = 2;
            this.apply_btn.Text = "Apply";
            this.apply_btn.UseVisualStyleBackColor = true;
            this.apply_btn.Click += new System.EventHandler(this.apply_btn_Click);
            // 
            // UseTimeLabel
            // 
            this.UseTimeLabel.AutoSize = true;
            this.UseTimeLabel.Location = new System.Drawing.Point(12, 38);
            this.UseTimeLabel.Name = "UseTimeLabel";
            this.UseTimeLabel.Size = new System.Drawing.Size(29, 12);
            this.UseTimeLabel.TabIndex = 3;
            this.UseTimeLabel.Text = "0 ms";
            // 
            // MaxTimeLabel
            // 
            this.MaxTimeLabel.AutoSize = true;
            this.MaxTimeLabel.Location = new System.Drawing.Point(12, 50);
            this.MaxTimeLabel.Name = "MaxTimeLabel";
            this.MaxTimeLabel.Size = new System.Drawing.Size(29, 12);
            this.MaxTimeLabel.TabIndex = 4;
            this.MaxTimeLabel.Text = "0 ms";
            // 
            // StaticPolygonCollisionTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.MaxTimeLabel);
            this.Controls.Add(this.UseTimeLabel);
            this.Controls.Add(this.apply_btn);
            this.Controls.Add(this.restrict_btn);
            this.Controls.Add(this.InfoStatusStrip);
            this.DoubleBuffered = true;
            this.Name = "StaticPolygonCollisionTest";
            this.Text = "Static Polygon Collision Test";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.StaticPolygonCollisionTest_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTest_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTest_MouseUp);
            this.InfoStatusStrip.ResumeLayout(false);
            this.InfoStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip InfoStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel MousePositionLabel;
        private System.Windows.Forms.Button restrict_btn;
        private System.Windows.Forms.Button apply_btn;
        private System.Windows.Forms.Label UseTimeLabel;
        private System.Windows.Forms.Label MaxTimeLabel;
    }
}

