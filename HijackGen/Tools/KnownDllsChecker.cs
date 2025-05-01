using Microsoft.Win32;
using System.Diagnostics;

namespace HijackGen.Tools
{
    public sealed class KnownDllsChecker
    {
        private const string LastKeyDir = @"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit";

        private const string LastKeyValue = "LastKey";

        private const string KnownDllsDir = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\KnownDlls";

        public void Check()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(LastKeyDir))
            {
                key.SetValue(LastKeyValue, KnownDllsDir, RegistryValueKind.String);
            }
            Process.Start("regedit.exe");
        }
    }
}
