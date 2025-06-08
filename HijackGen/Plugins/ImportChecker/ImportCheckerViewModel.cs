using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HijackGen.Models;
using HijackGen.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace HijackGen.Plugins.ImportChecker
{
    internal partial class ImportCheckerViewModel : ObservableObject
    {
        internal ImportParser Parser;
        internal List<ExeImportInfo> Infos;

        [ObservableProperty]
        private string pePath;

        [ObservableProperty]
        private ObservableCollection<ExeImportInfo> searchedInfos;

        [ObservableProperty]
        private string searchText = string.Empty;

        [RelayCommand]
        public void OpenFile()
        {
            OpenFileDialog ofd = new()
            {
                InitialDirectory = string.IsNullOrWhiteSpace(PePath) ? Settings.DefaultDir : Path.GetDirectoryName(PePath),
            };
            if (ofd.ShowDialog() is true)
            {
                PePath = ofd.FileName;
            }
        }

        partial void OnPePathChanged(string value)
        {
            Parser = null;
            Infos = [];
            SearchedInfos = null;
            try
            {
                Parser = new(PePath);
                Infos = Parser.GetInfos();
                SearchedInfos = new ObservableCollection<ExeImportInfo>(Infos);
            }
            catch
            {
                return;
            }
        }

        partial void OnSearchTextChanged(string value)
        {
            SearchedInfos = new ObservableCollection<ExeImportInfo>(string.IsNullOrWhiteSpace(value) ?
                Infos : Infos.FindAll(x => !string.IsNullOrWhiteSpace(x.DllName) && x.DllName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                        !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
