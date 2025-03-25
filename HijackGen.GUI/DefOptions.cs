using System;
using System.IO;
using System.Windows.Forms;

namespace HijackGen.GUI
{
    public partial class DefOptions : OptionsTemplate
    {
        public DefOptions()
        {
            InitializeComponent();
            SavePath = Path.Combine(Settings.SaveDir, Path.GetFileNameWithoutExtension(Settings.DllPath) + ".def");
        }

        private void DefOptions_Load(object sender, EventArgs e)
        {
            this.txtPath.Text = SavePath;
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
                sfd.Filter = "Def Files (*.def)|*.def|All Files (*.*)|*.*";
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
                using (DefGenerator gen = new DefGenerator(Path.GetFileNameWithoutExtension(Settings.DllPath), MainForm.Items))
                {
                    foreach (var content in gen.Generate())
                    {
                        switch (content.Key)
                        {
                            case FileProperty.Header:
                                throw new InvalidOperationException("Invalid .h file generated.");
                            case FileProperty.Def:
                                File.WriteAllText(SavePath, content.Value);
                                break;
                        }
                    }
                    Result = OperationResult.Success;
                    this.Close();
                }
            }
            catch
            {
                Result = OperationResult.Failed;
            }
        }
    }
}
