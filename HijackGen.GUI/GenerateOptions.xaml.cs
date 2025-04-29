using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HijackGen.GUI
{
    public partial class GenerateOptions : Window
    {
        private readonly DllParser Parser;
        private readonly List<DllExportInfo> DllInfos;
        private RadioButton SelectedButton;
        private bool ContainsSpecialChars => DllInfos.Any(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOfAny(InvalidChars.InvalidCharList) >= 0);

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
            this.TbPath.Text = Settings.Default.SaveDir;
            InitFormatSelection();
        }

        private void TbPath_TextChanged(object sender, RoutedEventArgs e)
        {
            Settings.Default.SaveDir = this.TbPath.Text;
        }

        private void BtSelectPath_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                fbd.Description = Message.msgSpecifyDir;
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TbPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void BtGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedButton == null)
            {
                MessageBox.Show(Message.msgSpecifyFormat);
                return;
            }

            Generator gen = Generator.Create(Path.GetFileNameWithoutExtension(Settings.Default.DllPath), DllInfos, this.RbSystemDll.IsChecked == true, this.RbX64.IsChecked == true, SelectedButton.Tag.ToString());

            try
            {
                if (!string.Equals(SelectedButton.Tag.ToString(), "def") && this.RbSystemDll.IsChecked == true && ContainsSpecialChars)
                {
                    if (MessageBox.Show(Message.msgContainsInvalidChars, Message.msgWarning, MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
                    {
                        Result = OperationResult.Canceled;
                        return;
                    }
                }
                foreach (var content in gen.Generate())
                {
                    File.WriteAllText(Path.Combine(Settings.Default.SaveDir, content.Key), content.Value);
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Settings.Default.Save();
        }

        private void RbFormat_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb && rb.IsChecked == true)
            {
                Settings.Default.SelectedButtonName = rb.Name;
                SelectedButton = rb;
            }
        }

        private void InitFormatSelection()
        {
            if (!string.IsNullOrEmpty(Settings.Default.SelectedButtonName))
            {
                foreach (RadioButton rb in LogicalTreeHelper.GetChildren(this.GridFormat).OfType<RadioButton>())
                {
                    if (rb.Name == Settings.Default.SelectedButtonName)
                    {
                        rb.IsChecked = true;
                        break;
                    }
                }
            }
            else
            {
                this.RbH.IsChecked = true;
            }
        }
    }
}
