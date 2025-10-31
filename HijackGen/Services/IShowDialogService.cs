using HijackGen.ViewModels;
using System.Windows;

namespace HijackGen.Services;

internal interface IShowDialogService
{
    void ShowDialog<TView, TViewModel>(Window parent) where TView : Window where TViewModel : ViewModelBase;

    void ShowDialog<TView>(ViewModelBase vm, Window parent) where TView : Window;
}
