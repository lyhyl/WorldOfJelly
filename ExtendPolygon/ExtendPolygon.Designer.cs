namespace ExtendPolygon
{
    partial class ExtendPolygon
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
            this.SuspendLayout();
            // 
            // ExtendPolygon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.DoubleBuffered = true;
            this.Name = "ExtendPolygon";
            this.Text = "ExtendPolygon";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ExtendPolygon_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ExtendPolygon_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ExtendPolygon_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

