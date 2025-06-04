using HijackGen.ViewModels;

namespace HijackGen.SamplePlugin
{
    internal class PluginViewModel(MainViewModel parentVM)
    {
        private readonly MainViewModel _parentVM = parentVM ?? throw new ArgumentNullException(nameof(parentVM));

        public string Message => $"Currently selected DLL: {_parentVM.DllPath}";
    }
}
