using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace HijackGen.GUI
{
    public partial class MainForm : Form
    {
        private DllParser Parser;
        internal static List<FunctionInfo> Infos = new List<FunctionInfo>();
        private static List<DllExportInfo> DllInfos => Infos.OfType<DllExportInfo>().ToList();
        private static List<ExeImportInfo> ExeInfos => Infos.OfType<ExeImportInfo>().ToList();
        internal static bool ContainsSpecialChars => !DllInfos.Any(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOfAny(InvalidChars.InvalidCharList) < 0);
        private readonly string CmdArg;

        public MainForm()
        {
            InitializeComponent();
            #region DoubleBuffered Controls
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            Type table = this.table.GetType();
            Type dataGrid = this.Data.GetType();
            PropertyInfo tableInfo = table.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            PropertyInfo dataGridInfo = dataGrid.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            tableInfo.SetValue(this.table, true, null);
            dataGridInfo.SetValue(this.Data, true, null);
            #endregion
        }

        public MainForm(string[] args) : this()
        {
            if (args.Length > 0)
            {
                CmdArg = args[0];
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.pnlGen.Enabled = false;
            this.Data.AutoGenerateColumns = false;
            this.LbStatus.Text = Message.msgReady;
            this.lbInfo.Alignment = ToolStripItemAlignment.Right;
            if (!string.IsNullOrWhiteSpace(CmdArg) && File.Exists(CmdArg))
            {
                this.TextPath.Text = CmdArg;
            }
            else if (!string.IsNullOrWhiteSpace(Settings.DllPath))
            {
                this.TextPath.Text = Settings.DllPath;
            }
        }

        #region DragDrop
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
                TextPath.Text = files[0];
            }
        }
        #endregion

        private void UpdateDataGrid()
        {
            this.Data.Columns.Clear();
            if (Parser != null)
            {
                switch (Parser.Type)
                {
                    case PeType.Dll:
                        this.Data.DataSource = DllInfos;
                        this.Data.Columns.AddRange(new DataGridViewColumn[]
                        {
                        new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "Ordinal",
                            HeaderText = Message.clmOrdinal,
                        },
                        new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "Address",
                            HeaderText = Message.clmAddress,
                            DefaultCellStyle = new DataGridViewCellStyle { Format = "X" }
                        },
                        new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "Name",
                            HeaderText = Message.clmName,
                        },
                        new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "HasForward",
                            HeaderText = Message.clmHasForward,
                        },
                        new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "ForwardName",
                            HeaderText = Message.clmForwardName,
                        }
                        });
                        this.pnlGen.Enabled = true;
                        break;
                    case PeType.Exe:
                        this.Data.DataSource = ExeInfos;
                        this.Data.Columns.AddRange(new DataGridViewColumn[]
                        {
                        new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "DllName",
                            HeaderText = Message.clmDllName,
                        },
                        new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "Name",
                            HeaderText = Message.clmName,
                        },
                        new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "Hint",
                            HeaderText = Message.clmHint,
                            DefaultCellStyle = new DataGridViewCellStyle { Format = "X" }
                        },
                        new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "IATOffset",
                            HeaderText = Message.clmIATOffset,
                            DefaultCellStyle = new DataGridViewCellStyle { Format = "X" }
                        }
                        });
                        this.pnlGen.Enabled = false;
                        break;
                }
            }
        }

        private void UpdateStatus()
        {
            if (Parser != null)
            {
                switch (Parser.Type)
                {
                    case PeType.Dll:
                        this.LbStatus.Text = string.Format(Message.msgExportFound, DllInfos.Count);
                        break;
                    case PeType.Exe:
                        this.LbStatus.Text = string.Format(Message.msgImportFound, ExeInfos.Count);
                        break;
                }
            }
        }

        private void UpdateInfo()
        {
            if (Parser == null)
            {
                this.lbInfo.Text = string.Empty;
            }
            else
            {
                this.lbInfo.Text = Parser.Type == PeType.Unknown ? $"{Parser.Type}" : $"{Parser.Architecture} {Parser.Type}";
            }
        }

        private void TextPath_TextChanged(object sender, EventArgs e)
        {
            Settings.DllPath = this.TextPath.Text;
            Settings.Default.Save();
            this.Data.Columns.Clear();
            this.Data.DataSource = null;
            Parser = null;
            Infos = null;

            try
            {
                Parser = new DllParser(this.TextPath.Text);
                Infos = Parser.GetFuncInfos();
            }
            catch (Exception ex)
            {
                this.LbStatus.Text = ex.Message;
                this.pnlGen.Enabled = false;
            }
            UpdateDataGrid();
            UpdateStatus();
            UpdateInfo();
            this.TextSearch.Text = string.Empty;
            this.Data.ClearSelection();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "PE file|*.exe;*.dll|All Files|*.*";
                ofd.InitialDirectory = string.IsNullOrWhiteSpace(TextPath.Text) ? Settings.DefaultDir : Path.GetDirectoryName(TextPath.Text);
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.TextPath.Text = ofd.FileName;
                }
            }
        }

        private void MenuABout_Click(object sender, EventArgs e)
        {
            using (AboutBox about = new AboutBox())
            {
                about.ShowDialog();
            }
        }

        private void BtnGenDef_Click(object sender, EventArgs e)
        {
            this.LbStatus.Text = Message.msgWorking;
            using (DefOptions defOptions = new DefOptions())
            {
                defOptions.ShowDialog();
                this.LbStatus.Text = QueryResult(defOptions);
            }
        }

        private void BtnGenH_Click(object sender, EventArgs e)
        {
            this.LbStatus.Text = Message.msgWorking;
            using (HOptions hOptions = new HOptions())
            {
                hOptions.ShowDialog();
                this.LbStatus.Text = QueryResult(hOptions);
            }
        }

        private void TextSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.TextSearch.Text))
            {
                switch (Parser.Type)
                {
                    case PeType.Dll:
                        this.Data.DataSource = DllInfos.FindAll(x => x.Ordinal.ToString().IndexOf(TextSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(TextSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.HasForward.ToString().IndexOf(TextSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.HasForward && x.ForwardName.IndexOf(TextSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;
                    case PeType.Exe:
                        this.Data.DataSource = ExeInfos.FindAll(x => !string.IsNullOrWhiteSpace(x.DllName) && x.DllName.IndexOf(TextSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(TextSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;
                }
            }
            else
            {
                switch (Parser.Type)
                {
                    case PeType.Dll:
                        this.Data.DataSource = DllInfos;
                        break;
                    case PeType.Exe:
                        this.Data.DataSource = ExeInfos;
                        break;
                }
            }
            this.Data.ClearSelection();
        }

        private string QueryResult(OptionsTemplate options)
        {
            switch (options.Result)
            {
                case OperationResult.Canceled:
                    return Message.msgCanceled;
                case OperationResult.Failed when options.Exception != null:
                    return string.Format(Message.msgFailedWithMsg, options.Exception.Message);
                case OperationResult.Failed:
                    return Message.msgFailed;
                case OperationResult.Success:
                    return Message.msgSuccess;
                default:
                    return string.Empty;
            }
        }
    }

    public enum OperationResult
    {
        Canceled,
        Failed,
        Success
    }
}
