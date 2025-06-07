using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HijackGen.Messengers;
using HijackGen.Models;
using HijackGen.Models.Enums;
using HijackGen.Services;
using HijackGen.Strings;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace HijackGen.ViewModels
{
    internal partial class GenerationViewModel : ObservableObject
    {
        private readonly MainViewModel _parentVM;
        private readonly IDialogService _dialogService;

        private readonly ExportParser Parser;
        private readonly List<DllExportInfo> DllInfos;

        private const string FolderName = "Hijack";
        private bool ContainsSpecialChars => DllInfos.Any(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOfAny(InvalidChars.InvalidCharList) >= 0);

        public GenerationViewModel(MainViewModel parentVM, IDialogService dialogService)
        {
            _parentVM = parentVM ?? throw new ArgumentNullException(nameof(parentVM));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            Parser = parentVM.Parser;
            DllInfos = parentVM.Infos;
            SavePath = Settings.Default.SaveDir;
            SelectedType = Parser.GenerationType;
            SelectedArchitecture = Parser.Architecture;
            SelectedFormat = Enum.TryParse(Settings.Default.SelectedFormatName, out GenerationFormat format) ? format : GenerationFormat.Sln;
        }

        [ObservableProperty]
        public string savePath;
        [ObservableProperty]
        public GenerationType selectedType;
        [ObservableProperty]
        public PeArchitecture selectedArchitecture;
        [ObservableProperty]
        public GenerationFormat selectedFormat;

        public Visibility TextVisibility
        {
            get
            {
                if (SelectedType == GenerationType.System && SelectedFormat != GenerationFormat.Def && ContainsSpecialChars)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        [RelayCommand]
        public void Generate()
        {
            try
            {
                Generator gen = Generator.Create(
                    Path.GetFileNameWithoutExtension(Settings.Default.DllPath),
                    DllInfos,
                    SelectedType,
                    SelectedArchitecture,
                    SelectedFormat);

                foreach (KeyValuePair<string, string> content in gen.Generate())
                {
                    string path = Path.Combine(SavePath, FolderName, content.Key);
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    File.WriteAllText(path, content.Value);
                }
                WeakReferenceMessenger.Default.Send(new StatusBarMessage(Messages.msgSuccess));
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new StatusBarMessage(string.Format(Messages.msgFailedWithMsg, ex.Message)));
            }
            finally
            {
                WeakReferenceMessenger.Default.Send(new CloseWindowMessage());
            }
        }

        [RelayCommand]
        public void BrowseFolder()
        {
            OpenFolderDialog ofd = new()
            {
                Title = Messages.msgSpecifyPath,
                Multiselect = false
            };
            if (ofd.ShowDialog() == true)
            {
                SavePath = ofd.FolderName ?? string.Empty;
            }
        }

        [RelayCommand]
        public void Close()
        {
            WeakReferenceMessenger.Default.Send(new CloseWindowMessage());
        }

        partial void OnSavePathChanged(string value)
        {
            Settings.Default.SaveDir = value;
            Settings.Default.Save();
        }

        partial void OnSelectedTypeChanged(GenerationType value)
        {
            OnPropertyChanged(nameof(TextVisibility));
        }

        partial void OnSelectedArchitectureChanged(PeArchitecture value)
        {
            OnPropertyChanged(nameof(TextVisibility));
        }

        partial void OnSelectedFormatChanged(GenerationFormat value)
        {
            Settings.Default.SelectedFormatName = value.ToString();
            Settings.Default.Save();
            OnPropertyChanged(nameof(TextVisibility));
        }
    }
}
