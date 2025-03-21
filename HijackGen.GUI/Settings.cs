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
        public string DefSavePath
        {
            get => (string)this[nameof(DefSavePath)];
            set => this[nameof(DefSavePath)] = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string HSavePath
        {
            get => (string)this[nameof(HSavePath)];
            set => this[nameof(HSavePath)] = value;
        }
    }
}
