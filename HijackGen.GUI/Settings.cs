using System.Configuration;
using System.Diagnostics;

namespace HijackGen.GUI
{
    internal class Settings : ApplicationSettingsBase
    {
        public static Settings Default { get; } = (Settings)Synchronized(new Settings());

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string DllPath
        {
            get => (string)this[nameof(DllPath)];
            set => this[nameof(DllPath)] = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string SaveDir
        {
            get => (string)this[nameof(SaveDir)];
            set => this[nameof(SaveDir)] = value;
        }
    }
}
