using HijackGen.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace HijackGen.Services;

internal sealed class SettingsService : ISettingsService
{
    private static readonly string DefaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Assembly.GetExecutingAssembly().GetName().Name, "settings.json");
    private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };
    public AppSettings Settings { get; }

    public SettingsService()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(DefaultPath));
        if (!File.Exists(DefaultPath))
        {
            Settings = new();
        }
        else
        {
            try
            {
                string json = File.ReadAllText(DefaultPath);
                Settings = JsonSerializer.Deserialize<AppSettings>(json) ?? new();
            }
            catch
            {
                Settings = new();
            }
        }
    }

    public void SaveSettings()
    {
        string json = JsonSerializer.Serialize(Settings, _jsonOptions);
        File.WriteAllText(DefaultPath, json);
    }
}
