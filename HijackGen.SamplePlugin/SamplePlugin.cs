using HijackGen.Plugins;
using HijackGen.Services;

namespace HijackGen.SamplePlugin
{
    public class SamplePlugin : Plugin
    {
        private IDialogService _dialogService;

        public override string Name => "Sample Plugin";
        public override string Description => "A sample plugin for HijackGen to demonstrate plugin functionality.";

        public override void Initialize(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        }

        public override void Execute()
        {
            PluginViewModel vm = new();
            _dialogService.ShowDialog(typeof(PluginView), vm);
        }
    }
}
