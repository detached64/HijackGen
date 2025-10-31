using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HijackGen.Models;
using HijackGen.Services;
using HijackGen.Strings;
using HijackGen.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace HijackGen.ViewModels;

internal partial class MainViewModel : ViewModelBase
{
    private readonly IShowDialogService _dialogService;
    private readonly ISettingsService _settingsService;

    public MainViewModel(IShowDialogService dialogService, ISettingsService settingsService)
    {
        _dialogService = dialogService;
        _settingsService = settingsService;

        FilePath = settingsService.Settings.FilePath;
    }

    [ObservableProperty]
    private string filePath;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
    private PeParser parser;
    [ObservableProperty]
    private List<ImportInfo> importInfos;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
    private List<ExportInfo> exportInfos;
    [ObservableProperty]
    private ObservableCollection<ImportInfo> importSearchedInfos;
    [ObservableProperty]
    private string importSearchText;
    [ObservableProperty]
    private ObservableCollection<ExportInfo> exportSearchedInfos;
    [ObservableProperty]
    private string exportSearchText;
    [ObservableProperty]
    private string peInfo;

    [RelayCommand(CanExecute = nameof(CanGenerate))]
    private void Generate(Window window)
    {
        _dialogService.ShowDialog<GenerationView, GenerationViewModel>(window);
    }

    [RelayCommand]
    private void ShowAbout(Window window)
    {
        _dialogService.ShowDialog<AboutView, AboutViewModel>(window);
    }

    [RelayCommand]
    private void OpenFile()
    {
        OpenFileDialog ofd = new()
        {
            Filter = $"{GuiStrings.PeFilter} (*.dll;*.exe)|*.dll;*.exe|{GuiStrings.AllFilesFilter} (*.*)|*.*",
            InitialDirectory = string.IsNullOrWhiteSpace(FilePath) ? Environment.GetFolderPath(Environment.SpecialFolder.System) : Path.GetDirectoryName(FilePath),
        };
        if (ofd.ShowDialog() == true)
        {
            FilePath = ofd.FileName;
        }
    }

    [RelayCommand]
    private static void OpenRegistryKnownDlls()
    {
        using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit"))
        {
            key.SetValue("LastKey", @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\KnownDlls", RegistryValueKind.String);
        }
        Process.Start(new ProcessStartInfo
        {
            FileName = "regedit.exe",
            UseShellExecute = true
        });
    }

    partial void OnFilePathChanged(string value)
    {
        _settingsService.Settings.FilePath = value;

        Parser = null;
        ImportInfos = [];
        ExportInfos = [];
        ImportSearchedInfos = [];
        ExportSearchedInfos = [];
        PeInfo = string.Empty;

        try
        {
            Parser = new(FilePath);
            ImportInfos = Parser.GetImportInfos();
            ExportInfos = Parser.GetExportInfos();
            ImportSearchedInfos = new ObservableCollection<ImportInfo>(ImportInfos);
            ExportSearchedInfos = new ObservableCollection<ExportInfo>(ExportInfos);
            PeInfo = $@"{GuiStrings.Architecture}: {Parser.Architecture}{Environment.NewLine}{GuiStrings.ExportCount}: {ImportInfos.Count}{Environment.NewLine}{GuiStrings.ImportCount}: {ExportInfos.Count}";
        }
        catch (Exception ex)
        {
            ImportInfos = [];
            ExportInfos = [];
            ImportSearchedInfos = [];
            ExportSearchedInfos = [];
            PeInfo = ex.Message;
            return;
        }

        ImportSearchText = string.Empty;
        ExportSearchText = string.Empty;
    }

    partial void OnExportInfosChanged(List<ExportInfo> value)
    {
        _settingsService.Settings.ExportInfos = value;
    }

    partial void OnParserChanged(PeParser value)
    {
        if (value is null)
            return;
        _settingsService.Settings.SelectedArchitecture = value.Architecture;
        _settingsService.Settings.SelectedType = value.GenerationType;
    }

    partial void OnImportSearchTextChanged(string value)
    {
        ImportSearchedInfos = new ObservableCollection<ImportInfo>(string.IsNullOrWhiteSpace(value) ?
            ImportInfos : ImportInfos.FindAll(x => !string.IsNullOrWhiteSpace(x.DllName) && x.DllName.Contains(importSearchText, StringComparison.OrdinalIgnoreCase) ||
                    !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(importSearchText, StringComparison.OrdinalIgnoreCase)));
    }

    partial void OnExportSearchTextChanged(string value)
    {
        ExportSearchedInfos = new ObservableCollection<ExportInfo>(string.IsNullOrWhiteSpace(value) ?
            ExportInfos : ExportInfos.FindAll(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(ExportSearchText, StringComparison.OrdinalIgnoreCase) ||
                    x.HasForward.ToString().Contains(ExportSearchText, StringComparison.OrdinalIgnoreCase) ||
                    x.HasForward && !string.IsNullOrWhiteSpace(x.ForwardName) && x.ForwardName.Contains(ExportSearchText, StringComparison.OrdinalIgnoreCase)));
    }

    private bool CanGenerate => Parser?.IsDll == true && ExportInfos?.Count > 0;
}
