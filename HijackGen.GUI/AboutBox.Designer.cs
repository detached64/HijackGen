namespace HijackGen.GUI
{
    partial class AboutBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.lbName = new System.Windows.Forms.Label();
            this.linkSite = new System.Windows.Forms.LinkLabel();
            this.lbLicense = new System.Windows.Forms.Label();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.table.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbName
            // 
            resources.ApplyResources(this.lbName, "lbName");
            this.lbName.Name = "lbName";
            // 
            // linkSite
            // 
            resources.ApplyResources(this.linkSite, "linkSite");
            this.linkSite.Name = "linkSite";
            this.linkSite.TabStop = true;
            this.linkSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbSite_LinkClicked);
            // 
            // lbLicense
            // 
            resources.ApplyResources(this.lbLicense, "lbLicense");
            this.lbLicense.Name = "lbLicense";
            // 
            // table
            // 
            resources.ApplyResources(this.table, "table");
            this.table.Controls.Add(this.lbCopyright, 0, 2);
            this.table.Controls.Add(this.linkSite, 0, 3);
            this.table.Controls.Add(this.lbLicense, 0, 1);
            this.table.Controls.Add(this.lbName, 0, 0);
            this.table.Name = "table";
            // 
            // lbCopyright
            // 
            resources.ApplyResources(this.lbCopyright, "lbCopyright");
            this.lbCopyright.Name = "lbCopyright";
            // 
            // AboutBox
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.table);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Load += new System.EventHandler(this.AboutBox_Load);
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.LinkLabel linkSite;
        private System.Windows.Forms.Label lbLicense;
        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.Label lbCopyright;
    }
}