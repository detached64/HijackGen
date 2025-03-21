namespace HijackGen.GUI
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btSelect = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.clmOrdinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmHasForward = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmForwardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbPath = new System.Windows.Forms.Label();
            this.btGenH = new System.Windows.Forms.Button();
            this.btGenDef = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lbSearch = new System.Windows.Forms.Label();
            this.lbInfo = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            resources.ApplyResources(this.txtPath, "txtPath");
            this.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtPath.Name = "txtPath";
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // btSelect
            // 
            resources.ApplyResources(this.btSelect, "btSelect");
            this.btSelect.Name = "btSelect";
            this.btSelect.TabStop = false;
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGrid, "dataGrid");
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmOrdinal,
            this.clmAddress,
            this.clmName,
            this.clmHasForward,
            this.clmForwardName});
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.RowTemplate.Height = 30;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            // 
            // clmOrdinal
            // 
            this.clmOrdinal.DataPropertyName = "Ordinal";
            resources.ApplyResources(this.clmOrdinal, "clmOrdinal");
            this.clmOrdinal.Name = "clmOrdinal";
            this.clmOrdinal.ReadOnly = true;
            // 
            // clmAddress
            // 
            this.clmAddress.DataPropertyName = "Address";
            dataGridViewCellStyle2.Format = "X";
            this.clmAddress.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.clmAddress, "clmAddress");
            this.clmAddress.Name = "clmAddress";
            this.clmAddress.ReadOnly = true;
            // 
            // clmName
            // 
            this.clmName.DataPropertyName = "Name";
            resources.ApplyResources(this.clmName, "clmName");
            this.clmName.Name = "clmName";
            this.clmName.ReadOnly = true;
            // 
            // clmHasForward
            // 
            this.clmHasForward.DataPropertyName = "HasForward";
            resources.ApplyResources(this.clmHasForward, "clmHasForward");
            this.clmHasForward.Name = "clmHasForward";
            this.clmHasForward.ReadOnly = true;
            // 
            // clmForwardName
            // 
            this.clmForwardName.DataPropertyName = "ForwardName";
            resources.ApplyResources(this.clmForwardName, "clmForwardName");
            this.clmForwardName.Name = "clmForwardName";
            this.clmForwardName.ReadOnly = true;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatus,
            this.lbInfo});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // lbStatus
            // 
            this.lbStatus.Name = "lbStatus";
            resources.ApplyResources(this.lbStatus, "lbStatus");
            // 
            // lbPath
            // 
            resources.ApplyResources(this.lbPath, "lbPath");
            this.lbPath.Name = "lbPath";
            // 
            // btGenH
            // 
            resources.ApplyResources(this.btGenH, "btGenH");
            this.btGenH.Name = "btGenH";
            this.btGenH.UseVisualStyleBackColor = true;
            this.btGenH.Click += new System.EventHandler(this.btGenH_Click);
            // 
            // btGenDef
            // 
            resources.ApplyResources(this.btGenDef, "btGenDef");
            this.btGenDef.Name = "btGenDef";
            this.btGenDef.UseVisualStyleBackColor = true;
            this.btGenDef.Click += new System.EventHandler(this.btGenDef_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // pnlControl
            // 
            resources.ApplyResources(this.pnlControl, "pnlControl");
            this.pnlControl.Controls.Add(this.txtSearch);
            this.pnlControl.Controls.Add(this.lbSearch);
            this.pnlControl.Controls.Add(this.btGenDef);
            this.pnlControl.Controls.Add(this.dataGrid);
            this.pnlControl.Controls.Add(this.btGenH);
            this.pnlControl.Name = "pnlControl";
            // 
            // txtSearch
            // 
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lbSearch
            // 
            resources.ApplyResources(this.lbSearch, "lbSearch");
            this.lbSearch.Name = "lbSearch";
            this.lbSearch.SizeChanged += new System.EventHandler(this.lbSearch_SizeChanged);
            // 
            // lbInfo
            // 
            this.lbInfo.Name = "lbInfo";
            resources.ApplyResources(this.lbInfo, "lbInfo");
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbPath);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.btSelect);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.pnlControl);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.Button btGenH;
        private System.Windows.Forms.Button btGenDef;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lbSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOrdinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHasForward;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmForwardName;
        private System.Windows.Forms.ToolStripStatusLabel lbInfo;
    }
}

