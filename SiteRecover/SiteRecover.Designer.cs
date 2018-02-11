namespace SiteRecover
{
    partial class SiteRecover
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
            this.file_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.recover_frame_id = new System.Windows.Forms.NumericUpDown();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.proccoll_btn = new System.Windows.Forms.Button();
            this.play_btn = new System.Windows.Forms.Button();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.inval_bar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.recover_frame_id)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inval_bar)).BeginInit();
            this.SuspendLayout();
            // 
            // file_btn
            // 
            this.file_btn.Location = new System.Drawing.Point(12, 12);
            this.file_btn.Name = "file_btn";
            this.file_btn.Size = new System.Drawing.Size(75, 23);
            this.file_btn.TabIndex = 0;
            this.file_btn.Text = "File";
            this.file_btn.UseVisualStyleBackColor = true;
            this.file_btn.Click += new System.EventHandler(this.file_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Frame:";
            // 
            // recover_frame_id
            // 
            this.recover_frame_id.Location = new System.Drawing.Point(302, 15);
            this.recover_frame_id.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.recover_frame_id.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.recover_frame_id.Name = "recover_frame_id";
            this.recover_frame_id.Size = new System.Drawing.Size(120, 21);
            this.recover_frame_id.TabIndex = 3;
            this.recover_frame_id.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.recover_frame_id.ValueChanged += new System.EventHandler(this.recover_event);
            // 
            // proccoll_btn
            // 
            this.proccoll_btn.Location = new System.Drawing.Point(93, 12);
            this.proccoll_btn.Name = "proccoll_btn";
            this.proccoll_btn.Size = new System.Drawing.Size(75, 23);
            this.proccoll_btn.TabIndex = 1;
            this.proccoll_btn.Text = "Coll Proc";
            this.proccoll_btn.UseVisualStyleBackColor = true;
            this.proccoll_btn.Click += new System.EventHandler(this.proccoll_btn_Click);
            // 
            // play_btn
            // 
            this.play_btn.Location = new System.Drawing.Point(174, 12);
            this.play_btn.Name = "play_btn";
            this.play_btn.Size = new System.Drawing.Size(75, 23);
            this.play_btn.TabIndex = 2;
            this.play_btn.Text = "Auto Play";
            this.play_btn.UseVisualStyleBackColor = true;
            this.play_btn.Click += new System.EventHandler(this.play_btn_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 50;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // inval_bar
            // 
            this.inval_bar.Location = new System.Drawing.Point(81, 41);
            this.inval_bar.Maximum = 200;
            this.inval_bar.Minimum = 5;
            this.inval_bar.Name = "inval_bar";
            this.inval_bar.Size = new System.Drawing.Size(332, 45);
            this.inval_bar.SmallChange = 5;
            this.inval_bar.TabIndex = 5;
            this.inval_bar.TickFrequency = 5;
            this.inval_bar.Value = 50;
            this.inval_bar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "interval :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(115, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "1^x";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(154, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "2^x";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(233, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "4^x";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(390, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "8^x";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(428, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Focus";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SiteRecover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inval_bar);
            this.Controls.Add(this.play_btn);
            this.Controls.Add(this.proccoll_btn);
            this.Controls.Add(this.recover_frame_id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.file_btn);
            this.Name = "SiteRecover";
            this.Text = "Site Recover";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SiteRecover_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SiteRecover_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SiteRecover_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.SiteRecover_MouseWheel);
            ((System.ComponentModel.ISupportInitialize)(this.recover_frame_id)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inval_bar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button file_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown recover_frame_id;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button proccoll_btn;
        private System.Windows.Forms.Button play_btn;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.TrackBar inval_bar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
    }
}

