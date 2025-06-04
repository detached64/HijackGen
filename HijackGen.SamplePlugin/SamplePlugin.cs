using HijackGen.Plugins;
using HijackGen.Services;
using HijackGen.ViewModels;

namespace HijackGen.SamplePlugin
{
    public class SamplePlugin : Plugin
    {
        private MainViewModel _mainViewModel;
        private IDialogService _dialogService;

        public override string Name => "Sample Plugin";
        public override string Description => "A sample plugin for HijackGen to demonstrate plugin functionality.";

        public override void Initialize(MainViewModel vm, IDialogService dialogService)
        {
            _mainViewModel = vm ?? throw new ArgumentNullException(nameof(vm));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        }

        public override void Execute()
        {
            PluginViewModel vm = new(_mainViewModel);
            _dialogService.ShowDialog(typeof(PluginView), vm);
        }
    }
}
