using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace HijackGen.ViewModels
{
    internal class MenuItemViewModel : INotifyPropertyChanged
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public ImageSource Icon { get; set; }
        public ICommand Command { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
