using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace HijackGen.GUI
{
    public partial class GenerateOptions : Window
    {
        private readonly DllParser Parser;
        private readonly List<DllExportInfo> DllInfos;
        private string Extension => Settings.GenerateHeader ? ".h" : ".def";
        private bool ContainsSpecialChars => !DllInfos.Any(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOfAny(InvalidChars.InvalidCharList) < 0);

        public Exception Exception { get; private set; }
        public OperationResult Result { get; private set; }

        public GenerateOptions()
        {
            InitializeComponent();
        }

        public GenerateOptions(DllParser parser, List<DllExportInfo> dllInfos) : this()
        {
            Parser = parser;
            DllInfos = dllInfos;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.RbSystemDll.IsChecked = Parser.IsSystemDll;
            this.RbCustomDll.IsChecked = !Parser.IsSystemDll;
            this.RbX86.IsChecked = Parser.IsX86;
            this.RbX64.IsChecked = Parser.IsX64;
            this.RbH.IsChecked = Settings.GenerateHeader;
            this.RbDef.IsChecked = !Settings.GenerateHeader;
            this.TbPath.Text = Path.Combine(Settings.SaveDir, Path.GetFileNameWithoutExtension(Settings.DllPath) + Extension);
        }

        private void TbPath_TextChanged(object sender, RoutedEventArgs e)
        {
            Settings.SaveDir = Path.GetDirectoryName(this.TbPath.Text);
        }

        private void BtSelectPath_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = $"{Message.msgHFilter} (*{Extension})|*{Extension}|{Message.msgAllFilesFilter} (*.*)|*.*",
                FileName = $"{Path.GetFileName(this.TbPath.Text)}",
                InitialDirectory = Settings.SaveDir
            };
            if (sfd.ShowDialog() == true)
            {
                this.TbPath.Text = sfd.FileName;
            }
        }

        private void BtGenerate_Click(object sender, RoutedEventArgs e)
        {
            Generator gen = null;
            try
            {
                if (Settings.GenerateHeader)
                {
                    gen = new HGenerator(Path.GetFileNameWithoutExtension(Settings.DllPath), DllInfos, this.RbSystemDll.IsChecked == true, this.RbX64.IsChecked == true);
                }
                else
                {
                    gen = new DefGenerator(Path.GetFileNameWithoutExtension(Settings.DllPath), DllInfos);
                }

                if (Settings.GenerateHeader && this.RbSystemDll.IsChecked == true && ContainsSpecialChars)
                {
                    if (MessageBox.Show(Message.msgContainsInvalidChars, Message.msgWarning, MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
                    {
                        Result = OperationResult.Canceled;
                        return;
                    }
                }
                foreach (var content in gen.Generate())
                {
                    switch (content.Key)
                    {
                        case FileType.Header:
                            File.WriteAllText(this.TbPath.Text, content.Value);
                            break;
                        case FileType.Def:
                            File.WriteAllText(Path.ChangeExtension(this.TbPath.Text, "def"), content.Value);
                            break;
                    }
                }
                Result = OperationResult.Success;
            }
            catch (Exception ex)
            {
                Result = OperationResult.Failed;
                Exception = ex;
            }
            finally
            {
                gen.Dispose();
                this.Close();
            }
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = OperationResult.Canceled;
            this.Close();
        }

        private void RbH_Checked(object sender, RoutedEventArgs e)
        {
            Settings.GenerateHeader = true;
            this.TbPath.Text = Path.ChangeExtension(this.TbPath.Text, Extension);
        }

        private void RbDef_Checked(object sender, RoutedEventArgs e)
        {
            Settings.GenerateHeader = false;
            this.TbPath.Text = Path.ChangeExtension(this.TbPath.Text, Extension);
        }
    }
}
