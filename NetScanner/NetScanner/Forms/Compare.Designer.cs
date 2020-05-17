namespace NetScanner.Forms
{
    partial class Compare
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_ss1 = new System.Windows.Forms.Label();
            this.lbl_ss2 = new System.Windows.Forms.Label();
            this.compareGrid1 = new System.Windows.Forms.DataGridView();
            this.compareGrid2 = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compareGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.compareGrid2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1235, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 673);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1235, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_ss1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_ss2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.compareGrid1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.compareGrid2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1235, 649);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lbl_ss1
            // 
            this.lbl_ss1.AutoSize = true;
            this.lbl_ss1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ss1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ss1.Location = new System.Drawing.Point(3, 0);
            this.lbl_ss1.Name = "lbl_ss1";
            this.lbl_ss1.Size = new System.Drawing.Size(611, 64);
            this.lbl_ss1.TabIndex = 0;
            this.lbl_ss1.Text = "Snapshot File 1";
            this.lbl_ss1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ss2
            // 
            this.lbl_ss2.AutoSize = true;
            this.lbl_ss2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ss2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ss2.Location = new System.Drawing.Point(620, 0);
            this.lbl_ss2.Name = "lbl_ss2";
            this.lbl_ss2.Size = new System.Drawing.Size(612, 64);
            this.lbl_ss2.TabIndex = 1;
            this.lbl_ss2.Text = "Snapshot File 2";
            this.lbl_ss2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // compareGrid1
            // 
            this.compareGrid1.AllowUserToAddRows = false;
            this.compareGrid1.AllowUserToDeleteRows = false;
            this.compareGrid1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.compareGrid1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.compareGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.compareGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compareGrid1.Location = new System.Drawing.Point(3, 67);
            this.compareGrid1.MultiSelect = false;
            this.compareGrid1.Name = "compareGrid1";
            this.compareGrid1.ReadOnly = true;
            this.compareGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.compareGrid1.ShowEditingIcon = false;
            this.compareGrid1.Size = new System.Drawing.Size(611, 545);
            this.compareGrid1.TabIndex = 2;
            this.compareGrid1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.compareGrid1_CellClick);
            // 
            // compareGrid2
            // 
            this.compareGrid2.AllowUserToAddRows = false;
            this.compareGrid2.AllowUserToDeleteRows = false;
            this.compareGrid2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.compareGrid2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.compareGrid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.compareGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compareGrid2.Location = new System.Drawing.Point(620, 67);
            this.compareGrid2.MultiSelect = false;
            this.compareGrid2.Name = "compareGrid2";
            this.compareGrid2.ReadOnly = true;
            this.compareGrid2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.compareGrid2.ShowEditingIcon = false;
            this.compareGrid2.Size = new System.Drawing.Size(612, 545);
            this.compareGrid2.TabIndex = 3;
            this.compareGrid2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.compareGrid2_CellClick);
            // 
            // Compare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 695);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Compare";
            this.Text = "Compare";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Compare_FormClosed);
            this.Load += new System.EventHandler(this.Compare_Load);
            this.Shown += new System.EventHandler(this.Compare_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compareGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.compareGrid2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_ss1;
        private System.Windows.Forms.Label lbl_ss2;
        private System.Windows.Forms.DataGridView compareGrid1;
        private System.Windows.Forms.DataGridView compareGrid2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}