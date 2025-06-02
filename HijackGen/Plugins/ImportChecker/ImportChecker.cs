using HijackGen.Services;
using HijackGen.Strings;
using System;

namespace HijackGen.Plugins.ImportChecker
{
    internal sealed class ImportChecker : Plugin
    {
        private IDialogService _dialogService;

        public override string Name => GUIStrings.PluginNameImportChecker;

        public override string Description => GUIStrings.PluginDescImportChecker;

        public override void Initialize(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        }

        public override void Execute()
        {
            _dialogService.ShowDialog(typeof(ImportCheckerView), new ImportCheckerViewModel());
        }
    }
}
