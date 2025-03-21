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
        private DllParser _parser;
        private List<DataItem> _dataItems;

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
                _parser = new DllParser(txtPath.Text);
                pnlControl.Enabled = true;
                _dataItems = _parser.GetExportInfos();
                dataGrid.DataSource = _dataItems;
                lbStatus.Text = string.Format(Message.msgFound, _dataItems.Count);
            }
            catch (Exception ex)
            {
                lbStatus.Text = ex.Message;
                pnlControl.Enabled = false;
                _dataItems = null;
                dataGrid.DataSource = null;
            }
            txtSearch.Text = string.Empty;
            dataGrid.ClearSelection();
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
            lbStatus.Text = Message.msgWorking;
            string former = string.IsNullOrWhiteSpace(Settings.Default.DefSavePath) ? Path.ChangeExtension(txtPath.Text, ".def") : Settings.Default.DefSavePath;
            if (ChooseFilePath(former, "Def File|*.def", out string path))
            {
                Settings.Default.DefSavePath = path;
                Settings.Default.Save();
                DefGenerator defGen = new DefGenerator(Path.GetFileNameWithoutExtension(txtPath.Text), _dataItems);
                File.WriteAllText(path, defGen.Generate());
                lbStatus.Text = string.Format(Message.msgSaveSuccess, ".def", path);
            }
            else
            {
                lbStatus.Text = Message.msgCanceled;
            }
        }

        private void btGenH_Click(object sender, EventArgs e)
        {
            lbStatus.Text = Message.msgWorking;
            string former = string.IsNullOrWhiteSpace(Settings.Default.HSavePath) ? Path.ChangeExtension(txtPath.Text, ".h") : Settings.Default.HSavePath;
            if (ChooseFilePath(former, "C Header|*.h", out string path))
            {
                Settings.Default.HSavePath = path;
                Settings.Default.Save();
                HGenerator hGen = new HGenerator(Path.GetFileNameWithoutExtension(txtPath.Text), _dataItems);
                File.WriteAllText(path, hGen.Generate());
                lbStatus.Text = string.Format(Message.msgSaveSuccess, ".h", path);
            }
            else
            {
                lbStatus.Text = Message.msgCanceled;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                dataGrid.DataSource = _dataItems.FindAll(x => x.Ordinal.ToString().IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    x.HasForward.ToString().IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    x.HasForward && x.ForwardName.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            else
            {
                dataGrid.DataSource = _dataItems;
            }
            dataGrid.ClearSelection();
        }

        private void lbSearch_SizeChanged(object sender, EventArgs e)
        {
            this.txtSearch.Location = new Point(this.lbSearch.Location.X + this.lbSearch.Width + 5, this.txtSearch.Location.Y);
        }

        private bool ChooseFilePath(string former, string filter, out string newPath)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = $"{filter}|All Files|*.*";
                sfd.InitialDirectory = Path.GetDirectoryName(former);
                sfd.FileName = former;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    newPath = sfd.FileName;
                    return true;
                }
            }
            newPath = string.Empty;
            return false;
        }
    }
}
