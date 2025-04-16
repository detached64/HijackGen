namespace HijackGen.GUI.Legacy
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.LbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnl = new System.Windows.Forms.Panel();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.Data = new System.Windows.Forms.DataGridView();
            this.LbPath = new System.Windows.Forms.Label();
            this.pnlPath = new System.Windows.Forms.Panel();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.TextPath = new System.Windows.Forms.TextBox();
            this.pnlOperation = new System.Windows.Forms.Panel();
            this.pnlGen = new System.Windows.Forms.Panel();
            this.BtnGenDef = new System.Windows.Forms.Button();
            this.BtnGenH = new System.Windows.Forms.Button();
            this.TextSearch = new System.Windows.Forms.TextBox();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.pnl.SuspendLayout();
            this.table.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Data)).BeginInit();
            this.pnlPath.SuspendLayout();
            this.pnlOperation.SuspendLayout();
            this.pnlGen.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAbout});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // MenuAbout
            // 
            this.MenuAbout.Name = "MenuAbout";
            resources.ApplyResources(this.MenuAbout, "MenuAbout");
            this.MenuAbout.Click += new System.EventHandler(this.MenuABout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LbStatus,
            this.lbInfo});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // LbStatus
            // 
            this.LbStatus.Name = "LbStatus";
            resources.ApplyResources(this.LbStatus, "LbStatus");
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
            this.table.Controls.Add(this.Data, 0, 2);
            this.table.Controls.Add(this.LbPath, 0, 0);
            this.table.Controls.Add(this.pnlPath, 0, 1);
            this.table.Controls.Add(this.pnlOperation, 0, 3);
            this.table.Name = "table";
            // 
            // Data
            // 
            this.Data.AllowUserToAddRows = false;
            this.Data.AllowUserToDeleteRows = false;
            this.Data.AllowUserToOrderColumns = true;
            this.Data.AllowUserToResizeRows = false;
            this.Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.Data.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.Data, "Data");
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            this.Data.RowHeadersVisible = false;
            this.Data.RowTemplate.Height = 30;
            this.Data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            // 
            // LbPath
            // 
            resources.ApplyResources(this.LbPath, "LbPath");
            this.LbPath.Name = "LbPath";
            // 
            // pnlPath
            // 
            this.pnlPath.Controls.Add(this.BtnSelect);
            this.pnlPath.Controls.Add(this.TextPath);
            resources.ApplyResources(this.pnlPath, "pnlPath");
            this.pnlPath.Name = "pnlPath";
            // 
            // BtnSelect
            // 
            resources.ApplyResources(this.BtnSelect, "BtnSelect");
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // TextPath
            // 
            resources.ApplyResources(this.TextPath, "TextPath");
            this.TextPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TextPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.TextPath.Name = "TextPath";
            this.TextPath.TextChanged += new System.EventHandler(this.TextPath_TextChanged);
            // 
            // pnlOperation
            // 
            this.pnlOperation.Controls.Add(this.pnlGen);
            this.pnlOperation.Controls.Add(this.TextSearch);
            resources.ApplyResources(this.pnlOperation, "pnlOperation");
            this.pnlOperation.Name = "pnlOperation";
            // 
            // pnlGen
            // 
            resources.ApplyResources(this.pnlGen, "pnlGen");
            this.pnlGen.Controls.Add(this.BtnGenDef);
            this.pnlGen.Controls.Add(this.BtnGenH);
            this.pnlGen.Name = "pnlGen";
            // 
            // BtnGenDef
            // 
            resources.ApplyResources(this.BtnGenDef, "BtnGenDef");
            this.BtnGenDef.Name = "BtnGenDef";
            this.BtnGenDef.UseVisualStyleBackColor = true;
            this.BtnGenDef.Click += new System.EventHandler(this.BtnGenDef_Click);
            // 
            // BtnGenH
            // 
            resources.ApplyResources(this.BtnGenH, "BtnGenH");
            this.BtnGenH.Name = "BtnGenH";
            this.BtnGenH.UseVisualStyleBackColor = true;
            this.BtnGenH.Click += new System.EventHandler(this.BtnGenH_Click);
            // 
            // TextSearch
            // 
            resources.ApplyResources(this.TextSearch, "TextSearch");
            this.TextSearch.Name = "TextSearch";
            this.TextSearch.TextChanged += new System.EventHandler(this.TextSearch_TextChanged);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.Data)).EndInit();
            this.pnlPath.ResumeLayout(false);
            this.pnlPath.PerformLayout();
            this.pnlOperation.ResumeLayout(false);
            this.pnlOperation.PerformLayout();
            this.pnlGen.ResumeLayout(false);
            this.pnlGen.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.Label LbPath;
        private System.Windows.Forms.Panel pnlPath;
        private System.Windows.Forms.TextBox TextPath;
        private System.Windows.Forms.Button BtnSelect;
        private System.Windows.Forms.DataGridView Data;
        private System.Windows.Forms.Panel pnlOperation;
        private System.Windows.Forms.Button BtnGenDef;
        private System.Windows.Forms.Button BtnGenH;
        private System.Windows.Forms.ToolStripMenuItem MenuAbout;
        private System.Windows.Forms.ToolStripStatusLabel LbStatus;
        private System.Windows.Forms.ToolStripStatusLabel lbInfo;
        private System.Windows.Forms.TextBox TextSearch;
        private System.Windows.Forms.Panel pnlGen;
    }
}