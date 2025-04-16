using System;
using System.IO;
using System.Windows.Forms;

namespace HijackGen.GUI.Legacy
{
    public partial class HOptions : OptionsTemplate
    {
        public HOptions()
        {
            InitializeComponent();
            SavePath = Path.Combine(Settings.SaveDir, Path.GetFileNameWithoutExtension(Settings.DllPath) + ".h");
        }

        private void HOptions_Load(object sender, EventArgs e)
        {
            this.txtPath.Text = SavePath;
            this.rbtSystem.Checked = Settings.IsSystemDll;
            this.rbtCustom.Checked = !Settings.IsSystemDll;
            this.rbtX86.Checked = !Settings.IsX64;
            this.rbtX64.Checked = Settings.IsX64;
            this.chkbxGenDefX64.Checked = Settings.GenDefX64;
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            SavePath = this.txtPath.Text;
            Settings.SaveDir = Path.GetDirectoryName(this.txtPath.Text);
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Header Files (*.h)|*.h|All Files (*.*)|*.*";
                sfd.FileName = Path.GetFileName(SavePath);
                sfd.InitialDirectory = Path.GetDirectoryName(SavePath);
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.txtPath.Text = sfd.FileName;
                }
            }
        }

        private void btGen_Click(object sender, EventArgs e)
        {
            try
            {
                using (HijackGen.Legacy.HGenerator gen = new HijackGen.Legacy.HGenerator(Path.GetFileNameWithoutExtension(Settings.DllPath), MainForm.Infos, Settings.IsSystemDll, Settings.IsX64, Settings.GenDefX64))
                {
                    if (Settings.IsSystemDll && MainForm.ContainsSpecialChars)
                    {
                        if (MessageBox.Show(Message.msgContainsInvalidChars, Message.msgWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                        {
                            Result = OperationResult.Canceled;
                            return;
                        }
                    }
                    foreach (var content in gen.Generate())
                    {
                        switch (content.Key)
                        {
                            case HijackGen.Legacy.FileType.Header:
                                File.WriteAllText(SavePath, content.Value);
                                break;
                            case HijackGen.Legacy.FileType.Def:
                                File.WriteAllText(Path.ChangeExtension(SavePath, "def"), content.Value);
                                break;
                        }
                    }
                    Result = OperationResult.Success;
                }
            }
            catch (Exception ex)
            {
                Result = OperationResult.Failed;
                Exception = ex;
            }
            this.Close();
        }

        private void rbtX86_CheckedChanged(object sender, EventArgs e)
        {
            this.chkbxGenDefX64.Enabled = Settings.IsX64 = !this.rbtX86.Checked;
        }

        private void rbtX64_CheckedChanged(object sender, EventArgs e)
        {
            this.chkbxGenDefX64.Enabled = Settings.IsX64 = this.rbtX64.Checked;
        }

        private void chkbxGenDefX64_CheckedChanged(object sender, EventArgs e)
        {
            Settings.GenDefX64 = this.chkbxGenDefX64.Checked;
        }

        private void rbtSystem_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlArchitecture.Enabled = this.pnlExtraOptions.Enabled = Settings.IsSystemDll = this.rbtSystem.Checked;
        }

        private void rbtCustom_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlArchitecture.Enabled = this.pnlExtraOptions.Enabled = Settings.IsSystemDll = !this.rbtCustom.Checked;
        }
    }
}
