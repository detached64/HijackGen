using HijackGen.Models;

namespace HijackGen.Services;

internal interface ISettingsService
{
    AppSettings Settings { get; }
    void SaveSettings();
}
