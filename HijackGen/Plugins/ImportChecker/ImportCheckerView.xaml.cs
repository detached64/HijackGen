using System.Windows;

namespace HijackGen.Plugins.ImportChecker
{
    public partial class ImportCheckerView : Window
    {
        public ImportCheckerView()
        {
            InitializeComponent();
        }

        private void Window_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.None;
            e.Handled = true;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
            {
                this.TxtPEPath.Text = files[0];
            }
        }
    }
}
