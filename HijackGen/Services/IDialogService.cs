using System;

namespace HijackGen.Services
{
    public interface IDialogService
    {
        void ShowDialog(Type viewType, object viewModel);
        void CloseDialog(object viewModel);
    }
}
