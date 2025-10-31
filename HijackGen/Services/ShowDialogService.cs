using HijackGen.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace HijackGen.Services;

internal sealed class ShowDialogService(IServiceProvider serviceProvider) : IShowDialogService
{
    public void ShowDialog<TView, TViewModel>(Window parent) where TView : Window where TViewModel : ViewModelBase
    {
        Window view = serviceProvider.GetService<TView>() as Window ?? throw new InvalidOperationException($"Could not resolve view of type {typeof(TView).FullName}");
        view.DataContext = serviceProvider.GetService<TViewModel>();
        view.Owner = parent;
        _ = view.ShowDialog();
    }

    public void ShowDialog<TView>(ViewModelBase vm, Window parent) where TView : Window
    {
        Window view = serviceProvider.GetService<TView>() as Window ?? throw new InvalidOperationException($"Could not resolve view of type {typeof(TView).FullName}");
        view.DataContext = vm;
        view.Owner = parent;
        _ = view.ShowDialog();
    }
}
