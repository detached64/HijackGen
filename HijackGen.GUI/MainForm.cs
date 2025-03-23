using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace HijackGen.GUI
{
    public partial class MainForm : Form
    {
        private DllParser Parser;
        private List<DataItem> Items;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            Type type = this.dataGrid.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dataGrid, true, null);

            this.pnlControl.Enabled = false;
            this.dataGrid.AutoGenerateColumns = false;
            this.lbStatus.Text = Message.msgReady;
            this.lbInfo.Alignment = ToolStripItemAlignment.Right;
            if (!string.IsNullOrWhiteSpace(Settings.Default.DllPath))
            {
                this.txtPath.Text = Settings.Default.DllPath;
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                txtPath.Text = files[0];
            }
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.DllPath = txtPath.Text;
            Settings.Default.Save();
            try
            {
                Parser = new DllParser(txtPath.Text);
                this.pnlControl.Enabled = true;
                Items = Parser.GetExportInfos();
                this.dataGrid.DataSource = Items;
                this.lbStatus.Text = string.Format(Message.msgFound, Items.Count);
            }
            catch (Exception ex)
            {
                this.lbStatus.Text = ex.Message;
                this.pnlControl.Enabled = false;
                Parser = null;
                Items = null;
                this.dataGrid.DataSource = null;
            }
            this.txtSearch.Text = string.Empty;
            UpdateInfo();
            this.dataGrid.ClearSelection();
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "DLL|*.dll|All Files|*.*";
                ofd.InitialDirectory = string.IsNullOrWhiteSpace(txtPath.Text) ? Environment.GetFolderPath(Environment.SpecialFolder.System) : Path.GetDirectoryName(txtPath.Text);
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = ofd.FileName;
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void btGenDef_Click(object sender, EventArgs e)
        {
            this.lbStatus.Text = Message.msgWorking;
            string dir = string.IsNullOrWhiteSpace(Settings.Default.SaveDir) ? Path.GetDirectoryName(txtPath.Text) : Settings.Default.SaveDir;
            string name = Path.GetFileNameWithoutExtension(txtPath.Text) + ".def";
            if (ChooseFilePath(dir, name, "Def File|*.def", out string fullPath))
            {
                Settings.Default.SaveDir = Path.GetDirectoryName(fullPath);
                Settings.Default.Save();
                using (Generator gen = new DefGenerator(Path.GetFileNameWithoutExtension(txtPath.Text), Items))
                {
                    File.WriteAllText(fullPath, gen.Generate());
                }
                this.lbStatus.Text = string.Format(Message.msgSaveSuccess, ".def", fullPath);
            }
            else
            {
                this.lbStatus.Text = Message.msgCanceled;
            }
        }

        private void btGenH_Click(object sender, EventArgs e)
        {
            this.lbStatus.Text = Message.msgWorking;
            string dir = string.IsNullOrWhiteSpace(Settings.Default.SaveDir) ? Path.GetDirectoryName(txtPath.Text) : Settings.Default.SaveDir;
            string name = Path.GetFileNameWithoutExtension(txtPath.Text) + ".h";
            if (ChooseFilePath(dir, name, "C Header|*.h", out string fullPath))
            {
                Settings.Default.SaveDir = Path.GetDirectoryName(fullPath);
                Settings.Default.Save();
                using (Generator gen = new HGenerator(Path.GetFileNameWithoutExtension(txtPath.Text), Items))
                {
                    File.WriteAllText(fullPath, gen.Generate());
                }
                this.lbStatus.Text = string.Format(Message.msgSaveSuccess, ".h", fullPath);
            }
            else
            {
                this.lbStatus.Text = Message.msgCanceled;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                this.dataGrid.DataSource = Items.FindAll(x => x.Ordinal.ToString().IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    x.HasForward.ToString().IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    x.HasForward && x.ForwardName.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            else
            {
                this.dataGrid.DataSource = Items;
            }
            this.dataGrid.ClearSelection();
        }

        private void lbSearch_SizeChanged(object sender, EventArgs e)
        {
            this.txtSearch.Location = new Point(this.lbSearch.Location.X + this.lbSearch.Width + 5, this.txtSearch.Location.Y);
        }

        private bool ChooseFilePath(string dir, string name, string filter, out string newPath)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = $"{filter}|All Files|*.*";
                sfd.InitialDirectory = dir;
                sfd.FileName = name;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    newPath = sfd.FileName;
                    return true;
                }
            }
            newPath = string.Empty;
            return false;
        }

        private void UpdateInfo()
        {
            if (Parser == null)
            {
                this.lbInfo.Text = string.Empty;
                return;
            }
            this.lbInfo.Text = Parser.Architecture;
        }
    }
}
