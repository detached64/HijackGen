namespace HijackGen.GUI
{
    partial class DefOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefOptions));
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPath = new System.Windows.Forms.Panel();
            this.btSelect = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lbPath = new System.Windows.Forms.Label();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.btGen = new System.Windows.Forms.Button();
            this.table.SuspendLayout();
            this.pnlPath.SuspendLayout();
            this.pnlOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // table
            // 
            resources.ApplyResources(this.table, "table");
            this.table.Controls.Add(this.pnlPath, 0, 1);
            this.table.Controls.Add(this.lbPath, 0, 0);
            this.table.Controls.Add(this.pnlOptions, 0, 2);
            this.table.Name = "table";
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
            this.txtPath.Name = "txtPath";
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // lbPath
            // 
            resources.ApplyResources(this.lbPath, "lbPath");
            this.lbPath.Name = "lbPath";
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.btGen);
            resources.ApplyResources(this.pnlOptions, "pnlOptions");
            this.pnlOptions.Name = "pnlOptions";
            // 
            // btGen
            // 
            resources.ApplyResources(this.btGen, "btGen");
            this.btGen.Name = "btGen";
            this.btGen.UseVisualStyleBackColor = true;
            this.btGen.Click += new System.EventHandler(this.btGen_Click);
            // 
            // DefOptions
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.table);
            this.Name = "DefOptions";
            this.Load += new System.EventHandler(this.DefOptions_Load);
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            this.pnlPath.ResumeLayout(false);
            this.pnlPath.PerformLayout();
            this.pnlOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.Panel pnlPath;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.Button btGen;
    }
}