namespace MyCAD
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出为CSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataCompressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.douglasPeukerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据加密ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dis_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rel_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dx_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dm_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.add_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.del_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.se_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导线点符号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.教堂符号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.open_bt = new System.Windows.Forms.ToolStripButton();
            this.save_bt = new System.Windows.Forms.ToolStripButton();
            this.clear_bt = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.globle_bt = new System.Windows.Forms.ToolStripButton();
            this.enlarge_bt = new System.Windows.Forms.ToolStripButton();
            this.shrink_bt = new System.Windows.Forms.ToolStripButton();
            this.move_bt = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ImportFDL = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.导线点符号ToolStripMenuItem,
            this.教堂符号ToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportImageToolStripMenuItem,
            this.导出为CSVToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            resources.ApplyResources(this.importToolStripMenuItem, "importToolStripMenuItem");
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportImageToolStripMenuItem
            // 
            this.exportImageToolStripMenuItem.Name = "exportImageToolStripMenuItem";
            resources.ApplyResources(this.exportImageToolStripMenuItem, "exportImageToolStripMenuItem");
            this.exportImageToolStripMenuItem.Click += new System.EventHandler(this.exportImageToolStripMenuItem_Click);
            // 
            // 导出为CSVToolStripMenuItem
            // 
            this.导出为CSVToolStripMenuItem.Name = "导出为CSVToolStripMenuItem";
            resources.ApplyResources(this.导出为CSVToolStripMenuItem, "导出为CSVToolStripMenuItem");
            this.导出为CSVToolStripMenuItem.Click += new System.EventHandler(this.导出为CSVToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataCompressionToolStripMenuItem,
            this.数据加密ToolStripMenuItem,
            this.dis_ToolStripMenuItem,
            this.rel_ToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // dataCompressionToolStripMenuItem
            // 
            this.dataCompressionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.douglasPeukerToolStripMenuItem});
            this.dataCompressionToolStripMenuItem.Name = "dataCompressionToolStripMenuItem";
            resources.ApplyResources(this.dataCompressionToolStripMenuItem, "dataCompressionToolStripMenuItem");
            // 
            // douglasPeukerToolStripMenuItem
            // 
            this.douglasPeukerToolStripMenuItem.Name = "douglasPeukerToolStripMenuItem";
            resources.ApplyResources(this.douglasPeukerToolStripMenuItem, "douglasPeukerToolStripMenuItem");
            this.douglasPeukerToolStripMenuItem.Click += new System.EventHandler(this.douglasPeukerToolStripMenuItem_Click);
            // 
            // 数据加密ToolStripMenuItem
            // 
            this.数据加密ToolStripMenuItem.Name = "数据加密ToolStripMenuItem";
            resources.ApplyResources(this.数据加密ToolStripMenuItem, "数据加密ToolStripMenuItem");
            this.数据加密ToolStripMenuItem.Click += new System.EventHandler(this.数据加密ToolStripMenuItem_Click);
            // 
            // dis_ToolStripMenuItem
            // 
            this.dis_ToolStripMenuItem.Name = "dis_ToolStripMenuItem";
            resources.ApplyResources(this.dis_ToolStripMenuItem, "dis_ToolStripMenuItem");
            this.dis_ToolStripMenuItem.Click += new System.EventHandler(this.dis_ToolStripMenuItem_Click);
            // 
            // rel_ToolStripMenuItem
            // 
            this.rel_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem,
            this.dx_ToolStripMenuItem,
            this.dm_ToolStripMenuItem});
            this.rel_ToolStripMenuItem.Name = "rel_ToolStripMenuItem";
            resources.ApplyResources(this.rel_ToolStripMenuItem, "rel_ToolStripMenuItem");
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            resources.ApplyResources(this.ToolStripMenuItem, "ToolStripMenuItem");
            this.ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // dx_ToolStripMenuItem
            // 
            this.dx_ToolStripMenuItem.Name = "dx_ToolStripMenuItem";
            resources.ApplyResources(this.dx_ToolStripMenuItem, "dx_ToolStripMenuItem");
            this.dx_ToolStripMenuItem.Click += new System.EventHandler(this.dx_ToolStripMenuItem_Click);
            // 
            // dm_ToolStripMenuItem
            // 
            this.dm_ToolStripMenuItem.Name = "dm_ToolStripMenuItem";
            resources.ApplyResources(this.dm_ToolStripMenuItem, "dm_ToolStripMenuItem");
            this.dm_ToolStripMenuItem.Click += new System.EventHandler(this.dm_ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add_ToolStripMenuItem,
            this.del_ToolStripMenuItem,
            this.se_ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            resources.ApplyResources(this.编辑ToolStripMenuItem, "编辑ToolStripMenuItem");
            // 
            // add_ToolStripMenuItem
            // 
            this.add_ToolStripMenuItem.Name = "add_ToolStripMenuItem";
            resources.ApplyResources(this.add_ToolStripMenuItem, "add_ToolStripMenuItem");
            this.add_ToolStripMenuItem.Click += new System.EventHandler(this.add_ToolStripMenuItem_Click);
            // 
            // del_ToolStripMenuItem
            // 
            this.del_ToolStripMenuItem.Name = "del_ToolStripMenuItem";
            resources.ApplyResources(this.del_ToolStripMenuItem, "del_ToolStripMenuItem");
            this.del_ToolStripMenuItem.Click += new System.EventHandler(this.del_ToolStripMenuItem_Click);
            // 
            // se_ToolStripMenuItem
            // 
            this.se_ToolStripMenuItem.Name = "se_ToolStripMenuItem";
            resources.ApplyResources(this.se_ToolStripMenuItem, "se_ToolStripMenuItem");
            this.se_ToolStripMenuItem.Click += new System.EventHandler(this.se_ToolStripMenuItem_Click);
            // 
            // 导线点符号ToolStripMenuItem
            // 
            this.导线点符号ToolStripMenuItem.Name = "导线点符号ToolStripMenuItem";
            resources.ApplyResources(this.导线点符号ToolStripMenuItem, "导线点符号ToolStripMenuItem");
            this.导线点符号ToolStripMenuItem.Click += new System.EventHandler(this.导线点符号ToolStripMenuItem_Click);
            // 
            // 教堂符号ToolStripMenuItem
            // 
            this.教堂符号ToolStripMenuItem.Name = "教堂符号ToolStripMenuItem";
            resources.ApplyResources(this.教堂符号ToolStripMenuItem, "教堂符号ToolStripMenuItem");
            this.教堂符号ToolStripMenuItem.Click += new System.EventHandler(this.教堂符号ToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.open_bt,
            this.save_bt,
            this.clear_bt,
            this.toolStripSeparator1,
            this.globle_bt,
            this.enlarge_bt,
            this.shrink_bt,
            this.move_bt,
            this.toolStripSeparator2});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // open_bt
            // 
            this.open_bt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.open_bt, "open_bt");
            this.open_bt.Name = "open_bt";
            this.open_bt.Click += new System.EventHandler(this.open_bt_Click);
            // 
            // save_bt
            // 
            this.save_bt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.save_bt, "save_bt");
            this.save_bt.Name = "save_bt";
            this.save_bt.Click += new System.EventHandler(this.save_bt_Click);
            // 
            // clear_bt
            // 
            this.clear_bt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.clear_bt, "clear_bt");
            this.clear_bt.Name = "clear_bt";
            this.clear_bt.Click += new System.EventHandler(this.clear_bt_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // globle_bt
            // 
            this.globle_bt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.globle_bt, "globle_bt");
            this.globle_bt.Name = "globle_bt";
            this.globle_bt.Click += new System.EventHandler(this.globle_bt_Click);
            // 
            // enlarge_bt
            // 
            this.enlarge_bt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.enlarge_bt, "enlarge_bt");
            this.enlarge_bt.Name = "enlarge_bt";
            this.enlarge_bt.Click += new System.EventHandler(this.enlarge_bt_Click);
            // 
            // shrink_bt
            // 
            this.shrink_bt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.shrink_bt, "shrink_bt");
            this.shrink_bt.Name = "shrink_bt";
            this.shrink_bt.Click += new System.EventHandler(this.shrink_bt_Click);
            // 
            // move_bt
            // 
            this.move_bt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.move_bt, "move_bt");
            this.move_bt.Name = "move_bt";
            this.move_bt.Click += new System.EventHandler(this.move_bt_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // ImportFDL
            // 
            this.ImportFDL.FileName = "ImportFDL";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton open_bt;
        private System.Windows.Forms.ToolStripButton save_bt;
        private System.Windows.Forms.ToolStripButton clear_bt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton globle_bt;
        private System.Windows.Forms.ToolStripButton enlarge_bt;
        private System.Windows.Forms.ToolStripButton move_bt;
        private System.Windows.Forms.ToolStripButton shrink_bt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.OpenFileDialog ImportFDL;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataCompressionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem douglasPeukerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据加密ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dis_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出为CSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rel_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dx_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dm_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem add_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem del_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem se_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导线点符号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 教堂符号ToolStripMenuItem;
    }
}