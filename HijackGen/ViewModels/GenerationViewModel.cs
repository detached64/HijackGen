using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HijackGen.Enums;
using HijackGen.Models;
using HijackGen.Models.Generators;
using HijackGen.Services;
using HijackGen.Strings;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace HijackGen.ViewModels;

internal partial class GenerationViewModel : ViewModelBase
{
    private readonly ISettingsService _settingsService;
    private const string FolderName = "Hijack";

    public GenerationViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;

        SaveDir = string.IsNullOrWhiteSpace(settingsService.Settings.SaveDirectory)
            ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            : settingsService.Settings.SaveDirectory;
        SelectedArchitecture = settingsService.Settings.SelectedArchitecture;
        SelectedType = settingsService.Settings.SelectedType;
        SelectedFormat = settingsService.Settings.SelectedFormat;
    }

    [ObservableProperty]
    private string saveDir;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextVisibility))]
    private PeArchitecture selectedArchitecture;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextVisibility))]
    private PeType selectedType;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextVisibility))]
    private GenerationFormat selectedFormat;

    private bool ContainsSpecialChars => _settingsService.Settings.ExportInfos.Any(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOfAny(InvalidChars.InvalidCharList) >= 0);
    public Visibility TextVisibility => SelectedType is PeType.System && SelectedFormat is not GenerationFormat.Def && ContainsSpecialChars
                ? Visibility.Visible
                : Visibility.Collapsed;

    [RelayCommand]
    private void Generate(Window window)
    {
        try
        {
            using Generator gen = Generator.Create(
                Path.GetFileNameWithoutExtension(_settingsService.Settings.FilePath),
                _settingsService.Settings.ExportInfos,
                SelectedType,
                SelectedArchitecture,
                SelectedFormat);

            foreach (KeyValuePair<string, string> content in gen.Generate())
            {
                string path = Path.Combine(SaveDir, FolderName, content.Key);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, content.Value);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(string.Format(MsgStrings.FailedWithMsg, ex.Message), GuiStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            window?.Close();
        }
    }

    [RelayCommand]
    private void BrowseFolder()
    {
        OpenFolderDialog ofd = new()
        {
            Multiselect = false
        };
        if (ofd.ShowDialog() is true)
        {
            SaveDir = ofd.FolderName ?? string.Empty;
        }
    }

    [RelayCommand]
    private static void Close(Window window)
    {
        window?.Close();
    }

    partial void OnSaveDirChanged(string value)
    {
        _settingsService.Settings.SaveDirectory = value;
    }

    partial void OnSelectedArchitectureChanged(PeArchitecture value)
    {
        _settingsService.Settings.SelectedArchitecture = value;
    }

    partial void OnSelectedTypeChanged(PeType value)
    {
        _settingsService.Settings.SelectedType = value;
    }

    partial void OnSelectedFormatChanged(GenerationFormat value)
    {
        _settingsService.Settings.SelectedFormat = value;
    }
}
