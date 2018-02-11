namespace JellyCodeBuilder
{
    partial class JellyCodeBuilder
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
            this.MainUI = new System.Windows.Forms.SplitContainer();
            this.DrawSpt = new System.Windows.Forms.SplitContainer();
            this.DrawPlane = new System.Windows.Forms.Panel();
            this.SettingsPlane = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.EdgeBox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.InformationLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ErrorLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CNoneBTn = new System.Windows.Forms.RadioButton();
            this.CAreBtn = new System.Windows.Forms.RadioButton();
            this.CDisBtn = new System.Windows.Forms.RadioButton();
            this.CNodeBtn = new System.Windows.Forms.RadioButton();
            this.UnitSizeNUD = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.RangeNUD = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.CodeProSpt = new System.Windows.Forms.SplitContainer();
            this.CodeViewer = new System.Windows.Forms.RichTextBox();
            this.PropertyPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.NamespaceTextBox = new System.Windows.Forms.TextBox();
            this.TypeNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.rebuildNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.MainUI)).BeginInit();
            this.MainUI.Panel1.SuspendLayout();
            this.MainUI.Panel2.SuspendLayout();
            this.MainUI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawSpt)).BeginInit();
            this.DrawSpt.Panel1.SuspendLayout();
            this.DrawSpt.Panel2.SuspendLayout();
            this.DrawSpt.SuspendLayout();
            this.SettingsPlane.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UnitSizeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RangeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CodeProSpt)).BeginInit();
            this.CodeProSpt.Panel1.SuspendLayout();
            this.CodeProSpt.Panel2.SuspendLayout();
            this.CodeProSpt.SuspendLayout();
            this.PropertyPanel.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainUI
            // 
            this.MainUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainUI.Location = new System.Drawing.Point(3, 28);
            this.MainUI.Name = "MainUI";
            // 
            // MainUI.Panel1
            // 
            this.MainUI.Panel1.Controls.Add(this.DrawSpt);
            // 
            // MainUI.Panel2
            // 
            this.MainUI.Panel2.Controls.Add(this.CodeProSpt);
            this.MainUI.Size = new System.Drawing.Size(778, 531);
            this.MainUI.SplitterDistance = 467;
            this.MainUI.TabIndex = 0;
            // 
            // DrawSpt
            // 
            this.DrawSpt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawSpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawSpt.Location = new System.Drawing.Point(0, 0);
            this.DrawSpt.Name = "DrawSpt";
            this.DrawSpt.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // DrawSpt.Panel1
            // 
            this.DrawSpt.Panel1.Controls.Add(this.DrawPlane);
            // 
            // DrawSpt.Panel2
            // 
            this.DrawSpt.Panel2.Controls.Add(this.SettingsPlane);
            this.DrawSpt.Size = new System.Drawing.Size(467, 531);
            this.DrawSpt.SplitterDistance = 419;
            this.DrawSpt.TabIndex = 1;
            // 
            // DrawPlane
            // 
            this.DrawPlane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawPlane.Location = new System.Drawing.Point(0, 0);
            this.DrawPlane.Name = "DrawPlane";
            this.DrawPlane.Size = new System.Drawing.Size(465, 417);
            this.DrawPlane.TabIndex = 0;
            this.DrawPlane.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPlane_Paint);
            this.DrawPlane.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawPlane_MouseDown);
            this.DrawPlane.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawPlane_MouseMove);
            this.DrawPlane.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawPlane_MouseUp);
            // 
            // SettingsPlane
            // 
            this.SettingsPlane.Controls.Add(this.label3);
            this.SettingsPlane.Controls.Add(this.EdgeBox);
            this.SettingsPlane.Controls.Add(this.statusStrip1);
            this.SettingsPlane.Controls.Add(this.CNoneBTn);
            this.SettingsPlane.Controls.Add(this.CAreBtn);
            this.SettingsPlane.Controls.Add(this.CDisBtn);
            this.SettingsPlane.Controls.Add(this.CNodeBtn);
            this.SettingsPlane.Controls.Add(this.UnitSizeNUD);
            this.SettingsPlane.Controls.Add(this.label5);
            this.SettingsPlane.Controls.Add(this.RangeNUD);
            this.SettingsPlane.Controls.Add(this.label4);
            this.SettingsPlane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsPlane.Location = new System.Drawing.Point(0, 0);
            this.SettingsPlane.Name = "SettingsPlane";
            this.SettingsPlane.Size = new System.Drawing.Size(465, 106);
            this.SettingsPlane.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "Edge:";
            // 
            // EdgeBox
            // 
            this.EdgeBox.Location = new System.Drawing.Point(49, 52);
            this.EdgeBox.Name = "EdgeBox";
            this.EdgeBox.Size = new System.Drawing.Size(413, 21);
            this.EdgeBox.TabIndex = 11;
            this.EdgeBox.TextChanged += new System.EventHandler(this.EdgeBox_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InformationLabel,
            this.ErrorLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 84);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(465, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // InformationLabel
            // 
            this.InformationLabel.Name = "InformationLabel";
            this.InformationLabel.Size = new System.Drawing.Size(31, 17);
            this.InformationLabel.Text = "Info";
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(38, 17);
            this.ErrorLabel.Text = "Error";
            // 
            // CNoneBTn
            // 
            this.CNoneBTn.AutoSize = true;
            this.CNoneBTn.Checked = true;
            this.CNoneBTn.Location = new System.Drawing.Point(157, 30);
            this.CNoneBTn.Name = "CNoneBTn";
            this.CNoneBTn.Size = new System.Drawing.Size(65, 16);
            this.CNoneBTn.TabIndex = 9;
            this.CNoneBTn.TabStop = true;
            this.CNoneBTn.Tag = "3";
            this.CNoneBTn.Text = "None/Op";
            this.CNoneBTn.UseVisualStyleBackColor = true;
            this.CNoneBTn.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // CAreBtn
            // 
            this.CAreBtn.AutoSize = true;
            this.CAreBtn.Location = new System.Drawing.Point(110, 30);
            this.CAreBtn.Name = "CAreBtn";
            this.CAreBtn.Size = new System.Drawing.Size(41, 16);
            this.CAreBtn.TabIndex = 7;
            this.CAreBtn.TabStop = true;
            this.CAreBtn.Tag = "2";
            this.CAreBtn.Text = "Are";
            this.CAreBtn.UseVisualStyleBackColor = true;
            this.CAreBtn.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // CDisBtn
            // 
            this.CDisBtn.AutoSize = true;
            this.CDisBtn.Location = new System.Drawing.Point(63, 30);
            this.CDisBtn.Name = "CDisBtn";
            this.CDisBtn.Size = new System.Drawing.Size(41, 16);
            this.CDisBtn.TabIndex = 6;
            this.CDisBtn.TabStop = true;
            this.CDisBtn.Tag = "1";
            this.CDisBtn.Text = "Dis";
            this.CDisBtn.UseVisualStyleBackColor = true;
            this.CDisBtn.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // CNodeBtn
            // 
            this.CNodeBtn.AutoSize = true;
            this.CNodeBtn.Location = new System.Drawing.Point(10, 30);
            this.CNodeBtn.Name = "CNodeBtn";
            this.CNodeBtn.Size = new System.Drawing.Size(47, 16);
            this.CNodeBtn.TabIndex = 5;
            this.CNodeBtn.TabStop = true;
            this.CNodeBtn.Tag = "0";
            this.CNodeBtn.Text = "Node";
            this.CNodeBtn.UseVisualStyleBackColor = true;
            this.CNodeBtn.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // UnitSizeNUD
            // 
            this.UnitSizeNUD.Location = new System.Drawing.Point(282, 3);
            this.UnitSizeNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.UnitSizeNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UnitSizeNUD.Name = "UnitSizeNUD";
            this.UnitSizeNUD.Size = new System.Drawing.Size(120, 21);
            this.UnitSizeNUD.TabIndex = 4;
            this.UnitSizeNUD.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.UnitSizeNUD.ValueChanged += new System.EventHandler(this.UnitSizeNUD_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(211, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "Unit Size:";
            // 
            // RangeNUD
            // 
            this.RangeNUD.Location = new System.Drawing.Point(85, 3);
            this.RangeNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.RangeNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RangeNUD.Name = "RangeNUD";
            this.RangeNUD.Size = new System.Drawing.Size(120, 21);
            this.RangeNUD.TabIndex = 2;
            this.RangeNUD.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.RangeNUD.ValueChanged += new System.EventHandler(this.RangeNUD_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Range(+/-):";
            // 
            // CodeProSpt
            // 
            this.CodeProSpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeProSpt.Location = new System.Drawing.Point(0, 0);
            this.CodeProSpt.Name = "CodeProSpt";
            this.CodeProSpt.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // CodeProSpt.Panel1
            // 
            this.CodeProSpt.Panel1.Controls.Add(this.CodeViewer);
            // 
            // CodeProSpt.Panel2
            // 
            this.CodeProSpt.Panel2.Controls.Add(this.PropertyPanel);
            this.CodeProSpt.Size = new System.Drawing.Size(307, 531);
            this.CodeProSpt.SplitterDistance = 450;
            this.CodeProSpt.TabIndex = 1;
            // 
            // CodeViewer
            // 
            this.CodeViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeViewer.Location = new System.Drawing.Point(0, 0);
            this.CodeViewer.Name = "CodeViewer";
            this.CodeViewer.ReadOnly = true;
            this.CodeViewer.Size = new System.Drawing.Size(307, 450);
            this.CodeViewer.TabIndex = 0;
            this.CodeViewer.Text = "";
            this.CodeViewer.WordWrap = false;
            // 
            // PropertyPanel
            // 
            this.PropertyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PropertyPanel.Controls.Add(this.label2);
            this.PropertyPanel.Controls.Add(this.NamespaceTextBox);
            this.PropertyPanel.Controls.Add(this.TypeNameTextBox);
            this.PropertyPanel.Controls.Add(this.label1);
            this.PropertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyPanel.Location = new System.Drawing.Point(0, 0);
            this.PropertyPanel.Name = "PropertyPanel";
            this.PropertyPanel.Size = new System.Drawing.Size(307, 77);
            this.PropertyPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Namespace:";
            // 
            // NamespaceTextBox
            // 
            this.NamespaceTextBox.Location = new System.Drawing.Point(74, 29);
            this.NamespaceTextBox.Name = "NamespaceTextBox";
            this.NamespaceTextBox.Size = new System.Drawing.Size(195, 21);
            this.NamespaceTextBox.TabIndex = 2;
            this.NamespaceTextBox.Text = "JellyFactory";
            this.NamespaceTextBox.TextChanged += new System.EventHandler(this.NamespaceTextBox_TextChanged);
            // 
            // TypeNameTextBox
            // 
            this.TypeNameTextBox.Location = new System.Drawing.Point(74, 2);
            this.TypeNameTextBox.Name = "TypeNameTextBox";
            this.TypeNameTextBox.Size = new System.Drawing.Size(195, 21);
            this.TypeNameTextBox.TabIndex = 1;
            this.TypeNameTextBox.Text = "Default";
            this.TypeNameTextBox.TextChanged += new System.EventHandler(this.TypeNameTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type Name:";
            // 
            // MainMenu
            // 
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rebuildNewToolStripMenuItem,
            this.copyCodeToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(289, 25);
            this.MainMenu.TabIndex = 2;
            this.MainMenu.Text = "menuStrip1";
            // 
            // rebuildNewToolStripMenuItem
            // 
            this.rebuildNewToolStripMenuItem.Name = "rebuildNewToolStripMenuItem";
            this.rebuildNewToolStripMenuItem.Size = new System.Drawing.Size(95, 21);
            this.rebuildNewToolStripMenuItem.Text = "Rebuild/New";
            this.rebuildNewToolStripMenuItem.Click += new System.EventHandler(this.rebuildNewToolStripMenuItem_Click);
            // 
            // copyCodeToolStripMenuItem
            // 
            this.copyCodeToolStripMenuItem.Name = "copyCodeToolStripMenuItem";
            this.copyCodeToolStripMenuItem.Size = new System.Drawing.Size(85, 21);
            this.copyCodeToolStripMenuItem.Text = "Copy Code";
            this.copyCodeToolStripMenuItem.Click += new System.EventHandler(this.copyCodeToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(101, 21);
            this.exportToolStripMenuItem.Text = "Export To *.cs";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.MainUI, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.MainMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 562);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "C# files|*.cs";
            // 
            // JellyCodeBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "JellyCodeBuilder";
            this.Text = "Jelly Code Builder";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.JellyCodeGenerater_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.JellyCodeGenerater_KeyDown);
            this.MainUI.Panel1.ResumeLayout(false);
            this.MainUI.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainUI)).EndInit();
            this.MainUI.ResumeLayout(false);
            this.DrawSpt.Panel1.ResumeLayout(false);
            this.DrawSpt.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DrawSpt)).EndInit();
            this.DrawSpt.ResumeLayout(false);
            this.SettingsPlane.ResumeLayout(false);
            this.SettingsPlane.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UnitSizeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RangeNUD)).EndInit();
            this.CodeProSpt.Panel1.ResumeLayout(false);
            this.CodeProSpt.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CodeProSpt)).EndInit();
            this.CodeProSpt.ResumeLayout(false);
            this.PropertyPanel.ResumeLayout(false);
            this.PropertyPanel.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainUI;
        private System.Windows.Forms.RichTextBox CodeViewer;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem rebuildNewToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer CodeProSpt;
        private System.Windows.Forms.Panel PropertyPanel;
        private System.Windows.Forms.TextBox TypeNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NamespaceTextBox;
        private System.Windows.Forms.Panel DrawPlane;
        private System.Windows.Forms.ToolStripMenuItem copyCodeToolStripMenuItem;
        private System.Windows.Forms.SplitContainer DrawSpt;
        private System.Windows.Forms.Panel SettingsPlane;
        private System.Windows.Forms.NumericUpDown UnitSizeNUD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown RangeNUD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton CAreBtn;
        private System.Windows.Forms.RadioButton CDisBtn;
        private System.Windows.Forms.RadioButton CNodeBtn;
        private System.Windows.Forms.RadioButton CNoneBTn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel InformationLabel;
        private System.Windows.Forms.ToolStripStatusLabel ErrorLabel;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox EdgeBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

