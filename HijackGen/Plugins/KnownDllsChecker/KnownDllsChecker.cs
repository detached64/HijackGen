using HijackGen.Strings;
using Microsoft.Win32;
using System.Diagnostics;

namespace HijackGen.Plugins.KnownDllsChecker
{
    public sealed class KnownDllsChecker : Plugin
    {
        public override string Name => GUIStrings.PluginNameKnownDllsChecker;
        public override string Description => GUIStrings.PluginDescKnownDllsChecker;

        private const string LastKeyDir = @"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit";
        private const string LastKeyValue = "LastKey";
        private const string KnownDllsDir = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\KnownDlls";

        public override void Execute()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(LastKeyDir))
            {
                key.SetValue(LastKeyValue, KnownDllsDir, RegistryValueKind.String);
            }
            Process.Start(new ProcessStartInfo
            {
                FileName = "regedit.exe",
                UseShellExecute = true
            });
        }
    }
}
