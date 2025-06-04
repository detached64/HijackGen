using HijackGen.Services;
using HijackGen.ViewModels;
using System.Windows.Media;

namespace HijackGen.Plugins
{
    public abstract class Plugin
    {
        public abstract string Name { get; }
        public virtual string Description => string.Empty;
        public virtual ImageSource Icon => null;
        public virtual void Initialize(MainViewModel vm, IDialogService dialogService) { }
        public abstract void Execute();
    }
}
