using HijackGen.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HijackGen.GUI
{
    public partial class MainWindow : Window
    {
        private DllParser Parser;
        private List<FunctionInfo> Infos = new List<FunctionInfo>();
        private List<DllExportInfo> DllInfos => Infos.OfType<DllExportInfo>().ToList();
        private List<ExeImportInfo> ExeInfos => Infos.OfType<ExeImportInfo>().ToList();

        private readonly string CmdArg;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string[] args) : this()
        {
            if (args.Length > 0)
            {
                CmdArg = args[0];
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.PnlGenerate.IsEnabled = false;
            this.TxtStatus.Text = Message.msgReady;
            if (!string.IsNullOrWhiteSpace(CmdArg) && File.Exists(CmdArg))
            {
                this.TbPath.Text = CmdArg;
            }
            else if (!string.IsNullOrWhiteSpace(Settings.Default.DllPath))
            {
                this.TbPath.Text = Settings.Default.DllPath;
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Owner = this;
            aboutBox.ShowDialog();
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                this.TbPath.Text = files[0];
            }
        }

        private void BtSelectPE_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = $"{Message.msgPEFilter} (*.dll;*.exe)|*.dll;*.exe|{Message.msgAllFilesFilter} (*.*)|*.*",
                InitialDirectory = string.IsNullOrWhiteSpace(this.TbPath.Text) ? Settings.DefaultDir : Path.GetDirectoryName(this.TbPath.Text),
            };
            if (ofd.ShowDialog() == true)
            {
                this.TbPath.Text = ofd.FileName;
            }
        }

        private void TbPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Default.DllPath = this.TbPath.Text;
            Parser = null;
            Infos = null;

            try
            {
                Parser = new DllParser(this.TbPath.Text);
                Infos = Parser.GetFuncInfos();
            }
            catch (Exception ex)
            {
                this.TxtStatus.Text = ex.Message;
                this.PnlGenerate.IsEnabled = false;
            }
            UpdateColumns();
            UpdateStatus();
            UpdateInfo();
            this.TbSearch.Text = string.Empty;
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.TbSearch.Text))
            {
                switch (Parser.Type)
                {
                    case PeType.Dll:
                        this.Data.ItemsSource = DllInfos;
                        break;
                    case PeType.Exe:
                        this.Data.ItemsSource = ExeInfos;
                        break;
                }
            }
            else
            {
                switch (Parser.Type)
                {
                    case PeType.Dll:
                        this.Data.ItemsSource = DllInfos.FindAll(x => x.Ordinal.ToString().IndexOf(this.TbSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(this.TbSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.HasForward.ToString().IndexOf(this.TbSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.HasForward && !string.IsNullOrWhiteSpace(x.ForwardName) && x.ForwardName.IndexOf(this.TbSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;
                    case PeType.Exe:
                        this.Data.ItemsSource = ExeInfos.FindAll(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(this.TbSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        !string.IsNullOrWhiteSpace(x.DllName) && x.DllName.IndexOf(this.TbSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;
                }
            }
        }

        private void BtGenerate_Click(object sender, RoutedEventArgs e)
        {
            this.TxtStatus.Text = Message.msgWorking;
            GenerateOptions options = new GenerateOptions(Parser, DllInfos);
            options.Owner = this;
            options.ShowDialog();
            this.TxtStatus.Text = QueryResult(options);
        }

        private void UpdateColumns()
        {
            this.Data.Columns.Clear();
            this.Data.ItemsSource = null;
            DataGridColumn[] columns;
            if (Parser != null)
            {
                switch (Parser.Type)
                {
                    case PeType.Dll:
                        columns = (DataGridColumn[])this.Data.Resources["ColumnsDll"];
                        this.PnlGenerate.IsEnabled = true;
                        break;
                    case PeType.Exe:
                        columns = (DataGridColumn[])this.Data.Resources["ColumnsExe"];
                        this.PnlGenerate.IsEnabled = false;
                        break;
                    default:
                        this.PnlGenerate.IsEnabled = false;
                        return;
                }
                foreach (var column in columns)
                {
                    this.Data.Columns.Add(column);
                }
                switch (Parser.Type)
                {
                    case PeType.Dll:
                        this.Data.ItemsSource = DllInfos;
                        break;
                    case PeType.Exe:
                        this.Data.ItemsSource = ExeInfos;
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
                        this.TxtStatus.Text = string.Format(Message.msgExportFound, DllInfos.Count);
                        break;
                    case PeType.Exe:
                        this.TxtStatus.Text = string.Format(Message.msgImportFound, ExeInfos.Count);
                        break;
                }
            }
        }

        private void UpdateInfo()
        {
            if (Parser == null)
            {
                this.TxtInfo.Text = string.Empty;
            }
            else
            {
                this.TxtInfo.Text = Parser.Type == PeType.Unknown ? $"{Parser.Type}" : $"{Parser.Architecture} {Parser.Type}";
            }
        }

        private string QueryResult(GenerateOptions options)
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.Save();
        }

        private void CheckKnownDlls_Click(object sender, RoutedEventArgs e)
        {
            KnownDllsChecker checker = new KnownDllsChecker();
            checker.Check();
        }
    }

    public enum OperationResult
    {
        Canceled,
        Failed,
        Success
    }
}
