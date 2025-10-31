using HijackGen.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HijackGen.Models;

internal sealed class AppSettings
{
    public string FilePath { get; set; }
    public string SaveDirectory { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    [JsonIgnore]
    public List<ExportInfo> ExportInfos { get; set; }
    [JsonIgnore]
    public PeArchitecture SelectedArchitecture { get; set; }
    [JsonIgnore]
    public PeType SelectedType { get; set; }
    public GenerationFormat SelectedFormat { get; set; } = GenerationFormat.Solution;
}
