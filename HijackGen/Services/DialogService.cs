using System;
using System.Collections.Generic;
using System.Windows;

namespace HijackGen.Services
{
    public sealed class DialogService : IDialogService
    {
        private readonly Dictionary<object, Window> _openWindows = [];

        public void ShowDialog(Type viewType, object viewModel)
        {
            if (!typeof(Window).IsAssignableFrom(viewType))
            {
                throw new ArgumentException("The provided type must be a Window.", nameof(viewType));
            }

            Window window = (Window)Activator.CreateInstance(viewType)!;
            window.DataContext = viewModel;
            window.Owner = Application.Current.MainWindow;
            window.Closed += (s, e) => _openWindows.Remove(viewModel);

            _openWindows[viewModel] = window;
            window.ShowDialog();
        }

        public void CloseDialog(object viewModel)
        {
            if (_openWindows.TryGetValue(viewModel, out Window window))
            {
                window.Close();
                _openWindows.Remove(viewModel);
            }
        }
    }
}
