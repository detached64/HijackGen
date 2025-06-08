using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HijackGen.Messengers;
using HijackGen.Models;
using HijackGen.Plugins;
using HijackGen.Services;
using HijackGen.Strings;
using HijackGen.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace HijackGen.ViewModels
{
    public partial class MainViewModel : ObservableObject, IRecipient<StatusBarMessage>
    {
        private readonly IDialogService _dialogService;
        private readonly PluginManager _pluginManager;
        internal ExportParser Parser;
        internal List<DllExportInfo> Infos;

        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _pluginManager = new PluginManager();
            MenuItems = [];
            LoadPlugins();
            WeakReferenceMessenger.Default.Register(this);

            DllPath = Settings.Default.DllPath;
        }

        [ObservableProperty]
        private string dllPath;

        [ObservableProperty]
        private ObservableCollection<DllExportInfo> searchedInfos;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private string statusText = Messages.msgReady;

        [ObservableProperty]
        private string infoText;

        [ObservableProperty]
        private bool canGenerate;

        [ObservableProperty]
        private ObservableCollection<MenuItemViewModel> menuItems;

        [RelayCommand]
        public void ShowOptions()
        {
            if (CanGenerate)
            {
                GenerationViewModel childVM = new(this, _dialogService);
                _dialogService.ShowDialog(typeof(GenerationView), childVM);
            }
        }

        [RelayCommand]
        public void ShowAbout()
        {
            AboutViewModel childVM = new();
            _dialogService.ShowDialog(typeof(AboutView), childVM);
        }

        [RelayCommand]
        public void OpenFile()
        {
            OpenFileDialog ofd = new()
            {
                Filter = $"{GUIStrings.FilterDll} (*.dll)|*.dll|{GUIStrings.FilterAllFiles} (*.*)|*.*",
                InitialDirectory = string.IsNullOrWhiteSpace(DllPath) ? Settings.DefaultDir : Path.GetDirectoryName(DllPath),
            };
            if (ofd.ShowDialog() is true)
            {
                DllPath = ofd.FileName;
            }
        }

        public void Receive(StatusBarMessage message)
        {
            if (message is null)
            {
                return;
            }
            StatusText = message.Content;
        }

        private void LoadPlugins()
        {
            _pluginManager.LoadBuiltInPlugins();
            _pluginManager.LoadThirdPartyPlugins();
            AddPluginsToMenu(_pluginManager.BuiltInPlugins, Messages.msgBuiltIn);
            AddPluginsToMenu(_pluginManager.ThirdPartyPlugins, Messages.msgThirdParty);
        }

        private void AddPluginsToMenu(IEnumerable<Plugin> plugins, string type)
        {
            foreach (Plugin plugin in plugins)
            {
                MenuItems.Add(new MenuItemViewModel
                {
                    Header = plugin.Name,
                    Description = $"[{type}] {plugin.Description}",
                    Icon = plugin.Icon,
                    Command = new RelayCommand(() =>
                    {
                        try
                        {
                            plugin.Initialize(this, _dialogService);
                            plugin.Execute();
                        }
                        catch (Exception ex)
                        {
                            StatusText = string.Format(Messages.msgPluginError, ex.Message);
                        }
                    })
                });
            }
        }

        partial void OnDllPathChanged(string value)
        {
            Settings.Default.DllPath = value;
            Settings.Default.Save();
            Parser = null;
            Infos = [];
            SearchedInfos = null;
            InfoText = string.Empty;
            try
            {
                Parser = new(DllPath);
                Infos = Parser.GetInfos();
                SearchedInfos = new ObservableCollection<DllExportInfo>(Infos);
            }
            catch (Exception ex)
            {
                StatusText = ex.Message;
                CanGenerate = false;
                return;
            }
            CanGenerate = SearchedInfos.Count > 0;
            StatusText = string.Format(Messages.msgExportFound, SearchedInfos.Count);
            InfoText = $"{Parser.Architecture}";
        }

        partial void OnSearchTextChanged(string value)
        {
            SearchedInfos = new ObservableCollection<DllExportInfo>(string.IsNullOrWhiteSpace(value) ?
                Infos : Infos.FindAll(x => x.Ordinal.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                        !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                        x.HasForward.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                        x.HasForward && !string.IsNullOrWhiteSpace(x.ForwardName) && x.ForwardName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
