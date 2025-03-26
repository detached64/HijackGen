namespace HijackGen.GUI
{
    partial class HOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HOptions));
            this.btSelect = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lbPath = new System.Windows.Forms.Label();
            this.rbtX86 = new System.Windows.Forms.RadioButton();
            this.rbtX64 = new System.Windows.Forms.RadioButton();
            this.chkbxGenDefX64 = new System.Windows.Forms.CheckBox();
            this.btGen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btSelect
            // 
            resources.ApplyResources(this.btSelect, "btSelect");
            this.btSelect.Name = "btSelect";
            this.btSelect.TabStop = false;
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
            // rbtX86
            // 
            resources.ApplyResources(this.rbtX86, "rbtX86");
            this.rbtX86.Name = "rbtX86";
            this.rbtX86.TabStop = true;
            this.rbtX86.UseVisualStyleBackColor = true;
            this.rbtX86.CheckedChanged += new System.EventHandler(this.rbtX86_CheckedChanged);
            // 
            // rbtX64
            // 
            resources.ApplyResources(this.rbtX64, "rbtX64");
            this.rbtX64.Name = "rbtX64";
            this.rbtX64.TabStop = true;
            this.rbtX64.UseVisualStyleBackColor = true;
            this.rbtX64.CheckedChanged += new System.EventHandler(this.rbtX64_CheckedChanged);
            // 
            // chkbxGenDefX64
            // 
            resources.ApplyResources(this.chkbxGenDefX64, "chkbxGenDefX64");
            this.chkbxGenDefX64.Name = "chkbxGenDefX64";
            this.chkbxGenDefX64.UseVisualStyleBackColor = true;
            this.chkbxGenDefX64.CheckedChanged += new System.EventHandler(this.chkbxGenDefX64_CheckedChanged);
            // 
            // btGen
            // 
            resources.ApplyResources(this.btGen, "btGen");
            this.btGen.Name = "btGen";
            this.btGen.UseVisualStyleBackColor = true;
            this.btGen.Click += new System.EventHandler(this.btGen_Click);
            // 
            // HOptions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.btGen);
            this.Controls.Add(this.chkbxGenDefX64);
            this.Controls.Add(this.rbtX64);
            this.Controls.Add(this.rbtX86);
            this.Controls.Add(this.btSelect);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lbPath);
            this.Name = "HOptions";
            this.Load += new System.EventHandler(this.HOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.RadioButton rbtX86;
        private System.Windows.Forms.RadioButton rbtX64;
        private System.Windows.Forms.CheckBox chkbxGenDefX64;
        private System.Windows.Forms.Button btGen;
    }
}