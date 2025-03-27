namespace HijackGen.GUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnl = new System.Windows.Forms.Panel();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.clmOrdinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmHasForward = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmForwardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbPath = new System.Windows.Forms.Label();
            this.pnlPath = new System.Windows.Forms.Panel();
            this.btSelect = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.pnlOperation = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btGenDef = new System.Windows.Forms.Button();
            this.btGenH = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.pnl.SuspendLayout();
            this.table.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.pnlPath.SuspendLayout();
            this.pnlOperation.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
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
            // lbInfo
            // 
            this.lbInfo.Name = "lbInfo";
            resources.ApplyResources(this.lbInfo, "lbInfo");
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.table);
            resources.ApplyResources(this.pnl, "pnl");
            this.pnl.Name = "pnl";
            // 
            // table
            // 
            resources.ApplyResources(this.table, "table");
            this.table.Controls.Add(this.dataGrid, 0, 2);
            this.table.Controls.Add(this.lbPath, 0, 0);
            this.table.Controls.Add(this.pnlPath, 0, 1);
            this.table.Controls.Add(this.pnlOperation, 0, 3);
            this.table.Name = "table";
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGrid.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmOrdinal,
            this.clmAddress,
            this.clmName,
            this.clmHasForward,
            this.clmForwardName});
            resources.ApplyResources(this.dataGrid, "dataGrid");
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
            dataGridViewCellStyle1.Format = "X";
            this.clmAddress.DefaultCellStyle = dataGridViewCellStyle1;
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
            // lbPath
            // 
            resources.ApplyResources(this.lbPath, "lbPath");
            this.lbPath.Name = "lbPath";
            // 
            // pnlPath
            // 
            this.pnlPath.Controls.Add(this.btSelect);
            this.pnlPath.Controls.Add(this.txtPath);
            resources.ApplyResources(this.pnlPath, "pnlPath");
            this.pnlPath.Name = "pnlPath";
            // 
            // btSelect
            // 
            resources.ApplyResources(this.btSelect, "btSelect");
            this.btSelect.Name = "btSelect";
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // txtPath
            // 
            resources.ApplyResources(this.txtPath, "txtPath");
            this.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtPath.Name = "txtPath";
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // pnlOperation
            // 
            this.pnlOperation.Controls.Add(this.txtSearch);
            this.pnlOperation.Controls.Add(this.btGenDef);
            this.pnlOperation.Controls.Add(this.btGenH);
            resources.ApplyResources(this.pnlOperation, "pnlOperation");
            this.pnlOperation.Name = "pnlOperation";
            // 
            // txtSearch
            // 
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btGenDef
            // 
            resources.ApplyResources(this.btGenDef, "btGenDef");
            this.btGenDef.Name = "btGenDef";
            this.btGenDef.UseVisualStyleBackColor = true;
            this.btGenDef.Click += new System.EventHandler(this.btGenDef_Click);
            // 
            // btGenH
            // 
            resources.ApplyResources(this.btGenH, "btGenH");
            this.btGenH.Name = "btGenH";
            this.btGenH.UseVisualStyleBackColor = true;
            this.btGenH.Click += new System.EventHandler(this.btGenH_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pnl.ResumeLayout(false);
            this.table.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.pnlPath.ResumeLayout(false);
            this.pnlPath.PerformLayout();
            this.pnlOperation.ResumeLayout(false);
            this.pnlOperation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.Panel pnlPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOrdinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHasForward;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmForwardName;
        private System.Windows.Forms.Panel pnlOperation;
        private System.Windows.Forms.Button btGenDef;
        private System.Windows.Forms.Button btGenH;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.ToolStripStatusLabel lbInfo;
        private System.Windows.Forms.TextBox txtSearch;
    }
}