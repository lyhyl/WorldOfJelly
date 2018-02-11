namespace StaticPolygonCollisionTestB
{
    partial class StaticPolygonCollisionTestB
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
            this.restrict_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // restrict_btn
            // 
            this.restrict_btn.Location = new System.Drawing.Point(12, 12);
            this.restrict_btn.Name = "restrict_btn";
            this.restrict_btn.Size = new System.Drawing.Size(75, 23);
            this.restrict_btn.TabIndex = 0;
            this.restrict_btn.Text = "Restrict";
            this.restrict_btn.UseVisualStyleBackColor = true;
            this.restrict_btn.Click += new System.EventHandler(this.restrict_btn_Click);
            // 
            // StaticPolygonCollisionTestB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.restrict_btn);
            this.DoubleBuffered = true;
            this.Name = "StaticPolygonCollisionTestB";
            this.Text = "StaticPolygonCollisionTestB";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.StaticPolygonCollisionTestB_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTestB_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTestB_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button restrict_btn;
    }
}

